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

namespace Moodis.Feature.Group
{
    [Activity(Label = "Group List")]
    public class GroupListActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetSupportActionBar();
            SetContentView(Resource.Layout.activity_group_list);
            InitAdapter();
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void InitAdapter()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.group_list);
            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            var adapter = new GroupListAdapter(GroupActivityModel.groups);
            recyclerView.SetAdapter(adapter);
            Console.WriteLine("ilgis " + GroupActivityModel.groups.Count());
        }
    }
}