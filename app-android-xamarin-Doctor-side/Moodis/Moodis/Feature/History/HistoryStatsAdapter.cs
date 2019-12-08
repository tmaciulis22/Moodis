using Android.Support.V7.Widget;
using Android.Views;
using Microcharts;
using Moodis.Feature.History;
using Moodis.Ui;
using System.Collections.Generic;
using System.Linq;

namespace Moodis.History
{
    public class HistoryStatsAdapter : RecyclerView.Adapter
    {
        public const int DAILY_CHART = 0;
        private const int STAT = 1;

        private List<object> _itemList;

        public HistoryStatsAdapter(IList<object> itemList)
        {
            _itemList = itemList.ToList();
        }

        public override int ItemCount
        {
            get { return _itemList.Count; }
        }

        public override int GetItemViewType(int position)
        {
            if (_itemList.ElementAt(position) is DonutChart)
            {
                return DAILY_CHART;
            }
            else
            {
                return STAT;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder.ItemViewType == STAT)
            {
                (holder as StatViewHolder).OnBind(_itemList[position] as ImageInfo);
            }
            else if (holder.ItemViewType == DAILY_CHART)
            {
                var stats = _itemList.SkipWhile(item => item is Chart).Select(stat => stat as ImageInfo).ToList();
                (holder as ChartViewHolder).OnBind(stats, _itemList[position] as DonutChart);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView;
            if (viewType == STAT)
            {
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.holder_history_stat, parent, false);
                return new StatViewHolder(itemView);
            }
            else
            {
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.holder_chart, parent, false);
                return new ChartViewHolder(itemView);
            }
        }

        public void UpdateList(IList<object> itemList)
        {
            _itemList = itemList.ToList();
            NotifyDataSetChanged();
        }
    }
}