using System;
using System.Collections;
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
            var emotionLabels = new List<Label> { lblAnger, lblContempt, lblDisgust, lblFear, lblHappiness, lblNeutral, lblSadness, lblSurprise };
            foreach (var label in emotionLabels)
            {
                label.Text = "loading";
            }
            ShowImage(currentImage.imagePath);

            Face face = Face.Instance;
            string imageFilePath = Console.ReadLine();
            string jsonAsString = await face.SendImageForAnalysis(currentImage.imagePath);
            currentImage.setImageInfo(jsonAsString);

            ICollection keyColl = currentImage.Emotions.Keys;
            string[] emotionNames = new string[emotionLabels.Count];
            keyColl.CopyTo(emotionNames, 0);
            int counter = 0;
            foreach (var label in emotionLabels)
            {
                label.Text = emotionNames[counter] + ": " + (string)currentImage.Emotions[emotionNames[counter]];
                counter++;
            }
        }
    }
}
