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

        event RadarEventHandler<(IEnumerable<RadarEvent>, RadarUser)> EventsReceived;
        event RadarEventHandler<(Location, RadarUser)> LocationUpdated;
        event RadarEventHandler<(Location, bool, RadarLocationSource)> ClientLocationUpdated;
        event RadarEventHandler<RadarStatus> Error;
        event RadarEventHandler<string> Log;

        Task<(RadarStatus, RadarEvent)> LogConversion(string name, JSONObject metadata);
        Task<(RadarStatus, RadarEvent)> LogConversion(string name, double revenue, JSONObject metadata);

        // Initialization
        void Initialize(string publishableKey);
        /// <summary>
        /// Android-only
        /// </summary>
        void Initialize(string publishableKey, RadarLocationServicesProvider locationServicesProvider, bool fraud);

        /// <summary>
        /// Android-only
        /// </summary>
        void SetForegroundServiceOptions(RadarTrackingOptionsForegroundService options);

        // Properties
        string UserId { get; set; }
        string Description { get; set; }
        JSONObject Metadata { get; set; }
        bool AnonymousTrackingEnabled { set; }

        // Get Location
        Task<(RadarStatus, Location, bool)> GetLocation();
        Task<(RadarStatus, Location, bool)> GetLocation(RadarTrackingOptionsDesiredAccuracy desiredAccuracy);

        // Tracking
        Task<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> TrackOnce();
        Task<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> TrackOnce(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons);
        Task<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> TrackOnce(Location location);
        void StartTracking(RadarTrackingOptions options);
        void MockTracking(Location origin, Location destination, RadarRouteMode mode, int steps, int interval, Action<(RadarStatus, Location, IEnumerable<RadarEvent>, RadarUser)> callback);
        void StopTracking();
        bool IsTracking { get; }
        RadarTrackingOptions TrackingOptions { get; }

        // Event IDs
        void AcceptEventId(string eventId, string verifiedPlaceId = null);
        void RejectEventId(string eventId);

        // Trips
        RadarTripOptions TripOptions { get; }
        Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> StartTrip(RadarTripOptions options);
        Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> StartTrip(RadarTripOptions options, RadarTrackingOptions trackingOptions);
        Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> UpdateTrip(RadarTripOptions options, RadarTripStatus status = RadarTripStatus.Unknown);
        Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> CompleteTrip();
        Task<(RadarStatus, RadarTrip, IEnumerable<RadarEvent>)> CancelTrip();

        // Device Context
        Task<(RadarStatus, Location, RadarContext)> GetContext();
        Task<(RadarStatus, Location, RadarContext)> GetContext(Location location);

        // Search
        Task<(RadarStatus, Location, IEnumerable<RadarPlace>)> SearchPlaces(Location near, int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 100, IDictionary<string, string> chainMetadata = null);
        Task<(RadarStatus, Location, IEnumerable<RadarPlace>)> SearchPlaces(int radius, IEnumerable<string> chains = null, IEnumerable<string> categories = null, IEnumerable<string> groups = null, int limit = 100, IDictionary<string, string> chainMetadata = null);
        Task<(RadarStatus, Location, IEnumerable<RadarGeofence>)> SearchGeofences(Location near, int radius, IEnumerable<string> tags = null, JSONObject metadata = null, int limit = 100);
        Task<(RadarStatus, Location, IEnumerable<RadarGeofence>)> SearchGeofences(int radius, IEnumerable<string> tags = null, JSONObject metadata = null, int limit = 100);
        Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, Location near = null, int limit = 100);
        Task<(RadarStatus, IEnumerable<RadarAddress>)> Autocomplete(string query, Location near = null, IEnumerable<string> layers = null, int limit = 100, string country = null);

        // Geocoding
        Task<(RadarStatus, IEnumerable<RadarAddress>)> Geocode(string query);
        Task<(RadarStatus, IEnumerable<RadarAddress>)> ReverseGeocode();
        Task<(RadarStatus, IEnumerable<RadarAddress>)> ReverseGeocode(Location location);
        Task<(RadarStatus, RadarAddress, bool)> IpGeocode();

        // Distances
        Task<(RadarStatus, RadarRoutes)> GetDistance(Location destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units);
        Task<(RadarStatus, RadarRoutes)> GetDistance(Location source, Location destination, IEnumerable<RadarRouteMode> modes, RadarRouteUnits units);
        Task<(RadarStatus, RadarRouteMatrix)> GetMatrix(IEnumerable<Location> origins, IEnumerable<Location> destinations, RadarRouteMode mode, RadarRouteUnits units);

        // Logging
        void SetLogLevel(RadarLogLevel level);

        // Utilities
        string StringForStatus(RadarStatus status);
        string StringForLocationSource(RadarLocationSource source);
        string StringForMode(RadarRouteMode mode);
        string StringForTripStatus(RadarTripStatus status);
        JSONObject DictionaryForLocation(Location location);
    }

    public enum RadarLogLevel
    {
        None,
        Error,
        Warning,
        Info,
        Debug
    }

    public class Location
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
        public Location Location;
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
        public Location Location;
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
        /// Android-onlyZ
        /// </summary>
        public bool Sharing;
    }

    public class RadarMeta
    {
        public RadarTrackingOptions TrackingOptions;
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

}