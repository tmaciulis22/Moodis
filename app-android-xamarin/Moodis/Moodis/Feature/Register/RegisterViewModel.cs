using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Login;
using Moodis.Network.Face;

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
            if(userList.Exists(userFromList => userFromList.username == username))
            {
                return Response.UserExists;
            }
            else
            {
                currentUser = new User(username, Crypto.CalculateMD5Hash(password));
                currentUser.personGroupId = Guid.NewGuid().ToString();
                DatabaseModel.AddUserToDatabase(currentUser);
                UpdateLocalStorage();

                var newFaceApiPerson = await Face.Instance.CreateNewPerson(currentUser.personGroupId, username);

                if (newFaceApiPerson != null)
                {
                    currentUser.faceApiPerson = newFaceApiPerson;
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
            var wasSuccessful = await Face.Instance.DeletePerson(currentUser.personGroupId);
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
            bool wasSuccessful = await Face.Instance.AddFaceToPerson(imagePath, currentUser.personGroupId, currentUser);

            if (wasSuccessful)
            {
                photosTaken++;

                if (photosTaken == RequiredNumberOfPhotos)
                {
                    try
                    {
                        await Face.Instance.TrainPersonGroup(currentUser.personGroupId);
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
            userList = Database.DatabaseModel.FetchUsers();
        }
    }
}