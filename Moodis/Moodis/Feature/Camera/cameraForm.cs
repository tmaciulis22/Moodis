using AForge.Video;
using AForge.Video.DirectShow;
using Moodis.Constants.Enums;
using Moodis.Feature.Camera;
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
        private const string RegistrationSuccessful = "Registration was successful, now you may login!";
        private const int ProgressBarValueFactor = 33;
        private const int RequiredNumberOfPhotos = 3;

        private MenuForm menuWindow;

        private MenuViewModel menuViewModel;
        private CameraViewModel cameraViewModel;
        private RegisterViewModel registerViewModel;

        private bool isRegistering = false;

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

        private async void ButtonPicture(object sender, EventArgs e)
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

            if (isRegistering == false)
            {
                if (menuWindow == null)
                {
                    menuViewModel = new MenuViewModel();
                    menuViewModel.currentImage.ImagePath = fileName;
                    menuWindow = new MenuForm(menuViewModel, this);
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
            else
            {
                var response = await registerViewModel.AddFaceToPerson(fileName);

                if (response == Response.OK)
                {
                    progressBar.Value = ProgressBarValueFactor * registerViewModel.photosTaken;

                    if (registerViewModel.photosTaken == RequiredNumberOfPhotos)
                    {
                        MessageBox.Show(RegistrationSuccessful);
                        Hide();
                    }
                }
                else if(response == Response.ApiTrainingError)
                {
                    MessageBox.Show(ApiErrorMessage);
                    Close();
                }
                else
                {
                    MessageBox.Show(WarningFaceDetection);
                }
            }
        }

        private void CameraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                cam.Stop();
                Application.Exit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
