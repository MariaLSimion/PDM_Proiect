using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PDMProiect
{
    [Table("album")]
    public class Album : INotifyPropertyChanged
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("album_name")]
        public string AlbumName { get; set; }

        [Column("artist_name")]
        public string ArtistName { get; set; }
        

        [Column("cover_image")]
        public string CoverImage { get; set; }

        [Column("songs")]
        public string SongsSerialized
        {
            get => string.Join(",", Songs); // converteste lista în string separat prin virgule
            set => Songs = value.Split(',').ToList(); // converteste stringul înapoi în listă
        }

        [Ignore]
        public List<string> Songs { get; set; } = new List<string>();
        //public List<string> Songs { get; set; }



        private bool isSongsVisible;

        [Ignore]
        public bool IsSongsVisible
        {
            get { return isSongsVisible; }
            set
            {
                if (isSongsVisible != value)
                {
                    isSongsVisible = value;
                    OnPropertyChanged(nameof(IsSongsVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
