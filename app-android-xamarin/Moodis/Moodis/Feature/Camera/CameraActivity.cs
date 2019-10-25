using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace Moodis.Feature.Camera
{
    [Activity(Label = "Camera")]
    class CameraActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_camera);
            FragmentManager.BeginTransaction()
              .Replace(Resource.Id.content_frame, new CameraFragment())
              .Commit();
            InitButtonsAndInputs();
            //TODO when Android.Arch.Lifecycle lib gets updated use this provider, so various lifecycle and configuration changes won't affect data stored in viewmodel
            //SignInViewModel = ViewModelProviders.Of(this).Get(Java.Lang.Class.FromType(typeof(SignInViewModel))) as SignInViewModel;
        }

        private void InitButtonsAndInputs()
        {
            var btnTakePicture = FindViewById(Resource.Id.snap);
        }
    }
}