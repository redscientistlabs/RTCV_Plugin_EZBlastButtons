using Ceras;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public struct EZBlastSharedSettings
    {
        public float IntensityMultiplier;
        public long OverrideIntensity;
        public long Intensity => (long)((OverrideIntensity > 0? OverrideIntensity : RtcCore.Intensity) * IntensityMultiplier);

        public EZBlastSharedSettings(float intensityMultiplier = 1.0f, long overrideIntensity = -1)
        {
            OverrideIntensity = overrideIntensity;
            IntensityMultiplier = intensityMultiplier;
        }



        //public EZBlastSharedSettings() { }
    }
}
