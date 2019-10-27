using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Moodis.Feature.Login;

namespace Moodis.Database
{
    class databaseModel
    {
        private static SQLite.SQLiteConnection databaseConnection;
        static databaseModel()
        {
            string filename = "users_db.sqlite";
            string fileLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            databaseConnection = new SQLite.SQLiteConnection(Path.Combine(fileLocation, filename));
            databaseConnection.CreateTable<User>();
        }

        public static List<User> FetchData()
        {
            return databaseConnection.Table<User>().ToList();
        }
        public static void AddUserToDatabase(User user)
        {
            databaseConnection.Insert(user);
        }

        public static void CloseConnectionToDatabase()
        {
            databaseConnection.Dispose();
        }
    }
}