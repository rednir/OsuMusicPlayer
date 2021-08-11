using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuParsers.Beatmaps;
using OsuParsers.Decoders;

namespace OsuMusicPlayer.Models
{
    public class OsuSongCollection : List<OsuSong>
    {
        public OsuSongCollection()
        {
        }

        public void Populate(DirectoryInfo songsFolder)
        {
            DirectoryInfo[] songFolders = songsFolder.GetDirectories();
            foreach (DirectoryInfo folder in songFolders)
            {
                FileInfo[] beatmapFiles = folder.GetFiles("*.osu");
                Beatmap beatmap = BeatmapDecoder.Decode(beatmapFiles[0].FullName);
                this.Add(new OsuSong(beatmap, folder.FullName));

                /*List<string> audioFileNames = new();
                foreach (FileInfo file in beatmapFiles)
                {
                    Beatmap beatmap = BeatmapDecoder.Decode(file.FullName);

                    // Avoid adding the same audio file.
                    if (!audioFileNames.Contains(beatmap.GeneralSection.AudioFilename))
                    {
                        audioFileNames.Add(beatmap.GeneralSection.AudioFilename);
                        this.Add(new OsuSong(beatmap, folder.FullName));
                    }
                }*/
            }
        }
    }
}
