using System.Windows.Forms;
using System.Collections.Generic;

namespace Moodis.Feature.Statistics
{
    public partial class CalendarForm : Form
    {
        private CalendarViewModel calendarViewModel;
        public List<string> dates = new List<string>();

        public CalendarForm(CalendarViewModel viewModel)
        {
            InitializeComponent();
            calendarViewModel = viewModel;
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
        }

        private void updateView()
        {
            if (listOfData.Items.Count > 0)
            {

            }
            else
            {
                string test = "no pictures found";
                listOfData.Items.Add(test);
            }
        }
    }
}

