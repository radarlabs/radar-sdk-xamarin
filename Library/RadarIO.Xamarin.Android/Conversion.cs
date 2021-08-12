using System;
using System.Linq;
using Android.Locations;

namespace RadarIO.Xamarin
{
    public static class Conversion
    {
        public static RadarStatus ToSDK(this AndroidBinding.Radar.RadarStatus status)
            => (RadarStatus)status.Ordinal();

        public static RadarLocation ToSDK(this Location location)
            => new RadarLocation
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Bearing = location.Bearing,
                Altitude = location.Altitude,
                Speed = location.Speed,
                Timestamp = location.Time.ToDateTime()
            };

        public static RadarEvent ToSDK(this AndroidBinding.RadarEvent ev)
            => new RadarEvent
            {
                Id = ev.Get_id(),
                CreatedAt = ev.CreatedAt?.ToSDK(),
                ActualCreatedAt = ev.ActualCreatedAt?.ToSDK(),
                Live = ev.Live,
                Type = (RadarEventType)ev.Type.Ordinal(),
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
                Location = ev.Location?.ToSDK()
            };

        public static RadarUser ToSDK(this AndroidBinding.RadarUser user)
            => new RadarUser
            {
                Id = user.Get_id(),
                UserId = user.UserId,
                DeviceId = user.DeviceId,
                Description = user.Description,
                Metadata = user.Metadata?.ToSDK(),
                Location = user.Location?.ToSDK(),
                Geofences = user.GetGeofences()?.Select(ToSDK),
                Place = user.Place?.ToSDK(),
                Insights = user.Insights?.ToSDK(),
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
                Proxy = user.Proxy,
                Trip = user.Trip?.ToSDK()
            };

        public static RadarSegment ToSDK(this AndroidBinding.RadarSegment segment)
            => new RadarSegment
            {
                Description = segment.Description,
                ExternalId = segment.ExternalId
            };

        public static RadarUserInsights ToSDK(this AndroidBinding.RadarUserInsights insights)
            => new RadarUserInsights
            {
                HomeLocation = insights.HomeLocation?.ToSDK(),
                OfficeLocation = insights.OfficeLocation?.ToSDK(),
                State = insights.State?.ToSDK(),
            };

        public static RadarUserInsightsState ToSDK(this AndroidBinding.RadarUserInsightsState state)
            => new RadarUserInsightsState
            {
                Home = state.Home,
                Office = state.Office,
                Traveling = state.Traveling,
                Commuting = state.Commuting
            };

        public static RadarUserInsightsLocation ToSDK(this AndroidBinding.RadarUserInsightsLocation location)
            => new RadarUserInsightsLocation
            {
                Type = (RadarUserInsightsLocationType)location.Type.Ordinal(),
                Location = location.Location?.ToSDK(),
                Confidence = (RadarUserInsightsLocationConfidence)location.Confidence.Ordinal(),
                UpdatedAt = location.UpdatedAt?.ToSDK(),
                Country = location.Country?.ToSDK(),
                State = location.State?.ToSDK(),
                DMA = location.Dma?.ToSDK(),
                PostalCode = location.PostalCode?.ToSDK(),
            };

        public static RadarTrip ToSDK(this AndroidBinding.RadarTrip trip)
            => new RadarTrip
            {
                Id = trip.Get_id(),
                ExternalId = trip.ExternalId,
                Metadata = trip.Metadata?.ToSDK(),
                DestinationGeofenceTag = trip.DestinationGeofenceTag,
                DestinationGeofenceExternalId = trip.DestinationGeofenceExternalId,
                DestinationLocation = trip.DestinationLocation?.ToSDK(),
                Mode = (RadarRouteMode)trip.Mode.Ordinal(),
                EtaDistance = (double)trip.EtaDistance,
                EtaDuration = (double)trip.EtaDuration,
                Status = (RadarTripStatus)trip.Status.Ordinal()
            };

        public static RadarRegion ToSDK(this AndroidBinding.RadarRegion region)
            => new RadarRegion
            {
                Id = region.Get_id(),
                Name = region.Name,
                Code = region.Code,
                Type = region.Type,
                Flag = region.Flag
            };

        public static RadarBeacon ToSDK(this AndroidBinding.RadarBeacon beacon)
            => new RadarBeacon
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
            };

        public static RadarGeofence ToSDK(this AndroidBinding.RadarGeofence geofence)
            => new RadarGeofence
            {
                Id = geofence.Get_id(),
                Description = geofence.Description,
                Tag = geofence.Tag,
                ExternalId = geofence.ExternalId,
                Metadata = geofence.Metadata?.ToSDK(),
                Geometry = geofence.Geometry?.ToSDK()
            };

        public static RadarGeofenceGeometry ToSDK(this AndroidBinding.RadarGeofenceGeometry geometry)
        {
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

        public static RadarCoordinate ToSDK(this AndroidBinding.RadarCoordinate coordinate)
            => new RadarCoordinate
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude
            };

        public static RadarPlace ToSDK(this AndroidBinding.RadarPlace place)
            => new RadarPlace
            {
                Id = place.Get_id(),
                Name = place.Name,
                Categories = place.GetCategories(),
                Chain = place.Chain?.ToSDK(),
                Location = place.Location?.ToSDK(),
                Group = place.Group,
                Metadata = place.Metadata?.ToSDK(),
            };

        public static RadarChain ToSDK(this AndroidBinding.RadarChain chain)
            => new RadarChain
            {
                Slug = chain.Slug,
                Name = chain.Name,
                ExternalId = chain.ExternalId,
                Metadata = chain.ToJson()?.ToSDK()
            };

        private static DateTime ToSDK(this Java.Util.Date date)
            => date.Time.ToDateTime();

        private static DateTime ToDateTime(this long time)
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(time);

        private static JSONObject ToSDK(this Org.Json.JSONObject obj)
            => null; // todo
    }
}
