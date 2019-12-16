using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace Moodis.Widget
{
    public class DatePickerFragment : Android.Support.V4.App.DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
    {
        public static readonly string TAG = "DatePicker:" + typeof(DatePickerFragment).Name.ToUpper();

        Action<DateTime> _dateSelectedHandler = delegate { };

        private static DateTime SelectedDate = DateTime.Now;

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment
            {
                _dateSelectedHandler = onDateSelected
            };
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(Activity, this, SelectedDate.Year, SelectedDate.Month - 1, SelectedDate.Day);
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            SelectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            _dateSelectedHandler(SelectedDate);
        }
    }
}