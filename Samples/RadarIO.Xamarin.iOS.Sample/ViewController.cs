using Foundation;
using RadarIO.Xamarin.Shared.Sample;
using System;
using System.Linq;
using UIKit;

using static RadarIO.Xamarin.RadarSingleton;

namespace RadarIO.Xamarin.iOS.Sample
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        bool toggle = false;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Radar.Log += msg =>
            //{

            //};

            //Radar.StartTrip(new RadarTripOptions
            //{
            //    ExternalId = "999",
            //    DestinationGeofenceTag = "tag",
            //    DestinationGeofenceExternalId = "id",
            //    Mode = RadarRouteMode.Car,
            //    Metadata = new JSONObject
            //            {
            //                { "bing", "bong" },
            //                { "ding", "dong" },
            //                { "int", 10 },
            //                { "bool", true }
            //            }
            //});
            //Radar.StartTracking(Radar.ContinuousTrackingOptions);
            TrackOnce.TouchUpInside += async (sender, e) =>
            {
                toggle = !toggle;

                var (status, location, events, user) = await Demo.Test();
                //var fences = user.Geofences.ToList();
                //if (toggle)
                //{
                //    var status = await Radar.StartTrip(new RadarTripOptions
                //    {
                //        ExternalId = "999",
                //        DestinationGeofenceTag = "tag",
                //        DestinationGeofenceExternalId = "id",
                //        Mode = RadarRouteMode.Car,
                //        Metadata = new JSONObject
                //        {
                //            { "bing", "bong" },
                //            { "ding", "dong" },
                //            { "int", 10 },
                //            { "bool", true }
                //        }
                //    });
                //    Radar.StartTracking(RadarTrackingOptions.Continuous);

                //    TrackOnce.SetTitle("Complete Trip", UIControlState.Normal);
                //    UIApplication.SharedApplication.ScheduleLocalNotification(
                //        new UILocalNotification
                //        {
                //            FireDate = NSDate.Now,
                //            AlertAction = "View Alert",
                //            AlertBody = $"Started trip: {status}", //\nLocation: {location?.Latitude} {location?.Longitude}\nEvents: {events?.Count()}\nUser: {user?.UserId}",
                //            ApplicationIconBadgeNumber = 1,
                //            SoundName = UILocalNotification.DefaultSoundName
                //        });
                //}
                //else
                //{
                //    var status = await Radar.CompleteTrip();
                //    Radar.StopTracking();

                //    TrackOnce.SetTitle("Start Trip", UIControlState.Normal);
                UIApplication.SharedApplication.ScheduleLocalNotification(
                    new UILocalNotification
                    {
                        FireDate = NSDate.Now,
                        AlertAction = "View Alert",
                        AlertBody = $"Result: {status}\nLocation: {location?.Latitude} {location?.Longitude}\nEvents: {events?.Count()}\nUser: {user?.UserId}",
                            ApplicationIconBadgeNumber = 1,
                        SoundName = UILocalNotification.DefaultSoundName
                    });
                //}
            };
        }
    }
}
