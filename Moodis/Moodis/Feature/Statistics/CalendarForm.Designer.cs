namespace Moodis.Feature.Statistics
{
    partial class CalendarForm
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
            this.lblDataNumber = new System.Windows.Forms.Label();
            this.listOfData = new System.Windows.Forms.ListBox();
            this.customCalendar = new Moodis.Feature.Statistics.CustomMonthCalendar();
            this.SuspendLayout();
            // 
            // lblDataNumber
            // 
            this.lblDataNumber.AutoSize = true;
            this.lblDataNumber.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataNumber.Location = new System.Drawing.Point(9, 230);
            this.lblDataNumber.Name = "lblDataNumber";
            this.lblDataNumber.Size = new System.Drawing.Size(141, 17);
            this.lblDataNumber.TabIndex = 1;
            this.lblDataNumber.Text = "Available picture data: ";
            // 
            // listOfData
            // 
            this.listOfData.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.listOfData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listOfData.FormattingEnabled = true;
            this.listOfData.ItemHeight = 20;
            this.listOfData.Location = new System.Drawing.Point(12, 250);
            this.listOfData.Name = "listOfData";
            this.listOfData.Size = new System.Drawing.Size(286, 184);
            this.listOfData.TabIndex = 2;
            // 
            // customCalendar
            // 
            this.customCalendar.Font = new System.Drawing.Font("Microsoft Tai Le", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customCalendar.Location = new System.Drawing.Point(310, -7);
            this.customCalendar.Name = "customCalendar";
            this.customCalendar.ShowToday = false;
            this.customCalendar.TabIndex = 0;
            this.customCalendar.TitleBackColor = System.Drawing.SystemColors.Highlight;
            this.customCalendar.TrailingForeColor = System.Drawing.SystemColors.ButtonShadow;
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 448);
            this.Controls.Add(this.listOfData);
            this.Controls.Add(this.lblDataNumber);
            this.Controls.Add(this.customCalendar);
            this.Name = "CalendarForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClose);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomMonthCalendar customCalendar;
        private System.Windows.Forms.Label lblDataNumber;
        private System.Windows.Forms.ListBox listOfData;
    }
}