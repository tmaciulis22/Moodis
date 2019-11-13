using AndroidX.Lifecycle;
using Moodis.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
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
            size = dailyList[0].emotions.Count - 1;
            List<double> confidenceList = new List<double>(new double[size + 1]);

            foreach (ImageInfo imageInfo in dailyList)
            {
                var query = imageInfo.emotions.Select(x => x.Confidence);
                var i = 0;
                foreach (var confidence in query)
                {
                    confidenceList[i] = confidenceList[i] + confidence;
                    i++;
                }
            }
            int index = confidenceList.IndexOf(confidenceList.Max());
            return dailyList[0].emotions[index].Name + " avg: " + (confidenceList[index] / dailyList.Count).ToString(FormatDouble);
        }

        public IList<ImageInfo> FetchStats(string userId, DateTime? dateTime = null)
        {
            return DatabaseModel.FetchUserStats(userId, dateTime);
        }
    }
}