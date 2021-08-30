using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using AndroidX.AppCompat.App;

namespace RadarIO.Xamarin
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "io.radar.sdk.RECEIVED" })]
    public class RadarSDKImpl : AndroidBinding.RadarReceiver, RadarSDK
    {
        public RadarTrackingOptions TrackingOptionsContinuous
            => AndroidBinding.RadarTrackingOptions.Continuous.ToSDK();
        public RadarTrackingOptions TrackingOptionsResponsive
            => AndroidBinding.RadarTrackingOptions.Responsive.ToSDK();
        public RadarTrackingOptions TrackingOptionsEfficient
            => AndroidBinding.RadarTrackingOptions.Efficient.ToSDK();

        public event RadarEventHandler<(IEnumerable<RadarEvent>, RadarUser)> EventsReceived;
        public event RadarEventHandler<(RadarLocation, RadarUser)> LocationUpdated;
        public event RadarEventHandler<(RadarLocation, bool, RadarLocationSource)> ClientLocationUpdated;
        public event RadarEventHandler<RadarStatus> Error;
        public event RadarEventHandler<string> Log;

        #region RadarReceiver

        RadarSDKImpl Radar => (RadarSDKImpl)RadarSingleton.Radar;

        public override void OnClientLocationUpdated(Context context, Location location, bool stopped, AndroidBinding.Radar.RadarLocationSource source)
        {
            LaunchApp(context);
            Radar.ClientLocationUpdated?.Invoke((location?.ToSDK(), stopped, (RadarLocationSource)source.Ordinal()));
        }

        public override void OnError(Context context, AndroidBinding.Radar.RadarStatus status)
        {
            LaunchApp(context);
            Radar.Error?.Invoke((RadarStatus)status.Ordinal());
        }

        public override void OnEventsReceived(Context context, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
        {
            LaunchApp(context);
            Radar.EventsReceived?.Invoke((events?.Select(e => e?.ToSDK()), user?.ToSDK()));
        }

        public override void OnLocationUpdated(Context context, Location location, AndroidBinding.RadarUser user)
        {
            LaunchApp(context);
            Radar.LocationUpdated?.Invoke((location?.ToSDK(), user?.ToSDK()));
        }

        public override void OnLog(Context context, string message)
        {
            LaunchApp(context);
            Radar.Log?.Invoke(message);
        }

        private static void LaunchApp(Context context)
        {
            //if (RadarSingleton.IsInitialized)
            //    return;

            //var intent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
            //intent.SetFlags(ActivityFlags.NewTask);
            //context.StartActivity(intent);
        }

        #endregion

        public void Initialize(string publishableKey)
        {
            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey);
            //Application.Context.RegisterReceiver(this, new IntentFilter("io.radar.sdk.RECEIVED"));
        }

        public string UserId
        {
            get => AndroidBinding.Radar.UserId;
            set => AndroidBinding.Radar.UserId = value;
        }

        public string Description
        {
            get => AndroidBinding.Radar.Description;
            set => AndroidBinding.Radar.Description = value;
        }

        public JSONObject Metadata
        {
            get => AndroidBinding.Radar.Metadata?.ToSDK();
            set => AndroidBinding.Radar.Metadata = value?.ToBinding();
        }

        public Task<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> TrackOnce()
        {
            var handler = new TrackCallbackHandler();
            AndroidBinding.Radar.TrackOnce(handler);
            return handler.Task;
        }

        public void StartTracking(RadarTrackingOptions options)
        {
            AndroidBinding.Radar.StartTracking(options.ToBinding());
        }

        public void StopTracking()
        {
            AndroidBinding.Radar.StopTracking();
        }

        public void MockTracking(RadarLocation origin, RadarLocation destination, RadarRouteMode mode, int steps, int interval, Action<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> callback)
        {
            var handler = new RepeatingTrackCallbackHandler(callback);
            AndroidBinding.Radar.MockTracking(
                origin?.ToBinding(),
                destination?.ToBinding(),
                AndroidBinding.Radar.RadarRouteMode.Values()[(int)mode],
                steps,
                interval,
                handler);
        }

        public Task<RadarStatus> StartTrip(RadarTripOptions options)
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.StartTrip(options.ToBinding(), handler);
            return handler.Task;
        }

        public Task<RadarStatus> CancelTrip()
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.CancelTrip(handler);
            return handler.Task;
        }

        public Task<RadarStatus> CompleteTrip()
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.CompleteTrip(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, RadarLocation near, int limit)
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.Autocomplete(query, near?.ToBinding(), limit, handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> Geocode(string query)
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.Geocode(query, handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> ReverseGeocode(RadarLocation location)
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.ReverseGeocode(location?.ToBinding(), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)> SearchGeofences(RadarLocation near, int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
        {
            var handler = new SearchGeofencesCallbackHandler();
            AndroidBinding.Radar.SearchGeofences(near?.ToBinding(), radius, tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)> SearchGeofences(int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
        {
            var handler = new SearchGeofencesCallbackHandler();
            AndroidBinding.Radar.SearchGeofences(radius, tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarLocation, IEnumerable<RadarPlace>)> SearchPlaces(RadarLocation near, int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 0)
        {
            var handler = new SearchPlacesCallbackHandler();
            AndroidBinding.Radar.SearchPlaces(near?.ToBinding(), radius, chains?.ToArray(), categories?.ToArray(), groups?.ToArray(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarLocation, IEnumerable<RadarPlace>)> SearchPlaces(int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 0)
        {
            var handler = new SearchPlacesCallbackHandler();
            AndroidBinding.Radar.SearchPlaces(radius, chains?.ToArray(), categories?.ToArray(), groups?.ToArray(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarRoutes)> GetDistance(RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
        {
            var handler = new RouteCallbackHandler();
            AndroidBinding.Radar.GetDistance(destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarRoutes)> GetDistance(RadarLocation source, RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
        {
            var handler = new RouteCallbackHandler();
            AndroidBinding.Radar.GetDistance(source?.ToBinding(), destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarRouteMatrix)> GetMatrix(IEnumerable<RadarLocation> origins, IEnumerable<RadarLocation> destinations, RadarRouteMode mode, RadarRouteUnits units)
        {
            var handler = new MatrixCallbackHandler();
            AndroidBinding.Radar.GetMatrix(origins?.Select(Conversion.ToBinding).ToArray(), destinations?.Select(Conversion.ToBinding).ToArray(), AndroidBinding.Radar.RadarRouteMode.Values()[(int)mode], AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
            return handler.Task;
        }
    }

    internal class RadarRouteMatrixImpl : RadarRouteMatrix
    {
        public override RadarRoute RouteBetween(int originIndex, int destinationIndex)
            => matrix.ElementAtOrDefault(originIndex)?.ElementAtOrDefault(destinationIndex);
    }
}
