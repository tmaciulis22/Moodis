using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Feature.Login;
using Moodis.Feature.SignIn;
using Moodis.Network.Face;
using System;
using System.Threading.Tasks;

namespace Moodis.Feature.Register
{
    public class RegisterViewModel
    {

        public async Task<Response> AddUser(string username, string password)
        {
            if(SignInViewModel.userList.Exists(userFromList => userFromList.Username == username))
            {
                return Response.UserExists;
            }
            else
            {
                SignInViewModel.currentUser = new User(username, Crypto.CalculateMD5Hash(password))
                {
                    PersonGroupId = Guid.NewGuid().ToString()
                };
                DatabaseModel.AddUserToDatabase(SignInViewModel.currentUser);
                UpdateLocalStorage();

                await Face.Instance.CreateNewDoctor(SignInViewModel.currentUser.PersonGroupId, username);

                return Response.OK;
            }
        }

        public void UpdateLocalStorage()
        {
            SignInViewModel.userList = DatabaseModel.FetchUsers();
        }

        public static string GetIdByUsername(string username)
        {
           return SignInViewModel.userList.Find(user => user.Username == username).Id;
        }
    }
}