using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Extensions;
using Moodis.Feature.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Moodis.Network.Face
{
    public sealed class Face
    {
        private Face(){}

        private static string SUBSCRIPTION_KEY = Environment.GetEnvironmentVariable("FACE_SUBSCRIPTION_KEY");
        private static string ENDPOINT = Environment.GetEnvironmentVariable("FACE_ENDPOINT");
        private int TRAIN_WAIT_TIME_DELAY = 1000;

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

        public async Task<DetectedFace?> DetectFaceEmotions(string imageFilePath, string personGroupId, string username)
        {
            IList<FaceAttributeType> faceAttributes = new FaceAttributeType[]
            {
                FaceAttributeType.Gender,
                FaceAttributeType.Age,
                FaceAttributeType.Emotion
            };
            IList<DetectedFace> detectedFaces = null;

            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    detectedFaces = await faceClient.Face.DetectWithStreamAsync(imageFileStream, true, false, faceAttributes);
                }
            }
            catch (APIErrorException apiException)
            {
                Console.WriteLine(apiException.StackTrace);
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return null;
            }

            var faceIds = detectedFaces.Select(face => face.FaceId.Value).ToList();
            var identifiedPersons = await faceClient.Face.IdentifyAsync(faceIds, personGroupId);

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

        public async Task<Person> CreateNewPerson(string personGroupId, string username)
        {
            await faceClient.PersonGroup.CreateAsync(personGroupId, "My Friends");

            return await faceClient.PersonGroupPerson.CreateAsync(
                personGroupId,
                username
            );
        }

        public async void AddFaceToPerson(string imageFilePath, string personGroupId, User user)
        {
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
                            personGroupId, user.faceApiPerson.PersonId, imageFileStream);
                }
            }
            catch (APIErrorException apiException)
            {
                Console.WriteLine(apiException.StackTrace);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }

        public async Task<bool> TrainPersonGroup(string personGroupId)
        {
            await faceClient.PersonGroup.TrainAsync(personGroupId);

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
    }
}
