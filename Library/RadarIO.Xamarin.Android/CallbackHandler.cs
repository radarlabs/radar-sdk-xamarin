using System;
using System.Threading.Tasks;
using Android.Locations;
using RadarIO.Xamarin.AndroidBinding;

namespace RadarIO.Xamarin
{
    public abstract class CallbackHandler<T> : Java.Lang.Object
    {
        protected TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();

        public Task<T> Result => taskSource.Task;
    }

    public class TrackCallbackHandler
        : CallbackHandler<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)>
        , AndroidBinding.Radar.IRadarTrackCallback
    {
        public void OnComplete(Radar.RadarStatus status, Location location, IO.Radar.Sdk.Model.RadarEvent[] events, IO.Radar.Sdk.Model.RadarUser user)
        {
            // todo
            taskSource.SetResult((status.ToSDK(), null, null, null));
        }
    }
}
