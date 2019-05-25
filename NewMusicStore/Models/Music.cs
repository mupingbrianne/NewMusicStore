using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewMusicStore.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int SingerId { get; set; }
        public Singer Singer { get; set; }

    }
}
