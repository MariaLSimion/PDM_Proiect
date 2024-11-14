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
            //DropTableIfExists();
            conn.CreateTable<Artist>();
        }
        /*private void DropTableIfExists()
        {
            conn.DropTable<Artist>();
        } */
        public bool ArtistExists(string name, int id, string genre)
        {
            return conn.Table<Artist>()
                       .Any(a => a.name == name && a.id == id && a.genre == genre);
        }

        public int InsertAll(List<Artist> artists)
        {
            int insertedCount = 0;
            foreach (var artist in artists)
            {
                if (!ArtistExists(artist.name, artist.id, artist.genre))
                {
                    conn.Insert(artist);
                    insertedCount++;
                }
            }
            return insertedCount;
        }

        public List<string> GetUniqueGenres()
        {
            return conn.Table<Artist>()
                       .Select(a => a.genre)
                       .Distinct()
                       .ToList();
        }
        public List<Artist> GetArtistsByGenre(string genre)
        {
            return conn.Table<Artist>()
                       .Where(a => a.genre == genre)
                       .ToList();
        }
        public Artist GetArtistById(int id)
        {
            return conn.Table<Artist>()
               .FirstOrDefault(a => a.id == id);
        }
    }
}
