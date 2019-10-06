using Moodis.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            try
            {
                size = dailyList[0].emotions.Length - 1;
            }
            catch (ArgumentOutOfRangeException)
            {
                return ErrorWhenNoEmotionsFound;
            }
            List<double> confidenceList = new List<double>(new double[size + 1]);
            foreach (ImageInfo imageInfo in dailyList)
            {

                for (int i = 0; i < size; i++)
                {
                    confidenceList[i] = confidenceList[i] + imageInfo.emotions[i].confidence;
                }
            }
            int index = confidenceList.IndexOf(confidenceList.Max());
            return dailyList[0].emotions[index].name + " avg: " + (confidenceList[index] / dailyList.Count).ToString(FormatDouble);
        }
    }
}
