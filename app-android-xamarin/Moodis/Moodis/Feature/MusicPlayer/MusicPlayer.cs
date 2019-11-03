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
using Android.Support.Design.Widget;

namespace Moodis.Feature.Music
{
    public class MusicPlayer
    {
        private readonly string TAG = nameof(MusicPlayer);
        private Context context;

        private static MediaPlayer player;
        public MusicPlayer(Context context)
        {
            player = new MediaPlayer();
            this.context = context;
        }

        public void Play(string filePath)
        {
            if (player != null)
            {
                Uri uriResult;
                if (Uri.TryCreate(filePath, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp))
                {
                    Log.Info(TAG, "url");
                    try
                    {
                        if (!player.IsPlaying)
                        {
                            player.SetDataSource(filePath);
                            player.Prepare();
                        }
                        else
                        {
                            Snackbar.Make(((Activity)context).FindViewById(Resource.Id.menuActivity), Resource.String.song_already_playing, Snackbar.LengthShort).Show();
                        }
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
        }

        public void Play(int resId)
        {
            player = MediaPlayer.Create(context, resId);
            player.Start();
        }

        public void Stop()
        {
            if (player != null) { 
                player.Stop();
                player.Reset();
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