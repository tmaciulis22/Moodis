using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Moodis.Feature.Login;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Moodis.Feature.Group
{
    class Group
    {
        private const string SEPARATOR = "__,__";
        [PrimaryKey]
        public string Id { get; set; }

        [Ignore]
        public List<string> members { get; set; }
        public string membersInString { get; set; }

        public string Groupname { get; set; }

        public Group()
        {
            Id = Guid.NewGuid().ToString();
            members = new List<string>();
        }

        public Group(string name, string firstUser) : this()
        {
            this.Groupname = name;
            members.Add(firstUser);
        }

        public void AddMember(string user)
        {
            members.Add(user);
        }

        public void RemoveMember(string user)
        {
            members.Remove(user);
        }

        public bool isMember(string username)
        {
            return members.Contains(username) ? true : false;
        }

        public void ConvertToString()
        {
            StringBuffer stringBuffer = new StringBuffer();
            members.ForEach(entry => stringBuffer.Append(entry).Append(SEPARATOR));
            stringBuffer.SetLength(stringBuffer.Length() - SEPARATOR.Length);
            membersInString = stringBuffer.ToString();
        }

        public void ConvertToList()
        {
            members = membersInString.Split(SEPARATOR).ToList();
        }
    }
}