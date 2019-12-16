namespace Moodis.Feature.Login
{
    public class User
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDoctor { get; set; }
        public string PersonGroupId { get; set; }
        public string PersonId { get; set; }

        public User()
        {
            IsDoctor = false;
        }

        public User(string username, string password) : this()
        {
            Username = username;
            Password = password;
        }
    }
}
