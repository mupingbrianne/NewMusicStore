using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewMusicStore.Models
{
    public class MusicGenreViewModel
    {
        public List<Music> Musics;

        public SelectList Genre;
        public string MusicGenre { get; set; }
        public string SearchString { get; set; }

    }
}
