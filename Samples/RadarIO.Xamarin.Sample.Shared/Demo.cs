using System;
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

        public static async Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce()
            => await Radar.TrackOnce();
    }
}
