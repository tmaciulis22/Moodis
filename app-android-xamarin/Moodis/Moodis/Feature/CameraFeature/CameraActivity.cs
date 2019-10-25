using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Moodis.Feature.CameraFeature;

namespace Moodis.Feature.CameraFeature
{
    [Activity(Label = "Camera activity")]
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
        }

        private void InitButtonsAndInputs()
        {
        }
    }
}