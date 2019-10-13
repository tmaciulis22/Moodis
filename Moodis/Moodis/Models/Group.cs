using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moodis.Feature.Login;

namespace Moodis.Feature.Group
{
    [Serializable]
    public class Group
    {
        public string name { get; set; }
        public int managerID { get; set; }
        public List<int> usersIds = new List<int>();

        public Group(string name, int managerID)
        {
            this.name = name;
            this.managerID = managerID;
        }

        public void addUserToGroup(int userID)
        {
            if (userID != this.managerID)
            {
                usersIds.Add(userID);
            }
        }

        public void removeUserFromGroup(int userID)
        {
            if (usersIds.Contains(userID))
            {
                usersIds.Remove(userID);
            }
        }

        public List<User> getUsers()
        {
            List<User> users = new List<User>();
            List<User> userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (userList.Count != 0) {
                foreach (User user in userList)
                {
                    if (usersIds.Contains(user.userID))
                    {
                        users.Add(user);
                    }
                }
            }
            return users;
        }
    }
}
