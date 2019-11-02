using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Widget;

namespace Moodis.History
{
    [Activity(Label = "History")]
    public class HistoryActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetSupportActionBar();
            SetContentView(Resource.Layout.activity_history);
            AddClickListeners();
        }

        void AddClickListeners()
        {
            //var dateInput = FindViewById<EditText>(Resource.Id.datePicker);
            //dateInput.Click += (sender, e) =>
            //{
            //    DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            //    {
            //        dateInput.Text = time.ToLongDateString();
            //    });
            //    frag.Show(FragmentManager, DatePickerFragment.TAG);
            //};
        }
    }
}