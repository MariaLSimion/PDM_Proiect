using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMProiect
{
    [Table("merch")]
    public class Merch
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int id { get; set; }
        [Column("artist")]
        public string artist { get; set; }
        [Column("productName")]
        public string productName { get; set; }
        [Column("imageLink")]
        public string imageLink { get; set; }
        [Column("price")]
        public string price { get; set; }
        [Column("type")]
        public string type { get; set; }
        [Column("disponibility")]
        public string disponibility { get; set; }
        [Column("details")]
        public string details { get; set; }

        public Merch()
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
