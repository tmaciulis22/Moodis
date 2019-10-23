using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

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