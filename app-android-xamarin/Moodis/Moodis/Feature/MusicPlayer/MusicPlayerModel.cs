/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Moodis.Feature.Music
{
    class MusicPlayerModel
    {
        int[] musicLabels = { Resource.Raw.Anger, Resource.Raw.Contempt, Resource.Raw.Disgust, Resource.Raw.Fear, Resource.Raw.Happiness, Resource.Raw.Neutral, 
                                Resource.Raw.Sadness, Resource.Raw.Surprise };

        public void StartMusic(int highestEmotionIndex)
        {
            if (highestEmotionIndex != -1)
            {
                player.Play(musicLabels[highestEmotionIndex]);
                isPlaying = true;
            }
        }

        public void StopMusic()
        {
            player.Stop();
            isPlaying = false;
        }
    }
}
*/