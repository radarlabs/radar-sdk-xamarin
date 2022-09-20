using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static RadarIO.Xamarin.RadarSingleton;

namespace RadarIO.Xamarin.Shared.Sample
{
    public static class Demo
    {
        private const string RADAR_KEY = "ENTER KEY HERE";

        public static void Initialize(RadarSDK sdk)
        {
            RadarSingleton.Initialize(sdk);
            Radar.Initialize(RADAR_KEY);
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
        }

        public static async Task<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> Test()
        {
            //var trackingOptions = new RadarTrackingOptions
            //{
            //    Sync = RadarTrackingOptionsSync.All,
            //    Replay = RadarTrackingOptionsReplay.Stops,
            //    DesiredAccuracy = RadarTrackingOptionsDesiredAccuracy.Medium
            //};

            var res = await Radar.SendEvent("test-event", new JSONObject { { "asd", 123 } });
            return res;

            //var trackingOptions = Radar.TrackingOptionsResponsive;

            //Radar.StartTracking(trackingOptions);
            //return (RadarStatus.Success, null, null, null);

            //var ret = await Radar.TrackOnce();
            //var (_, loc, _, _) = ret;
            //var (_, addresses) = await Radar.Autocomplete("Kung Fu", loc, 5);
            //(_, addresses) = await Radar.ReverseGeocode(loc);
            //(_, addresses) = await Radar.Geocode("Kung Fu Tea Pinellas Park");
            //var (_, _, geofences) = await Radar.SearchGeofences(10, new[] { "tag" });
            //(_, _, geofences) = await Radar.SearchGeofences(loc, 10, new[] { "tag" });
            //var (_, _, places) = await Radar.SearchPlaces(1000, categories: new[] { "food-beverage" });
            //(_, _, places) = await Radar.SearchPlaces(loc, 1000, categories: new[] { "food-beverage" });

            /* var address = addresses.Select(a => new Location { Latitude = a.Coordinate.Latitude, Longitude = a.Coordinate.Longitude }).First();
            var (_, routes) = await Radar.GetDistance(address, new[] { RadarRouteMode.Bike, RadarRouteMode.Car }, RadarRouteUnits.Metric);
            var (_, matrix) = await Radar.GetMatrix(new[] { loc }, new[] { address }, RadarRouteMode.Car, RadarRouteUnits.Imperial);
            var matrixTest = matrix.RouteBetween(0, 0);
            var (_, ipGeocode, isProxy) = await Radar.IpGeocode();*/

            //return ret;
        }
    }
}
