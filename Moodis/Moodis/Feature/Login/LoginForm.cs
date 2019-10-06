using moodis;
using Moodis.Feature.Login.Register;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moodis.Feature.Login
{
    public partial class LoginForm : Form
    {
        LoginViewModel loginViewModel;
        public const String usernameEmpty = "Username field is empty!";
        public const String passwordEmpty = "Password field is empty!";
        public const String userNotFound = "User not found!";
        public LoginForm()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
            loginViewModel.AddUser("admin", "admin");
        }

        private void ButtonSignIn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxUsername.Text))
            {
                labelNotification.Text = usernameEmpty;
            }
            else if(string.IsNullOrEmpty(textBoxPassword.Text))
            {
                labelNotification.Text = passwordEmpty;
            }
            else
            {
                if(loginViewModel.Authenticate(textBoxUsername.Text,textBoxPassword.Text) != null)
                {
                    var cameraWindow = new CameraForm();
                    cameraWindow.Show();
                    this.Hide();
                }
                else
                {
                    labelNotification.Text = userNotFound;
                }
            }
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            var registerWindow = new RegisterForm();
            registerWindow.Show();
        }

        private void LabelSignIn_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm();
            cameraForm.Show();
            this.Hide();
        }
    }
}
