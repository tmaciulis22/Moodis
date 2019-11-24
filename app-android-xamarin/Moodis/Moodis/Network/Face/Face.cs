using Android.Util;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Extensions;
using Moodis.Feature.Login;
using Moodis.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Moodis.Network.Face
{
    public sealed class Face
    {
        private Face() { }

        private static string SUBSCRIPTION_KEY = Secrets.ApiKey;
        private static string ENDPOINT = Secrets.FaceEndpoint;
        private int TRAIN_WAIT_TIME_DELAY = 1000;
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

        private async Task<IList<DetectedFace>> DetectFaceEmotions(string imageFilePath)
        {
            imageFilePath.RotateImage();

            IList<FaceAttributeType> faceAttributes = new FaceAttributeType[]
            {
                FaceAttributeType.Gender,
                FaceAttributeType.Age,
                FaceAttributeType.Emotion
            };

            try
            {
                using Stream imageFileStream = File.OpenRead(imageFilePath);
                var detectedFaces = await faceClient.Face.DetectWithStreamAsync(imageFileStream, true, false, faceAttributes);

                if (detectedFaces.IsNullOrEmpty())
                {
                    return null;
                }
                else
                {
                    return detectedFaces;
                }
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

        public async Task<DetectedFace> DetectUserEmotions(string imageFilePath, string personGroupId, string username)
        {
            var detectedFaces = await DetectFaceEmotions(imageFilePath);
            var faceIds = detectedFaces.Select(face => face.FaceId.Value).ToList();
            var identifiedPersons = await faceClient.Face.IdentifyAsync(faceIds, personGroupId);

            if (identifiedPersons.IsNullOrEmpty())
            {
                return null;
            }

            foreach (var identifiedPerson in identifiedPersons)
            {
                if (identifiedPerson.Candidates.Count != 0)
                {
                    var candidateId = identifiedPerson.Candidates[0].PersonId;
                    var person = await faceClient.PersonGroupPerson.GetAsync(personGroupId, candidateId);

                    if (person.Name == username)
                    {
                        return detectedFaces.First(face => face.FaceId == identifiedPerson.FaceId);
                    }
                }
            }

            return null;
        }

        public async Task<IList<Person>> IdentifyPersons(string imageFilePath, Action<List<DetectedFace>> callback)
        {
            var detectedFaces = await DetectFaceEmotions(imageFilePath);
            callback(detectedFaces.ToList<DetectedFace>());

            var faceIds = detectedFaces.Select(face => face.FaceId.Value).ToList();

            var personsList = new List<Person>();
            var personGroups = await faceClient.PersonGroup.ListAsync();

            foreach (var group in personGroups)
            {
                var identifiedPersons = await faceClient.Face.IdentifyAsync(faceIds, group.PersonGroupId);
                //l
                if (identifiedPersons.IsNullOrEmpty())
                {
                    continue;
                }

                foreach (var identifiedPerson in identifiedPersons)
                {
                    if (identifiedPerson.Candidates.Count != 0)
                    {
                        var candidateId = identifiedPerson.Candidates[0].PersonId;
                        var person = await faceClient.PersonGroupPerson.GetAsync(group.PersonGroupId, candidateId);

                        personsList.AddNonNull(person);
                    }
                }
            }
            return personsList;
        }

        //returns true if one or more users have accounts false otherwise.
        public async Task<Boolean> MultipleAccounts(string imageFilePath, Action<List<DetectedFace>> callback)
        {
            var detectedFaces = await DetectFaceEmotions(imageFilePath);
            callback(detectedFaces.ToList<DetectedFace>());

            var faceIds = detectedFaces.Select(face => face.FaceId.Value).ToList();
            var personGroups = await faceClient.PersonGroup.ListAsync();

            foreach (var face in faceIds)
            {
                int counter = 0;

                foreach (var group in personGroups)
                {
                    VerifyResult identifiedPerson = await faceClient.Face.VerifyFaceToPersonAsync(face, new Guid(group.PersonGroupId));
                    if (identifiedPerson.Confidence > 0.4)
                    {
                        counter++;
                    } else if (counter > 1) {
                        return true;
                     }
                }
            }
            return false;
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

        public async Task<bool> AddFaceToPerson(string imageFilePath, string personGroupId, User user)
        {
            try
            {
                imageFilePath.RotateImage();

                using Stream imageFileStream = File.OpenRead(imageFilePath);
                await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(personGroupId,
                    user.FaceApiPerson.PersonId, imageFileStream);
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

        public async Task<bool> TrainPersonGroup(string personGroupId)
        {
            try
            {
                await faceClient.PersonGroup.TrainAsync(personGroupId);
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.StackTrace);
                return false;
            }

            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await faceClient.PersonGroup.GetTrainingStatusAsync(personGroupId);

                if (trainingStatus.Status != TrainingStatusType.Running)
                {
                    return true;
                }

                await Task.Delay(TRAIN_WAIT_TIME_DELAY);
            }
        }

        public async Task<bool> DeletePerson(string personGroupId)
        {
            try
            {
                await faceClient.PersonGroup.DeleteAsync(personGroupId);//TODO Change to deleting only one person from group and move deletion of group to other method, when that group is empty and no longer used
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
