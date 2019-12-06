using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Moodis.Extensions;
using Moodis.Feature.Group;
using Moodis.Feature.Register;
using Moodis.Feature.SignIn;
using Moodis.Widget;
using System;
using System.Collections.Generic;

namespace Moodis.History
{
    [Activity(Label = "History")]
    public class HistoryActivity : AppCompatActivity
    {
        private readonly HistoryViewModel historyViewModel = new HistoryViewModel();
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
                    var ids = new List<string>();
                    ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
                    (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchItemList(ids, time));
                });
                frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
            };
        }

        private void InitAdapter()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.statsList);

            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            var ids = new List<string>();
            ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
            var adapter = new HistoryStatsAdapter(historyViewModel.FetchItemList(ids, DateTime.Now));
            recyclerView.SetAdapter(adapter);
        }
    }
}