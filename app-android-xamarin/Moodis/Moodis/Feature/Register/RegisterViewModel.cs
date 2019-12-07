using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Login;
using Moodis.Feature.SignIn;
using Moodis.Network.Face;
using System;
using System.Threading.Tasks;

namespace Moodis.Feature.Register
{
    public class RegisterViewModel
    {
        public const int RequiredNumberOfPhotos = 3;

        internal int photosTaken = 0;

        public async Task<Response> AddUser(string username, string password)
        {
            if(SignInViewModel.userList.Exists(userFromList => userFromList.Username == username))
            {
                return Response.UserExists;
            }
            else
            {
                SignInViewModel.currentUser = new User(username, Crypto.CalculateMD5Hash(password))
                {
                    PersonGroupId = Guid.NewGuid().ToString()
                };
                DatabaseModel.AddUserToDatabase(SignInViewModel.currentUser);
                UpdateLocalStorage();

                var newFaceApiPerson = await Face.Instance.CreateNewPerson(SignInViewModel.currentUser.PersonGroupId, username);

                if (newFaceApiPerson != null)
                {
                    SignInViewModel.currentUser.FaceApiPerson = newFaceApiPerson;
                    SignInViewModel.currentUser.personId = Convert.ToString(SignInViewModel.currentUser.FaceApiPerson.PersonId);
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
            DatabaseModel.DeleteUserFromDatabase(SignInViewModel.currentUser);
            return await Face.Instance.DeletePerson(SignInViewModel.currentUser.PersonGroupId);
        }

        //TODO MOVE THIS TO FACE CLASS
        public async Task<Response> AddFaceToPerson(string imagePath)
        {

            var response = await Face.Instance.AddFaceToPerson(imagePath, SignInViewModel.currentUser.PersonGroupId, SignInViewModel.currentUser);

            if (response == Response.OK)
            {
                photosTaken++;

                if (photosTaken == RequiredNumberOfPhotos)
                {
                    return await Face.Instance.TrainPersonGroup(SignInViewModel.currentUser.PersonGroupId);
                }
            }
            return response;
        }

        public void UpdateLocalStorage()
        {
            SignInViewModel.userList = DatabaseModel.FetchUsers();
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
           return SignInViewModel.userList.Find(user => user.Username == username).Id;
        }
    }
}