using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Moodis.Constants.Enums;
using Moodis.Events;
using Moodis.Extensions;
using Moodis.Feature.CameraFeature;

namespace Moodis.Feature.Register
{
    [Activity(Label = "Register")]
    public class RegisterFaceActivity : AppCompatActivity
    {
        RegisterViewModel registerViewModel = new RegisterViewModel();

        Camera camera;
        private bool CameraReleased = false;
        static readonly int REQUEST_CAMERA = 0;
        private readonly string TAG = nameof(RegisterFaceActivity);

        event EventHandler<TakenPictureArgs> AfterTakenPictures;

        TextView PhotosLeft;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_face);

            PhotosLeft = FindViewById<TextView>(Resource.Id.photosLeft);
            PhotosLeft.Text = GetString(Resource.String.register_face_photos_left, RegisterViewModel.RequiredNumberOfPhotos - registerViewModel.photosTaken);

            this.SetSupportActionBar();

            InitEventHandler();

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                RequestCameraPermission();
            }
            else
            {
                StartCamera();
            }
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            SetResult(Result.Canceled);
            Finish();
        }

        private void RequestCameraPermission()
        {
            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
        }

        private void StartCamera()
        {

            var snapButton = FindViewById<Button>(Resource.Id.buttonTakePicture);
            camera = SetUpCamera();

            snapButton.Click += (sender, e) => {
                try
                {
                    camera.StartPreview();
                    camera.TakePicture(null, null, new CameraPictureCallBack(this, AfterTakenPictures));//sends photo to cameraPicturecallBack
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, Resource.String.camera_error, ToastLength.Short).Show();
                    Log.Error(TAG, "Error taking picture: " + ex.Message);
                }
            };

            SetCameraPreview();
        }

        protected override void OnDestroy()
        {
            camera.StopPreview();
            camera.Release();
            CameraReleased = true;
            base.OnDestroy();
        }

        protected override void OnResume()
        {
            if (CameraReleased)
            {
                camera.Reconnect();
                camera.StartPreview();
                CameraReleased = false;
            }
            base.OnResume();
        }

        private void InitEventHandler()
        {
            AfterTakenPictures = async (sender, e) =>
            {
                var progressBar = FindViewById(Resource.Id.progressBarRegisterFace);
                progressBar.Visibility = ViewStates.Visible;

                var response = await registerViewModel.AddFaceToPerson(e.ImagePath);
                if (response == Response.ApiError || response == Response.ApiTrainingError)
                {
                    Toast.MakeText(this, Resource.String.api_error, ToastLength.Short).Show();
                }
                else if (response == Response.RegistrationDone)
                {
                    SetResult(Result.FirstUser);
                    Finish();
                }
                else
                {
                    PhotosLeft.Text = GetString(Resource.String.register_face_photos_left, RegisterViewModel.RequiredNumberOfPhotos - registerViewModel.photosTaken);
                }
                progressBar.Visibility = ViewStates.Gone;
            };
        }

        private void SetCameraPreview()
        {
            var photoContainter = FindViewById<FrameLayout>(Resource.Id.photoContainer);
            photoContainter.AddView(new CameraPreview(this, camera));
        }

        private Camera SetUpCamera()
        {
            Camera c = null;
            try
            {
                c = Camera.Open(1);
            }
            catch (Exception e)
            {
                Log.Debug(TAG, "Device camera not available now. " + e.Message);
            }

            return c;
        }
    }
}