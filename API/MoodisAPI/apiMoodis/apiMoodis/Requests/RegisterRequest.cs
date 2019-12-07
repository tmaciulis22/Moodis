using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiMoodis.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDoctor { get; set; }
    }
}