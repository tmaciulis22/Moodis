using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Moodis.Extensions;
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
        private RecyclerView RecyclerView;
        private TextView StatusLabel;
        private View ProgressBar;

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
            StatusLabel = FindViewById<TextView>(Resource.Id.statusLabel);
            ProgressBar = FindViewById(Resource.Id.progressBarHistory);
            var dateInput = FindViewById<EditText>(Resource.Id.datePicker);

            dateInput.Click += (sender, e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(async delegate (DateTime time)
                {
                    dateInput.Text = time.ToLongDateString();

                    ProgressBar.Visibility = ViewStates.Visible;
                    ProgressBar.BringToFront();

                    var itemList = await historyViewModel.FetchItemList(SignInViewModel.currentUser.Id, time);

                    if (itemList.IsNullOrEmpty())
                    {
                        RecyclerView.Visibility = ViewStates.Gone;
                        StatusLabel.Visibility = ViewStates.Visible;

                        if (itemList == null)
                        {
                            StatusLabel.Text = GetString(Resource.String.api_error);
                        }
                        else
                        {
                            StatusLabel.Text = GetString(Resource.String.history_no_data);
                        }
                    }
                    else
                    {
                        RecyclerView.Visibility = ViewStates.Visible;
                        StatusLabel.Visibility = ViewStates.Gone;
                        (RecyclerView.GetAdapter() as HistoryStatsAdapter).UpdateList(itemList);
                    }

                    ProgressBar.Visibility = ViewStates.Gone;
                });
                frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
            };
        }

        private async void InitAdapter()
        {
            RecyclerView = FindViewById<RecyclerView>(Resource.Id.statsList);

            var layoutManager = new LinearLayoutManager(this);
            RecyclerView.SetLayoutManager(layoutManager);

            ProgressBar.Visibility = ViewStates.Visible;
            ProgressBar.BringToFront();

            var itemList = await historyViewModel.FetchItemList(SignInViewModel.currentUser.Id, DateTime.UtcNow);
            HistoryStatsAdapter adapter;

            if (itemList.IsNullOrEmpty())
            {
                adapter = new HistoryStatsAdapter(new List<object>());
                RecyclerView.Visibility = ViewStates.Gone;
                StatusLabel.Visibility = ViewStates.Visible;
                if (itemList == null)
                {
                    StatusLabel.Text = GetString(Resource.String.api_error);
                }
                else
                {
                    StatusLabel.Text = GetString(Resource.String.history_no_data);
                }
            }
            else
            {
                adapter = new HistoryStatsAdapter(itemList);
            }

            RecyclerView.SetAdapter(adapter);

            ProgressBar.Visibility = ViewStates.Gone;
        }
    }
}