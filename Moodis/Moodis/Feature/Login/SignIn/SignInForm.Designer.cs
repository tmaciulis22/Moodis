namespace Moodis.Feature.Login
{
    partial class SignInForm
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
            this.labelSignIn = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.labelNotification = new System.Windows.Forms.Label();
            this.buttonSignInFace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSignIn
            // 
            this.labelSignIn.AutoSize = true;
            this.labelSignIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignIn.Location = new System.Drawing.Point(89, 54);
            this.labelSignIn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSignIn.Name = "labelSignIn";
            this.labelSignIn.Size = new System.Drawing.Size(284, 91);
            this.labelSignIn.TabIndex = 0;
            this.labelSignIn.Text = "Sign In";
            this.labelSignIn.Click += new System.EventHandler(this.LabelSignIn_Click);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(76, 218);
            this.labelUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(73, 17);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "Username";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(76, 273);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(69, 17);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Password";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(169, 218);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(228, 22);
            this.textBoxUsername.TabIndex = 3;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(169, 273);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(228, 22);
            this.textBoxPassword.TabIndex = 4;
            // 
            // buttonSignIn
            // 
            this.buttonSignIn.Location = new System.Drawing.Point(80, 343);
            this.buttonSignIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSignIn.Name = "buttonSignIn";
            this.buttonSignIn.Size = new System.Drawing.Size(319, 28);
            this.buttonSignIn.TabIndex = 5;
            this.buttonSignIn.Text = "Sign In";
            this.buttonSignIn.UseVisualStyleBackColor = true;
            this.buttonSignIn.Click += new System.EventHandler(this.ButtonSignIn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 426);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Do not have account?";
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(80, 456);
            this.buttonRegister.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(319, 28);
            this.buttonRegister.TabIndex = 7;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.ButtonRegister_Click);
            // 
            // labelNotification
            // 
            this.labelNotification.AutoSize = true;
            this.labelNotification.Location = new System.Drawing.Point(165, 302);
            this.labelNotification.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNotification.Name = "labelNotification";
            this.labelNotification.Size = new System.Drawing.Size(0, 17);
            this.labelNotification.TabIndex = 8;
            // 
            // buttonSignInFace
            // 
            this.buttonSignInFace.Location = new System.Drawing.Point(80, 378);
            this.buttonSignInFace.Name = "buttonSignInFace";
            this.buttonSignInFace.Size = new System.Drawing.Size(319, 28);
            this.buttonSignInFace.TabIndex = 9;
            this.buttonSignInFace.Text = "Sign In using your face";
            this.buttonSignInFace.UseVisualStyleBackColor = true;
            this.buttonSignInFace.Click += new System.EventHandler(this.buttonSignInFace_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 524);
            this.Controls.Add(this.buttonSignInFace);
            this.Controls.Add(this.labelNotification);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSignIn);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.labelSignIn);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Moodis: Sign In";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSignIn;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonSignIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Label labelNotification;
        private System.Windows.Forms.Button buttonSignInFace;
    }
}