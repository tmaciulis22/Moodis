using AForge.Video.DirectShow;
using System.Linq;

namespace Moodis.Feature.Camera
{
    class CameraViewModel
    {
        public int getHighestResoliutionIndex(VideoCaptureDevice cam)
        {
            int index = -1;
            int highestResoliution = 0;
            foreach (var option in cam.VideoCapabilities)
            {
                int height = option.FrameSize.Height;
                int width = option.FrameSize.Width;
                string temp = width.ToString() + "*" + height.ToString();
                if (height * width > highestResoliution)
                {
                    highestResoliution = height * width;
                    index = cam.VideoCapabilities.ToList().IndexOf(option);
                }
            }
            return index;
        }
    }
}
