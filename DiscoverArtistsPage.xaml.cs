using System.Text;
using Microcharts;
using SkiaSharp;
using System.Linq;
using System.Diagnostics;

namespace PDMProiect;

public partial class DiscoverArtistsPage : ContentPage
{
    public Artist Artist { get; set; }
    public DiscoverArtistsPage(int artistId)
	{
		InitializeComponent();
        Artist artist = getArtistData(artistId);
        this.Artist = artist;
        BindingContext = Artist;
        CreatePopularityChart();
    }

    private Artist getArtistData(int artistId)
    {
        DaoArtist daoArtist = new DaoArtist();
        return daoArtist.GetArtistById(artistId);
    }
    private void OnPickerChanged(object sender, EventArgs e)
    {
        switch (InfoPicker.SelectedIndex)
        {
            case 0: 
                InfoLabel.Text = Artist.biography ?? "Biography not available.";
                break;
            case 1: 
                InfoLabel.Text = Artist.funfacts ?? "Fun Facts not available.";
                break;
            case 2: 
                if (!string.IsNullOrEmpty(Artist.socials))
                {
                    
                    InfoLabel.Text = string.Join(Environment.NewLine, Artist.socials.Split(' '));
                }
                else
                {
                    InfoLabel.Text = "Social links not available.";
                }
                break;
            default:
                InfoLabel.Text = "Select an option.";
                break;
        }
    }
    private void CreatePopularityChart()
    {
        var popularityData = Artist.popularity.Split(' ')
        .Select(value => int.Parse(value))
        .ToList();
        var months = new List<string>
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };
        if (popularityData.Count != 12)
        {
            InfoLabel.Text = "Popularity data is incomplete or incorrect.";
            return;
        }
        var chartEntries = new List<Microcharts.ChartEntry>();
        for (int i = 0; i < 12; i++)
        {
            chartEntries.Add(new Microcharts.ChartEntry(popularityData[i])
            {
                Label = months[i],
                ValueLabel = popularityData[i].ToString(),
                Color = SKColor.Parse("#9370DB"),
                TextColor = SKColor.Parse("#6A0C9D")
            });
        }

        var lineChart = new LineChart
        {
            Entries = chartEntries,
            LineMode = LineMode.Straight,
            LineSize = 4,
            PointMode = PointMode.Circle,
            PointSize = 8,
            BackgroundColor = SKColors.Transparent,
            LabelTextSize = 20,
            LabelOrientation = Orientation.Horizontal,
            ValueLabelOrientation = Orientation.Horizontal,
            MaxValue = 100,
            MinValue = 0
        };
        popularityChart.Chart = lineChart;
    }

    private void OnMerchStoreButtonClicked(object sender, EventArgs e)
    {
        if (Artist != null)
        {
            Navigation.PushAsync(new MerchStore());
        }

    }
}