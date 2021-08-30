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

    public class SearchGeofencesCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)>
        , AndroidBinding.Radar.IRadarSearchGeofencesCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Location location, AndroidBinding.RadarGeofence[] geofences)
        {
            taskSource.SetResult(((RadarStatus)status.Ordinal(), location.ToSDK(), geofences?.Select(Conversion.ToSDK)));
        }
    }

    public class SearchPlacesCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarLocation, IEnumerable<RadarPlace>)>
        , AndroidBinding.Radar.IRadarSearchPlacesCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Location location, AndroidBinding.RadarPlace[] places)
        {
            taskSource.SetResult(((RadarStatus)status.Ordinal(), location.ToSDK(), places?.Select(Conversion.ToSDK)));
        }
    }

    public class RouteCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarRoutes)>
        , AndroidBinding.Radar.IRadarRouteCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarRoutes routes)
        {
            taskSource.SetResult(((RadarStatus)status.Ordinal(), routes?.ToSDK()));
        }
    }

    public class MatrixCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarRouteMatrix)>
        , AndroidBinding.Radar.IRadarMatrixCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarRouteMatrix matrix)
        {
            taskSource.SetResult(((RadarStatus)status.Ordinal(), matrix.ToSDK()));
        }
    }

    public class IpGeocodeallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarAddress, bool)>
        , AndroidBinding.Radar.IRadarIpGeocodeCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarAddress address, bool proxy)
        {
            taskSource.SetResult(((RadarStatus)status.Ordinal(), address.ToSDK(), proxy));
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
