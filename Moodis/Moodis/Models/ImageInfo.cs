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
        private DateTime imageDate;
        private string id;
        private string age;
        private string gender;

        public void SetImageInfo(String jsonInfo)
        {
            dynamic data = JObject.Parse(jsonInfo);
            imageDate = DateTime.Now;
            id = data.faceId;
            age = data.faceAttributes.age;
            gender = data.faceAttributes.gender;
            AddEmotions(data);
        }

        private void AddEmotions(dynamic data)
        {
            emotions = new Emotion[8];
            int counter = 0;
            foreach(dynamic emotion in data.faceAttributes.emotion)
            {
                emotions[counter].name = (string) emotion.Name;
                emotions[counter].confidence = (double) emotion.Value;
                counter++;
            }
        }
    }
}
