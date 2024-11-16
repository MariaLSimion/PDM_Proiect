using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PDMProiect
{
    public partial class DiscoverAlbumsPage : ContentPage
    {
        public List<Album> Albums { get; set; }
        public ObservableCollection<Album> FilteredAlbums { get; set; }
        public Command ShowSongsCommand { get; set; }
        public Command<Album> ToggleSongsVisibilityCommand { get; set; }
        public Command<string> PlaySongCommand { get; set; }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterAlbums();
            }
        }

        public DiscoverAlbumsPage()
        {
            InitializeComponent();

            Albums = LoadAndInsertAlbums();

            ToggleSongsVisibilityCommand = new Command<Album>(ToggleSongsVisibility);

            PlaySongCommand = new Command<string>(PlaySong);


            FilteredAlbums = new ObservableCollection<Album>(Albums);


            BindingContext = this;
        }

        private void FilterAlbums()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredAlbums.Clear();
                foreach (var album in Albums)
                {
                    FilteredAlbums.Add(album);
                }
            }
            else
            {
                var filtered = Albums.Where(a => a.AlbumName.ToLower().Contains(SearchText.ToLower()) ||
                                                  a.ArtistName.ToLower().Contains(SearchText.ToLower()) ||
                                                  a.Songs.Any(s => s.Title.ToLower().Contains(SearchText.ToLower())));

                FilteredAlbums.Clear();
                foreach (var album in filtered)
                {
                    FilteredAlbums.Add(album);
                }
            }
        }

        private void ToggleSongsVisibility(Album album)
        {
            album.IsSongsVisible = !album.IsSongsVisible;
        }

        private async void PlaySong(string songUrl)
        {
            if (Uri.IsWellFormedUriString(songUrl, UriKind.Absolute))
            {
                await Launcher.OpenAsync(new Uri(songUrl));
            }
            else
            {
                await DisplayAlert("Eroare", "URL invalid.", "OK");
            }
        }




        private List<Album> LoadAndInsertAlbums()
        {
            var albums = AlbumService.getAlbums();
            if (albums != null && albums.Any())
            {
                DaoAlbum daoAlbum = new DaoAlbum();
                daoAlbum.InsertAll(albums);
            }
            return albums;
        }

        private void OnDiscoverArtistsBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void OnConcertsCalendarBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConcertsCalendarPage());
        }

        private void OnDiscoverAlbumsBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DiscoverAlbumsPage());
        }
        private void OnDiscoverSongsBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DiscoverSongsPage());
        }
        private void OnMerchStoreBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MerchStore());
        }


    }

}
