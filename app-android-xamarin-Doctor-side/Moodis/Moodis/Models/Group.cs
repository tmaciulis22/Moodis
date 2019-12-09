using System;
using System.Collections.Generic;
using System.Linq;
using Java.Lang;
using SQLite;

namespace Moodis.Feature.Group
{
    class Group
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string DoctorId { get; set; }

        public string Groupname { get; set; }

        public Group()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Group(string name, string firstUser) : this()
        {
            Groupname = name;
            DoctorId = firstUser;
        }
    }
}