using System.Collections.ObjectModel;

namespace PDMProiect
{
    public partial class MainPage : ContentPage
    {
       public ObservableCollection<SpotifyTopSong> Songs { get; set; } = new ObservableCollection<SpotifyTopSong>();
        public ObservableCollection<SpotifyTopArtist> Artists { get; set; } = new ObservableCollection<SpotifyTopArtist>();

        public ObservableCollection<string> Genres { get; set; } = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

             LoadSongs();
            LoadArtists();
            LoadAndInsertArtists();
            LoadGenres();
           
        }
        private void LoadAndInsertArtists()
        {
            var artists = ArtistService.getArtists();
            if (artists != null && artists.Any())
            {
                DaoArtist daoArtist = new DaoArtist();
                daoArtist.InsertAll(artists);
            }
        }
        private void LoadGenres()
        {
            DaoArtist daoArtist= new DaoArtist();
            var uniqueGenres= daoArtist.GetUniqueGenres();

            Genres.Clear();
            foreach (var genre in uniqueGenres) { 
                Genres.Add(genre);
            }

        }
        private void LoadSongs()

        {
            Songs.Clear();
            var topSongs = SpotifySongService.GetTopSongs();
            foreach (var song in topSongs)
            {
                Songs.Add(song);
            }
        }
        private void LoadArtists()
        {
            Artists.Clear();
            var topArtists = SpotifyArtistService.GetTopArtists();
            foreach(var artist in topArtists)
            {
                Artists.Add(artist); 
            }
        }

        private void OnArtistByGenresBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArtistByGenrePage());

        }

        private void OnDiscoverArtistsBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ArtistByGenrePage());
        }

        private void OnConcertsCalendarBtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConcertsCalendarPage());
        }
        private void OnConcertDetailsBtnClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ConcertDetailsPage());
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

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedItem = (string)picker.SelectedItem;

            // Handle the selection change
            if (selectedItem == "Top 10 Spotify Artists")
            {
                // Load top 10 artists logic here
                SpotifyCollectionView.IsVisible = false;
                SpotifyArtistsCollectionView.IsVisible = true;

            }
            else if (selectedItem == "Top 10 Spotify Songs")

            {
                // Load top 10 songs logic here
                SpotifyArtistsCollectionView.IsVisible = false;
                SpotifyCollectionView.IsVisible = true;
            }
        }

    }



}
