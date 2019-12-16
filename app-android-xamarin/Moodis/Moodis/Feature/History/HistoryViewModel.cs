using AndroidX.Lifecycle;
using Microcharts;
using Moodis.Network;
using Moodis.Ui;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moodis.History
{
    class HistoryViewModel : ViewModel
    {
        public async Task<IList<object>> FetchItemList(string userId, DateTime dateTime)
        {
            var listToReturn = new List<object>();
            IEnumerable<ImageInfo> imageInfos;
            try
            {
                imageInfos = await API.ImageInfoEndpoint.GetImageInfos(userId, dateTime);
            }
            catch
            {
                return null;
            }

            listToReturn.AddRange(imageInfos);

            if (listToReturn.Count != 0)
            {
                listToReturn.Insert(0, new DonutChart());
            }

            return listToReturn;
        }
    }
}