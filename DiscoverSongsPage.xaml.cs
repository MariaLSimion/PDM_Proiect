using System.Collections.ObjectModel;

namespace PDMProiect;

public partial class DiscoverSongsPage : ContentPage
{
    public ObservableCollection<Song> RandomSongs { get; set; }
    public ObservableCollection<Album> RandomAlbums { get; set; }
    public Command<Song> SelectSongCommand { get; set; }
    public Command<Album> SelectAlbumCommand { get; set; }
    public Command RestartCommand { get; set; }

    private Dictionary<Song, Album> songAlbumMatches;
    private Dictionary<Song, Album> currentMatches;

    private Song selectedSong;
    private Album selectedAlbum;

    public DiscoverSongsPage()
    {
        InitializeComponent();

        // Generate random albums
        var albums = LoadAndInsertAlbums();
        RandomAlbums = new ObservableCollection<Album>(albums.OrderBy(a => Guid.NewGuid()).Take(5));

        // For each album, pick one random song
        RandomSongs = new ObservableCollection<Song>(
            RandomAlbums.Select(a => a.Songs.OrderBy(s => Guid.NewGuid()).FirstOrDefault()).Where(s => s != null));

        // Initialize matching
        songAlbumMatches = RandomSongs.ToDictionary(song => song, song => RandomAlbums.First(album => album.Songs.Contains(song)));
        currentMatches = new Dictionary<Song, Album>();

        SelectSongCommand = new Command<Song>(song => OnSelectSong(song));
        SelectAlbumCommand = new Command<Album>(album => OnSelectAlbum(album));
        RestartCommand = new Command(RestartGame);

        BindingContext = this;
    }

    private void OnSelectSong(Song song)
    {
        if (currentMatches.ContainsKey(song))
        {
            // Song already matched, do nothing
            return;
        }

        if (selectedSong != null)
        {
            // Deselect previous song
            selectedSong.IsSelected = false;
        }

        if (selectedSong == song)
        {
            // Deselect current song
            selectedSong.IsSelected = false;
            selectedSong = null;
        }
        else
        {
            // Select new song
            selectedSong = song;
            selectedSong.IsSelected = true;
        }

        CheckMatch();
    }

    private void OnSelectAlbum(Album album)
    {
        if (currentMatches.ContainsValue(album))
        {
            // Album already matched, do nothing
            return;
        }

        if (selectedAlbum != null)
        {
            // Deselect previous album
            selectedAlbum.IsSelected = false;
        }

        if (selectedAlbum == album)
        {
            // Deselect current album
            selectedAlbum.IsSelected = false;
            selectedAlbum = null;
        }
        else
        {
            // Select new album
            selectedAlbum = album;
            selectedAlbum.IsSelected = true;
        }

        CheckMatch();
    }

    private void CheckMatch()
    {
        if (selectedSong != null && selectedAlbum != null)
        {
            if (songAlbumMatches[selectedSong] == selectedAlbum)
            {
                // Correct match
                selectedSong.IsMatched = true;
                selectedAlbum.IsMatched = true;
                currentMatches[selectedSong] = selectedAlbum;
                selectedSong = null;
                selectedAlbum = null;

                // Check if all songs are matched
                if (currentMatches.Count == RandomSongs.Count)
                {
                    DisplayAlert("Felicitări", "Ai matchuit toate melodiile corect!", "OK");
                }
            }
            else
            {
                // Incorrect match, deselect both
                selectedSong.IsSelected = false;
                selectedSong = null;
                selectedAlbum.IsSelected = false;
                selectedAlbum = null;
            }
        }
    }

    private void RestartGame()
    {
        // Reinitialize game
        var albums = LoadAndInsertAlbums();
        RandomAlbums = new ObservableCollection<Album>(albums.OrderBy(a => Guid.NewGuid()).Take(5));

        // For each album, pick one random song
        RandomSongs = new ObservableCollection<Song>(
            RandomAlbums.Select(a => a.Songs.OrderBy(s => Guid.NewGuid()).FirstOrDefault()).Where(s => s != null));

        // Initialize matching
        songAlbumMatches = RandomSongs.ToDictionary(song => song, song => RandomAlbums.First(album => album.Songs.Contains(song)));
        currentMatches.Clear();
        selectedSong = null;
        selectedAlbum = null;

        // Reset IsSelected and IsMatched properties
        foreach (var song in RandomSongs)
        {
            song.IsSelected = false;
            song.IsMatched = false;
        }
        foreach (var album in RandomAlbums)
        {
            album.IsSelected = false;
            album.IsMatched = false;
        }

        OnPropertyChanged(nameof(RandomAlbums));
        OnPropertyChanged(nameof(RandomSongs));
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
