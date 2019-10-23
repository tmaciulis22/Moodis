using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Arch.Lifecycle;
using Android.Widget;
using Android.Content;

namespace Moodis.Feature.SignIn
{
    [Activity(Label = "SignInActivity")]
    public class SignInActivity : AppCompatActivity
    {
        private static string EXTRA_USER = "EXTRA_USER";

        private SignInViewModel SignInViewModel = new SignInViewModel();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_signin);
            InitButtonsAndInputs();
            //TODO when Android.Arch.Lifecycle lib gets updated use this provider, so various lifecycle and configuration changes won't affect data stored in viewmodel
            //SignInViewModel = ViewModelProviders.Of(this).Get(Java.Lang.Class.FromType(typeof(SignInViewModel))) as SignInViewModel;
        }

        private void InitButtonsAndInputs()
        {
            var usernameInput = FindViewById<EditText>(Resource.Id.usernameInput);
            var passwordInput = FindViewById<EditText>(Resource.Id.passwordInput);
            var signInButton = FindViewById(Resource.Id.signInButton);
            var signInWithFaceButton = FindViewById(Resource.Id.signInFaceButton);
            var registerButton = FindViewById(Resource.Id.registerButton);

            usernameInput.TextChanged += (sender, e) => {
                if (string.IsNullOrEmpty((sender as EditText).Text))
                {
                    usernameInput.SetError(GetString(Resource.String.username_empty_error), null);
                }
            };
            passwordInput.TextChanged += (sender, e) => {
                if (string.IsNullOrEmpty((sender as EditText).Text))
                {
                    usernameInput.SetError(GetString(Resource.String.password_empty_error), null);
                }
            };

            signInButton.Click += (sender, e) => {
                if (string.IsNullOrEmpty(usernameInput.Text))
                {
                    usernameInput.SetError(GetString(Resource.String.username_empty_error), null);
                }
                else if (string.IsNullOrWhiteSpace(passwordInput.Text))
                {
                    passwordInput.SetError(GetString(Resource.String.password_empty_error), null);
                }
                else
                {
                    if (SignInViewModel.Authenticate(usernameInput.Text, passwordInput.Text))
                    {
                        SetResult(Result.Ok);
                        Finish();
                    }
                    else
                    {
                        usernameInput.SetError(GetString(Resource.String.user_not_found_error), null);
                    }
                }
            };
            signInWithFaceButton.Click += (sender, e) => {
                //StartActivityForResult(new Android.Content.Intent(this, typeof(CameraActivity)), REQUEST_CODE_CAMERA);
            };
            registerButton.Click += (sender, e) =>
            {
                //StartActivityForResult(new Android.Content.Intent(this, typeof(RegisterActivity)), REQUEST_CODE_REGISTER);
            };
        }
    }
}