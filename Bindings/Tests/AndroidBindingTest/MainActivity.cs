using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android;
using IO.Radar.Sdk;
using static IO.Radar.Sdk.Radar;
using Android.Locations;
using IO.Radar.Sdk.Model;
using System.Linq;

namespace AndroidBindingTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const string RADAR_KEY = "prj_test_pk_8c93cbcd86a49ae4cc090c67ae378767b48638ec "; // "ENTER KEY HERE";

        private AppCompatTextView ResultView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            ResultView = FindViewById<AppCompatTextView>(Resource.Id.result_view);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessBackgroundLocation }, 0);
            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 0);
            }

            Radar.Initialize(this, RADAR_KEY);
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

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            ResultView.Text = "Loading...";
            Radar.TrackOnce(new RadarTrackCallback((status, location, events, user) =>
            {
                RunOnUiThread(() => ResultView.Text = $"Status: {status}\nLocation: {location}\nEvents: {string.Join('\n', events.Select(e => e.ToJson()))}\nUser: {user}");
            }));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}

    public class RadarTrackCallback : Java.Lang.Object, IRadarTrackCallback
    {
        private Action<RadarStatus, Location, RadarEvent[], RadarUser> handler;

        public RadarTrackCallback(Action<RadarStatus, Location, RadarEvent[], RadarUser> handler)
        {
            this.handler = handler;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            handler = null;
        }

        public void OnComplete(RadarStatus status, Location location, RadarEvent[] events, RadarUser user)
            => handler(status, location, events, user);
    }
}
