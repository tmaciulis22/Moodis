namespace moodis
{
    partial class CameraForm
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelOutputDevice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(32, 466);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 28);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // buttonTakePicture
            // 
            this.buttonTakePicture.Location = new System.Drawing.Point(203, 466);
            this.buttonTakePicture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonTakePicture.Name = "buttonTakePicture";
            this.buttonTakePicture.Size = new System.Drawing.Size(100, 28);
            this.buttonTakePicture.TabIndex = 1;
            this.buttonTakePicture.Text = "Picture!";
            this.buttonTakePicture.UseVisualStyleBackColor = true;
            this.buttonTakePicture.Click += new System.EventHandler(this.ButtonPicture);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(384, 466);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 28);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(32, 13);
            this.picBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(452, 404);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 3;
            this.picBox.TabStop = false;
            // 
            // labelOutputDevice
            // 
            this.labelOutputDevice.AutoSize = true;
            this.labelOutputDevice.Location = new System.Drawing.Point(36, 428);
            this.labelOutputDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOutputDevice.Name = "labelOutputDevice";
            this.labelOutputDevice.Size = new System.Drawing.Size(96, 17);
            this.labelOutputDevice.TabIndex = 5;
            this.labelOutputDevice.Text = "Output device";
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 510);
            this.Controls.Add(this.labelOutputDevice);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonTakePicture);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CameraForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Moodis";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CameraForm_FormClosed);
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
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelOutputDevice;
    }
}

