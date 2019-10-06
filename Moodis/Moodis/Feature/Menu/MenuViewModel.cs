using Moodis.Network.Face;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Moodis.Feature.Login;
using Moodis.Extensions;

namespace Moodis.Ui
{
    class MenuViewModel
    {
        public ImageInfo currentImage = new ImageInfo();
        private Bitmap userImage;
        private string jsonAsString;

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
            Face face = Face.Instance;
            var json = await face.SendImageForAnalysis(currentImage.ImagePath);
            var jsonAsString = json.FromJsonToString();

            if (!string.IsNullOrEmpty(jsonAsString))
            {
                currentImage.SetImageInfo(jsonAsString);
            }
        }

        public void UserAddImage()
        {
           LoginViewModel.currentUser.addImage(currentImage);
            Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", LoginViewModel.userList);
        }
    }
}
