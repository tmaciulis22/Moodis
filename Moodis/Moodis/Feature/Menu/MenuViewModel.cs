using Moodis.Network.Face;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using moodis;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moodis.Extensions;

namespace Moodis.Ui
{
    class MenuViewModel
    {
        public static ImageInfo currentImage = new ImageInfo();
        private static Bitmap userImage;

        public Bitmap ShowImage(string fileToDisplay)
        {
            userImage?.Dispose();
            userImage = new Bitmap(fileToDisplay);
            return userImage;
        }

        public async Task GetFaceEmotionsAsync()
        {
            Face face = Face.Instance;
            var json = await face.SendImageForAnalysis(currentImage.ImagePath);
            var jsonAsString = json.ValidateJson();
            if (jsonAsString != null)
            {
                currentImage.SetImageInfo(jsonAsString);
            }
        }
    }
}
