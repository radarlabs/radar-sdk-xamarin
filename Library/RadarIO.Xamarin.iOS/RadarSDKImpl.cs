using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarIO.Xamarin
{
    public class RadarSDKImpl : RadarSDK
    {
        public override void Initialize(string publishableKey)
        {
            iOSBinding.Radar.InitializeWithPublishableKey(publishableKey);
        }

        public override Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce()
        {
            var src = new TaskCompletionSource<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)>();
            iOSBinding.Radar.TrackOnceWithCompletionHandler((status, location, ev, user) =>
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), ev?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK()));
            });
            return src.Task;
        }

        public override void StartTracking(RadarTrackingOptions options)
        {
            iOSBinding.Radar.StartTrackingWithOptions(options.ToBinding());
        }

        public override void StopTracking()
        {
            iOSBinding.Radar.StopTracking();
        }

        public override Task<RadarStatus> StartTrip(RadarTripOptions options)
        {
            var src = new TaskCompletionSource<RadarStatus>();
            iOSBinding.Radar.StartTripWithOptions(options.ToBinding(), status =>
            {
                src.SetResult(status.ToSDK());
            });
            return src.Task;
        }

        public override Task<RadarStatus> CancelTrip()
        {
            var src = new TaskCompletionSource<RadarStatus>();
            iOSBinding.Radar.CancelTripWithCompletionHandler(status =>
            {
                src.SetResult(status.ToSDK());
            });
            return src.Task;
        }

        public override Task<RadarStatus> CompleteTrip()
        {
            var src = new TaskCompletionSource<RadarStatus>();
            iOSBinding.Radar.CompleteTripWithCompletionHandler(status =>
            {
                src.SetResult(status.ToSDK());
            });
            return src.Task;
        }
    }

    public partial class RadarTrackingOptions
    {
        public bool ShowBlueBar;
        public bool UseVisits;
        public bool UseSignificantLocationChanges;
        public static RadarTrackingOptions Continuous
            => iOSBinding.RadarTrackingOptions.Continuous.ToSDK();
        public static RadarTrackingOptions Responsive
            => iOSBinding.RadarTrackingOptions.Responsive.ToSDK();
        public static RadarTrackingOptions Efficient
            => iOSBinding.RadarTrackingOptions.Efficient.ToSDK();
    }
}
