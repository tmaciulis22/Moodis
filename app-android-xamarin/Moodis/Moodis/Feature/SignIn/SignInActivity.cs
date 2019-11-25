using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Feature.Register;

namespace Moodis.Feature.SignIn
{
    [Activity(Label = "Sign In")]
    public class SignInActivity : AppCompatActivity
    {
        public static int REQUEST_CODE_REGISTER = 1;
        public static int REQUEST_CODE_FACE = 2;
        public static int REQUEST_CODE_REGISTER_FACE = 3;
        private readonly SignInViewModel SignInViewModel = new SignInViewModel();

        public static string EXTRA_SIGNED_IN = "EXTRA_SIGNED_IN";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_signin);
            InitButtonsAndInputs();
            //TODO when Android.Arch.Lifecycle lib gets updated use this provider, so various lifecycle and configuration changes won't affect data stored in viewmodel
            //SignInViewModel = ViewModelProviders.Of(this).Get(Java.Lang.Class.FromType(typeof(SignInViewModel))) as SignInViewModel;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            SetResult(Result.Canceled);
            Finish();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //As we implement new Activities there will be more if statements
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.FirstUser && requestCode == REQUEST_CODE_REGISTER)
            {
                Toast.MakeText(this, Resource.String.user_created, ToastLength.Short);
            }
            else if (resultCode == Result.Ok && requestCode == REQUEST_CODE_FACE)
            {
                SetResult(Result.Ok, new Intent().PutExtra(EXTRA_SIGNED_IN, true));
                Finish();
            }
            else if (resultCode == Result.Ok && requestCode == REQUEST_CODE_REGISTER_FACE)
            {
                SetResult(Result.Ok, new Intent().PutExtra(EXTRA_SIGNED_IN, true));
                Finish();
            }
        }

        private void InitButtonsAndInputs()
        {
            var usernameInput = FindViewById<EditText>(Resource.Id.usernameInput);
            var passwordInput = FindViewById<EditText>(Resource.Id.passwordInput);
            var signInButton = FindViewById(Resource.Id.signInButton);
            var signInWithFaceButton = FindViewById(Resource.Id.signInFaceButton);
            var registerButton = FindViewById(Resource.Id.registerButton);

            usernameInput.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty((sender as EditText).Text))
                {
                    usernameInput.SetError(GetString(Resource.String.username_empty_error), null);
                }
            };
            passwordInput.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty((sender as EditText).Text))
                {
                    usernameInput.SetError(GetString(Resource.String.password_empty_error), null);
                }
            };

            usernameInput.EditorAction += (sender, e) =>
            {
                if (e.ActionId == ImeAction.Next)
                {
                    if (string.IsNullOrEmpty((sender as EditText).Text))
                    {
                        usernameInput.SetError(GetString(Resource.String.username_empty_error), null);
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = false;
                }
            };

            passwordInput.KeyPress += (sender, e) =>
            {
                if (e.KeyCode == Android.Views.Keycode.Enter && e.Event.Action == Android.Views.KeyEventActions.Down)
                {
                    e.Handled = true;
                    if (string.IsNullOrWhiteSpace((sender as EditText).Text))
                    {
                        passwordInput.SetError(GetString(Resource.String.password_empty_error), null);
                    }
                    else
                    {
                        this.HideKeyboard(passwordInput);
                        SignIn(usernameInput.Text, passwordInput.Text);
                    }
                }
                else
                {
                    e.Handled = false;
                }
            };

            signInButton.Click += (sender, e) =>
            {
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
                    SignIn(usernameInput.Text, passwordInput.Text);
                }
            };
            signInWithFaceButton.Click += (sender, e) =>
            {
                StartActivityForResult(new Intent(this, typeof(SignInFaceActivity)), REQUEST_CODE_FACE);
            };
            registerButton.Click += (sender, e) =>
            {
                StartActivityForResult(new Intent(this, typeof(RegisterActivity)), REQUEST_CODE_REGISTER);
            };
        }

        private void SignIn(string username, string password)
        {
            var progressBar = FindViewById(Resource.Id.progressBarSignIn);
            progressBar.Visibility = ViewStates.Visible;
            progressBar.BringToFront();

            if (SignInViewModel.Authenticate(username, password))
            {
                DisplayFaceNewewWindow();
                SetResult(Result.Ok, new Intent().PutExtra(EXTRA_SIGNED_IN, true));
                Finish();
            }
            else
            {
                progressBar.Visibility = ViewStates.Gone;
                Toast.MakeText(this, Resource.String.user_not_found_error, ToastLength.Short).Show();
            }
        }

        private void DisplayFaceNewewWindow()
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this)
                .SetTitle(Resource.String.update_face)
                .SetMessage(Resource.String.update_face_confirmation_message)
                .SetNegativeButton(Resource.String.no, (senderAlert, args) => { })
                .SetPositiveButton(Resource.String.yes, (senderAlert, args) => {
                    StartActivityForResult(new Intent(this, typeof(RegisterFaceActivity)).PutExtra("update", true), REQUEST_CODE_REGISTER_FACE);
                });
            builder.Create().Show();
            builder.Dispose();
        }
    }
}