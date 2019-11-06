using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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
        }
    }
}