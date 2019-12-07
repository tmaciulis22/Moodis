using AndroidX.Lifecycle;
using Microcharts;
using Moodis.Database;
using Moodis.Network;
using Moodis.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moodis.History
{
    class HistoryViewModel : ViewModel
    {
        public async Task<IList<object>> FetchItemList(string userId, DateTime? dateTime = null)
        {
            var listToReturn = new List<object>();

            listToReturn.AddRange(await API.ImageInfoEndpoint.GetImageInfos(userId, dateTime.ToString()));//TODO Possible bug here

            if (listToReturn.Count != 0)
            {
                listToReturn.Insert(0, new DonutChart());
            }

            return listToReturn;
        }
    }
}