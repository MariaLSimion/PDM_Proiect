using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PDMProiect
{
    public class DaoArtist
    {
        SQLiteConnection conn;

        public DaoArtist()
        {
            string cale = Path.Combine(FileSystem.AppDataDirectory, "artist.db");
            conn = new SQLiteConnection(cale);  
            conn.CreateTable<Artist>();
        }
        public int InsertAll(List<Artist> artists)
        {
            return conn.InsertAll(artists);
        }

        public List<string> GetUniqueGenres()
        {
            return conn.Table<Artist>()
                       .Select(a => a.genre)
                       .Distinct()
                       .ToList();
        }


    }
}
