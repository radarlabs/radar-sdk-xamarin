using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadarIO
{
    public interface RadarSDK
    {
        RadarTrackingOptions TrackingOptionsResponsive { get; }
        RadarTrackingOptions TrackingOptionsContinuous { get; }
        RadarTrackingOptions TrackingOptionsEfficient { get; }

        event RadarEventHandler<EventsData> EventsReceived;
        event RadarEventHandler<LocationUpdatedData> LocationUpdated;
        event RadarEventHandler<ClientLocationUpdatedData> ClientLocationUpdated;
        event RadarEventHandler<RadarStatus> Error;
        event RadarEventHandler<string> Log;
        event RadarEventHandler<string> TokenUpdated;

        Task<EventData> LogConversion(string name, JSONObject metadata);
        Task<EventData> LogConversion(string name, double revenue, JSONObject metadata);

        void Initialize(string publishableKey, bool fraud = false);
        void SetLogLevel(RadarLogLevel level);
        /// <summary>
        /// Android-only
        /// </summary>
        void SetNotificationOptions(RadarNotificationOptions options);
        /// <summary>
        /// Android-only
        /// </summary>
        void SetForegroundServiceOptions(RadarTrackingOptionsForegroundService options);

        // todo: request permissions

        string UserId { get; set; }
        string Description { get; set; }
        string SdkVersion { get; }
        JSONObject Metadata { get; set; }
        bool AnonymousTrackingEnabled { set; }
        // todo: permission status

        Task<LocationData> GetLocation();
        Task<LocationData> GetLocation(RadarTrackingOptionsDesiredAccuracy desiredAccuracy);

        Task<TrackData> TrackOnce();
        Task<TrackData> TrackOnce(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons);
        Task<TrackData> TrackOnce(RadarLocation location);
        Task<TrackData> TrackVerified(bool beacons);
        Task<TokenData> TrackVerifiedToken(bool beacons);
        void StartTracking(RadarTrackingOptions options);
        void StartTrackingVerified(bool token, int interval, bool beacons);
        void MockTracking(RadarLocation origin, RadarLocation destination, RadarRouteMode mode, int steps, int interval, Action<TrackData> callback);
        void StopTracking();
        bool IsTracking { get; }
        RadarTrackingOptions TrackingOptions { get; }
        bool IsUsingRemoteTrackingOptions { get; }

        // Event IDs
        void AcceptEventId(string eventId, string verifiedPlaceId = null);
        void RejectEventId(string eventId);

        // Trips
        RadarTripOptions TripOptions { get; }
        Task<TripData> StartTrip(RadarTripOptions options);
        Task<TripData> StartTrip(RadarTripOptions options, RadarTrackingOptions trackingOptions);
        Task<TripData> UpdateTrip(RadarTripOptions options, RadarTripStatus status = RadarTripStatus.Unknown);
        Task<TripData> CompleteTrip();
        Task<TripData> CancelTrip();

        // Device Context
        Task<ContextData> GetContext();
        Task<ContextData> GetContext(RadarLocation location);

        // Search
        Task<PlacesData> SearchPlaces(RadarLocation near, int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 100, IDictionary<string, string> chainMetadata = null);
        Task<PlacesData> SearchPlaces(int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 100, IDictionary<string, string> chainMetadata = null);
        Task<GeofencesData> SearchGeofences(RadarLocation near, int radius, IEnumerable<string> tags = null, JSONObject metadata = null, int limit = 100);
        Task<GeofencesData> SearchGeofences(int radius, IEnumerable<string> tags = null, JSONObject metadata = null, int limit = 100);
        Task<AddressesData> Autocomplete(string query, RadarLocation near = null, int limit = 100);
        Task<AddressesData> Autocomplete(string query, RadarLocation near = null, IEnumerable<string> layers = null, int limit = 100, string country = null);

        // Geocoding
        Task<AddressesData> Geocode(string query);
        Task<AddressesData> ReverseGeocode();
        Task<AddressesData> ReverseGeocode(RadarLocation location);
        Task<AddressData> IpGeocode();

        // Distances
        Task<RoutesData> GetDistance(RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units);
        Task<RoutesData> GetDistance(RadarLocation source, RadarLocation destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units);
        Task<RouteMatrixData> GetMatrix(IEnumerable<RadarLocation> origins, IEnumerable<RadarLocation> destinations, RadarRouteMode mode, RadarRouteUnits units);

        // Utilities
        string StringForStatus(RadarStatus status);
        string StringForLocationSource(RadarLocationSource source);
        string StringForMode(RadarRouteMode mode);
        string StringForTripStatus(RadarTripStatus status);
        JSONObject DictionaryForLocation(RadarLocation location);
    }

    public enum RadarLogLevel
    {
        None,
        Error,
        Warning,
        Info,
        Debug
    }

    public class RadarLocation
    {
        public double? Accuracy { get; set; }
        public double? Altitude { get; set; }
        // ars?
        public double? Course { get; set; }
        public bool IsFromMockProvider { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Speed { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public double? VerticalAccuracy { get; set; }
    }

    public class RadarContext
    {
        public RadarRegion Country;
        public RadarRegion Dma;
        public IEnumerable<RadarGeofence> Geofences;
        public RadarPlace Place;
        public RadarRegion PostalCode;
        public RadarRegion State;
    }

    public abstract class RadarRouteMatrix
    {
        public IEnumerable<IEnumerable<RadarRoute>> matrix;

        public abstract RadarRoute RouteBetween(int originIndex, int destinationIndex);
    }

    public delegate void RadarEventHandler<T>(T args);

    public enum RadarRouteUnits
    {
        Imperial,
        Metric
    }

    public class RadarRoutes
    {
        /// <summary>
        /// iOS only uses distance property
        /// </summary>
        public RadarRoute Geodesic;
        public RadarRoute Foot;
        public RadarRoute Bike;
        public RadarRoute Car;
        public RadarRoute Truck;
        public RadarRoute Motorbike;
    }

    public class RadarRoute
    {
        public RadarRouteDistance Distance;
        public RadarRouteDuration Duration;
        public RadarRouteGeometry Geometry;
    }

    public class RadarRouteGeometry
    {
        public IEnumerable<RadarCoordinate> Coordinates;
    }

    public class RadarRouteDuration
    {
        public double Value;
        public string Text;
    }

    public class RadarRouteDistance
    {
        public double Value;
        public string Text;
    }

    public class RadarAddress
    {
        public RadarCoordinate Coordinate;
        public string FormattedAddress;
        public string Country;
        public string CountryCode;
        public string CountryFlag;
        public string Dma;
        public string DmaCode;
        public string State;
        public string StateCode;
        public string PostalCode;
        public string City;
        public string Borough;
        public string County;
        public string Neighborhood;
        public string Number;
        public string AddressLabel;
        public string PlaceLabel;
        public RadarAddressConfidence Confidence;

        /// <summary>
        /// Android-only
        /// </summary>
        public string Street;
        public string Unit;
        public string Plus4;
        public string Layer;
        public JSONObject Metadata;
    }

    public enum RadarAddressConfidence
    {
        None,
        Exact,
        Interpolated,
        Fallback
    }

    public class RadarTripOptions
    {
        public string ExternalId;
        public string DestinationGeofenceTag;
        public string DestinationGeofenceExternalId;
        public RadarRouteMode Mode;
        public JSONObject Metadata;
        public DateTime? ScheduledArrivalAt;
        public int ApproachingThreshold;
    }

    public class RadarTrackingOptions
    {
        public int DesiredStoppedUpdateInterval;
        public int DesiredMovingUpdateInterval;
        public int DesiredSyncInterval;
        public RadarTrackingOptionsDesiredAccuracy DesiredAccuracy;
        public int StopDuration;
        public int StopDistance;
        public DateTime? StartTrackingAfter;
        public DateTime? StopTrackingAfter;
        public RadarTrackingOptionsReplay Replay;
        public RadarTrackingOptionsSync Sync;
        public bool UseStoppedGeofence;
        public int StoppedGeofenceRadius;
        public bool UseMovingGeofence;
        public int MovingGeofenceRadius;
        public bool SyncGeofences;
        public bool Beacons;

        /// <summary>
        /// Android-only
        /// </summary>
        public int FastestStoppedUpdateInterval;

        /// <summary>
        /// Android-only
        /// </summary>
        public int FastestMovingUpdateInterval;

        /// <summary>
        /// Android-only
        /// </summary>
        public int SyncGeofencesLimit;

        /// <summary>
        /// Android-only
        /// </summary>
        public bool ForegroundServiceEnabled;


        /// <summary>
        /// iOS-only
        /// </summary>
        public bool ShowBlueBar;

        /// <summary>
        /// iOS-only
        /// </summary>
        public bool UseVisits;

        /// <summary>
        /// iOS-only
        /// </summary>
        public bool UseSignificantLocationChanges;
    }

    /// <summary>
    /// Android-only
    /// </summary>
    public class RadarTrackingOptionsForegroundService
    {
        public string Text;
        public string Title;
        public int Icon;
        public bool UpdatesOnly;
        public string Activity;
        public int Importance;
        public int Id;
        public string ChannelName;
    }

    public enum RadarTrackingOptionsSync
    {
        None,
        StopsAndExits,
        All
    }

    public enum RadarTrackingOptionsReplay
    {
        Stops,
        None
    }

    public enum RadarTrackingOptionsDesiredAccuracy
    {
        High,
        Medium,
        Low,
        /// <summary>
        ///  Android-only
        /// </summary>
        None
    }

    public class RadarUser
    {
        public string Id;
        public string UserId;
        public string DeviceId;
        public string Description;
        public JSONObject Metadata;
        public RadarLocation Location;
        public IEnumerable<RadarGeofence> Geofences;
        public RadarPlace Place;
        public IEnumerable<RadarBeacon> Beacons;
        public bool Stopped;
        public bool Foreground;
        public RadarRegion Country;
        public RadarRegion State;
        public RadarRegion DMA;
        public RadarRegion PostalCode;
        public IEnumerable<RadarChain> NearbyPlaceChains;
        public IEnumerable<RadarSegment> Segments;
        public IEnumerable<RadarChain> TopChains;
        public RadarLocationSource Source;
        public RadarTrip Trip;
        public RadarFraud Fraud;

        /// <summary>
        /// Android-only
        /// </summary>
        public bool Debug;
    }

    public class RadarTrip
    {
        public string Id;
        public string ExternalId;
        public JSONObject Metadata;
        public string DestinationGeofenceTag;
        public string DestinationGeofenceExternalId;
        public RadarCoordinate DestinationLocation;
        public RadarRouteMode? Mode;
        public double EtaDistance;
        public double EtaDuration;
        public RadarTripStatus Status;
    }

    public enum RadarTripStatus
    {
        Unknown,
        Started,
        Approaching,
        Arrived,
        Expired,
        Completed,
        Canceled
    }

    [Flags]
    public enum RadarRouteMode
    {
        Foot,
        Bike,
        Car,
        Truck,
        Motorbike
    }

    public enum RadarLocationSource
    {
        ForegroundLocation,
        BackgroundLocation,
        ManualLocation,
        VisitArrival,
        VisitDeparture,
        GeofenceEnter,
        GeofenceExit,
        MockLocation,
        BeaconEnter,
        BeaconExit,
        Unknown
    }

    public class RadarSegment
    {
        public string Description;
        public string ExternalId;
    }

    public class RadarChain
    {
        public string Slug;
        public string Name;
        public string ExternalId;
        public JSONObject Metadata;
    }


    public class RadarRegion
    {
        public string Id;
        public string Name;
        public string Code;
        public string Type;
        public string Flag;
        public bool Allowed;
    }

    public class RadarBeacon
    {
        public string Id;
        public string Description;
        public string Tag;
        public string ExternalId;
        public string UUID;
        public string Major;
        public string Minor;
        public JSONObject Metadata;
        public RadarCoordinate Location;
        public int Rssi;
    }

    public class RadarPlace
    {
        public string Id;
        public string Name;
        public IEnumerable<string> Categories;
        public RadarChain Chain;
        public RadarCoordinate Location;
        public string Group;
        public JSONObject Metadata;
    }

    public class RadarGeofence
    {
        public string Id;
        public string Description;
        public string Tag;
        public string ExternalId;
        public JSONObject Metadata;
        public RadarGeofenceGeometry Geometry;
    }

    public abstract class RadarGeofenceGeometry { }

    public class RadarCircleGeometry : RadarGeofenceGeometry
    {
        public RadarCoordinate Center;
        public double Radius;
    }

    public class RadarPolygonGeometry : RadarGeofenceGeometry
    {
        public RadarCoordinate Center;
        public IEnumerable<RadarCoordinate> Coordinates;
        public double Radius;
    }

    public class RadarCoordinate
    {
        public double Latitude;
        public double Longitude;
    }

    public class RadarEvent
    {
        public string Id;
        public DateTime? CreatedAt;
        public DateTime? ActualCreatedAt;
        public bool Live;
        public RadarEventType Type;
        public string ConversionName;
        public RadarGeofence Geofence;
        public RadarPlace Place;
        public RadarRegion Region;
        public RadarBeacon Beacon;
        public RadarTrip Trip;
        public IEnumerable<RadarPlace> AlternatePlaces;
        public RadarPlace VerifiedPlace;
        public RadarEventVerification Verification;
        public RadarEventConfidence Confidence;
        public float Duration;
        public RadarLocation Location;
        public JSONObject Metadata;
    }

    public class RadarFraud
    {
        public bool Passed;
        public bool Bypassed;
        public bool Verified;
        public bool Proxy;
        public bool Mocked;
        public bool Compromised;
        public bool Jumped;

        /// <summary>
        /// Android-only
        /// </summary>
        public bool Sharing;
    }

    public class RadarMeta
    {
        public RadarTrackingOptions TrackingOptions;
    }

    public class RadarNotificationOptions
    {
        public string IconString;
        public string IconColor;
        public string ForegroundServiceIconString;
        public string ForegroundServiceIconColor;
        public string EventIconString;
        public string EventIconColor;

        public string ForegroundServiceIcon => ForegroundServiceIconString ?? IconString;
        public string ForegroundServiceColor => ForegroundServiceIconColor ?? IconColor;
        public string EventIcon => EventIconString ?? IconString;
        public string EventColor => EventIconColor ?? IconColor;
    }

    public enum RadarEventConfidence
    {
        None,
        Low,
        Medium,
        High
    }

    public enum RadarEventVerification
    {
        Accept = 1,
        Unverify = 0,
        Reject = -1
    }

    public enum RadarEventType
    {
        Unknown,
        Conversion,
        UserEnteredGeofence,
        UserExitedGeofence,
        UserEnteredPlace,
        UserExitedPlace,
        UserNearbyPlaceChain,
        UserEnteredRegionCountry,
        UserExitedRegionCountry,
        UserEnteredRegionState,
        UserExitedRegionState,
        UserEnteredRegionDMA,
        UserExitedRegionDMA,
        UserStartedTrip,
        UserUpdatedTrip,
        UserApproachingTripDestination,
        UserArrivedAtTripDestination,
        UserStoppedTrip,
        UserEnteredBeacon,
        UserExitedBeacon,
        UserEnteredRegionPostalCode,
        UserExitedRegionPostalCode,
        UserDwelledInGeofence
    }

    public enum RadarStatus
    {
        Success,
        ErrorPublishableKey,
        ErrorPermissions,
        ErrorLocation,
        ErrorBluetooth,
        ErrorNetwork,
        ErrorBadRequest,
        ErrorUnauthorized,
        ErrorPaymentRequired,
        ErrorForbidden,
        ErrorNotFound,
        ErrorRateLimit,
        ErrorServer,
        ErrorUnknown
    }

    /// <summary>
    /// Android-only
    /// </summary>
    public enum RadarLocationServicesProvider
    {
        Google,
        Huawei
    }

    public class JSONObject : Dictionary<string, object> { }

    public record struct TrackData(RadarStatus Status, RadarLocation Location, IEnumerable<RadarEvent> Events, RadarUser User)
    {
        public static implicit operator (RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser)(TrackData value)
            => (value.Status, value.Location, value.Events, value.User);

        public static implicit operator TrackData((RadarStatus, RadarLocation, IEnumerable<RadarEvent>, RadarUser) value)
            => new(value.Item1, value.Item2, value.Item3, value.Item4);
    }

    public record struct TokenData(RadarStatus Status, string Token)
    {
        public static implicit operator (RadarStatus, string)(TokenData value)
            => (value.Status, value.Token);

        public static implicit operator TokenData((RadarStatus, string) value)
            => new(value.Item1, value.Item2);
    }

    public record struct TripData(RadarStatus Status, RadarTrip Trip, IEnumerable<RadarEvent> Events)
    {
        public static implicit operator (RadarStatus, RadarTrip, IEnumerable<RadarEvent>)(TripData value)
            => (value.Status, value.Trip, value.Events);

        public static implicit operator TripData((RadarStatus, RadarTrip, IEnumerable<RadarEvent>) value)
            => new(value.Item1, value.Item2, value.Item3);
    }

    public record struct ContextData(RadarStatus Status, RadarLocation Location, RadarContext Context)
    {
        public static implicit operator (RadarStatus, RadarLocation, RadarContext)(ContextData value)
            => (value.Status, value.Location, value.Context);

        public static implicit operator ContextData((RadarStatus, RadarLocation, RadarContext) value)
            => new(value.Item1, value.Item2, value.Item3);
    }

    public record struct PlacesData(RadarStatus Status, RadarLocation Location, IEnumerable<RadarPlace> Places)
    {
        public static implicit operator (RadarStatus, RadarLocation, IEnumerable<RadarPlace>)(PlacesData value)
            => (value.Status, value.Location, value.Places);

        public static implicit operator PlacesData((RadarStatus, RadarLocation, IEnumerable<RadarPlace>) value)
            => new(value.Item1, value.Item2, value.Item3);
    }

    public record struct GeofencesData(RadarStatus Status, RadarLocation Location, IEnumerable<RadarGeofence> Geofences)
    {
        public static implicit operator (RadarStatus, RadarLocation, IEnumerable<RadarGeofence>)(GeofencesData value)
            => (value.Status, value.Location, value.Geofences);

        public static implicit operator GeofencesData((RadarStatus, RadarLocation, IEnumerable<RadarGeofence>) value)
            => new(value.Item1, value.Item2, value.Item3);
    }

    public record struct AddressesData(RadarStatus Status, IEnumerable<RadarAddress> Addresses)
    {
        public static implicit operator (RadarStatus, IEnumerable<RadarAddress>)(AddressesData value)
            => (value.Status, value.Addresses);

        public static implicit operator AddressesData((RadarStatus, IEnumerable<RadarAddress>) value)
            => new(value.Item1, value.Item2);
    }

    public record struct AddressData(RadarStatus Status, RadarAddress Address, bool Proxy)
    {
        public static implicit operator (RadarStatus, RadarAddress, bool)(AddressData value)
            => (value.Status, value.Address, value.Proxy);

        public static implicit operator AddressData((RadarStatus, RadarAddress, bool) value)
            => new(value.Item1, value.Item2, value.Item3);
    }

    public record struct RoutesData(RadarStatus Status, RadarRoutes Routes)
    {
        public static implicit operator (RadarStatus, RadarRoutes)(RoutesData value)
            => (value.Status, value.Routes);

        public static implicit operator RoutesData((RadarStatus, RadarRoutes) value)
            => new(value.Item1, value.Item2);
    }

    public record struct RouteMatrixData(RadarStatus Status, RadarRouteMatrix RouteMatrix)
    {
        public static implicit operator (RadarStatus, RadarRouteMatrix)(RouteMatrixData value)
            => (value.Status, value.RouteMatrix);

        public static implicit operator RouteMatrixData((RadarStatus, RadarRouteMatrix) value)
            => new(value.Item1, value.Item2);
    }

    public record struct EventData(RadarStatus Status, RadarEvent Event)
    {
        public static implicit operator (RadarStatus, RadarEvent)(EventData value)
            => (value.Status, value.Event);

        public static implicit operator EventData((RadarStatus, RadarEvent) value)
            => new(value.Item1, value.Item2);
    }

    public record struct ClientLocationUpdatedData(RadarLocation Location, bool Stopped, RadarLocationSource Source)
    {
        public static implicit operator (RadarLocation, bool, RadarLocationSource)(ClientLocationUpdatedData value)
            => (value.Location, value.Stopped, value.Source);

        public static implicit operator ClientLocationUpdatedData((RadarLocation, bool, RadarLocationSource) value)
            => new(value.Item1, value.Item2, value.Item3);
    }

    public record struct LocationUpdatedData(RadarLocation Location, RadarUser User)
    {
        public static implicit operator (RadarLocation, RadarUser)(LocationUpdatedData value)
            => (value.Location, value.User);

        public static implicit operator LocationUpdatedData((RadarLocation, RadarUser) value)
            => new(value.Item1, value.Item2);
    }

    public record struct EventsData(IEnumerable<RadarEvent> Events, RadarUser User)
    {
        public static implicit operator (IEnumerable<RadarEvent>, RadarUser)(EventsData value)
            => (value.Events, value.User);

        public static implicit operator EventsData((IEnumerable<RadarEvent>, RadarUser) value)
            => new(value.Item1, value.Item2);
    }

    public record struct LocationData(RadarStatus Status, RadarLocation Location, bool Stopped)
    {
        public static implicit operator (RadarStatus, RadarLocation, bool)(LocationData value)
            => (value.Status, value.Location, value.Stopped);

        public static implicit operator LocationData((RadarStatus, RadarLocation, bool) value)
            => new(value.Item1, value.Item2, value.Item3);
    }
}