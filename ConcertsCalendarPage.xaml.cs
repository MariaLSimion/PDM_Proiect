namespace PDMProiect;

public partial class ConcertsCalendarPage : ContentPage
{
    public ConcertsCalendarPage()
    {
        InitializeComponent();

        // Lista de concerte
        var concerte = new List<Concert>
        {
            new Concert { NumeConcert = "Rock Fest", DataLocatie = "12 August 2024 - Bucuresti", RezervaBileteCommand = new Command(() => Rezerva("Rock Fest")) },
            new Concert { NumeConcert = "Jazz Night", DataLocatie = "20 August 2024 - Cluj-Napoca", RezervaBileteCommand = new Command(() => Rezerva("Jazz Night")) },
            new Concert { NumeConcert = "Pop Hits", DataLocatie = "30 August 2024 - Timisoara", RezervaBileteCommand = new Command(() => Rezerva("Pop Hits")) },
            new Concert { NumeConcert = "Indie Vibes", DataLocatie = "5 Septembrie 2024 - Brasov", RezervaBileteCommand = new Command(() => Rezerva("Indie Vibes")) },
            new Concert { NumeConcert = "Electro Pulse", DataLocatie = "15 Septembrie 2024 - Iasi", RezervaBileteCommand = new Command(() => Rezerva("Electro Pulse")) },
            new Concert { NumeConcert = "Hip-Hop Revolution", DataLocatie = "25 Septembrie 2024 - Constanta", RezervaBileteCommand = new Command(() => Rezerva("Hip-Hop Revolution")) },
            new Concert { NumeConcert = "Classical Evening", DataLocatie = "1 Octombrie 2024 - Sibiu", RezervaBileteCommand = new Command(() => Rezerva("Classical Evening")) },
            new Concert { NumeConcert = "Reggae Chillout", DataLocatie = "10 Octombrie 2024 - Oradea", RezervaBileteCommand = new Command(() => Rezerva("Reggae Chillout")) }
        };

        // Seteaz? sursa pentru ListView
        ConcertList.ItemsSource = concerte;
    }

    private async void Rezerva(string numeConcert)
    {
        await DisplayAlert("Rezervare", $"Ai rezervat bilete pentru {numeConcert}!", "OK");
    }
}

// Clasa Concert
public class Concert
{
    public string NumeConcert { get; set; }
    public string DataLocatie { get; set; }
    public Command RezervaBileteCommand { get; set; }
}
