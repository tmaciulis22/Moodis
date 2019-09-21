using Moodis.Network.Face;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moodis.Ui
{
    class MenuViewModel
    {
        private MenuViewModel() { }

        public static ImageInfo currentImage = new ImageInfo();
        private static Bitmap userImage;
        private static string jsonAsString;

        private static MenuViewModel instance = null;
        public static MenuViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuViewModel();
                }
                return instance;
            }
        }

        public Bitmap ShowImage(String fileToDisplay)
        {
            userImage?.Dispose();
            userImage = new Bitmap(fileToDisplay);
            return userImage;
        }

        public async Task GetFaceEmotionsAsync()
        {
            Face face = Face.Instance;
            jsonAsString = await face.SendImageForAnalysis(currentImage.ImagePath);
            if (ValidateJson())
            {
                currentImage.SetImageInfo(jsonAsString);
            }
        }

        public bool ValidateJson()
        {
            jsonAsString = jsonAsString.Replace("[", " ").Replace("]", "").Replace(" ", "");
            if (string.IsNullOrEmpty(jsonAsString))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
