using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Moodie.Network.Face
{
    public sealed class Face
    {
        private Face(){}

        private const string SubscriptionKey = "e285e7e510e0493bbaa7ffe5ffc2db77";
        private const string UriBase =
            "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";
        private const string RequestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=age,gender,emotion";

        private static Face instance = null;
        public static Face Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Face();
                }
                return instance;
            }
        }

        public async Task<string> SendImageForAnalysis(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = UriBase + "?" + RequestParameters;

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(uri, content);

                return await response.Content.ReadAsStringAsync();
            }
        }

        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                var binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }
}  