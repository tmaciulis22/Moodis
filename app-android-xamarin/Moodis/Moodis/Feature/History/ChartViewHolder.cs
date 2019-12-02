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

            stats.First().emotions.ForEach(emotion => {
                maxEmotions.Add(new KeyValuePair<string, int>(emotion.Name, 0));
            });

            stats.ForEach(stat => {
                var maxEmotion = stat.emotions.Max();
                var oldPairIndex = maxEmotions.FindIndex(pair => pair.Key == maxEmotion.Name);
                var oldPair = maxEmotions[oldPairIndex];

                maxEmotions.RemoveAt(oldPairIndex);
                maxEmotions.Insert(oldPairIndex, new KeyValuePair<string, int>(oldPair.Key, oldPair.Value + 1));
            });

            maxEmotions.ForEach(emotion => {
                var color = SKColor.Parse(GenerateRandomColor());
                entries.Add(new ChartEntry(emotion.Value) { 
                    Label = emotion.Key, 
                    ValueLabel = emotion.Value.ToString(),
                    Color = color,
                    TextColor = color
                });
            });

            chart.Entries = entries;
            ChartView.Chart = chart;
        }

        //Refactor this piece of shit on optimization of OnBind
        private string GenerateRandomColor()
        {
            Random rnd = new Random();
            var color = Color.Argb(255, rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return string.Format("#%02x%02x%02x", color.R, color.G, color.B);
        }
    }
}