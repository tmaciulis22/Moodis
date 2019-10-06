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
    public class ImageInfo
    {
        public struct Emotion
        {
            public string name;
            public double confidence;
        }

        public string ImagePath { get; set; }
        public Emotion[] emotions;
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
            emotions[0].name = "Anger";
            emotions[0].confidence = detectedEmotions.Anger;
            emotions[1].name = "Contempt";
            emotions[1].confidence = detectedEmotions.Contempt;
            emotions[2].name = "Disgust";
            emotions[2].confidence = detectedEmotions.Disgust;
            emotions[3].name = "Fear";
            emotions[3].confidence = detectedEmotions.Fear;
            emotions[4].name = "Happiness";
            emotions[4].confidence = detectedEmotions.Happiness;
            emotions[5].name = "Neutral";
            emotions[5].confidence = detectedEmotions.Neutral;
            emotions[6].name = "Sadness";
            emotions[6].confidence = detectedEmotions.Sadness;
            emotions[7].name = "Neutral";
            emotions[7].confidence = detectedEmotions.Surprise;
        }
    }
}
