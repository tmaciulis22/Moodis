using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Moodis.Feature.Login;
using Moodis.Feature.MP3Player;
using Moodis.Feature.Statistics;
using Music;

namespace Moodis.Ui
{
    public partial class MenuForm : Form
    {
        public bool running = false;
        private const string WarningInRequest = "Azure api request failed. Is your internet turned on ?";
        private const string WarningFaceDetection = "Face not detected, please try to use better lighting and stay in front of camera";
        public MenuViewModel menuViewModel;
        private MusicPlayerModel player = new MusicPlayerModel();
        private const string FormatDouble = "N3";

        public MenuForm(MenuViewModel viewModel)
        {
            InitializeComponent();
            menuViewModel = viewModel;
            UpdateLabels();
        }

        public async void UpdateLabels()
        {
            imgTakenPicture.Image = menuViewModel.ShowImage(menuViewModel.currentImage.ImagePath);
            var emotionLabels = new List<Label> { lblAnger, lblContempt, lblDisgust, lblFear, lblHappiness, lblNeutral, lblSadness, lblSurprise };
            foreach (var label in emotionLabels)
            {
                label.Text = "loading";
            }
            
            try
            {
                await menuViewModel.GetFaceEmotionsAsync();
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                Console.WriteLine(e);
                MessageBox.Show(WarningInRequest);
                Application.Exit();
            }

            if (menuViewModel.currentImage.emotions != null)
            {
                var counter = 0;
                foreach (var label in emotionLabels)
                {
                    label.Text = menuViewModel.currentImage.emotions[counter].name + " : " 
                        + menuViewModel.currentImage.emotions[counter].confidence.ToString(FormatDouble);
                    counter++;
                }
                menuViewModel.UserAddImage();
            }
            else
            {
               MessageBox.Show(WarningFaceDetection);
            }
        }
        private void BtnCalendar_Click(object sender, EventArgs e)
        {
            Hide();
            var calendarForm = new CalendarForm(new CalendarViewModel(), this);
            calendarForm.Show();
            player.StopMusic();

        }
        private void MenuFormClose(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                player.StopMusic();
            }
        }
        private void ButtonMusicController_Click(object sender, EventArgs e)
        {
            double highestEmotion = 0;
            int counter = 0;
            int index = -1;
            foreach (var label in menuViewModel.currentImage.emotions)
            {
                if(label.confidence > highestEmotion)
                {
                    highestEmotion = label.confidence;
                    index = counter;
                }
                counter++;
            }
            player.StartMusic(index);
        }
    }
}
