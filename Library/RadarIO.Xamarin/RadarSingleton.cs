using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RadarIO.Xamarin
{
    public static class RadarSingleton
    {
        private static RadarSDK instance;
        public static RadarSDK Radar => instance
            ?? throw new Exception("RadarSingleton must be initalized with a RadarSDKImpl instance before use.");

        public static void Initialize(RadarSDK sdk)
        {
            if (instance != null)
                throw new Exception("RadarSingleton has already been initialized!");

            instance = sdk;
        }
    }
}