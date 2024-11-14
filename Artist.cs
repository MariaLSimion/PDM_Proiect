using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PDMProiect
{
    [Table("artist")]
    public class Artist
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column ("id")]
        public int id { get; set; }
        [Column ("name")]
        public string name { get; set; }
        [Column ("urlPhoto")]
        public string urlPhoto { get; set; }
        [Column ("genre")]
        public string genre { get; set; }
        [Column ("merchStore")]
        public string urlMerchStore { get; set; }
        [Column("biography")]
        public string biography { get; set; }
        [Column("funfacts")]
        public string funfacts { get; set; }
        [Column("socials")]
        public string socials { get; set; }
        [Column("popularity")]
        public string popularity { get; set; }
        public Artist()
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
