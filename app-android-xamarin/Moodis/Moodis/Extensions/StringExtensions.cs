using Android.Graphics;
using System.IO;

namespace Moodis.Extensions
{
    public static class StringExtensions
    {
        public static void RotateImage(this string imageFilePath)
        {
            var matrix = new Matrix();
            matrix.PostRotate(-90);
            var bitmap = BitmapFactory.DecodeFile(imageFilePath);
            bitmap = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);

            var stream = new FileStream(imageFilePath, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            stream.Close();
            bitmap.Dispose();
        }

        public static int EmotionColor(this string emotionName)
        {
            return emotionName switch
            {
                "Anger" => Resource.Color.color_anger,
                "Disgust" => Resource.Color.color_disgust,
                "Fear" => Resource.Color.color_fear,
                "Sadness" => Resource.Color.color_sadness,
                "Contempt" => Resource.Color.color_contempt,
                "Neutral" => Resource.Color.color_neutral,
                "Surprise" => Resource.Color.color_suprise,
                _ => Resource.Color.color_happiness //Happiness
            };
        }
    }
}