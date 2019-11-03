using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Moodis.Events;
using Moodis.Extensions;

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
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }
    }
}