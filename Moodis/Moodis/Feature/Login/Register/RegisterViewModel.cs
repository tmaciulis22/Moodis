using Moodis.Network.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodis.Feature.Login.Register
{
    public class RegisterViewModel
    {
        public static List<User> userList;
        public User currentUser;
        internal int photosTaken = 0;

        public async Task<bool> AddUser(string username, string password)
        {
            userList = new List<User>();

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin"))
            {
                userList = Serializer.Load<List<User>>(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin");
            }

            if (userList.Exists(x => x.username == username))
            {
                return false;
            }
            else
            {
                currentUser = new User(username, Crypto.CalculateMD5Hash(password));
                currentUser.personGroupId = userList.Count().ToString();
                currentUser.faceApiPerson = await Face.Instance.CreateNewPerson(currentUser.personGroupId, username);

                userList.Add(currentUser);

                return Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", userList);
            }
        }

        public async Task<bool> AddFaceToPerson(string imagePath)
        {
            var wasSuccessful = await Face.Instance.AddFaceToPerson(imagePath, currentUser.personGroupId, currentUser);
            if (wasSuccessful)
            {
                photosTaken++;

                if (photosTaken == 3)
                {
                    await Face.Instance.TrainPersonGroup(currentUser.personGroupId);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
