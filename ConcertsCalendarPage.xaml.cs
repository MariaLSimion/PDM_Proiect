using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PDMProiect;

public partial class ConcertsCalendarPage : ContentPage
{
    public List<Concert?> Concerts { get; set; }
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
        Concerts = LoadAndInsertConcerts();

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

    private List<Concert?> LoadAndInsertConcerts()
    {
        var concerts = ConcertService.getConcerts();
        if (concerts != null && concerts.Any())
        {
            DaoConcert daoAlbum = new DaoConcert();
            daoAlbum.InsertAll(concerts);
        }
        return concerts;
    }

}

