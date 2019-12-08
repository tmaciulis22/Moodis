using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using SQLite;
using System;

namespace Moodis.Feature.Login
{
    [Serializable]
    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PersonGroupId { get; set; }
        public string GroupName { get; set; }
        public string PersonId { get; set; }
        public bool IsDoctor { get; set; }

        [Ignore]
        public Person FaceApiPerson { get; set; }
        public bool IsSelected { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            IsDoctor = true;
            GroupName = "";
        }
        public User(string username, string password) : this()
        {
            Username = username;
            Password = password;
        }
    }
}
