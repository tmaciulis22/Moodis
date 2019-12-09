using Android.Arch.Lifecycle;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Extensions;
using Moodis.Feature.Login;
using Moodis.Network;
using Moodis.Network.Face;
using Moodis.Network.Requests;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Moodis.Feature.SignIn
{
    public class SignInViewModel : ViewModel
    {
        public static User currentUser;

        public async Task<Response> Authenticate(string username, string password)
        {
            try
            {
                currentUser = await API.UserEndpoint.LoginUser(new LoginRequest(username, password));
                return Response.OK;
            }
            catch (ApiException ex)
            {
                var statusCode = ex.StatusCode;
                return statusCode switch
                {
                    HttpStatusCode.NotFound => Response.BadCredentials,
                    HttpStatusCode.BadRequest => Response.BadCredentials,
                    _ => Response.ApiError
                };
            }
        }
    }
}
