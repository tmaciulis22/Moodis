using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                        faceClient.Endpoint = ENDPOINT + "/detect";
                        instance = new Face();
                    }
                }
                return instance;
            }
        }

        public async Task<IList<DetectedFace>> DetectFaceEmotions(string imageFilePath)
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
                    imageFileStream.Position = 0;
                    return await faceClient.Face.DetectWithStreamAsync(imageFileStream, true, false, faceAttributes);
                }
            }
            catch (APIErrorException apiException)
            {
                Console.WriteLine(apiException.StackTrace);
                return new List<DetectedFace>();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return new List<DetectedFace>();
            }
        }
    }
}
