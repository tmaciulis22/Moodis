using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.Constraints;
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
        public static int EXTRA_REASON = 0; // 0 your own, 1 - group, 2 - person;
        public static string EXTRA_NAME = "EXTRA_NAME";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetSupportActionBar();
            SetContentView(Resource.Layout.activity_history);
            EXTRA_NAME = Intent.GetStringExtra("name");
            EXTRA_REASON = Intent.GetIntExtra("reason",0);
            InitView();
            InitAdapter();

            AnimationExtension.AnimateBackground(FindViewById(Resource.Id.constraintLayoutHistory));
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
                    switch (EXTRA_REASON)
                    {
                        case 0:
                            ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
                            (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchItemList(ids, time));
                            break;
                        case 1:
                            ids = GroupActivityModel.GetGroupUserIds(EXTRA_NAME);
                            (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchItemList(ids, time));
                            break;
                        case 2:
                            ids.Add(RegisterViewModel.GetIdByUsername(EXTRA_NAME));
                            (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchItemList(ids, time));
                            break;
                    }
                });
                frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
            };
        }

        private void InitAdapter()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.statsList);

            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            HistoryStatsAdapter adapter;
            var ids = new List<string>();

            switch (EXTRA_REASON)
            {
                case 0:
                    ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
                    adapter = new HistoryStatsAdapter(historyViewModel.FetchItemList(ids, DateTime.Now));
                    break;
                case 1:
                    ids = GroupActivityModel.GetGroupUserIds(EXTRA_NAME);
                    adapter = new HistoryStatsAdapter(historyViewModel.FetchItemList(ids, DateTime.Now));
                    break;
                case 2:
                    ids.Add(RegisterViewModel.GetIdByUsername(EXTRA_NAME));
                    adapter = new HistoryStatsAdapter(historyViewModel.FetchItemList(ids, DateTime.Now));
                    break;
                default:
                    ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
                    adapter = new HistoryStatsAdapter(historyViewModel.FetchItemList(ids, DateTime.Now));
                    break;
            }
            recyclerView.SetAdapter(adapter);
        }
    }
}