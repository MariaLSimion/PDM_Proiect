namespace PDMProiect;

public partial class ConcertDetailsPage : ContentPage
{
    public ConcertDetailsPage(Concert concert)
    {
        InitializeComponent();
        BindingContext = concert; 
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