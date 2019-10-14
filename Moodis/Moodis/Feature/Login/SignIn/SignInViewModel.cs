using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Network.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Moodis.Feature.Login
{
    public class SignInViewModel
    {
        public static List<User> userList;
        public static User currentUser;

        public bool Authenticate(string username, string password)
        {
            userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }
            currentUser = userList.Find(user => user.username == username && user.password == Crypto.CalculateMD5Hash(password));

            if (currentUser == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Response> AuthenticateWithFace(string imagePath)
        {
            List<Person> identifiedPersons = null;
            try
            {
                identifiedPersons = await Face.Instance.IdentifyPersons(imagePath) as List<Person>;
            }
            catch
            {
                return Response.ApiError;
            }

            currentUser = userList.Find(user => user.username == identifiedPersons[0].Name); //TODO change this to multiple user recognition.

            if (currentUser == null)
            {
                return Response.UserNotFound;
            }
            return Response.OK;
        }
    }
}
