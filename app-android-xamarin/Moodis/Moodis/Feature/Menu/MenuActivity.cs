using System;
using System.Collections.Generic;
using System.IO;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Moodis.Feature.CameraFeature;
using Moodis.Ui;

namespace Moodis.Feature.Menu
{
    [Activity(Label = "Menu Activity")]
    public class MenuActivity : AppCompatActivity
    {
        private const string WarningInRequest = "Something wrong happened. Is your internet turned on?";
        private const string WarningFaceDetection = "Face not detected, please try to use better lighting and stay in front of camera";
        private const string WarningPlayingMusic = "Because face was not detected, cannot play music based on it.";
        public MenuViewModel ActivityMenuViewModel { get; set; }
        private AppCompatActivity parentActivity;
        private const string FormatDouble = "N3";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActivityMenuViewModel = new MenuViewModel();
            SetContentView(Resource.Layout.activity_menu);

            Intent intent = Intent;
            ActivityMenuViewModel.currentImage.ImagePath = intent.GetStringExtra("ImagePath");

            InitButtonsAndInputs();
            UpdateLabels();
            DeleteImage(ActivityMenuViewModel.currentImage.ImagePath);
        }

        public async void UpdateLabels()
        {
            var imageBox = FindViewById<ImageView>(Resource.Id.imageForView);
            Bitmap bmImg = BitmapFactory.DecodeFile(ActivityMenuViewModel.currentImage.ImagePath);
            bmImg = RotateImage(bmImg);
            imageBox.SetImageBitmap(bmImg);
            var emotionLabels = new List<TextView> { FindViewById<TextView>(Resource.Id.lblAnger), FindViewById<TextView>(Resource.Id.lblContempt), FindViewById<TextView>(Resource.Id.lblDisgust),
                FindViewById<TextView>(Resource.Id.lblFear), FindViewById<TextView>(Resource.Id.lblHappiness), FindViewById<TextView>(Resource.Id.lblNeutral), FindViewById<TextView>(Resource.Id.lblSadness),
                FindViewById<TextView>(Resource.Id.lblSurprise) };
            foreach (var label in emotionLabels)
            {
                label.Text = "loading";
            }
            /*
            try
            {
                await ActivityMenuViewModel.GetFaceEmotionsAsync();
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                Log.Debug(TAG,e.Message);
                Toast.MakeText(this, WarningInRequest, ToastLength.Short).Show();
                JavaSystem.Exit(0);
            }

            if (ActivityMenuViewModel.currentImage.emotions != null)
            {
                var counter = 0;
                foreach (var label in emotionLabels)
                {
                    label.Text = ActivityMenuViewModel.currentImage.emotions[counter].name + " : "
                        + ActivityMenuViewModel.currentImage.emotions[counter].confidence.ToString(FormatDouble);
                    counter++;
                }
                ActivityMenuViewModel.UserAddImage();
            }
            else
            {
                Toast.MakeText(this, WarningFaceDetection, ToastLength.Short).Show();
            }
            */
        }

        private Bitmap RotateImage(Bitmap image)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(-90);
            return Bitmap.CreateBitmap(image, 0, 0, image.Width, image.Height, matrix, true);
        }
        public override void OnBackPressed()
        {
            Finish();
        }

        private void InitButtonsAndInputs()
        {
            var bntToCalendar = FindViewById(Resource.Id.goToCalendar);
            var btnPlayMusic = FindViewById(Resource.Id.playMusic);
            var btnStopMusic = FindViewById(Resource.Id.StopMusic);
            var btnGroups = FindViewById(Resource.Id.groups);
            var btnTakePicture = FindViewById(Resource.Id.goToCamera);
            bntToCalendar.Click += (sender, e) => {
                BtnToCalendar_Click();
            };
            btnPlayMusic.Click += (sender, e) => {
                BtnPlayMusic_Click();
            };
            btnStopMusic.Click += (sender, e) => {
                BtnStopMusic_Click();
            };
            btnGroups.Click += (sender, e) => {
                BtnGroups_Click();
            };
            btnTakePicture.Click += (sender, e) => {
                BtnTakePicture_Click();
            };
        }

        private void DeleteImage(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void BtnTakePicture_Click()
        {
            var cameraActivity = new Intent(this, typeof(CameraActivity));
            StartActivity(cameraActivity);
            Finish();
        }

        private void BtnGroups_Click()
        {
            throw new NotImplementedException();
        }

        private void BtnStopMusic_Click()
        {
            throw new NotImplementedException();
        }

        private void BtnPlayMusic_Click()
        {
            throw new NotImplementedException();
        }

        private void BtnToCalendar_Click()
        {
            throw new NotImplementedException();
        }
    }
}
