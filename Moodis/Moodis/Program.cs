using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moodis.Feature.Login;

using Moodis.Feature.Statistics;

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

            if(checkForInternet())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
            else
            {
                //Application.Exit();
                MessageBox.Show(WarningInRequest);
                return;
            }
            
            
        }

        private static bool checkForInternet()
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
