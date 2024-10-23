namespace PDMProiect
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        // Funcție care se declanșează când se apasă butonul
        private async void OnConcertsButtonClicked(object sender, EventArgs e)
        {
            // Navighează către ConcertsCalendarPage
            await Navigation.PushAsync(new ConcertsCalendarPage());
        }
    }

}
