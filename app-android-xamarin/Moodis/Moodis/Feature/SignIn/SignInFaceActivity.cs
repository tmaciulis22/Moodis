using Android;
using Android.App;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Support.Design.Widget;
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
using Moodis.Ui;
using System;

namespace Moodis.Feature.SignIn
{
    [Activity(Label = "Sign In")]
    public class SignInFaceActivity : AppCompatActivity
    {
        readonly SignInViewModel SignInViewModel = new SignInViewModel();

        Camera camera;
        private bool CameraReleased = false;
        static readonly int REQUEST_CAMERA = 0;
        private readonly string TAG = nameof(SignInFaceActivity);

        private Button snapButton;
        private FrameLayout photoContainer;
        private View progressBar;

        event EventHandler<TakenPictureArgs> AfterTakenPicture;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_signin_face);

            this.SetSupportActionBar();

            InitEventHandler();

            photoContainer = FindViewById<FrameLayout>(Resource.Id.photoContainerSignIn);
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
            photoContainer = FindViewById<FrameLayout>(Resource.Id.photoContainerSignIn);
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera))
            {
                Snackbar.Make(photoContainer, Resource.String.permission_camera_rationale,
                    Snackbar.LengthIndefinite)
                    .SetAction(Resource.String.ok, new Action<View>(delegate (View obj)
                    {
                        ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
                    })).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
            }
        }

        private void StartCamera()
        {

            snapButton = FindViewById<Button>(Resource.Id.buttonSignIn);
            camera = SetUpCamera();

            snapButton.Click += (sender, e) =>
            {
                try
                {
                    progressBar = FindViewById(Resource.Id.progressBarSignInFace);
                    progressBar.Visibility = ViewStates.Visible;
                    progressBar.BringToFront();
                    snapButton.Enabled = false;

                    camera.StartPreview();
                    camera.TakePicture(null, null, new CameraPictureCallBack(this, AfterTakenPicture));//sends photo to cameraPicturecallBack
                }
                catch (Exception ex)
                {
                    progressBar.Visibility = ViewStates.Gone;
                    snapButton.Enabled = true;
                    Toast.MakeText(this, Resource.String.camera_error, ToastLength.Short).Show();
                    Log.Error(TAG, "Error taking picture: " + ex.Message);
                }
            };

            SetCameraPreview();
        }

        protected override void OnDestroy()
        {
            try
            {
                camera.StopPreview();
                camera.Release();
            }
            catch (Exception e)
            {
                Log.Error(TAG, e.StackTrace);
            }

            CameraReleased = true;
            base.OnDestroy();
        }

        protected override void OnResume()
        {
            if (CameraReleased)
            {
                try
                {
                    camera.Reconnect();
                    camera.StartPreview();
                }
                catch (Exception e)
                {
                    Log.Error(TAG, e.StackTrace);
                }
                CameraReleased = false;
            }
            base.OnResume();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == REQUEST_CAMERA)
            {
                if (grantResults.Length == 1 && grantResults[0] == Permission.Granted)
                {
                    Snackbar.Make(photoContainer, Resource.String.permission_available_camera, Snackbar.LengthShort).Show();
                    StartCamera();
                }
                else
                {
                    Snackbar.Make(photoContainer, Resource.String.permissions_not_granted, Snackbar.LengthShort).Show();
                    RequestCameraPermission();
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        private void InitEventHandler()
        {
            AfterTakenPicture = async (sender, e) =>
            {
                var menuViewModel = MenuViewModel.Instance;
                var response = await SignInViewModel.AuthenticateWithFace(e.ImagePath, menuViewModel.currentImage.SetImageInfo);
                if (response == Response.ApiError)
                {
                    Toast.MakeText(this, Resource.String.api_error, ToastLength.Short).Show();
                }
                else if (response == Response.FaceNotDetected)
                {
                    Toast.MakeText(this, Resource.String.warning_face_detection, ToastLength.Short).Show();
                }
                else if (response == Response.UserNotFound)
                {
                    Toast.MakeText(this, Resource.String.user_not_found_error, ToastLength.Short).Show();
                }
                else
                {
                    menuViewModel.currentImage.ImagePath = e.ImagePath;
                    SetResult(Result.Ok);
                    Finish();
                }
                progressBar.Visibility = ViewStates.Gone;
                snapButton.Enabled = true;
            };
        }

        private void SetCameraPreview()
        {
            photoContainer.AddView(new CameraPreview(this, camera));
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