using Android.Util;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moodis.Network.Face
{
    public sealed class Face
    {
        private Face() { }

        private static string SUBSCRIPTION_KEY = Secrets.FaceApiKey;
        private static string ENDPOINT = Secrets.FaceEndpoint;
        private string API_ERROR = "API Error";
        private string GENERAL_ERROR = "General Error";
        private const string TAG = "Face";

        private static readonly IFaceClient faceClient = new FaceClient(
            new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY),
            new System.Net.Http.DelegatingHandler[] { }
        );

        private static Face instance = null;
        public static Face Instance
        {
            get
            {
                if (instance == null)
                {
                    if (Uri.IsWellFormedUriString(ENDPOINT, UriKind.Absolute))
                    {
                        faceClient.Endpoint = ENDPOINT;
                        instance = new Face();
                    }
                }
                return instance;
            }
        }
        public async Task<Person> CreateNewPerson(string personGroupId, string username)
        {
            try
            {
                await faceClient.PersonGroup.CreateAsync(personGroupId, username); //TODO Change username to group manager's(doctor's) group name of (users)patients

                return await faceClient.PersonGroupPerson.CreateAsync(
                    personGroupId,
                    username
                );
            }
            catch (APIErrorException apiException)
            {
                Log.Error(TAG, API_ERROR + " " + apiException.StackTrace);
                return null;
            }
            catch (Exception exception)
            {
                Log.Error(TAG, GENERAL_ERROR + " " + exception.StackTrace);
                return null;
            }
        }

        public async Task<Response> CreateNewDoctor(string personGroupId, string username)
        {
            try
            {
                await faceClient.PersonGroup.CreateAsync(personGroupId, username);
                return Response.OK;
            }
            catch (APIErrorException apiException)
            {
                Log.Error(TAG, API_ERROR + " " + apiException.StackTrace);
                return Response.ApiError;
            }
            catch (Exception exception)
            {
                Log.Error(TAG, GENERAL_ERROR + " " + exception.StackTrace);
                return Response.ApiError;
            }
        }

        public async Task<Response> DeletePersonGroup(string personGroupId)
        {
            try
            {
                await faceClient.PersonGroup.DeleteAsync(personGroupId);
                return Response.OK;
            }
            catch (APIErrorException apiException)
            {
                Log.Error(TAG, API_ERROR + " " + apiException.StackTrace);
                return Response.ApiError;
            }
            catch (Exception exception)
            {
                Log.Error(TAG, GENERAL_ERROR + " " + exception.StackTrace);
                return Response.GeneralError;
            }
        }

        public async Task<Response> DeleteEverything()
        {
            try
            {
                var listOfPersonGroups = await faceClient.PersonGroup.ListAsync();
                listOfPersonGroups.ToList().ForEach(async group => {
                    await faceClient.PersonGroup.DeleteAsync(group.PersonGroupId);
                });
                return Response.OK;
            }
            catch (APIErrorException apiException)
            {
                Log.Error(TAG, API_ERROR + " " + apiException.StackTrace);
                return Response.ApiError;
            }
            catch (Exception exception)
            {
                Log.Error(TAG, GENERAL_ERROR + " " + exception.StackTrace);
                return Response.GeneralError;
            }
        }

        public async Task<bool> MovePerson(string personGroupId, string personId,string username, string newGroupId)
        {
            try
            {
                await faceClient.PersonGroupPerson.DeleteAsync(personGroupId,Guid.Parse(personId));
                var newFaceApiPerson = await Face.Instance.CreateNewPerson(newGroupId, username);
                return true;
            }
            catch (APIErrorException apiException)
            {
                Log.Error(TAG, API_ERROR + " " + apiException.StackTrace);
                return false;
            }
            catch (Exception exception)
            {
                Log.Error(TAG, GENERAL_ERROR + " " + exception.StackTrace);
                return false;
            }
        }
    }
}
