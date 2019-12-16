using Moodis.Feature.Group;
using Moodis.Feature.Login;
using Moodis.Ui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Moodis.Ui.ImageInfo;

namespace Moodis.Database
{
    class DatabaseModel
    {
        private static readonly SQLite.SQLiteConnection databaseConnection;
        static DatabaseModel()
        {
            string filename = "users_db.sqlite";
            string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            databaseConnection = new SQLite.SQLiteConnection(Path.Combine(fileLocation, filename));
            databaseConnection.CreateTable<User>();
            databaseConnection.CreateTable<ImageInfo>();
            databaseConnection.CreateTable<Emotion>();
            databaseConnection.CreateTable<Group>();
        }

        public static List<User> FetchUsers()
        {
            return databaseConnection.Table<User>().ToList();
        }

        public static List<ImageInfo> FetchUserStats(List<string> userIds, DateTime? dateTime = null)
        {
            var stats = databaseConnection.Table<ImageInfo>().ToList();

            stats = stats.Where(stat => userIds.Contains(stat.UserId)).ToList();

            stats.ForEach(stat => stat.ImageDate = DateTime.Parse(stat.DateAsString));

            if (dateTime != null)
            {
                stats = stats.Where(stat => stat.ImageDate.Date == dateTime.Value.Date).ToList();
            }

            var emotions = databaseConnection.Table<Emotion>().ToList();

            stats.ForEach(stat =>
            {
                stat.emotions = new List<Emotion>();
                stat.emotions.AddRange(emotions.Where(emotion => emotion.ImageId == stat.Id));
            });

            return stats;
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
            imageInfo.DateAsString = imageInfo.ImageDate.ToString();
            databaseConnection.Insert(imageInfo);
            imageInfo.emotions.ForEach(emotion => databaseConnection.Insert(emotion));
        }

        public static List<Group> FetchGroupFromDatabase()
        {
            var groups = databaseConnection.Table<Group>().ToList();
            groups.ForEach(entry => entry.ConvertToList());
            return groups;
        }

        public static void AddGroupToDatabase(Group group)
        {
            group.ConvertToString();
            databaseConnection.Insert(group);
        }

        public static void UpdateGroupToDatabase(Group group)
        {
            group.ConvertToString();
            databaseConnection.Update(group);
        }

        public static void DeleteGroupFromDatabase(Group group)
        {
            databaseConnection.Delete(group);
        }

        public static void CloseConnectionToDatabase()
        {
            databaseConnection.Dispose();
        }
    }
}