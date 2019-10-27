
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
using Moodis.Feature.Menu;

namespace Moodis.Feature.CameraFeature
{
    public class CameraPictureCallBack : Java.Lang.Object, Camera.IPictureCallback
    {
        private static readonly string TAG = nameof(CameraPictureCallBack);
        Context context;
        private string imageFileName;

        public CameraPictureCallBack(Context cont)
        {
            context = cont;
        }

        public void OnPictureTaken(byte[] data, Camera camera)
        {
            try
            {
                var fileNameFormatting = System.DateTime.Now.ToString().Replace("-", "").Replace("/", "").Replace(":", "").Replace("PM", "").Replace(" ", "") + ".jpeg";
                imageFileName = Uri.Parse(fileNameFormatting).LastPathSegment;
                var os = context.OpenFileOutput(imageFileName, FileCreationMode.Private);

                System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(os);
                binaryWriter.Write(data);
                binaryWriter.Close();
      
                var menuActivity = new Intent(context, typeof(MenuActivity));
                imageFileName = (string)context.GetFileStreamPath(imageFileName);
                menuActivity.PutExtra("ImagePath", imageFileName);
                context.StartActivity(menuActivity);

                camera.StartPreview();
            }
            catch (System.Exception e)
            {
                Log.Debug(TAG, "File not found: " + e.Message);
            }
        }
    }
}
