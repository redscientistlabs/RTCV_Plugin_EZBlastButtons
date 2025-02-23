using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons
{
    public class EngineSupportInfo
    {
        public string EngineName { get; private set; }

        public Func<PartialSpec> DefaultPartialFunc { get; set; }

        public int ComboBoxIndex { get; set; }

        public CorruptionEngine CorruptionEngineType { get; private set; }

        public bool Supported => this.ComboBoxIndex > -1;

        public EngineSupportInfo(string engineName, Func<PartialSpec> defaultPartialFunc, CorruptionEngine corruptionEngine)
        {
            EngineName = engineName;
            DefaultPartialFunc = defaultPartialFunc;
            CorruptionEngineType = corruptionEngine;
        }

        public void RefreshSupported()
        {

        }

    }
}
