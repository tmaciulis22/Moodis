using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Feature.Login;
using Moodis.Feature.SignIn;
using Moodis.Network;
using Moodis.Network.Face;
using Moodis.Network.Requests;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Moodis.Feature.Register
{
    public class RegisterViewModel
    {

        public async Task<Response> AddUser(string username, string password)
        {
            try
            {
                SignInViewModel.currentUser = await API.UserEndpoint.RegisterUser(new RegisterRequest(username, password));
            }
            catch (ApiException ex)
            {
                return ex.StatusCode switch
                {
                    HttpStatusCode.BadRequest => Response.UserExists,
                    _ => Response.ApiError
                };
            }

            var newFaceApiPerson = await Face.Instance.CreateNewPerson(SignInViewModel.currentUser.PersonGroupId, username);

            if (newFaceApiPerson != null)
            {
                SignInViewModel.currentUser.PersonId = Convert.ToString(newFaceApiPerson.PersonId);
                try
                {
                    await API.UserEndpoint.UpdateUser(SignInViewModel.currentUser);
                }
                catch
                {
                    return Response.ApiError;
                }
            }
            else
            {
                return Response.ApiError;
            }
            return Response.OK;
        }

        public async Task<Response> DeleteUser()
        {
            try
            {
                await API.UserEndpoint.DeleteUser(SignInViewModel.currentUser.Id);
            }
            catch
            {
                return Response.ApiError;
            }
            return await Face.Instance.DeletePersonGroup(SignInViewModel.currentUser.PersonGroupId);
        }
    }
}