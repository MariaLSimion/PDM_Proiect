using System;
using System.Collections.Generic;
using System.ComponentModel;
using SQLite;

namespace PDMProiect
{
    [Table("album")]
    public class Album : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("album_name")]
        public string AlbumName { get; set; }

        [Column("artist_name")]
        public string ArtistName { get; set; }

        [Column("cover_image")]
        public string CoverImage { get; set; }

        [Ignore]
        public List<Song> Songs { get; set; } = new List<Song>();

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
