﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Moodis.Constants.Enums;
using Moodis.Extensions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moodis.Feature.Register
{
    [Activity(Label = "Register")]
    public class RegisterActivity : Activity
    {
        RegisterViewModel registerViewModel = new RegisterViewModel();
        private const int REQUEST_CODE_REGISTER_FACE = 1;

        View progressBar;
        Button registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
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

        private void InitButtonsAndInputs()
        {
            var usernameInput = FindViewById<EditText>(Resource.Id.usernameInputToAdd);
            var passwordInput = FindViewById<EditText>(Resource.Id.passwordInputToAdd);
            progressBar = FindViewById(Resource.Id.progressBarRegister);
            registerButton = FindViewById<Button>(Resource.Id.registerButtonToAdd);

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

            usernameInput.KeyPress += (sender, e) =>
            {
                if (e.KeyCode == Android.Views.Keycode.Enter && e.Event.Action == Android.Views.KeyEventActions.Down)
                {
                    e.Handled = true;
                    if (!string.IsNullOrEmpty((sender as EditText).Text))
                    {
                        this.HideKeyboard(usernameInput);
                        passwordInput.RequestFocus();
                        this.ShowKeyboard(passwordInput);
                    }
                    else
                    {
                        usernameInput.SetError(GetString(Resource.String.username_empty_error), null);
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
                    if (string.IsNullOrEmpty((sender as EditText).Text))
                    {
                        passwordInput.SetError(GetString(Resource.String.password_empty_error), null);
                    }
                    else
                    {
                        this.HideKeyboard(passwordInput);
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
                    progressBar.Visibility = ViewStates.Visible;
                    progressBar.BringToFront();
                    registerButton.Enabled = false;

                    var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,15}$");
                    if (regex.IsMatch(passwordInput.Text))
                    {
                        var response = await registerViewModel.AddUser(usernameInput.Text, passwordInput.Text);

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
            };
        }

        private async Task DeleteUser()
        {
            progressBar.Visibility = ViewStates.Visible;
            progressBar.BringToFront();
            registerButton.Enabled = false;

            var response = await registerViewModel.DeleteUser();
            while (response != Response.OK)
            {
                response = await registerViewModel.DeleteUser();
            }

            progressBar.Visibility = ViewStates.Gone;
            registerButton.Enabled = true;
        }
    }
}