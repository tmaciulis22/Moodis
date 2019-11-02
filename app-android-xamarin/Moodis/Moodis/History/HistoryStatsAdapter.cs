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
using Moodis.Ui;

namespace Moodis.History
{
    class HistoryStatsAdapter : RecyclerView.Adapter
    {
        public List<ImageInfo> _statList;
        public HistoryStatsAdapter(IList<ImageInfo> statList)
        {
            _statList = statList.ToList();
        }

        public override int ItemCount => _statList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as StatViewHolder;
            viewholder.Image.SetImageResource(mPhotoAlbum[position].mPhotoID);
            vh.Caption.Text = mPhotoAlbum[position].mCaption;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}