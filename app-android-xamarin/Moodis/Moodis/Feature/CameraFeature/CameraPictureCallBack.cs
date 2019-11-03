using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Moodis.Events;
using Moodis.Feature.Menu;
using Moodis.Feature.Register;
using Moodis.Feature.SignIn;

namespace Moodis.Feature.CameraFeature
{
    public class CameraPictureCallBack : Java.Lang.Object, Camera.IPictureCallback
    {
        private static readonly string TAG = nameof(CameraPictureCallBack);
        Context context;
        event EventHandler<TakenPictureArgs> AfterTakenPicture;

        public CameraPictureCallBack(Context cont, EventHandler<TakenPictureArgs> afterTakenPicture = null)
        {
            context = cont;
            AfterTakenPicture = afterTakenPicture;
        }

        public void OnPictureTaken(byte[] data, Camera camera)
        {
            try
            {
                var fileNameFormatting = System.DateTime.Now.ToString().Replace("-", "").Replace("/", "").Replace(":", "").Replace("PM", "").Replace(" ", "") + ".jpeg";
                var imageFileName = Android.Net.Uri.Parse(fileNameFormatting).LastPathSegment;
                var os = context.OpenFileOutput(imageFileName, FileCreationMode.Private);

                System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(os);
                binaryWriter.Write(data);
                binaryWriter.Close();

                imageFileName = (string)context.GetFileStreamPath(imageFileName);
                if (AfterTakenPicture != null)
                {
                    AfterTakenPicture.Invoke(this, new TakenPictureArgs(imageFileName));
                }
                else
                {
                    var menuActivity = new Intent(context, typeof(MenuActivity));
                    menuActivity.PutExtra("ImagePath", imageFileName);
                    context.StartActivity(menuActivity);
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                Log.Debug(TAG, "File not found: " + e.Message);
            }
        }
    }
}
