# Migration guides

## 3.2.x to 3.5.3

- `foregroundService` is no longer available in `RadarTrackingOptions`. This has been replaced by `Radar.SetForegroundServiceOptions` instead.
- `RadarSingleton` must now be initialized before use. Call `RadarSingleton.Initialize(new RadarSDKImpl())` before using it.

```c#
// 3.5.3

// using statements
using RadarIO.Xamarin; // contains cross-platform sdk library as well as platform-specific RadarSDKImpl
using static RadarIO.Xamarin.RadarSingleton; // to use static Radar instance

// initialize
RadarSingleton.Initialize(new RadarSDKImpl());

// enable or disable the foreground service
RadarTrackingOptions trackingOptions = RadarTrackingOptions(...);
trackingOptions.ForegroundServiceEnabled = true;
// set the foreground service options
RadarTrackingOptionsForegroundService foregroundOptions = RadarTrackingOptionsForegroundService(...);
Radar.SetForegroundServiceOptions(foregroundOptions);
// start tracking
Radar.StartTracking(trackingOptions);
```

```c#
// 3.2.x

// initialize
RadarSingleton.Initialize();

// enabling foreground service
RadarTrackingOptions trackingOptions = RadarTrackingOptions(...);
trackingOptions.ForegroundService = RadarTrackingOptionsForegroundService(...);
Radar.StartTracking(trackingOptions);
```
