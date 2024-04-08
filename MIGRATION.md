# Migration guides

## Upgrading to 3.7.8

- The `RadarIO.Xamarin` namespace has been replaced with `RadarIO`.
- The use of the `Xamarin.Essentials.Location` class has been replaced with `RadarIO.RadarLocation`.

## Upgrading to 3.5.3

- `foregroundService` is no longer available in `RadarTrackingOptions`. This has been replaced by `Radar.SetForegroundServiceOptions` instead.
- `RadarSingleton` must now be initialized before use. Call `RadarSingleton.Initialize(new RadarSDKImpl())` before using it.

```c#
// 3.5.3

// using statements
using RadarIO.Xamarin; // contains cross-platform sdk library as well as platform-specific RadarSDKImpl
using static RadarIO.Xamarin.RadarSingleton; // to use static Radar instance

// initialize
RadarSingleton.Initialize(new RadarSDKImpl()); // RadarSDKImpl can only be constructed from an Android or iOS project, not a NetStandard project

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
