﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Moodis.Feature.SignIn;
using Android.Content;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Moodis.Feature.CameraFeature;
using Moodis.Feature.Menu;

namespace Moodis
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static int REQUEST_CODE_LOGIN = 1;
        public static int REQUEST_CODE_FACE = 2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenter.Start("4493172f-d0e0-49d4-bb48-bc5a529ac6ee",typeof(Analytics), typeof(Crashes));
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            StartActivityForResult(new Intent(this, typeof(SignInFaceActivity)), REQUEST_CODE_FACE);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(resultCode == Result.Ok && requestCode == REQUEST_CODE_FACE)
            {
                var intent = new Intent(this, typeof(MenuActivity)).PutExtra(SignInFaceActivity.EXTRA_SIGNED_IN, data.GetBooleanExtra(SignInFaceActivity.EXTRA_SIGNED_IN, false));
                StartActivity(intent);
            }
            else if(resultCode == Result.Canceled && requestCode == REQUEST_CODE_FACE)
            {
                StartActivityForResult(new Intent(this, typeof(SignInActivity)), REQUEST_CODE_LOGIN);
            }

            if (resultCode == Result.Ok && requestCode == REQUEST_CODE_LOGIN)
            {
                var intent = new Intent(this, typeof(MenuActivity))
                    .PutExtra(SignInActivity.EXTRA_SIGNED_IN, data.GetBooleanExtra(SignInActivity.EXTRA_SIGNED_IN, false));

                StartActivity(intent);
            }
            else if (resultCode == Result.Canceled && requestCode == REQUEST_CODE_LOGIN)
            {
                Finish();
            }

        }
    }
}