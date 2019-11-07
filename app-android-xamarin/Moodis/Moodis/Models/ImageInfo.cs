using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Feature.SignIn;
using SQLite;
using System;
using System.Collections.Generic;
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

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }
        public List<Emotion> emotions;
        public DateTime ImageDate { get; set; }
        private double? age;
        private Gender? gender;

        public void SetImageInfo(DetectedFace face)
        {
            UserId = SignInViewModel.currentUser.Id;
            age = face.FaceAttributes.Age;
            gender = face.FaceAttributes.Gender;
            ImageDate = DateTime.Now;
            AddEmotions(face.FaceAttributes.Emotion);
        }

        private void AddEmotions(Microsoft.Azure.CognitiveServices.Vision.Face.Models.Emotion detectedEmotions)
        {
            emotions = new List<Emotion>(8);

            var properties = detectedEmotions.GetType().GetProperties().ToList();
            properties.ForEach(property =>
            {
                var index = properties.IndexOf(property);
                emotions.Add(new Emotion
                {
                    name = property.Name,
                    confidence = (double)property.GetValue(detectedEmotions, null),
                });
            });
        }
        public override string ToString()
        {
            return ImageDate.ToString("yyyy/MM/dd HH:mm");
        }
    }
}
