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

using Android.Media;
using Android.Util;
using System.IO;

namespace Moodis.Feature.Music
{
    public class MusicPlayer
    {
        private readonly string TAG = nameof(MusicPlayer);
        private Context context;

        private MediaPlayer player;
        public MusicPlayer(Context context)
        {
            player = new MediaPlayer();
            this.context = context;
        }

        public void Play(string filePath)
        {
                Uri uriResult;
                if (Uri.TryCreate(filePath, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp))
                {
                    Log.Info(TAG, "url");
                    try
                    {
                        player.SetDataSource(filePath);
                        player.Prepare();
                    }
                    catch (IOException e)
                    {
                        Log.Debug(TAG, e.Message);
                    }
                    player.Start();
            }
            else
            {
                Log.Info(TAG, "other");
                Play(Resource.Raw.sample);
            }
        }

        public void Play(int resId)
        {
            player = MediaPlayer.Create(context, resId);
        }

        public void Stop()
        {
            if (player != null) { 
            player.Stop();
            }
        }

        public void Release()
        {
            if (player != null)
            {
                player.Release();
            }
        }
    }
}