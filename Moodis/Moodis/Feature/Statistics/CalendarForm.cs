using System.Windows.Forms;
using System.Collections.Generic;
using Moodis.Ui;
using Moodis.Feature.Login;

namespace Moodis.Feature.Statistics
{
    public partial class CalendarForm : Form
    {
        private CalendarViewModel calendarViewModel;
        private Form parentForm;
        private const string FormatDouble = "N3";
        private string username;

        public CalendarForm(CalendarViewModel viewModel, Form form, string username)
        {
            InitializeComponent();
            calendarViewModel = viewModel;
            parentForm = form;
            this.username = username;
            updateView();
        }

        private void SelectDate(object sender, DateRangeEventArgs e)
        {
            updateView();
        }

        private void FormClose(object sender, FormClosedEventArgs e)
        {
            parentForm.Location = Location;
            parentForm.Show();
            Dispose();
        }

        private void updateView()
        {
            listOfData.Items.Clear();
            calendarViewModel.updateViewModel();

            int counter = 0, counterM = 0, counterD = 0;
            foreach (ImageInfo data in SignInViewModel.getUser(username).imageStats)
            {
                if (data.imageDate.Date == customCalendar.SelectionRange.Start.Date)
                {
                    listOfData.Items.Add(SignInViewModel.getUser(username).imageStats[counter]);
                    counter++;
                }
                if (data.imageDate.Month == customCalendar.SelectionRange.Start.Month)
                {
                    calendarViewModel.monthlyList.Add(SignInViewModel.getUser(username).imageStats[counterM]);
                    counterM++;
                }
                if (data.imageDate.Day == customCalendar.SelectionRange.Start.Day)
                {
                    calendarViewModel.dailyList.Add(SignInViewModel.getUser(username).imageStats[counterD]);
                    counterD++;
                }
            }

            lblDataNumber.Text = "Available picture data: " + listOfData.Items.Count;
            lblDay.Text = "Daily highest emotion: " + calendarViewModel.getAverageEmotionStats(calendarViewModel.dailyList);
            lblMonthly.Text = "Monthly highest emotion: " + calendarViewModel.getAverageEmotionStats(calendarViewModel.monthlyList);
        }

        private void SelectItem(object sender, System.EventArgs e)
        {
            UpdateLabels();
        }
        public void UpdateLabels()
        {
            ImageInfo img = listOfData.SelectedItem as ImageInfo;
            var emotionLabels = new List<Label> { lblAnger, lblContempt, lblDisgust, lblFear, lblHappiness, lblNeutral, lblSadness, lblSurprise };
            var counter = 0;
            foreach (var label in emotionLabels)
            {
               label.Text = img.emotions[counter].name + " : "
                    + img.emotions[counter].confidence.ToString(FormatDouble);
               counter++;
            }
        }

        private void BtnToParentForm_Click(object sender, System.EventArgs e)
        {
            parentForm.Location = Location;
            parentForm.Show();
            Dispose();
        }
    }
}

