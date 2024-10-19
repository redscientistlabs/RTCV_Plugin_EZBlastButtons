using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons.Structures
{
    [Serializable]
    public class SpecKVP
    {
        [JsonProperty]
        public string Key;

        [JsonProperty]
        public object Value;
        public SpecKVP() { }
        public SpecKVP(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
