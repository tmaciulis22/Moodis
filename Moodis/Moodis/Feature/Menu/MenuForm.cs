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
        public bool running = false;
        private const string WarningFaceDetection = "Face not detected, please try to use better lighting and stay in front of camera";
        private readonly MenuViewModel menuViewModel;
        private const string formatDouble = "N3";
        public MenuForm()
        {
            InitializeComponent();
            menuViewModel = new MenuViewModel();
            running = true;
            UpdateLabels();
        }

        public async void UpdateLabels()
        {
            imgTakenPicture.Image = menuViewModel.ShowImage(MenuViewModel.currentImage.ImagePath);
            var emotionLabels = new List<Label> { lblAnger, lblContempt, lblDisgust, lblFear, lblHappiness, lblNeutral, lblSadness, lblSurprise };
            foreach (var label in emotionLabels)
            {
                label.Text = "loading";
            }
            await menuViewModel.GetFaceEmotionsAsync();
            if (menuViewModel.ValidateJson())
            {
                int counter = 0;
                foreach (var label in emotionLabels)
                {
                    label.Text = MenuViewModel.currentImage.emotions[counter].name + " : " 
                        + MenuViewModel.currentImage.emotions[counter].confidence.ToString(formatDouble);
                    counter++;
                }
            }
            else
            {
                MessageBox.Show(WarningFaceDetection);
            }
        }

        private void MenuFormClose(object sender, FormClosedEventArgs e)
        {
            running = false;
        }
    }
}
