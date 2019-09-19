namespace Moodis.Ui
{
    partial class MenuForm
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
            this.imgTakenPicture = new System.Windows.Forms.PictureBox();
            this.lblCurrentPicture = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgTakenPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // imgTakenPicture
            // 
            this.imgTakenPicture.Location = new System.Drawing.Point(12, 28);
            this.imgTakenPicture.Name = "imgTakenPicture";
            this.imgTakenPicture.Size = new System.Drawing.Size(330, 285);
            this.imgTakenPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgTakenPicture.TabIndex = 0;
            this.imgTakenPicture.TabStop = false;
            // 
            // lblCurrentPicture
            // 
            this.lblCurrentPicture.AutoSize = true;
            this.lblCurrentPicture.Location = new System.Drawing.Point(12, 12);
            this.lblCurrentPicture.Name = "lblCurrentPicture";
            this.lblCurrentPicture.Size = new System.Drawing.Size(80, 13);
            this.lblCurrentPicture.TabIndex = 1;
            this.lblCurrentPicture.Text = "Current Picture:";
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblCurrentPicture);
            this.Controls.Add(this.imgTakenPicture);
            this.Name = "MenuForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imgTakenPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgTakenPicture;
        private System.Windows.Forms.Label lblCurrentPicture;
    }
}