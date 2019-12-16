using Android.Graphics;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.SignIn;
using Moodis.Network.Face;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Moodis.Ui
{
    public class MenuViewModel
    {

        private static readonly Lazy<MenuViewModel> obj = new Lazy<MenuViewModel>(() => new MenuViewModel());
        private MenuViewModel()
        {
        }

        public static MenuViewModel Instance
        {
            get
            {
                return obj.Value;
            }
        }
        public async void MovePersonGroupAsync(string personGroupId,string personId,string username, string newGroupId)
        {
           await Face.Instance.MovePerson(personGroupId, personId, username, newGroupId);
        }
    }
}
