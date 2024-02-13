using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        : TaskCallbackHandler<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)>
        , AndroidBinding.Radar.IRadarTrackCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Android.Locations.Location location, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
        {
            try
            {
                taskSource.SetResult((status.ToSDK(), location?.ToSDK(), events?.Select(Conversion.ToSDK), user?.ToSDK()));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class TripCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)>
        , AndroidBinding.Radar.IRadarTripCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarTrip trip, AndroidBinding.RadarEvent[] events)
        {
            try
            {
                taskSource.SetResult((status.ToSDK(), trip.ToSDK(), events?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class GeocodeCallbackHandler
        : TaskCallbackHandler<(RadarStatus, IEnumerable<RadarAddress>)>
        , AndroidBinding.Radar.IRadarGeocodeCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarAddress[] addresses)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), addresses?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class SearchGeofencesCallbackHandler
        : TaskCallbackHandler<(RadarStatus, Location, IEnumerable<RadarGeofence>)>
        , AndroidBinding.Radar.IRadarSearchGeofencesCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Android.Locations.Location location, AndroidBinding.RadarGeofence[] geofences)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), location.ToSDK(), geofences?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class SearchPlacesCallbackHandler
        : TaskCallbackHandler<(RadarStatus, Location, IEnumerable<RadarPlace>)>
        , AndroidBinding.Radar.IRadarSearchPlacesCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Android.Locations.Location location, AndroidBinding.RadarPlace[] places)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), location.ToSDK(), places?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class RouteCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarRoutes)>
        , AndroidBinding.Radar.IRadarRouteCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarRoutes routes)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), routes?.ToSDK()));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class MatrixCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarRouteMatrix)>
        , AndroidBinding.Radar.IRadarMatrixCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarRouteMatrix matrix)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), matrix.ToSDK()));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class IpGeocodeCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarAddress, bool)>
        , AndroidBinding.Radar.IRadarIpGeocodeCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarAddress address, bool proxy)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), address.ToSDK(), proxy));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class RepeatingTrackCallbackHandler
        : RepeatingCallbackHandler<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)>
        , AndroidBinding.Radar.IRadarTrackCallback
    {
        public RepeatingTrackCallbackHandler(Action<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> callback)
            : base(callback) { }

        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Android.Locations.Location location, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
        {
            callback?.Invoke((status.ToSDK(), location?.ToSDK(), events?.Select(Conversion.ToSDK), user?.ToSDK()));
        }
    }

    public class LocationCallbackHandler
        : TaskCallbackHandler<(RadarStatus, Location, bool)>
        , AndroidBinding.Radar.IRadarLocationCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Android.Locations.Location location, bool stopped)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), location.ToSDK(), stopped));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class ContextCallbackHandler
        : TaskCallbackHandler<(RadarStatus, Location, RadarContext)>
        , AndroidBinding.Radar.IRadarContextCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, Android.Locations.Location location, AndroidBinding.RadarContext context)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), location.ToSDK(), context.ToSDK()));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }

    public class LogConversionCallbackHandler
        : TaskCallbackHandler<(RadarStatus, RadarEvent)>
        , AndroidBinding.Radar.IRadarLogConversionCallback
    {
        public void OnComplete(AndroidBinding.Radar.RadarStatus status, AndroidBinding.RadarEvent events)
        {
            try
            {
                taskSource.SetResult(((RadarStatus)status.Ordinal(), events?.ToSDK()));
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        }
    }
}
