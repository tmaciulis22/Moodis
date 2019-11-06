using Moodis.Network.Face;
using System;
using System.Threading.Tasks;
using Moodis.Feature.Login;
using Moodis.Extensions;
using System.Linq;
using Moodis.Feature.SignIn;
using System.IO;
using Android.Graphics;
using Moodis.Constants.Enums;
using Moodis.Database;

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

        public async Task<Response> GetFaceEmotionsAsync()
        {
            var face = await Face.Instance.DetectUserEmotions(currentImage.ImagePath, SignInViewModel.currentUser.personGroupId, SignInViewModel.currentUser.username);

            if (face != null)
            {
                currentImage.SetImageInfo(face);
                return Response.OK;
            }
            else
            {
                return Response.FaceNotDetected;
            }
        }

        public void DeleteImage()
        {
            if (File.Exists(currentImage.ImagePath))
            {
                File.Delete(currentImage.ImagePath);
            }
        }

        public void AddImage()
        {
            DatabaseModel.AddImageInfoToDatabase(currentImage);
        }

        public int GetHighestEmotionIndex()
        {
            var highestConfidence = currentImage.emotions.Max();
            return currentImage.emotions.ToList().IndexOf(highestConfidence);
        }
    }
}
