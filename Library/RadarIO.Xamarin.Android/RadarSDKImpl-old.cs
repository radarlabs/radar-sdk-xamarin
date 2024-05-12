﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Android.App;
//using Android.Content;

//namespace RadarIO
//{
//    [BroadcastReceiver(Enabled = true, Exported = true)]
//    [IntentFilter(new[] { "io.radar.sdk.RECEIVED" })]
//    public class RadarSDKImpl : AndroidBinding.RadarReceiver, RadarSDK
//    {
//        public RadarTrackingOptions TrackingOptionsContinuous
//            => AndroidBinding.RadarTrackingOptions.Continuous.ToSDK();
//        public RadarTrackingOptions TrackingOptionsResponsive
//            => AndroidBinding.RadarTrackingOptions.Responsive.ToSDK();
//        public RadarTrackingOptions TrackingOptionsEfficient
//            => AndroidBinding.RadarTrackingOptions.Efficient.ToSDK();

//        public event RadarEventHandler<EventsData> EventsReceived;
//        public event RadarEventHandler<LocationUpdatedData> LocationUpdated;
//        public event RadarEventHandler<ClientLocationUpdatedData> ClientLocationUpdated;
//        public event RadarEventHandler<RadarStatus> Error;
//        public event RadarEventHandler<string> Log;

//        #region RadarReceiver

//        RadarSDKImpl Radar => (RadarSDKImpl)RadarSingleton.Radar;

//        public override void OnClientLocationUpdated(Context context, Android.Locations.Location location, bool stopped, AndroidBinding.Radar.RadarLocationSource source)
//        {
//            LaunchApp(context);
//            Radar.ClientLocationUpdated?.Invoke((location?.ToSDK(), stopped, (RadarLocationSource)source.Ordinal()));
//        }

//        public override void OnError(Context context, AndroidBinding.Radar.RadarStatus status)
//        {
//            LaunchApp(context);
//            Radar.Error?.Invoke((RadarStatus)status.Ordinal());
//        }

//        public override void OnEventsReceived(Context context, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
//        {
//            LaunchApp(context);
//            Radar.EventsReceived?.Invoke((events?.Select(e => e?.ToSDK()), user?.ToSDK()));
//        }

//        public override void OnLocationUpdated(Context context, Android.Locations.Location location, AndroidBinding.RadarUser user)
//        {
//            LaunchApp(context);
//            Radar.LocationUpdated?.Invoke((location?.ToSDK(), user?.ToSDK()));
//        }

//        public override void OnLog(Context context, string message)
//        {
//            LaunchApp(context);
//            Radar.Log?.Invoke(message);
//        }

//        private static void LaunchApp(Context context)
//        {
//            //if (RadarSingleton.IsInitialized)
//            //    return;

//            //var intent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
//            //intent.SetFlags(ActivityFlags.NewTask);
//            //context.StartActivity(intent);
//        }

//        #endregion

//        public void Initialize(string publishableKey)
//        {
//            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey, this, AndroidBinding.Radar.RadarLocationServicesProvider.Google, false);
//            //Application.Context.RegisterReceiver(this, new IntentFilter("io.radar.sdk.RECEIVED"));
//        }

//        public void Initialize(string publishableKey, RadarLocationServicesProvider locationServicesProvider, bool fraud)
//        {
//            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey, this, locationServicesProvider.ToBinding(), fraud);
//            //Application.Context.RegisterReceiver(this, new IntentFilter("io.radar.sdk.RECEIVED"));
//        }

//        public void SetForegroundServiceOptions(RadarTrackingOptionsForegroundService options)
//        {
//            AndroidBinding.Radar.SetForegroundServiceOptions(options?.ToBinding());
//        }

//        public void SetLogLevel(RadarLogLevel level)
//        {
//            AndroidBinding.Radar.SetLogLevel(AndroidBinding.Radar.RadarLogLevel.Values()[(int)level]);
//        }

//        public string UserId
//        {
//            get => AndroidBinding.Radar.UserId;
//            set => AndroidBinding.Radar.UserId = value;
//        }

//        public string Description
//        {
//            get => AndroidBinding.Radar.Description;
//            set => AndroidBinding.Radar.Description = value;
//        }

//        public JSONObject Metadata
//        {
//            get => AndroidBinding.Radar.Metadata?.ToSDK();
//            set => AndroidBinding.Radar.Metadata = value?.ToBinding();
//        }
//        public bool AnonymousTrackingEnabled { set => AndroidBinding.Radar.SetAnonymousTrackingEnabled(value); }

//        public bool IsTracking => AndroidBinding.Radar.IsTracking;

//        public RadarTrackingOptions TrackingOptions => AndroidBinding.Radar.TrackingOptions?.ToSDK();

//        public RadarTripOptions TripOptions => AndroidBinding.Radar.TripOptions?.ToSDK();


//        public Task<TrackData> TrackOnce()
//        {
//            var handler = new TrackCallbackHandler();
//            AndroidBinding.Radar.TrackOnce(handler);
//            return handler.Task;
//        }

//        public Task<TrackData> TrackOnce(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons)
//        {
//            var handler = new TrackCallbackHandler();
//            AndroidBinding.Radar.TrackOnce(desiredAccuracy.ToBinding(), beacons, handler);
//            return handler.Task;
//        }

//        public Task<TrackData> TrackOnce(RadarLocation location)
//        {
//            var handler = new TrackCallbackHandler();
//            AndroidBinding.Radar.TrackOnce(location?.ToBinding(), handler);
//            return handler.Task;
//        }

//        public void StartTracking(RadarTrackingOptions options)
//        {
//            AndroidBinding.Radar.StartTracking(options.ToBinding());
//        }

//        public void StopTracking()
//        {
//            AndroidBinding.Radar.StopTracking();
//        }

//        public void MockTracking(RadarLocation origin, RadarLocation destination, RadarRouteMode mode, int steps, int interval, Action<TrackData> callback)
//        {
//            var handler = new RepeatingTrackCallbackHandler(callback);
//            AndroidBinding.Radar.MockTracking(
//                origin?.ToBinding(),
//                destination?.ToBinding(),
//                AndroidBinding.Radar.RadarRouteMode.Values()[(int)mode],
//                steps,
//                interval,
//                handler);
//        }

//        public Task<TripData> StartTrip(RadarTripOptions options)
//        {
//            var handler = new TripCallbackHandler();
//            AndroidBinding.Radar.StartTrip(options.ToBinding(), handler);
//            return handler.Task;
//        }

//        public Task<TripData> StartTrip(RadarTripOptions options, RadarTrackingOptions trackingOptions)
//        {
//            var handler = new TripCallbackHandler();
//            AndroidBinding.Radar.StartTrip(options.ToBinding(), trackingOptions?.ToBinding(), handler);
//            return handler.Task;
//        }

//        public Task<TripData> UpdateTrip(RadarTripOptions options, RadarTripStatus status = RadarTripStatus.Unknown)
//        {
//            var handler = new TripCallbackHandler();
//            AndroidBinding.Radar.UpdateTrip(options.ToBinding(), status.ToBinding(), handler);
//            return handler.Task;
//        }

//        public Task<TripData> CancelTrip()
//        {
//            var handler = new TripCallbackHandler();
//            AndroidBinding.Radar.CancelTrip(handler);
//            return handler.Task;
//        }

//        public Task<TripData> CompleteTrip()
//        {
//            var handler = new TripCallbackHandler();
//            AndroidBinding.Radar.CompleteTrip(handler);
//            return handler.Task;
//        }

//        public Task<AddressesData> Autocomplete(string query, RadarLocation near, int limit)
//        {
//            var handler = new GeocodeCallbackHandler();
//            AndroidBinding.Radar.Autocomplete(query, near?.ToBinding(), new Java.Lang.Integer(limit), handler);
//            return handler.Task;
//        }

//        public Task<AddressesData> Autocomplete(string query, RadarLocation near = null, IEnumerable<string> layers = null, int limit = 100, string country = null)
//        {
//            var handler = new GeocodeCallbackHandler();
//            AndroidBinding.Radar.Autocomplete(query, near?.ToBinding(), layers?.ToArray(), new Java.Lang.Integer(limit), country, handler);
//            return handler.Task;
//        }

//        public Task<AddressesData> Geocode(string query)
//        {
//            var handler = new GeocodeCallbackHandler();
//            AndroidBinding.Radar.Geocode(query, handler);
//            return handler.Task;
//        }

//        public Task<AddressesData> ReverseGeocode()
//        {
//            var handler = new GeocodeCallbackHandler();
//            AndroidBinding.Radar.ReverseGeocode(handler);
//            return handler.Task;
//        }

//        public Task<AddressesData> ReverseGeocode(RadarLocation location)
//        {
//            var handler = new GeocodeCallbackHandler();
//            AndroidBinding.Radar.ReverseGeocode(location?.ToBinding(), handler);
//            return handler.Task;
//        }

//        public Task<GeofencesData> SearchGeofences(RadarLocation near, int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
//        {
//            var handler = new SearchGeofencesCallbackHandler();
//            AndroidBinding.Radar.SearchGeofences(near?.ToBinding(), radius, tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
//            return handler.Task;
//        }

//        public Task<GeofencesData> SearchGeofences(int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
//        {
//            var handler = new SearchGeofencesCallbackHandler();
//            AndroidBinding.Radar.SearchGeofences(radius, tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
//            return handler.Task;
//        }

//        public Task<PlacesData> SearchPlaces(RadarLocation near, int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 0, IDictionary<string, string> chainMetadata = null)
//        {
//            var handler = new SearchPlacesCallbackHandler();
//            AndroidBinding.Radar.SearchPlaces(near?.ToBinding(), radius, chains?.ToArray(), chainMetadata, categories?.ToArray(), groups?.ToArray(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
//            return handler.Task;
//        }

//        public Task<PlacesData> SearchPlaces(int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 0, IDictionary<string, string> chainMetadata = null)
//        {
//            var handler = new SearchPlacesCallbackHandler();
//            AndroidBinding.Radar.SearchPlaces(radius, chains?.ToArray(), chainMetadata, categories?.ToArray(), groups?.ToArray(), limit == 0 ? null : new Java.Lang.Integer(limit), handler);
//            return handler.Task;
//        }

//        public Task<RoutesData> GetDistance(RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
//        {
//            var handler = new RouteCallbackHandler();
//            AndroidBinding.Radar.GetDistance(destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
//            return handler.Task;
//        }

//        public Task<RoutesData> GetDistance(RadarLocation source, RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
//        {
//            var handler = new RouteCallbackHandler();
//            AndroidBinding.Radar.GetDistance(source?.ToBinding(), destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
//            return handler.Task;
//        }

//        public Task<RouteMatrixData> GetMatrix(IEnumerable<RadarLocation> origins, IEnumerable<RadarLocation> destinations, RadarRouteMode mode, RadarRouteUnits units)
//        {
//            var handler = new MatrixCallbackHandler();
//            AndroidBinding.Radar.GetMatrix(origins?.Select(Conversion.ToBinding).ToArray(), destinations?.Select(Conversion.ToBinding).ToArray(), AndroidBinding.Radar.RadarRouteMode.Values()[(int)mode], AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
//            return handler.Task;
//        }

//        public Task<AddressData> IpGeocode()
//        {
//            var handler = new IpGeocodeCallbackHandler();
//            AndroidBinding.Radar.IpGeocode(handler);
//            return handler.Task;
//        }

//        public Task<LocationData> GetLocation()
//        {
//            var handler = new LocationCallbackHandler();
//            AndroidBinding.Radar.GetLocation(handler);
//            return handler.Task;
//        }

//        public Task<LocationData> GetLocation(RadarTrackingOptionsDesiredAccuracy desiredAccuracy)
//        {
//            var handler = new LocationCallbackHandler();
//            AndroidBinding.Radar.GetLocation(desiredAccuracy.ToBinding(), handler);
//            return handler.Task;
//        }

//        public void AcceptEventId(string eventId, string verifiedPlaceId = null)
//        {
//            AndroidBinding.Radar.AcceptEvent(eventId, verifiedPlaceId);
//        }

//        public void RejectEventId(string eventId)
//        {
//            AndroidBinding.Radar.RejectEvent(eventId);
//        }

//        public Task<ContextData> GetContext()
//        {
//            var handler = new ContextCallbackHandler();
//            AndroidBinding.Radar.GetContext(handler);
//            return handler.Task;
//        }

//        public Task<ContextData> GetContext(RadarLocation location)
//        {
//            var handler = new ContextCallbackHandler();
//            AndroidBinding.Radar.GetContext(location?.ToBinding(), handler);
//            return handler.Task;
//        }

//        public Task<EventData> LogConversion(string name, JSONObject metadata)
//        {
//            var handler = new LogConversionCallbackHandler();
//            AndroidBinding.Radar.LogConversion(name, metadata?.ToBinding(), handler);
//            return handler.Task;
//        }

//        public Task<EventData> LogConversion(string name, double revenue, JSONObject metadata)
//        {
//            var handler = new LogConversionCallbackHandler();
//            AndroidBinding.Radar.LogConversion(name, revenue, metadata?.ToBinding(), handler);
//            return handler.Task;
//        }

//        public string StringForStatus(RadarStatus status)
//            => status.ToString();

//        public string StringForLocationSource(RadarLocationSource source)
//            => AndroidBinding.Radar.StringForSource(source.ToBinding());

//        public string StringForMode(RadarRouteMode mode)
//            => AndroidBinding.Radar.StringForMode(mode.ToBinding());

//        public string StringForTripStatus(RadarTripStatus status)
//            => AndroidBinding.Radar.StringForTripStatus(status.ToBinding());

//        public JSONObject DictionaryForLocation(RadarLocation location)
//            => AndroidBinding.Radar.JsonForLocation(location?.ToBinding())?.ToSDK();
//    }

//    internal class RadarRouteMatrixImpl : RadarRouteMatrix
//    {
//        public override RadarRoute RouteBetween(int originIndex, int destinationIndex)
//            => matrix.ElementAtOrDefault(originIndex)?.ElementAtOrDefault(destinationIndex);
//    }
//}
