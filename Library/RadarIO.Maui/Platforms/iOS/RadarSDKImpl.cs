using CoreLocation;

namespace RadarIO;

public class RadarSDKImpl : iOSBinding.RadarDelegate, RadarSDK
{
    public RadarTrackingOptions TrackingOptionsContinuous
        => iOSBinding.RadarTrackingOptions.PresetContinuous.ToSDK();
    public RadarTrackingOptions TrackingOptionsResponsive
        => iOSBinding.RadarTrackingOptions.PresetResponsive.ToSDK();
    public RadarTrackingOptions TrackingOptionsEfficient
        => iOSBinding.RadarTrackingOptions.PresetEfficient.ToSDK();

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

    public void Initialize(string publishableKey, RadarLocationServicesProvider locationServicesProvider, bool fraud)
        => Initialize(publishableKey);

    public void SetForegroundServiceOptions(RadarTrackingOptionsForegroundService options) { }

    public void SetLogLevel(RadarLogLevel level)
    {
        iOSBinding.Radar.SetLogLevel((iOSBinding.RadarLogLevel)level);
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
    public bool AnonymousTrackingEnabled { set => iOSBinding.Radar.SetAnonymousTrackingEnabled(value); }

    public bool IsTracking => iOSBinding.Radar.IsTracking;

    public RadarTrackingOptions TrackingOptions => iOSBinding.Radar.TrackingOptions?.ToSDK();

    public RadarTripOptions TripOptions => iOSBinding.Radar.TripOptions?.ToSDK();

    public Task<(RadarStatus, RadarLocation, bool)> GetLocation()
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, bool)>();
        iOSBinding.Radar.GetLocationWithCompletionHandler((status, location, stopped) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), stopped));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, bool)> GetLocation(RadarTrackingOptionsDesiredAccuracy desiredAccuracy)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, bool)>();
        iOSBinding.Radar.GetLocationWithDesiredAccuracy(desiredAccuracy.ToBinding(), (status, location, stopped) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), stopped));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> TrackOnce()
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)>();
        iOSBinding.Radar.TrackOnceWithCompletionHandler((status, location, ev, user) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), ev?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> TrackOnce(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)>();
        iOSBinding.Radar.TrackOnceWithDesiredAccuracy(desiredAccuracy.ToBinding(), beacons, (status, location, ev, user) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), ev?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)> TrackOnce(RadarLocation location)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)>();
        iOSBinding.Radar.TrackOnceWithLocation(location?.ToBinding(), (status, _location, ev, user) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), _location?.ToSDK(), ev?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
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

    public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> StartTrip(RadarTripOptions options)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)>();
        iOSBinding.Radar.StartTripWithOptions(options.ToBinding(), (status, trip, events) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), trip.ToSDK(), events?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> StartTrip(RadarTripOptions options, RadarTrackingOptions trackingOptions)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)>();
        iOSBinding.Radar.StartTripWithOptions(options.ToBinding(), trackingOptions?.ToBinding(), (status, trip, events) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), trip.ToSDK(), events?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> UpdateTrip(RadarTripOptions options, RadarTripStatus status = RadarTripStatus.Unknown)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)>();
        iOSBinding.Radar.UpdateTripWithOptions(options?.ToBinding(), status.ToBinding(), (_status, trip, events) =>
        {
            try
            {
                src.SetResult((_status.ToSDK(), trip.ToSDK(), events?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> CancelTrip()
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)>();
        iOSBinding.Radar.CancelTripWithCompletionHandler((status, trip, events) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), trip.ToSDK(), events?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> CompleteTrip()
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)>();
        iOSBinding.Radar.CompleteTripWithCompletionHandler((status, trip, events) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), trip.ToSDK(), events?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, RadarLocation near, int limit)
    {
        var src = new TaskCompletionSource<(RadarStatus, IEnumerable<RadarAddress>)>();
        iOSBinding.Radar.AutocompleteQuery(query, near?.ToBinding(), limit, (status, addresses) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), addresses?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, RadarLocation near = null, IEnumerable<string> layers = null, int limit = 100, string country = null)
    {
        var src = new TaskCompletionSource<(RadarStatus, IEnumerable<RadarAddress>)>();
        iOSBinding.Radar.AutocompleteQuery(query, near?.ToBinding(), limit, (status, addresses) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), addresses?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, IEnumerable<RadarAddress>)> Geocode(string query)
    {
        var src = new TaskCompletionSource<(RadarStatus, IEnumerable<RadarAddress>)>();
        iOSBinding.Radar.GeocodeAddress(query, (status, addresses) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), addresses?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, IEnumerable<RadarAddress>)> ReverseGeocode()
    {
        var src = new TaskCompletionSource<(RadarStatus, IEnumerable<RadarAddress>)>();
        iOSBinding.Radar.ReverseGeocodeWithCompletionHandler((status, addresses) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), addresses?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, IEnumerable<RadarAddress>)> ReverseGeocode(RadarLocation location)
    {
        var src = new TaskCompletionSource<(RadarStatus, IEnumerable<RadarAddress>)>();
        iOSBinding.Radar.ReverseGeocodeLocation(location?.ToBinding(), (status, addresses) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), addresses?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarAddress, bool)> IpGeocode()
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarAddress, bool)>();
        iOSBinding.Radar.IpGeocodeWithCompletionHandler((status, address, isProxy)  =>
        {
            try
            {
                src.SetResult((status.ToSDK(), address?.ToSDK(), isProxy));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)> SearchGeofences(RadarLocation near, int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)>();
        iOSBinding.Radar.SearchGeofencesNear(near?.ToBinding(), radius, tags?.ToArray(), metadata?.ToBinding(), limit, (status, location, geofences) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), geofences?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)> SearchGeofences(int radius, IEnumerable<string> tags, JSONObject metadata, int limit)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)>();
        iOSBinding.Radar.SearchGeofencesWithRadius(radius, tags?.ToArray(), metadata?.ToBinding(), limit, (status, location, geofences) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), geofences?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, IEnumerable<RadarPlace>)> SearchPlaces(RadarLocation near, int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 100, IDictionary<string, string> chainMetadata = null)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarPlace>)>();
        iOSBinding.Radar.SearchPlacesNear(near?.ToBinding(), radius, chains?.ToArray(), chainMetadata?.ToBinding(), categories?.ToArray(), groups?.ToArray(), limit, (status, location, places) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), places?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, IEnumerable<RadarPlace>)> SearchPlaces(int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 100, IDictionary<string, string> chainMetadata = null)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, IEnumerable<RadarPlace>)>();
        iOSBinding.Radar.SearchPlacesWithRadius(radius, chains?.ToArray(), chainMetadata?.ToBinding(), categories?.ToArray(), groups?.ToArray(), limit, (status, location, places) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), places?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarRoutes)> GetDistance(RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarRoutes)>();
        iOSBinding.Radar.GetDistanceToDestination(destination?.ToBinding(), modes?.ToBinding() ?? 0, (iOSBinding.RadarRouteUnits)units, (status, routes) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), routes?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarRoutes)> GetDistance(RadarLocation source, RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarRoutes)>();
        iOSBinding.Radar.GetDistanceFromOrigin(source?.ToBinding(), destination?.ToBinding(), modes?.ToBinding() ?? 0, (iOSBinding.RadarRouteUnits)units, (status, routes) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), routes?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarRouteMatrix)> GetMatrix(IEnumerable<RadarLocation> origins, IEnumerable<RadarLocation> destinations, RadarRouteMode mode, RadarRouteUnits units)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarRouteMatrix)>();
        iOSBinding.Radar.GetMatrixFromOrigins(origins?.Select(Conversion.ToBinding).ToArray(), destinations?.Select(Conversion.ToBinding).ToArray(), (iOSBinding.RadarRouteMode)mode, (iOSBinding.RadarRouteUnits)units, (status, matrix) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), matrix?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public void AcceptEventId(string eventId, string verifiedPlaceId = null)
    {
        iOSBinding.Radar.AcceptEventId(eventId, verifiedPlaceId);
    }

    public void RejectEventId(string eventId)
    {
        iOSBinding.Radar.RejectEventId(eventId);
    }

    public Task<(RadarStatus, RadarLocation, RadarContext)> GetContext()
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, RadarContext)>();
        iOSBinding.Radar.GetContextWithCompletionHandler((status, location, context) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), context?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarLocation, RadarContext)> GetContext(RadarLocation location)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarLocation, RadarContext)>();
        iOSBinding.Radar.GetContextForLocation(location?.ToBinding(), (status, _location, context) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), _location?.ToSDK(), context?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarEvent)> LogConversion(string name, JSONObject metadata)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarEvent)>();
        iOSBinding.Radar.LogConversionWithName(name, metadata?.ToBinding(), (status, events) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), events?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public Task<(RadarStatus, RadarEvent)> LogConversion(string name, double revenue, JSONObject metadata)
    {
        var src = new TaskCompletionSource<(RadarStatus, RadarEvent)>();
        iOSBinding.Radar.LogConversionWithName(name, revenue, metadata?.ToBinding(), (status, events) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), events?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public string StringForStatus(RadarStatus status)
        => iOSBinding.Radar.StringForStatus(status.ToBinding());

    public string StringForLocationSource(RadarLocationSource source)
        => iOSBinding.Radar.StringForLocationSource(source.ToBinding());

    public string StringForMode(RadarRouteMode mode)
        => iOSBinding.Radar.StringForMode(mode.ToBinding());

    public string StringForTripStatus(RadarTripStatus status)
        => iOSBinding.Radar.StringForTripStatus(status.ToBinding());

    public JSONObject DictionaryForLocation(RadarLocation location)
        => iOSBinding.Radar.DictionaryForLocation(location?.ToBinding())?.ToSDK();
}

internal class RadarRouteMatrixImpl : RadarRouteMatrix
{
    public override RadarRoute RouteBetween(int originIndex, int destinationIndex)
        => matrix.ElementAtOrDefault(originIndex)?.ElementAtOrDefault(destinationIndex);
}
