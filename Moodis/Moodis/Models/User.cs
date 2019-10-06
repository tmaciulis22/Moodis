using Moodis.Ui;
using System;
using System.Collections.Generic;

namespace Moodis.Feature.Login
{
    [Serializable]
    class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public List<ImageInfo> imageStats = new List<ImageInfo>();

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public void addImage(ImageInfo image)
        {
            imageStats.Add(image);
        }

    }
}
