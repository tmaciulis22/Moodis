using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using moodis;
using System.Text.RegularExpressions;

namespace Moodis.Feature.Login
{
    public partial class LoginForm : Form
    {

        public const String usernameEmpty = "Username field is empty!";
        public const String passwordEmpty = "Password field is empty!";
        public const String userNotFound = "User not found!";
        public const String passwordsNotSame = "Passwords must be the same!";
        public const String created = " was created!";
        public const String exists = " alreadyExists!";
        public const String strongerPassword = "Password must be stronger!";

        private readonly LoginViewModel loginViewModel;
        public LoginForm()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameField.Text))
            {
                errorLabel.Text = usernameEmpty;
            }else if (string.IsNullOrWhiteSpace(passwordField.Text))
            {
                errorLabel.Text = passwordEmpty;
            }
            else
            {
                if (loginViewModel.Authenticate(usernameField.Text, passwordField.Text) != null)
                {
                    var cameraWindow = new CameraForm();
                    cameraWindow.Show();
                    this.Hide();
                }
                else
                {
                    errorLabel.Text = userNotFound;
                }
                
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            errorRegisterLabel.ForeColor = Color.Red;
            if (string.IsNullOrWhiteSpace(usernameRegisterField.Text))
            {
                errorRegisterLabel.Text = usernameEmpty;
            }
            else if (string.IsNullOrWhiteSpace(passwordRegisterField.Text))
            {
                errorRegisterLabel.Text = passwordEmpty;
            }
            else
            {
                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,15}$");
                if (regex.Match(passwordRegisterField.Text).Success)
                {
                    if (passwordRegisterField.Text != passwordRepRegisterField.Text)
                    {
                        errorRegisterLabel.Text = passwordsNotSame;
                    }
                    else
                    {
                        if (loginViewModel.AddUser(usernameRegisterField.Text,passwordRegisterField.Text))
                        {
                            errorRegisterLabel.ForeColor = Color.Green;
                            errorRegisterLabel.Text = usernameRegisterField.Text + created;
                        }
                        else
                        {
                            errorRegisterLabel.Text = usernameRegisterField.Text + exists;
                        }

                    } 
                }
                else
                {
                    errorRegisterLabel.Text = strongerPassword;
                }

            }
        }

        private void LoginClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
