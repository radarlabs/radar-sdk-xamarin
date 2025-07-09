using Android.App;
using Android.Content;

namespace RadarIO;

public class RadarSDKImpl : RadarSDK
{
    public RadarTrackingOptions TrackingOptionsContinuous
        => AndroidBinding.RadarTrackingOptions.Continuous.ToSDK();
    public RadarTrackingOptions TrackingOptionsResponsive
        => AndroidBinding.RadarTrackingOptions.Responsive.ToSDK();
    public RadarTrackingOptions TrackingOptionsEfficient
        => AndroidBinding.RadarTrackingOptions.Efficient.ToSDK();

    public event RadarEventHandler<EventsData> EventsReceived;
    public event RadarEventHandler<LocationUpdatedData> LocationUpdated;
    public event RadarEventHandler<ClientLocationUpdatedData> ClientLocationUpdated;
    public event RadarEventHandler<RadarStatus> Error;
    public event RadarEventHandler<string> Log;
    public event RadarEventHandler<RadarVerifiedLocationToken> TokenUpdated;

    public void Initialize(string publishableKey, bool fraud = false, RadarInitializeOptions options = null)
    {
        var prefs = Android.App.Application.Context.GetSharedPreferences("RadarSDK", FileCreationMode.Private);
        var edit = prefs.Edit();
#if NET
        edit.PutString("x_platform_sdk_type", "Maui");
#else
        edit.PutString("x_platform_sdk_type", "Xamarin");
#endif
        edit.PutString("x_platform_sdk_version", "3.9.3");
        edit.Apply();

        if (fraud)
        {
            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey, new RadarReceiver(this), AndroidBinding.Radar.RadarLocationServicesProvider.Google, fraud);
            AndroidBinding.Radar.SetVerifiedReceiver(new RadarVerifiedReceiver(this));
        }
        else
        {
            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey);
            AndroidBinding.Radar.SetReceiver(new RadarReceiver(this));
        }
    }

    public void SetForegroundServiceOptions(RadarTrackingOptionsForegroundService options)
        => AndroidBinding.Radar.SetForegroundServiceOptions(options?.ToBinding());

    public void SetLogLevel(RadarLogLevel level)
        => AndroidBinding.Radar.SetLogLevel(AndroidBinding.Radar.RadarLogLevel.Values()[(int)level]);

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

    public string Product
    {
        get => AndroidBinding.Radar.Product;
        set => AndroidBinding.Radar.Product = value;
    }

    public bool AnonymousTrackingEnabled { set => AndroidBinding.Radar.SetAnonymousTrackingEnabled(value); }

    public bool IsTracking => AndroidBinding.Radar.IsTracking;

    public bool IsTrackingVerified => AndroidBinding.Radar.IsTrackingVerified;

    public RadarTrackingOptions TrackingOptions => AndroidBinding.Radar.TrackingOptions?.ToSDK();

    public RadarTripOptions TripOptions => AndroidBinding.Radar.TripOptions?.ToSDK();

    public string SdkVersion => AndroidBinding.Radar.SdkVersion();

    public bool IsUsingRemoteTrackingOptions => AndroidBinding.Radar.IsUsingRemoteTrackingOptions;

    // todo: add request permissions

    private static Task<T> UseHandler<H, T>(Action<H> action) where H : TaskCallbackHandler<T>, new()
    {
        var handler = new H();
        action(handler);
        return handler.Task;
    }

    public Task<TrackData> TrackOnce()
        => UseHandler<TrackCallbackHandler, TrackData>(AndroidBinding.Radar.TrackOnce);

    public Task<TrackData> TrackOnce(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons)
        => UseHandler<TrackCallbackHandler, TrackData>(handler => AndroidBinding.Radar.TrackOnce(desiredAccuracy.ToBinding(), beacons, handler));

    public Task<TrackData> TrackOnce(RadarLocation location)
        => UseHandler<TrackCallbackHandler, TrackData>(handler => AndroidBinding.Radar.TrackOnce(location?.ToBinding(), handler));

    public Task<TokenData> TrackVerified(bool beacons = false, RadarTrackingOptionsDesiredAccuracy desiredAccuracy = RadarTrackingOptionsDesiredAccuracy.Medium, string reason = null, string transactionId = null)
        => UseHandler<TrackTokenCallbackHandler, TokenData>(handler => AndroidBinding.Radar.TrackVerified(beacons, desiredAccuracy.ToBinding(), reason, transactionId, handler));

    public void StartTracking(RadarTrackingOptions options)
        => AndroidBinding.Radar.StartTracking(options.ToBinding());

    public void StartTrackingVerified(int interval = 1, bool beacons = false)
        => AndroidBinding.Radar.StartTrackingVerified(interval, beacons);

    public void StopTracking()
        => AndroidBinding.Radar.StopTracking();

    public void StopTrackingVerified()
        => AndroidBinding.Radar.StopTrackingVerified();

    public Task<TokenData> GetVerifiedLocationToken(bool beacons = false, RadarTrackingOptionsDesiredAccuracy desiredAccuracy = RadarTrackingOptionsDesiredAccuracy.Medium)
        => UseHandler<TrackTokenCallbackHandler, TokenData>(handler => AndroidBinding.Radar.GetVerifiedLocationToken(beacons, desiredAccuracy.ToBinding(), handler));

    public void ClearVerifiedLocationToken()
        => AndroidBinding.Radar.ClearVerifiedLocationToken();

    public void MockTracking(RadarLocation origin, RadarLocation destination, RadarRouteMode mode, int steps, int interval, Action<TrackData> callback)
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

    public void SetExpectedJurisdiction(string countryCode = null, string stateCode = null)
        => AndroidBinding.Radar.SetExpectedJurisdiction(countryCode, stateCode);

    public Task<TripData> StartTrip(RadarTripOptions options)
    {
        var handler = new TripCallbackHandler();
        AndroidBinding.Radar.StartTrip(options.ToBinding(), handler);
        return handler.Task;
    }

    public Task<TripData> StartTrip(RadarTripOptions options, RadarTrackingOptions trackingOptions)
    {
        var handler = new TripCallbackHandler();
        AndroidBinding.Radar.StartTrip(options.ToBinding(), trackingOptions?.ToBinding(), handler);
        return handler.Task;
    }

    public Task<TripData> UpdateTrip(RadarTripOptions options, RadarTripStatus status = RadarTripStatus.Unknown)
    {
        var handler = new TripCallbackHandler();
        AndroidBinding.Radar.UpdateTrip(options.ToBinding(), status.ToBinding(), handler);
        return handler.Task;
    }

    public Task<TripData> CancelTrip()
    {
        var handler = new TripCallbackHandler();
        AndroidBinding.Radar.CancelTrip(handler);
        return handler.Task;
    }

    public Task<TripData> CompleteTrip()
    {
        var handler = new TripCallbackHandler();
        AndroidBinding.Radar.CompleteTrip(handler);
        return handler.Task;
    }

    public Task<AddressesData> Autocomplete(string query, RadarLocation near, int limit)
    {
        var handler = new GeocodeCallbackHandler();
        AndroidBinding.Radar.Autocomplete(query, near?.ToBinding(), Java.Lang.Integer.ValueOf(limit), handler);
        return handler.Task;
    }

    public Task<AddressesData> Autocomplete(string query, RadarLocation near = null, IEnumerable<string> layers = null, int limit = 100, string country = null, bool expandUnits = false, bool mailable = false)
    {
        var handler = new GeocodeCallbackHandler();
        AndroidBinding.Radar.Autocomplete(query, near?.ToBinding(), layers?.ToArray(), Java.Lang.Integer.ValueOf(limit), country, Java.Lang.Boolean.ValueOf(expandUnits), Java.Lang.Boolean.ValueOf(mailable), handler);
        return handler.Task;
    }

    public Task<AddressesData> Geocode(string query, IEnumerable<string> layers = null, IEnumerable<string> countries = null)
    {
        var handler = new GeocodeCallbackHandler();
        AndroidBinding.Radar.Geocode(query, layers?.ToArray(), countries?.ToArray(), handler);
        return handler.Task;
    }

    public Task<AddressesData> ReverseGeocode(RadarLocation location = null, IEnumerable<string> layers = null)
    {
        var handler = new GeocodeCallbackHandler();
        if (location == null)
            AndroidBinding.Radar.ReverseGeocode(layers?.ToArray(), handler);
        else
            AndroidBinding.Radar.ReverseGeocode(location?.ToBinding(), layers?.ToArray(), handler);
        return handler.Task;
    }

    public Task<GeofencesData> SearchGeofences(int radius = -1, RadarLocation near = null, IEnumerable<string> tags = null, JSONObject metadata = null, int limit = 100, bool includeGeometry = false)
    {
        var handler = new SearchGeofencesCallbackHandler();
        if (near == null)
            AndroidBinding.Radar.SearchGeofences(radius < 0 ? null : Java.Lang.Integer.ValueOf(radius), tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : Java.Lang.Integer.ValueOf(limit), Java.Lang.Boolean.ValueOf(includeGeometry), handler);
        else
            AndroidBinding.Radar.SearchGeofences(near?.ToBinding(), radius < 0 ? null : Java.Lang.Integer.ValueOf(radius), tags?.ToArray(), metadata?.ToBinding(), limit == 0 ? null : Java.Lang.Integer.ValueOf(limit), Java.Lang.Boolean.ValueOf(includeGeometry), handler);
        return handler.Task;
    }

    public Task<PlacesData> SearchPlaces(int radius, RadarLocation near = null, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, IEnumerable<string> countryCodes = null, int limit = 100, IDictionary<string, string> chainMetadata = null)
    {
        var handler = new SearchPlacesCallbackHandler();
        if (near == null)
            AndroidBinding.Radar.SearchPlaces(radius, chains?.ToArray(), chainMetadata, categories?.ToArray(), groups?.ToArray(), countryCodes?.ToArray(), limit == 0 ? null : Java.Lang.Integer.ValueOf(limit), handler);
        else
            AndroidBinding.Radar.SearchPlaces(near?.ToBinding(), radius, chains?.ToArray(), chainMetadata, categories?.ToArray(), groups?.ToArray(), countryCodes?.ToArray(), limit == 0 ? null : Java.Lang.Integer.ValueOf(limit), handler);
        return handler.Task;
    }

    public Task<RoutesData> GetDistance(RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
    {
        var handler = new RouteCallbackHandler();
        AndroidBinding.Radar.GetDistance(destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
        return handler.Task;
    }

    public Task<RoutesData> GetDistance(RadarLocation source, RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
    {
        var handler = new RouteCallbackHandler();
        AndroidBinding.Radar.GetDistance(source?.ToBinding(), destination?.ToBinding(), modes?.ToBinding(), AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
        return handler.Task;
    }

    public Task<RouteMatrixData> GetMatrix(IEnumerable<RadarLocation> origins, IEnumerable<RadarLocation> destinations, RadarRouteMode mode, RadarRouteUnits units)
    {
        var handler = new MatrixCallbackHandler();
        AndroidBinding.Radar.GetMatrix(origins?.Select(Conversion.ToBinding).ToArray(), destinations?.Select(Conversion.ToBinding).ToArray(), AndroidBinding.Radar.RadarRouteMode.Values()[(int)mode], AndroidBinding.Radar.RadarRouteUnits.Values()[(int)units], handler);
        return handler.Task;
    }

    public Task<AddressData> IpGeocode()
    {
        var handler = new IpGeocodeCallbackHandler();
        AndroidBinding.Radar.IpGeocode(handler);
        return handler.Task;
    }

    public Task<LocationData> GetLocation()
    {
        var handler = new LocationCallbackHandler();
        AndroidBinding.Radar.GetLocation(handler);
        return handler.Task;
    }

    public Task<LocationData> GetLocation(RadarTrackingOptionsDesiredAccuracy desiredAccuracy)
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

    public Task<ContextData> GetContext()
    {
        var handler = new ContextCallbackHandler();
        AndroidBinding.Radar.GetContext(handler);
        return handler.Task;
    }

    public Task<ContextData> GetContext(RadarLocation location)
    {
        var handler = new ContextCallbackHandler();
        AndroidBinding.Radar.GetContext(location?.ToBinding(), handler);
        return handler.Task;
    }

    public Task<EventData> LogConversion(string name, JSONObject metadata)
    {
        var handler = new LogConversionCallbackHandler();
        AndroidBinding.Radar.LogConversion(name, metadata?.ToBinding(), handler);
        return handler.Task;
    }

    public Task<EventData> LogConversion(string name, double revenue, JSONObject metadata)
    {
        var handler = new LogConversionCallbackHandler();
        AndroidBinding.Radar.LogConversion(name, revenue, metadata?.ToBinding(), handler);
        return handler.Task;
    }

    public string StringForStatus(RadarStatus status)
        => status.ToString();

    public string StringForVerificationStatus(RadarAddressVerificationStatus status)
        => AndroidBinding.Radar.StringForVerificationStatus(status.ToBinding());

    public string StringForActivityType(RadarActivityType type)
        => type.ToString()?.ToLower();

    public string StringForLocationSource(RadarLocationSource source)
        => AndroidBinding.Radar.StringForSource(source.ToBinding());

    public string StringForMode(RadarRouteMode mode)
        => AndroidBinding.Radar.StringForMode(mode.ToBinding());

    public string StringForTripStatus(RadarTripStatus status)
        => AndroidBinding.Radar.StringForTripStatus(status.ToBinding());

    public JSONObject DictionaryForLocation(RadarLocation location)
        => AndroidBinding.Radar.JsonForLocation(location?.ToBinding())?.ToSDK();

    public void SetNotificationOptions(RadarNotificationOptions options)
        => AndroidBinding.Radar.SetNotificationOptions(options?.ToBinding());

    public void NativeSetup(RadarInitializeOptions options) { }

    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "io.radar.sdk.RECEIVED" })]
    class RadarReceiver : AndroidBinding.RadarReceiver
    {
        private readonly RadarSDKImpl Radar;

        public RadarReceiver(RadarSDKImpl radar)
        {
            Radar = radar;
        }

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
    }

    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "io.radar.sdk.RECEIVED" })]
    class RadarVerifiedReceiver : AndroidBinding.RadarVerifiedReceiver
    {
        private readonly RadarSDKImpl Radar;

        public RadarVerifiedReceiver(RadarSDKImpl radar)
        {
            Radar = radar;
        }

        public override void OnTokenUpdated(Context context, AndroidBinding.RadarVerifiedLocationToken token)
        {
            Radar.TokenUpdated?.Invoke(token?.ToSDK());
        }
    }
}

internal class RadarRouteMatrixImpl : RadarRouteMatrix
{
    public override RadarRoute RouteBetween(int originIndex, int destinationIndex)
        => matrix.ElementAtOrDefault(originIndex)?.ElementAtOrDefault(destinationIndex);
}