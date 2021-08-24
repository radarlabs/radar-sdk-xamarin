using System;
using System.Threading.Tasks;

namespace RadarIO.Xamarin.Shared.Sample
{

    public static class Demo
    {
        private const string RADAR_KEY = "prj_test_pk_8c93cbcd86a49ae4cc090c67ae378767b48638ec "; // "ENTER KEY HERE";

        public static void Initialize(this RadarSDK Radar)
        {
            Radar.Initialize(RADAR_KEY);
        }

        public static async Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce(this RadarSDK Radar)
            => await Radar.TrackOnce();
    }
}
