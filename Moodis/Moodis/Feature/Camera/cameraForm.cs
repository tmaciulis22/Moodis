using AForge.Video;
using AForge.Video.DirectShow;
using Moodis.Feature.Camera;
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
        private MenuForm menuWindow;
        private MenuViewModel menuViewModel;
        private CameraViewModel cameraViewModel;

        public CameraForm()
        {
            cameraViewModel = new CameraViewModel();
            InitializeComponent();
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
                if (menuWindow == null)
                {
                    menuViewModel = new MenuViewModel();
                    menuViewModel.currentImage.ImagePath = fileName;
                    menuWindow = new MenuForm(menuViewModel);
                    menuWindow.Show();
                }
                else
                {
                    menuViewModel.currentImage.ImagePath = fileName;
                    menuWindow.UpdateLabels();
                    menuWindow.Show();
                }
            }
            catch
            {
                MessageBox.Show(WarningMessage);
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
