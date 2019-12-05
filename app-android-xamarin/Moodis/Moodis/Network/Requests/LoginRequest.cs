using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Moodis.Network.Requests
{
    class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private LoginRequest() { }

        public LoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}