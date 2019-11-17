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
        GroupActivityModel groupActivityModel = new GroupActivityModel();

        public GroupListAdapter(List<Group> groups)
        {
            this.groups = groups;
        }
        public override int ItemCount
        {
            get { return groups.Count(); }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Console.WriteLine("indekas" + position);
            var viewHolder = holder as GroupViewHolder;
            viewHolder.GroupNameLabel.Text = groups[position].Groupname;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.holder_group_entry, parent, false);
            return new GroupViewHolder(itemView);
        }
    }
}