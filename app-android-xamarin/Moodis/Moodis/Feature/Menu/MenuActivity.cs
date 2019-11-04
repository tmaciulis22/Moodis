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
using Moodis.Extensions;
using Moodis.Feature.CameraFeature;
using Moodis.History;
using Moodis.Ui;

namespace Moodis.Feature.Menu
{
    [Activity(Label = "Menu")]
    public class MenuActivity : AppCompatActivity
    {
        private MenuViewModel MenuViewModel;
        private const string FormatDouble = "N3";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetSupportActionBar();

            MenuViewModel = MenuViewModel.Instance;
            SetContentView(Resource.Layout.activity_menu);

            MenuViewModel.currentImage.ImagePath = Intent.GetStringExtra("ImagePath");
            MenuViewModel.image = BitmapFactory.DecodeFile(MenuViewModel.currentImage.ImagePath);

            InitButtons();
            UpdateLabels();
            MenuViewModel.DeleteImage();
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
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
            if (MenuViewModel.currentImage.emotions != null)
            {
                var counter = 0;
                foreach (var label in emotionLabels)
                {
                    label.Text = MenuViewModel.currentImage.emotions[counter].name + " : "
                        + MenuViewModel.currentImage.emotions[counter].confidence.ToString(FormatDouble);
                    counter++;
                }
                MenuViewModel.AddImage();
            }
            else
            {
                Toast.MakeText(this, GetString(Resource.String.warning_face_detection), ToastLength.Short).Show();
            }
        }
        public override void OnBackPressed()
        {
            Finish();
        }

        private void InitButtons()
        {
            var bntToCalendar = FindViewById(Resource.Id.goToCalendar);
            var btnPlayMusic = FindViewById(Resource.Id.playMusic);
            var btnStopMusic = FindViewById(Resource.Id.StopMusic);
            var btnGroups = FindViewById(Resource.Id.groups);

            bntToCalendar.Click += (sender, e) => {
                StartActivity(new Intent(this, typeof(HistoryActivity)));
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
        }
    }
}
