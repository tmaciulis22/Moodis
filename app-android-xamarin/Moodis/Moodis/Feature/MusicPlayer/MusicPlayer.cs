using Android.Content;
using Android.Media;
using Android.Util;
using Java.IO;
using System;

namespace Moodis.Feature.Music
{
    public class MusicPlayer
    {
        private readonly string TAG = nameof(MusicPlayer);
        private Context context;

        private static MediaPlayer player;
        public MusicPlayer(Context context)
        {
            this.context = context;
            player = new MediaPlayer();
        }

        public void Play(string filePath)
        {
            if (player != null && !player.IsPlaying)
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
                        player.Start();
                    }
                    catch (IOException e)
                    {
                        Log.Debug(TAG, e.Message);
                    }
                }
                else
                {
                    FileInputStream fileInputStream = null;
                    try
                    {
                        fileInputStream = new FileInputStream(filePath);
                        player.SetDataSource(fileInputStream.FD);
                        fileInputStream.Close();
                        player.Prepare();
                    }
                    catch (FileNotFoundException e)
                    {
                        Log.Debug(TAG, e.Message);
                    }
                    catch (IOException e)
                    {
                        Log.Debug(TAG, e.Message);
                    }
                }
            }
        }

        public void Play(int resId)
        {
            if (player != null && !player.IsPlaying)
            {
                player = MediaPlayer.Create(context, resId);
                player.Start();
            }
        }

        public void Stop()
        {
            if (player != null)
            {
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

        public bool IsPlaying()
        {
            return player.IsPlaying;
        }
    }
}