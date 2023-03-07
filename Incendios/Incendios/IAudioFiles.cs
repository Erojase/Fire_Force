using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incendios
{
    internal class IAudioFiles
    {
        public string principal;
        public string classic;
        public string windy;

        public IAudioFiles()
        {
            principal = "..\\..\\audio\\fire_force_main.mp3";
            classic = "..\\..\\audio\\At_Dooms_Gate.mp3";
            windy = "..\\..\\audio\\tuba.mp3";
        }

        public string IntToSong(int index)
        {
            switch (index)
            {
                case 0:
                    return principal;
                case 1:
                    return classic;
                case 2:
                    return windy;
                default:
                    return principal;
            }
        }
    }
}
