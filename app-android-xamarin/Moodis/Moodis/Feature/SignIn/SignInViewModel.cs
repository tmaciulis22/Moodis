using Android.Arch.Lifecycle;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Extensions;
using Moodis.Feature.Login;
using Moodis.Network.Face;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moodis.Feature.SignIn
{
    public class SignInViewModel : ViewModel
    {
        public static List<User> userList;
        public static User currentUser;

        public bool Authenticate(string username, string password)
        {
            FetchUserList();

            currentUser = userList.Find(user => user.Username == username && user.Password == Crypto.CalculateMD5Hash(password));

            if (currentUser == null)
            {
                return false;
            }
            return true;
        }

        //TODO change this to multiple user recognition.
        public async Task<Response> AuthenticateWithFace(string imagePath, Action<DetectedFace> callback)
        {
            FetchUserList();

            List<DetectedFace> detectedFaces = null;
            void setFace(List<DetectedFace> face) => detectedFaces = face;
            DetectedFace face;

            List<Person> identifiedPersons = null;
            try
            {
                identifiedPersons = await Face.Instance.IdentifyPersons(imagePath, setFace) as List<Person>;
            }
            catch (APIErrorException)
            {
                return Response.ApiError;
            }

            if (identifiedPersons.IsNullOrEmpty())
            {
                return Response.UserNotFound;
            }

            currentUser = userList.Find(user => user.Username == identifiedPersons.ToArray()[0].Name);
            face = detectedFaces.ToArray()[0];

            if (currentUser == null)
            {
                return Response.UserNotFound;
            }
            callback(face);
            return Response.OK;
        }

        private void FetchUserList()
        {
            userList = Database.DatabaseModel.FetchUsers();
        }
    }
}
