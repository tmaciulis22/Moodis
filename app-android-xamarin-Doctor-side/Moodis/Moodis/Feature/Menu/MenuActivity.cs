using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Feature.Group;
using Moodis.Feature.SignIn;
using Moodis.History;
using Moodis.Ui;
using System;
using System.Linq;

namespace Moodis.Feature.Menu
{
    [Activity(Label = "Menu", Theme = "@style/AppTheme.NoActionBar")]
    public class MenuActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private readonly string TAG = nameof(MenuActivity);
        RecyclerView recyclerView;
        private MenuViewModel MenuViewModel;
        private int spinnerPosition;
        private const string FormatDouble = "N3";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MenuViewModel = MenuViewModel.Instance;
            SetContentView(Resource.Layout.activity_menu);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);


            recyclerView = FindViewById<RecyclerView>(Resource.Id.userRecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));

            /* !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            TODO THIS SHOULD LATER CHECK IF USER ALREADY HAS A GROUP AS A USER CAN ONLY HAVE 1 GROUP BECAUSE OF THE FACE API STUFF 
            !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/

            var userList = SignInViewModel.userList.Where(user => !user.IsDoctor).ToList();
            recyclerView.SetAdapter(new UserListAdapter(userList));

            InitialiseInputs();

            AnimationExtension.AnimateBackground(FindViewById(Resource.Id.menuActivity));
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
                Finish();
            }
        }

        private void LogoutWindowShow()
        {
            var dialog = this.ConfirmationAlert(
                    titleRes: Resource.String.logout,
                    messageRes: Resource.String.logout_confirmation_message,
                    positiveButtonRes: Resource.String.yes,
                    negativeButtonRes: Resource.String.no,
                    positiveCallback: delegate { HandleLogout(); });
            dialog.Show();
            dialog.Dispose();
        }

        private void HandleLogout()
        {
            StartActivity(new Intent(this, typeof(SignInActivity)));
            FinishAffinity();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_groups)
            {
                StartActivity(new Intent(this, typeof(GroupActivity)));
            }
            else if (id == Resource.Id.nav_history)
            {
                StartActivity(new Intent(this, typeof(HistoryActivity)));
            }
            else if (id == Resource.Id.nav_menu_logout)
            {
                LogoutWindowShow();
            }

            Log.Info(TAG, "Interraction with navigation window");
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private void InitialiseInputs()
        {
            var AddUserToGroup = FindViewById(Resource.Id.AddToGroup);
            var CheckGroupHistory = FindViewById(Resource.Id.CheckGroupHistory);
            var CheckUserHistory = FindViewById(Resource.Id.CheckPersonHistory);

            AddUserToGroup.Click += delegate {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view = layoutInflater.Inflate(Resource.Layout.user_input_dialog_box, null);

                var groupsList = GroupActivityModel.groups.Where(group => group.IsMember(SignInViewModel.currentUser.Username))
                .Select(group => group.Groupname).ToList();

                var spinner = view.FindViewById<Spinner>(Resource.Id.spinnerGroupName);
                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, groupsList);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = adapter;

                var selectedUsers = SignInViewModel.userList.Where(user => user.IsSelected).ToList();
                string choice = spinner.GetItemAtPosition(spinnerPosition).ToString();

                Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertbuilder.SetView(view);
                alertbuilder.SetCancelable(false)
                                .SetPositiveButton("Confirm", delegate
                                {
                                    foreach(var user in selectedUsers)
                                    {
                                            foreach(var group in GroupActivityModel.groups)
                                            {
                                                if(group.Groupname == choice)
                                                {
                                                    group.AddMember(user.Username);
                                                    //MenuViewModel.Instance.MovePersonGroupAsync(user.PersonGroupId, user.PersonId, user.Username, group.groupId);
                                                }
                                            }
                                    }
                                })
                                .SetNegativeButton("Cancel", delegate
                                {
                                    alertbuilder.Dispose();
                                });
                Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
                dialog.Show();
            };

            CheckGroupHistory.Click += delegate {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view = layoutInflater.Inflate(Resource.Layout.user_input_dialog_box, null);

                var groupsList = GroupActivityModel.groups.Where(group => group.IsMember(SignInViewModel.currentUser.Username))
                .Select(group => group.Groupname).ToList();

                var spinner = view.FindViewById<Spinner>(Resource.Id.spinnerGroupName);
                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, groupsList);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = adapter;

                Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertbuilder.SetView(view);
                alertbuilder.SetCancelable(false)
                                .SetPositiveButton("Confirm", delegate
                                {
                                    string choice = spinner.GetItemAtPosition(spinnerPosition).ToString();
                                    StartActivity(new Intent(this, typeof(HistoryActivity)).PutExtra("group", choice).PutExtra("reason",1));
                                })
                                .SetNegativeButton("Cancel", delegate
                                {
                                    alertbuilder.Dispose();
                                });
                Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
                dialog.Show();
            };

            CheckUserHistory.Click += delegate {
                var selectedUsers = SignInViewModel.userList.Where(user => user.IsSelected).ToList();
                if(selectedUsers.Count == 1)
                {
                    StartActivity(new Intent(this, typeof(HistoryActivity)).PutExtra("reason", 2));
                }
                else
                {
                    Toast.MakeText(this, Resource.String.select_only_one, ToastLength.Short).Show();
                }
            };

        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            spinnerPosition = e.Position;
        }
    }
}