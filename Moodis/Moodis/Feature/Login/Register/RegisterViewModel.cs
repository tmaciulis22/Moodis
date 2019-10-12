using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Feature.Login.Register
{
    public class RegisterViewModel
    {
        public static List<User> userList;

        public bool AddUser(string username, string password)
        {
            userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (userList.Exists(x => x.username == username))
            {
                return false;
            }
            else
            {
                User user = new User(username, Crypto.CalculateMD5Hash(password));
                userList.Add(user);
                return Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", userList);
            }
        }
    }
}
