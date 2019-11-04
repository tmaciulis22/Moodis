using Moodis.Network.Face;
using System;
using System.Threading.Tasks;
using Moodis.Feature.Login;
using Moodis.Extensions;
using System.Linq;
using Moodis.Feature.SignIn;
using System.IO;
using Android.Graphics;
using Android.Util;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Moodis.Ui
{
    public class MenuViewModel
    {
        public ImageInfo currentImage;
        public Bitmap image;
        private readonly string TAG = nameof(MenuViewModel);

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
            Log.Debug(TAG, "getFaceEmotions async start");
            //var face = await Face.Instance.DetectUserEmotions(currentImage.ImagePath, SignInViewModel.currentUser.personGroupId, SignInViewModel.currentUser.username);
            // Reform after creating login with user faces
            var face = await Face.Instance.DetectFaceEmotions(currentImage.ImagePath);
            DetectedFace tmp;
            try
            {
                tmp = face.First();
            }
            catch (ArgumentNullException e)
            {
                Log.Debug(TAG, e.Message);
                tmp = null;
            }
            Log.Debug(TAG, "getting face");
            if (tmp != null)
            {
                currentImage.SetImageInfo(tmp);
            }
            else
            {
                currentImage = new ImageInfo();
            }
        }
        private Bitmap RotateImage(Bitmap image)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(-90);
            return Bitmap.CreateBitmap(image, 0, 0, image.Width, image.Height, matrix, true);
        }

        public void RotateAndOverrideImage()
        {
            image = RotateImage(image);
            var stream = new FileStream(currentImage.ImagePath, FileMode.Create);
            image.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            stream.Close();
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
