using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;

namespace RadarIO.Xamarin.iOS.Binding
{
	// @interface RadarCoordinate : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarCoordinate
	{
		// @property (readonly, assign, nonatomic) CLLocationCoordinate2D coordinate;
		[Export("coordinate", ArgumentSemantic.Assign)]
		CLLocationCoordinate2D Coordinate { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarAddress : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarAddress
	{
		// @property (readonly, assign, nonatomic) CLLocationCoordinate2D coordinate;
		[Export("coordinate", ArgumentSemantic.Assign)]
		CLLocationCoordinate2D Coordinate { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable formattedAddress;
		[NullAllowed, Export("formattedAddress")]
		string FormattedAddress { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable country;
		[NullAllowed, Export("country")]
		string Country { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable countryCode;
		[NullAllowed, Export("countryCode")]
		string CountryCode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable countryFlag;
		[NullAllowed, Export("countryFlag")]
		string CountryFlag { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable dma;
		[NullAllowed, Export("dma")]
		string Dma { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable dmaCode;
		[NullAllowed, Export("dmaCode")]
		string DmaCode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable state;
		[NullAllowed, Export("state")]
		string State { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable stateCode;
		[NullAllowed, Export("stateCode")]
		string StateCode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable postalCode;
		[NullAllowed, Export("postalCode")]
		string PostalCode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable city;
		[NullAllowed, Export("city")]
		string City { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable borough;
		[NullAllowed, Export("borough")]
		string Borough { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable county;
		[NullAllowed, Export("county")]
		string County { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable neighborhood;
		[NullAllowed, Export("neighborhood")]
		string Neighborhood { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable number;
		[NullAllowed, Export("number")]
		string Number { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable addressLabel;
		[NullAllowed, Export("addressLabel")]
		string AddressLabel { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable placeLabel;
		[NullAllowed, Export("placeLabel")]
		string PlaceLabel { get; }

		// @property (assign, nonatomic) enum RadarAddressConfidence confidence;
		[Export("confidence", ArgumentSemantic.Assign)]
		RadarAddressConfidence Confidence { get; set; }

		// +(NSArray<NSDictionary *> * _Nullable)arrayForAddresses:(NSArray<RadarAddress *> * _Nullable)addresses;
		[Static]
		[Export("arrayForAddresses:")]
		[return: NullAllowed]
		NSDictionary[] ArrayForAddresses([NullAllowed] RadarAddress[] addresses);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarGeofenceGeometry : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarGeofenceGeometry
	{
	}

	// @interface RadarGeofence : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarGeofence
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _id;
		[Export("_id")]
		string _id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull _description;
		[Export("_description")]
		string _description { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable tag;
		[NullAllowed, Export("tag")]
		string Tag { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable externalId;
		[NullAllowed, Export("externalId")]
		string ExternalId { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		// @property (readonly, nonatomic, strong) RadarGeofenceGeometry * _Nonnull geometry;
		[Export("geometry", ArgumentSemantic.Strong)]
		RadarGeofenceGeometry Geometry { get; }

		// +(NSArray<NSDictionary *> * _Nullable)arrayForGeofences:(NSArray<RadarGeofence *> * _Nullable)geofences;
		[Static]
		[Export("arrayForGeofences:")]
		[return: NullAllowed]
		NSDictionary[] ArrayForGeofences([NullAllowed] RadarGeofence[] geofences);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarChain : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarChain
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull slug;
		[Export("slug")]
		string Slug { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable externalId;
		[NullAllowed, Export("externalId")]
		string ExternalId { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		// +(NSArray<NSDictionary *> * _Nullable)arrayForChains:(NSArray<RadarChain *> * _Nullable)chains;
		[Static]
		[Export("arrayForChains:")]
		[return: NullAllowed]
		NSDictionary[] ArrayForChains([NullAllowed] RadarChain[] chains);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarPlace : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarPlace
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _id;
		[Export("_id")]
		string _id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSArray<NSString *> * _Nonnull categories;
		[Export("categories", ArgumentSemantic.Copy)]
		string[] Categories { get; }

		// @property (readonly, nonatomic, strong) RadarChain * _Nullable chain;
		[NullAllowed, Export("chain", ArgumentSemantic.Strong)]
		RadarChain Chain { get; }

		// @property (readonly, nonatomic, strong) RadarCoordinate * _Nonnull location;
		[Export("location", ArgumentSemantic.Strong)]
		RadarCoordinate Location { get; }

		// @property (readonly, nonatomic, strong) NSString * _Nullable group;
		[NullAllowed, Export("group", ArgumentSemantic.Strong)]
		string Group { get; }

		// @property (readonly, nonatomic, strong) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Strong)]
		NSDictionary Metadata { get; }

		// -(BOOL)isChain:(NSString * _Nullable)slug;
		[Export("isChain:")]
		bool IsChain([NullAllowed] string slug);

		// -(BOOL)hasCategory:(NSString * _Nullable)category;
		[Export("hasCategory:")]
		bool HasCategory([NullAllowed] string category);

		// +(NSArray<NSDictionary *> * _Nullable)arrayForPlaces:(NSArray<RadarPlace *> * _Nullable)places;
		[Static]
		[Export("arrayForPlaces:")]
		[return: NullAllowed]
		NSDictionary[] ArrayForPlaces([NullAllowed] RadarPlace[] places);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarRegion : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarRegion
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _id;
		[Export("_id")]
		string _id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull code;
		[Export("code")]
		string Code { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable flag;
		[NullAllowed, Export("flag")]
		string Flag { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarContext : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarContext
	{
		// @property (readonly, copy, nonatomic) NSArray<RadarGeofence *> * _Nonnull geofences;
		[Export("geofences", ArgumentSemantic.Copy)]
		RadarGeofence[] Geofences { get; }

		// @property (readonly, copy, nonatomic) RadarPlace * _Nullable place;
		[NullAllowed, Export("place", ArgumentSemantic.Copy)]
		RadarPlace Place { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable country;
		[NullAllowed, Export("country", ArgumentSemantic.Strong)]
		RadarRegion Country { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable state;
		[NullAllowed, Export("state", ArgumentSemantic.Strong)]
		RadarRegion State { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable dma;
		[NullAllowed, Export("dma", ArgumentSemantic.Strong)]
		RadarRegion Dma { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable postalCode;
		[NullAllowed, Export("postalCode", ArgumentSemantic.Strong)]
		RadarRegion PostalCode { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarBeacon : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarBeacon
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _id;
		[Export("_id")]
		string _id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull _description;
		[Export("_description")]
		string _description { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable tag;
		[NullAllowed, Export("tag")]
		string Tag { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable externalId;
		[NullAllowed, Export("externalId")]
		string ExternalId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull uuid;
		[Export("uuid")]
		string Uuid { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull major;
		[Export("major")]
		string Major { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull minor;
		[Export("minor")]
		string Minor { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		// @property (readonly, nonatomic, strong) RadarCoordinate * _Nonnull geometry;
		[Export("geometry", ArgumentSemantic.Strong)]
		RadarCoordinate Geometry { get; }

		// +(NSArray<NSDictionary *> * _Nullable)arrayForBeacons:(NSArray<RadarBeacon *> * _Nullable)beacons;
		[Static]
		[Export("arrayForBeacons:")]
		[return: NullAllowed]
		NSDictionary[] ArrayForBeacons([NullAllowed] RadarBeacon[] beacons);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarSegment : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarSegment
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _description;
		[Export("_description")]
		string _description { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull externalId;
		[Export("externalId")]
		string ExternalId { get; }

		// +(NSArray<NSDictionary *> * _Nullable)arrayForSegments:(NSArray<RadarSegment *> * _Nullable)segments;
		[Static]
		[Export("arrayForSegments:")]
		[return: NullAllowed]
		NSDictionary[] ArrayForSegments([NullAllowed] RadarSegment[] segments);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarTrip : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarTrip
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _id;
		[Export("_id")]
		string _id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable externalId;
		[NullAllowed, Export("externalId")]
		string ExternalId { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable destinationGeofenceTag;
		[NullAllowed, Export("destinationGeofenceTag")]
		string DestinationGeofenceTag { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable destinationGeofenceExternalId;
		[NullAllowed, Export("destinationGeofenceExternalId")]
		string DestinationGeofenceExternalId { get; }

		// @property (readonly, nonatomic, strong) RadarCoordinate * _Nullable destinationLocation;
		[NullAllowed, Export("destinationLocation", ArgumentSemantic.Strong)]
		RadarCoordinate DestinationLocation { get; }

		// @property (readonly, assign, nonatomic) RadarRouteMode mode;
		[Export("mode", ArgumentSemantic.Assign)]
		RadarRouteMode Mode { get; }

		// @property (readonly, assign, nonatomic) float etaDistance;
		[Export("etaDistance")]
		float EtaDistance { get; }

		// @property (readonly, assign, nonatomic) float etaDuration;
		[Export("etaDuration")]
		float EtaDuration { get; }

		// @property (readonly, assign, nonatomic) RadarTripStatus status;
		[Export("status", ArgumentSemantic.Assign)]
		RadarTripStatus Status { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarUserInsightsLocation : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarUserInsightsLocation
	{
		// @property (readonly, assign, nonatomic) RadarUserInsightsLocationType type;
		[Export("type", ArgumentSemantic.Assign)]
		RadarUserInsightsLocationType Type { get; }

		// @property (readonly, nonatomic, strong) RadarCoordinate * _Nullable location;
		[NullAllowed, Export("location", ArgumentSemantic.Strong)]
		RadarCoordinate Location { get; }

		// @property (readonly, assign, nonatomic) RadarUserInsightsLocationConfidence confidence;
		[Export("confidence", ArgumentSemantic.Assign)]
		RadarUserInsightsLocationConfidence Confidence { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull updatedAt;
		[Export("updatedAt", ArgumentSemantic.Copy)]
		NSDate UpdatedAt { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable country;
		[NullAllowed, Export("country", ArgumentSemantic.Strong)]
		RadarRegion Country { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable state;
		[NullAllowed, Export("state", ArgumentSemantic.Strong)]
		RadarRegion State { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable dma;
		[NullAllowed, Export("dma", ArgumentSemantic.Strong)]
		RadarRegion Dma { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable postalCode;
		[NullAllowed, Export("postalCode", ArgumentSemantic.Strong)]
		RadarRegion PostalCode { get; }

		// +(NSString * _Nullable)stringForType:(RadarUserInsightsLocationType)type;
		[Static]
		[Export("stringForType:")]
		[return: NullAllowed]
		string StringForType(RadarUserInsightsLocationType type);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarUserInsightsState : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarUserInsightsState
	{
		// @property (readonly, assign, nonatomic) BOOL home;
		[Export("home")]
		bool Home { get; }

		// @property (readonly, assign, nonatomic) BOOL office;
		[Export("office")]
		bool Office { get; }

		// @property (readonly, assign, nonatomic) BOOL traveling;
		[Export("traveling")]
		bool Traveling { get; }

		// @property (readonly, assign, nonatomic) BOOL commuting;
		[Export("commuting")]
		bool Commuting { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarUserInsights : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarUserInsights
	{
		// @property (readonly, nonatomic, strong) RadarUserInsightsLocation * _Nullable homeLocation;
		[NullAllowed, Export("homeLocation", ArgumentSemantic.Strong)]
		RadarUserInsightsLocation HomeLocation { get; }

		// @property (readonly, nonatomic, strong) RadarUserInsightsLocation * _Nullable officeLocation;
		[NullAllowed, Export("officeLocation", ArgumentSemantic.Strong)]
		RadarUserInsightsLocation OfficeLocation { get; }

		// @property (readonly, nonatomic, strong) RadarUserInsightsState * _Nonnull state;
		[Export("state", ArgumentSemantic.Strong)]
		RadarUserInsightsState State { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarUser : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarUser
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _id;
		[Export("_id")]
		string _id { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable userId;
		[NullAllowed, Export("userId")]
		string UserId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable deviceId;
		[NullAllowed, Export("deviceId")]
		string DeviceId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable _description;
		[NullAllowed, Export("_description")]
		string _description { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		// @property (readonly, nonatomic, strong) CLLocation * _Nonnull location;
		[Export("location", ArgumentSemantic.Strong)]
		CLLocation Location { get; }

		// @property (readonly, copy, nonatomic) NSArray<RadarGeofence *> * _Nullable geofences;
		[NullAllowed, Export("geofences", ArgumentSemantic.Copy)]
		RadarGeofence[] Geofences { get; }

		// @property (readonly, copy, nonatomic) RadarPlace * _Nullable place;
		[NullAllowed, Export("place", ArgumentSemantic.Copy)]
		RadarPlace Place { get; }

		// @property (readonly, nonatomic, strong) RadarUserInsights * _Nullable insights;
		[NullAllowed, Export("insights", ArgumentSemantic.Strong)]
		RadarUserInsights Insights { get; }

		// @property (readonly, copy, nonatomic) NSArray<RadarBeacon *> * _Nullable beacons;
		[NullAllowed, Export("beacons", ArgumentSemantic.Copy)]
		RadarBeacon[] Beacons { get; }

		// @property (readonly, assign, nonatomic) BOOL stopped;
		[Export("stopped")]
		bool Stopped { get; }

		// @property (readonly, assign, nonatomic) BOOL foreground;
		[Export("foreground")]
		bool Foreground { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable country;
		[NullAllowed, Export("country", ArgumentSemantic.Strong)]
		RadarRegion Country { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable state;
		[NullAllowed, Export("state", ArgumentSemantic.Strong)]
		RadarRegion State { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable dma;
		[NullAllowed, Export("dma", ArgumentSemantic.Strong)]
		RadarRegion Dma { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable postalCode;
		[NullAllowed, Export("postalCode", ArgumentSemantic.Strong)]
		RadarRegion PostalCode { get; }

		// @property (readonly, copy, nonatomic) NSArray<RadarChain *> * _Nullable nearbyPlaceChains;
		[NullAllowed, Export("nearbyPlaceChains", ArgumentSemantic.Copy)]
		RadarChain[] NearbyPlaceChains { get; }

		// @property (readonly, copy, nonatomic) NSArray<RadarSegment *> * _Nullable segments;
		[NullAllowed, Export("segments", ArgumentSemantic.Copy)]
		RadarSegment[] Segments { get; }

		// @property (readonly, copy, nonatomic) NSArray<RadarChain *> * _Nullable topChains;
		[NullAllowed, Export("topChains", ArgumentSemantic.Copy)]
		RadarChain[] TopChains { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }

		// @property (readonly, assign, nonatomic) RadarLocationSource source;
		[Export("source", ArgumentSemantic.Assign)]
		RadarLocationSource Source { get; }

		// @property (readonly, assign, nonatomic) BOOL proxy;
		[Export("proxy")]
		bool Proxy { get; }

		// @property (readonly, nonatomic, strong) RadarTrip * _Nullable trip;
		[NullAllowed, Export("trip", ArgumentSemantic.Strong)]
		RadarTrip Trip { get; }
	}

	// @interface RadarEvent : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarEvent
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull _id;
		[Export("_id")]
		string _id { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull createdAt;
		[Export("createdAt", ArgumentSemantic.Copy)]
		NSDate CreatedAt { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull actualCreatedAt;
		[Export("actualCreatedAt", ArgumentSemantic.Copy)]
		NSDate ActualCreatedAt { get; }

		// @property (readonly, assign, nonatomic) BOOL live;
		[Export("live")]
		bool Live { get; }

		// @property (readonly, assign, nonatomic) RadarEventType type;
		[Export("type", ArgumentSemantic.Assign)]
		RadarEventType Type { get; }

		// @property (readonly, nonatomic, strong) RadarGeofence * _Nullable geofence;
		[NullAllowed, Export("geofence", ArgumentSemantic.Strong)]
		RadarGeofence Geofence { get; }

		// @property (readonly, nonatomic, strong) RadarPlace * _Nullable place;
		[NullAllowed, Export("place", ArgumentSemantic.Strong)]
		RadarPlace Place { get; }

		// @property (readonly, nonatomic, strong) RadarRegion * _Nullable region;
		[NullAllowed, Export("region", ArgumentSemantic.Strong)]
		RadarRegion Region { get; }

		// @property (readonly, nonatomic, strong) RadarBeacon * _Nullable beacon;
		[NullAllowed, Export("beacon", ArgumentSemantic.Strong)]
		RadarBeacon Beacon { get; }

		// @property (readonly, nonatomic, strong) RadarTrip * _Nullable trip;
		[NullAllowed, Export("trip", ArgumentSemantic.Strong)]
		RadarTrip Trip { get; }

		// @property (readonly, nonatomic, strong) NSArray<RadarPlace *> * _Nullable alternatePlaces;
		[NullAllowed, Export("alternatePlaces", ArgumentSemantic.Strong)]
		RadarPlace[] AlternatePlaces { get; }

		// @property (readonly, nonatomic, strong) RadarPlace * _Nullable verifiedPlace;
		[NullAllowed, Export("verifiedPlace", ArgumentSemantic.Strong)]
		RadarPlace VerifiedPlace { get; }

		// @property (readonly, assign, nonatomic) RadarEventVerification verification;
		[Export("verification", ArgumentSemantic.Assign)]
		RadarEventVerification Verification { get; }

		// @property (readonly, assign, nonatomic) RadarEventConfidence confidence;
		[Export("confidence", ArgumentSemantic.Assign)]
		RadarEventConfidence Confidence { get; }

		// @property (readonly, assign, nonatomic) float duration;
		[Export("duration")]
		float Duration { get; }

		// @property (readonly, nonatomic, strong) CLLocation * _Nonnull location;
		[Export("location", ArgumentSemantic.Strong)]
		CLLocation Location { get; }

		// +(NSString * _Nullable)stringForType:(RadarEventType)type;
		[Static]
		[Export("stringForType:")]
		[return: NullAllowed]
		string StringForType(RadarEventType type);

		// +(NSArray<NSDictionary *> * _Nullable)arrayForEvents:(NSArray<RadarEvent *> * _Nullable)events;
		[Static]
		[Export("arrayForEvents:")]
		[return: NullAllowed]
		NSDictionary[] ArrayForEvents([NullAllowed] RadarEvent[] events);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarRouteDistance : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarRouteDistance
	{
		// @property (readonly, assign, nonatomic) double value;
		[Export("value")]
		double Value { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull text;
		[Export("text")]
		string Text { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarRouteDuration : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarRouteDuration
	{
		// @property (readonly, assign, nonatomic) double value;
		[Export("value")]
		double Value { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull text;
		[Export("text")]
		string Text { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarRouteGeometry : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarRouteGeometry
	{
		// @property (readonly, copy, nonatomic) NSArray<RadarCoordinate *> * _Nullable coordinates;
		[NullAllowed, Export("coordinates", ArgumentSemantic.Copy)]
		RadarCoordinate[] Coordinates { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarRoute : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarRoute
	{
		// @property (readonly, nonatomic, strong) RadarRouteDistance * _Nonnull distance;
		[Export("distance", ArgumentSemantic.Strong)]
		RadarRouteDistance Distance { get; }

		// @property (readonly, nonatomic, strong) RadarRouteDuration * _Nonnull duration;
		[Export("duration", ArgumentSemantic.Strong)]
		RadarRouteDuration Duration { get; }

		// @property (readonly, nonatomic, strong) RadarRouteGeometry * _Nonnull geometry;
		[Export("geometry", ArgumentSemantic.Strong)]
		RadarRouteGeometry Geometry { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarRouteMatrix : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarRouteMatrix
	{
		// -(RadarRoute * _Nullable)routeBetweenOriginIndex:(NSUInteger)originIndex destinationIndex:(NSUInteger)destinationIndex __attribute__((swift_name("routeBetween(originIndex:destinationIndex:)")));
		[Export("routeBetweenOriginIndex:destinationIndex:")]
		[return: NullAllowed]
		RadarRoute RouteBetweenOriginIndex(nuint originIndex, nuint destinationIndex);

		// -(NSArray<NSArray<NSDictionary *> *> * _Nonnull)arrayValue;
		[Export("arrayValue")]

		NSArray<NSDictionary>[] ArrayValue { get; }
	}

	// @interface RadarRoutes : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarRoutes
	{
		// @property (readonly, nonatomic, strong) RadarRouteDistance * _Nullable geodesic;
		[NullAllowed, Export("geodesic", ArgumentSemantic.Strong)]
		RadarRouteDistance Geodesic { get; }

		// @property (readonly, nonatomic, strong) RadarRoute * _Nullable foot;
		[NullAllowed, Export("foot", ArgumentSemantic.Strong)]
		RadarRoute Foot { get; }

		// @property (readonly, nonatomic, strong) RadarRoute * _Nullable bike;
		[NullAllowed, Export("bike", ArgumentSemantic.Strong)]
		RadarRoute Bike { get; }

		// @property (readonly, nonatomic, strong) RadarRoute * _Nullable car;
		[NullAllowed, Export("car", ArgumentSemantic.Strong)]
		RadarRoute Car { get; }

		// @property (readonly, nonatomic, strong) RadarRoute * _Nullable truck;
		[NullAllowed, Export("truck", ArgumentSemantic.Strong)]
		RadarRoute Truck { get; }

		// @property (readonly, nonatomic, strong) RadarRoute * _Nullable motorbike;
		[NullAllowed, Export("motorbike", ArgumentSemantic.Strong)]
		RadarRoute Motorbike { get; }

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// @interface RadarTrackingOptions : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarTrackingOptions
	{
		// @property (assign, nonatomic) int desiredStoppedUpdateInterval;
		[Export("desiredStoppedUpdateInterval")]
		int DesiredStoppedUpdateInterval { get; set; }

		// @property (assign, nonatomic) int desiredMovingUpdateInterval;
		[Export("desiredMovingUpdateInterval")]
		int DesiredMovingUpdateInterval { get; set; }

		// @property (assign, nonatomic) int desiredSyncInterval;
		[Export("desiredSyncInterval")]
		int DesiredSyncInterval { get; set; }

		// @property (assign, nonatomic) RadarTrackingOptionsDesiredAccuracy desiredAccuracy;
		[Export("desiredAccuracy", ArgumentSemantic.Assign)]
		RadarTrackingOptionsDesiredAccuracy DesiredAccuracy { get; set; }

		// @property (assign, nonatomic) int stopDuration;
		[Export("stopDuration")]
		int StopDuration { get; set; }

		// @property (assign, nonatomic) int stopDistance;
		[Export("stopDistance")]
		int StopDistance { get; set; }

		// @property (copy, nonatomic) NSDate * _Nullable startTrackingAfter;
		[NullAllowed, Export("startTrackingAfter", ArgumentSemantic.Copy)]
		NSDate StartTrackingAfter { get; set; }

		// @property (copy, nonatomic) NSDate * _Nullable stopTrackingAfter;
		[NullAllowed, Export("stopTrackingAfter", ArgumentSemantic.Copy)]
		NSDate StopTrackingAfter { get; set; }

		// @property (assign, nonatomic) RadarTrackingOptionsReplay replay;
		[Export("replay", ArgumentSemantic.Assign)]
		RadarTrackingOptionsReplay Replay { get; set; }

		// @property (assign, nonatomic) RadarTrackingOptionsSync sync;
		[Export("sync", ArgumentSemantic.Assign)]
		RadarTrackingOptionsSync Sync { get; set; }

		// @property (assign, nonatomic) BOOL showBlueBar;
		[Export("showBlueBar")]
		bool ShowBlueBar { get; set; }

		// @property (assign, nonatomic) BOOL useStoppedGeofence;
		[Export("useStoppedGeofence")]
		bool UseStoppedGeofence { get; set; }

		// @property (assign, nonatomic) int stoppedGeofenceRadius;
		[Export("stoppedGeofenceRadius")]
		int StoppedGeofenceRadius { get; set; }

		// @property (assign, nonatomic) BOOL useMovingGeofence;
		[Export("useMovingGeofence")]
		bool UseMovingGeofence { get; set; }

		// @property (assign, nonatomic) int movingGeofenceRadius;
		[Export("movingGeofenceRadius")]
		int MovingGeofenceRadius { get; set; }

		// @property (assign, nonatomic) BOOL syncGeofences;
		[Export("syncGeofences")]
		bool SyncGeofences { get; set; }

		// @property (assign, nonatomic) BOOL useVisits;
		[Export("useVisits")]
		bool UseVisits { get; set; }

		// @property (assign, nonatomic) BOOL useSignificantLocationChanges;
		[Export("useSignificantLocationChanges")]
		bool UseSignificantLocationChanges { get; set; }

		// @property (assign, nonatomic) BOOL beacons;
		[Export("beacons")]
		bool Beacons { get; set; }

		// @property (readonly, copy, class) RadarTrackingOptions * _Nonnull continuous;
		[Static]
		[Export("continuous", ArgumentSemantic.Copy)]
		RadarTrackingOptions Continuous { get; }

		// @property (readonly, copy, class) RadarTrackingOptions * _Nonnull responsive;
		[Static]
		[Export("responsive", ArgumentSemantic.Copy)]
		RadarTrackingOptions Responsive { get; }

		// @property (readonly, copy, class) RadarTrackingOptions * _Nonnull efficient;
		[Static]
		[Export("efficient", ArgumentSemantic.Copy)]
		RadarTrackingOptions Efficient { get; }

		// +(NSString * _Nonnull)stringForDesiredAccuracy:(RadarTrackingOptionsDesiredAccuracy)desiredAccuracy;
		[Static]
		[Export("stringForDesiredAccuracy:")]
		string StringForDesiredAccuracy(RadarTrackingOptionsDesiredAccuracy desiredAccuracy);

		// +(RadarTrackingOptionsDesiredAccuracy)desiredAccuracyForString:(NSString * _Nonnull)str;
		[Static]
		[Export("desiredAccuracyForString:")]
		RadarTrackingOptionsDesiredAccuracy DesiredAccuracyForString(string str);

		// +(NSString * _Nonnull)stringForReplay:(RadarTrackingOptionsReplay)replay;
		[Static]
		[Export("stringForReplay:")]
		string StringForReplay(RadarTrackingOptionsReplay replay);

		// +(RadarTrackingOptionsReplay)replayForString:(NSString * _Nonnull)str;
		[Static]
		[Export("replayForString:")]
		RadarTrackingOptionsReplay ReplayForString(string str);

		// +(NSString * _Nonnull)stringForSync:(RadarTrackingOptionsSync)sync;
		[Static]
		[Export("stringForSync:")]
		string StringForSync(RadarTrackingOptionsSync sync);

		// +(RadarTrackingOptionsSync)syncForString:(NSString * _Nonnull)str;
		[Static]
		[Export("syncForString:")]
		RadarTrackingOptionsSync SyncForString(string str);

		// +(RadarTrackingOptions * _Nonnull)trackingOptionsFromDictionary:(NSDictionary * _Nonnull)dictionary;
		[Static]
		[Export("trackingOptionsFromDictionary:")]
		RadarTrackingOptions TrackingOptionsFromDictionary(NSDictionary dictionary);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

	// typedef void (^ _Nullable)(RadarStatus, CLLocation * _Nullable, BOOL) RadarLocationCompletionHandler;
	delegate void RadarLocationCompletionHandler(RadarStatus arg0, [NullAllowed] CLLocation arg1, bool arg2);

	// typedef void (^ _Nullable)(RadarStatus, NSArray<NSString *> * _Nullable) RadarBeaconCompletionHandler;
	delegate void RadarBeaconCompletionHandler(RadarStatus arg0, [NullAllowed] string[] arg1);

	// typedef void (^ _Nullable)(RadarStatus, CLLocation * _Nullable, NSArray<RadarEvent *> * _Nullable, RadarUser * _Nullable) RadarTrackCompletionHandler;
	delegate void RadarTrackCompletionHandler(RadarStatus arg0, [NullAllowed] CLLocation arg1, [NullAllowed] RadarEvent[] arg2, [NullAllowed] RadarUser arg3);

	// typedef void (^ _Nullable)(RadarStatus) RadarTripCompletionHandler;
	delegate void RadarTripCompletionHandler(RadarStatus arg0);

	// typedef void (^ _Nonnull)(RadarStatus, CLLocation * _Nullable, RadarContext * _Nullable) RadarContextCompletionHandler;
	delegate void RadarContextCompletionHandler(RadarStatus arg0, [NullAllowed] CLLocation arg1, [NullAllowed] RadarContext arg2);

	// typedef void (^ _Nonnull)(RadarStatus, CLLocation * _Nullable, NSArray<RadarPlace *> * _Nullable) RadarSearchPlacesCompletionHandler;
	delegate void RadarSearchPlacesCompletionHandler(RadarStatus arg0, [NullAllowed] CLLocation arg1, [NullAllowed] RadarPlace[] arg2);

	// typedef void (^ _Nonnull)(RadarStatus, CLLocation * _Nullable, NSArray<RadarGeofence *> * _Nullable) RadarSearchGeofencesCompletionHandler;
	delegate void RadarSearchGeofencesCompletionHandler(RadarStatus arg0, [NullAllowed] CLLocation arg1, [NullAllowed] RadarGeofence[] arg2);

	// typedef void (^ _Nonnull)(RadarStatus, NSArray<RadarAddress *> * _Nullable) RadarGeocodeCompletionHandler;
	delegate void RadarGeocodeCompletionHandler(RadarStatus arg0, [NullAllowed] RadarAddress[] arg1);

	// typedef void (^ _Nonnull)(RadarStatus, RadarAddress * _Nullable, BOOL) RadarIPGeocodeCompletionHandler;
	delegate void RadarIPGeocodeCompletionHandler(RadarStatus arg0, [NullAllowed] RadarAddress arg1, bool arg2);

	// typedef void (^ _Nonnull)(RadarStatus, RadarRoutes * _Nullable) RadarRouteCompletionHandler;
	delegate void RadarRouteCompletionHandler(RadarStatus arg0, [NullAllowed] RadarRoutes arg1);

	// typedef void (^ _Nonnull)(RadarStatus, RadarRouteMatrix * _Nullable) RadarRouteMatrixCompletionHandler;
	delegate void RadarRouteMatrixCompletionHandler(RadarStatus arg0, [NullAllowed] RadarRouteMatrix arg1);

	// @interface Radar : NSObject
	[BaseType(typeof(NSObject))]
	interface Radar
	{
		// +(void)initializeWithPublishableKey:(NSString * _Nonnull)publishableKey __attribute__((swift_name("initialize(publishableKey:)")));
		[Static]
		[Export("initializeWithPublishableKey:")]
		void InitializeWithPublishableKey(string publishableKey);

		// +(void)setUserId:(NSString * _Nullable)userId;
		[Static]
		[Export("setUserId:")]
		void SetUserId([NullAllowed] string userId);

		// +(NSString * _Nullable)getUserId;
		[Static]
		[NullAllowed, Export("getUserId")]

		string UserId { get; }

		// +(void)setDescription:(NSString * _Nullable)description;
		[Static]
		[Export("setDescription:")]
		void SetDescription([NullAllowed] string description);

		// +(NSString * _Nullable)getDescription;
		[Static]
		[NullAllowed, Export("getDescription")]

		string Description { get; }

		// +(void)setMetadata:(NSDictionary * _Nullable)metadata;
		[Static]
		[Export("setMetadata:")]
		void SetMetadata([NullAllowed] NSDictionary metadata);

		// +(NSDictionary * _Nullable)getMetadata;
		[Static]
		[NullAllowed, Export("getMetadata")]

		NSDictionary Metadata { get; }

		// +(void)setAdIdEnabled:(BOOL)enabled;
		[Static]
		[Export("setAdIdEnabled:")]
		void SetAdIdEnabled(bool enabled);

		// +(void)getLocationWithCompletionHandler:(RadarLocationCompletionHandler _Nullable)completionHandler __attribute__((swift_name("getLocation(completionHandler:)")));
		[Static]
		[Export("getLocationWithCompletionHandler:")]
		void GetLocationWithCompletionHandler([NullAllowed] RadarLocationCompletionHandler completionHandler);

		// +(void)getLocationWithDesiredAccuracy:(RadarTrackingOptionsDesiredAccuracy)desiredAccuracy completionHandler:(RadarLocationCompletionHandler _Nullable)completionHandler __attribute__((swift_name("getLocation(desiredAccuracy:completionHandler:)")));
		[Static]
		[Export("getLocationWithDesiredAccuracy:completionHandler:")]
		void GetLocationWithDesiredAccuracy(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, [NullAllowed] RadarLocationCompletionHandler completionHandler);

		// +(void)trackOnceWithCompletionHandler:(RadarTrackCompletionHandler _Nullable)completionHandler __attribute__((swift_name("trackOnce(completionHandler:)")));
		[Static]
		[Export("trackOnceWithCompletionHandler:")]
		void TrackOnceWithCompletionHandler([NullAllowed] RadarTrackCompletionHandler completionHandler);

		// +(void)trackOnceWithDesiredAccuracy:(RadarTrackingOptionsDesiredAccuracy)desiredAccuracy beacons:(BOOL)beacons completionHandler:(RadarTrackCompletionHandler _Nullable)completionHandler __attribute__((swift_name("trackOnce(desiredAccuracy:beacons:completionHandler:)")));
		[Static]
		[Export("trackOnceWithDesiredAccuracy:beacons:completionHandler:")]
		void TrackOnceWithDesiredAccuracy(RadarTrackingOptionsDesiredAccuracy desiredAccuracy, bool beacons, [NullAllowed] RadarTrackCompletionHandler completionHandler);

		// +(void)trackOnceWithLocation:(CLLocation * _Nonnull)location completionHandler:(RadarTrackCompletionHandler _Nullable)completionHandler __attribute__((swift_name("trackOnce(location:completionHandler:)")));
		[Static]
		[Export("trackOnceWithLocation:completionHandler:")]
		void TrackOnceWithLocation(CLLocation location, [NullAllowed] RadarTrackCompletionHandler completionHandler);

		// +(void)startTrackingWithOptions:(RadarTrackingOptions * _Nonnull)options __attribute__((swift_name("startTracking(trackingOptions:)")));
		[Static]
		[Export("startTrackingWithOptions:")]
		void StartTrackingWithOptions(RadarTrackingOptions options);

		// +(void)mockTrackingWithOrigin:(CLLocation * _Nonnull)origin destination:(CLLocation * _Nonnull)destination mode:(RadarRouteMode)mode steps:(int)steps interval:(NSTimeInterval)interval completionHandler:(RadarTrackCompletionHandler _Nullable)completionHandler __attribute__((swift_name("mockTracking(origin:destination:mode:steps:interval:completionHandler:)")));
		[Static]
		[Export("mockTrackingWithOrigin:destination:mode:steps:interval:completionHandler:")]
		void MockTrackingWithOrigin(CLLocation origin, CLLocation destination, RadarRouteMode mode, int steps, double interval, [NullAllowed] RadarTrackCompletionHandler completionHandler);

		// +(void)stopTracking;
		[Static]
		[Export("stopTracking")]
		void StopTracking();

		// +(BOOL)isTracking;
		[Static]
		[Export("isTracking")]

		bool IsTracking { get; }

		// +(RadarTrackingOptions * _Nonnull)getTrackingOptions;
		[Static]
		[Export("getTrackingOptions")]

		RadarTrackingOptions TrackingOptions { get; }

		// +(void)setDelegate:(id<RadarDelegate> _Nullable)delegate;
		[Static]
		[Export("setDelegate:")]
		void SetDelegate([NullAllowed] RadarDelegate @delegate);

		// +(void)acceptEventId:(NSString * _Nonnull)eventId verifiedPlaceId:(NSString * _Nullable)verifiedPlaceId __attribute__((swift_name("acceptEventId(_:verifiedPlaceId:)")));
		[Static]
		[Export("acceptEventId:verifiedPlaceId:")]
		void AcceptEventId(string eventId, [NullAllowed] string verifiedPlaceId);

		// +(void)rejectEventId:(NSString * _Nonnull)eventId __attribute__((swift_name("rejectEventId(_:)")));
		[Static]
		[Export("rejectEventId:")]
		void RejectEventId(string eventId);

		// +(RadarTripOptions * _Nullable)getTripOptions;
		[Static]
		[NullAllowed, Export("getTripOptions")]

		RadarTripOptions TripOptions { get; }

		// +(void)startTripWithOptions:(RadarTripOptions * _Nonnull)options __attribute__((swift_name("startTrip(options:)")));
		[Static]
		[Export("startTripWithOptions:")]
		void StartTripWithOptions(RadarTripOptions options);

		// +(void)startTripWithOptions:(RadarTripOptions * _Nonnull)options completionHandler:(RadarTripCompletionHandler _Nullable)completionHandler __attribute__((swift_name("startTrip(options:completionHandler:)")));
		[Static]
		[Export("startTripWithOptions:completionHandler:")]
		void StartTripWithOptions(RadarTripOptions options, [NullAllowed] RadarTripCompletionHandler completionHandler);

		// +(void)updateTripWithOptions:(RadarTripOptions * _Nonnull)options status:(RadarTripStatus)status completionHandler:(RadarTripCompletionHandler _Nullable)completionHandler __attribute__((swift_name("updateTrip(options:status:completionHandler:)")));
		[Static]
		[Export("updateTripWithOptions:status:completionHandler:")]
		void UpdateTripWithOptions(RadarTripOptions options, RadarTripStatus status, [NullAllowed] RadarTripCompletionHandler completionHandler);

		// +(void)completeTrip;
		[Static]
		[Export("completeTrip")]
		void CompleteTrip();

		// +(void)completeTripWithCompletionHandler:(RadarTripCompletionHandler _Nullable)completionHandler __attribute__((swift_name("completeTrip(completionHandler:)")));
		[Static]
		[Export("completeTripWithCompletionHandler:")]
		void CompleteTripWithCompletionHandler([NullAllowed] RadarTripCompletionHandler completionHandler);

		// +(void)cancelTrip;
		[Static]
		[Export("cancelTrip")]
		void CancelTrip();

		// +(void)cancelTripWithCompletionHandler:(RadarTripCompletionHandler _Nullable)completionHandler __attribute__((swift_name("cancelTrip(completionHandler:)")));
		[Static]
		[Export("cancelTripWithCompletionHandler:")]
		void CancelTripWithCompletionHandler([NullAllowed] RadarTripCompletionHandler completionHandler);

		// +(void)getContextWithCompletionHandler:(RadarContextCompletionHandler _Nonnull)completionHandler __attribute__((swift_name("getContext(completionHandler:)")));
		[Static]
		[Export("getContextWithCompletionHandler:")]
		void GetContextWithCompletionHandler(RadarContextCompletionHandler completionHandler);

		// +(void)getContextForLocation:(CLLocation * _Nonnull)location completionHandler:(RadarContextCompletionHandler _Nonnull)completionHandler __attribute__((swift_name("getContext(location:completionHandler:)")));
		[Static]
		[Export("getContextForLocation:completionHandler:")]
		void GetContextForLocation(CLLocation location, RadarContextCompletionHandler completionHandler);

		// +(void)searchPlacesWithRadius:(int)radius chains:(NSArray<NSString *> * _Nullable)chains categories:(NSArray<NSString *> * _Nullable)categories groups:(NSArray<NSString *> * _Nullable)groups limit:(int)limit completionHandler:(RadarSearchPlacesCompletionHandler)completionHandler __attribute__((swift_name("searchPlaces(radius:chains:categories:groups:limit:completionHandler:)")));
		[Static]
		[Export("searchPlacesWithRadius:chains:categories:groups:limit:completionHandler:")]
		void SearchPlacesWithRadius(int radius, [NullAllowed] string[] chains, [NullAllowed] string[] categories, [NullAllowed] string[] groups, int limit, RadarSearchPlacesCompletionHandler completionHandler);

		// +(void)searchPlacesNear:(CLLocation * _Nonnull)near radius:(int)radius chains:(NSArray<NSString *> * _Nullable)chains categories:(NSArray<NSString *> * _Nullable)categories groups:(NSArray<NSString *> * _Nullable)groups limit:(int)limit completionHandler:(RadarSearchPlacesCompletionHandler)completionHandler __attribute__((swift_name("searchPlaces(near:radius:chains:categories:groups:limit:completionHandler:)")));
		[Static]
		[Export("searchPlacesNear:radius:chains:categories:groups:limit:completionHandler:")]
		void SearchPlacesNear(CLLocation near, int radius, [NullAllowed] string[] chains, [NullAllowed] string[] categories, [NullAllowed] string[] groups, int limit, RadarSearchPlacesCompletionHandler completionHandler);

		// +(void)searchGeofencesWithRadius:(int)radius tags:(NSArray<NSString *> * _Nullable)tags metadata:(NSDictionary * _Nullable)metadata limit:(int)limit completionHandler:(RadarSearchGeofencesCompletionHandler)completionHandler __attribute__((swift_name("searchGeofences(radius:tags:metadata:limit:completionHandler:)")));
		[Static]
		[Export("searchGeofencesWithRadius:tags:metadata:limit:completionHandler:")]
		void SearchGeofencesWithRadius(int radius, [NullAllowed] string[] tags, [NullAllowed] NSDictionary metadata, int limit, RadarSearchGeofencesCompletionHandler completionHandler);

		// +(void)searchGeofencesNear:(CLLocation * _Nonnull)near radius:(int)radius tags:(NSArray<NSString *> * _Nullable)tags metadata:(NSDictionary * _Nullable)metadata limit:(int)limit completionHandler:(RadarSearchGeofencesCompletionHandler)completionHandler __attribute__((swift_name("searchGeofences(near:radius:tags:metadata:limit:completionHandler:)")));
		[Static]
		[Export("searchGeofencesNear:radius:tags:metadata:limit:completionHandler:")]
		void SearchGeofencesNear(CLLocation near, int radius, [NullAllowed] string[] tags, [NullAllowed] NSDictionary metadata, int limit, RadarSearchGeofencesCompletionHandler completionHandler);

		// +(void)autocompleteQuery:(NSString * _Nonnull)query near:(CLLocation * _Nullable)near layers:(NSArray<NSString *> * _Nullable)layers limit:(int)limit country:(NSString * _Nullable)country completionHandler:(RadarGeocodeCompletionHandler)completionHandler __attribute__((swift_name("autocomplete(query:near:layers:limit:country:completionHandler:)")));
		[Static]
		[Export("autocompleteQuery:near:layers:limit:country:completionHandler:")]
		void AutocompleteQuery(string query, [NullAllowed] CLLocation near, [NullAllowed] string[] layers, int limit, [NullAllowed] string country, RadarGeocodeCompletionHandler completionHandler);

		// +(void)autocompleteQuery:(NSString * _Nonnull)query near:(CLLocation * _Nullable)near limit:(int)limit completionHandler:(RadarGeocodeCompletionHandler)completionHandler __attribute__((swift_name("autocomplete(query:near:limit:completionHandler:)")));
		[Static]
		[Export("autocompleteQuery:near:limit:completionHandler:")]
		void AutocompleteQuery(string query, [NullAllowed] CLLocation near, int limit, RadarGeocodeCompletionHandler completionHandler);

		// +(void)geocodeAddress:(NSString * _Nonnull)query completionHandler:(RadarGeocodeCompletionHandler)completionHandler __attribute__((swift_name("geocode(address:completionHandler:)")));
		[Static]
		[Export("geocodeAddress:completionHandler:")]
		void GeocodeAddress(string query, RadarGeocodeCompletionHandler completionHandler);

		// +(void)reverseGeocodeWithCompletionHandler:(RadarGeocodeCompletionHandler)completionHandler __attribute__((swift_name("reverseGeocode(completionHandler:)")));
		[Static]
		[Export("reverseGeocodeWithCompletionHandler:")]
		void ReverseGeocodeWithCompletionHandler(RadarGeocodeCompletionHandler completionHandler);

		// +(void)reverseGeocodeLocation:(CLLocation * _Nonnull)location completionHandler:(RadarGeocodeCompletionHandler)completionHandler __attribute__((swift_name("reverseGeocode(location:completionHandler:)")));
		[Static]
		[Export("reverseGeocodeLocation:completionHandler:")]
		void ReverseGeocodeLocation(CLLocation location, RadarGeocodeCompletionHandler completionHandler);

		// +(void)ipGeocodeWithCompletionHandler:(RadarIPGeocodeCompletionHandler)completionHandler __attribute__((swift_name("ipGeocode(completionHandler:)")));
		[Static]
		[Export("ipGeocodeWithCompletionHandler:")]
		void IpGeocodeWithCompletionHandler(RadarIPGeocodeCompletionHandler completionHandler);

		// +(void)getDistanceToDestination:(CLLocation * _Nonnull)destination modes:(RadarRouteMode)modes units:(RadarRouteUnits)units completionHandler:(RadarRouteCompletionHandler)completionHandler __attribute__((swift_name("getDistance(destination:modes:units:completionHandler:)")));
		[Static]
		[Export("getDistanceToDestination:modes:units:completionHandler:")]
		void GetDistanceToDestination(CLLocation destination, RadarRouteMode modes, RadarRouteUnits units, RadarRouteCompletionHandler completionHandler);

		// +(void)getDistanceFromOrigin:(CLLocation * _Nonnull)origin destination:(CLLocation * _Nonnull)destination modes:(RadarRouteMode)modes units:(RadarRouteUnits)units completionHandler:(RadarRouteCompletionHandler)completionHandler __attribute__((swift_name("getDistance(origin:destination:modes:units:completionHandler:)")));
		[Static]
		[Export("getDistanceFromOrigin:destination:modes:units:completionHandler:")]
		void GetDistanceFromOrigin(CLLocation origin, CLLocation destination, RadarRouteMode modes, RadarRouteUnits units, RadarRouteCompletionHandler completionHandler);

		// +(void)getMatrixFromOrigins:(NSArray<CLLocation *> * _Nonnull)origins destinations:(NSArray<CLLocation *> * _Nonnull)destinations mode:(RadarRouteMode)mode units:(RadarRouteUnits)units completionHandler:(RadarRouteMatrixCompletionHandler)completionHandler __attribute__((swift_name("getMatrix(origins:destinations:mode:units:completionHandler:)")));
		[Static]
		[Export("getMatrixFromOrigins:destinations:mode:units:completionHandler:")]
		void GetMatrixFromOrigins(CLLocation[] origins, CLLocation[] destinations, RadarRouteMode mode, RadarRouteUnits units, RadarRouteMatrixCompletionHandler completionHandler);

		// +(void)setLogLevel:(RadarLogLevel)level;
		[Static]
		[Export("setLogLevel:")]
		void SetLogLevel(RadarLogLevel level);

		// +(NSString * _Nonnull)stringForStatus:(RadarStatus)status __attribute__((swift_name("stringForStatus(_:)")));
		[Static]
		[Export("stringForStatus:")]
		string StringForStatus(RadarStatus status);

		// +(NSString * _Nonnull)stringForSource:(RadarLocationSource)source __attribute__((swift_name("stringForSource(_:)")));
		[Static]
		[Export("stringForSource:")]
		string StringForSource(RadarLocationSource source);

		// +(NSString * _Nonnull)stringForMode:(RadarRouteMode)mode __attribute__((swift_name("stringForMode(_:)")));
		[Static]
		[Export("stringForMode:")]
		string StringForMode(RadarRouteMode mode);

		// +(NSString * _Nonnull)stringForTripStatus:(RadarTripStatus)status __attribute__((swift_name("stringForTripStatus(_:)")));
		[Static]
		[Export("stringForTripStatus:")]
		string StringForTripStatus(RadarTripStatus status);

		// +(NSDictionary * _Nonnull)dictionaryForLocation:(CLLocation * _Nonnull)location __attribute__((swift_name("dictionaryForLocation(_:)")));
		[Static]
		[Export("dictionaryForLocation:")]
		NSDictionary DictionaryForLocation(CLLocation location);
	}

	// @interface RadarCircleGeometry : RadarGeofenceGeometry
	[BaseType(typeof(RadarGeofenceGeometry))]
	interface RadarCircleGeometry
	{
		// @property (readonly, nonatomic, strong) RadarCoordinate * _Nonnull center;
		[Export("center", ArgumentSemantic.Strong)]
		RadarCoordinate Center { get; }

		// @property (readonly, assign, nonatomic) double radius;
		[Export("radius")]
		double Radius { get; }
	}

	// @protocol RadarDelegate <NSObject>
	[Protocol, Model(AutoGeneratedName = true)]
	[BaseType(typeof(NSObject))]
	interface RadarDelegate
	{
		// @required -(void)didReceiveEvents:(NSArray<RadarEvent *> * _Nonnull)events user:(RadarUser * _Nonnull)user __attribute__((swift_name("didReceiveEvents(_:user:)")));
		[Abstract]
		[Export("didReceiveEvents:user:")]
		void DidReceiveEvents(RadarEvent[] events, RadarUser user);

		// @required -(void)didUpdateLocation:(CLLocation * _Nonnull)location user:(RadarUser * _Nonnull)user __attribute__((swift_name("didUpdateLocation(_:user:)")));
		[Abstract]
		[Export("didUpdateLocation:user:")]
		void DidUpdateLocation(CLLocation location, RadarUser user);

		// @required -(void)didUpdateClientLocation:(CLLocation * _Nonnull)location stopped:(BOOL)stopped source:(RadarLocationSource)source __attribute__((swift_name("didUpdateClientLocation(_:stopped:source:)")));
		[Abstract]
		[Export("didUpdateClientLocation:stopped:source:")]
		void DidUpdateClientLocation(CLLocation location, bool stopped, RadarLocationSource source);

		// @required -(void)didFailWithStatus:(RadarStatus)status __attribute__((swift_name("didFail(status:)")));
		[Abstract]
		[Export("didFailWithStatus:")]
		void DidFailWithStatus(RadarStatus status);

		// @required -(void)didLogMessage:(NSString * _Nonnull)message __attribute__((swift_name("didLog(message:)")));
		[Abstract]
		[Export("didLogMessage:")]
		void DidLogMessage(string message);
	}

	// @interface RadarPolygonGeometry : RadarGeofenceGeometry
	[BaseType(typeof(RadarGeofenceGeometry))]
	interface RadarPolygonGeometry
	{
		// @property (readonly, copy, nonatomic) NSArray<RadarCoordinate *> * _Nullable coordinates;
		[NullAllowed, Export("coordinates", ArgumentSemantic.Copy)]
		RadarCoordinate[] Coordinates { get; }

		// @property (readonly, nonatomic, strong) RadarCoordinate * _Nonnull center;
		[Export("center", ArgumentSemantic.Strong)]
		RadarCoordinate Center { get; }

		// @property (readonly, assign, nonatomic) double radius;
		[Export("radius")]
		double Radius { get; }
	}

	// @interface RadarTripOptions : NSObject
	[BaseType(typeof(NSObject))]
	interface RadarTripOptions
	{
		// -(instancetype _Nonnull)initWithExternalId:(NSString * _Nonnull)externalId;
		[Export("initWithExternalId:")]
		IntPtr Constructor(string externalId);

		// @property (copy, nonatomic) NSString * _Nonnull externalId;
		[Export("externalId")]
		string ExternalId { get; set; }

		// @property (copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable destinationGeofenceTag;
		[NullAllowed, Export("destinationGeofenceTag")]
		string DestinationGeofenceTag { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable destinationGeofenceExternalId;
		[NullAllowed, Export("destinationGeofenceExternalId")]
		string DestinationGeofenceExternalId { get; set; }

		// @property (assign, nonatomic) RadarRouteMode mode;
		[Export("mode", ArgumentSemantic.Assign)]
		RadarRouteMode Mode { get; set; }

		// +(RadarTripOptions * _Nonnull)tripOptionsFromDictionary:(NSDictionary * _Nonnull)dict;
		[Static]
		[Export("tripOptionsFromDictionary:")]
		RadarTripOptions TripOptionsFromDictionary(NSDictionary dict);

		// -(NSDictionary * _Nonnull)dictionaryValue;
		[Export("dictionaryValue")]

		NSDictionary DictionaryValue { get; }
	}

}