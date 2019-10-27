using System;
using System.Collections.Generic;

namespace Moodis.Feature.Group
{
    [Serializable]
    public class Group
    {
        public string name { get; set; }
        public string manager { get; set; }
        public List<string> users = new List<string>();

        public Group(string name, string managerID)
        {
            this.name = name;
            this.manager = managerID;
        }

        public void addUserToGroup(string username)
        {
            if (username != this.manager)
            {
                users.Add(username);
            }
        }

        public void removeUserFromGroup(string username)
        {
            if (users.Contains(username))
            {
                users.Remove(username);
            }
        }

    }
}
