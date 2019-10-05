using Moodis.Ui;
using System;
using System.Collections.Generic;

namespace Moodis.Feature.Login
{
    [Serializable]
    class User
    {
        public String username { get; set; }
        public String password { get; set; }
        public List<ImageInfo> imageStats = new List<ImageInfo>();

        public User(String username, String password)
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
