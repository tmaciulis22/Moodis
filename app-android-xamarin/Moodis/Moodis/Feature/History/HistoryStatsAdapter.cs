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
using Moodis.Ui;

namespace Moodis.History
{
    class HistoryStatsAdapter : RecyclerView.Adapter
    {
        private List<ImageInfo> _statList;

        public HistoryStatsAdapter(IList<ImageInfo> statList)
        {
            _statList = statList.ToList();
        }

        public override int ItemCount => _statList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as StatViewHolder;
            viewHolder.TimeLabel.Text = (_statList[position].imageDate.Hour.ToString() + ":" + _statList[position].imageDate.Minute.ToString());
            viewHolder.EmotionLabel.Text = _statList[position].emotions.Max().name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.holder_history_stat, parent, false);
            return new StatViewHolder(itemView);
        }

        public void UpdateList(IList<ImageInfo> statList)
        {
            _statList = statList.ToList();
            NotifyDataSetChanged();
        }
    }
}