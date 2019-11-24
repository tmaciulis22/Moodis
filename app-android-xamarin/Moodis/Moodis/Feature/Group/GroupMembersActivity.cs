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
    [Activity(Label = "Group Members")]
    public class GroupMembersActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_group_details);
            InitAdapter();
        }

        private void InitAdapter()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.group_details);
            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            var adapter = new MembersGroupAdapter(Intent.GetIntExtra("clicked", 0));
            recyclerView.SetAdapter(adapter);
        }
    }
}