using System;
using System.Collections.Generic;
using System.IO;

namespace Moodis.Feature.Login
{
    class SignInViewModel
    {
        public static List<User> userList;
        public static User currentUser;

        public User Authenticate(string username, string password)
        {
            userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }
            currentUser = userList.Find(user => user.username == username && user.password == Crypto.CalculateMD5Hash(password));
            return currentUser;
        }
    }
}
