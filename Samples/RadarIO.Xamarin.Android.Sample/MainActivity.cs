using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Content.PM;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;

using static RadarIO.Xamarin.RadarSingleton;

namespace RadarIO.Xamarin.Android.Sample
{
    using Xamarin = global::Xamarin;
    using global::Android;
    using System.Threading.Tasks;
    using System.Linq;
    using RadarIO.Xamarin.Shared.Sample;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Demo.Initialize(new RadarSDKImpl());
            RadarTrackingOptions trackingOptions = new RadarTrackingOptions();
            //trackingOptions.ForegroundServiceEnabled = true;
            // set the foreground service options
            //RadarTrackingOptionsForegroundService foregroundOptions = null;
            //Radar.SetForegroundServiceOptions(foregroundOptions);
            // start tracking
            Radar.StartTracking(trackingOptions);
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += async (_, __) => await FabOnClick();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessBackgroundLocation }, 0);
            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 0);
            }

            //Radar.Log += args =>
            //{
            //    //var (loc, user) = args;
            //};

            //Radar.Initialize(RADAR_KEY);

            //Radar.StartTrip(new RadarTripOptions
            //{
            //    ExternalId = "999",
            //    DestinationGeofenceTag = "tag",
            //    DestinationGeofenceExternalId = "id",
            //    Mode = RadarRouteMode.Truck,
            //    Metadata = new JSONObject
            //        {
            //            { "bing", "bong" },
            //            { "ding", "dong" },
            //            { "int", 5 },
            //            { "bool", true }
            //        },
            //});
            //Radar.StopTracking();
            //var trackingOptions = RadarTrackingOptions.Responsive;
            //trackingOptions.ForegroundService = new RadarTrackingOptionsForegroundService
            //{
            //    Id = 123,
            //    Title = "title",
            //    Text = "text",
            //    Activity = "RadarIO.Xamarin.Android.Sample.MainActivity", 
            //};
            //Radar.StartTracking(trackingOptions);
            //Radar.MockTracking(
            //    new Location { Latitude = 40.78382, Longitude = -73.97536 },
            //    new Location { Latitude = 40.70390, Longitude = -73.98670 },
            //    RadarRouteMode.Car,
            //    10,
            //    3,
            //    _ => { });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private bool toggle = false;
        private async Task FabOnClick()
        {
            var ResultView = FindViewById<AppCompatTextView>(Resource.Id.result_view);
            ResultView.Text = "Loading...";
            
            var (status, location, events, user) = await Demo.Test();
            RunOnUiThread(() => ResultView.Text = $"Status: {status}\nLocation: {location?.Latitude} {location?.Longitude}\nEvents: {events?.Count()}\nUser: {user?.UserId}");    

            //toggle = !toggle;
            //if (toggle)
            //{
            //    var res = await Radar.StartTrip(new RadarTripOptions
            //    {
            //        ExternalId = "999",
            //        DestinationGeofenceTag = "tag",
            //        DestinationGeofenceExternalId = "id",
            //        Mode = RadarRouteMode.Truck,
            //        Metadata = new JSONObject
            //        {
            //            { "bing", "bong" },
            //            { "ding", "dong" },
            //            { "int", 5 },
            //            { "bool", true }
            //        }
            //    });
            //    Radar.StartTracking(RadarTrackingOptions.Continuous);
            //    RunOnUiThread(() => ResultView.Text = $"Started trip: {res}");
            //}
            //else
            //{
            //    var res = await Radar.CompleteTrip();
            //    Radar.StopTracking();
            //    RunOnUiThread(() => ResultView.Text = $"Completed trip: {res}");
            //}

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
