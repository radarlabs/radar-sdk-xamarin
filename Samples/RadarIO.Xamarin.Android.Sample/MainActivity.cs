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

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const string RADAR_KEY = "prj_test_pk_8c93cbcd86a49ae4cc090c67ae378767b48638ec "; // "ENTER KEY HERE";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessBackgroundLocation }, 0);
            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 0);
            }

            Radar.Initialize(RADAR_KEY);
            Radar.StopTracking();
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
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            var ResultView = FindViewById<AppCompatTextView>(Resource.Id.result_view);
            //ResultView.Text = "Loading...";
            //Task.Run(async () =>
            //{
            //    var (status, location, events, user) = await Radar.TrackOnce();
            //    RunOnUiThread(() => ResultView.Text = $"Status: {status}\nLocation: {location?.Latitude} {location?.Longitude}\nEvents: {events?.Count()}\nUser: {user?.UserId}");
            //});

            toggle = !toggle;
            if (toggle)
            {
                RunOnUiThread(() => ResultView.Text = "Started trip");
                Radar.StartTrip(new RadarTripOptions
                {
                    ExternalId = "999",
                    DestinationGeofenceTag = "tag",
                    DestinationGeofenceExternalId = "777",
                    Mode = RadarRouteMode.Car
                });
                Radar.StartTracking(RadarTrackingOptions.Continuous);
            }
            else
            {
                RunOnUiThread(() => ResultView.Text = "Completed trip");
                Radar.CompleteTrip();
                Radar.StopTracking();
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
