using Android.Support.V7.App;
using System;

namespace Moodis.Extensions
{
    public static class AppCompatActivityExtensions
    {
        public static void SetSupportActionBar(this AppCompatActivity activity, int? titleRes = null)
        {
            if (titleRes != null)
            {
                activity.SupportActionBar.SetTitle(titleRes.Value);
            }
            activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            activity.SupportActionBar.SetDisplayShowHomeEnabled(true);
        }

        public static AppCompatDialog ConfirmationAlert(
            this AppCompatActivity activity, 
            int titleRes, 
            int messageRes,
            int positiveButtonRes,
            int negativeButtonRes, 
            Action positiveCallback = null,
            Action negativeCallback = null
        ) {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);

            builder.SetTitle(titleRes);
            builder.SetMessage(messageRes);

            builder.SetPositiveButton(activity.GetString(positiveButtonRes), (senderAler, args) => {
                positiveCallback?.Invoke();
            });
            builder.SetNegativeButton(activity.GetString(negativeButtonRes), (senderAler, args) => {
                negativeCallback?.Invoke();
            });

            return builder.Create();
        }
    }
}