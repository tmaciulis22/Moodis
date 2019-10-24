using Android.Arch.Lifecycle;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Extensions;
using Moodis.Feature.Login;
using Moodis.Network.Face;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moodis.Feature.SignIn
{
    public class SignInViewModel: ViewModel
    {
        public static List<User> userList;
        public static User currentUser;

        public bool Authenticate(string username, string password)
        {
            FetchUserList();

            //TODO Change to Crypto.CalculateMD5Hash(password));
            currentUser = userList.Find(user => user.username == username && user.password == password);

            if (currentUser == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Response> AuthenticateWithFace(string imagePath)
        {
            FetchUserList();

            List<Person> identifiedPersons = null;
            try
            {
                identifiedPersons = await Face.Instance.IdentifyPersons(imagePath) as List<Person>;
            }
            catch
            {
                return Response.ApiError;
            }

            if (identifiedPersons.IsNullOrEmpty())
            {
                return Response.UserNotFound;
            }

            currentUser = userList.Find(user => user.username == identifiedPersons.ToArray()[0].Name); //TODO change this to multiple user recognition.

            if (currentUser == null)
            {
                return Response.UserNotFound;
            }
            return Response.OK;
        }

        private void FetchUserList()
        {
            userList = new List<User>();
            userList.Add(new User("juris", "@Komparchas1"));

            //if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            //{
            //    userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            //}
        }

        public static User getUser(string username)
        {
            //userList = new List<User>();

            //if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            //{
            //    userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            //}
            return userList.Find(user => user.username == username);
        }
    }
}
