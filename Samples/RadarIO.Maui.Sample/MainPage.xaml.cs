using RadarIO.Xamarin;
using static RadarIO.Xamarin.RadarSingleton;

namespace RadarIO.Maui.Sample;

public partial class MainPage : ContentPage
{
    private const string RADAR_KEY = "prj_test_pk_8d7149cfe4a0fa5e5bb6a440a47a978995447ffc";

    public MainPage()
    {
        InitializeComponent();
        Initialize(new RadarSDKImpl());
        Radar.Initialize(RADAR_KEY);
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {
            await Permissions.RequestAsync<Permissions.LocationAlways>();

            var (status, loc, _, _) = await Radar.TrackOnce();
            CounterBtn.Text = status == Xamarin.RadarStatus.Success
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


