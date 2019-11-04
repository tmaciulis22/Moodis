using System;
using System.Collections.Generic;
using System.IO;
using Moodis.Feature.Login;
using Moodis.Ui;
using static Moodis.Ui.ImageInfo;

namespace Moodis.Database
{
    class DatabaseModel
    {
        private static SQLite.SQLiteConnection databaseConnection;
        static DatabaseModel()
        {
            string filename = "users_db.sqlite";
            string fileLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            databaseConnection = new SQLite.SQLiteConnection(Path.Combine(fileLocation, filename));
            //Use this for testing compatability with new users
            //databaseConnection.DeleteAll<User>();
            //databaseConnection.DeleteAll<ImageInfo>();
            databaseConnection.CreateTable<User>();
            databaseConnection.CreateTable<ImageInfo>();
        }

        public static List<User> FetchUsers()
        {
            return databaseConnection.Table<User>().ToList();
        }

        public static List<ImageInfo> FetchUserStats(int userId, DateTime? dateTime = null)
        {
            var table = databaseConnection.Table<ImageInfo>();

            table = table.Where(stat => stat.UserId == userId);
            if (dateTime != null)
            {
                table = table.Where(stat => stat.imageDate == dateTime);
            }

            return table.ToList();
        }

        public static void AddUserToDatabase(User user)
        {
            databaseConnection.Insert(user);
        }

        public static void DeleteUserFromDatabase(User user)
        {
            databaseConnection.Delete(user);
        }

        public static void AddImageInfoToDatabase(ImageInfo imageInfo)
        {
            databaseConnection.Insert(imageInfo);
        }

        public static void CloseConnectionToDatabase()
        {
            databaseConnection.Dispose();
        }
    }
}