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
        public static Boolean running = false;
        private const string WarningFaceDetection = "Face not detected, please try to use better lighting and stay in front of camera";

        public MenuForm()
        {
            InitializeComponent();
            running = true;
            UpdateLabels();
        }
        public async void UpdateLabels()
        {
            imgTakenPicture.Image = MenuMethods.ShowImage(MenuMethods.currentImage.ImagePath);
            var emotionLabels = new List<Label> { lblAnger, lblContempt, lblDisgust, lblFear, lblHappiness, lblNeutral, lblSadness, lblSurprise };
            foreach (var label in emotionLabels)
            {
                label.Text = "loading";
            }
            await MenuMethods.GetFaceEmotionsAsync();
            if (MenuMethods.ValidateJson())
            {
                ICollection keyColl = MenuMethods.currentImage.Emotions.Keys;
                string[] emotionNames = new string[emotionLabels.Count];
                keyColl.CopyTo(emotionNames, 0);

                int counter = 0;
                foreach (var label in emotionLabels)
                {
                    label.Text = emotionNames[counter] + ": " + (string) MenuMethods.currentImage.Emotions[emotionNames[counter]];
                    counter++;
                }
            }
            else
            {
                MessageBox.Show(WarningFaceDetection);
            }
        }

        private void MenuFormCloseEvent(object sender, FormClosedEventArgs e)
        {
            running = false;
        }
    }
}
