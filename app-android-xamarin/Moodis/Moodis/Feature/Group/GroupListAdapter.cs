using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Moodis.Feature.Group
{  
    class GroupListAdapter : RecyclerView.Adapter
    {
        List<Group> groups;
        Context context;

        public GroupListAdapter(Context context,List<Group> groups)
        {
            this.groups = groups;
            this.context = context;
        }

        public override int ItemCount
        {
            get { return groups.Count(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as GroupViewHolder;
            viewHolder.GroupNameLabel.Text = groups[position].Groupname;
            viewHolder.GroupNameLabel.Click += (sender, e) =>
            {
                var usersInGroup = groups[position].Members;
                var MyIntent = new Intent(context, typeof(GroupMembersActivity));
                MyIntent.PutExtra("clicked", position);
                context.StartActivity(MyIntent);   
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.holder_group_entry, parent, false);
            return new GroupViewHolder(itemView);
        }
    }
}