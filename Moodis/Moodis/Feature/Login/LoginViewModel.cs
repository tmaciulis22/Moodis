using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Feature.Login
{
    class LoginViewModel
    {
        
        public User Authenticate(String username, String password)
        {
            List<User> userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (userList.Exists(x => x.getUsername() == username && x.getPassword() == password))
            {
                return userList.Find(x => x.getUsername() == username && x.getPassword() == password);
            }
            else
            {
                return null;
            }
        }
    }
}
