using System;
using System.Windows.Forms;
using Moodis.Feature.Login;

namespace moodis
{
    static class Program
    {
        private const string WarningInRequest = "Connection failed. Is your internet turned on ?";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (CheckForInternet())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SignInForm());
            }
            else
            {
                MessageBox.Show(WarningInRequest);
                return;
            }
        }

        private static bool CheckForInternet()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    using (client.OpenRead("https://google.com"))
                        return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
