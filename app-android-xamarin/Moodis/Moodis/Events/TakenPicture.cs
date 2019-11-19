using System;

namespace Moodis.Events
{
    public class TakenPictureArgs : EventArgs
    {
        public string ImagePath { get; set; }

        public TakenPictureArgs(string imagePath)
        {
            ImagePath = imagePath;
        }

        private TakenPictureArgs() { }
    }
}