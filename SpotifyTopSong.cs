using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMProiect
{
    public class SpotifyTopSong
    {
        public int id { get; set; }

        public string title { get; set; }
        public string albumName { get; set; }
        public string duration { get; set; }
        public string artistName { get; set; }
        public string noOfListens { get; set; }
        public string genre { get; set; }

        public SpotifyTopSong() { }
    }
}
