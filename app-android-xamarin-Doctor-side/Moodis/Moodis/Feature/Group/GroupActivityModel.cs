using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moodis.Constants.Enums;
using Moodis.Feature.Login;
using Moodis.Feature.Register;
using Moodis.Feature.SignIn;
using Moodis.Network;

namespace Moodis.Feature.Group
{
    class GroupActivityModel
    {
        public async Task<Response> AddUserToGroupAsync(User user, string groupid)
        {
            var groups = await API.GroupEndpoint.GetAllGroups();
            if(groups != null)
            {
                var username = SignInViewModel.currentUser.Username;
                var group = groups.Find(groupTemp => groupTemp.Id == groupid);
                if(group == null)
                {
                    return Response.GroupNotFound;
                }
                //if(!group.IsMember(username))
                if(user.GroupId != group.Id)
                {
                    user.GroupId = group.Id;
                    await API.UserEndpoint.UpdateUser(user);
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

        public async Task<Response> CreateGroupAsync(string groupName)
        {
            var groups = await API.GroupEndpoint.GetAllGroups();
            if (!groups.Exists(group => group.Groupname == groupName))
            {
                var newGroup = new Group(groupName, SignInViewModel.currentUser.Username);
                groups.Add(newGroup);
                await API.GroupEndpoint.CreateGroup(newGroup);
                return Response.OK;
            }
            else
            {
                return Response.GroupExists;
            }
        }

        public static async Task<List<string>> GetGroupUserIdsAsync(string groupId)
        {
            var groups = await API.GroupEndpoint.GetByIdGroup(groupId);
            List<string> UserIds = new List<string>();
            var users = await API.UserEndpoint.
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