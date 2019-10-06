using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Moodis.Ui
{
    [Serializable]
    public class ImageInfo
    {
        [Serializable]
        public struct Emotion
        {
            public string name;
            public double confidence;
        }

        public string ImagePath { get; set; }
        public Emotion[] emotions;
        public DateTime imageDate { get; set; }
        private string id;
        private double? age;
        private Gender? gender;

        public void SetImageInfo(IList<DetectedFace> faceList)
        {
            //TODO change to faceList.ForEach(face => action), when implementing identity feature
            id = faceList[0].FaceId.ToString();
            age = faceList[0].FaceAttributes.Age;
            gender = faceList[0].FaceAttributes.Gender;
            AddEmotions(faceList[0].FaceAttributes.Emotion);
        }

        private void AddEmotions(Microsoft.Azure.CognitiveServices.Vision.Face.Models.Emotion detectedEmotions)
        {
            emotions = new Emotion[8];

            var properties = detectedEmotions.GetType().GetProperties().ToList();
            properties.ForEach(property => {
                var index = properties.IndexOf(property);
                emotions[index].name = property.Name;
                emotions[index].confidence = (double) property.GetValue(detectedEmotions, null);
            });
        }
        public override string ToString()
        {
            return imageDate.ToString("yyyy/MM/dd HH:mm");
        }
    }
}
