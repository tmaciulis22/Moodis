using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.groupName = "";
        }
        public void addImage(ImageInfo image)
        {
            imageStats.Add(image);
        }
    }
}
