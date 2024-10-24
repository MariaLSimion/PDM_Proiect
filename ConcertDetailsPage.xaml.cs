namespace PDMProiect;

public partial class ConcertDetailsPage : ContentPage
{
    public ConcertDetailsPage(Concert concert)
    {
        InitializeComponent();
        BindingContext = concert; // Seteaz? BindingContext-ul la concertul ales
    }
}