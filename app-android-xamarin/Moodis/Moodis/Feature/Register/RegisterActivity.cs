using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Moodis.Constants.Enums;
using Moodis.Extensions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moodis.Feature.Register
{
    [Activity(Label = "Register")]
    public class RegisterActivity : AppCompatActivity
    {
        readonly RegisterViewModel RegisterViewModel = new RegisterViewModel();
        private const int REQUEST_CODE_REGISTER_FACE = 1;

        View progressBar;
        Button registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            this.SetSupportActionBar();
            InitButtonsAndInputs();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            SetResult(Result.Canceled);
            Finish();
        }

        protected override async void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.FirstUser && requestCode == REQUEST_CODE_REGISTER_FACE)
            {
                SetResult(Result.FirstUser);
                Finish();
            }
            else if (resultCode == Result.Canceled && requestCode == REQUEST_CODE_REGISTER_FACE)
            {
                await DeleteUser();
                SetResult(Result.Canceled);
                Finish();
            }
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void InitButtonsAndInputs()
        {
            var usernameInput = FindViewById<EditText>(Resource.Id.usernameInputRegister);
            var passwordInput = FindViewById<EditText>(Resource.Id.passwordInputRegister);
            progressBar = FindViewById(Resource.Id.progressBarRegister);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

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

            passwordInput.KeyPress += async (sender, e) =>
            {
                if (e.KeyCode == Keycode.Enter && e.Event.Action == KeyEventActions.Down)
                {
                    e.Handled = true;
                    if (string.IsNullOrEmpty((sender as EditText).Text))
                    {
                        passwordInput.SetError(GetString(Resource.String.password_empty_error), null);
                    }
                    else
                    {
                        this.HideKeyboard(passwordInput);
                        await Register(usernameInput.Text, passwordInput.Text);
                    }
                }
                else
                {
                    e.Handled = false;
                }
            };

            registerButton.Click += async (sender, e) =>
            {
                if (string.IsNullOrEmpty(usernameInput.Text))
                {
                    usernameInput.SetError(GetString(Resource.String.username_empty_error), null);
                }
                else if (string.IsNullOrEmpty(passwordInput.Text))
                {
                    passwordInput.SetError(GetString(Resource.String.username_empty_error), null);
                }
                else
                {
                    await Register(usernameInput.Text, passwordInput.Text);
                }
            };
        }

        private async Task Register(string username, string password)
        {
            progressBar.Visibility = ViewStates.Visible;
            progressBar.BringToFront();
            registerButton.Enabled = false;

            var regex = new Regex(@"^(?=.*\d)(?=.*[A-Z])(.+)$");
            if (regex.IsMatch(password))
            {
                var response = await RegisterViewModel.AddUser(username, password);

                if (response == Response.OK)
                {
                    StartActivityForResult(new Intent(this, typeof(RegisterFaceActivity)), REQUEST_CODE_REGISTER_FACE);
                }
                else if (response == Response.UserExists)
                {
                    Toast.MakeText(this, Resource.String.user_exists, ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(this, Resource.String.stronger_password, ToastLength.Short).Show();
            }
            progressBar.Visibility = ViewStates.Gone;
            registerButton.Enabled = true;
        }

        private async Task DeleteUser()
        {
            progressBar.Visibility = ViewStates.Visible;
            progressBar.BringToFront();
            registerButton.Enabled = false;

            var response = await RegisterViewModel.DeleteUser();
            if (response != Response.OK)
            { 
                Log.Error(Class.Name, MethodBase.GetCurrentMethod().Name + ": " + response.ToString());
            }
            progressBar.Visibility = ViewStates.Gone;
            registerButton.Enabled = true;
        }
    }
}