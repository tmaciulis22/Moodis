using System.Windows.Forms;
using System.Collections.Generic;
using Moodis.Ui;
using System.Linq;
using System;

namespace Moodis.Feature.Statistics
{
    public partial class CalendarForm : Form
    {
        private CalendarViewModel calendarViewModel;
        private Form parentForm;
        private const string FormatDouble = "N3";
        private List<ImageInfo> monthlyList = new List<ImageInfo>();
        private List<ImageInfo> dailyList = new List<ImageInfo>();

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
            int counter = 0, counterM = 0, counterD = 0;
            foreach (ImageInfo data in Login.LoginViewModel.currentUser.imageStats)
            {
                if (data.imageDate.Date == customCalendar.SelectionRange.Start.Date)
                {
                    listOfData.Items.Add(Login.LoginViewModel.currentUser.imageStats[counter]);
                    counter++;
                }
                if(data.imageDate.Month == customCalendar.SelectionRange.Start.Month)
                {
                    monthlyList.Add(Login.LoginViewModel.currentUser.imageStats[counterM]);
                    counterM++;
                }
                if (data.imageDate.Day == customCalendar.SelectionRange.Start.Day)
                {
                    dailyList.Add(Login.LoginViewModel.currentUser.imageStats[counterD]);
                    counterD++;
                }
            }
            lblDataNumber.Text = "Available picture data: " + listOfData.Items.Count;

            int size = dailyList[0].emotions.Length - 1;
            List<double> confidenceList = new List<double>(new double[size+1]);
            foreach (ImageInfo imageInfo in dailyList)
            {

                for (int i = 0; i < size; i++)
                {
                    confidenceList[i] = confidenceList[i] + imageInfo.emotions[i].confidence;
                }
            }
            int index = confidenceList.IndexOf(confidenceList.Max());
            lblDay.Text = "Daily highest emotion: " + dailyList[0].emotions[index].name + " avg: " + (confidenceList[index] / dailyList.Count).ToString(FormatDouble);

            confidenceList = new List<double>(new double[size + 1]);
            foreach (ImageInfo imageInfo in monthlyList)
            {

                for (int i = 0; i < size; i++)
                {
                    confidenceList[i] = confidenceList[i] + imageInfo.emotions[i].confidence;
                }
            }
            index = confidenceList.IndexOf(confidenceList.Max());
            lblMonthly.Text = "Monthly highest emotion: " + monthlyList[0].emotions[index].name + " avg: " + (confidenceList[index] / monthlyList.Count).ToString(FormatDouble);
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

