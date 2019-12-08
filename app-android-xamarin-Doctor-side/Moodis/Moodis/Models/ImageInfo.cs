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
            [PrimaryKey]
            public string Id { get; set; }
            public string ImageId { get; set; }
            public string Name { get; set; }
            public double Confidence { get; set; }

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                var otherEmotion = (Emotion)obj;
                return Confidence.CompareTo(otherEmotion.Confidence);
            }
        }

        [PrimaryKey]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public List<Emotion> emotions;
        public DateTime ImageDate { get; set; }
        public string DateAsString { get; set; }
        private double? Age;
        private Gender? Gender;

        public ImageInfo()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void SetImageInfo(DetectedFace face)
        {
            UserId = SignInViewModel.currentUser.Id;
            Age = face.FaceAttributes.Age;
            Gender = face.FaceAttributes.Gender;
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
                emotions.Add(new Emotion {
                    Id = Guid.NewGuid().ToString(),
                    Name = property.Name,
                    Confidence = (double)property.GetValue(detectedEmotions, null),
                    ImageId = Id
                });
            });
        }
    }
}
