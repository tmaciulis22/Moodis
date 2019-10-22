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
            this.lblAnger = new System.Windows.Forms.Label();
            this.lblContempt = new System.Windows.Forms.Label();
            this.lblDisgust = new System.Windows.Forms.Label();
            this.lblFear = new System.Windows.Forms.Label();
            this.lblHappiness = new System.Windows.Forms.Label();
            this.lblNeutral = new System.Windows.Forms.Label();
            this.lblSadness = new System.Windows.Forms.Label();
            this.lblSurprise = new System.Windows.Forms.Label();
            this.lblInfoText = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.lblMonthly = new System.Windows.Forms.Label();
            this.btnToParentForm = new System.Windows.Forms.Button();
            this.customCalendar = new Moodis.Feature.Statistics.CustomMonthCalendar();
            this.SuspendLayout();
            // 
            // lblDataNumber
            // 
            this.lblDataNumber.AutoSize = true;
            this.lblDataNumber.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataNumber.Location = new System.Drawing.Point(12, 230);
            this.lblDataNumber.Name = "lblDataNumber";
            this.lblDataNumber.Size = new System.Drawing.Size(141, 16);
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
            this.listOfData.SelectedIndexChanged += new System.EventHandler(this.SelectItem);
            // 
            // lblAnger
            // 
            this.lblAnger.AutoSize = true;
            this.lblAnger.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnger.Location = new System.Drawing.Point(17, 40);
            this.lblAnger.Name = "lblAnger";
            this.lblAnger.Size = new System.Drawing.Size(49, 14);
            this.lblAnger.TabIndex = 3;
            this.lblAnger.Text = "waiting.";
            // 
            // lblContempt
            // 
            this.lblContempt.AutoSize = true;
            this.lblContempt.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContempt.Location = new System.Drawing.Point(17, 65);
            this.lblContempt.Name = "lblContempt";
            this.lblContempt.Size = new System.Drawing.Size(49, 14);
            this.lblContempt.TabIndex = 4;
            this.lblContempt.Text = "waiting.";
            // 
            // lblDisgust
            // 
            this.lblDisgust.AutoSize = true;
            this.lblDisgust.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisgust.Location = new System.Drawing.Point(17, 88);
            this.lblDisgust.Name = "lblDisgust";
            this.lblDisgust.Size = new System.Drawing.Size(49, 14);
            this.lblDisgust.TabIndex = 5;
            this.lblDisgust.Text = "waiting.";
            // 
            // lblFear
            // 
            this.lblFear.AutoSize = true;
            this.lblFear.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFear.Location = new System.Drawing.Point(17, 112);
            this.lblFear.Name = "lblFear";
            this.lblFear.Size = new System.Drawing.Size(49, 14);
            this.lblFear.TabIndex = 6;
            this.lblFear.Text = "waiting.";
            // 
            // lblHappiness
            // 
            this.lblHappiness.AutoSize = true;
            this.lblHappiness.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHappiness.Location = new System.Drawing.Point(118, 40);
            this.lblHappiness.Name = "lblHappiness";
            this.lblHappiness.Size = new System.Drawing.Size(49, 14);
            this.lblHappiness.TabIndex = 7;
            this.lblHappiness.Text = "waiting.";
            // 
            // lblNeutral
            // 
            this.lblNeutral.AutoSize = true;
            this.lblNeutral.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNeutral.Location = new System.Drawing.Point(118, 65);
            this.lblNeutral.Name = "lblNeutral";
            this.lblNeutral.Size = new System.Drawing.Size(49, 14);
            this.lblNeutral.TabIndex = 8;
            this.lblNeutral.Text = "waiting.";
            // 
            // lblSadness
            // 
            this.lblSadness.AutoSize = true;
            this.lblSadness.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSadness.Location = new System.Drawing.Point(118, 88);
            this.lblSadness.Name = "lblSadness";
            this.lblSadness.Size = new System.Drawing.Size(49, 14);
            this.lblSadness.TabIndex = 9;
            this.lblSadness.Text = "waiting.";
            // 
            // lblSurprise
            // 
            this.lblSurprise.AutoSize = true;
            this.lblSurprise.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurprise.Location = new System.Drawing.Point(118, 112);
            this.lblSurprise.Name = "lblSurprise";
            this.lblSurprise.Size = new System.Drawing.Size(49, 14);
            this.lblSurprise.TabIndex = 10;
            this.lblSurprise.Text = "waiting.";
            // 
            // lblInfoText
            // 
            this.lblInfoText.AutoSize = true;
            this.lblInfoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoText.Location = new System.Drawing.Point(15, 13);
            this.lblInfoText.Name = "lblInfoText";
            this.lblInfoText.Size = new System.Drawing.Size(152, 16);
            this.lblInfoText.TabIndex = 11;
            this.lblInfoText.Text = "Selected data statistics: ";
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDay.Location = new System.Drawing.Point(12, 200);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(43, 16);
            this.lblDay.TabIndex = 12;
            this.lblDay.Text = "label1";
            // 
            // lblMonthly
            // 
            this.lblMonthly.AutoSize = true;
            this.lblMonthly.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthly.Location = new System.Drawing.Point(12, 172);
            this.lblMonthly.Name = "lblMonthly";
            this.lblMonthly.Size = new System.Drawing.Size(43, 16);
            this.lblMonthly.TabIndex = 13;
            this.lblMonthly.Text = "label1";
            // 
            // btnToParentForm
            // 
            this.btnToParentForm.Location = new System.Drawing.Point(723, 393);
            this.btnToParentForm.Name = "btnToParentForm";
            this.btnToParentForm.Size = new System.Drawing.Size(121, 43);
            this.btnToParentForm.TabIndex = 15;
            this.btnToParentForm.Text = "Back to menu";
            this.btnToParentForm.UseVisualStyleBackColor = true;
            this.btnToParentForm.Click += new System.EventHandler(this.BtnToParentForm_Click);
            // 
            // customCalendar
            // 
            this.customCalendar.Font = new System.Drawing.Font("Microsoft Tai Le", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customCalendar.Location = new System.Drawing.Point(316, -26);
            this.customCalendar.Name = "customCalendar";
            this.customCalendar.ShowToday = false;
            this.customCalendar.TabIndex = 0;
            this.customCalendar.TitleBackColor = System.Drawing.SystemColors.Highlight;
            this.customCalendar.TrailingForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.customCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.SelectDate);
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 448);
            this.Controls.Add(this.btnToParentForm);
            this.Controls.Add(this.lblMonthly);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblInfoText);
            this.Controls.Add(this.lblSurprise);
            this.Controls.Add(this.lblSadness);
            this.Controls.Add(this.lblNeutral);
            this.Controls.Add(this.lblHappiness);
            this.Controls.Add(this.lblFear);
            this.Controls.Add(this.lblDisgust);
            this.Controls.Add(this.lblContempt);
            this.Controls.Add(this.lblAnger);
            this.Controls.Add(this.listOfData);
            this.Controls.Add(this.lblDataNumber);
            this.Controls.Add(this.customCalendar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CalendarForm";
            this.Text = "User calendar";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClose);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomMonthCalendar customCalendar;
        private System.Windows.Forms.Label lblDataNumber;
        private System.Windows.Forms.ListBox listOfData;
        private System.Windows.Forms.Label lblAnger;
        private System.Windows.Forms.Label lblContempt;
        private System.Windows.Forms.Label lblDisgust;
        private System.Windows.Forms.Label lblFear;
        private System.Windows.Forms.Label lblHappiness;
        private System.Windows.Forms.Label lblNeutral;
        private System.Windows.Forms.Label lblSadness;
        private System.Windows.Forms.Label lblSurprise;
        private System.Windows.Forms.Label lblInfoText;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblMonthly;
        private System.Windows.Forms.Button btnToParentForm;
    }
}