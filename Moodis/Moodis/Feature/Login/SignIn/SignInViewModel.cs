using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public static User getUser(string username)
        {
            userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }
            return userList.Find(user => user.username == username);
        }
    }
}
