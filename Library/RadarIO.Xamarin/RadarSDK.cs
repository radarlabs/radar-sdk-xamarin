using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadarIO.Xamarin
{
    public abstract class RadarSDK
    {
        public abstract Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce();
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
        Canceled,
        Completed,
        Expired,
        Arrived,
        Approaching,
        Started,
        Unknown
    }

    public enum RadarRouteMode
    {
        Motorbike,
        Truck,
        Car,
        Bike,
        Foot
    }

    public enum RadarLocationSource
    {
        Unknown,
        BeaconExit,
        BeaconEnter,
        MockLocation,
        GeofenceExit,
        GeofenceDwell,
        GeofenceEnter,
        ManualLocation,
        BackgroundLocation,
        ForegroundLocation
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
        public RadarLocation Location;
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
        High,
        Medium,
        Low,
        None
    }

    public enum RadarUserInsightsLocationType
    {
        Office,
        Home,
        Unknown
    }

    public class RadarPlace
    {
        public string Id;
        public string Name;
        public IEnumerable<string> Categories;
        public RadarChain Chain;
        public RadarLocation Location;
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
        ErrorUnknown,
        ErrorServer,
        ErrorRateLimit,
        ErrorNotFound,
        ErrorForbidden,
        ErrorPaymentRequired,
        ErrorUnauthorized,
        ErrorBadRequest,
        ErrorNetwork,
        ErrorBluetooth,
        ErrorLocation,
        ErrorPermissions,
        ErrorPublishableKey,
        Success
    }

    public class JSONObject : Dictionary<string, object> { }

}