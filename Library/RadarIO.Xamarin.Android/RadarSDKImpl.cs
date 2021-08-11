using System;
using System.Threading.Tasks;
using Android.Content;

namespace RadarIO.Xamarin
{
    public class RadarSDKImpl : RadarSDK
    {
        public override void Initialize(string publishableKey)
        {
            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey);
        }

        public override Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce()
        {
            var handler = new TrackCallbackHandler();
            AndroidBinding.Radar.TrackOnce(handler);
            return handler.Result;
        }
    }
}
