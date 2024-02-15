using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;

namespace RadarIO
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
        public event RadarEventHandler<(Location, RadarUser)> LocationUpdated;
        public event RadarEventHandler<(Location, bool, RadarLocationSource)> ClientLocationUpdated;
        public event RadarEventHandler<RadarStatus> Error;
        public event RadarEventHandler<string> Log;

        #region RadarReceiver

        RadarSDKImpl Radar => (RadarSDKImpl)RadarSingleton.Radar;

        public override void OnClientLocationUpdated(Context context, Android.Locations.Location location, bool stopped, AndroidBinding.Radar.RadarLocationSource source)
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

        public override void OnLocationUpdated(Context context, Android.Locations.Location location, AndroidBinding.RadarUser user)
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
            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey, this, AndroidBinding.Radar.RadarLocationServicesProvider.Google, false);
            //Application.Context.RegisterReceiver(this, new IntentFilter("io.radar.sdk.RECEIVED"));
        }

        public void Initialize(string publishableKey, RadarLocationServicesProvider locationServicesProvider, bool fraud)
        {
            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey, this, locationServicesProvider.ToBinding(), fraud);
            //Application.Context.RegisterReceiver(this, new IntentFilter("io.radar.sdk.RECEIVED"));
        }

        public void SetForegroundServiceOptions(RadarTrackingOptionsForegroundService options)
        {
            AndroidBinding.Radar.SetForegroundServiceOptions(options?.ToBinding());
        }

        public void SetLogLevel(RadarLogLevel level)
        {
            AndroidBinding.Radar.SetLogLevel(AndroidBinding.Radar.RadarLogLevel.Values()[(int)level]);
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
        public bool AnonymousTrackingEnabled { set => AndroidBinding.Radar.SetAnonymousTrackingEnabled(value); }

        public bool IsTracking => AndroidBinding.Radar.IsTracking;

        public RadarTrackingOptions TrackingOptions => AndroidBinding.Radar.TrackingOptions?.ToSDK();

        public RadarTripOptions TripOptions => AndroidBinding.Radar.TripOptions?.ToSDK();


        public Task<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> TrackOnce()
        {
            var handler = new TrackCallbackHandler();
            AndroidBinding.Radar.TrackOnce(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> TrackOnce(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons)
        {
            var handler = new TrackCallbackHandler();
            AndroidBinding.Radar.TrackOnce(desiredAccuracy.ToBinding(), beacons, handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> TrackOnce(Location location)
        {
            var handler = new TrackCallbackHandler();
            AndroidBinding.Radar.TrackOnce(location?.ToBinding(), handler);
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

        public void MockTracking(Location origin, Location destination, RadarRouteMode mode, int steps, int interval, Action<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> callback)
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

        public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> StartTrip(RadarTripOptions options)
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.StartTrip(options.ToBinding(), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> StartTrip(RadarTripOptions options, RadarTrackingOptions trackingOptions)
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.StartTrip(options.ToBinding(), trackingOptions?.ToBinding(), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> UpdateTrip(RadarTripOptions options, RadarTripStatus status = RadarTripStatus.Unknown)
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.UpdateTrip(options.ToBinding(), status.ToBinding(), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> CancelTrip()
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.CancelTrip(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> CompleteTrip()
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.CompleteTrip(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, Location near, int limit)
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.Autocomplete(query, near?.ToBinding(), new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, Location near = null, IEnumerable<string> layers = null, int limit = 100, string country = null)
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.Autocomplete(query, near?.ToBinding(), layers?.ToArray(), new Java.Lang.Integer(limit), country, handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> Geocode(string query)
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.Geocode(query, handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> ReverseGeocode()
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.ReverseGeocode(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, IEnumerable<RadarAddress>)> ReverseGeocode(Location location)
        {
            var handler = new GeocodeCallbackHandler();
            AndroidBinding.Radar.ReverseGeocode(location?.ToBinding(), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, IEnumerable<RadarGeofence>)> SearchGeofences(Location near, int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
        {
            var handler = new SearchGeofencesCallbackHandler();
            AndroidBinding.Radar.SearchGeofences(near?.ToBinding(), radius, tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, IEnumerable<RadarGeofence>)> SearchGeofences(int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
        {
            var handler = new SearchGeofencesCallbackHandler();
            AndroidBinding.Radar.SearchGeofences(radius, tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, IEnumerable<RadarPlace>)> SearchPlaces(Location near, int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 0, IDictionary<string, string> chainMetadata = null)
        {
            var handler = new SearchPlacesCallbackHandler();
            AndroidBinding.Radar.SearchPlaces(near?.ToBinding(), radius, chains?.ToArray(), chainMetadata, categories?.ToArray(), groups?.ToArray(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, IEnumerable<RadarPlace>)> SearchPlaces(int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 0, IDictionary<string, string> chainMetadata = null)
        {
            var handler = new SearchPlacesCallbackHandler();
            AndroidBinding.Radar.SearchPlaces(radius, chains?.ToArray(), chainMetadata, categories?.ToArray(), groups?.ToArray(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarRoutes)> GetDistance(Location destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
        {
            var handler = new RouteCallbackHandler();
            AndroidBinding.Radar.GetDistance(destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarRoutes)> GetDistance(Location source, Location destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
        {
            var handler = new RouteCallbackHandler();
            AndroidBinding.Radar.GetDistance(source?.ToBinding(), destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarRouteMatrix)> GetMatrix(IEnumerable<Location> origins, IEnumerable<Location> destinations, RadarRouteMode mode, RadarRouteUnits units)
        {
            var handler = new MatrixCallbackHandler();
            AndroidBinding.Radar.GetMatrix(origins?.Select(Conversion.ToBinding).ToArray(), destinations?.Select(Conversion.ToBinding).ToArray(), AndroidBinding.Radar.RadarRouteMode.Values()[(int)mode], AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarAddress, bool)> IpGeocode()
        {
            var handler = new IpGeocodeCallbackHandler();
            AndroidBinding.Radar.IpGeocode(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, bool)> GetLocation()
        {
            var handler = new LocationCallbackHandler();
            AndroidBinding.Radar.GetLocation(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, bool)> GetLocation(RadarTrackingOptionsDesiredAccuracy desiredAccuracy)
        {
            var handler = new LocationCallbackHandler();
            AndroidBinding.Radar.GetLocation(desiredAccuracy.ToBinding(), handler);
            return handler.Task;
        }

        public void AcceptEventId(string eventId, string verifiedPlaceId = null)
        {
            AndroidBinding.Radar.AcceptEvent(eventId, verifiedPlaceId);
        }

        public void RejectEventId(string eventId)
        {
            AndroidBinding.Radar.RejectEvent(eventId);
        }

        public Task<(RadarStatus, Location, RadarContext)> GetContext()
        {
            var handler = new ContextCallbackHandler();
            AndroidBinding.Radar.GetContext(handler);
            return handler.Task;
        }

        public Task<(RadarStatus, Location, RadarContext)> GetContext(Location location)
        {
            var handler = new ContextCallbackHandler();
            AndroidBinding.Radar.GetContext(location?.ToBinding(), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarEvent)> LogConversion(string name, JSONObject metadata)
        {
            var handler = new LogConversionCallbackHandler();
            AndroidBinding.Radar.LogConversion(name, metadata?.ToBinding(), handler);
            return handler.Task;
        }

        public Task<(RadarStatus, RadarEvent)> LogConversion(string name, double revenue, JSONObject metadata)
        {
            var handler = new LogConversionCallbackHandler();
            AndroidBinding.Radar.LogConversion(name, revenue, metadata?.ToBinding(), handler);
            return handler.Task;
        }

        public string StringForStatus(RadarStatus status)
            => status.ToString();

        public string StringForLocationSource(RadarLocationSource source)
            => AndroidBinding.Radar.StringForSource(source.ToBinding());

        public string StringForMode(RadarRouteMode mode)
            => AndroidBinding.Radar.StringForMode(mode.ToBinding());

        public string StringForTripStatus(RadarTripStatus status)
            => AndroidBinding.Radar.StringForTripStatus(status.ToBinding());

        public JSONObject DictionaryForLocation(Location location)
            => AndroidBinding.Radar.JsonForLocation(location?.ToBinding())?.ToSDK();
    }

    internal class RadarRouteMatrixImpl : RadarRouteMatrix
    {
        public override RadarRoute RouteBetween(int originIndex, int destinationIndex)
            => matrix.ElementAtOrDefault(originIndex)?.ElementAtOrDefault(destinationIndex);
    }
}
