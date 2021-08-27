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
        public RadarTrackingOptions ContinuousTrackingOptions
            => AndroidBinding.RadarTrackingOptions.Continuous.ToSDK();
        public RadarTrackingOptions ResponsiveTrackingOptions
            => AndroidBinding.RadarTrackingOptions.Responsive.ToSDK();
        public RadarTrackingOptions EfficientTrackingOptions
            => AndroidBinding.RadarTrackingOptions.Efficient.ToSDK();

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

        public string UserId
        {
            get => AndroidBinding.Radar.UserId;
            set => AndroidBinding.Radar.UserId = value;
        }

        public string Description
        {
            get => AndroidBinding.Radar.Description;
            set => AndroidBinding.Radar.Description = value;
        }

        public JSONObject Metadata
        {
            get => AndroidBinding.Radar.Metadata?.ToSDK();
            set => AndroidBinding.Radar.Metadata = value?.ToBinding();
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

        public void MockTracking(RadarLocation origin, RadarLocation destination, RadarRouteMode mode, int steps, int interval, Action<(RadarStatus, RadarLocation, RadarEvent[], RadarUser)> callback)
        {
            var handler = new RepeatingTrackCallbackHandler(callback);
            AndroidBinding.Radar.MockTracking(
                origin?.ToBinding(),
                destination?.ToBinding(),
                AndroidBinding.Radar.RadarRouteMode.Values()[(int)mode],
                steps,
                interval,
                handler);
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
}
