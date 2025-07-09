namespace RadarIO;

internal static class Conversion
{
    internal static RadarStatus ToSDK(this AndroidBinding.Radar.RadarStatus status)
        => (RadarStatus)status.Ordinal();

    internal static RadarRouteMatrix ToSDK(this AndroidBinding.RadarRouteMatrix matrix)
        => new RadarRouteMatrixImpl
        {
            matrix = matrix?.GetMatrix()?.Select(arr => arr?.Select(ToSDK))
        };

    internal static RadarRoutes ToSDK(this AndroidBinding.RadarRoutes routes)
        => routes == null ? null : new RadarRoutes
        {
            Geodesic = routes.Geodesic?.ToSDK(),
            Foot = routes.Foot?.ToSDK(),
            Bike = routes.Bike?.ToSDK(),
            Car = routes.Car?.ToSDK(),
            Truck = routes.Truck?.ToSDK(),
            Motorbike = routes.Motorbike?.ToSDK(),
        };

    internal static RadarRoute ToSDK(this AndroidBinding.RadarRoute route)
        => route == null ? null : new RadarRoute
        {
            Distance = route.Distance?.ToSDK(),
            Duration = route.Duration?.ToSDK(),
            Geometry = route.Geometry?.ToSDK()
        };

    internal static RadarRouteDistance ToSDK(this AndroidBinding.RadarRouteDistance distance)
        => distance == null ? null : new RadarRouteDistance
        {
            Value = distance.Value,
            Text = distance.Text
        };

    internal static RadarRouteDuration ToSDK(this AndroidBinding.RadarRouteDuration duration)
        => duration == null ? null : new RadarRouteDuration
        {
            Value = duration.Value,
            Text = duration.Text
        };

    internal static RadarRouteGeometry ToSDK(this AndroidBinding.RadarRouteGeometry geometry)
        => geometry == null ? null : new RadarRouteGeometry
        {
            Coordinates = geometry.GetCoordinates()?.Select(ToSDK)
        };

    internal static RadarAddress ToSDK(this AndroidBinding.RadarAddress address)
        => address == null ? null : new RadarAddress
        {
            Coordinate = address.Coordinate?.ToSDK(),
            FormattedAddress = address.FormattedAddress,
            Country = address.Country,
            CountryCode = address.CountryCode,
            CountryFlag = address.CountryFlag,
            Dma = address.Dma,
            DmaCode = address.DmaCode,
            State = address.State,
            StateCode = address.StateCode,
            PostalCode = address.PostalCode,
            City = address.City,
            Borough = address.Borough,
            County = address.County,
            Neighborhood = address.Neighborhood,
            Street = address.Street,
            Number = address.Number,
            AddressLabel = address.AddressLabel,
            PlaceLabel = address.PlaceLabel,
            Unit = address.Unit,
            Plus4 = address.Plus4,
            Distance = address.Distance?.DoubleValue() ?? 0,
            Layer = address.Layer,
            Metadata = address.Metadata?.ToSDK(),
            Confidence = (RadarAddressConfidence)address.Confidence.Ordinal(),
            TimeZone = address.TimeZone?.ToSDK(),
        };

    internal static RadarLocation ToSDK(this Android.Locations.Location location)
        => location == null ? null : new RadarLocation
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Accuracy = location.Accuracy,
            Course = location.Bearing,
            Altitude = location.Altitude,
            Speed = location.Speed,
            Timestamp = location.Time.ToDateTime()
        };

    internal static RadarEvent ToSDK(this AndroidBinding.RadarEvent ev)
        => ev == null ? null : new RadarEvent
        {
            Id = ev.Get_id(),
            CreatedAt = ev.CreatedAt?.ToSDK(),
            ActualCreatedAt = ev.ActualCreatedAt?.ToSDK(),
            Live = ev.Live,
            Type = (RadarEventType)ev.Type.Ordinal(),
            ConversionName = ev.ConversionName,
            Geofence = ev.Geofence?.ToSDK(),
            Place = ev.Place?.ToSDK(),
            Region = ev.Region?.ToSDK(),
            Beacon = ev.Beacon?.ToSDK(),
            Trip = ev.Trip?.ToSDK(),
            AlternatePlaces = ev.GetAlternatePlaces()?.Select(ToSDK),
            VerifiedPlace = ev.VerifiedPlace?.ToSDK(),
            Verification = (RadarEventVerification)ev.Verification.Ordinal(),
            Confidence = (RadarEventConfidence)ev.Confidence.Ordinal(),
            Duration = ev.Duration,
            Location = ev.Location?.ToSDK(),
            Metadata = ev.Metadata?.ToSDK(),
            Fraud = ev.Fraud?.ToSDK(),
            Replayed = ev.Replayed
        };

    internal static RadarUser ToSDK(this AndroidBinding.RadarUser user)
        => user == null ? null : new RadarUser
        {
            Id = user.Get_id(),
            UserId = user.UserId,
            DeviceId = user.DeviceId,
            Description = user.Description,
            Metadata = user.Metadata?.ToSDK(),
            Location = user.Location?.ToSDK(),
            ActivityType = (RadarActivityType)user.ActivityType.Ordinal(),
            Geofences = user.GetGeofences()?.Select(ToSDK),
            Place = user.Place?.ToSDK(),
            Beacons = user.GetBeacons()?.Select(ToSDK),
            Stopped = user.Stopped,
            Foreground = user.Foreground,
            Country = user.Country?.ToSDK(),
            State = user.State?.ToSDK(),
            DMA = user.Dma?.ToSDK(),
            PostalCode = user.PostalCode?.ToSDK(),
            NearbyPlaceChains = user.GetNearbyPlaceChains()?.Select(ToSDK),
            Segments = user.GetSegments()?.Select(ToSDK),
            TopChains = user.GetTopChains()?.Select(ToSDK),
            Source = (RadarLocationSource)user.Source.Ordinal(),
            Trip = user.Trip?.ToSDK(),
            Debug = user.Debug,
            Fraud = user.Fraud?.ToSDK(),
        };

    internal static RadarSegment ToSDK(this AndroidBinding.RadarSegment segment)
        => segment == null ? null : new RadarSegment
        {
            Description = segment.Description,
            ExternalId = segment.ExternalId
        };

    internal static RadarTrip ToSDK(this AndroidBinding.RadarTrip trip)
        => trip == null ? null : new RadarTrip
        {
            Id = trip.Get_id(),
            ExternalId = trip.ExternalId,
            Metadata = trip.Metadata?.ToSDK(),
            DestinationGeofenceTag = trip.DestinationGeofenceTag,
            DestinationGeofenceExternalId = trip.DestinationGeofenceExternalId,
            DestinationLocation = trip.DestinationLocation?.ToSDK(),
            Mode = trip.Mode == null ? null : (RadarRouteMode?)trip.Mode.Ordinal(),
            EtaDistance = trip.EtaDistance?.DoubleValue() ?? 0,
            EtaDuration = trip.EtaDuration?.DoubleValue() ?? 0,
            Status = (RadarTripStatus)trip.Status.Ordinal()
        };

    internal static RadarRegion ToSDK(this AndroidBinding.RadarRegion region)
        => region == null ? null : new RadarRegion
        {
            Id = region.Get_id(),
            Name = region.Name,
            Code = region.Code,
            Type = region.Type,
            Flag = region.Flag,
            Allowed = region.Allowed,
            Passed = region.Passed,
            InExclusionZone = region.InExclusionZone,
            InBufferZone = region.InBufferZone,
            DistanceToBorder = region.DistanceToBorder,
            Expected = region.Expected,
        };

    internal static RadarBeacon ToSDK(this AndroidBinding.RadarBeacon beacon)
        => beacon == null ? null : new RadarBeacon
        {
            Id = beacon.Get_id(),
            Description = beacon.Description,
            Tag = beacon.Tag,
            ExternalId = beacon.ExternalId,
            UUID = beacon.Uuid,
            Major = beacon.Major,
            Minor = beacon.Minor,
            Metadata = beacon.ToJson()?.ToSDK(),
            Location = beacon.Location?.ToSDK(),
            Rssi = (int)beacon.Rssi
        };

    internal static RadarGeofence ToSDK(this AndroidBinding.RadarGeofence geofence)
        => geofence == null ? null : new RadarGeofence
        {
            Id = geofence.Get_id(),
            Description = geofence.Description,
            Tag = geofence.Tag,
            ExternalId = geofence.ExternalId,
            Metadata = geofence.Metadata?.ToSDK(),
            OperatingHours = geofence.OperatingHours?.ToSDK(),
            Geometry = geofence.Geometry?.ToSDK()
        };

    internal static RadarGeofenceGeometry ToSDK(this AndroidBinding.RadarGeofenceGeometry geometry)
    {
        if (geometry == null)
            return null;

        if (geometry is AndroidBinding.RadarCircleGeometry)
        {
            var circle = geometry as AndroidBinding.RadarCircleGeometry;
            return new RadarCircleGeometry
            {
                Center = circle.Center?.ToSDK(),
                Radius = circle.Radius
            };
        }

        if (geometry is AndroidBinding.RadarPolygonGeometry)
        {
            var polygon = geometry as AndroidBinding.RadarPolygonGeometry;
            return new RadarPolygonGeometry
            {
                Center = polygon.Center?.ToSDK(),
                Radius = polygon.Radius,
                Coordinates = polygon.GetCoordinates()?.Select(ToSDK)
            };
        }

        return null;
    }

    internal static RadarCoordinate ToSDK(this AndroidBinding.RadarCoordinate coordinate)
        => coordinate == null ? null : new RadarCoordinate
        {
            Latitude = coordinate.Latitude,
            Longitude = coordinate.Longitude
        };

    internal static RadarPlace ToSDK(this AndroidBinding.RadarPlace place)
        => place == null ? null : new RadarPlace
        {
            Id = place.Get_id(),
            Name = place.Name,
            Categories = place.GetCategories(),
            Chain = place.Chain?.ToSDK(),
            Location = place.Location?.ToSDK(),
            Group = place.Group,
            Metadata = place.Metadata?.ToSDK(),
            Address = place.Address?.ToSDK(),
        };

    internal static RadarChain ToSDK(this AndroidBinding.RadarChain chain)
        => chain == null ? null : new RadarChain
        {
            Slug = chain.Slug,
            Name = chain.Name,
            ExternalId = chain.ExternalId,
            Metadata = chain.ToJson()?.ToSDK()
        };

    internal static RadarTripOptions ToSDK(this AndroidBinding.RadarTripOptions options)
        => options == null ? null : new RadarTripOptions
        {
            ExternalId = options.ExternalId,
            DestinationGeofenceTag = options.DestinationGeofenceTag,
            DestinationGeofenceExternalId = options.DestinationGeofenceExternalId,
            Mode = (RadarRouteMode)options.Mode.Ordinal(),
            Metadata = options.Metadata?.ToSDK(),
            ScheduledArrivalAt = options.ScheduledArrivalAt?.ToSDK(),
            ApproachingThreshold = options.ApproachingThreshold,
            StartTracking = options.StartTracking,
        };

    internal static RadarTrackingOptions ToSDK(this AndroidBinding.RadarTrackingOptions options)
        => options == null ? null : new RadarTrackingOptions
        {
            DesiredStoppedUpdateInterval = options.DesiredStoppedUpdateInterval,
            FastestStoppedUpdateInterval = options.FastestStoppedUpdateInterval,
            DesiredMovingUpdateInterval = options.DesiredMovingUpdateInterval,
            FastestMovingUpdateInterval = options.FastestMovingUpdateInterval,
            DesiredSyncInterval = options.DesiredSyncInterval,
            DesiredAccuracy = (RadarTrackingOptionsDesiredAccuracy)options.DesiredAccuracy.Ordinal(),
            StopDuration = options.StopDuration,
            StopDistance = options.StopDistance,
            StartTrackingAfter = options.StartTrackingAfter?.ToSDK(),
            StopTrackingAfter = options.StopTrackingAfter?.ToSDK(),
            Replay = (RadarTrackingOptionsReplay)options.Replay.Ordinal(),
            Sync = (RadarTrackingOptionsSync)options.Sync.Ordinal(),
            UseStoppedGeofence = options.UseStoppedGeofence,
            StoppedGeofenceRadius = options.StoppedGeofenceRadius,
            UseMovingGeofence = options.UseMovingGeofence,
            MovingGeofenceRadius = options.MovingGeofenceRadius,
            SyncGeofences = options.SyncGeofences,
            SyncGeofencesLimit = options.SyncGeofencesLimit,
            ForegroundServiceEnabled = options.ForegroundServiceEnabled,
            Beacons = options.Beacons
        };

    internal static RadarContext ToSDK(this AndroidBinding.RadarContext context)
        => context == null ? null : new RadarContext
        {
            Country = context.Country?.ToSDK(),
            Dma = context.Dma?.ToSDK(),
            Geofences = context.GetGeofences().Select(ToSDK),
            Place = context.Place?.ToSDK(),
            PostalCode = context.PostalCode?.ToSDK(),
            State = context.State?.ToSDK()
        };

    internal static RadarTrackingOptionsForegroundService ToSDK(this AndroidBinding.RadarTrackingOptions.RadarTrackingOptionsForegroundService service)
        => service == null ? null : new RadarTrackingOptionsForegroundService
        {
            Text = service.Text,
            Title = service.Title,
            Icon = service.Icon is null ? 0 : (int)service.Icon,
            UpdatesOnly = service.UpdatesOnly,
            Activity = service.Activity,
            Importance = service.Importance is null ? 0 : (int)service.Importance,
            Id = service.Id is null ? 0 : (int)service.Id
        };

    internal static RadarFraud ToSDK(this AndroidBinding.RadarFraud fraud)
        => fraud == null ? null : new RadarFraud
        {
            Passed = fraud.Passed,
            Bypassed = fraud.Bypassed,
            Verified = fraud.Verified,
            Proxy = fraud.Proxy,
            Mocked = fraud.Mocked,
            Compromised = fraud.Compromised,
            Jumped = fraud.Jumped,
            Inaccurate = fraud.Inaccurate,
            Sharing = fraud.Sharing,
            Blocked = fraud.Blocked,
        };

    internal static RadarTimeZone ToSDK(this AndroidBinding.RadarTimeZone timeZone)
        => timeZone == null ? null : new RadarTimeZone
        {
            Name = timeZone.Name,
            Code = timeZone.Code,
            CurrentTime = timeZone.CurrentTime?.ToSDK(),
            UtcOffset = timeZone.UtcOffset,
            DstOffset = timeZone.DstOffset
        };

    internal static RadarVerifiedLocationToken ToSDK(this AndroidBinding.RadarVerifiedLocationToken token)
        => token == null ? null : new RadarVerifiedLocationToken
        {
            User = token.User?.ToSDK(),
            Events = token.GetEvents()?.Select(ToSDK),
            Token = token.Token,
            ExpiresAt = token.ExpiresAt?.ToSDK(),
            ExpiresIn = token.ExpiresIn,
            Passed = token.Passed,
            FailureReasons = token.GetFailureReasons()
        };

    internal static RadarOperatingHours ToSDK(this AndroidBinding.RadarOperatingHours hours)
    {
        if (hours == null)
            return null;

        Org.Json.JSONObject obj = hours.ToJson();
        var res = new Dictionary<string, List<List<string>>>();
        var keysItr = obj.Keys();
        while (keysItr.HasNext)
        {
            var key = keysItr.Next().ToString();
            var value = obj.GetJSONArray(key);
            var times = new List<List<string>>();
            for (int i = 0; i < value.Length(); i++)
            {
                var timeSlot = value.GetJSONArray(i);
                var timeList = new List<string>();
                for (int j = 0; j < timeSlot.Length(); j++)
                {
                    timeList.Add(timeSlot.GetString(j));
                }
                times.Add(timeList);
            }
            res.Add(key, times);
        }

        return new RadarOperatingHours
        {
            Hours = res
        };
    }

    internal static AndroidBinding.RadarTripOptions ToBinding(this RadarTripOptions options)
        => options == null ? null : new AndroidBinding.RadarTripOptions(
            options.ExternalId,
            options.Metadata?.ToBinding(),
            options.DestinationGeofenceTag,
            options.DestinationGeofenceExternalId,
            AndroidBinding.Radar.RadarRouteMode.Values()[(int)options.Mode],
            options.ScheduledArrivalAt?.ToBinding(),
            options.ApproachingThreshold,
            options.StartTracking
        );

    internal static AndroidBinding.RadarTrackingOptions ToBinding(this RadarTrackingOptions options)
        => options == null ? null : new AndroidBinding.RadarTrackingOptions(
             options.DesiredStoppedUpdateInterval,
             options.FastestStoppedUpdateInterval,
             options.DesiredMovingUpdateInterval,
             options.FastestMovingUpdateInterval,
             options.DesiredSyncInterval,
             options.DesiredAccuracy.ToBinding(),
             options.StopDuration,
             options.StopDistance,
             options.StartTrackingAfter?.ToBinding(),
             options.StopTrackingAfter?.ToBinding(),
             AndroidBinding.RadarTrackingOptions.RadarTrackingOptionsReplay.Values().ElementAt((int)options.Replay),
             AndroidBinding.RadarTrackingOptions.RadarTrackingOptionsSync.Values().ElementAt((int)options.Sync),
             options.UseStoppedGeofence,
             options.StoppedGeofenceRadius,
             options.UseMovingGeofence,
             options.MovingGeofenceRadius,
             options.SyncGeofences,
             options.SyncGeofencesLimit,
             options.ForegroundServiceEnabled,
             options.Beacons
            );

    internal static AndroidBinding.RadarNotificationOptions ToBinding(this RadarNotificationOptions options)
        => options == null ? null : new AndroidBinding.RadarNotificationOptions(
            options.IconString,
            options.IconColor,
            options.ForegroundServiceIconString,
            options.ForegroundServiceIconColor,
            options.EventIconString,
            options.EventIconColor,
            options.DeepLink);

    internal static DateTime ToSDK(this Java.Util.Date date)
        => date == null ? DateTime.MinValue : date.Time.ToDateTime();

    internal static DateTime ToDateTime(this long time)
        => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(time);

    internal static JSONObject ToSDK(this Org.Json.JSONObject obj)
    {
        if (obj == null)
            return null;

        var res = new JSONObject();
        var keysItr = obj.Keys();
        while (keysItr.HasNext)
        {
            var key = keysItr.Next().ToString();
            var value = obj.Get(key);
            object val;
            if (value is Java.Lang.Integer)
                val = (int)value;
            else if (value is Java.Lang.Boolean)
                val = (bool)value;
            else
                val = (string)value;
            res.Add(key, val);
        }
        return res;
    }

    internal static AndroidBinding.RadarTrackingOptions.RadarTrackingOptionsDesiredAccuracy ToBinding(this RadarTrackingOptionsDesiredAccuracy accuracy)
        => AndroidBinding.RadarTrackingOptions.RadarTrackingOptionsDesiredAccuracy.Values().ElementAt((int)accuracy);

    internal static AndroidBinding.RadarTrip.RadarTripStatus ToBinding(this RadarTripStatus status)
        => AndroidBinding.RadarTrip.RadarTripStatus.Values().ElementAt((int)status);

    internal static AndroidBinding.Radar.RadarLocationSource ToBinding(this RadarLocationSource source)
        => AndroidBinding.Radar.RadarLocationSource.Values().ElementAt((int)source);

    internal static AndroidBinding.Radar.RadarRouteMode ToBinding(this RadarRouteMode mode)
        => AndroidBinding.Radar.RadarRouteMode.Values().ElementAt((int)mode);

    internal static AndroidBinding.Radar.RadarAddressVerificationStatus ToBinding(this RadarAddressVerificationStatus status)
        => AndroidBinding.Radar.RadarAddressVerificationStatus.Values().ElementAt((int)status);

    internal static AndroidBinding.Radar.RadarActivityType ToBinding(this RadarActivityType activityType)
        => AndroidBinding.Radar.RadarActivityType.Values().ElementAt((int)activityType);

    internal static AndroidBinding.Radar.RadarLocationServicesProvider ToBinding(this RadarLocationServicesProvider locationServicesProvider)
        => AndroidBinding.Radar.RadarLocationServicesProvider.Values().ElementAt((int)locationServicesProvider);

    internal static Org.Json.JSONObject ToBinding(this JSONObject obj)
        => new Org.Json.JSONObject(obj);

    internal static AndroidBinding.RadarTrackingOptions.RadarTrackingOptionsForegroundService ToBinding(this RadarTrackingOptionsForegroundService service)
        => new AndroidBinding.RadarTrackingOptions.RadarTrackingOptionsForegroundService(
                service.Text,
                service.Title,
                Java.Lang.Integer.ValueOf(service.Icon),
                service.UpdatesOnly,
                service.Activity,
                Java.Lang.Integer.ValueOf(service.Importance),
                Java.Lang.Integer.ValueOf(service.Id),
                service.ChannelName,
                service.IconString,
                service.IconColor,
                service.DeepLink
            );

    internal static Android.Locations.Location ToBinding(this RadarLocation location)
        => new Android.Locations.Location("mock")
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Bearing = (float)(location.Course ?? 0),
            Altitude = location.Altitude ?? 0,
            Speed = (float)(location.Speed ?? 0),
            Time = location.Timestamp.Ticks / 10000
        };

    internal static Java.Util.EnumSet ToBinding(this IEnumerable<RadarRouteMode> modes)
    {
        var boundModes = modes.Select(m => AndroidBinding.Radar.RadarRouteMode.Values()[(int)m]);
        //return Java.Util.EnumSet.CopyOf(boundModes.)
        if (boundModes.Count() == 0)
            return null;
        if (boundModes.Count() == 1)
            return Java.Util.EnumSet.Of(boundModes.Single());
        return Java.Util.EnumSet.Of(boundModes.First(), boundModes.Skip(1).ToArray());
    }

    internal static Java.Util.Date ToBinding(this DateTime date)
        => new Java.Util.Date(new DateTimeOffset(date).ToUnixTimeMilliseconds());

    //internal static T InvertEnum<T>(int val)
    //    => Enum.GetValues(typeof(T)).Cast<T>().Reverse().ElementAt(val);

    //internal static T ToBinding<T>(this Enum e) where T : Java.Lang.Enum
    //    => (T)Java.Lang.Enum.ValueOf(Java.Lang.Class.FromType(typeof(T)), e.ToString().ToUpper());
}
