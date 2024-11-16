using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PDMProiect;

public partial class ArtistByGenrePage : ContentPage

{
    public ObservableCollection<Artist> Artists { get; set; } = new ObservableCollection<Artist>();
    public ObservableCollection<Artist> FilteredArtists { get; set; } = new ObservableCollection<Artist>();

    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged(nameof(SearchText));
            FilterArtists(); 
        }
    }

    public ArtistByGenrePage(string genre)
    {
        InitializeComponent();
        BindingContext = this;
        LoadArtistsByGenre(genre);
    }
    private void FilterArtists()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            
            FilteredArtists.Clear();
            foreach (var artist in Artists)
            {
                FilteredArtists.Add(artist);
            }
        }
        else
        {
            var filtered = Artists.Where(a => a.name.ToLower().Contains(SearchText.ToLower()));

            FilteredArtists.Clear();
            foreach (var artist in filtered)
            {
                FilteredArtists.Add(artist);
            }
        }
    }
    private void LoadArtistsByGenre(string genre)
    {
        DaoArtist daoArtist = new DaoArtist();
        var artists = daoArtist.GetArtistsByGenre(genre);

        var uniqueArtists= artists.GroupBy(a => a.name)
                               .Select(g => g.First())
                               .ToList();


        Artists.Clear();
        foreach (var artist in artists)
        {
            Artists.Add(artist);
        }

        FilteredArtists.Clear();
        foreach (var artist in uniqueArtists)
        {
            FilteredArtists.Add(artist);
        }


    }
    private async void OnArtistSelected(object sender, EventArgs e)
    {
        var selectedArtist = (Artist)((Button)sender).BindingContext;
        await Navigation.PushAsync(new DiscoverArtistsPage(selectedArtist.id));
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