namespace moodie
{
    partial class cameraForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonTakePicture = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.cmbOutputDevices = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelOutputDevice = new System.Windows.Forms.Label();
            this.cmbCamResoliution = new System.Windows.Forms.ComboBox();
            this.labelResoliution = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(24, 379);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // buttonTakePicture
            // 
            this.buttonTakePicture.Location = new System.Drawing.Point(152, 379);
            this.buttonTakePicture.Name = "buttonTakePicture";
            this.buttonTakePicture.Size = new System.Drawing.Size(75, 23);
            this.buttonTakePicture.TabIndex = 1;
            this.buttonTakePicture.Text = "Picture!";
            this.buttonTakePicture.UseVisualStyleBackColor = true;
            this.buttonTakePicture.Click += new System.EventHandler(this.ButtonPicture);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(288, 379);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(24, 38);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(339, 280);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 3;
            this.picBox.TabStop = false;
            // 
            // cmbOutputDevices
            // 
            this.cmbOutputDevices.FormattingEnabled = true;
            this.cmbOutputDevices.Location = new System.Drawing.Point(110, 324);
            this.cmbOutputDevices.Name = "cmbOutputDevices";
            this.cmbOutputDevices.Size = new System.Drawing.Size(253, 21);
            this.cmbOutputDevices.TabIndex = 4;
            // 
            // labelOutputDevice
            // 
            this.labelOutputDevice.AutoSize = true;
            this.labelOutputDevice.Location = new System.Drawing.Point(30, 327);
            this.labelOutputDevice.Name = "labelOutputDevice";
            this.labelOutputDevice.Size = new System.Drawing.Size(74, 13);
            this.labelOutputDevice.TabIndex = 5;
            this.labelOutputDevice.Text = "Output device";
            // 
            // cmbCamResoliution
            // 
            this.cmbCamResoliution.FormattingEnabled = true;
            this.cmbCamResoliution.Location = new System.Drawing.Point(110, 351);
            this.cmbCamResoliution.Name = "cmbCamResoliution";
            this.cmbCamResoliution.Size = new System.Drawing.Size(253, 21);
            this.cmbCamResoliution.TabIndex = 7;
            this.cmbCamResoliution.SelectedIndexChanged += new System.EventHandler(this.ComboBoxResoliution);
            // 
            // labelResoliution
            // 
            this.labelResoliution.AutoSize = true;
            this.labelResoliution.Location = new System.Drawing.Point(30, 354);
            this.labelResoliution.Name = "labelResoliution";
            this.labelResoliution.Size = new System.Drawing.Size(59, 13);
            this.labelResoliution.TabIndex = 6;
            this.labelResoliution.Text = "Resoliution";
            // 
            // cameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 414);
            this.Controls.Add(this.cmbCamResoliution);
            this.Controls.Add(this.labelResoliution);
            this.Controls.Add(this.labelOutputDevice);
            this.Controls.Add(this.cmbOutputDevices);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonTakePicture);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "cameraForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Moodis";
            this.Load += new System.EventHandler(this.CameraFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonTakePicture;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.ComboBox cmbOutputDevices;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelOutputDevice;
        private System.Windows.Forms.ComboBox cmbCamResoliution;
        private System.Windows.Forms.Label labelResoliution;
    }
}

