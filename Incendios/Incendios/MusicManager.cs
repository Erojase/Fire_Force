using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Incendios
{
    class MusicManager
    {

        private MediaPlayer Player;
        private bool isMuted = false;
        private double prevVolume;


        public MusicManager() {
            Player = new MediaPlayer();
        }

        public void Play(string url)
        {
            Player.Open(new Uri(url, UriKind.Relative));
            Player.Play();
            prevVolume = Player.Volume;
        }

        public void Mute()
        {
            if (isMuted)
            {
                Player.Volume = prevVolume;
            }
            else
            {
                Player.Volume = 0;
            }
            isMuted = !isMuted;
        }
    }
}
