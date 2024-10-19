using Newtonsoft.Json;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons.Structures
{
    [Serializable]
    public class SaveableEngineSettings
    {
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        List<SpecKVP> cachedSpec = new List<SpecKVP>();
        //Dictionary<string,object> cachedSpec = new Dictionary<string,object>();

        public SaveableEngineSettings() { }
        public SaveableEngineSettings(PartialSpec otherSpec)
        {
            Name = otherSpec.Name;
            ExtractFrom(otherSpec);
        }

        void ExtractFrom(PartialSpec otherSpec)
        {
            var keys = otherSpec.GetKeys();
            foreach (var key in keys)
            {
                cachedSpec.Add(new SpecKVP(key, otherSpec.Get<object>(key)));
                //cachedSpec[key] = otherSpec.Get<object>(key);
            }
        }

        public static implicit operator PartialSpec(SaveableEngineSettings sp)
        {
            var ps = new PartialSpec(sp.Name);
            foreach (var kvp in sp.cachedSpec)
            {
                ps[kvp.Key] = kvp.Value;
            }
            return ps;
        }


    }
}
