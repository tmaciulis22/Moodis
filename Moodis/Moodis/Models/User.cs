using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Ui;
using System;
using System.Collections.Generic;

namespace Moodis.Feature.Login
{
    [Serializable]
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string personGroupId { get; set; }
        public string groupName { get; set; }

        public List<ImageInfo> imageStats = new List<ImageInfo>();
        [field: NonSerialized]
        public Person faceApiPerson { get; set; }

        public User(string username, string password,string groupName = "")
        {
            this.username = username;
            this.password = password;
            this.groupName = groupName;
        }
        public void addImage(ImageInfo image)
        {
            imageStats.Add(image);
        }
    }
}
