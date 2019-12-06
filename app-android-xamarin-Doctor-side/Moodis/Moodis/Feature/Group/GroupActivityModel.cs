using System.Collections.Generic;
using System.Linq;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Register;
using Moodis.Feature.SignIn;

namespace Moodis.Feature.Group
{
    class GroupActivityModel
    {
        public static List<Group> groups = DatabaseModel.FetchGroupFromDatabase();

        public Response AddUserToGroup(string groupName)
        {
            if(groups != null)
            {
                var username = SignInViewModel.currentUser.Username;
                var group = groups.Find(groupTemp => groupTemp.Groupname == groupName);
                if(group == null)
                {
                    return Response.GroupNotFound;
                }
                if(!group.IsMember(username))
                {
                    group.AddMember(username);
                    DatabaseModel.UpdateGroupToDatabase(group);
                    return Response.OK;
                }
                else
                {
                    return Response.GroupExists;
                }
            }
            else
            {
                return Response.GeneralError;
            }
        }

        public Response CreateGroup(string groupName)
        {
            if(!groups.Exists(group => group.Groupname == groupName))
            {
                var newGroup = new Group(groupName, SignInViewModel.currentUser.Username);
                groups.Add(newGroup);
                DatabaseModel.AddGroupToDatabase(newGroup);
                return Response.OK;
            }
            else
            {
                return Response.GroupExists;
            }
        }

        public static List<string> GetGroupUserIds(string groupName)
        {
            var whereUserIs = groups.Where(group => group.Groupname == groupName).ToList();
            List<string> FriendUsernames = new List<string>();
            whereUserIs.ForEach(group => group.Members.ForEach(username => FriendUsernames.Add(username)));
            List<string> UserIds = new List<string>();
            FriendUsernames.ForEach(username => UserIds.Add(RegisterViewModel.GetIdByUsername(username)));
            return UserIds;
        }

        public static void LeaveGroup(string groupname)
        {
            var group = groups.Find(group => group.Groupname == groupname);
            group.Members.Remove(SignInViewModel.currentUser.Username);
            DatabaseModel.UpdateGroupToDatabase(group);
        }
    }
}