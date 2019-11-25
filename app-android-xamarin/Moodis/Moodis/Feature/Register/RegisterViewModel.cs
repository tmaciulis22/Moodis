using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Extensions;
using Moodis.Feature.Login;
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
            var wasSuccessful = await Face.Instance.DeletePerson(currentUser.PersonGroupId);
            if (wasSuccessful)
            {
                return Response.OK;
            }
            else
            {
                return Response.ApiError;
            }
        }

        public async Task<Response> AddFaceToPerson(string imagePath)
        {
            bool wasSuccessful = await Face.Instance.AddFaceToPerson(imagePath, currentUser.PersonGroupId, currentUser);

            if (wasSuccessful)
            {
                photosTaken++;

                if (photosTaken == RequiredNumberOfPhotos)
                {
                    try
                    {
                        await Face.Instance.TrainPersonGroup(currentUser.PersonGroupId);
                        return Response.RegistrationDone;
                    }
                    catch
                    {
                        return Response.ApiTrainingError;
                    }
                }

                return Response.OK;
            }
            else
            {
                return Response.ApiError;
            }
        }

        public void UpdateLocalStorage()
        {
            userList = DatabaseModel.FetchUsers();
        }

        internal async Task<Response> AuthenticateFace(string imagePath)
        {
            List<DetectedFace> detectedFaces = null;
            void setFace(List<DetectedFace> face) => detectedFaces = face;
            DetectedFace face;
            bool userExists;
            try
            {
                userExists = await Face.Instance.MultipleAccounts(imagePath, setFace);
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
    }
}