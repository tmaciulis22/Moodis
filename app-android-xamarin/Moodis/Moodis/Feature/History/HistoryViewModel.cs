using AndroidX.Lifecycle;
using Microcharts;
using Moodis.Database;
using Moodis.Ui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moodis.History
{
    class HistoryViewModel : ViewModel
    {
        public IList<object> FetchItemList(List<string> userIds, DateTime? dateTime = null)
        {
            var listToReturn = new List<object>();

            listToReturn.AddRange(DatabaseModel.FetchUserStats(userIds, dateTime));
            if (listToReturn.Count != 0)
            {
                listToReturn.Insert(0, new DonutChart());
            }

            return listToReturn;
        }
    }
}