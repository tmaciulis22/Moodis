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
            lblAnger.Text = "loading";
            lblContempt.Text = "loading";
            lblDisgust.Text = "loading";
            lblFear.Text = "loading";
            lblHappiness.Text = "loading";
            lblNeutral.Text = "loading";
            lblSadness.Text = "loading";
            lblSurprise.Text = "loading";
            ShowImage(currentImage.imagePath);
            face = Face.Instance;
            string imageFilePath = Console.ReadLine();
            string jsonAsString = await face.SendImageForAnalysis(currentImage.imagePath);
            currentImage.setImageInfo(jsonAsString);
            ICollection valueColl = currentImage.Emotions.Values;
            lblAnger.Text = "anger: " + (string)currentImage.Emotions["anger"];
            lblContempt.Text = "contempt: " + (string)currentImage.Emotions["contempt"];
            lblDisgust.Text = "disgust: " + (string)currentImage.Emotions["disgust"];
            lblFear.Text = "fear: " + (string)currentImage.Emotions["fear"];
            lblHappiness.Text = "happiness: " + (string)currentImage.Emotions["happiness"];
            lblNeutral.Text = "neutral: " + (string)currentImage.Emotions["neutral"];
            lblSadness.Text = "sadness: " + (string)currentImage.Emotions["sadness"];
            lblSurprise.Text = "surprise: " + (string)currentImage.Emotions["surprise"];
             
        }
    }
}
