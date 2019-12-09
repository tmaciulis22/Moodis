using System;
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
        public static List<Group> groups;

        public GroupActivityModel()
        {
            oncreate();
        }

        private async void oncreate()
        {
            groups = await API.GroupEndpoint.GetAllGroups();
        }

        public async Task<Response> CreateGroupAsync(string groupName)
        {
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
            List<string> UserIds = new List<string>();
            var users = await API.UserEndpoint.GetAllUsersByGroup(groupId);
            users.ForEach(user => UserIds.Add(user.Id));
            return UserIds;
        }

        public static async Task DeleteGroupAsync(string groupId)
        {
            var group = await API.GroupEndpoint.GetByIdGroup(groupId);
            var users = await API.UserEndpoint.GetAllUsersByGroup(groupId);
            users.ForEach(user => user.GroupId = null);
            users.ForEach(async user => await API.UserEndpoint.UpdateUser(user));
            await API.GroupEndpoint.DeleteGroup(groupId);
        }
    }
}