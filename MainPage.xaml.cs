namespace PDMProiect
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
          //  BindingContext = new MainPageViewModel();
        }

        //// Funcție care se declanșează când se apasă butonul
        private async void OnConcertsButtonClicked(object sender, EventArgs e)
        {
            //Navighează către ConcertsCalendarPage
            await Navigation.PushAsync(new ConcertsCalendarPage());
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
    }

    

}
