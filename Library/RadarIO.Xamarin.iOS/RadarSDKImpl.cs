using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarIO.Xamarin
{
    public class RadarSDKImpl : RadarSDK
    {
        public override Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce()
        {
            var task = new TaskCompletionSource<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)>();
            iOS.Binding.Radar.TrackOnceWithCompletionHandler((status, location, ev, user) =>
            {
                task.SetResult((status.ToSDK(), location?.ToSDK(), ev?.Select(Conversion.ToSDK).ToArray(), user?.ToSDK()));
            });
            return task.Task;
        }
    }
}
