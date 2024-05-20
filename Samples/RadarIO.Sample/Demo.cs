using System.Collections.Generic;
using System.Threading.Tasks;
using static RadarIO.RadarSingleton;

namespace RadarIO.Sample
{
    public static class Demo
    {
        private const string RADAR_KEY = "prj_test_pk_8d7149cfe4a0fa5e5bb6a440a47a978995447ffc";

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

            Radar.Log += msg =>
            {

            };
        }

        public static async Task<TrackData> Test()
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

            var ret = await Radar.TrackOnce();
            //var (_, loc, _, _) = ret;
            //var (_, addresses) = await Radar.Autocomplete("grocery store", loc, 5);
            ////(_, addresses) = await Radar.ReverseGeocode(loc);
            ////(_, addresses) = await Radar.Geocode("Kung Fu Tea Pinellas Park");
            ////var (_, _, geofences) = await Radar.SearchGeofences(10, new[] { "tag" });
            ////(_, _, geofences) = await Radar.SearchGeofences(loc, 10, new[] { "tag" });
            //var (_, _, places) = await Radar.SearchPlaces(1000, categories: new[] { "food-beverage" });
            //(_, _, places) = await Radar.SearchPlaces(loc, 1000, categories: new[] { "food-beverage" });

            //var address = addresses.Select(a => new Location { Latitude = a.Coordinate.Latitude, Longitude = a.Coordinate.Longitude }).First();
            //var (_, routes) = await Radar.GetDistance(address, new[] { RadarRouteMode.Bike, RadarRouteMode.Car }, RadarRouteUnits.Metric);
            //var (_, matrix) = await Radar.GetMatrix(new[] { loc }, new[] { address }, RadarRouteMode.Car, RadarRouteUnits.Imperial);
            //var matrixTest = matrix.RouteBetween(0, 0);
            //var (_, ipGeocode, isProxy) = await Radar.IpGeocode();

            //var (status, loc2, places) = await Radar.SearchPlaces(10, chainMetadata: new Dictionary<string, string> { { "asd", "qwe" } });
            RadarTrackingOptions trackingOptions = Radar.TrackingOptionsContinuous;
            trackingOptions.DesiredStoppedUpdateInterval = 180;
            trackingOptions.DesiredStoppedUpdateInterval = 60;
            trackingOptions.DesiredSyncInterval = 50;
            trackingOptions.DesiredAccuracy = RadarTrackingOptionsDesiredAccuracy.High;
            trackingOptions.StopDuration = 140;
            trackingOptions.StopDistance = 70;
            trackingOptions.Sync = RadarTrackingOptionsSync.All;
            trackingOptions.Replay = RadarTrackingOptionsReplay.None;
            return ret;
        }

        public static async Task<TrackData> TrackOnce()
            => await Radar.TrackOnce();

        public static async Task<TrackData> TrackVerified()
            => await Radar.TrackVerified();

        public static async Task<TokenData> TrackVerifiedToken()
            => await Radar.TrackVerifiedToken();


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
    }
}
