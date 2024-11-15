using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PDMProiect
{
    [Table("concert")]
    public class Concert : INotifyPropertyChanged
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("nume_concert")]
        public string NumeConcert { get; set; }
        [Column("data_locatie")]
        public string DataLocatie { get; set; }

        [Column("artisti")]
        public string ArtistsSerialized
        {
            get => string.Join(",", Artists); // converteste lista în string separat prin virgule
            set => Artists = value.Split(',').ToList(); // converteste stringul înapoi în listă
        }

        [Ignore]
        public List<string> Artists { get; set; } = new List<string>();

        [Column("urlImagine")]
        public string ImageUrl { get; set; }

        [Column("pret_bilet")]
        public string PretBilet { get; set; }

        private bool isArtistsVisible;
        public bool IsArtistsVisible
        {
            get { return isArtistsVisible; }
            set
            {
                if (isArtistsVisible != value)
                {
                    isArtistsVisible = value;
                    OnPropertyChanged(nameof(IsArtistsVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
