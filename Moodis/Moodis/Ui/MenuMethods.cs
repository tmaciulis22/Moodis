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
    class MenuMethods
    {
        public static ImageInfo currentImage = new ImageInfo();
        private static Bitmap userImage;
        private static string jsonAsString;

        public static Bitmap ShowImage(String fileToDisplay)
        {
            if (userImage != null)
            {
                userImage.Dispose();
            }
            userImage = new Bitmap(fileToDisplay);
            return userImage;
        }
        public static async Task GetFaceEmotionsAsync()
        {
            Face face = Face.Instance;
            jsonAsString = await face.SendImageForAnalysis(currentImage.ImagePath);
            if (ValidateJson())
            {
                currentImage.SetImageInfo(jsonAsString);
            }
        }
        public static bool ValidateJson()
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
