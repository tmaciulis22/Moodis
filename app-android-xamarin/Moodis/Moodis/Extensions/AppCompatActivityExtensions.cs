using Android.Support.V7.App;

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
    }
}