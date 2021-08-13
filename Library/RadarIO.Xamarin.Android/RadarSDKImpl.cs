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

        public override void StartTracking(RadarTrackingOptions options)
        {
            AndroidBinding.Radar.StartTracking(options.ToBinding());
        }

        public override void StopTracking()
        {
            AndroidBinding.Radar.StopTracking();
        }
    }

    public partial class RadarTrackingOptions
    {
        public int FastestStoppedUpdateInterval;
        public int FastestMovingUpdateInterval;
        public int SyncGeofencesLimit;
        public RadarTrackingOptionsForegroundService ForegroundService;
        public static RadarTrackingOptions Responsive
            => AndroidBinding.RadarTrackingOptions.Responsive.ToSDK();
        public static RadarTrackingOptions Continuous
            => AndroidBinding.RadarTrackingOptions.Continuous.ToSDK();
    }

    public class RadarTrackingOptionsForegroundService
    {
        public string Text;
        public string Title;
        public int Icon;
        public bool UpdatesOnly;
        public string Activity;
        public int Importance;
        public int Id;
    }
}
