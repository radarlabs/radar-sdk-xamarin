using static RadarIO.Sample.Demo;

namespace RadarIO.Maui.Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Initialize(new RadarSDKImpl());
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {
            await Permissions.RequestAsync<Permissions.LocationAlways>();

            var (status, loc, _, _) = await Test();
            CounterBtn.Text = status == RadarStatus.Success
                ? $"TrackOnce Success! {loc.Latitude} {loc.Longitude}"
                : $"TrackOnce Failed! {status}";
        }
        catch (Exception ex)
        {
            CounterBtn.Text = $"TrackOnce Failed! {ex}";
        }
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}


