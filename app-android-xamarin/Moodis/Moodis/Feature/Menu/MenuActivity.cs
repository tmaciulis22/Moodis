using System;
using System.Collections.Generic;
using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Java.Lang;
using Moodis.Feature.CameraFeature;
using Moodis.Ui;

namespace Moodis.Feature.Menu
{
    [Activity(Label = "Menu")]
    public class MenuActivity : AppCompatActivity
    {
        private readonly string TAG = nameof(MenuActivity);
        private MenuViewModel MenuViewModel;
        private const string FormatDouble = "N3";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            MenuViewModel = MenuViewModel.Instance;
            SetContentView(Resource.Layout.activity_menu);

            MenuViewModel.currentImage.ImagePath = Intent.GetStringExtra("ImagePath");
            MenuViewModel.image = BitmapFactory.DecodeFile(MenuViewModel.currentImage.ImagePath);

            InitButtons();
            UpdateLabels();
            MenuViewModel.DeleteImage();
        }

        public async void UpdateLabels()
        {
            var imageBox = FindViewById<ImageView>(Resource.Id.imageForView);
            imageBox.SetImageBitmap(MenuViewModel.RotateImage());

            var emotionLabels = new List<TextView> { FindViewById<TextView>(Resource.Id.lblAnger), FindViewById<TextView>(Resource.Id.lblContempt), FindViewById<TextView>(Resource.Id.lblDisgust),
                FindViewById<TextView>(Resource.Id.lblFear), FindViewById<TextView>(Resource.Id.lblHappiness), FindViewById<TextView>(Resource.Id.lblNeutral), FindViewById<TextView>(Resource.Id.lblSadness),
                FindViewById<TextView>(Resource.Id.lblSurprise) };
            foreach (var label in emotionLabels)
            {
                label.Text = GetString(Resource.String.loading);
            }
            /* COMENTED UNTIL WE FIGURE OUT A WAY TO STORE ENVIROMENTAL VARIABLES
            try
            {
                await ActivityMenuViewModel.GetFaceEmotionsAsync();
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                Log.Debug(TAG,e.Message);
                Toast.MakeText(this, GetString(Resource.String.warning_in_request), ToastLength.Short).Show();
                JavaSystem.Exit(0);
            }
            */
            if (MenuViewModel.currentImage.emotions != null)
            {
                var counter = 0;
                foreach (var label in emotionLabels)
                {
                    label.Text = MenuViewModel.currentImage.emotions[counter].name + " : "
                        + MenuViewModel.currentImage.emotions[counter].confidence.ToString(FormatDouble);
                    counter++;
                }
                MenuViewModel.UserAddImage();
            }
            else
            {
                Toast.MakeText(this, GetString(Resource.String.warning_face_detection), ToastLength.Short).Show();
            }
        }
        public override void OnBackPressed()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            //TODO figure out what to do when back pressed while in menu (maybe logout user ?) it currently exits program.
        }

        private void InitButtons()
        {
            var bntToCalendar = FindViewById(Resource.Id.goToCalendar);
            var btnPlayMusic = FindViewById(Resource.Id.playMusic);
            var btnStopMusic = FindViewById(Resource.Id.StopMusic);
            var btnGroups = FindViewById(Resource.Id.groups);
            var btnTakePicture = FindViewById(Resource.Id.goToCamera);

            bntToCalendar.Click += (sender, e) => {
                throw new NotImplementedException();
            };
            btnPlayMusic.Click += (sender, e) => {
                throw new NotImplementedException();
            };
            btnStopMusic.Click += (sender, e) => {
                throw new NotImplementedException();
            };
            btnGroups.Click += (sender, e) => {
                throw new NotImplementedException();
            };
            btnTakePicture.Click += (sender, e) => {
                var cameraActivity = new Intent(this, typeof(CameraActivity));
                StartActivity(cameraActivity);
            };
        }
    }
}
