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
using System;
using System.Threading.Tasks;

namespace Moodis.Feature.Register
{
    [Activity(Label = "Register")]
    public class RegisterFaceActivity : AppCompatActivity
    {
        readonly RegisterViewModel registerViewModel = new RegisterViewModel();

        Camera camera;
        private bool CameraReleased = false;
        private bool updating = false;
        static readonly int REQUEST_CAMERA = 0;
        private readonly string TAG = nameof(RegisterFaceActivity);
        private const string EXTRA_UPDATE = "update";

        event EventHandler<TakenPictureArgs> AfterTakenPictures;

        private TextView PhotosLeft;
        private Button snapButton;
        private FrameLayout photoContainer;
        private View progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_face);

            this.SetSupportActionBar();

            PhotosLeft = FindViewById<TextView>(Resource.Id.photosLeft);
            PhotosLeft.Text = GetString(Resource.String.register_face_photos_left, RegisterViewModel.RequiredNumberOfPhotos - registerViewModel.photosTaken);
            updating = Intent.GetBooleanExtra(EXTRA_UPDATE, false);

            InitEventHandler();

            photoContainer = FindViewById<FrameLayout>(Resource.Id.photoContainerRegister);
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                RequestCameraPermission();
            }
            else
            {
                StartCamera();
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            SetResult(Result.Canceled);
            Finish();
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void RequestCameraPermission()
        {
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
            snapButton = FindViewById<Button>(Resource.Id.buttonTakePicture);
            camera = SetUpCamera();

            snapButton.Click += (sender, e) =>
            {
                try
                {
                    progressBar = FindViewById(Resource.Id.progressBarRegisterFace);
                    progressBar.Visibility = ViewStates.Visible;
                    progressBar.BringToFront();
                    snapButton.Enabled = false;

                    camera.StartPreview();
                    camera.TakePicture(null, null, new CameraPictureCallBack(this, AfterTakenPictures));//sends photo to cameraPicturecallBack
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
            AfterTakenPictures = async (sender, e) =>
            {
                e.ImagePath.RotateImage();
                var response = await registerViewModel.AddFaceToPerson(e.ImagePath);
                if (response == Response.ApiError || response == Response.GeneralError)
                {
                    Toast.MakeText(this, Resource.String.api_error, ToastLength.Short).Show();
                }
                else if (response == Response.FaceNotDetected)
                {
                    Toast.MakeText(this, Resource.String.warning_face_detection, ToastLength.Short).Show();
                }
                else if (response == Response.RegistrationDone)
                {
                    if (updating)
                    {
                        SetResult(Result.Ok);
                        Finish();
                    } 
                    else
                    {
                        await CheckIfUserFaceAlreadyUsedAsync(e.ImagePath);
                        Toast.MakeText(this, Resource.String.user_created, ToastLength.Short);
                        SetResult(Result.FirstUser);
                        Finish();
                    }
                }
                else
                {
                    PhotosLeft.Text = GetString(Resource.String.register_face_photos_left, RegisterViewModel.RequiredNumberOfPhotos - registerViewModel.photosTaken);
                }
                progressBar.Visibility = ViewStates.Gone;
                snapButton.Enabled = true;
            };
        }

        private async Task CheckIfUserFaceAlreadyUsedAsync(string imagePath)
        {
            var response = await registerViewModel.AuthenticateFace(imagePath);
            if (response == Response.ApiError)
            {
                Toast.MakeText(this, Resource.String.api_error, ToastLength.Short).Show();
            }
            else if (response == Response.UserExists)
            {
                Toast.MakeText(this, Resource.String.user_face_exists, ToastLength.Short).Show();
                SetResult(Result.Canceled);
                Finish();
            }
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