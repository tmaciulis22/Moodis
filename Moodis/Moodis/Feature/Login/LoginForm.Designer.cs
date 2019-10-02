namespace Moodis.Feature.Login
{
    partial class LoginForm
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
            this.signInLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.usernameField = new System.Windows.Forms.TextBox();
            this.passwordField = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.registerLabel = new System.Windows.Forms.Label();
            this.usernameRegisterField = new System.Windows.Forms.TextBox();
            this.passwordRegisterField = new System.Windows.Forms.TextBox();
            this.passwordRepRegisterField = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.usernameRegisterLabel = new System.Windows.Forms.Label();
            this.passwordRegisterLabel = new System.Windows.Forms.Label();
            this.passwordRepRegisterLabel = new System.Windows.Forms.Label();
            this.errorRegisterLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // signInLabel
            // 
            this.signInLabel.AutoSize = true;
            this.signInLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.signInLabel.Location = new System.Drawing.Point(91, 37);
            this.signInLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.signInLabel.Name = "signInLabel";
            this.signInLabel.Size = new System.Drawing.Size(109, 36);
            this.signInLabel.TabIndex = 0;
            this.signInLabel.Text = "Sign In";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(115, 83);
            this.errorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 13);
            this.errorLabel.TabIndex = 1;
            // 
            // usernameField
            // 
            this.usernameField.Location = new System.Drawing.Point(117, 106);
            this.usernameField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usernameField.Name = "usernameField";
            this.usernameField.Size = new System.Drawing.Size(114, 20);
            this.usernameField.TabIndex = 2;
            // 
            // passwordField
            // 
            this.passwordField.Location = new System.Drawing.Point(117, 145);
            this.passwordField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.passwordField.Name = "passwordField";
            this.passwordField.PasswordChar = '*';
            this.passwordField.Size = new System.Drawing.Size(114, 20);
            this.passwordField.TabIndex = 3;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(48, 110);
            this.usernameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(48, 147);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password:";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(117, 188);
            this.loginButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 24);
            this.loginButton.TabIndex = 6;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // registerLabel
            // 
            this.registerLabel.AutoSize = true;
            this.registerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.registerLabel.Location = new System.Drawing.Point(371, 37);
            this.registerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.registerLabel.Name = "registerLabel";
            this.registerLabel.Size = new System.Drawing.Size(126, 36);
            this.registerLabel.TabIndex = 7;
            this.registerLabel.Text = "Register";
            // 
            // usernameRegisterField
            // 
            this.usernameRegisterField.Location = new System.Drawing.Point(376, 106);
            this.usernameRegisterField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usernameRegisterField.Name = "usernameRegisterField";
            this.usernameRegisterField.Size = new System.Drawing.Size(114, 20);
            this.usernameRegisterField.TabIndex = 8;
            // 
            // passwordRegisterField
            // 
            this.passwordRegisterField.Location = new System.Drawing.Point(376, 141);
            this.passwordRegisterField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.passwordRegisterField.Name = "passwordRegisterField";
            this.passwordRegisterField.PasswordChar = '*';
            this.passwordRegisterField.Size = new System.Drawing.Size(114, 20);
            this.passwordRegisterField.TabIndex = 9;
            // 
            // passwordRepRegisterField
            // 
            this.passwordRepRegisterField.Location = new System.Drawing.Point(376, 176);
            this.passwordRepRegisterField.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.passwordRepRegisterField.Name = "passwordRepRegisterField";
            this.passwordRepRegisterField.PasswordChar = '*';
            this.passwordRepRegisterField.Size = new System.Drawing.Size(114, 20);
            this.passwordRepRegisterField.TabIndex = 10;
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(376, 215);
            this.registerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(75, 24);
            this.registerButton.TabIndex = 11;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // usernameRegisterLabel
            // 
            this.usernameRegisterLabel.AutoSize = true;
            this.usernameRegisterLabel.Location = new System.Drawing.Point(314, 109);
            this.usernameRegisterLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.usernameRegisterLabel.Name = "usernameRegisterLabel";
            this.usernameRegisterLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameRegisterLabel.TabIndex = 12;
            this.usernameRegisterLabel.Text = "Username:";
            // 
            // passwordRegisterLabel
            // 
            this.passwordRegisterLabel.AutoSize = true;
            this.passwordRegisterLabel.Location = new System.Drawing.Point(317, 143);
            this.passwordRegisterLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.passwordRegisterLabel.Name = "passwordRegisterLabel";
            this.passwordRegisterLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordRegisterLabel.TabIndex = 13;
            this.passwordRegisterLabel.Text = "Password:";
            // 
            // passwordRepRegisterLabel
            // 
            this.passwordRepRegisterLabel.AutoSize = true;
            this.passwordRepRegisterLabel.Location = new System.Drawing.Point(327, 179);
            this.passwordRepRegisterLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.passwordRepRegisterLabel.Name = "passwordRepRegisterLabel";
            this.passwordRepRegisterLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordRepRegisterLabel.TabIndex = 14;
            this.passwordRepRegisterLabel.Text = "Confirm:";
            // 
            // errorRegisterLabel
            // 
            this.errorRegisterLabel.AutoSize = true;
            this.errorRegisterLabel.ForeColor = System.Drawing.Color.Red;
            this.errorRegisterLabel.Location = new System.Drawing.Point(374, 83);
            this.errorRegisterLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.errorRegisterLabel.Name = "errorRegisterLabel";
            this.errorRegisterLabel.Size = new System.Drawing.Size(0, 13);
            this.errorRegisterLabel.TabIndex = 15;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.errorRegisterLabel);
            this.Controls.Add(this.passwordRepRegisterLabel);
            this.Controls.Add(this.passwordRegisterLabel);
            this.Controls.Add(this.usernameRegisterLabel);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.passwordRepRegisterField);
            this.Controls.Add(this.passwordRegisterField);
            this.Controls.Add(this.usernameRegisterField);
            this.Controls.Add(this.registerLabel);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordField);
            this.Controls.Add(this.usernameField);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.signInLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LoginForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginClose);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label signInLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.TextBox usernameField;
        private System.Windows.Forms.TextBox passwordField;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label registerLabel;
        private System.Windows.Forms.TextBox usernameRegisterField;
        private System.Windows.Forms.TextBox passwordRegisterField;
        private System.Windows.Forms.TextBox passwordRepRegisterField;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label usernameRegisterLabel;
        private System.Windows.Forms.Label passwordRegisterLabel;
        private System.Windows.Forms.Label passwordRepRegisterLabel;
        private System.Windows.Forms.Label errorRegisterLabel;
    }
}