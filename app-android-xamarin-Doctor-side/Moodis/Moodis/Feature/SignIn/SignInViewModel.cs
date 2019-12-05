﻿using Android.Arch.Lifecycle;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Constants.Enums;
using Moodis.Database;
using Moodis.Extensions;
using Moodis.Feature.Login;
using Moodis.Network.Face;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moodis.Feature.SignIn
{
    public class SignInViewModel : ViewModel
    {
        public static List<User> userList;
        public static User currentUser;

        public bool Authenticate(string username, string password)
        {
            FetchUserList();

            currentUser = userList.Find(user => user.Username == username && user.Password == Crypto.CalculateMD5Hash(password));

            if (currentUser == null || !currentUser.IsDoctor)
            {
                return false;
            }
            return true;
        }

        //NOTE this method is used only for development and testing purposes, to clear everything in DB and in Face API
        public async Task<Response> DeleteEverything()
        {
            DatabaseModel.DeleteEverything();
            return await Face.Instance.DeleteEverything();
        }

        private void FetchUserList()
        {
            userList = DatabaseModel.FetchUsers();
        }

        public async Task<Response> DeleteUser()
        {
            DatabaseModel.DeleteUserFromDatabase(SignInViewModel.currentUser);
            return await Face.Instance.DeletePersonGroup(SignInViewModel.currentUser.PersonGroupId);
        }
    }
}
