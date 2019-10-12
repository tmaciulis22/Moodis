using Moodis.Network.Face;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Moodis.Feature.Login;
using Moodis.Extensions;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;

namespace Moodis.Ui
{
    public class MenuViewModel
    {
        public ImageInfo currentImage;
        private Bitmap userImage;

        public MenuViewModel()
        {
            currentImage = new ImageInfo();
        }

        public Bitmap ShowImage(string fileToDisplay)
        {
            userImage?.Dispose();
            userImage = new Bitmap(fileToDisplay);
            return userImage;
        }

        public async Task GetFaceEmotionsAsync()
        {
            var faceList = await Face.Instance.DetectFaceEmotions(currentImage.ImagePath, "asd", "Asd");

            if (faceList != null)
            {
                currentImage.SetImageInfo(new List<DetectedFace>());
            }
        }

        public void UserAddImage()
        {
            SignInViewModel.currentUser.addImage(currentImage);
            Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", SignInViewModel.userList);
        }
    }
}
