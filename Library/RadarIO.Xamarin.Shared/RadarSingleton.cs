using System;

namespace RadarIO.Xamarin
{
    public static class RadarSingleton
    {
        private static readonly Lazy<RadarSDK> instance
            = new Lazy<RadarSDK>(() => new RadarSDKImpl(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static RadarSDK Radar => instance.Value;

        internal static bool IsInitialized => instance.IsValueCreated;
    }
}