using AForge.Video;
using AForge.Video.DirectShow;
using Moodis.Constants.Enums;
using Moodis.Feature.Camera;
using Moodis.Feature.Login;
using Moodis.Feature.Login.Register;
using Moodis.Ui;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace moodis
{
    public partial class CameraForm : Form
    {
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;

        private const string WarningMessage = "You must first turn on the camera!";
        private const string NoDeviceMessage = "Your device does not have a camera.";

        private const string WarningFaceDetection = "Face not detected, please try to use better lighting and stay in front of camera";
        private const string ApiErrorMessage = "Something wrong happened, please try again later";

        private const string RegistrationSuccessful = "Registration was successful, you may now login!";
        private const string SignInSuccessful = "You successfully signed in!";
        private const string UserNotFoundMessage = "User not found";


        private const int ProgressBarValueFactor = 33;
        private const int RequiredNumberOfPhotos = 3;

        private MenuForm menuWindow;

        private MenuViewModel menuViewModel;
        private CameraViewModel cameraViewModel;
        private RegisterViewModel registerViewModel;
        private SignInViewModel signInViewModel;

        private bool isRegistering = false;
        private bool isSignIn = false;

        public CameraForm()
        {
            cameraViewModel = new CameraViewModel();
            InitializeComponent();
        }

        public CameraForm(bool isRegistering, RegisterViewModel registerViewModel)
        {
            this.isRegistering = isRegistering;
            this.registerViewModel = registerViewModel;
            cameraViewModel = new CameraViewModel();
            
            InitializeComponent();

            tipLabel.Visible = true;
            progressBar.Visible = true;
        }

        public CameraForm(bool isSignIn, SignInViewModel signInViewModel)
        {
            this.isSignIn = isSignIn;
            this.signInViewModel = signInViewModel;
            cameraViewModel = new CameraViewModel();

            InitializeComponent();

            labelSignIn.Visible = true;
            buttonBack.Visible = true;
        }

        private void CameraFormLoad(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if(webcam.Count == 0)
            {
                MessageBox.Show(NoDeviceMessage);
                Application.Exit();
                return;
            }

            cam = new VideoCaptureDevice(webcam[0].MonikerString);
            cam.VideoResolution = cam.VideoCapabilities[cameraViewModel.getHighestResoliutionIndex(cam)];
            cam.NewFrame += new NewFrameEventHandler(cam_newFrame);
            cam.Start();
        }

        private void ButtonStartClick(object sender, EventArgs e)
        {            
            cam.NewFrame += new NewFrameEventHandler(cam_newFrame);
            cam.Start();
        }

        void cam_newFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            picBox.Image = bit;
        }

        private void ButtonStop(object sender, EventArgs e)
        {
            if(cam.IsRunning)
            {
                cam.Stop();
            }
        }

        private void ButtonPicture(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            var fileName = DateTime.Now.ToString().Replace("-","").Replace("/", "").Replace(":","").Replace("PM","").Replace(" ","") + ".jpeg";
            saveFileDialog.FileName = fileName;

            try
            {
                picBox.Image.Save(saveFileDialog.FileName);
            }
            catch
            {
                MessageBox.Show(WarningMessage);
            }

            if(isRegistering)
            {
                TakePictureForRegistration(fileName);
            }
            else if(isSignIn)
            {
                TakePictureForSignIn(fileName);
            }
            else
            {
                TakePictureForStatistics(fileName);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            new SignInForm().Show();
            Close();
        }

        private void CameraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                cam.Stop();
                if(isSignIn != true && isRegistering != true)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void TakePictureForRegistration(string fileName)
        {
            var response = await registerViewModel.AddFaceToPerson(fileName);

            if (response == Response.OK)
            {
                progressBar.Value = ProgressBarValueFactor * registerViewModel.photosTaken;

                if (registerViewModel.photosTaken == RequiredNumberOfPhotos)
                {
                    MessageBox.Show(RegistrationSuccessful);
                    new SignInForm().Show();
                    Close();
                }
            }
            else if (response == Response.ApiTrainingError)
            {
                MessageBox.Show(ApiErrorMessage);
                new SignInForm().Show();
                Close();
            }
            else
            {
                MessageBox.Show(WarningFaceDetection);
            }
        }

        private async void TakePictureForSignIn(string fileName)
        {
            var response = await signInViewModel.AuthenticateWithFace(fileName);

            if (response == Response.OK)
            {
                MessageBox.Show(SignInSuccessful);

                TakePictureForStatistics(fileName);

                Close();
            }
            else if (response == Response.UserNotFound)
            {
                MessageBox.Show(UserNotFoundMessage);
            }
            else
            {
                MessageBox.Show(WarningFaceDetection);
            }
        }

        private void TakePictureForStatistics(string fileName)
        {
            if (menuWindow == null)
            {
                menuViewModel = new MenuViewModel();
                menuViewModel.currentImage.ImagePath = fileName;
                if (isSignIn)
                {
                    menuWindow = new MenuForm(menuViewModel, new CameraForm());
                }
                else
                {
                    menuWindow = new MenuForm(menuViewModel, this);
                }
                menuWindow.StartPosition = FormStartPosition.Manual;
            }
            else
            {
                menuViewModel.currentImage.ImagePath = fileName;
                menuWindow.UpdateLabels();
            }
            menuWindow.Location = Location;
            menuWindow.Show();
            Hide();
        }
    }
}
