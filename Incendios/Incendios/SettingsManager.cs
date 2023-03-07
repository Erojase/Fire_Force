using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Incendios
{
    class SettingsManager
    {

        public int Song {get; set;}
        public double Volume { get; set;}
        public int Resolution { get; set;}
        public int Difficulty { get; set;}

        public SettingsManager() 
        {

        }

        public void LoadConfig()
        {
            var dir = Directory.GetFiles("./");
            if (!File.Exists("./config"))
            {
                File.WriteAllText("./config", DefaultSettings());
            }

            string[] rawSettings = File.ReadAllText("./config").Split(';');
            Song = int.Parse(rawSettings[0].Replace("\n", "").Split(':')[1]);
            Volume = double.Parse(rawSettings[1].Replace("\n", "").Split(':')[1])/100;
            Resolution = int.Parse(rawSettings[2].Replace("\n", "").Split(':')[1]);
            Difficulty = int.Parse(rawSettings[3].Replace("\n", "").Split(':')[1]);

        }
        
        public void SaveSettings(int resolutions, int difficulty, double volume, int song)
        {
            Song = song;
            Volume = volume;
            Resolution = resolutions;
            Difficulty = difficulty;
            File.WriteAllText("./config", "song:"+song+";\nvolume:"+volume+";\nresolution:"+resolutions+";\ndifficulty:"+difficulty+"");
        }

        private string DefaultSettings()
        {
            return "song:0;\nvolume:50;\nresolution:0;\ndifficulty:1";
        }
    }
}
