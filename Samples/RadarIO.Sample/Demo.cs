using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RadarIO.RadarSingleton;

namespace RadarIO.Sample
{
    public static class Demo
    {
        private const string RADAR_KEY = null;

        public static void Initialize(RadarSDK sdk, string radarKey = null)
        {
            RadarSingleton.Initialize(sdk);
            Radar.Initialize(string.IsNullOrEmpty(radarKey) ? RADAR_KEY : radarKey);
            Radar.SetLogLevel(RadarLogLevel.Debug);
            Radar.UserId = "test user";
            Radar.Description = "test desc";
            Radar.Metadata = new JSONObject
            {
                { "bing", "bong" },
                { "ding", "dong" },
                { "int", 10 },
                { "bool", true }
            };


            // Radar.ClientLocationUpdated += (result) =>
            // {
            //     // do something with result.location, result.stopped, result.source
            // };

            // Radar.LocationUpdated += (result) =>
            // {
            //     // do something with result.location, result.user
            // };

            // Radar.EventsReceived += (result) =>
            // {
            //     // do something with result.events, result.user
            // };

            // Radar.Error += (err) => {
            //     // do something with err
            // };

            Radar.Log += msg =>
            {

            };
        }

        public static async Task Test()
        {
            //var trackingOptions = new RadarTrackingOptions
            //{
            //    Sync = RadarTrackingOptionsSync.All,
            //    Replay = RadarTrackingOptionsReplay.Stops,
            //    DesiredAccuracy = RadarTrackingOptionsDesiredAccuracy.Medium
            //};

            //var res = await Radar.SendEvent("test-event", new JSONObject {  });
            //return res;

            //var trackingOptions = Radar.TrackingOptionsResponsive;

            //Radar.StartTracking(trackingOptions);
            //return (RadarStatus.Success, null, null, null);

            var (_, loc, _, user) = await Radar.TrackOnce();
            var (_, addresses) = await Radar.Autocomplete("brooklyn", loc);
            var (_, rgeo) = await Radar.ReverseGeocode(loc);
            var (_, geo) = await Radar.Geocode("Whole Foods Market, 1000 3rd St, San Francisco, CA 94158, USA");
            var (_, _, geofences) = await Radar.SearchGeofences();
            var (_, _, places) = await Radar.SearchPlaces(1000, loc, categories: ["food-beverage"]);

            RadarLocation origin = new() { Latitude = 40.78382, Longitude = -73.97536 };
            RadarLocation destination = new() { Latitude = 40.70390, Longitude = -73.98670 };
            var (_, routes) = await Radar.GetDistance(origin, destination, [RadarRouteMode.Bike, RadarRouteMode.Car], RadarRouteUnits.Metric);
            var (_, matrix) = await Radar.GetMatrix([origin], [destination], RadarRouteMode.Car, RadarRouteUnits.Imperial);
            var matrixTest = matrix.RouteBetween(0, 0);
            var (_, ipGeocode, isProxy) = await Radar.IpGeocode();

            Radar.TokenUpdated += token =>
            {
                // send token to server
            };

            Radar.StartTrackingVerified(1200, false);
        }

        public static async Task<TrackData> TrackOnce()
            => await Radar.TrackOnce();

        public static async Task<TokenData> TrackVerified()
            => await Radar.TrackVerified();

        public static void StartTrackingResponsive()
            => Radar.StartTracking(Radar.TrackingOptionsResponsive);


        public static void StartTrackingContinuous()
            => Radar.StartTracking(Radar.TrackingOptionsContinuous);


        public static void StopTracking()
            => Radar.StopTracking();


        public static void StartTrip(string geofenceId = null, string geofenceTag = null)
            => Radar.StartTrip(
                new RadarTripOptions
                {
                    ExternalId = System.Guid.NewGuid().ToString(),
                    Mode = RadarRouteMode.Car,
                    DestinationGeofenceExternalId = string.IsNullOrEmpty(geofenceId) ? "maui-demo" : geofenceId,
                    DestinationGeofenceTag = string.IsNullOrEmpty(geofenceTag) ? "maui-demo" : geofenceTag
                },
                Radar.TrackingOptionsContinuous);


        public static async Task StopTrip()
            => await Radar.CompleteTrip();

        public static async Task<GeofencesData> SearchGeofences()
            => await Radar.SearchGeofences();
    }
}
