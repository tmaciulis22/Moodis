using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace moodie
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        private const string WarningMsg = "You must first turn on the camera!";

        private void Form1_Load(object sender, EventArgs e)
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

        private void Button1_Click(object sender, EventArgs e)
        {            
            cam.NewFrame += new NewFrameEventHandler(cam_newFrame);
            cam.Start();
        }

        void cam_newFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bit;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(cam.IsRunning)
            {
                cam.Stop();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog1.FileName = "Image.jpeg";

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);

                }
            }

            catch
            {
                MessageBox.Show(WarningMsg);
            }            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
