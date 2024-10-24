using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PDMProiect;

public partial class ConcertsCalendarPage : ContentPage
{
    public ObservableCollection<Concert> Concerts { get; set; }
    public ObservableCollection<Concert> FilteredConcerts { get; set; }
    public Command ShowArtistsCommand { get; set; }

    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged(nameof(SearchText));
            FilterConcerts(); // Apeleaz? filtrarea la schimbarea textului
        }
    }

    public ConcertsCalendarPage()
    {
        InitializeComponent();

        // Definirea concertelor
        Concerts = new ObservableCollection<Concert>
            {
                new Concert { NumeConcert = "Jazz Night", DataLocatie = "20 August 2024 - Cluj-Napoca", Artists = new List<string> { "Herbie Hancock", "Esperanza Spalding" }, ImageUrl = "https://e7.pngegg.com/pngimages/810/412/png-clipart-new-orleans-jazz-heritage-festival-jazzfest-berlin-nice-jazz-festival-montreal-international-jazz-festival-music-festival-design-text-orange-thumbnail.png" },
                new Concert { NumeConcert = "Pop Hits", DataLocatie = "30 August 2024 - Timisoara", Artists = new List<string> { "Dua Lipa", "Harry Styles" }, ImageUrl = "https://w7.pngwing.com/pngs/450/793/png-transparent-pop-music-poster-pop-art-singing-creative-leaflets-game-painted-hand-thumbnail.png" },
                new Concert { NumeConcert = "Indie Vibes", DataLocatie = "5 Septembrie 2024 - Brasov", Artists = new List<string> { "Tame Impala", "Arctic Monkeys" }, ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNryLYi2Tnx6qmv4P1np1qL7txQeD7OiW27Q&s" },
                new Concert { NumeConcert = "Electro Pulse", DataLocatie = "15 Septembrie 2024 - Iasi", Artists = new List<string> { "Deadmau5", "Calvin Harris" }, ImageUrl = "https://images.squarespace-cdn.com/content/v1/5edf06dcd417cb4da8ba2163/3cf0b903-c04e-43fb-ba38-9ba01cb1de31/s2o+4.png" },
                new Concert { NumeConcert = "Hip-Hop Revolution", DataLocatie = "25 Septembrie 2024 - Constanta", Artists = new List<string> { "Kendrick Lamar", "J. Cole" }, ImageUrl = "https://e7.pngegg.com/pngimages/59/445/png-clipart-hip-hop-hip-hop-music-hip-hop-dance-hip-hop-miscellaneous-text-thumbnail.png" },
                new Concert { NumeConcert = "Classical Evening", DataLocatie = "1 Octombrie 2024 - Sibiu", Artists = new List<string> { "Berlin Philharmonic Orchestra", "Lang Lang" }, ImageUrl = "https://e7.pngegg.com/pngimages/138/411/png-clipart-youth-orchestra-opera-house-classical-music-concert-violin-stage-classical-music-thumbnail.png" },
                new Concert { NumeConcert = "Reggae Chillout", DataLocatie = "10 Octombrie 2024 - Oradea", Artists = new List<string> { "Damian Marley", "Alborosie" }, ImageUrl = "https://w7.pngwing.com/pngs/949/884/png-transparent-reggae-rastafari-rock-happy-birthday-vector-images-fictional-character-cartoon-characters-thumbnail.png" }
            };

        // Initializeaz? lista filtrat? cu toate concertele
        FilteredConcerts = new ObservableCollection<Concert>(Concerts);

        // Comanda pentru a afi?a arti?tii
        ShowArtistsCommand = new Command<List<string>>(ShowArtists);

        // Seteaz? BindingContext-ul
        BindingContext = this;
    }

    private void FilterConcerts()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            // Când c?utarea este goal?, arat? toate concertele
            FilteredConcerts.Clear();
            foreach (var concert in Concerts)
            {
                FilteredConcerts.Add(concert);
            }
        }
        else
        {
            var filtered = Concerts.Where(c => c.NumeConcert.ToLower().Contains(SearchText.ToLower()) ||
                                                c.Artists.Any(a => a.ToLower().Contains(SearchText.ToLower())));

            FilteredConcerts.Clear();
            foreach (var concert in filtered)
            {
                FilteredConcerts.Add(concert);
            }
        }
    }

    private async void ShowArtists(List<string> artists)
    {
        // Afi?eaz? lista arti?tilor într-un dialog
        string artistsList = string.Join(", ", artists);
        await DisplayAlert("Artisti", artistsList, "OK");
    }

    private async void OnConcertTapped(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var concert = (Concert)label.BindingContext; // Ob?ine concertul asociat cu label-ul
        await Navigation.PushAsync(new ConcertDetailsPage(concert)); // Navigheaz? la pagina detaliilor concertului
    }

}

public class Concert : INotifyPropertyChanged
{
    public string NumeConcert { get; set; }
    public string DataLocatie { get; set; }
    public List<string> Artists { get; set; }

    public string ImageUrl { get; set; }

    private bool isArtistsVisible;
    public bool IsArtistsVisible
    {
        get { return isArtistsVisible; }
        set
        {
            if (isArtistsVisible != value)
            {
                isArtistsVisible = value;
                OnPropertyChanged(nameof(IsArtistsVisible));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

