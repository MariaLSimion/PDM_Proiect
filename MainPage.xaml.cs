﻿using System.Collections.ObjectModel;

namespace PDMProiect
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<SpotifyTopSong> Songs { get; set; } = new ObservableCollection<SpotifyTopSong>();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            LoadSongs();
          
        }
        private void LoadSongs()
        {
            var topSongs = SpotifySongService.GetTopSongs();
            foreach (var song in topSongs)
            {
                Songs.Add(song);
            }
        }

        //// Funcție care se declanșează când se apasă butonul
        //private async void OnConcertsButtonClicked(object sender, EventArgs e)
        //{
        //    //Navighează către ConcertsCalendarPage
        //    await Navigation.PushAsync(new ConcertsCalendarPage());
        //}
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
    }

    

}
