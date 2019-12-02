using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Feature.SignIn;
using Moodis.Ui;
using System.Linq;

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

        public void OnBind(ImageInfo stat)
        {
            var context = EmotionLabel.Context;
            var emotionName = stat.emotions.Max().Name;
            TimeLabel.Text = stat.ImageDate.Hour.ToString() + ":" + stat.ImageDate.Minute.ToString();
            EmotionLabel.Text = emotionName;
            EmotionLabel.SetTextColor(new Color(ContextCompat.GetColor(context, emotionName.EmotionColor())));
            UsernameLabel.Text = SignInViewModel.userList.Find(user => user.Id == stat.UserId).Username;
        }
    }
}