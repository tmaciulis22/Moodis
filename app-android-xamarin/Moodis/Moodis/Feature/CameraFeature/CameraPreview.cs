using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace Moodis.Feature.CameraFeature
{
    public class CameraPreview : SurfaceView, ISurfaceHolderCallback
    {
        Android.Hardware.Camera customCamera;
        static bool stopped;

        public CameraPreview(Context context, Android.Hardware.Camera camera) : base(context)
        {
            customCamera = camera;
            customCamera.SetDisplayOrientation(90);

            Holder.AddCallback(this);
            // deprecated but required on Android versions less than 3.0
            Holder.SetType(SurfaceType.PushBuffers);
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            if (Holder.Surface == null)
            {
                return;
            }

            try
            {
                customCamera.StopPreview();
            }
            catch (Exception e)
            {
                Log.Error("CAMERA_PREVIEW","Error Stopping preview" + e.Message);
            }

            try
            {
                customCamera.SetPreviewDisplay(Holder);
                customCamera.StartPreview();
            }
            catch (Exception e)
            {
                Log.Debug("CAMERA_PREVIEW", "Error starting camera preview: " + e.Message);
            }
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            try
            {
                customCamera.SetPreviewDisplay(holder);
                customCamera.StartPreview();
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            customCamera.Dispose();
        }
    }
}