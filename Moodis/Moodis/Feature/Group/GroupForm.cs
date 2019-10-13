using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moodis.Feature.Login;

namespace Moodis.Feature.Group
{
    public partial class GroupForm : Form
    {
        GroupViewModel groupViewModel;
        public GroupForm()
        {
            groupViewModel = new GroupViewModel();
            InitializeComponent();
            updateView();
        }

        private void updateView()
        {
            groupViewModel.getGroup();
            Console.WriteLine(SignInViewModel.currentUser.groupName);

            if (GroupViewModel.group != null) {
                yourGroupLabel.Show();
                yourGroupLabel.Text = "Your group: " + GroupViewModel.group.name;

                createGroup.Hide();
                groupJoinButton.Hide();
                


                if (GroupViewModel.group.manager == SignInViewModel.currentUser.username)
                {
                    groupName.Hide();
                    groupsLabel.Hide();
                    usersInGroupLabel.Show();
                    leaveGroupButton.Hide();
                    groupList.Hide();
                    groupUsersList.Show();
                    groupUsersList.Items.Clear();
                    removeGroupButton.Show();
                    foreach (User user in groupViewModel.getGroupUsers(GroupViewModel.group.name))
                    {
                        groupUsersList.Items.Add(user.username);
                    }
                }
                else
                {
                    groupName.Hide();
                    groupsLabel.Show();
                    leaveGroupButton.Show();
                    groupList.Show();
                    groupUsersList.Hide();
                    usersInGroupLabel.Hide();
                    removeGroupButton.Hide();

                    groupList.Items.Clear();

                    if (groupViewModel.getAllGroups() != null)
                    {
                        foreach (Group group in groupViewModel.getAllGroups())
                        {
                            groupList.Items.Add(group.name);
                        }
                    }

                }
            }
            else
            {
                usersInGroupLabel.Hide();
                removeGroupButton.Hide();

                leaveGroupButton.Hide();
                createGroup.Show();
                groupName.Show();
                groupJoinButton.Show();
                groupList.Show();
                yourGroupLabel.Hide();
                groupUsersList.Hide();
                groupsLabel.Show();

                groupList.Items.Clear();

                if (groupViewModel.getAllGroups() != null)
                {
                    foreach (Group group in groupViewModel.getAllGroups())
                    {
                        groupList.Items.Add(group.name);
                    }
                }
            }


        }

        private void CreateGroup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(groupName.Text))
            {
                groupViewModel.createGroup(groupName.Text);
            }
            updateView();
        }

        private void GroupJoinButton_Click(object sender, EventArgs e)
        {
            if (GroupViewModel.group == null && groupList.SelectedItem != null)
            {
                groupViewModel.joinGroup(groupList.SelectedItem.ToString());
                updateView();

            }
        }

        private void LeaveGroupButton_Click(object sender, EventArgs e)
        {
            if (GroupViewModel.group != null)
            {
                groupViewModel.leaveGroup();

                updateView();
            }
        }

        private void RemoveGroupButton_Click(object sender, EventArgs e)
        {
            groupViewModel.removeGroup();
            
            updateView();
        }
    }
}
