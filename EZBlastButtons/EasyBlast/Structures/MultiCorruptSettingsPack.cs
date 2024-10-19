using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;

namespace EZBlastButtons.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MultiCorruptSettingsPack
    {
        public string Name { get; set; } = "Unnamed";

        public List<EngineSettings> Settings { get; private set; }
        private static Random rand = new Random();
        public long OverrideIntensity { get; set; } = -1;
        public MultiCorruptSettingsPack()
        {
            Settings = new List<EngineSettings>();
        }



        public MultiCorruptSettingsPack Duplicate()
        {
            var p = new MultiCorruptSettingsPack();
            for (int i = 0; i < Settings.Count; i++)
            {
                p.Settings.Add(Settings[i].Duplicate());
            }
            p.Name = Name;
            p.OverrideIntensity = OverrideIntensity;
            return p;
        }

        public void Extract(MultiCorruptSettingsPack p)
        {
            Settings.Clear();
            for (int i = 0; i < p.Settings.Count; i++)
            {
                Settings.Add(p.Settings[i].Duplicate());
            }
            Name = p.Name;
            OverrideIntensity = p.OverrideIntensity;
        }

        public void AddSetting(EngineSettings setting)
        {
            Settings.Add(setting);
        }
        public void RemoveSetting(EngineSettings setting)
        {
            Settings.Remove(setting);
        }

        public EngineSettings GetRandomSettings()
        {
            if (Settings.Count == 0) return null;
            else return Settings[rand.Next(Settings.Count)];
        }
    }

}
