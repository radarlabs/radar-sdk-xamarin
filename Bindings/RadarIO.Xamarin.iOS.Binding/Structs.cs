using System;
using ObjCRuntime;

namespace RadarIO.Xamarin.iOSBinding
{

	[Native]
	public enum RadarAddressConfidence : long
	{
		None = 0,
		Exact = 1,
		Interpolated = 2,
		Fallback = 3
	}

	[Flags]
	[Native]
	public enum RadarRouteMode : long
	{
		Foot = 1L << 0,
		Bike = 1L << 1,
		Car = 1L << 2,
		Truck = 1L << 3,
		Motorbike = 1L << 4
	}

	[Native]
	public enum RadarTripStatus : long
	{
		Unknown,
		Started,
		Approaching,
		Arrived,
		Expired,
		Completed,
		Canceled
	}

	[Native]
	public enum RadarEventType : long
	{
		Unknown,
		Custom,
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

	[Native]
	public enum RadarEventConfidence : long
	{
		None = 0,
		Low = 1,
		Medium = 2,
		High = 3
	}

	[Native]
	public enum RadarEventVerification : long
	{
		Accept = 1,
		Unverify = 0,
		Reject = -1
	}

	[Native]
	public enum RadarTrackingOptionsDesiredAccuracy : long
	{
		High,
		Medium,
		Low
	}

	[Native]
	public enum RadarTrackingOptionsReplay : long
	{
		Stops,
		None
	}

	[Native]
	public enum RadarTrackingOptionsSyncLocations : long
	{
		All,
		StopsAndExits,
		None
	}

	[Native]
	public enum RadarStatus : long
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

	[Native]
	public enum RadarLocationSource : long
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

	[Native]
	public enum RadarLogLevel : long
	{
		None = 0,
		Error = 1,
		Warning = 2,
		Info = 3,
		Debug = 4
	}

	[Native]
	public enum RadarRouteUnits : long
	{
		Imperial,
		Metric
	}

}