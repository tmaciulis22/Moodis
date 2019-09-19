using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using moodis;
using Moodis.Network.Face;

namespace Moodis.Ui
{
    public partial class MenuForm : Form
    {
        public static ImageInfo currentImage = new ImageInfo();
        public static Boolean running = false;
        private Bitmap userImage;
        private Face face;
        public MenuForm()
        {
            InitializeComponent();
            running = true;
            update();
        }
        private void ShowImage(String fileToDisplay)
        {
            if (userImage != null)
            {
                userImage.Dispose();
            }
            userImage = new Bitmap(fileToDisplay);
            imgTakenPicture.Image = userImage;
        }
        public async void update()
        {
            ShowImage(currentImage.imagePath);
            face = Face.Instance;

            string imageFilePath = Console.ReadLine();
            string x = await face.SendImageForAnalysis(currentImage.imagePath);
            Console.WriteLine(x);
        }
    }
}
