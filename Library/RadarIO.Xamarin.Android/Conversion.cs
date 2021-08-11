using System;

namespace RadarIO.Xamarin
{
    public static class Conversion
    {
        public static RadarStatus ToSDK(this AndroidBinding.Radar.RadarStatus status)
            => (RadarStatus)status.Ordinal();
    }
}
