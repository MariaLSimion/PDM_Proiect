using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PDMProiect;

public partial class ArtistByGenrePage : ContentPage

{
    public ObservableCollection<Artist> Artists { get; set; } = new ObservableCollection<Artist>();
    public ArtistByGenrePage(string genre)
    {
        InitializeComponent();
        BindingContext = this;
        LoadArtistsByGenre(genre);
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


    }
    private async void OnArtistSelected(object sender, EventArgs e)
    {
        var selectedArtist = (Artist)((Button)sender).BindingContext;
        await Navigation.PushAsync(new DiscoverArtistsPage(selectedArtist.id));
    }


}