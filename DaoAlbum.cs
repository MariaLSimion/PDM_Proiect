using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            conn.CreateTable<Song>();
        }

        public bool AlbumExists(string name, int id)
        {
            return conn.Table<Album>()
                       .Any(a => a.AlbumName == name && a.Id == id);
        }

        public int InsertAll(List<Album> albums)
        {
            int insertedCount = 0;
            int albumId = 0;
            int songId = 0;
            foreach (var album in albums)
            {
                if (!AlbumExists(album.AlbumName, album.Id))
                {
                    conn.Insert(album);

                    foreach (var song in album.Songs)
                    {
                        song.AlbumId = album.Id; // AlbumId este acum setat corect
                        conn.Insert(song);
                     
                    }
                    insertedCount++;
                }
            }
            return insertedCount;
        }

        public List<Album> GetAllAlbumsWithSongs()
        {
            var albums = conn.Table<Album>().ToList();
            foreach (var album in albums)
            {
                album.Songs = conn.Table<Song>().Where(s => s.AlbumId == album.Id).ToList();
            }
            return albums;
        }


    }
}
