using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moodis.Feature.SignIn;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Moodis.Feature.SignIn
{
    [Activity(Label = "SignInActivity")]
    public class SignInActivity : Activity
    {
        private SignInViewModel signInViewModel = new SignInViewModel();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_signin);
        }
    }
}