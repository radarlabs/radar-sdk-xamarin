using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadarIO.Xamarin
{
    public abstract class RadarSDK
    {
        public abstract void Initialize(string publishableKey);
        public abstract Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce();
        public abstract void StartTracking(RadarTrackingOptions options);
        public abstract void StopTracking();
    }

    public partial class RadarTrackingOptions
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
        public RadarUserInsights Insights;
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
        public bool Proxy;
        public RadarTrip Trip;
    }

    public class RadarTrip
    {
        public string Id;
        public string ExternalId;
        public JSONObject Metadata;
        public string DestinationGeofenceTag;
        public string DestinationGeofenceExternalId;
        public RadarCoordinate DestinationLocation;
        public RadarRouteMode Mode;
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
        Foot = 1,
        Bike = 2,
        Car = 4,
        Truck = 8,
        Motorbike = 16
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
    }

    public class RadarUserInsights
    {
        public RadarUserInsightsLocation HomeLocation;
        public RadarUserInsightsLocation OfficeLocation;
        public RadarUserInsightsState State;
    }

    public class RadarUserInsightsState
    {
        public bool Home;
        public bool Office;
        public bool Traveling;
        public bool Commuting;
    }

    public class RadarUserInsightsLocation
    {
        public RadarUserInsightsLocationType Type;
        public RadarCoordinate Location;
        public RadarUserInsightsLocationConfidence Confidence;
        public DateTime? UpdatedAt;
        public RadarRegion Country;
        public RadarRegion State;
        public RadarRegion DMA;
        public RadarRegion PostalCode;
    }

    public enum RadarUserInsightsLocationConfidence
    {
        None,
        Low,
        Medium,
        High
    }

    public enum RadarUserInsightsLocationType
    {
        Unknown,
        Home,
        Office
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
        UserEnteredGeofence,
        UserExitedGeofence,
        UserEnteredHome,
        UserExitedHome,
        UserEnteredOffice,
        UserExitedOffice,
        UserStartedTraveling,
        UserStoppedTraveling,
        UserEnteredPlace,
        UserExitedPlace,
        UserNearbyPlaceChain,
        UserEnteredRegionCountry,
        UserExitedRegionCountry,
        UserEnteredRegionState,
        UserExitedRegionState,
        UserEnteredRegionDMA,
        UserExitedRegionDMA,
        UserStartedCommuting,
        UserStoppedCommuting,
        UserStartedTrip,
        UserUpdatedTrip,
        UserApproachingTripDestination,
        UserArrivedAtTripDestination,
        UserStoppedTrip,
        UserEnteredBeacon,
        UserExitedBeacon,
        UserEnteredRegionPostalCode,
        UserExitedRegionPostalCode
    }

    public class RadarLocation
    {
        public double Longitude;
        public double Latitude;
        public float Bearing;
        public double Altitude;
        public float Speed;
        public DateTime? Timestamp;
        // todo
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

    public class JSONObject : Dictionary<string, string> { }

}