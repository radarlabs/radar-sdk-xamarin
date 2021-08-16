using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Locations;

namespace RadarIO.Xamarin
{
    public abstract class CallbackHandler<T> : Java.Lang.Object
    {
        protected TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();

        public Task<T> Result => taskSource.Task;
    }

    public class TrackCallbackHandler
        : CallbackHandler<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)>
        , AndroidBinding.Radar.IRadarTrackCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Location location, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
        {
            taskSource.SetResult((status.ToSDK(), location?.ToSDK(), events?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK()));
        }
    }

    public class TripCallbackHandler
        : CallbackHandler<RadarStatus>
        , AndroidBinding.Radar.IRadarTripCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status)
        {
            taskSource.SetResult(status.ToSDK());
        }
    }
}
