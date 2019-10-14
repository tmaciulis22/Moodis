namespace Moodis.Feature.Group
{
    partial class GroupForm
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
            this.groupList = new System.Windows.Forms.ListBox();
            this.groupName = new System.Windows.Forms.TextBox();
            this.createGroup = new System.Windows.Forms.Button();
            this.groupJoinButton = new System.Windows.Forms.Button();
            this.groupUsersList = new System.Windows.Forms.ListBox();
            this.leaveGroupButton = new System.Windows.Forms.Button();
            this.usersInGroupLabel = new System.Windows.Forms.Label();
            this.groupsLabel = new System.Windows.Forms.Label();
            this.yourGroupLabel = new System.Windows.Forms.Label();
            this.removeGroupButton = new System.Windows.Forms.Button();
            this.seeCalendarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // groupList
            // 
            this.groupList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.groupList.ItemHeight = 25;
            this.groupList.Location = new System.Drawing.Point(12, 76);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(151, 354);
            this.groupList.TabIndex = 0;
            // 
            // groupName
            // 
            this.groupName.Location = new System.Drawing.Point(175, 105);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(224, 22);
            this.groupName.TabIndex = 1;
            // 
            // createGroup
            // 
            this.createGroup.Location = new System.Drawing.Point(175, 135);
            this.createGroup.Name = "createGroup";
            this.createGroup.Size = new System.Drawing.Size(224, 26);
            this.createGroup.TabIndex = 2;
            this.createGroup.Text = "Create Group";
            this.createGroup.UseVisualStyleBackColor = true;
            this.createGroup.Click += new System.EventHandler(this.CreateGroup_Click);
            // 
            // groupJoinButton
            // 
            this.groupJoinButton.Location = new System.Drawing.Point(175, 76);
            this.groupJoinButton.Name = "groupJoinButton";
            this.groupJoinButton.Size = new System.Drawing.Size(224, 23);
            this.groupJoinButton.TabIndex = 4;
            this.groupJoinButton.Text = "Join Group";
            this.groupJoinButton.UseVisualStyleBackColor = true;
            this.groupJoinButton.Click += new System.EventHandler(this.GroupJoinButton_Click);
            // 
            // groupUsersList
            // 
            this.groupUsersList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.groupUsersList.FormattingEnabled = true;
            this.groupUsersList.ItemHeight = 25;
            this.groupUsersList.Location = new System.Drawing.Point(12, 76);
            this.groupUsersList.Name = "groupUsersList";
            this.groupUsersList.Size = new System.Drawing.Size(151, 354);
            this.groupUsersList.TabIndex = 5;
            // 
            // leaveGroupButton
            // 
            this.leaveGroupButton.Location = new System.Drawing.Point(175, 76);
            this.leaveGroupButton.Name = "leaveGroupButton";
            this.leaveGroupButton.Size = new System.Drawing.Size(224, 23);
            this.leaveGroupButton.TabIndex = 6;
            this.leaveGroupButton.Text = "Leave Group";
            this.leaveGroupButton.UseVisualStyleBackColor = true;
            this.leaveGroupButton.Click += new System.EventHandler(this.LeaveGroupButton_Click);
            // 
            // usersInGroupLabel
            // 
            this.usersInGroupLabel.AutoSize = true;
            this.usersInGroupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.usersInGroupLabel.Location = new System.Drawing.Point(12, 39);
            this.usersInGroupLabel.Name = "usersInGroupLabel";
            this.usersInGroupLabel.Size = new System.Drawing.Size(138, 25);
            this.usersInGroupLabel.TabIndex = 7;
            this.usersInGroupLabel.Text = "Users in group";
            // 
            // groupsLabel
            // 
            this.groupsLabel.AutoSize = true;
            this.groupsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.groupsLabel.Location = new System.Drawing.Point(12, 39);
            this.groupsLabel.Name = "groupsLabel";
            this.groupsLabel.Size = new System.Drawing.Size(76, 25);
            this.groupsLabel.TabIndex = 8;
            this.groupsLabel.Text = "Groups";
            // 
            // yourGroupLabel
            // 
            this.yourGroupLabel.AutoSize = true;
            this.yourGroupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.yourGroupLabel.Location = new System.Drawing.Point(175, 39);
            this.yourGroupLabel.Name = "yourGroupLabel";
            this.yourGroupLabel.Size = new System.Drawing.Size(114, 25);
            this.yourGroupLabel.TabIndex = 9;
            this.yourGroupLabel.Text = "Your group:";
            // 
            // removeGroupButton
            // 
            this.removeGroupButton.Location = new System.Drawing.Point(175, 76);
            this.removeGroupButton.Name = "removeGroupButton";
            this.removeGroupButton.Size = new System.Drawing.Size(224, 26);
            this.removeGroupButton.TabIndex = 10;
            this.removeGroupButton.Text = "Remove Group";
            this.removeGroupButton.UseVisualStyleBackColor = true;
            this.removeGroupButton.Click += new System.EventHandler(this.RemoveGroupButton_Click);
            // 
            // seeCalendarButton
            // 
            this.seeCalendarButton.Location = new System.Drawing.Point(180, 392);
            this.seeCalendarButton.Name = "seeCalendarButton";
            this.seeCalendarButton.Size = new System.Drawing.Size(219, 38);
            this.seeCalendarButton.TabIndex = 11;
            this.seeCalendarButton.Text = "See Calendar";
            this.seeCalendarButton.UseVisualStyleBackColor = true;
            this.seeCalendarButton.Click += new System.EventHandler(this.SeeCalendarButton_Click);
            // 
            // GroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 450);
            this.Controls.Add(this.seeCalendarButton);
            this.Controls.Add(this.removeGroupButton);
            this.Controls.Add(this.yourGroupLabel);
            this.Controls.Add(this.groupsLabel);
            this.Controls.Add(this.usersInGroupLabel);
            this.Controls.Add(this.leaveGroupButton);
            this.Controls.Add(this.groupUsersList);
            this.Controls.Add(this.groupJoinButton);
            this.Controls.Add(this.createGroup);
            this.Controls.Add(this.groupName);
            this.Controls.Add(this.groupList);
            this.Name = "GroupForm";
            this.Text = "Groups";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox groupList;
        private System.Windows.Forms.TextBox groupName;
        private System.Windows.Forms.Button createGroup;
        private System.Windows.Forms.Button groupJoinButton;
        private System.Windows.Forms.ListBox groupUsersList;
        private System.Windows.Forms.Button leaveGroupButton;
        private System.Windows.Forms.Label usersInGroupLabel;
        private System.Windows.Forms.Label groupsLabel;
        private System.Windows.Forms.Label yourGroupLabel;
        private System.Windows.Forms.Button removeGroupButton;
        private System.Windows.Forms.Button seeCalendarButton;
    }
}