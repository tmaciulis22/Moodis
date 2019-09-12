using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace moodie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;

        private void Form1_Load(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo VideoCaptureDevice in webcam)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;

            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);

            foreach (VideoCapabilities option in cam.VideoCapabilities)
            {
                string temp = option.FrameSize.Width.ToString() + " * " + option.FrameSize.Height.ToString();
                comboBox2.Items.Add(temp);
            }

            comboBox2.SelectedIndex = 0;


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
                MessageBox.Show("You must first turn on the camera!");
            }

            
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cam.IsRunning)
            {
                cam.Stop();
                cam.VideoResolution = cam.VideoCapabilities[comboBox2.SelectedIndex];
                cam.Start();
            }
            
            else
            {
                cam.VideoResolution = cam.VideoCapabilities[comboBox2.SelectedIndex];
            }

            
        }
    }
}
