using System.Collections.Generic;
using System.Threading.Tasks;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Login;

namespace Moodis.Feature.Register
{
    class RegisterViewModel
    {
        public static List<User> userList = databaseModel.FetchData();
        public User currentUser;
        public async Task<Response> AddUser(string username, string password)
        {
            if(userList.Exists(userFromList => userFromList.username == username))
            {
                return Response.UserExists;
            }
            else
            {
                currentUser = new User(username, Crypto.CalculateMD5Hash(password));
                databaseModel.AddUserToDatabase(currentUser);
                UpdateLocalStorage();
                return Response.OK;
            }
        }
        public void UpdateLocalStorage()
        {
            userList = databaseModel.FetchData();
        }
    }
}