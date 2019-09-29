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
    }
}
