using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Constraints;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Moodis.Feature.Group
{
    class GroupViewHolder : RecyclerView.ViewHolder
    {
        public TextView GroupNameLabel { get; set; }
        public Button GroupLeaveButton { get; set; }

        public ConstraintLayout parentLayout;
        public GroupViewHolder(View view) : base(view)
        {
            GroupNameLabel = view.FindViewById<TextView>(Resource.Id.label_groupname);
            GroupLeaveButton = view.FindViewById<Button>(Resource.Id.button_leave_group);
            parentLayout = view.FindViewById<ConstraintLayout>(Resource.Id.group_list);
        }
    }
}