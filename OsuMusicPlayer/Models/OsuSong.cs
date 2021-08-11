using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuParsers.Beatmaps;

namespace OsuMusicPlayer.Models
{
    public class OsuSong
    {
        public OsuSong(Beatmap beatmap, string folderPath)
        {
            Title = beatmap.MetadataSection.Title;
            Artist = beatmap.MetadataSection.Artist;
            AudioFileName = beatmap.GeneralSection.AudioFilename;
            BackgroundFileName = beatmap.EventsSection.BackgroundImage;
            FolderPath = folderPath;
        }

        public string Title { get; set; } = "Unknown Title";

        public string Artist { get; set; } = "Unknown Artist";

        public string AudioFileName { get; set; }

        public string? BackgroundFileName { get; set; }

        public string FolderPath { get; set; }
    }
}
