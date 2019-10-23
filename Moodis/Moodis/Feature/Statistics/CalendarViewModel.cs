using Moodis.Ui;
using System.Collections.Generic;
using System.Linq;

namespace Moodis.Feature.Statistics
{
    public class CalendarViewModel
    {
        private const string FormatDouble = "N3";
        private const string ErrorWhenNoEmotionsFound = "data not found";
        public List<ImageInfo> monthlyList;
        public List<ImageInfo> dailyList;

        public void updateViewModel ()
        {
            dailyList = new List<ImageInfo>();
            monthlyList = new List<ImageInfo>();
        }
        public string getAverageEmotionStats(List<ImageInfo> dailyList)
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
    }
}
