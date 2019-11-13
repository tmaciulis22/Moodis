using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Feature.CameraFeature;
using Moodis.Feature.Music;
using Moodis.Feature.SignIn;
using Moodis.History;
using Moodis.Ui;
using System;
using System.Collections.Generic;

namespace Moodis.Feature.Menu
{
    [Activity(Label = "Menu")]
    public class MenuActivity : AppCompatActivity
    {
        private readonly string TAG = nameof(MenuActivity);
        private MenuViewModel MenuViewModel;
        private MusicPlayer MusicPlayer;
        private const string FormatDouble = "N3";

        private bool JustSignedIn = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetSupportActionBar();

            MenuViewModel = MenuViewModel.Instance;
            MusicPlayer = new MusicPlayer(this);
            SetContentView(Resource.Layout.activity_menu);

            if (MenuViewModel.currentImage.ImagePath == null)
            {
                StartActivity(new Intent(this, typeof(CameraActivity)));
                Finish();
            }

            MenuViewModel.image = BitmapFactory.DecodeFile(MenuViewModel.currentImage.ImagePath);

            JustSignedIn = Intent.GetBooleanExtra(SignInActivity.EXTRA_SIGNED_IN, false);

            InitButtons();
            UpdateLabels();
            MenuViewModel.DeleteImage();
        }

        public override bool OnSupportNavigateUp()
        {
            if (JustSignedIn)
            {
                StartActivity(new Intent(this, typeof(CameraActivity)));
                Finish();
            }
            else
            {
                OnBackPressed();
            }
            return true;
        }

        public void UpdateLabels()
        {
            var imageBox = FindViewById<ImageView>(Resource.Id.imageForView);
            var emotionLabels = new List<TextView> { FindViewById<TextView>(Resource.Id.lblAnger), FindViewById<TextView>(Resource.Id.lblContempt), FindViewById<TextView>(Resource.Id.lblDisgust),
                FindViewById<TextView>(Resource.Id.lblFear), FindViewById<TextView>(Resource.Id.lblHappiness), FindViewById<TextView>(Resource.Id.lblNeutral), FindViewById<TextView>(Resource.Id.lblSadness),
                FindViewById<TextView>(Resource.Id.lblSurprise) };

            imageBox.SetImageBitmap(MenuViewModel.image);
            foreach (var label in emotionLabels)
            {
                label.Text = GetString(Resource.String.loading);
            }

            if (MenuViewModel.currentImage.emotions != null)
            {
                var counter = 0;
                foreach (var label in emotionLabels)
                {
                    label.Text = MenuViewModel.currentImage.emotions[counter].Name + " : "
                        + MenuViewModel.currentImage.emotions[counter].Confidence.ToString(FormatDouble);
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

        //TODO SHOW LOGOUT WINDOW WHEN USER LOGS OUT FROM A MENU THAT IS YET TO BE MADE.
        public void LogoutWindowShow()
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle(Resource.String.logout)
                .SetMessage(Resource.String.logout_confirmation_message)
                .SetNegativeButton(Resource.String.no, (senderAlert, args) => { })
                .SetPositiveButton(Resource.String.yes, (senderAlert, args) => {
                    StartActivity(new Intent(this, typeof(SignInActivity)));
                    Finish();
                });
            builder.Create().Show();
            builder.Dispose();
        }
        private void InitButtons()
        {
            var btnHistory = FindViewById(Resource.Id.buttonHistory);
            var btnPlayMusic = FindViewById(Resource.Id.playMusic);
            var btnStopMusic = FindViewById(Resource.Id.StopMusic);
            var btnGroups = FindViewById(Resource.Id.groups);

            btnHistory.Click += (sender, e) =>
            {
                StartActivity(new Intent(this, typeof(HistoryActivity)));
            };
            btnPlayMusic.Click += (sender, e) =>
            {
                if (MenuViewModel.currentImage.emotions != null && !MusicPlayer.IsPlaying())
                {
                    int[] musicLabels = { Resource.Raw.Anger, Resource.Raw.Contempt, Resource.Raw.Disgust, Resource.Raw.Fear, Resource.Raw.Happiness, Resource.Raw.Neutral,
                                Resource.Raw.Sadness, Resource.Raw.Surprise };
                    if (MenuViewModel.GetHighestEmotionIndex() != -1)
                    {
                        MusicPlayer.Play(musicLabels[MenuViewModel.GetHighestEmotionIndex()]);
                    }
                }
                else if (MusicPlayer.IsPlaying())
                {
                    Snackbar.Make(FindViewById(Resource.Id.menuActivity), Resource.String.info_music_is_playing, Snackbar.LengthShort).Show();
                }
                else
                {
                    Log.Debug(TAG, Resources.GetString(Resource.String.warning_playing_music));
                    Snackbar.Make(FindViewById(Resource.Id.menuActivity), Resource.String.warning_playing_music, Snackbar.LengthShort).Show();
                }

            };
            btnStopMusic.Click += (sender, e) =>
            {
                if (MusicPlayer != null)
                    MusicPlayer.Stop();
            };
            btnGroups.Click += (sender, e) =>
            {
                throw new NotImplementedException();
            };
        }
    }
}
