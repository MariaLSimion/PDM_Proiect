using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace PDMProiect;

public partial class DiscoverAlbumsPage : ContentPage
{
    public List<Album?> Albums { get; set; }
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
            FilterAlbums(); // Filtrarea se activeaz? când se schimb? textul de c?utare
        }
    }


    public DiscoverAlbumsPage()
	{
		InitializeComponent();

        // Definirea albumelor
        //Albums = new ObservableCollection<Album>
        //    {
        //        new Album { AlbumName = "Bad", ArtistName = "Michael Jackson", Songs = new List<string> { "Bad", "Smooth Criminal", "Man in the Mirror" }, CoverImage = "https://upload.wikimedia.org/wikipedia/en/5/51/Michael_Jackson_-_Bad.png" },
        //        new Album { AlbumName = "Midnights", ArtistName = "Taylor Swift", Songs = new List<string> { "Lavender Haze", "Anti-Hero" }, CoverImage = "https://www.eriereader.com/image/scale/auto/auto/articles/547338_taylor-swift-copy.jpg" },
        //        new Album { AlbumName = "Harry's House", ArtistName = "Harry Styles", Songs = new List<string> { "Late Night Talking", "As It Was" }, CoverImage = "https://upload.wikimedia.org/wikipedia/en/d/d5/Harry_Styles_-_Harry%27s_House.png" },
        //        new Album { AlbumName = "Be", ArtistName = "BTS", Songs = new List<string> { "Blue & Grey", "Dynamite", "Stay" }, CoverImage = "https://upload.wikimedia.org/wikipedia/en/a/a2/BTS_-_Be_Cover.png" },
        //        // Adaug? alte albume aici
        //    };
        Albums = LoadAndInsertAlbums();

        // Comanda pentru a alterna vizibilitatea melodiilor
        ToggleSongsVisibilityCommand = new Command<Album>(ToggleSongsVisibility);

        // Comanda pentru a reda o melodie
        PlaySongCommand = new Command<string>(PlaySong);

        // Initializeaz? lista filtrat? cu toate albumele
        FilteredAlbums = new ObservableCollection<Album>(Albums);

        // Comanda pentru a afi?a melodiile din album
        ShowSongsCommand = new Command<List<string>>(ShowSongs);

        // Seteaz? BindingContext-ul
        BindingContext = this;
    }

    public void FilterAlbums()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            // Când c?utarea este goal?, afi?ez toate albumele
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
                                              a.Songs.Any(s => s.ToLower().Contains(SearchText.ToLower())));

            FilteredAlbums.Clear();
            foreach (var album in filtered)
            {
                FilteredAlbums.Add(album);
            }
        }
    }

    private async void ShowSongs(List<string> songs)
    {
        // Afi?eaz? lista de melodii într-un dialog
        string songsList = string.Join(", ", songs);
        await DisplayAlert("Melodii", songsList, "OK");
    }

    private void ToggleSongsVisibility(Album album)
    {
        // Alternare vizibilitate melodii
        album.IsSongsVisible = !album.IsSongsVisible;
    }

    private async void PlaySong(string songTitle)
    {
        // afi?ez un mesaj cu titlul melodiei
        await DisplayAlert("Redare melodia", $"Redare: {songTitle}", "OK");
    }
    private List<Album?> LoadAndInsertAlbums()
    {
        var albums = AlbumService.getAlbums();
        if (albums != null && albums.Any())
        {
            DaoAlbum daoAlbum = new DaoAlbum();
            daoAlbum.InsertAll(albums);
        }
        return albums;
    }
}
