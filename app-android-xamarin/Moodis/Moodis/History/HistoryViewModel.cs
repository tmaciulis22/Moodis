using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Lifecycle;
using Moodis.Ui;
using static Moodis.Ui.ImageInfo;

namespace Moodis.History
{
    class HistoryViewModel : ViewModel
    {
        private const string FormatDouble = "N3";
        private const string ErrorWhenNoEmotionsFound = "data not found";
        public List<ImageInfo> monthlyList;
        public List<ImageInfo> dailyList;

        public void UpdateViewModel()
        {
            dailyList = new List<ImageInfo>();
            monthlyList = new List<ImageInfo>();
        }
        public string GetAverageEmotionStats(List<ImageInfo> dailyList)
        {
            int size;
            if (dailyList.Count.Equals(0))
            {
                return ErrorWhenNoEmotionsFound;
            }
            size = dailyList[0].emotions.Length - 1;
            List<double> confidenceList = new List<double>(new double[size + 1]);

            foreach (ImageInfo imageInfo in dailyList)
            {
                var query = imageInfo.emotions.Select(x => x.confidence);
                var i = 0;
                foreach (var confidence in query)
                {
                    confidenceList[i] = confidenceList[i] + confidence;
                    i++;
                }
            }
            int index = confidenceList.IndexOf(confidenceList.Max());
            return dailyList[0].emotions[index].name + " avg: " + (confidenceList[index] / dailyList.Count).ToString(FormatDouble);
        }

        public IList<ImageInfo> FetchStats(string userId, DateTime? dateTime = null)
        {
            //TODO refactor this method to fetch from DB emotion statistics, if dateTime is null, then return todays stats
            var listToReturn = new List<ImageInfo>();
            var exampleImageInfo = new ImageInfo();
            exampleImageInfo.emotions = new Emotion[8];
            exampleImageInfo.emotions[0].name = "Happiness";
            exampleImageInfo.emotions[0].confidence = 1;
            exampleImageInfo.imageDate = DateTime.Now;
            listToReturn.Add(exampleImageInfo);
            return listToReturn;
        }
    }
}