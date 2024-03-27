using Foundation;
using RadarIO.Sample;
using System;
using System.Linq;
using UIKit;


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

            TrackOnce.TouchUpInside += async (sender, e) =>
            {
                toggle = !toggle;

                var (status, location, events, user) = await Demo.Test();
                UIApplication.SharedApplication.ScheduleLocalNotification(
                    new UILocalNotification
                    {
                        FireDate = NSDate.Now,
                        AlertAction = "View Alert",
                        AlertBody = $"Result: {status}\nLocation: {location?.Latitude} {location?.Longitude}\nEvents: {events?.Count()}\nUser: {user?.UserId}",
                            ApplicationIconBadgeNumber = 1,
                        SoundName = UILocalNotification.DefaultSoundName
                    });
            };
        }
    }
}
