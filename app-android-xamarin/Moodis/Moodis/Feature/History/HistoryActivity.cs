﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Feature.SignIn;
using Moodis.Widget;
using System;

namespace Moodis.History
{
    [Activity(Label = "History")]
    public class HistoryActivity : AppCompatActivity
    {
        private HistoryViewModel historyViewModel = new HistoryViewModel();
        private RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetSupportActionBar();
            SetContentView(Resource.Layout.activity_history);
            InitView();
            InitAdapter();
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void InitView()
        {
            var dateInput = FindViewById<EditText>(Resource.Id.datePicker);
            dateInput.Click += (sender, e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    dateInput.Text = time.ToLongDateString();
                    (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchStats(SignInViewModel.currentUser.Id, time));
                });
                frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
            };
        }

        private void InitAdapter()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.statsList);

            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            var adapter = new HistoryStatsAdapter(historyViewModel.FetchStats(SignInViewModel.currentUser.Id, DateTime.Now));
            recyclerView.SetAdapter(adapter);
        }
    }
}