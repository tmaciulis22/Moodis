using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace moodie
{
    public partial class cameraForm : Form
    {
        public cameraForm()
        {
            InitializeComponent();
        }

        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        private const string WarningMsg = "You must first turn on the camera!";

        private void CameraFormLoad(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo VideoCaptureDevice in webcam)
            {
                cmbOutputDevices.Items.Add(VideoCaptureDevice.Name);
            }
            cmbOutputDevices.SelectedIndex = 0;

            cam = new VideoCaptureDevice(webcam[cmbOutputDevices.SelectedIndex].MonikerString);

            foreach (VideoCapabilities option in cam.VideoCapabilities)
            {
                string temp = option.FrameSize.Width.ToString() + " * " + option.FrameSize.Height.ToString();
                cmbCamResoliution.Items.Add(temp);
            }

            cmbCamResoliution.SelectedIndex = 0;
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
            saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog1.FileName = "Image.jpeg";

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    picBox.Image.Save(saveFileDialog1.FileName);

                }
            }

            catch
            {
                MessageBox.Show(WarningMsg);
            }            
        }

        private void ComboBoxResoliution(object sender, EventArgs e)
        {
            if(cam.IsRunning)
            {
                cam.Stop();
                cam.VideoResolution = cam.VideoCapabilities[cmbCamResoliution.SelectedIndex];
                cam.Start();
            }
            
            else
            {
                cam.VideoResolution = cam.VideoCapabilities[cmbCamResoliution.SelectedIndex];
            }
        }
    }
}
