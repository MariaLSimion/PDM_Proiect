﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMProiect
{
    internal class SpotifyTopArtist
    {
        public int id { get; set; }
        public int rank { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string noOfListeners { get; set; }
        public string genre { get; set; }

        public SpotifyTopArtist()
        {

        }
    }
}
