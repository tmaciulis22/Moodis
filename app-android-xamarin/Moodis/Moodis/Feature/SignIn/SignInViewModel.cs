using Android.Arch.Lifecycle;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Extensions;
using Moodis.Feature.Login;
using Moodis.Network;
using Moodis.Network.Face;
using Moodis.Network.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moodis.Feature.SignIn
{
    public class SignInViewModel : ViewModel
    {
        public static User currentUser;

        public async Task<bool> Authenticate(string username, string password)
        {
            currentUser = await API.UserEndpoint.LoginUser(new LoginRequest(username, password));

            if (currentUser == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Response> AuthenticateWithFace(string imagePath, Action<DetectedFace> callback)
        {
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

            currentUser = await API.UserEndpoint.GetUser(identifiedPersons.ToArray()[0].Name);

            if (currentUser == null)
            {
                return Response.UserNotFound;
            }

            face = detectedFaces.ToArray()[0];
            callback(face);
            return Response.OK;
        }
    }
}
