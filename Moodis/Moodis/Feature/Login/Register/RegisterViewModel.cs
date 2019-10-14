using Moodis.Constants.Enums;
using Moodis.Network.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Moodis.Feature.Login.Register
{
    public class RegisterViewModel
    {
        private const int RequiredNumberOfPhotos = 3;

        public static List<User> userList;
        public User currentUser;
        internal int photosTaken = 0;

        public async Task<Response> AddUser(string username, string password)
        {
            userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (userList.Exists(x => x.username == username))
            {
                return Response.UserExists;
            }
            else
            {
                currentUser = new User(username, Crypto.CalculateMD5Hash(password));
                currentUser.personGroupId = Guid.NewGuid().ToString();

                var newFaceApiPerson = await Face.Instance.CreateNewPerson(currentUser.personGroupId, username);

                if (newFaceApiPerson != null)
                {
                    currentUser.faceApiPerson = newFaceApiPerson;
                }
                else
                {
                    return Response.ApiError;
                }

                userList.Add(currentUser);

                var wasSuccessful = Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", userList);

                if(wasSuccessful == true)
                {
                    return Response.OK;
                } 
                else
                {
                    return Response.SerializationError;
                }
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
    }
}
