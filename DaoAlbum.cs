using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PDMProiect
{
    public class DaoAlbum
    {
        SQLiteConnection conn;

        public DaoAlbum()
        {
            string cale = Path.Combine(FileSystem.AppDataDirectory, "album.db");
            conn = new SQLiteConnection(cale);
            conn.CreateTable<Album>();
        }
        public bool AlbumExists(string name, int id)
        {
            return conn.Table<Album>()
                       .Any(a => a.AlbumName == name && a.Id == id);
        }
        public int InsertAll(List<Album> albums)
        {
            int insertedCount = 0;
            foreach (var album in albums)
            {
                if (!AlbumExists(album.AlbumName, album.Id))
                {
                    conn.Insert(album);
                    insertedCount++;
                }
            }
            return insertedCount;
        }
    }
}
