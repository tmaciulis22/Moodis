using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Linq;


namespace Moodis.Ui
{
    [Serializable]
    public class ImageInfo
    {
        [Serializable]
        public struct Emotion : IComparable
        {
            public string name;
            public double confidence;

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                var otherEmotion = (Emotion)obj;
                return confidence.CompareTo(otherEmotion.confidence);
            }
        }

        public string ImagePath { get; set; }
        public Emotion[] emotions;
        public DateTime imageDate { get; set; }
        private string id;
        private double? age;
        private Gender? gender;

        public void SetImageInfo(DetectedFace face)
        {
            id = face.FaceId.ToString();
            age = face.FaceAttributes.Age;
            gender = face.FaceAttributes.Gender;
            imageDate = DateTime.Now;
            AddEmotions(face.FaceAttributes.Emotion);
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
