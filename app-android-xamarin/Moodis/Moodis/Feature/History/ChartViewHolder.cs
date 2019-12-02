using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Microcharts;
using Microcharts.Droid;
using Moodis.History;
using Moodis.Ui;
using SkiaSharp;

namespace Moodis.Feature.History
{
    class ChartViewHolder : RecyclerView.ViewHolder
    {
        ChartView ChartView;

        public ChartViewHolder(View view) : base(view)
        {
            ChartView = view.FindViewById<ChartView>(Resource.Id.chartView);
        }

        //TODO optimize this when connected to BE
        public void OnBind(List<ImageInfo> stats, Chart chart)
        {
            
            if (stats.Count == 0) return;

            var entries = new List<ChartEntry>();
            var maxEmotions = new List<KeyValuePair<string, int>>();

            stats.ForEach(stat => {
                var highestEmotion = stat.emotions.Max();

                var oldPairIndex = maxEmotions.FindIndex(pair => pair.Key == highestEmotion.Name);
                if (oldPairIndex != -1)
                {
                    var oldPair = maxEmotions[oldPairIndex];

                    maxEmotions.RemoveAt(oldPairIndex);
                    maxEmotions.Insert(oldPairIndex, new KeyValuePair<string, int>(oldPair.Key, oldPair.Value + 1));
                }
                else
                {
                    maxEmotions.Add(new KeyValuePair<string, int>(highestEmotion.Name, 1));
                }
            });

            var random = new Random();
            maxEmotions.ForEach(emotion => {
                var color = SKColor.Parse(GenerateRandomColor(random));
                entries.Add(new ChartEntry(emotion.Value) { 
                    Label = emotion.Key, 
                    ValueLabel = emotion.Value.ToString(),
                    Color = color,
                    TextColor = color
                });
            });

            chart.LabelTextSize = ChartView.Context.Resources.GetDimension(Resource.Dimension.chart_label);
            chart.Entries = entries;
            ChartView.Chart = chart;
        }

        //TODO Refactor out this piece of shit on optimization of OnBind
        private string GenerateRandomColor(Random random)
        {
            return string.Format("#{0:X6}", random.Next(0x1000000));
        }
    }
}