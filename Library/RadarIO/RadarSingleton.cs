using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RadarIO
{
    public static class RadarSingleton
    {
        private static RadarSDK instance;
        /// <summary>
        /// Singleton instance of RadarSDK, provided for convenience. Must be initialized with a RadarSDKImpl instance before use.
        /// </summary>
        public static RadarSDK Radar => instance
            ?? throw new Exception("RadarSingleton must be initialized with a RadarSDKImpl instance before use.");

        /// <summary>
        /// Initialize the singleton with an instance of RadarSDKImpl.
        /// </summary>
        /// <param name="sdk">An instance of RadarSDKImpl.</param>
        public static void Initialize(RadarSDK sdk)
        {
            if (instance != null)
                throw new Exception("RadarSingleton has already been initialized!");

            instance = sdk;
        }
    }
}