using moodis;
using Moodis.Constants.Enums;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Moodis.Feature.Login.Register
{
    public partial class RegisterForm : Form
    {
        RegisterViewModel registerViewModel;
        private Form parentForm;
        public const string passwordsNotSame = "Passwords must be the same!";
        public const string created = " was created!";
        public const string exists = " already exists!";
        public const string strongerPassword = "Password must be stronger!";
        public const string usernameEmpty = "Username field is empty!";
        public const string passwordEmpty = "Password field is empty!";
        public const string GeneralErrorMessage = "Something wrong happened, please try again later";


        public RegisterForm(Form parent)
        {
            registerViewModel = new RegisterViewModel();
            parentForm = parent;
            InitializeComponent();
        }

        private async void ButtonRegister_Click(object sender, EventArgs e)
        {
            labelNotification.ForeColor = Color.Red;

            if(string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                labelNotification.Text = usernameEmpty;
            }
            else if(string.IsNullOrWhiteSpace(textBoxPassword.Text) || string.IsNullOrWhiteSpace(textBoxPasswordRepeat.Text))
            {
                labelNotification.Text = passwordEmpty;
            }
            else
            {
                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,15}$");
                if(regex.Match(textBoxPassword.Text).Success)
                {
                    if(!textBoxPassword.Text.Equals(textBoxPasswordRepeat.Text))
                    {
                        labelNotification.Text = passwordsNotSame;
                    }
                    else
                    {
                        var response = await registerViewModel.AddUser(textBoxUsername.Text, textBoxPassword.Text);

                        if (response == Response.OK)
                        {
                            labelNotification.ForeColor = Color.Green;
                            labelNotification.Text = textBoxUsername.Text + created;

                            new CameraForm(true, registerViewModel).Show();
                            Close();
                        }
                        else if(response == Response.UserExists)
                        {
                            labelNotification.Text = textBoxUsername.Text + exists;
                        }
                        else
                        {
                            labelNotification.Text = GeneralErrorMessage;
                        }
                    }
                }
                else
                {
                    labelNotification.Text = strongerPassword;
                }
            }
        }

        private void BtnToSignIn_Click(object sender, EventArgs e)
        {
            parentForm.Location = Location;
            parentForm.Show();
            Close();
        }
    }
}
