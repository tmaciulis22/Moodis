using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Moodis.Constants.Enums;

namespace Moodis.Events { 
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