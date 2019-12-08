using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Moodis.Constants.Enums;
using Moodis.Events;
using Moodis.Feature.Menu;
using Moodis.Ui;
using System;

namespace Moodis.Feature.CameraFeature
{
    public class CameraFragment : Fragment
    {
        private readonly string TAG = nameof(CameraFragment);
        Camera camera;
        FrameLayout frameLayout;
        Button snapButton;
        View progressBar;

        bool CameraReleased = false;

        event EventHandler<TakenPictureArgs> AfterTakenPicture;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_camera, container, false);

            snapButton = view.FindViewById<Button>(Resource.Id.takePictureButton);
            snapButton.BringToFront();
            snapButton.Click += SnapButtonClick;

            camera = SetUpCamera();
            frameLayout = view.FindViewById<FrameLayout>(Resource.Id.camera_preview);
            SetCameraPreview();

            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            AfterTakenPicture = async (sender, e) =>
            {
                progressBar = view.FindViewById(Resource.Id.progressBarCamera);
                progressBar.Visibility = ViewStates.Visible;
                progressBar.BringToFront();
                snapButton.Enabled = false;  

                MenuViewModel.Instance.currentImage = new ImageInfo
                {
                    ImagePath = e.ImagePath
                };

                var response = await MenuViewModel.Instance.GetFaceEmotionsAsync();
                if (response == Response.ApiError)
                {
                    Toast.MakeText(Context, Resource.String.api_error, ToastLength.Short).Show();
                }
                else if (response == Response.FaceNotDetected)
                {
                    Toast.MakeText(Context, Resource.String.warning_face_detection, ToastLength.Short).Show();
                }
                else
                {
                    Activity.SetResult(Android.App.Result.Ok);
                    Activity.Finish();
                }
                progressBar.Visibility = ViewStates.Gone;
                snapButton.Enabled = true;
            };
        }

        private void SnapButtonClick(object sender, EventArgs e)
        {
            try
            {
                camera.StartPreview();
                camera.TakePicture(null, null, new CameraPictureCallBack(Activity, AfterTakenPicture));//sends photo to cameraPicturecallBack
            }
            catch (Exception ex)
            {
                Log.Error(TAG, "Error taking picture: " + ex.Message);
            }
        }

        public override void OnDestroy()
        {
            try
            {
                camera.StopPreview();
                camera.Release();
            }
            catch (Exception e)
            {
                Log.Error(TAG, e.StackTrace);
            }

            CameraReleased = true;
            base.OnDestroy();
        }

        public override void OnResume()
        {
            if (CameraReleased)
            {
                try
                {
                    camera.Reconnect();
                    camera.StartPreview();
                }
                catch (Exception e)
                {
                    Log.Error(TAG, e.StackTrace);
                }
                CameraReleased = false;
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