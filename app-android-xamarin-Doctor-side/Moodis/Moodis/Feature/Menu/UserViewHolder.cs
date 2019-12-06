using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Moodis.Feature.Login;
using Moodis.Feature.SignIn;

namespace Moodis.Feature.Menu
{
    public class UserViewHolder : RecyclerView.ViewHolder
    {
        public TextView UsernameLabel { get; private set; }
        public UserViewHolder(View itemView) : base(itemView)
        {
            UsernameLabel = itemView.FindViewById<TextView>(Resource.Id.menuListUserLabel);
        }
        public void OnBind(User user)
        {
            UsernameLabel.Text = user.Username;
        }
    }
}