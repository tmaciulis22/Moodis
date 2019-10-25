using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Moodis.Feature.CameraFeature;
using System;

namespace Moodis.Feature.CameraFeature
{
    [Activity(Label = "Camera activity")]
    class CameraActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_camera);


            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.AccessFineLocation))
            {
                Log.Info("REQUESTPERMISSION", "Displaying camera permission rationale to provide additional context.");

                var requiredPermissions = new String[] { Manifest.Permission.AccessFineLocation };
                Snackbar.Make((View)Resource.Layout.activity_camera,
                               Resource.String.permission_camera_rationale,
                               Snackbar.LengthIndefinite)
                        .SetAction(Resource.String.ok,
                                   new Action<View>(delegate (View obj) {
                                       ActivityCompat.RequestPermissions(this, requiredPermissions, 1);
                                   }
                        )
                ).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Camera }, 1);
                FragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, new CameraFragment())
               .Commit();
            }
        }

        private void InitButtonsAndInputs()
        {
        }
    }
}