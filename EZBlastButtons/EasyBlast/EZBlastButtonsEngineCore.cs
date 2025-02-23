using EZBlastButtons.Structures;
using RTCV.Common;
using RTCV.Common.CustomExtensions;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EZBlastButtons
{
    static class EZBlastButtonsEngineCore
    {
        static EZBlastSharedSettings sharedSettings;

        static MultiCorruptSettingsPack pack = null;
        public static long OverrideIntensity { get; internal set; } = 0;

        public static void SetSettings(MultiCorruptSettingsPack settingPack)
        {
            pack = settingPack;
        }
        public static void SetSharedSettings(EZBlastSharedSettings settings)
        {
            sharedSettings = settings;
        }


        public static BlastLayer Corrupt()
        {
            //Cache before our changes
            C.CacheMasterSpec();

            List<BlastUnit> bus = new List<BlastUnit>();
            if (pack == null) return new BlastLayer(bus);

            long intensity = sharedSettings.Intensity;// (pack.OverrideIntensity > 0) ? pack.OverrideIntensity : RtcCore.Intensity;
            for (int settingInd = 0; settingInd < pack.Settings.Count; settingInd++)
            {
                var setting = pack.Settings[settingInd];
                setting.ApplyPartial();
                //if(setting.EngineType == CorruptionEngine.CUSTOM)
                //{
                //    CustomEngine.
                //}

                int precision = setting.Precision;
                int alignment = setting.Alignment;

                //Calculate intensity to use
                long settingIntensity = setting.ForcedIntensity > 0 ? setting.ForcedIntensity : (long)(setting.Percentage * intensity);

                //Get domains in setting that exist, if none, use the selected ones from rtc
                var validDoms = (setting.Domains != null && setting.Domains.Length > 0) ? GetValidDomains(setting.Domains) : null;
                string[] domains = validDoms != null ? validDoms : (string[])AllSpec.UISpec["SELECTEDDOMAINS"];

                //No valid domains, skip
                if (domains == null || domains.Length == 0) continue;

                bool useAlignment = (bool)AllSpec.CorruptCoreSpec[RTCSPEC.CORE_USEALIGNMENT];
                for (int i = 0; i < settingIntensity; i++)
                {
                    string domain = domains[RtcCore.RND.Next(domains.Length)];
                    MemoryInterface mi = MemoryDomains.GetInterface(domain);
                    bus.AddRange(setting.GetBlastUnits(domain, RtcCore.RND.NextLong(0, mi.Size - (precision * 2)), precision, alignment, useAlignment));
                }
            }
            //Restore spec without pushing
            C.RestoreMasterSpec(false);

            return new BlastLayer(bus.Where(x => x != null).ToList());
        }

        private static string[] GetValidDomains(params string[] doms)
        {
            return doms.Where(x => MemoryDomains.AllMemoryInterfaces.ContainsKey(x)).ToArray();
        }

    }
}
