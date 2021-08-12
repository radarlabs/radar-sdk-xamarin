using Foundation;
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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TrackOnce.TouchUpInside += async (sender, e) =>
            {
                var (status, location, events, user) = await Radar.TrackOnce();

                var notification = new UILocalNotification
                {
                    FireDate = NSDate.Now,
                    AlertAction = "View Alert",
                    AlertBody = $"Status: {status}\nLocation: {location?.Latitude} {location?.Longitude}\nEvents: {events?.Count()}\nUser: {user?.UserId}",
                    ApplicationIconBadgeNumber = 1,
                    SoundName = UILocalNotification.DefaultSoundName
                };

                // schedule it
                UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            };
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
