using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Extensions;
using Moodis.Feature.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Moodis.Network.Face
{
    public sealed class Face
    {
        private Face(){}

        private static string SUBSCRIPTION_KEY = Environment.GetEnvironmentVariable("FACE_SUBSCRIPTION_KEY");
        private static string ENDPOINT = Environment.GetEnvironmentVariable("FACE_ENDPOINT");
        private int TRAIN_WAIT_TIME_DELAY = 1000;
        private string API_ERROR = "API Error";
        private string GENERAL_ERROR = "General Error";

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
            IList<FaceAttributeType> faceAttributes = new FaceAttributeType[]
            {
                FaceAttributeType.Gender,
                FaceAttributeType.Age,
                FaceAttributeType.Emotion
            };

            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
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
            }
            catch (APIErrorException apiException)
            {
                Console.WriteLine(API_ERROR + " " + apiException.StackTrace);
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(GENERAL_ERROR + " " + exception.StackTrace);
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

        public async Task<IList<Person>> IdentifyPersons(string imageFilePath)
        {
            var detectedFaces = await DetectFaceEmotions(imageFilePath);
            var faceIds = detectedFaces.Select(face => face.FaceId.Value).ToList();

            var personsList = new List<Person>();
            var personGroups = await faceClient.PersonGroup.ListAsync();

            foreach (var group in personGroups)
            {
                var identifiedPersons = await faceClient.Face.IdentifyAsync(faceIds, group.PersonGroupId);

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
                Console.WriteLine(API_ERROR + " " + apiException.StackTrace);
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(GENERAL_ERROR + " " + exception.StackTrace);
                return null;
            }
        }

        public async Task<bool> AddFaceToPerson(string imageFilePath, string personGroupId, User user)
        {
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(
                            personGroupId, user.faceApiPerson.PersonId, imageFileStream);
                    return true;
                }
            }
            catch (APIErrorException apiException)
            {
                Console.WriteLine(API_ERROR + " " + apiException.StackTrace);
                return false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(GENERAL_ERROR + " " + exception.StackTrace);
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
                Console.WriteLine(ex.StackTrace);
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
    }
}
