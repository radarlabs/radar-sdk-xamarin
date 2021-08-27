using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using static RadarIO.Xamarin.RadarSingleton;

namespace RadarIO.Xamarin.Shared.Sample
{
    public static class Demo
    {
        private const string RADAR_KEY = "prj_test_pk_8c93cbcd86a49ae4cc090c67ae378767b48638ec "; // "ENTER KEY HERE";

        public static void Initialize()
        {
            Radar.Initialize(RADAR_KEY);
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

        public static async Task<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> Test()
        {
            var ret = await Radar.TrackOnce();
            var (status, loc, events, user) = ret;
            var (code, addresses) = await Radar.Autocomplete("Kung Fu", loc, 5);
            (code, addresses) = await Radar.Geocode("Kung Fu Tea Pinellas Park");
            (code, addresses) = await Radar.ReverseGeocode(loc);

            return ret;
        }
    }
}
