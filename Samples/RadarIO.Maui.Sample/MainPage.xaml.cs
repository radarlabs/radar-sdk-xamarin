using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using static RadarIO.Sample.Demo;

namespace RadarIO.Maui.Sample;

public partial class MainPage : ContentPage
{
    private bool isTracking;
    private bool isTrip;

    public MainPage()
    {
        InitializeComponent();
        TrackOnceBtn.IsVisible = false;
        TrackVerifiedBtn.IsVisible = false;
        TrackingResponsiveBtn.IsVisible = false;
        TrackingContinuousBtn.IsVisible = false;
        TripBtn.IsVisible = false;
        GeofenceIdEntry.IsVisible = false;
        GeofenceTagEntry.IsVisible = false;
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        Initialize(new RadarSDKImpl(), ((Entry)sender).Text);
        RadarKeyEntry.IsVisible = false;
        TrackOnceBtn.IsVisible = true;
        TrackVerifiedBtn.IsVisible = true;
        TrackingResponsiveBtn.IsVisible = true;
        TrackingContinuousBtn.IsVisible = true;
        TripBtn.IsVisible = true;
        GeofenceIdEntry.IsVisible = true;
        GeofenceTagEntry.IsVisible = true;
    }

    private async void OnTrackOnceClicked(object sender, EventArgs e)
    {
        try
        {
            TrackOnceBtn.Text = "Testing TrackOnce...";
            TrackOnceBtn.IsEnabled = false;

            await Permissions.RequestAsync<Permissions.LocationAlways>();

            // await Test();
            var (status, loc, _, _) = await TrackOnce();
            TrackOnceBtn.Text = status == RadarStatus.Success
                ? $"TrackOnce Success! {loc.Latitude:#0.0000} {loc.Longitude:#0.0000}"
                : $"TrackOnce Failed! {status}";
            TrackOnceBtn.IsEnabled = true;
        }
        catch (Exception ex)
        {
            TrackOnceBtn.Text = $"TrackOnce Failed!";
            await ToastError(ex);
        }
    }

    private async void OnTrackVerifiedClicked(object sender, EventArgs e)
    {
        try
        {
            TrackVerifiedBtn.Text = "Testing TrackVerified...";
            TrackVerifiedBtn.IsEnabled = false;

            await Permissions.RequestAsync<Permissions.LocationAlways>();

            var (status, token) = await TrackVerified();
            TrackVerifiedBtn.Text = status == RadarStatus.Success
                ? $"TrackVerified Success! {token}"
                : $"TrackVerified Failed! {status}";
            TrackVerifiedBtn.IsEnabled = true;
        }
        catch (Exception ex)
        {
            TrackVerifiedBtn.Text = $"TrackVerified Failed!";
            await ToastError(ex);
        }
    }

    private async void OnTrackingResponsiveClicked(object sender, EventArgs e)
    {
        if (!isTracking)
        {
            try
            {
                TrackingResponsiveBtn.IsEnabled = false;
                TrackingContinuousBtn.IsEnabled = false;

                await Permissions.RequestAsync<Permissions.LocationAlways>();

                StartTrackingResponsive();
                TrackingResponsiveBtn.Text = "Stop tracking";
                TrackingResponsiveBtn.IsEnabled = true;
                isTracking = true;
            }
            catch (Exception ex)
            {
                TrackingResponsiveBtn.Text = $"StartTracking Failed!";
                TrackingResponsiveBtn.IsEnabled = true;
                TrackingContinuousBtn.IsEnabled = true;
                await ToastError(ex);
            }
        }
        else
        {
            try
            {
                TrackingResponsiveBtn.IsEnabled = false;
                TrackingContinuousBtn.IsEnabled = false;

                StopTracking();
                TrackingResponsiveBtn.Text = "Start tracking responsive";
                TrackingResponsiveBtn.IsEnabled = true;
                TrackingContinuousBtn.IsEnabled = true;
                isTracking = false;
            }
            catch (Exception ex)
            {
                TrackingResponsiveBtn.Text = $"StopTracking Failed!";
                TrackingResponsiveBtn.IsEnabled = true;
                TrackingContinuousBtn.IsEnabled = true;
                await ToastError(ex);
            }
        }
    }

    private async void OnTrackingContinuousClicked(object sender, EventArgs e)
    {
        if (!isTracking)
        {
            try
            {
                TrackingContinuousBtn.IsEnabled = false;
                TrackingResponsiveBtn.IsEnabled = false;

                await Permissions.RequestAsync<Permissions.LocationAlways>();

                StartTrackingContinuous();
                TrackingContinuousBtn.Text = "Stop tracking";
                TrackingContinuousBtn.IsEnabled = true;
                isTracking = true;
            }
            catch (Exception ex)
            {
                TrackingContinuousBtn.Text = $"StartTracking Failed!";
                TrackingContinuousBtn.IsEnabled = true;
                TrackingResponsiveBtn.IsEnabled = true;
                await ToastError(ex);
            }
        }
        else
        {
            try
            {
                TrackingContinuousBtn.IsEnabled = false;
                TrackingResponsiveBtn.IsEnabled = false;

                StopTracking();
                TrackingContinuousBtn.Text = "Start tracking continuous";
                TrackingContinuousBtn.IsEnabled = true;
                TrackingResponsiveBtn.IsEnabled = true;
                isTracking = false;
            }
            catch (Exception ex)
            {
                TrackingContinuousBtn.Text = $"StopTracking Failed!";
                TrackingContinuousBtn.IsEnabled = true;
                TrackingResponsiveBtn.IsEnabled = true;
                await ToastError(ex);
            }
        }
    }

    private async void OnTripClicked(object sender, EventArgs e)
    {
        if (!isTrip)
        {
            try
            {
                TripBtn.IsEnabled = false;
                GeofenceIdEntry.IsEnabled = false;
                GeofenceTagEntry.IsEnabled = false;

                await Permissions.RequestAsync<Permissions.LocationAlways>();

                StartTrip(GeofenceIdEntry.Text, GeofenceTagEntry.Text);
                TripBtn.Text = "Stop trip";
                TripBtn.IsEnabled = true;
                isTrip = true;
            }
            catch (Exception ex)
            {
                TripBtn.Text = $"StartTrip Failed!";
                TripBtn.IsEnabled = true;
                await ToastError(ex);
            }
        }
        else
        {
            try
            {
                TripBtn.IsEnabled = false;

                await StopTrip();
                TripBtn.Text = "Start trip";
                TripBtn.IsEnabled = true;
                GeofenceIdEntry.IsEnabled = true;
                GeofenceTagEntry.IsEnabled = true;
                isTrip = false;
            }
            catch (Exception ex)
            {
                TripBtn.Text = $"StopTrip Failed!";
                TripBtn.IsEnabled = true;
                await ToastError(ex);
            }
        }
    }

    private async Task ToastError(Exception ex)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(ex.ToString(), duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}
