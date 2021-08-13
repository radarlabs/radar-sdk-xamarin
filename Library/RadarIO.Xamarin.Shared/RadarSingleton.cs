using System;

namespace RadarIO.Xamarin
{
    public static class RadarSingleton
    {
        private static readonly Lazy<RadarSDK> instance
            = new Lazy<RadarSDK>(() => CreateInstance(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        private static RadarSDK CreateInstance()
        {
#if LIBRARY
            return null;
#else
            return new RadarSDKImpl();
#endif
        }

        public static RadarSDK Radar => instance.Value
            ?? throw new NotImplementedException(); // todo: message
    }
}