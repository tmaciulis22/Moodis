using Android.Content;
using Android.Views;
using Android.Views.InputMethods;

namespace Moodis.Extensions
{
    public static class ContextExtensions
    {
        public static void HideKeyboard(this Context context, View view)
        {
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
        }

        public static void ShowKeyboard(this Context context, View view)
        {
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.ShowSoftInput(view, ShowFlags.Implicit);
        }
    }
}