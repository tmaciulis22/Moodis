using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Ui
{
    public class ImageInfo
    {
        public string infoPath { get; set; }
        public string imagePath { get; set; }
        public string id { get; set; }
        public string emotion { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public ImageInfo()
        {

        }
        private void setImageInfo()
        {
            //get info from json to image
        }
    }
}
