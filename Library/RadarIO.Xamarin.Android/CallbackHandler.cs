using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Locations;

namespace RadarIO.Xamarin
{
    public abstract class TaskCallbackHandler<T> : Java.Lang.Object
    {
        protected TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();

        public Task<T> Task => taskSource.Task;
    }

    public abstract class RepeatingCallbackHandler<T> : Java.Lang.Object
    {
        protected Action<T> callback;

        public RepeatingCallbackHandler(Action<T> callback)
        {
            this.callback = callback;
        }
    }

    public class TrackCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)>
        , AndroidBinding.Radar.IRadarTrackCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Location location, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
        {
            taskSource.SetResult((status.ToSDK(), location?.ToSDK(), events?.Select(Conversion.ToSDK), user?.ToSDK()));
        }
    }

    public class TripCallbackHandler
        : TaskCallbackHandler<RadarStatus>
        , AndroidBinding.Radar.IRadarTripCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status)
        {
            taskSource.SetResult(status.ToSDK());
        }
    }

    public class GeocodeCallbackHandler
        : TaskCallbackHandler<(RadarStatus, IEnumerable<RadarAddress>)>
        , AndroidBinding.Radar.IRadarGeocodeCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarAddress[] addresses)
        {
            taskSource.SetResult(((RadarStatus)status.Ordinal(), addresses?.Select(Conversion.ToSDK)));
        }
    }

    public class RepeatingTrackCallbackHandler
        : RepeatingCallbackHandler<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)>
        , AndroidBinding.Radar.IRadarTrackCallback
    {
        public RepeatingTrackCallbackHandler(Action<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> callback)
            : base(callback) { }

        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Location location, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
        {
            callback?.Invoke((status.ToSDK(), location?.ToSDK(), events?.Select(Conversion.ToSDK), user?.ToSDK()));
        }
    }
}
