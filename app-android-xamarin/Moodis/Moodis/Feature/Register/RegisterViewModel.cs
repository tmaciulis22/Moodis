using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Login;
using Moodis.Feature.SignIn;
using Moodis.Network;
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
            SignInViewModel.currentUser = await API.UserEndpoint.RegisterUser(new User(username, password));

            if (SignInViewModel.currentUser == null)
            {
                return Response.UserExists;
            }
            else
            {
                var newFaceApiPerson = await Face.Instance.CreateNewPerson(SignInViewModel.currentUser.PersonGroupId, username);

                if (newFaceApiPerson != null)
                {
                    SignInViewModel.currentUser.PersonId = Convert.ToString(newFaceApiPerson.PersonId);
                    await API.UserEndpoint.UpdateUser(SignInViewModel.currentUser);
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
            await API.UserEndpoint.DeleteUser(SignInViewModel.currentUser.Id);
            return await Face.Instance.DeletePerson(SignInViewModel.currentUser.PersonGroupId);
        }

        //TODO MOVE THIS TO FACE CLASS
        public async Task<Response> AddFaceToPerson(string imagePath)
        {
            var response = await Face.Instance.AddFaceToPerson(imagePath, SignInViewModel.currentUser.PersonGroupId, SignInViewModel.currentUser.PersonId);

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
    }
}