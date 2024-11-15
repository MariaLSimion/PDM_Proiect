namespace PDMProiect;

public partial class MerchStore : ContentPage
{
    private List<Merch> MerchItems = new List<Merch>(); 
    private List<Merch> FilteredMerch = new List<Merch> ();
    private DaoMerch DaoMerch;

    public MerchStore()
    {
        InitializeComponent();
        LoadMerchData();
        LoadFilterOptions();
    }

    private void LoadMerchData()
    {
        List<Merch> merchList = MerchService.getMerch();
        if (merchList != null && merchList.Any())
        {
            this.DaoMerch = new DaoMerch();
            DaoMerch.RefreshDatabase();
            int a = DaoMerch.InsertAll(merchList);
            this.MerchItems = DaoMerch.GetAllMerch();
            MerchCollectionView.ItemsSource = MerchItems;
        }
    }

    private void LoadFilterOptions()
    {        
        List<string> types = DaoMerch.GetAllUniqueTypes();
        List<string> artists = DaoMerch.GetAllUniqueArtists();
        List<string> disponibility = DaoMerch.GetAllUniqueDisponibility();
        TypeFilter.ItemsSource = types;
        ArtistFilter.ItemsSource = artists;
        AvailabilityFilter.ItemsSource = disponibility;
    }

    private void OnApplyFilters(object sender, EventArgs e)
    {
        string selectedType = TypeFilter.SelectedItem?.ToString() ?? "All";
        string selectedArtist = ArtistFilter.SelectedItem?.ToString() ?? "All";
        string selectedAvailability = AvailabilityFilter.SelectedItem?.ToString() ?? "All";

        FilteredMerch = MerchItems.Where(m =>
            (selectedType == "All" || m.type == selectedType) &&
            (selectedArtist == "All" || m.artist == selectedArtist) &&
            (selectedAvailability == "All" || m.disponibility == selectedAvailability)
        ).ToList();

        MerchCollectionView.ItemsSource = FilteredMerch;
    }

    private void OnResetFilters(object sender, EventArgs e)
    {
        TypeFilter.SelectedItem = "All";     
        ArtistFilter.SelectedItem = "All";    
        AvailabilityFilter.SelectedItem = "All";
        MerchCollectionView.ItemsSource = MerchItems;
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var selectedMerch = (Merch)button.CommandParameter;

        await DisplayAlert("Merch Details", $"Name: {selectedMerch.productName}\nPrice: {selectedMerch.price:C}\nDetails: {selectedMerch.details}", "Close");
    }
}