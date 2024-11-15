using SQLite;
using System.ComponentModel;

namespace PDMProiect
{
    [Table("song")]
    public class Song : INotifyPropertyChanged
    {

        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("album_id")]
        public int AlbumId { get; set; }

        private bool isSelected;

        [Ignore]
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        private bool isMatched;

        [Ignore]
        public bool IsMatched
        {
            get { return isMatched; }
            set
            {
                if (isMatched != value)
                {
                    isMatched = value;
                    OnPropertyChanged(nameof(IsMatched));
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}
