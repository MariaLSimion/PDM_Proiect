using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMProiect
{
    internal class DaoMerch
    {
        SQLiteConnection conn;

        public DaoMerch()
        {
            string cale = Path.Combine(FileSystem.AppDataDirectory, "merch.db");
            conn = new SQLiteConnection(cale);
        }

        public void RefreshDatabase()
        {
            conn.DropTable<Merch>();
            conn.CreateTable<Merch>();
        }

        public bool MerchExists(int id)
        {
            return conn.Table<Merch>()
                       .Any(m => m.id == id);
        }

        public int InsertAll(List<Merch> merchList)
        {
            int insertedCount = 0;
            foreach (var merch in merchList)
            {
                if (!MerchExists(merch.id))
                {
                    conn.Insert(merch);
                    insertedCount++;
                }
            }
            return insertedCount;
        }

        public List<Merch> GetMerchByArtist(string artist)
        {
            return conn.Table<Merch>()
           .Where(m => m.artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
           .ToList();
        }

        public List<Merch> GetMerchByType(string type)
        {
            return conn.Table<Merch>()
           .Where(m => m.type.Equals(type, StringComparison.OrdinalIgnoreCase))
           .ToList();
        }

        public List<Merch> GetMerchByDisponibility(string disponibility)
        {
            return conn.Table<Merch>()
           .Where(m => m.disponibility.Equals(disponibility, StringComparison.OrdinalIgnoreCase))
           .ToList();
        }

        public List<Merch> GetAllMerch()
        {
            return conn.Table<Merch>().ToList();
        }
        public List<string> GetAllUniqueArtists()
        {
            return conn.Table<Merch>()
                       .Select(m => m.artist)
                       .Distinct()
                       .ToList();
        }

        public List<string> GetAllUniqueTypes()
        {
            return conn.Table<Merch>()
                       .Select(m => m.type)
                       .Distinct()
                       .ToList();
        }

        public List<string> GetAllUniqueDisponibility()
        {
            return conn.Table<Merch>()
                       .Select(m => m.disponibility)
                       .Distinct()
                       .ToList();
        }
    }
}
