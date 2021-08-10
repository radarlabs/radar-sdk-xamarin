using System;
using System.Linq;
using CoreLocation;

namespace RadarIO.Xamarin
{
    public static class Conversion
    {
        public static RadarStatus ToSDK(this iOS.Binding.RadarStatus status)
            => (RadarStatus)status;

        public static RadarLocation ToSDK(this CLLocation location)
            => new RadarLocation
            {
                Latitude = location.Coordinate.Latitude,
                Longitude = location.Coordinate.Longitude,
                Bearing = (float)location.Course,
                Altitude = location.Altitude,
                Speed = (float)location.Speed,
                Timestamp = (DateTime?)location.Timestamp
            };

        public static RadarEvent ToSDK(this iOS.Binding.RadarEvent ev)
            => new RadarEvent
            {
                Id = ev._id,
                CreatedAt = (DateTime)ev.CreatedAt,
                ActualCreatedAt = (DateTime)ev.ActualCreatedAt,
                Live = ev.Live,
                Type = (RadarEventType)ev.Type,
                Geofence = ev.Geofence?.ToSDK(),
                Place = ev.Place?.ToSDK(),
                Region = ev.Region?.ToSDK(),
                Beacon = ev.Beacon?.ToSDK(),
                Trip = ev.Trip?.ToSDK(),
                AlternatePlaces = ev.AlternatePlaces?.Select(ToSDK),
                VerifiedPlace  = ev.VerifiedPlace?.ToSDK(),
                Verification = ev.Verification.ToSDK(),
                Confidence = ev.Confidence.ToSDK(),
                Duration = ev.Duration,
                Location = ev.Location?.ToSDK()
            };

        public static RadarUser ToSDK(this iOS.Binding.RadarUser user)
            => new RadarUser
            {
                Id = user._id,
                UserId = user.UserId,
                DeviceId = user.DeviceId,
                Description = user.Description,
                Metadata = user.Metadata?.ToSDK(),
                Location = user.Location?.ToSDK(),
                Geofences = user.Geofences?.Select(ToSDK),
                Place = user.Place?.ToSDK(),
                Insights = user.Insights?.ToSDK(),
                Beacons = user.Beacons?.Select(ToSDK),
                Stopped = user.Stopped,
                Foreground = user.Foreground,
                Country = user.Country?.ToSDK(),
                State = user.State?.ToSDK(),
                DMA = user.Dma?.ToSDK(),
                PostalCode = user.PostalCode?.ToSDK(),
                NearbyPlaceChains = user.NearbyPlaceChains?.Select(ToSDK),
                Segments = user.Segments?.Select(ToSDK),
                TopChains = user.TopChains?.Select(ToSDK),
                Source = user.Source.ToSDK(),
                Proxy = user.Proxy,
                Trip = user.Trip?.ToSDK()
            };

        public static RadarLocationSource ToSDK(this iOS.Binding.RadarLocationSource source)
            => (RadarLocationSource)source;

        public static RadarSegment ToSDK(this iOS.Binding.RadarSegment segment)
            => new RadarSegment
            {
                Description = segment.Description,
                ExternalId = segment.ExternalId
            };

        public static RadarUserInsights ToSDK(this iOS.Binding.RadarUserInsights insights)
            => new RadarUserInsights
            {
                HomeLocation = insights.HomeLocation?.ToSDK(),
                OfficeLocation = insights.OfficeLocation?.ToSDK(),
                State = insights.State?.ToSDK(),
            };

        public static RadarUserInsightsState ToSDK(this iOS.Binding.RadarUserInsightsState state)
            => new RadarUserInsightsState
            {
                Home = state.Home,
                Office = state.Office,
                Traveling = state.Traveling,
                Commuting = state.Commuting
            };

        public static RadarUserInsightsLocation ToSDK(this iOS.Binding.RadarUserInsightsLocation location)
            => new RadarUserInsightsLocation
            {
                Type = location.Type.ToSDK(),
                Location = location.Location?.ToSDK(),
                Confidence = location.Confidence.ToSDK(),
                UpdatedAt = (DateTime?)location.UpdatedAt,
                Country = location.Country?.ToSDK(),
                State = location.State?.ToSDK(),
                DMA = location.Dma?.ToSDK(),
                PostalCode = location.PostalCode?.ToSDK(),
            };

        public static RadarUserInsightsLocationType ToSDK(this iOS.Binding.RadarUserInsightsLocationType type)
            => (RadarUserInsightsLocationType)type;

        public static RadarUserInsightsLocationConfidence ToSDK(this iOS.Binding.RadarUserInsightsLocationConfidence confidence)
            => (RadarUserInsightsLocationConfidence)confidence;

        public static RadarEventConfidence ToSDK(this iOS.Binding.RadarEventConfidence confidence)
            => (RadarEventConfidence)confidence;

        public static RadarTrip ToSDK(this iOS.Binding.RadarTrip trip)
            => new RadarTrip
            {
                Id = trip._id,
                ExternalId = trip.ExternalId,
                Metadata = trip.Metadata?.ToSDK(),
                DestinationGeofenceTag = trip.DestinationGeofenceTag,
                DestinationGeofenceExternalId = trip.DestinationGeofenceExternalId,
                DestinationLocation = trip.DestinationLocation?.ToSDK(),
                Mode = trip.Mode.ToSDK(),
                EtaDistance = trip.EtaDistance,
                EtaDuration = trip.EtaDuration,
                Status = trip.Status.ToSDK()
            };

        public static RadarRouteMode ToSDK(this iOS.Binding.RadarRouteMode mode)
            => (RadarRouteMode)mode;

        public static RadarTripStatus ToSDK(this iOS.Binding.RadarTripStatus status)
            => (RadarTripStatus)status;

        public static RadarRegion ToSDK(this iOS.Binding.RadarRegion region)
            => new RadarRegion
            {
                Id = region._id,
                Name = region.Name,
                Code = region.Code,
                Type = region.Type,
                Flag = region.Flag
            };

        public static RadarBeacon ToSDK(this iOS.Binding.RadarBeacon beacon)
            => new RadarBeacon
            {
                Id = beacon._id,
                Description = beacon.Description,
                Tag = beacon.Tag,
                ExternalId = beacon.ExternalId,
                UUID = beacon.Uuid,
                Major = beacon.Major,
                Minor = beacon.Minor,
                Metadata = beacon.Metadata?.ToSDK(),
                Location = beacon.Geometry?.ToSDK(),
            };

        public static RadarGeofence ToSDK(this iOS.Binding.RadarGeofence geofence)
            => new RadarGeofence
            {
                Id = geofence._id,
                Description = geofence.Description,
                Tag = geofence.Tag,
                ExternalId = geofence.ExternalId,
                Metadata = geofence.Metadata?.ToSDK(),
                Geometry = geofence.Geometry?.ToSDK()
            };

        public static RadarGeofenceGeometry ToSDK(this iOS.Binding.RadarGeofenceGeometry geometry)
        {
            if (geometry is iOS.Binding.RadarCircleGeometry)
            {
                var circle = geometry as iOS.Binding.RadarCircleGeometry;
                return new RadarCircleGeometry
                {
                    Center = circle.Center?.ToSDK(),
                    Radius = circle.Radius
                };
            }

            if (geometry is iOS.Binding.RadarPolygonGeometry)
            {
                var polygon = geometry as iOS.Binding.RadarPolygonGeometry;
                return new RadarPolygonGeometry
                {
                    Center = polygon.Center?.ToSDK(),
                    Radius = polygon.Radius,
                    Coordinates = polygon.Coordinates?.Select(ToSDK)
                };
            }

            return null;
        }

        public static RadarCoordinate ToSDK(this iOS.Binding.RadarCoordinate coordinate)
            => new RadarCoordinate
            {
                Latitude = coordinate.Coordinate.Latitude,
                Longitude = coordinate.Coordinate.Longitude
            };

        public static RadarPlace ToSDK(this iOS.Binding.RadarPlace place)
            => new RadarPlace
            {
                Id = place._id,
                Name = place.Name,
                Categories = place.Categories,
                Chain = place.Chain?.ToSDK(),
                Location = place.Location?.ToSDK(),
                Group = place.Group,
                Metadata = place.Metadata?.ToSDK(),
            };

        public static RadarChain ToSDK(this iOS.Binding.RadarChain chain)
            => new RadarChain
            {
                Slug = chain.Slug,
                Name = chain.Name,
                ExternalId = chain.ExternalId,
                Metadata = chain.Metadata?.ToSDK()
            };

        public static RadarEventVerification ToSDK(this iOS.Binding.RadarEventVerification verification)
            => (RadarEventVerification)verification;

        public static JSONObject ToSDK(this Foundation.NSDictionary metadata)
            => (JSONObject)metadata.ToDictionary(
                m => m.Key.ToString(), m => m.Value.ToString());
    }
}
