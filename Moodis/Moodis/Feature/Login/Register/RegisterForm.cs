using moodis;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Moodis.Feature.Login.Register
{
    public partial class RegisterForm : Form
    {
        RegisterViewModel registerViewModel = new RegisterViewModel();
        public const String passwordsNotSame = "Passwords must be the same!";
        public const String created = " was created!";
        public const String exists = " alreadyExists!";
        public const String strongerPassword = "Password must be stronger!";
        public const String usernameEmpty = "Username field is empty!";
        public const String passwordEmpty = "Password field is empty!";
        public RegisterForm()
        {
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
                        if (await registerViewModel.AddUser(textBoxUsername.Text, textBoxPassword.Text))
                        {
                            labelNotification.ForeColor = Color.Green;
                            labelNotification.Text = textBoxUsername.Text + created;
                        }
                        else
                        {
                            labelNotification.Text = textBoxUsername.Text + exists;
                        }
                    }
                }
                else
                {
                    labelNotification.Text = strongerPassword;
                }
            }

            new CameraForm(true, registerViewModel).Show();
            Close();
        }
    }
}
