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
using Moodis.Feature.Group;
using Moodis.Feature.SignIn;
using Moodis.History;
using Moodis.Ui;
using System;
using System.Collections.Generic;

namespace Moodis.Feature.Menu
{
    [Activity(Label = "Menu", Theme = "@style/AppTheme.NoActionBar")]
    public class MenuActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private readonly string TAG = nameof(MenuActivity);
        RecyclerView recyclerView;
        private MenuViewModel MenuViewModel;
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
            recyclerView.SetAdapter(new UserListAdapter(SignInViewModel.userList));
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
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle(Resource.String.logout)
                .SetMessage(Resource.String.logout_confirmation_message)
                .SetNegativeButton(Resource.String.no, (senderAlert, args) => { })
                .SetPositiveButton(Resource.String.yes, (senderAlert, args) =>
                {
                    StartActivity(new Intent(this, typeof(SignInActivity)));
                    FinishAffinity();
                });
            builder.Create().Show();
            builder.Dispose();
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
            var CheckUserHistory = FindViewById(Resource.Id.CheckPersonHistory);
            var CheckGroupHistory = FindViewById(Resource.Id.CheckGroupHistory);

            AddUserToGroup.Click += delegate {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view = layoutInflater.Inflate(Resource.Layout.user_input_dialog_box, null);
                Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertbuilder.SetView(view);

                var spinner = view.FindViewById<Spinner>(Resource.Id.spinnerGroupName);
                var groupsList = new List<string>();
                foreach(var group in GroupActivityModel.groups)
                {
                    if (group.IsMember(SignInViewModel.currentUser.Username))
                    {
                        groupsList.Add(group.Groupname);
                    }
                }

                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
                var strings = groupsList.ToArray();

                var adapter = new ArrayAdapter<string>(
                this, Android.Resource.Layout.SimpleSpinnerItem, strings);

                adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = adapter;

                alertbuilder.SetCancelable(false)
                                .SetPositiveButton("Confirm", delegate
                                {
                                    
                                })
                                .SetNegativeButton("Cancel", delegate
                                {
                                    alertbuilder.Dispose();
                                });
                Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
                dialog.Show();
            };
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("selected", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}


/*
AddPersonToGroup.Click += delegate {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
View view = layoutInflater.Inflate(Resource.Layout.user_input_dialog_box, null);
Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
alertbuilder.SetView(view);
                var userdata = view.FindViewById<EditText>(Resource.Id.editText);
alertbuilder.SetCancelable(false)
                .SetPositiveButton("Submit", delegate
                {
                    Toast.MakeText(this, "Submit Input: " + userdata.Text, ToastLength.Short).Show();
})
                .SetNegativeButton("Cancel", delegate
                {
                    alertbuilder.Dispose();
                });
                Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
dialog.Show();
            };
*/