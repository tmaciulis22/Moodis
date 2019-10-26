
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
using Android.Views;
using Android.Widget;
using Java.IO;
using static Java.Util.Jar.Attributes;

namespace Moodis.Feature.CameraFeature
{
    public class CameraPictureCallBack : Java.Lang.Object, Camera.IPictureCallback
    {
        private static readonly string TAG = nameof(CameraPictureCallBack);
        Context context;

        public CameraPictureCallBack(Context cont)
        {
            context = cont;
        }

        public void OnPictureTaken(byte[] data, Camera camera)
        {
            try
            {
                var fileNameFormatting = System.DateTime.Now.ToString().Replace("-", "").Replace("/", "").Replace(":", "").Replace("PM", "").Replace(" ", "") + ".jpeg";
                string fileName = Uri.Parse(fileNameFormatting).LastPathSegment;
                var os = context.OpenFileOutput(fileName, FileCreationMode.Private);
                System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(os);
                binaryWriter.Write(data);
                binaryWriter.Close();
                Log.Info(TAG,"Picture saved successfully with name: " + fileName);
                

               // context.DeleteFile(fileName);//TODO HANDLE FILE DELETION AFTER IT HAS BEEN PARSED THROUGH FACE DETECTION API

                camera.StartPreview();
            }
            catch (System.Exception e)
            {
                Log.Debug(TAG, "File not found: " + e.Message);
            }
        }
    }
}
