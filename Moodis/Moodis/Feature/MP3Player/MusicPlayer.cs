using System.Runtime.InteropServices;
using System.Text;

namespace Music
{
    class MusicPlayer
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwdCallBack);
        public void open(string File)
        {
            string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
            string command = string.Format(Format, File);
            mciSendString(command, null, 0, 0);
        }
        public void Play()
        {
            string command = "play MediaFile";
            mciSendString(command, null, 0, 0);
        }
        public void Stop()
        {
            string command = "close MediaFile";
            mciSendString(command, null, 0, 0);
        }
    }
}
