using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Xamarin.Essentials;

namespace RadarIO.Xamarin
{
    internal static class Conversion
    {
        internal static RadarStatus ToSDK(this iOSBinding.RadarStatus status)
            => (RadarStatus)status;

        internal static RadarRouteMatrix ToSDK(this iOSBinding.RadarRouteMatrix matrix)
        {
            // linq isn't working..
            var res = new List<IEnumerable<RadarRoute>>();
            foreach (var arr in matrix?.ArrayValue)
            {
                var routes = new List<RadarRoute>();
                foreach (var dict in arr)
                {
                    var route = new RadarRoute()
                    {
                        Distance =  new RadarRouteDistance
                        {
                            Text = dict["distance"].ValueForKey(new Foundation.NSString("text")).ToString(),
                            Value = ((Foundation.NSNumber)dict["distance"].ValueForKey(new Foundation.NSString("value"))).DoubleValue
                        },
                        Duration = new RadarRouteDuration
                        {
                            Text = dict["duration"].ValueForKey(new Foundation.NSString("text")).ToString(),
                            Value = ((Foundation.NSNumber)dict["duration"].ValueForKey(new Foundation.NSString("value"))).DoubleValue
                        }
                        // no geometry
                    };
                    routes.Add(route);
                }
                res.Add(routes);
            }
            return new RadarRouteMatrixImpl { matrix = res };
        }

        internal static RadarRoutes ToSDK(this iOSBinding.RadarRoutes routes)
            => new RadarRoutes
            {
                Geodesic = routes.Geodesic?.ToSDK(),
                Foot = routes.Foot?.ToSDK(),
                Bike = routes.Bike?.ToSDK(),
                Car = routes.Car?.ToSDK(),
                Truck = routes.Truck?.ToSDK(),
                Motorbike = routes.Motorbike?.ToSDK(),
            };

        internal static RadarRoute ToSDK(this iOSBinding.RadarRoute route)
            => new RadarRoute
            {
                Distance = route.Distance?.ToSDK(),
                Duration = route.Duration?.ToSDK(),
                Geometry = route.Geometry?.ToSDK()
            };

        internal static RadarRouteDistance ToSDK(this iOSBinding.RadarRouteDistance distance)
            => new RadarRouteDistance
            {
                Value = distance.Value,
                Text = distance.Text
            };

        internal static RadarRouteDuration ToSDK(this iOSBinding.RadarRouteDuration duration)
            => new RadarRouteDuration
            {
                Value = duration.Value,
                Text = duration.Text
            };

        internal static RadarRouteGeometry ToSDK(this iOSBinding.RadarRouteGeometry geometry)
            => new RadarRouteGeometry
            {
                Coordinates = geometry.Coordinates?.Select(ToSDK)
            };

        internal static RadarAddress ToSDK(this iOSBinding.RadarAddress address)
            => new RadarAddress
            {
                Coordinate = address.Coordinate.ToSDK(),
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
                Number = address.Number,
                AddressLabel = address.AddressLabel,
                PlaceLabel = address.PlaceLabel,
                Confidence = (RadarAddressConfidence)address.Confidence
            };

        internal static RadarCoordinate ToSDK(this CLLocationCoordinate2D coordinate)
            => new RadarCoordinate
            {
                Latitude = coordinate.Latitude,
                Longitude = coordinate.Longitude
            };

        internal static Location ToSDK(this CLLocation location)
            => new Location
            {
                Latitude = location.Coordinate.Latitude,
                Longitude = location.Coordinate.Longitude,
                Accuracy = Math.Min(location.HorizontalAccuracy, location.VerticalAccuracy),
                Course = (float)location.Course,
                Altitude = location.Altitude,
                Speed = (float)location.Speed,
                Timestamp = (DateTime)location.Timestamp
            };

        internal static RadarEvent ToSDK(this iOSBinding.RadarEvent ev)
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
                VerifiedPlace = ev.VerifiedPlace?.ToSDK(),
                Verification = (RadarEventVerification)ev.Verification,
                Confidence = (RadarEventConfidence)ev.Confidence,
                Duration = ev.Duration,
                Location = ev.Location?.ToSDK()
            };

        internal static RadarUser ToSDK(this iOSBinding.RadarUser user)
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
                Source = (RadarLocationSource)user.Source,
                Proxy = user.Proxy,
                Trip = user.Trip?.ToSDK()
            };

        internal static RadarSegment ToSDK(this iOSBinding.RadarSegment segment)
            => new RadarSegment
            {
                Description = segment.Description,
                ExternalId = segment.ExternalId
            };

        internal static RadarUserInsights ToSDK(this iOSBinding.RadarUserInsights insights)
            => new RadarUserInsights
            {
                HomeLocation = insights.HomeLocation?.ToSDK(),
                OfficeLocation = insights.OfficeLocation?.ToSDK(),
                State = insights.State?.ToSDK(),
            };

        internal static RadarUserInsightsState ToSDK(this iOSBinding.RadarUserInsightsState state)
            => new RadarUserInsightsState
            {
                Home = state.Home,
                Office = state.Office,
                Traveling = state.Traveling,
                Commuting = state.Commuting
            };

        internal static RadarUserInsightsLocation ToSDK(this iOSBinding.RadarUserInsightsLocation location)
            => new RadarUserInsightsLocation
            {
                Type = (RadarUserInsightsLocationType)location.Type,
                Location = location.Location?.ToSDK(),
                Confidence = (RadarUserInsightsLocationConfidence)location.Confidence,
                UpdatedAt = (DateTime?)location.UpdatedAt,
                Country = location.Country?.ToSDK(),
                State = location.State?.ToSDK(),
                DMA = location.Dma?.ToSDK(),
                PostalCode = location.PostalCode?.ToSDK(),
            };

        internal static RadarTrip ToSDK(this iOSBinding.RadarTrip trip)
            => new RadarTrip
            {
                Id = trip._id,
                ExternalId = trip.ExternalId,
                Metadata = trip.Metadata?.ToSDK(),
                DestinationGeofenceTag = trip.DestinationGeofenceTag,
                DestinationGeofenceExternalId = trip.DestinationGeofenceExternalId,
                DestinationLocation = trip.DestinationLocation?.ToSDK(),
                Mode = (RadarRouteMode)trip.Mode,
                EtaDistance = trip.EtaDistance,
                EtaDuration = trip.EtaDuration,
                Status = (RadarTripStatus)trip.Status
            };

        internal static RadarRegion ToSDK(this iOSBinding.RadarRegion region)
            => new RadarRegion
            {
                Id = region._id,
                Name = region.Name,
                Code = region.Code,
                Type = region.Type,
                Flag = region.Flag
            };

        internal static RadarBeacon ToSDK(this iOSBinding.RadarBeacon beacon)
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

        internal static RadarGeofence ToSDK(this iOSBinding.RadarGeofence geofence)
            => new RadarGeofence
            {
                Id = geofence._id,
                Description = geofence.Description,
                Tag = geofence.Tag,
                ExternalId = geofence.ExternalId,
                Metadata = geofence.Metadata?.ToSDK(),
                Geometry = geofence.Geometry?.ToSDK()
            };

        internal static RadarGeofenceGeometry ToSDK(this iOSBinding.RadarGeofenceGeometry geometry)
        {
            if (geometry is iOSBinding.RadarCircleGeometry)
            {
                var circle = geometry as iOSBinding.RadarCircleGeometry;
                return new RadarCircleGeometry
                {
                    Center = circle.Center?.ToSDK(),
                    Radius = circle.Radius
                };
            }

            if (geometry is iOSBinding.RadarPolygonGeometry)
            {
                var polygon = geometry as iOSBinding.RadarPolygonGeometry;
                return new RadarPolygonGeometry
                {
                    Center = polygon.Center?.ToSDK(),
                    Radius = polygon.Radius,
                    Coordinates = polygon.Coordinates?.Select(ToSDK)
                };
            }

            return null;
        }

        internal static RadarCoordinate ToSDK(this iOSBinding.RadarCoordinate coordinate)
            => new RadarCoordinate
            {
                Latitude = coordinate.Coordinate.Latitude,
                Longitude = coordinate.Coordinate.Longitude
            };

        internal static RadarPlace ToSDK(this iOSBinding.RadarPlace place)
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

        internal static RadarChain ToSDK(this iOSBinding.RadarChain chain)
            => new RadarChain
            {
                Slug = chain.Slug,
                Name = chain.Name,
                ExternalId = chain.ExternalId,
                Metadata = chain.Metadata?.ToSDK()
            };

        internal static JSONObject ToSDK(this Foundation.NSDictionary metadata)
        {
            var res = new JSONObject();
            foreach (var pair in metadata)
                res.Add(pair.Key.ToString(), pair.Value?.ToSDK());
            return res;
        }

        internal static object ToSDK(this Foundation.NSObject obj)
        {
            if (obj is Foundation.NSNumber num)
            {
                if (num.ObjCType == "c")
                    return num.BoolValue;
                else return num.Int32Value;
            }

            return obj.ToString();
        }

        internal static RadarTrackingOptions ToSDK(this iOSBinding.RadarTrackingOptions options)
            => new RadarTrackingOptions
            {
                DesiredStoppedUpdateInterval = options.DesiredStoppedUpdateInterval,
                DesiredMovingUpdateInterval = options.DesiredMovingUpdateInterval,
                DesiredSyncInterval = options.DesiredSyncInterval,
                StopDuration = options.StopDuration,
                StopDistance = options.StopDistance,
                UseStoppedGeofence = options.UseStoppedGeofence,
                StoppedGeofenceRadius = options.StoppedGeofenceRadius,
                UseMovingGeofence = options.UseMovingGeofence,
                MovingGeofenceRadius = options.MovingGeofenceRadius,
                SyncGeofences = options.SyncGeofences,
                ShowBlueBar = options.ShowBlueBar,
                UseVisits = options.UseVisits,
                UseSignificantLocationChanges = options.UseSignificantLocationChanges
            };

        internal static iOSBinding.RadarTripOptions ToBinding(this RadarTripOptions options)
            => new iOSBinding.RadarTripOptions
            {
                ExternalId = options.ExternalId,
                DestinationGeofenceTag = options.DestinationGeofenceTag,
                DestinationGeofenceExternalId = options.DestinationGeofenceExternalId,
                Mode = (iOSBinding.RadarRouteMode)Math.Pow(2, (double)options.Mode),
                Metadata = options.Metadata?.ToBinding()
            };

        internal static iOSBinding.RadarTrackingOptions ToBinding(this RadarTrackingOptions options)
            => new iOSBinding.RadarTrackingOptions
            {
                DesiredStoppedUpdateInterval = options.DesiredStoppedUpdateInterval,
                DesiredMovingUpdateInterval = options.DesiredMovingUpdateInterval,
                DesiredSyncInterval = options.DesiredSyncInterval,
                DesiredAccuracy = options.DesiredAccuracy == RadarTrackingOptionsDesiredAccuracy.None ? iOSBinding.RadarTrackingOptionsDesiredAccuracy.Low : (iOSBinding.RadarTrackingOptionsDesiredAccuracy)options.DesiredAccuracy,
                StopDuration = options.StopDuration,
                StopDistance = options.StopDistance,
                StartTrackingAfter = (Foundation.NSDate)options.StartTrackingAfter,
                StopTrackingAfter = (Foundation.NSDate)options.StopTrackingAfter,
                Replay = (iOSBinding.RadarTrackingOptionsReplay)options.Replay,
                Sync = InvertEnum<iOSBinding.RadarTrackingOptionsSync>((int)options.Sync),
                ShowBlueBar = options.ShowBlueBar,
                UseStoppedGeofence = options.UseStoppedGeofence,
                StoppedGeofenceRadius = options.StoppedGeofenceRadius,
                UseMovingGeofence = options.UseMovingGeofence,
                MovingGeofenceRadius = options.MovingGeofenceRadius,
                SyncGeofences = options.SyncGeofences,
                UseVisits = options.UseVisits,
                UseSignificantLocationChanges = options.UseSignificantLocationChanges,
                Beacons = options.Beacons
            };

        internal static CLLocation ToBinding(this Location location)
            => new CLLocation(
                new CLLocationCoordinate2D
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                },
                location.Altitude ?? 0,
                location.Accuracy ?? 0,
                location.Accuracy ?? 0,
                location.Course ?? 0,
                location.Speed ?? 0,
                (Foundation.NSDate)location.Timestamp.LocalDateTime);

        internal static iOSBinding.RadarRouteMode ToBinding(this IEnumerable<RadarRouteMode> modes)
            => (iOSBinding.RadarRouteMode)modes?.Sum(m => Math.Pow(2, (double)m));

        internal static Foundation.NSDictionary ToBinding(this JSONObject metadata)
            => Foundation.NSDictionary<Foundation.NSString, Foundation.NSObject>.FromObjectsAndKeys(
                metadata.Values.ToArray(),
                metadata.Keys.ToArray(),
                metadata.Count);

        internal static T InvertEnum<T>(int val)
            => Enum.GetValues(typeof(T)).Cast<T>().Reverse().ElementAt(val);
    }
}
