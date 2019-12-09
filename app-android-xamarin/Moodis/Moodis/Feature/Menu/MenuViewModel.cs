using Android.Graphics;
using Moodis.Constants.Enums;
using Moodis.Feature.SignIn;
using Moodis.Network.Face;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Moodis.Network;
using Refit;
using Android.Util;
using Moodis.Extensions;

namespace Moodis.Ui
{
    public class MenuViewModel
    {
        public ImageInfo currentImage;
        public Bitmap image;

        private static readonly Lazy<MenuViewModel> obj = new Lazy<MenuViewModel>(() => new MenuViewModel());
        private MenuViewModel()
        {
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
            try
            {
                currentImage.ImagePath.RotateImage();
                var face = await Face.Instance.DetectUserEmotions(currentImage.ImagePath, SignInViewModel.currentUser.PersonGroupId, SignInViewModel.currentUser.Username);
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
            catch
            {
                return Response.ApiError;
            }
        }

        public void DeleteImage()
        {
            if (File.Exists(currentImage.ImagePath))
            {
                File.Delete(currentImage.ImagePath);
            }
        }

        public async void AddImage()
        {
            try
            {
                await API.ImageInfoEndpoint.AddImageInfo(currentImage);
            }
            catch (ApiException ex)
            {
                Log.Error(GetType().Name, ex.Message);
            }
        }

        public int GetHighestEmotionIndex()
        {
            var properties = typeof(Emotion).GetProperties().ToList();

            return properties.FindIndex(property => {
                return property.Name == currentImage.HighestEmotion;
            });
        }
    }
}
