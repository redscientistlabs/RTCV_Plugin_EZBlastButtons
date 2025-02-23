using Ceras;
using Newtonsoft.Json;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class ButtonSet
    {
        [JsonProperty]
        public string EmulatorCore { get; set; } = "";
        [JsonProperty]
        public bool RestrictCore { get; set; } = false;

        [JsonProperty]
        public List<MultiCorruptSettingsPack> Buttons { get; set; }

        public ButtonSet() { }

        public void AssignCore(string core)
        {
            EmulatorCore = core;//(AllSpec.VanguardSpec[VSPEC.SYSTEM] as string);
            RestrictCore = !string.IsNullOrWhiteSpace(core);
        }

    }
}
