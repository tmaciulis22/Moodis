using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Moodis.Feature.Login;

namespace Moodis.Feature.Menu
{
    public class UserListAdapter : RecyclerView.Adapter, View.IOnClickListener
    {
        public List<User> userList;
        public UserListAdapter(List<User> user)
        {
            userList = user;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.holder_users, parent, false);
            UserViewHolder vh = new UserViewHolder(itemView);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            (holder as UserViewHolder).OnBind(userList[position]);
            (holder as UserViewHolder).ItemView.SetOnClickListener(this);
            (holder as UserViewHolder).ItemView.Tag = position;
            (holder as UserViewHolder).UsernameLabel.SetTextColor(userList[position].IsSelected ? Android.Graphics.Color.Green : Android.Graphics.Color.White);
        }

        public void OnClick(View v)
        {
            int position = (int)v.Tag;
            userList[position].IsSelected = !userList[position].IsSelected;
            NotifyDataSetChanged();
        }

        public override int ItemCount
        {
            get { return userList.Count; }
        }
    }
}