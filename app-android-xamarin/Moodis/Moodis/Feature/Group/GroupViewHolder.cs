using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Moodis.Feature.Group
{
    class GroupViewHolder : RecyclerView.ViewHolder
    {
        public TextView GroupNameLabel { get; set; }
        public GroupViewHolder(View view) : base(view)
        {
            GroupNameLabel = view.FindViewById<TextView>(Resource.Id.label_groupname);
        }
    }

}