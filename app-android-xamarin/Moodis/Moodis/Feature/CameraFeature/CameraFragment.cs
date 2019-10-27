﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


namespace Moodis.Feature.CameraFeature
{
    public class CameraFragment : Fragment
    {
        private readonly string TAG = nameof(CameraFragment);
        Camera camera;
        FrameLayout frameLayout;

        bool cameraReleased = false;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CameraFragmentLayout, container, false);

            var snapButton = view.FindViewById<Button>(Resource.Id.takePictureButton);
            snapButton.BringToFront();
            snapButton.Click += SnapButtonClick;

            camera = SetUpCamera();
            frameLayout = view.FindViewById<FrameLayout>(Resource.Id.camera_preview);
            SetCameraPreview();

            return view;
        }

        private void SnapButtonClick(object sender, EventArgs e)
        {
            try
            {
                camera.StartPreview();
                camera.TakePicture(null, null, new CameraPictureCallBack(Activity));
            }
            catch (Exception ex)
            {
                Log.Debug(TAG, ex.Message);
            }
        }

        public override void OnDestroy()
        {
            camera.StopPreview();
            camera.Release();
            cameraReleased = true;
            base.OnDestroy();
        }

        public override void OnResume()
        {
            if (cameraReleased)
            {
                camera.Reconnect();
                camera.StartPreview();
                cameraReleased = false;
            }
            base.OnResume();
        }

        private void SetCameraPreview()
        {
            frameLayout.AddView(new CameraPreview(Activity, camera));
        }

        Camera SetUpCamera()
        {
            Camera c = null;
            try
            {
                c = Camera.Open(1);
            }
            catch (Exception e)
            {
                Log.Debug(TAG, "Device camera not available now. " + e.Message);
            }

            return c;
        }
    }
}