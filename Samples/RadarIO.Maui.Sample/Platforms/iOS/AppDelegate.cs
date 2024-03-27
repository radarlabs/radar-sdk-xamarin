using CoreLocation;
using Foundation;
using UIKit;

namespace RadarIO.Maui.Sample;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    private CLLocationManager locationManager = new CLLocationManager();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var res = base.FinishedLaunching(application, launchOptions);

        RequestLocationPermissions();

        return res;
    }

    private void RequestLocationPermissions()
    {
        switch (locationManager.AuthorizationStatus)
        {
            case CLAuthorizationStatus.NotDetermined:
                locationManager.RequestWhenInUseAuthorization();
                break;
            case CLAuthorizationStatus.AuthorizedWhenInUse:
                locationManager.RequestAlwaysAuthorization();
                break;
        };
    }
}

