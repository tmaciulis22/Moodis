using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiMoodis.Models
{
    public class UserFE
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDoctor { get; set; }
        public string PersonGroupId { get; set; }
        public string PersonId { get; set; }
    }
}