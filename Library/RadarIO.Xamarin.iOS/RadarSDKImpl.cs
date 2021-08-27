using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLocation;

namespace RadarIO.Xamarin
{
    public class RadarSDKImpl : iOSBinding.RadarDelegate, RadarSDK
    {
        public RadarTrackingOptions ContinuousTrackingOptions
            => iOSBinding.RadarTrackingOptions.Continuous.ToSDK();
        public RadarTrackingOptions ResponsiveTrackingOptions
            => iOSBinding.RadarTrackingOptions.Responsive.ToSDK();
        public RadarTrackingOptions EfficientTrackingOptions
            => iOSBinding.RadarTrackingOptions.Efficient.ToSDK();

        #region RadarDelegate

        RadarSDKImpl Radar => (RadarSDKImpl)RadarSingleton.Radar;

        public override void DidFailWithStatus(iOSBinding.RadarStatus status)
        {
            Radar.Error?.Invoke((RadarStatus)status);
        }

        public override void DidLogMessage(string message)
        {
            Radar.Log?.Invoke(message);
        }

        public override void DidReceiveEvents(iOSBinding.RadarEvent[] events, iOSBinding.RadarUser user)
        {
            Radar.EventsReceived?.Invoke((events?.Select(e => e?.ToSDK()), user?.ToSDK()));
        }

        public override void DidUpdateClientLocation(CLLocation location, bool stopped, iOSBinding.RadarLocationSource source)
        {
            Radar.ClientLocationUpdated?.Invoke((location?.ToSDK(), stopped, (RadarLocationSource)source));
        }

        public override void DidUpdateLocation(CLLocation location, iOSBinding.RadarUser user)
        {
            Radar.LocationUpdated?.Invoke((location?.ToSDK(), user?.ToSDK()));
        }

        #endregion

        public event RadarEventHandler<(IEnumerable<RadarEvent>, RadarUser)> EventsReceived;
        public event RadarEventHandler<(RadarLocation, RadarUser)> LocationUpdated;
        public event RadarEventHandler<(RadarLocation, bool, RadarLocationSource)> ClientLocationUpdated;
        public event RadarEventHandler<RadarStatus> Error;
        public event RadarEventHandler<string> Log;

        public void Initialize(string publishableKey)
        {
            iOSBinding.Radar.InitializeWithPublishableKey(publishableKey);
            iOSBinding.Radar.SetDelegate(this);
        }

        public string UserId
        {
            get => iOSBinding.Radar.UserId;
            set => iOSBinding.Radar.SetUserId(value);
        }

        public string Description
        {
            get => iOSBinding.Radar.Description;
            set => iOSBinding.Radar.SetDescription(value);
        }

        public JSONObject Metadata
        {
            get => iOSBinding.Radar.Metadata?.ToSDK();
            set => iOSBinding.Radar.SetMetadata(value?.ToBinding());
        }

        public Task<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> TrackOnce()
        {
            var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)>();
            iOSBinding.Radar.TrackOnceWithCompletionHandler((status, location, ev, user) =>
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), ev?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK()));
            });
            return src.Task;
        }

        public void StartTracking(RadarTrackingOptions options)
        {
            iOSBinding.Radar.StartTrackingWithOptions(options.ToBinding());
        }

        public void StopTracking()
        {
            iOSBinding.Radar.StopTracking();
        }

        public void MockTracking(RadarLocation origin, RadarLocation destination, RadarRouteMode mode, int steps, int interval, Action<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> callback)
        {
            iOSBinding.Radar.MockTrackingWithOrigin(
                origin?.ToBinding(),
                destination?.ToBinding(),
                (iOSBinding.RadarRouteMode)mode,
                steps,
                interval,
                (status, location, events, user) => callback?.Invoke((status.ToSDK(), location?.ToSDK(), events?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK())));
        }

        public Task<RadarStatus> StartTrip(RadarTripOptions options)
        {
            var src = new TaskCompletionSource<RadarStatus>();
            iOSBinding.Radar.StartTripWithOptions(options.ToBinding(), status =>
            {
                src.SetResult(status.ToSDK());
            });
            return src.Task;
        }

        public Task<RadarStatus> CancelTrip()
        {
            var src = new TaskCompletionSource<RadarStatus>();
            iOSBinding.Radar.CancelTripWithCompletionHandler(status =>
            {
                src.SetResult(status.ToSDK());
            });
            return src.Task;
        }

        public Task<RadarStatus> CompleteTrip()
        {
            var src = new TaskCompletionSource<RadarStatus>();
            iOSBinding.Radar.CompleteTripWithCompletionHandler(status =>
            {
                src.SetResult(status.ToSDK());
            });
            return src.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, RadarLocation near, int limit)
        {
            var src = new TaskCompletionSource<(RadarStatus, IEnumerable<RadarAddress>)>();
            iOSBinding.Radar.AutocompleteQuery(query, near?.ToBinding(), limit, (status, addresses) =>
            {
                src.SetResult((status.ToSDK(), addresses?.Select(Conversion.ToSDK)));
            });
            return src.Task;
        }
    }
}
