using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using System;

namespace Moodis.Feature.CameraFeature
{
    public class CameraPreview : SurfaceView, ISurfaceHolderCallback
    {
        static readonly string TAG = "CameraPreview";
        Android.Hardware.Camera _camera;
        private const int rotation = 90;

        public CameraPreview(Context context, Android.Hardware.Camera camera) : base(context)
        {
            _camera = camera;
            _camera.SetDisplayOrientation(rotation);

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
                _camera.StopPreview();
            }
            catch (Exception e)
            {
                Log.Error("CAMERA_PREVIEW", "Error Stopping preview" + e.Message);
            }

            try
            {
                _camera.SetPreviewDisplay(Holder);
                _camera.StartPreview();
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
                _camera.SetPreviewDisplay(holder);
                _camera.StartPreview();
            }
            catch (Exception e)
            {
                Log.Debug(TAG, "Error setting camera preview: " + e.Message);
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            _camera.Dispose();
        }
    }
}