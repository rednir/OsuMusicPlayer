using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuMusicPlayer.Models
{
    public class UserData
    {
        public DateTime SongsFolderLastWriteTime { get; set; }

        public OsuSongCollection Songs { get; set; } = new();
    }
}
