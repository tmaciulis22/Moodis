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
        public string ImagePath { get; set; }
        public Hashtable Emotions = new Hashtable();
        private string id;
        private string age;
        private string gender;
        public ImageInfo()
        {
        }
        public void SetImageInfo(String jsonInfo)
        {
            dynamic data = JObject.Parse(jsonInfo);
            this.id = data.faceId;
            this.age = data.faceAttributes.age;
            this.gender = data.faceAttributes.gender;
            Emotions.Clear();
            AddEmotions(data);
        }
        private void AddEmotions(dynamic data)
        {
            Emotions.Add("anger",(string) data.faceAttributes.emotion.anger);
            Emotions.Add("contempt", (string)data.faceAttributes.emotion.contempt);
            Emotions.Add("disgust", (string)data.faceAttributes.emotion.disgust);
            Emotions.Add("fear", (string)data.faceAttributes.emotion.fear);
            Emotions.Add("happiness", (string)data.faceAttributes.emotion.happiness);
            Emotions.Add("neutral", (string)data.faceAttributes.emotion.neutral);
            Emotions.Add("sadness", (string)data.faceAttributes.emotion.sadness);
            Emotions.Add("surprise", (string)data.faceAttributes.emotion.surprise);
        }
    }
}
