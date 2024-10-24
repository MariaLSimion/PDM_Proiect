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
                new Concert { NumeConcert = "Jazz Night", DataLocatie = "20 August 2024 - Cluj-Napoca", Artists = new List<string> { "Herbie Hancock", "Esperanza Spalding" } },
                new Concert { NumeConcert = "Pop Hits", DataLocatie = "30 August 2024 - Timisoara", Artists = new List<string> { "Dua Lipa", "Harry Styles" } },
                new Concert { NumeConcert = "Indie Vibes", DataLocatie = "5 Septembrie 2024 - Brasov", Artists = new List<string> { "Tame Impala", "Arctic Monkeys" } },
                new Concert { NumeConcert = "Electro Pulse", DataLocatie = "15 Septembrie 2024 - Iasi", Artists = new List<string> { "Deadmau5", "Calvin Harris" } },
                new Concert { NumeConcert = "Hip-Hop Revolution", DataLocatie = "25 Septembrie 2024 - Constanta", Artists = new List<string> { "Kendrick Lamar", "J. Cole" } },
                new Concert { NumeConcert = "Classical Evening", DataLocatie = "1 Octombrie 2024 - Sibiu", Artists = new List<string> { "Berlin Philharmonic Orchestra", "Lang Lang" } },
                new Concert { NumeConcert = "Reggae Chillout", DataLocatie = "10 Octombrie 2024 - Oradea", Artists = new List<string> { "Damian Marley", "Alborosie" } }
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
}

public class Concert : INotifyPropertyChanged
{
    public string NumeConcert { get; set; }
    public string DataLocatie { get; set; }
    public List<string> Artists { get; set; }

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

