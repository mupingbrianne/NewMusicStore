using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicStore.Models
{
    public class Singer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Music> Musics { get; set; }
    }
}
