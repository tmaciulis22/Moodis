using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Login;

namespace Moodis.Feature.Register
{
    class RegisterViewModel
    {
        public static List<User> userList = DatabaseModel.FetchData();
        public User currentUser;
        public async Task<Response> AddUser(string username, string password)
        {
            if(userList.Exists(userFromList => userFromList.username == username))
            {
                return Response.UserExists;
            }
            else
            {
                currentUser = new User(username, Crypto.CalculateMD5Hash(password));
                DatabaseModel.AddUserToDatabase(currentUser);
                UpdateLocalStorage();
                return Response.OK;
            }
        }
        public void UpdateLocalStorage()
        {
            userList = DatabaseModel.FetchData();
        }
    }
}