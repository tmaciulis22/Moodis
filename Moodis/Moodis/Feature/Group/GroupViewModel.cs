using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moodis.Feature.Login;

namespace Moodis.Feature.Group
{
    class GroupViewModel
    {
        public static Group group;

        public Group createGroup(string name)
        {
            group = new Group(name, SignInViewModel.currentUser.username);

            var groupList = new List<Group>();
            var userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin"))
            {
                groupList = Serializer.Load<List<Group>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin");
            }

            if (!groupList.Exists(x => x.name == name && x.manager == SignInViewModel.currentUser.username))
            {
                groupList.Add(group);
                Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin", groupList);
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (userList.Exists(user => user.username == SignInViewModel.currentUser.username))
            {
                int index = userList.FindIndex(user => user.username == SignInViewModel.currentUser.username);
                userList[index].groupName = name;
                SignInViewModel.currentUser.groupName = name;
                Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin",userList);
            }
            return group;
        }

        public Group getGroup()
        {
            var groupList = new List<Group>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin"))
            {
                groupList = Serializer.Load<List<Group>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin");
            }

            if (groupList.Exists(x => x.manager == SignInViewModel.currentUser.username))
            {
                group = groupList.Find(x => x.manager == SignInViewModel.currentUser.username);
                return group;
            }
            else
            {
                if (groupList.Exists(x => x.name == SignInViewModel.currentUser.groupName))
                {
                    group = groupList.Find(x => x.name == SignInViewModel.currentUser.groupName);
                    return group;
                }
                else
                {
                    group = null;
                    return null;
                }
            }
        }
        public Group joinGroup(string groupName)
        {
            var groupList = new List<Group>();
            var userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin"))
            {
                groupList = Serializer.Load<List<Group>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin");
            }
            else
            {
                return null;
            }

            if (groupList.Exists(x => x.name == groupName))
            {
                int index = groupList.FindIndex(x => x.name == groupName);
                if (!groupList[index].users.Contains(SignInViewModel.currentUser.username))
                {
                    groupList[index].users.Add(SignInViewModel.currentUser.username);
                    Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin", groupList);
                    group = groupList[index];
                }
                else
                {
                    return null;
                }


                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
                {
                    userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
                }

                if (userList.Exists(user => user.username == SignInViewModel.currentUser.username))
                {
                    int userIndex = userList.FindIndex(user => user.username == SignInViewModel.currentUser.username);
                    userList[userIndex].groupName = groupName;
                    SignInViewModel.currentUser.groupName = groupName;

                    Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", userList);
                    return group;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            
        }

        public void leaveGroup() {

            var groupList = new List<Group>();
            var userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin"))
            {
                groupList = Serializer.Load<List<Group>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin");
            }

            if (groupList.Exists(x => x.name == SignInViewModel.currentUser.groupName))
            {
                int index = groupList.FindIndex(x => x.name == SignInViewModel.currentUser.groupName);
                int userIndexGroup = groupList[index].users.FindIndex(username => username == SignInViewModel.currentUser.username);

                groupList[index].users.RemoveAt(userIndexGroup);
                
                Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin", groupList);

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
                {
                    userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
                }

                if (userList.Exists(user => user.username == SignInViewModel.currentUser.username))
                {
                    int userIndex = userList.FindIndex(user => user.username == SignInViewModel.currentUser.username);
                    userList[userIndex].groupName = "";
                    SignInViewModel.currentUser.groupName = "";

                    Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", userList);
                }
                group = null;
            }
        }

        public void removeGroup()
        {
            var groupList = new List<Group>();
            var userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin"))
            {
                groupList = Serializer.Load<List<Group>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin");
            }

            if (groupList.Exists(x => x.name == SignInViewModel.currentUser.groupName))
            {
                group = groupList.Find(x => x.name == SignInViewModel.currentUser.groupName);
                int groupIndex = groupList.FindIndex(x => x.name == SignInViewModel.currentUser.groupName);

                foreach (string username in group.users)
                {
                    if (userList.Exists(user => user.username == username))
                    {
                        int index = userList.FindIndex(user => user.username == username);
                        userList[index].groupName = "";
                    }
                }
                if (userList.Exists(user => user.username == SignInViewModel.currentUser.username))
                {
                    int index = userList.FindIndex(user => user.username == SignInViewModel.currentUser.username);
                    userList[index].groupName = "";
                    SignInViewModel.currentUser.groupName = "";
                }
                Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", userList);
                groupList.RemoveAt(groupIndex);
                Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin", groupList);

            }


        }

        public List<Group> getAllGroups()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin"))
            {
                return Serializer.Load<List<Group>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/groups.bin");
            }
            else
            {
                return null;
            }
        }

        public List<User> getGroupUsers(string groupName)
        {
            List<User> groupUsers = new List<User>();
            List<User> userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (userList.Count != 0)
            {
                foreach (User user in userList)
                {
                    if (group.users.Contains(user.username))
                    {
                        groupUsers.Add(user);
                    }
                }
            }
            return groupUsers;
        }
    }
}
