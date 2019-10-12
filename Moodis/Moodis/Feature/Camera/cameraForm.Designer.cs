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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tipLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(29, 488);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 28);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // buttonTakePicture
            // 
            this.buttonTakePicture.Location = new System.Drawing.Point(203, 488);
            this.buttonTakePicture.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTakePicture.Name = "buttonTakePicture";
            this.buttonTakePicture.Size = new System.Drawing.Size(100, 28);
            this.buttonTakePicture.TabIndex = 1;
            this.buttonTakePicture.Text = "Picture!";
            this.buttonTakePicture.UseVisualStyleBackColor = true;
            this.buttonTakePicture.Click += new System.EventHandler(this.ButtonPicture);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(381, 488);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 28);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(29, 76);
            this.picBox.Margin = new System.Windows.Forms.Padding(4);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(452, 404);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 3;
            this.picBox.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(29, 46);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(452, 23);
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // tipLabel
            // 
            this.tipLabel.AutoSize = true;
            this.tipLabel.Location = new System.Drawing.Point(93, 9);
            this.tipLabel.Name = "tipLabel";
            this.tipLabel.Size = new System.Drawing.Size(331, 17);
            this.tipLabel.TabIndex = 5;
            this.tipLabel.Text = "Please take 3 pictures of your face for identification";
            this.tipLabel.Visible = false;
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 532);
            this.Controls.Add(this.tipLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonTakePicture);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label tipLabel;
    }
}

