﻿using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;
using Moodis.Ui;

namespace moodis
{
    public partial class CameraForm : Form
    {
        public CameraForm()
        {
            InitializeComponent();
        }

        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        private const string WarningMessage = "You must first turn on the camera!";
        private const string NoDeviceMessage = "Your device does not have a camera.";
        private MenuForm menuWindow;
        private MenuViewModel menuViewModel;
        private void CameraFormLoad(object sender, EventArgs e)
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if(webcam.Count == 0)
            {
                MessageBox.Show(NoDeviceMessage);
                this.Close();
                return;
            }

            foreach(FilterInfo VideoCaptureDevice in webcam)
            {
                cmbOutputDevices.Items.Add(VideoCaptureDevice.Name);
            }
            cmbOutputDevices.SelectedIndex = 0;

            cam = new VideoCaptureDevice(webcam[cmbOutputDevices.SelectedIndex].MonikerString);

            foreach (var option in cam.VideoCapabilities)
            {
                string temp = option.FrameSize.Width.ToString() + " * " + option.FrameSize.Height.ToString();
                cmbCamResoliution.Items.Add(temp);
            }

            cmbCamResoliution.SelectedIndex = cmbCamResoliution.Items.Count-1;
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

            menuViewModel = new MenuViewModel();
            menuViewModel.currentImage.ImagePath = fileName;
            try
            {
                    picBox.Image.Save(saveFileDialog.FileName);
                    if (menuWindow == null || menuWindow.running == false)
                    {
                        menuWindow = new MenuForm(menuViewModel);
                        menuWindow.Show();
                    }
                    else
                    {
                        menuWindow.UpdateLabels();
                    }
            }
            catch
            {
                MessageBox.Show(WarningMessage);
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

        private void int indexOfHighestResoliution(var cam)
        {
            return 0;
        }
    }
}
