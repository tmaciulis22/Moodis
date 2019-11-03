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

namespace Moodis.Feature.SignIn
{
    [Activity(Label = "SignInFaceActivity")]
    public class SignInFaceActivity : AppCompatActivity
    {
        SignInViewModel SignInViewModel = new SignInViewModel();

        Camera camera;
        private bool CameraReleased = false;
        static readonly int REQUEST_CAMERA = 0;
        private readonly string TAG = nameof(SignInFaceActivity);

        event EventHandler<TakenPictureArgs> AfterTakenPictures;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_signin_face);

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

        private void RequestCameraPermission()
        {
            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
        }

        private void StartCamera()
        {

            var snapButton = FindViewById<Button>(Resource.Id.buttonSignIn);
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
                var progressBar = FindViewById(Resource.Id.progressBarSignInFace);
                progressBar.Visibility = ViewStates.Visible;
                progressBar.BringToFront();

                var response = await SignInViewModel.AuthenticateWithFace(e.ImagePath);
                if (response == Response.ApiError)
                {
                    Toast.MakeText(this, Resource.String.api_error, ToastLength.Short).Show();
                }
                else if (response == Response.UserNotFound)
                {
                    Toast.MakeText(this, Resource.String.user_not_found_error, ToastLength.Short).Show();
                }
                else
                {
                    SetResult(Result.Ok);
                    Finish();
                }
            };
        }

        private void SetCameraPreview()
        {
            var photoContainter = FindViewById<FrameLayout>(Resource.Id.photoContainerSignIn);
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