using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Moodis.Feature.MP3Player;
using Moodis.Feature.Statistics;
using Moodis.Feature.Group;
using Moodis.Feature.Login;

namespace Moodis.Ui
{
    public partial class MenuForm : Form
    {
        public bool running = false;
        private const string WarningInRequest = "Something wrong happened. Is your internet turned on?";
        private const string WarningFaceDetection = "Face not detected, please try to use better lighting and stay in front of camera";
        private const string WarningPlayingMusic = "Because face was not detected, cannot play music based on it.";
        public MenuViewModel menuViewModel;
        private Form parentForm;
        private MusicPlayerModel player = new MusicPlayerModel();
        private const string FormatDouble = "N3";

        public MenuForm(MenuViewModel viewModel, Form parent)
        {
            InitializeComponent();
            menuViewModel = viewModel;
            parentForm = parent;
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
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
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
            var calendarForm = new CalendarForm(new CalendarViewModel(), this, SignInViewModel.currentUser.username);
            calendarForm.StartPosition = FormStartPosition.Manual;
            calendarForm.Location = Location;
            calendarForm.Show();
            player.StopMusic();
        }

        private void MenuFormClose(object sender, FormClosingEventArgs e)
        {
            player.StopMusic();
            Application.Exit();
        }

        private void BtnToCamera_Click(object sender, EventArgs e)
        {
            Hide();
            parentForm.Location = Location;
            parentForm.Show();
        }

        private void ButtonMusicController_Click(object sender, EventArgs e)
        {
            if(menuViewModel.currentImage.emotions != null)
            {
                player.StartMusic(menuViewModel.getHighestEmotionIndex());
            }
            else
            {
                MessageBox.Show(WarningPlayingMusic);
            }
        }

        private void BtnStopMusic_Click(object sender, EventArgs e)
        {
            player.StopMusic();
        }


        private void GroupButton_Click(object sender, EventArgs e)
        {
            var groupForm = new GroupForm(this);
            groupForm.StartPosition = FormStartPosition.Manual;
            groupForm.Location = Location;
            groupForm.Show();
            Hide();
        }
    }
}
