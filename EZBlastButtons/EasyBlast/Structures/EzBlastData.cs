using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons.Structures
{
    //For json
    public class EzBlastData
    {
        public Dictionary<string, ButtonSet> Sets { get; set; }
    }
}
