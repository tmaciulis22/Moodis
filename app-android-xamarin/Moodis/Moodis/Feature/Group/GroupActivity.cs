using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Moodis.Constants.Enums;

namespace Moodis.Feature.Group
{
    [Activity(Label = "Group Activity")]
    public class GroupActivity : AppCompatActivity
    {
        GroupActivityModel groupActivityModel = new GroupActivityModel(); 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_group);
            InitialiseInputs();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        private void InitialiseInputs()
        {
            var GroupNameTextField = FindViewById<EditText>(Resource.Id.text_groupName);
            var AddGroupButton = FindViewById(Resource.Id.button_join_group);
            var SeeGroupsButtop = FindViewById(Resource.Id.button_see_groups);

            GroupNameTextField.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty((sender as EditText).Text))
                {
                    GroupNameTextField.SetError(GetString(Resource.String.username_empty_error), null);
                }
            };

            AddGroupButton.Click += (sender, e) =>
            {
                if(!string.IsNullOrEmpty(GroupNameTextField.Text))
                {
                    var response = groupActivityModel.AddUserToGroup(GroupNameTextField.Text);
                    if(response == Response.OK)
                    {
                        Toast.MakeText(this, Resource.String.join_group_success, ToastLength.Short);
                    }
                    else if(response == Response.UserExists)
                    {
                        Toast.MakeText(this, Resource.String.join_group_fail, ToastLength.Short);
                    }
                    else
                    {
                        Toast.MakeText(this, Resource.String.api_error, ToastLength.Short);
                    }
                }
            };
        }
    }
}