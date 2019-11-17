
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Moodis.Constants.Enums;

namespace Moodis.Feature.Group
{
    [Activity(Label = "Groups")]
    public class GroupActivity : AppCompatActivity
    {
        readonly GroupActivityModel groupActivityModel = new GroupActivityModel(); 
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
            var JoinGroupButton = FindViewById(Resource.Id.button_join_group);
            var CreateGroupButton = FindViewById(Resource.Id.button_create_group);
            var SeeGroupsButton = FindViewById(Resource.Id.button_see_groups);

            GroupNameTextField.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty((sender as EditText).Text))
                {
                    GroupNameTextField.SetError(GetString(Resource.String.username_empty_error), null);
                }
            };

            JoinGroupButton.Click += (sender, e) =>
            {
                if(!string.IsNullOrEmpty(GroupNameTextField.Text))
                {
                    var response = groupActivityModel.AddUserToGroup(GroupNameTextField.Text);
                    if(response == Response.OK)
                    {
                        Toast.MakeText(this, Resource.String.join_group_success, ToastLength.Short).Show();
                    }
                    else if(response == Response.UserNotFound)
                    {
                        Toast.MakeText(this, Resource.String.group_not_exists_error, ToastLength.Short).Show();
                    }
                    else if(response == Response.UserExists)
                    {
                        Toast.MakeText(this, Resource.String.join_group_fail, ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, Resource.String.api_error, ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, Resource.String.text_group_empty_error, ToastLength.Short).Show();
                }
            };

            CreateGroupButton.Click += (sender, e) =>
            {
                if(!string.IsNullOrEmpty(GroupNameTextField.Text))
                {
                    var respone = groupActivityModel.CreateGroup(GroupNameTextField.Text);
                    if(respone == Response.OK)
                    {
                        Toast.MakeText(this, Resource.String.join_group_success, ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, Resource.String.join_group_fail, ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, Resource.String.text_group_empty_error, ToastLength.Short).Show();
                }
            };
        }
    }
}