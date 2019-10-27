using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Moodis.Feature.SignIn;
using Android.Content;
using Moodis.Feature.CameraFeature;

namespace Moodis
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static int REQUEST_CODE_LOGIN = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            StartActivityForResult(new Intent(this, typeof(SignInActivity)), REQUEST_CODE_LOGIN);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok && requestCode == REQUEST_CODE_LOGIN)
            {
                var cameraActivity = new Intent(this, typeof(CameraActivity));
                StartActivity(cameraActivity);
            }
            else if (resultCode == Result.Canceled && requestCode == REQUEST_CODE_LOGIN)
            {
                Finish();
            }
        }
    }
}