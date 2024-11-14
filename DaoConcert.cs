using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PDMProiect
{
    public class DaoConcert
    {
        SQLiteConnection conn;

        public DaoConcert()
        {
            string cale = Path.Combine(FileSystem.AppDataDirectory, "concert.db");
            conn = new SQLiteConnection(cale);
            conn.CreateTable<Concert>();
        }
        public bool ConcertExists(string name, int id)
        {
            return conn.Table<Concert>()
                       .Any(a => a.NumeConcert == name && a.Id == id);
        }
        public int InsertAll(List<Concert> concerts)
        {
            int insertedCount = 0;
            foreach (var concert in concerts)
            {
                if (!ConcertExists(concert.NumeConcert, concert.Id))
                {
                    conn.Insert(concert);
                    insertedCount++;
                }
            }
            return insertedCount;
        }
    }
}
