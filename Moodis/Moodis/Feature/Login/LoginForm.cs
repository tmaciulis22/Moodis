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
                errorLabel.Text = "Username field is empty!";
            }else if (string.IsNullOrWhiteSpace(passwordField.Text))
            {
                errorLabel.Text = "Password field is empty!";
            }
            else
            {
                if (loginViewModel.Authenticate(usernameField.Text, passwordField.Text) != null)
                {
                    CameraForm cameraWindow = new CameraForm();
                    cameraWindow.Show();
                    this.Hide();
                }
                else
                {
                    errorLabel.Text = "User not found!";
                }
                
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            errorRegisterLabel.ForeColor = Color.Red;
            if (string.IsNullOrWhiteSpace(usernameRegisterField.Text))
            {
                errorRegisterLabel.Text = "Username field is empty!";
            }
            else if (string.IsNullOrWhiteSpace(passwordRegisterField.Text))
            {
                errorRegisterLabel.Text = "Password field is empty!";
            }
            else
            {
                Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,15}$");
                if (regex.Match(passwordRegisterField.Text).Success)
                {
                    if (passwordRegisterField.Text != passwordRepRegisterField.Text)
                    {
                        errorRegisterLabel.Text = "Passwords must be the same!";
                    }
                    else
                    {
                        if (loginViewModel.AddUser(usernameRegisterField.Text,passwordRegisterField.Text))
                        {
                            errorRegisterLabel.ForeColor = Color.Green;
                            errorRegisterLabel.Text = usernameRegisterField.Text + " was created!";
                        }
                        else
                        {
                            errorRegisterLabel.Text = usernameRegisterField.Text + " already exists!";
                        }

                    } 
                }
                else
                {
                    errorRegisterLabel.Text = "Password must be stronger!";
                }

            }
        }
    }
}
