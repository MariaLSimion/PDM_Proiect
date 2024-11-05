using System.Collections.ObjectModel;

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

        Artists.Clear();
        foreach (var artist in artists)
        {
            Artists.Add(artist);
        }
    }
}