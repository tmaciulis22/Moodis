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
            this.lblAnger = new System.Windows.Forms.Label();
            this.lblContempt = new System.Windows.Forms.Label();
            this.lblDisgust = new System.Windows.Forms.Label();
            this.lblFear = new System.Windows.Forms.Label();
            this.lblHappiness = new System.Windows.Forms.Label();
            this.lblNeutral = new System.Windows.Forms.Label();
            this.lblSadness = new System.Windows.Forms.Label();
            this.lblSurprise = new System.Windows.Forms.Label();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.btnToCamera = new System.Windows.Forms.Button();
            this.buttonMusicController = new System.Windows.Forms.Button();
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
            // lblAnger
            // 
            this.lblAnger.AutoSize = true;
            this.lblAnger.Location = new System.Drawing.Point(15, 320);
            this.lblAnger.Name = "lblAnger";
            this.lblAnger.Size = new System.Drawing.Size(44, 13);
            this.lblAnger.TabIndex = 2;
            this.lblAnger.Text = "loading.";
            // 
            // lblContempt
            // 
            this.lblContempt.AutoSize = true;
            this.lblContempt.Location = new System.Drawing.Point(15, 349);
            this.lblContempt.Name = "lblContempt";
            this.lblContempt.Size = new System.Drawing.Size(44, 13);
            this.lblContempt.TabIndex = 3;
            this.lblContempt.Text = "loading.";
            // 
            // lblDisgust
            // 
            this.lblDisgust.AutoSize = true;
            this.lblDisgust.Location = new System.Drawing.Point(15, 376);
            this.lblDisgust.Name = "lblDisgust";
            this.lblDisgust.Size = new System.Drawing.Size(44, 13);
            this.lblDisgust.TabIndex = 4;
            this.lblDisgust.Text = "loading.";
            // 
            // lblFear
            // 
            this.lblFear.AutoSize = true;
            this.lblFear.Location = new System.Drawing.Point(15, 406);
            this.lblFear.Name = "lblFear";
            this.lblFear.Size = new System.Drawing.Size(44, 13);
            this.lblFear.TabIndex = 5;
            this.lblFear.Text = "loading.";
            // 
            // lblHappiness
            // 
            this.lblHappiness.AutoSize = true;
            this.lblHappiness.Location = new System.Drawing.Point(151, 320);
            this.lblHappiness.Name = "lblHappiness";
            this.lblHappiness.Size = new System.Drawing.Size(44, 13);
            this.lblHappiness.TabIndex = 6;
            this.lblHappiness.Text = "loading.";
            // 
            // lblNeutral
            // 
            this.lblNeutral.AutoSize = true;
            this.lblNeutral.Location = new System.Drawing.Point(151, 349);
            this.lblNeutral.Name = "lblNeutral";
            this.lblNeutral.Size = new System.Drawing.Size(44, 13);
            this.lblNeutral.TabIndex = 7;
            this.lblNeutral.Text = "loading.";
            // 
            // lblSadness
            // 
            this.lblSadness.AutoSize = true;
            this.lblSadness.Location = new System.Drawing.Point(151, 376);
            this.lblSadness.Name = "lblSadness";
            this.lblSadness.Size = new System.Drawing.Size(44, 13);
            this.lblSadness.TabIndex = 8;
            this.lblSadness.Text = "loading.";
            // 
            // lblSurprise
            // 
            this.lblSurprise.AutoSize = true;
            this.lblSurprise.Location = new System.Drawing.Point(151, 406);
            this.lblSurprise.Name = "lblSurprise";
            this.lblSurprise.Size = new System.Drawing.Size(44, 13);
            this.lblSurprise.TabIndex = 9;
            this.lblSurprise.Text = "loading.";
            // 
            // btnCalendar
            // 
            this.btnCalendar.Location = new System.Drawing.Point(366, 28);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(153, 41);
            this.btnCalendar.TabIndex = 10;
            this.btnCalendar.Text = "Go to Calendar";
            this.btnCalendar.UseVisualStyleBackColor = true;
            this.btnCalendar.Click += new System.EventHandler(this.BtnCalendar_Click);
            // 
            // btnToCamera
            // 
            this.btnToCamera.Location = new System.Drawing.Point(366, 378);
            this.btnToCamera.Name = "btnToCamera";
            this.btnToCamera.Size = new System.Drawing.Size(153, 41);
            this.btnToCamera.TabIndex = 12;
            this.btnToCamera.Text = "Take another picture";
            this.btnToCamera.UseVisualStyleBackColor = true;
            this.btnToCamera.Click += new System.EventHandler(this.BtnToCamera_Click);
            // 
            // buttonMusicController
            // 
            this.buttonMusicController.Location = new System.Drawing.Point(366, 88);
            this.buttonMusicController.Name = "buttonMusicController";
            this.buttonMusicController.Size = new System.Drawing.Size(153, 39);
            this.buttonMusicController.TabIndex = 11;
            this.buttonMusicController.Text = "Play suggested music";
            this.buttonMusicController.UseVisualStyleBackColor = true;
            this.buttonMusicController.Click += new System.EventHandler(this.ButtonMusicController_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(531, 442);
            this.Controls.Add(this.btnToCamera);
            this.Controls.Add(this.buttonMusicController);
            this.Controls.Add(this.btnCalendar);
            this.Controls.Add(this.lblSurprise);
            this.Controls.Add(this.lblSadness);
            this.Controls.Add(this.lblNeutral);
            this.Controls.Add(this.lblHappiness);
            this.Controls.Add(this.lblFear);
            this.Controls.Add(this.lblDisgust);
            this.Controls.Add(this.lblContempt);
            this.Controls.Add(this.lblAnger);
            this.Controls.Add(this.lblCurrentPicture);
            this.Controls.Add(this.imgTakenPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuFormClose);
            ((System.ComponentModel.ISupportInitialize)(this.imgTakenPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgTakenPicture;
        private System.Windows.Forms.Label lblCurrentPicture;
        private System.Windows.Forms.Label lblAnger;
        private System.Windows.Forms.Label lblContempt;
        private System.Windows.Forms.Label lblDisgust;
        private System.Windows.Forms.Label lblFear;
        private System.Windows.Forms.Label lblHappiness;
        private System.Windows.Forms.Label lblNeutral;
        private System.Windows.Forms.Label lblSadness;
        private System.Windows.Forms.Label lblSurprise;
        private System.Windows.Forms.Button btnCalendar;
        private System.Windows.Forms.Button btnToCamera;
        private System.Windows.Forms.Button buttonMusicController;
    }
}