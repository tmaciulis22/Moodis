using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Feature.SignIn;

namespace Moodis.Feature.Group
{
    [Activity(Label = "Group List")]
    public class GroupListActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_group_list);
            var NoGroupLabel = FindViewById<TextView>(Resource.Id.no_groups_label);
            var groups = GroupActivityModel.groups.Where(member => member.IsMember(SignInViewModel.currentUser.Username)).ToList();
            if(groups.Count > 0)
            {
                NoGroupLabel.Visibility = ViewStates.Gone;
            }
            InitAdapter(groups,NoGroupLabel);
        }

        private void InitAdapter(List<Group> groups, TextView noGroupLabel)
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.group_list);
            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            var adapter = new GroupListAdapter(this,groups,noGroupLabel);
            recyclerView.SetAdapter(adapter);
        }
    }
}