using Foundation;
using RadarIO.Xamarin.iOSBinding;
using System;
using UIKit;

namespace iOSBindingTest
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TrackOnce.TouchUpInside += (sender, e) =>
            {
                Radar.TrackOnceWithCompletionHandler((a, b, c, d) =>
                {
                    // create the notification
                    var notification = new UILocalNotification();

                    // set the fire date (the date time in which it will fire)
                    notification.FireDate = NSDate.Now;

                    // configure the alert
                    notification.AlertAction = "View Alert";
                    notification.AlertBody = $"{a} {b} {c} {d}";

                    // modify the badge
                    notification.ApplicationIconBadgeNumber = 1;

                    // set the sound to be the default sound
                    notification.SoundName = UILocalNotification.DefaultSoundName;

                    // schedule it
                    UIApplication.SharedApplication.ScheduleLocalNotification(notification);
                });
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
