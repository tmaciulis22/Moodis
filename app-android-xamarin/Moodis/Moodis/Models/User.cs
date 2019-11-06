using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Database;
using Moodis.Ui;
using SQLite;
using System;
using System.Collections.Generic;

namespace Moodis.Feature.Login
{
    [Serializable]
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string personGroupId { get; set; }
        public string groupName { get; set; }

        [Ignore]
        public Person FaceApiPerson { get; set; }

        public User()
        {
            this.groupName = "";
        }
        public User(string username, string password) : this()
        {
            this.username = username;
            this.password = password;
        }
    }
}
