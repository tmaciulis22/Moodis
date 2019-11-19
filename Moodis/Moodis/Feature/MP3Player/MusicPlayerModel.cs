using Music;
using System;

namespace Moodis.Feature.MP3Player
{
    class MusicPlayerModel
    {
        private MusicPlayer player = new MusicPlayer();
        String[] musicLabels = {"Anger.mp3", "Contempt.mp3", "Disgust.mp3", "Fear.mp3", "Happiness.mp3", "Neutral.mp3", "Sadness.mp3", "Surprise.mp3"};
        string location = System.IO.Path.GetFullPath(@"..\..\") + "Feature\\MP3Player\\Music";
        public void StartMusic(int highestEmotionIndex)
        {
            if (highestEmotionIndex != -1)
            {
                player.open(location + "\\" + musicLabels[highestEmotionIndex]);
                player.Play();
            }
        }
        public void StopMusic()
        {
            player.Stop();
        }  
    }
}
