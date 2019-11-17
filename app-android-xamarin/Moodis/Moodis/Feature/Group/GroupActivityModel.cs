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
                    return Response.UserNotFound;
                }
                if(!group.IsMember(username))
                {
                    group.AddMember(username);
                    return Response.OK;
                }
                else
                {
                    return Response.UserExists;
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
                return Response.UserExists;
            }
        }

        public static List<string> GetFriendIds()
        {
            var whereUserIs = groups.Where(group => group.IsMember(SignInViewModel.currentUser.Username)).ToList();
            List<string> FriendUsernames = new List<string>();
            whereUserIs.ForEach(group => group.Members.ForEach(username => FriendUsernames.Add(username)));
            List<string> UserIds = new List<string>();
            FriendUsernames.ForEach(username => UserIds.Add(RegisterViewModel.GetIdByUsername(username)));
            return UserIds;
        }
    }
}