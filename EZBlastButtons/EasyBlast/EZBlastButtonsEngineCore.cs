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
        //static EngineSettings settings = null;
        //static bool first = false;


        //public static void SetSettings(EngineSettings setting)
        //{
        //    settings = setting;
        //}

        //public static BlastLayer Corrupt()
        //{

        //    List<BlastUnit> bus = new List<BlastUnit>();
        //    if (settings == null) return null;


        //    settings.ApplyPartial();
        //    long intensity = (OverrideIntensity > 0) ? OverrideIntensity : RtcCore.Intensity;
        //    int precision = settings.Precision;
        //    int alignment = settings.Alignment;
        //    long settingIntensity = settings.ForcedIntensity > 0 ? settings.ForcedIntensity : (long)(settings.Percentage * intensity);

        //    //Get domains in setting that exist, if none, use the selected ones from rtc
        //    var validDoms = (settings.Domains != null && settings.Domains.Length > 0) ? GetValidDomains(settings.Domains) : null;
        //    string[] domains = validDoms != null ? validDoms : (string[])AllSpec.UISpec["SELECTEDDOMAINS"];
        //    //No valid domains, skip
        //    if (domains == null || domains.Length == 0) return null;// new BlastLayer(bus);

        //    for (int i = 0; i < settingIntensity; i++)
        //    {
        //        string domain = domains[RtcCore.RND.Next(domains.Length)];
        //        MemoryInterface mi = MemoryDomains.GetInterface(domain);
        //        bus.AddRange(settings.GetBlastUnits(domain, RtcCore.RND.NextLong(0, mi.Size - (precision * 2)), precision, alignment));
        //    }

        //    var bl = new BlastLayer(bus.Where(x => x != null).ToList());
        //    if(bl.Layer.Count > 0)
        //    {
        //        return bl;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        static MultiCorruptSettingsPack pack = null;
        public static long OverrideIntensity { get; internal set; } = 0;

        public static void SetSettings(MultiCorruptSettingsPack settingPack)
        {
            pack = settingPack;
        }

        public static BlastLayer Corrupt()
        {
            //Cache before our changes
            C.CacheMasterSpec();

            List<BlastUnit> bus = new List<BlastUnit>();
            if (pack == null) return new BlastLayer(bus);

            long intensity = (pack.OverrideIntensity > 0) ? pack.OverrideIntensity : RtcCore.Intensity;
            for (int settingInd = 0; settingInd < pack.Settings.Count; settingInd++)
            {
                var setting = pack.Settings[settingInd];
                setting.ApplyPartial();
                int precision = setting.Precision;
                int alignment = setting.Alignment;

                //Calculate intensity to use
                long settingIntensity = setting.ForcedIntensity > 0 ? setting.ForcedIntensity : (long)(setting.Percentage * intensity);

                //Get domains in setting that exist, if none, use the selected ones from rtc
                var validDoms = (setting.Domains != null && setting.Domains.Length > 0) ? GetValidDomains(setting.Domains) : null;
                string[] domains = validDoms != null ? validDoms : (string[])AllSpec.UISpec["SELECTEDDOMAINS"];
                //No valid domains, skip
                if (domains == null || domains.Length == 0) continue;

                for (int i = 0; i < settingIntensity; i++)
                {
                    string domain = domains[RtcCore.RND.Next(domains.Length)];
                    MemoryInterface mi = MemoryDomains.GetInterface(domain);
                    //TODO: switch on engine name
                    bus.AddRange(setting.GetBlastUnits(domain, RtcCore.RND.NextLong(0, mi.Size - (precision * 2)), precision, alignment));
                }
            }
            //Cache before our changes
            C.RestoreMasterSpec(false);

            return new BlastLayer(bus.Where(x => x != null).ToList());
        }

        private static string[] GetValidDomains(params string[] doms)
        {
            return doms.Where(x => MemoryDomains.AllMemoryInterfaces.ContainsKey(x)).ToArray();
        }

    }
}
