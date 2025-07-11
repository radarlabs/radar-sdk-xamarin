﻿using CoreLocation;
using Foundation;

namespace RadarIO;

public class RadarSDKImpl : RadarSDK
{
    public RadarTrackingOptions TrackingOptionsContinuous
        => iOSBinding.RadarTrackingOptions.PresetContinuous.ToSDK();
    public RadarTrackingOptions TrackingOptionsResponsive
        => iOSBinding.RadarTrackingOptions.PresetResponsive.ToSDK();
    public RadarTrackingOptions TrackingOptionsEfficient
        => iOSBinding.RadarTrackingOptions.PresetEfficient.ToSDK();

    #region RadarDelegate

    class RadarDelegate : iOSBinding.RadarDelegate
    {
        private readonly RadarSDKImpl Radar;

        public RadarDelegate(RadarSDKImpl radar) : base()
        {
            Radar = radar;
        }

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
    }

    class RadarVerifiedDelegate : iOSBinding.RadarVerifiedDelegate
    {
        private readonly RadarSDKImpl Radar;

        public RadarVerifiedDelegate(RadarSDKImpl radar)
        {
            Radar = radar;
        }

        public override void DidUpdateToken(iOSBinding.RadarVerifiedLocationToken token)
        {
            Radar.TokenUpdated?.Invoke(token?.ToSDK());
        }
    }

    #endregion

    public event RadarEventHandler<EventsData> EventsReceived;
    public event RadarEventHandler<LocationUpdatedData> LocationUpdated;
    public event RadarEventHandler<ClientLocationUpdatedData> ClientLocationUpdated;
    public event RadarEventHandler<RadarStatus> Error;
    public event RadarEventHandler<string> Log;
    public event RadarEventHandler<RadarVerifiedLocationToken> TokenUpdated;

    public void Initialize(string publishableKey, bool fraud = false, RadarInitializeOptions options = null)
    {
#if NET
        Preferences.Default.Set("radar-xPlatformSDKType", "Maui");
        Preferences.Default.Set("radar-xPlatformSDKVersion", "3.9.3");
#else
        var defaults = new Foundation.NSUserDefaults("RadarSDK");
        defaults.SetString("radar-xPlatformSDKType", "Xamarin");
        defaults.SetString("radar-xPlatformSDKVersion", "3.9.3");
        defaults.Synchronize();
#endif

        iOSBinding.Radar.InitializeWithPublishableKey(publishableKey, options?.ToBinding());
        iOSBinding.Radar.SetDelegate(new RadarDelegate(this));
        iOSBinding.Radar.SetVerifiedDelegate(new RadarVerifiedDelegate(this));
    }

    public void SetForegroundServiceOptions(RadarTrackingOptionsForegroundService options) { }

    public void SetLogLevel(RadarLogLevel level)
        => iOSBinding.Radar.SetLogLevel((iOSBinding.RadarLogLevel)level);

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

    public string Product
    {
        get => iOSBinding.Radar.Product;
        set => iOSBinding.Radar.SetProduct(value);
    }

    public bool AnonymousTrackingEnabled
    {
        set => iOSBinding.Radar.SetAnonymousTrackingEnabled(value);
    }

    public bool IsTracking => iOSBinding.Radar.IsTracking;

    public bool IsTrackingVerified => iOSBinding.Radar.IsTrackingVerified;

    public RadarTrackingOptions TrackingOptions => iOSBinding.Radar.TrackingOptions?.ToSDK();

    public RadarTripOptions TripOptions => iOSBinding.Radar.TripOptions?.ToSDK();

    public string SdkVersion => iOSBinding.Radar.SdkVersion;

    public bool IsUsingRemoteTrackingOptions => iOSBinding.Radar.IsUsingRemoteTrackingOptions;

    public Task<LocationData> GetLocation()
    {
        var src = new TaskCompletionSource<LocationData>();
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

    public Task<LocationData> GetLocation(RadarTrackingOptionsDesiredAccuracy desiredAccuracy)
    {
        var src = new TaskCompletionSource<LocationData>();
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

    public Task<TrackData> TrackOnce()
    {
        var src = new TaskCompletionSource<TrackData>();
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

    public Task<TrackData> TrackOnce(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons)
    {
        var src = new TaskCompletionSource<TrackData>();
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

    public Task<TrackData> TrackOnce(RadarLocation location)
    {
        var src = new TaskCompletionSource<TrackData>();
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

    public Task<TokenData> TrackVerified(bool beacons = false, RadarTrackingOptionsDesiredAccuracy desiredAccuracy = RadarTrackingOptionsDesiredAccuracy.Medium, string reason = null, string transactionId = null)
    {
        var src = new TaskCompletionSource<TokenData>();
        iOSBinding.Radar.TrackVerifiedWithBeacons(beacons, desiredAccuracy.ToBinding(), reason, transactionId, (status, token) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), token?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public void StartTracking(RadarTrackingOptions options)
        => iOSBinding.Radar.StartTrackingWithOptions(options.ToBinding());

    public void StartTrackingVerified(int interval = 1, bool beacons = false)
        => iOSBinding.Radar.StartTrackingVerifiedWithInterval(interval, beacons);

    public void StopTracking()
        => iOSBinding.Radar.StopTracking();

    public void StopTrackingVerified()
        => iOSBinding.Radar.StopTrackingVerified();

    public Task<TokenData> GetVerifiedLocationToken(bool beacons = false, RadarTrackingOptionsDesiredAccuracy desiredAccuracy = RadarTrackingOptionsDesiredAccuracy.Medium)
    {
        var src = new TaskCompletionSource<TokenData>();
        iOSBinding.Radar.GetVerifiedLocationTokenWithBeacons(beacons, desiredAccuracy.ToBinding(), (status, token) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), token?.ToSDK()));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        });
        return src.Task;
    }

    public void ClearVerifiedLocationToken()
        => iOSBinding.Radar.ClearVerifiedLocationToken();

    public void MockTracking(RadarLocation origin, RadarLocation destination, RadarRouteMode mode, int steps, int interval, Action<TrackData> callback)
        => iOSBinding.Radar.MockTrackingWithOrigin(
            origin?.ToBinding(),
            destination?.ToBinding(),
            (iOSBinding.RadarRouteMode)mode,
            steps,
            interval,
            (status, location, events, user) => callback?.Invoke((status.ToSDK(), location?.ToSDK(), events?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK())));

    public void SetExpectedJurisdiction(string countryCode = null, string stateCode = null)
        => iOSBinding.Radar.SetExpectedJurisdictionWithCountryCode(countryCode, stateCode);

    public Task<TripData> StartTrip(RadarTripOptions options)
    {
        var src = new TaskCompletionSource<TripData>();
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

    public Task<TripData> StartTrip(RadarTripOptions options, RadarTrackingOptions trackingOptions)
    {
        var src = new TaskCompletionSource<TripData>();
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

    public Task<TripData> UpdateTrip(RadarTripOptions options, RadarTripStatus status = RadarTripStatus.Unknown)
    {
        var src = new TaskCompletionSource<TripData>();
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

    public Task<TripData> CancelTrip()
    {
        var src = new TaskCompletionSource<TripData>();
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

    public Task<TripData> CompleteTrip()
    {
        var src = new TaskCompletionSource<TripData>();
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

    public Task<AddressesData> Autocomplete(string query, RadarLocation near, int limit)
    {
        var src = new TaskCompletionSource<AddressesData>();
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

    public Task<AddressesData> Autocomplete(string query, RadarLocation near = null, IEnumerable<string> layers = null, int limit = 100, string country = null, bool expandUnits = false, bool mailable = false)
    {
        var src = new TaskCompletionSource<AddressesData>();
        iOSBinding.Radar.AutocompleteQuery(query, near?.ToBinding(), layers?.ToArray(), limit, country, expandUnits, (status, addresses) =>
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

    public Task<AddressesData> Geocode(string query, IEnumerable<string> layers = null, IEnumerable<string> countries = null)
    {
        var src = new TaskCompletionSource<AddressesData>();
        iOSBinding.Radar.GeocodeAddress(query, layers?.ToArray(), countries?.ToArray(), (status, addresses) =>
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

    public Task<AddressesData> ReverseGeocode(RadarLocation location = null, IEnumerable<string> layers = null)
    {
        var src = new TaskCompletionSource<AddressesData>();
        iOSBinding.RadarGeocodeCompletionHandler handler = (status, addresses) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), addresses?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        };
        if (location == null)
            iOSBinding.Radar.ReverseGeocodeWithLayers(layers?.ToArray(), handler);
        else
            iOSBinding.Radar.ReverseGeocodeLocation(location.ToBinding(), layers?.ToArray(), handler);
        return src.Task;
    }

    public Task<AddressData> IpGeocode()
    {
        var src = new TaskCompletionSource<AddressData>();
        iOSBinding.Radar.IpGeocodeWithCompletionHandler((status, address, isProxy) =>
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

    public Task<GeofencesData> SearchGeofences(int radius = -1, RadarLocation near = null, IEnumerable<string> tags = null, JSONObject metadata = null, int limit = 100, bool includeGeometry = false)
    {
        var src = new TaskCompletionSource<GeofencesData>();
        iOSBinding.RadarSearchGeofencesCompletionHandler handler = (status, location, geofences) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), geofences?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        };
        iOSBinding.Radar.SearchGeofencesNear(near?.ToBinding(), radius, tags?.ToArray(), metadata?.ToBinding(), limit, includeGeometry, handler);
        return src.Task;
    }

    public Task<PlacesData> SearchPlaces(int radius, RadarLocation near = null, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, IEnumerable<string> countryCodes = null, int limit = 100, IDictionary<string, string> chainMetadata = null)
    {
        var src = new TaskCompletionSource<PlacesData>();
        iOSBinding.RadarSearchPlacesCompletionHandler handler = (status, location, places) =>
        {
            try
            {
                src.SetResult((status.ToSDK(), location?.ToSDK(), places?.Select(Conversion.ToSDK)));
            }
            catch (Exception ex)
            {
                src.SetException(ex);
            }
        };
        if (near == null)
            iOSBinding.Radar.SearchPlacesWithRadius(radius, chains?.ToArray(), chainMetadata?.ToBinding(), categories?.ToArray(), groups?.ToArray(), countryCodes?.ToArray(), limit, handler);
        else
            iOSBinding.Radar.SearchPlacesNear(near.ToBinding(), radius, chains?.ToArray(), chainMetadata?.ToBinding(), categories?.ToArray(), groups?.ToArray(), countryCodes?.ToArray(), limit, handler);
        return src.Task;
    }

    public Task<RoutesData> GetDistance(RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
    {
        var src = new TaskCompletionSource<RoutesData>();
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

    public Task<RoutesData> GetDistance(RadarLocation source, RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units)
    {
        var src = new TaskCompletionSource<RoutesData>();
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

    public Task<RouteMatrixData> GetMatrix(IEnumerable<RadarLocation> origins, IEnumerable<RadarLocation> destinations, RadarRouteMode mode, RadarRouteUnits units)
    {
        var src = new TaskCompletionSource<RouteMatrixData>();
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

    public Task<ContextData> GetContext()
    {
        var src = new TaskCompletionSource<ContextData>();
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

    public Task<ContextData> GetContext(RadarLocation location)
    {
        var src = new TaskCompletionSource<ContextData>();
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

    public Task<EventData> LogConversion(string name, JSONObject metadata)
    {
        var src = new TaskCompletionSource<EventData>();
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

    public Task<EventData> LogConversion(string name, double revenue, JSONObject metadata)
    {
        var src = new TaskCompletionSource<EventData>();
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

    public void SetNotificationOptions(RadarNotificationOptions options) { }

    public string StringForStatus(RadarStatus status)
        => iOSBinding.Radar.StringForStatus(status.ToBinding());

    public string StringForVerificationStatus(RadarAddressVerificationStatus status)
        => iOSBinding.Radar.StringForVerificationStatus(status.ToBinding());

    public string StringForActivityType(RadarActivityType type)
        => iOSBinding.Radar.StringForActivityType(type.ToBinding());

    public string StringForLocationSource(RadarLocationSource source)
        => iOSBinding.Radar.StringForLocationSource(source.ToBinding());

    public string StringForMode(RadarRouteMode mode)
        => iOSBinding.Radar.StringForMode(mode.ToBinding());

    public string StringForTripStatus(RadarTripStatus status)
        => iOSBinding.Radar.StringForTripStatus(status.ToBinding());

    public JSONObject DictionaryForLocation(RadarLocation location)
        => iOSBinding.Radar.DictionaryForLocation(location?.ToBinding())?.ToSDK();

    public void NativeSetup(RadarInitializeOptions options)
        => iOSBinding.Radar.NativeSetup(options?.ToBinding());
}

internal class RadarRouteMatrixImpl : RadarRouteMatrix
{
    public override RadarRoute RouteBetween(int originIndex, int destinationIndex)
        => matrix.ElementAtOrDefault(originIndex)?.ElementAtOrDefault(destinationIndex);
}
