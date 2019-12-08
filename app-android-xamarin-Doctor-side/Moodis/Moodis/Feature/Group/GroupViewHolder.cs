using Android.Support.Constraints;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Moodis.Feature.Group
{
    class GroupViewHolder : RecyclerView.ViewHolder
    {
        public TextView GroupNameLabel { get; set; }
        public ImageButton GroupLeaveButton { get; set; }

        public ConstraintLayout parentLayout;
        public GroupViewHolder(View view) : base(view)
        {
            GroupNameLabel = view.FindViewById<TextView>(Resource.Id.label_groupname);
            GroupLeaveButton = view.FindViewById<ImageButton>(Resource.Id.button_leave_group);
            parentLayout = view.FindViewById<ConstraintLayout>(Resource.Id.group_list);
        }
    }
}