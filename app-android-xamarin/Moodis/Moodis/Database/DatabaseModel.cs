using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //databaseConnection.DeleteAll<Emotion>():
            //databaseConnection.DropTable<User>();
            databaseConnection.DropTable<ImageInfo>();
            databaseConnection.DropTable<Emotion>();
            databaseConnection.CreateTable<User>();
            databaseConnection.CreateTable<ImageInfo>();
            databaseConnection.CreateTable<Emotion>();
        }

        public static List<User> FetchUsers()
        {
            return databaseConnection.Table<User>().ToList();
        }

        public static List<ImageInfo> FetchUserStats(string userId, DateTime? dateTime = null)
        {
            var stats = databaseConnection.Table<ImageInfo>();

            stats = stats.Where(stat => stat.UserId == userId);

            if (dateTime != null)
            {
                stats = stats.Where(stat => stat.ImageDate == dateTime);
            }

            var emotions = databaseConnection.Table<Emotion>().ToList();

            var statsToReturn = stats.ToList().Select(stat => {
                stat.emotions = new List<Emotion>();
                stat.emotions.AddRange(emotions.Where(emotion => emotion.ImageId == stat.Id));
                return stat;
            }).ToList();

            return statsToReturn;
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
            imageInfo.emotions.ForEach(emotion => databaseConnection.Insert(emotion));
        }

        public static void CloseConnectionToDatabase()
        {
            databaseConnection.Dispose();
        }
    }
}