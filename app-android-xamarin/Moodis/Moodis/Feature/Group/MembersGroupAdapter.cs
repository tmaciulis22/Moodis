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
    class MembersGroupAdapter : RecyclerView.Adapter
    {
        readonly List<string> members;

        public MembersGroupAdapter(int position)
        {
            members = GroupActivityModel.groups[position].Members;
        }
        public override int ItemCount => members.Count();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as MembersViewHolder;
            viewHolder.GroupMemberLabel.Text = members[position];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.holder_members_entry, parent, false);
            return new MembersViewHolder(itemView);
        }
    }
}