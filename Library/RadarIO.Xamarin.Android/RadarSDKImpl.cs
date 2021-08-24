using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using AndroidX.AppCompat.App;

namespace RadarIO.Xamarin
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "io.radar.sdk.RECEIVED" })]
    public class RadarSDKImpl : AndroidBinding.RadarReceiver, RadarSDK
    {
        public event RadarEventHandler<(IEnumerable<RadarEvent>, RadarUser)> EventsReceived;
        public event RadarEventHandler<(RadarLocation, RadarUser)> LocationUpdated;
        public event RadarEventHandler<(RadarLocation, bool, RadarLocationSource)> ClientLocationUpdated;
        public event RadarEventHandler<RadarStatus> Error;
        public event RadarEventHandler<string> Log;

        public void Initialize(string publishableKey)
        {
            AndroidBinding.Radar.Initialize(Android.App.Application.Context, publishableKey);
            //Application.Context.RegisterReceiver(this, new IntentFilter("io.radar.sdk.RECEIVED"));
        }

        public Task<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> TrackOnce()
        {
            var handler = new TrackCallbackHandler();
            AndroidBinding.Radar.TrackOnce(handler);
            return handler.Result;
        }

        public void StartTracking(RadarTrackingOptions options)
        {
            AndroidBinding.Radar.StartTracking(options.ToBinding());
        }

        public void StopTracking()
        {
            AndroidBinding.Radar.StopTracking();
        }

        public Task<RadarStatus> StartTrip(RadarTripOptions options)
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.StartTrip(options.ToBinding(), handler);
            return handler.Result;
        }

        public Task<RadarStatus> CancelTrip()
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.CancelTrip(handler);
            return handler.Result;
        }

        public Task<RadarStatus> CompleteTrip()
        {
            var handler = new TripCallbackHandler();
            AndroidBinding.Radar.CompleteTrip(handler);
            return handler.Result;
        }

        #region RadarReceiver

        RadarSDKImpl Radar => (RadarSDKImpl)RadarSingleton.Radar;

        public override void OnClientLocationUpdated(Context context, Location location, bool stopped, AndroidBinding.Radar.RadarLocationSource source)
        {
            LaunchApp(context);
            Radar.ClientLocationUpdated?.Invoke((location?.ToSDK(), stopped, (RadarLocationSource)source.Ordinal()));
        }

        public override void OnError(Context context, AndroidBinding.Radar.RadarStatus status)
        {
            LaunchApp(context);
            Radar.Error?.Invoke((RadarStatus)status.Ordinal());
        }

        public override void OnEventsReceived(Context context, AndroidBinding.RadarEvent[] events, AndroidBinding.RadarUser user)
        {
            LaunchApp(context);
            Radar.EventsReceived?.Invoke((events?.Select(e => e?.ToSDK()), user?.ToSDK()));
        }

        public override void OnLocationUpdated(Context context, Location location, AndroidBinding.RadarUser user)
        {
            LaunchApp(context);
            Radar.LocationUpdated?.Invoke((location?.ToSDK(), user?.ToSDK()));
        }

        public override void OnLog(Context context, string message)
        {
            LaunchApp(context);
            Radar.Log?.Invoke(message);
        }

        private static void LaunchApp(Context context)
        {
            //if (RadarSingleton.IsInitialized)
            //    return;

            //var intent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
            //intent.SetFlags(ActivityFlags.NewTask);
            //context.StartActivity(intent);
        }

        #endregion
    }

    public partial class RadarTrackingOptions
    {
        public int FastestStoppedUpdateInterval;
        public int FastestMovingUpdateInterval;
        public int SyncGeofencesLimit;
        public RadarTrackingOptionsForegroundService ForegroundService;
        public static RadarTrackingOptions Responsive
            => AndroidBinding.RadarTrackingOptions.Responsive.ToSDK();
        public static RadarTrackingOptions Continuous
            => AndroidBinding.RadarTrackingOptions.Continuous.ToSDK();
        public static RadarTrackingOptions Efficient
            => AndroidBinding.RadarTrackingOptions.Efficient.ToSDK();
    }

    public class RadarTrackingOptionsForegroundService
    {
        public string Text;
        public string Title;
        public int Icon;
        public bool UpdatesOnly;
        public string Activity;
        public int Importance;
        public int Id;
    }
}
