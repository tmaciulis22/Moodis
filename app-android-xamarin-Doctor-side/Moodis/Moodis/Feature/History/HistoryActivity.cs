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

        private const string EXTRA_REASON = "EXTRA_REASON";
        private const string EXTRA_NAME = "EXTRA_NAME";

        public static int extraReason; // 0 your own, 1 - group, 2 - person;
        public static string extraName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetSupportActionBar();
            SetContentView(Resource.Layout.activity_history);

            extraName = Intent.GetStringExtra(EXTRA_NAME);
            extraReason = Intent.GetIntExtra(EXTRA_REASON, 0);

            InitView();
            InitAdapter();

            //AnimationExtension.AnimateBackground(FindViewById(Resource.Id.constraintLayoutHistory));
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
                    switch (extraReason)
                    {
                        case 0:
                            ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
                            (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchItemList(ids, time));
                            break;
                        case 1:
                            ids = GroupActivityModel.GetGroupUserIds(extraName);
                            (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchItemList(ids, time));
                            break;
                        case 2:
                            ids.Add(RegisterViewModel.GetIdByUsername(extraName));
                            (recyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(historyViewModel.FetchItemList(ids, time));
                            break;
                    default:
                            ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
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

            switch (extraReason)
            {
                case 0:
                    ids.Add(RegisterViewModel.GetIdByUsername(SignInViewModel.currentUser.Username));
                    adapter = new HistoryStatsAdapter(historyViewModel.FetchItemList(ids, DateTime.Now));
                    break;
                case 1:
                    ids = GroupActivityModel.GetGroupUserIds(extraName);
                    adapter = new HistoryStatsAdapter(historyViewModel.FetchItemList(ids, DateTime.Now));
                    break;
                case 2:
                    ids.Add(RegisterViewModel.GetIdByUsername(extraName));
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