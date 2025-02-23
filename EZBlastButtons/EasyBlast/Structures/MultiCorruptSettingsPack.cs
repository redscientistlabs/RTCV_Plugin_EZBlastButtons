using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using Newtonsoft.Json;

namespace EZBlastButtons.Structures
{
    //AKA a button, rename
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MultiCorruptSettingsPack
    {
        [JsonProperty]
        public string Name { get; set; } = "Unnamed";
        [JsonProperty]
        public List<EngineSettings> Settings { get; private set; } = new List<EngineSettings>();


        //[JsonProperty]
        //public string EmulatorCore { get; set; } = "";
        //[JsonProperty]
        //public bool UseAnyCore { get; set; } = true;



        public MultiCorruptSettingsPack()
        {
        }

        public MultiCorruptSettingsPack Duplicate()
        {
            var p = new MultiCorruptSettingsPack();
            for (int i = 0; i < Settings.Count; i++)
            {
                p.Settings.Add(Settings[i].Duplicate());
            }
            p.Name = Name;
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
        }

        public void AddSetting(EngineSettings setting)
        {
            Settings.Add(setting);
        }
        public void RemoveSetting(EngineSettings setting)
        {
            Settings.Remove(setting);
        }

    }

}
