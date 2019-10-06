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

        public CalendarForm(CalendarViewModel viewModel, Form form)
        {
            InitializeComponent();
            calendarViewModel = viewModel;
            parentForm = form;
            updateView();
        }

        private void SelectDate(object sender, DateRangeEventArgs e)
        {
            updateView();
        }

        private void FormClose(object sender, FormClosedEventArgs e)
        {
            parentForm.Show();
            Dispose();
        }

        private void updateView()
        {
            listOfData.Items.Clear();
            calendarViewModel.updateViewModel();

            int counter = 0, counterM = 0, counterD = 0;
            foreach (ImageInfo data in LoginViewModel.currentUser.imageStats)
            {
                if (data.imageDate.Date == customCalendar.SelectionRange.Start.Date)
                {
                    listOfData.Items.Add(LoginViewModel.currentUser.imageStats[counter]);
                    counter++;
                }
                if(data.imageDate.Month == customCalendar.SelectionRange.Start.Month)
                {
                    calendarViewModel.monthlyList.Add(LoginViewModel.currentUser.imageStats[counterM]);
                    counterM++;
                }
                if (data.imageDate.Day == customCalendar.SelectionRange.Start.Day)
                {
                    calendarViewModel.dailyList.Add(LoginViewModel.currentUser.imageStats[counterD]);
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
            int counter = 0;
            foreach (var label in emotionLabels)
            {
               label.Text = img.emotions[counter].name + " : "
                    + img.emotions[counter].confidence.ToString(FormatDouble);
               counter++;
            }
        }
    }
}

