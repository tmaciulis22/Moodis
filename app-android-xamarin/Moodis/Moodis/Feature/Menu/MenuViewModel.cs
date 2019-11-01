using Moodis.Network.Face;
using System;
using System.Threading.Tasks;
using Moodis.Feature.Login;
using Moodis.Extensions;
using System.Linq;
using Moodis.Feature.SignIn;
using System.IO;
using Android.Graphics;

namespace Moodis.Ui
{
    public class MenuViewModel
    {
        public ImageInfo currentImage;
        public Bitmap image;

        private static readonly Lazy<MenuViewModel> obj = new Lazy<MenuViewModel>(() => new MenuViewModel());
        private MenuViewModel() { 
            currentImage = new ImageInfo(); 
        }
        public static MenuViewModel Instance
        {
            get
            {
                return obj.Value;
            }
        }

        public async Task GetFaceEmotionsAsync()
        {
            var face = await Face.Instance.DetectUserEmotions(currentImage.ImagePath, SignInViewModel.currentUser.personGroupId, SignInViewModel.currentUser.username);

            if (face != null)
            {
                currentImage.SetImageInfo(face);
            }
        }
        public Bitmap RotateImage()
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(-90);
            return Bitmap.CreateBitmap(image, 0, 0, image.Width, image.Height, matrix, true);
        }

        public void DeleteImage()
        {
            if (File.Exists(currentImage.ImagePath))
            {
                File.Delete(currentImage.ImagePath);
            }
        }

        public void UserAddImage()
        {
            /*
            SignInViewModel.currentUser.addImage(currentImage);
            Serializer.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/users.bin", SignInViewModel.userList);
            */
        }

        public int getHighestEmotionIndex()
        {
            var highestConfidence = currentImage.emotions.Max();
            return currentImage.emotions.ToList().IndexOf(highestConfidence);
        }
    }
}
