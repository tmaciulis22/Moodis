using System.Windows.Forms;
using System.Collections.Generic;
using Moodis.Ui;

namespace Moodis.Feature.Statistics
{
    public partial class CalendarForm : Form
    {
        private CalendarViewModel calendarViewModel;
        private Form parentForm;
        public List<string> dates = new List<string>();

        public CalendarForm(CalendarViewModel viewModel, Form form)
        {
            InitializeComponent();
            calendarViewModel = viewModel;
            parentForm = form;
            updateView();
        }

        private void SelectDate(object sender, DateRangeEventArgs e)
        {
            MessageBox.Show(customCalendar.SelectionRange.Start.ToShortDateString() + " selected date");
            updateView();
        }

        private void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            parentForm.Show();
        }

        private void updateView()
        {
            lblDataNumber.Text = "Available picture data: " + Login.LoginViewModel.currentUser.imageStats.Count;
            if (listOfData.Items.Count > 0)
            {
                listOfData.Items.Add(Login.LoginViewModel.currentUser.imageStats[0]);
                listOfData.Items.Add(Login.LoginViewModel.currentUser.imageStats[1]);
            }
            else
            {
                string test = "no pictures found";
                listOfData.Items.Add(test);
            }
        }
    }
}

