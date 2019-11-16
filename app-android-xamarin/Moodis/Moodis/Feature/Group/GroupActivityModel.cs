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
using Moodis.Feature.SignIn;

namespace Moodis.Feature.Group
{
    class GroupActivityModel
    {
        public List<Group> groups;
        
        public GroupActivityModel()
        {
            groups = DatabaseModel.FetchGroupFromDatabase();
        }

        public Response AddUserToGroup(string groupName)
        {
            if(groups != null)
            {
                var group = groups.Find(groupTemp => groupTemp.Groupname == groupName);
                Console.WriteLine(group.ToString());
                var username = SignInViewModel.currentUser.Username;
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
    }
}