using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Moodis.History
{
    class StatViewHolder : RecyclerView.ViewHolder
    {
        public TextView TimeLabel { get; set; }
        public TextView EmotionLabel { get; set; }
        public TextView UsernameLabel { get; set; }

        public StatViewHolder(View view) : base(view)
        {
            TimeLabel = view.FindViewById<TextView>(Resource.Id.timeLabel);
            EmotionLabel = view.FindViewById<TextView>(Resource.Id.emotionLabel);
            UsernameLabel = view.FindViewById<TextView>(Resource.Id.usernameLabelHistory);
        }
    }
}