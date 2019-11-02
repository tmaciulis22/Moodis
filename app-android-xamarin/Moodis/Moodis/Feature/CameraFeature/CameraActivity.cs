using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Moodis.Feature.CameraFeature;
using Moodis.Feature.Menu;
using Moodis.Ui;
using System;

namespace Moodis.Feature.CameraFeature
{
    [Activity(Label = "Camera")]
    class CameraActivity : AppCompatActivity
    {
        private readonly string TAG = nameof(CameraActivity);
        static readonly int REQUEST_CAMERA = 0;
        View layout;
        private CameraFragment cameraFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_camera);
            layout = FindViewById(Resource.Id.content_frame);

            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
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
            Finish();
        }
        private void StartCamera()
        {
            cameraFragment = new CameraFragment();
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, cameraFragment)
                .Commit();
        }

        private void RequestCameraPermission()
        {
            Log.Info(TAG, "CAMERA permission has NOT been granted. Requesting permission.");

            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera))
            {
                Snackbar.Make(layout, Resource.String.permission_camera_rationale,
                    Snackbar.LengthIndefinite)
                    .SetAction(Resource.String.ok, new Action<View>(delegate (View obj) {
                        ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
                    })).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, REQUEST_CAMERA);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == REQUEST_CAMERA)
            {
                Log.Info(TAG, "Received response for Camera permission request.");
                if (grantResults.Length == 1 && grantResults[0] == Permission.Granted)
                {
                    Log.Info(TAG, "CAMERA permission has now been granted. Showing preview.");
                    Snackbar.Make(layout, Resource.String.permission_available_camera, Snackbar.LengthShort).Show();
                    StartCamera();
                }
                else
                {
                    Log.Info(TAG, "CAMERA permission has not been granted. Asking again.");
                    Snackbar.Make(layout, Resource.String.permissions_not_granted, Snackbar.LengthShort).Show();
                    RequestCameraPermission();
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }
    }
}