namespace Moodis.Feature.Login.Register
{
    partial class RegistrationCameraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationCameraForm));
            this.picBox = new System.Windows.Forms.PictureBox();
            this.buttonTakePicture = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tipLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(27, 77);
            this.picBox.Margin = new System.Windows.Forms.Padding(4);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(452, 323);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 4;
            this.picBox.TabStop = false;
            // 
            // buttonTakePicture
            // 
            this.buttonTakePicture.Location = new System.Drawing.Point(193, 407);
            this.buttonTakePicture.Name = "buttonTakePicture";
            this.buttonTakePicture.Size = new System.Drawing.Size(97, 45);
            this.buttonTakePicture.TabIndex = 5;
            this.buttonTakePicture.Text = "Take Picture";
            this.buttonTakePicture.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 47);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(452, 23);
            this.progressBar.TabIndex = 6;
            // 
            // tipLabel
            // 
            this.tipLabel.AutoSize = true;
            this.tipLabel.Location = new System.Drawing.Point(87, 9);
            this.tipLabel.Name = "tipLabel";
            this.tipLabel.Size = new System.Drawing.Size(324, 17);
            this.tipLabel.TabIndex = 7;
            this.tipLabel.Text = "Please take 3 photos of your face for identification";
            // 
            // RegistrationCameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 464);
            this.Controls.Add(this.tipLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonTakePicture);
            this.Controls.Add(this.picBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RegistrationCameraForm";
            this.Text = "Registration";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button buttonTakePicture;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label tipLabel;
    }
}