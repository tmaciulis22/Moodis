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
       /* public struct Emotion
        {
            public string name { get; set; }
            public int confidence;
        };*/
        public string imagePath { get; set; }
        public Hashtable Emotions = new Hashtable();
        private string id;
        private string age;
        private string gender;
        public ImageInfo()
        {

        }
        public void setImageInfo(String jsonInfo)
        {
            jsonInfo = jsonInfo.Replace("["," ").Replace("]","");
            dynamic data = JObject.Parse(jsonInfo);
            Console.WriteLine(data);
            this.id = data.faceId;
            this.age = data.faceAttributes.age;
            this.gender = data.faceAttributes.gender;
            Emotions.Clear();
            addEmotions(data);
        }
        private void addEmotions(dynamic data)
        {
            Emotions.Add("anger",(string) data.faceAttributes.emotion.anger);
            Emotions.Add("contempt", (string)data.faceAttributes.emotion.contempt);
            Emotions.Add("disgust", (string)data.faceAttributes.emotion.disgust);
            Emotions.Add("fear", (string)data.faceAttributes.emotion.fear);
            Emotions.Add("happiness", (string)data.faceAttributes.emotion.happiness);
            Emotions.Add("neutral", (string)data.faceAttributes.emotion.neutral);
            Emotions.Add("sadness", (string)data.faceAttributes.emotion.sadness);
            Emotions.Add("surprise", (string)data.faceAttributes.emotion.surprise);
            /*
            Emotion anger = new Emotion();
            anger.name = "anger";
            anger.confidence = data.faceAttributes.emotion.anger;
            Emotions.Add(anger.name, anger);

            Emotion contempt = new Emotion();
            contempt.name = "contempt";
            contempt.confidence = data.faceAttributes.emotion.contempt;
            Emotions.Add(contempt.name, contempt);

            Emotion disgust = new Emotion();
            disgust.name = "disgust";
            disgust.confidence = data.faceAttributes.emotion.disgust;
            Emotions.Add(disgust.name, disgust);

            Emotion fear = new Emotion();
            fear.name = "fear";
            fear.confidence = data.faceAttributes.emotion.fear;
            Emotions.Add(fear.name, fear);

            Emotion happiness = new Emotion();
            happiness.name = "happiness";
            happiness.confidence = data.faceAttributes.emotion.happiness;
            Emotions.Add(happiness.name, happiness);

            Emotion neutral = new Emotion();
            neutral.name = "neutral";
            neutral.confidence = data.faceAttributes.emotion.neutral;
            Emotions.Add(neutral.name, neutral);

            Emotion sadness = new Emotion();
            sadness.name = "sadness";
            sadness.confidence = data.faceAttributes.emotion.sadness;
            Emotions.Add(sadness.name, sadness);

            Emotion surprise = new Emotion();
            surprise.name = "surprise";
            surprise.confidence = data.faceAttributes.emotion.surprise;
            Emotions.Add(surprise.name, surprise);
            */
        }
    }
}
