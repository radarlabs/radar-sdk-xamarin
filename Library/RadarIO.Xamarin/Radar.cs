using System;

namespace RadarIO.Xamarin
{
    public static class Radar
    {
        private static Lazy<RadarSDK> instance
            = new Lazy<RadarSDK>(() => CreateInstance(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        private static RadarSDK CreateInstance()
        {
#if LIBRARY
            return null;
#else
            return new RadarSDKImpl();
#endif
        }
    }
}