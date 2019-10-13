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
        SignInViewModel signInViewModel;
        public const String usernameEmpty = "Username field is empty!";
        public const String passwordEmpty = "Password field is empty!";
        public const String userNotFound = "User not found!";
        public LoginForm()
        {
            InitializeComponent();
            signInViewModel = new SignInViewModel();
        }

        private void ButtonSignIn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxUsername.Text))
            {
                labelNotification.Text = usernameEmpty;
            }
            else if(string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                labelNotification.Text = passwordEmpty;
            }
            else
            {
                if(signInViewModel.Authenticate(textBoxUsername.Text,textBoxPassword.Text) != null)
                {
                    var cameraWindow = new CameraForm();
                    cameraWindow.StartPosition = FormStartPosition.Manual;
                    cameraWindow.Location = Location;
                    cameraWindow.Show();
                    Hide();
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
            var cameraForm = new CameraForm();
            cameraForm.StartPosition = FormStartPosition.Manual;
            cameraForm.Location = Location;
            var registerViewModel = new RegisterViewModel();
            registerViewModel.AddUser("49874f70-b7e4-4d58-9ce4-67aa55dbd281", "34d75fd8 - 3f14 - 4e56 - a878 - e397447bd40b");
            signInViewModel.Authenticate("49874f70-b7e4-4d58-9ce4-67aa55dbd281", "34d75fd8 - 3f14 - 4e56 - a878 - e397447bd40b");
            cameraForm.Show();
            Hide();
        }
    }
}
