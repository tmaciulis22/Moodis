using Android.Arch.Lifecycle;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Database;
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
            void setFace(List<DetectedFace> faces) => detectedFaces = faces;
            DetectedFace face;

            List<Person> identifiedPersons = null;
            try
            {
                imagePath.RotateImage();
                identifiedPersons = await Face.Instance.IdentifyPersons(imagePath, setFace) as List<Person>;
            }
            catch
            {
                if (detectedFaces.IsNullOrEmpty())
                {
                    return Response.FaceNotDetected;
                }
                else
                {
                    return Response.ApiError;
                }
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

        //NOTE this method is used only for development and testing purposes, to clear everything in DB and in Face API
        public async Task<Response> DeleteEverything()
        {
            DatabaseModel.DeleteEverything();
            return await Face.Instance.DeleteEverything();
        }

        private void FetchUserList()
        {
            userList = Database.DatabaseModel.FetchUsers();
        }
    }
}
