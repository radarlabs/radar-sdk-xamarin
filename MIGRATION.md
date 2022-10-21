# Migration guides

## 3.2.x to 3.5.3

- `foregroundService` is no longer available in `RadarTrackingOptions`. This has been replaced by `Radar.SetForegroundServiceOptions` instead.
- `RadarSingleton` must now be initialized before use. Call `RadarSingleton.Initialize(new RadarSDKImpl())` from the iOS/Android project before using it.
