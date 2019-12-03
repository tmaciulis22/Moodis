using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Login;
using Moodis.Feature.SignIn;
using Moodis.Network.Face;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moodis.Feature.Register
{
    public class RegisterViewModel
    {
        public const int RequiredNumberOfPhotos = 3;

        public static List<User> userList = DatabaseModel.FetchUsers();
        public static User currentUser;
        internal int photosTaken = 0;

        public async Task<Response> AddUser(string username, string password)
        {
            if(userList.Exists(userFromList => userFromList.Username == username))
            {
                return Response.UserExists;
            }
            else
            {
                currentUser = new User(username, Crypto.CalculateMD5Hash(password))
                {
                    PersonGroupId = Guid.NewGuid().ToString()
                };
                DatabaseModel.AddUserToDatabase(currentUser);
                UpdateLocalStorage();

                var newFaceApiPerson = await Face.Instance.CreateNewPerson(currentUser.PersonGroupId, username);

                if (newFaceApiPerson != null)
                {
                    currentUser.FaceApiPerson = newFaceApiPerson;
                    currentUser.personId = Convert.ToString(currentUser.FaceApiPerson.PersonId);
                }
                else
                {
                    return Response.ApiError;
                }

                return Response.OK;
            }
        }

        public async Task<Response> DeleteUser()
        {
            DatabaseModel.DeleteUserFromDatabase(currentUser);
            return await Face.Instance.DeletePerson(currentUser.PersonGroupId);
        }

        //TODO MOVE THIS TO FACE CLASS
        public async Task<Response> AddFaceToPerson(string imagePath)
        {
            if (currentUser == null)
            {
                currentUser = SignInViewModel.currentUser;
            }

            var response = await Face.Instance.AddFaceToPerson(imagePath, currentUser.PersonGroupId, currentUser);

            if (response == Response.OK)
            {
                photosTaken++;

                if (photosTaken == RequiredNumberOfPhotos)
                {
                    return await Face.Instance.TrainPersonGroup(currentUser.PersonGroupId);
                }
            }
            return response;
        }

        public void UpdateLocalStorage()
        {
            userList = DatabaseModel.FetchUsers();
        }

        public async Task<Response> AuthenticateFace(string imagePath)
        {
            bool userExists;
            try
            {
                userExists = await Face.Instance.MultipleAccounts(imagePath, null);
            }
            catch (APIErrorException e)
            {
                userExists = false;
                Console.WriteLine(e.StackTrace);
                return Response.ApiError;
            }

            if (userExists)
            {
                return Response.UserExists;
            }

            return Response.UserNotFound;
        }

        public static string GetIdByUsername(string username)
        {
           return userList.Find(user => user.Username == username).Id;
        }
    }
}