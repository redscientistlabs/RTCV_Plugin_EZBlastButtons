using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerBoard.Structures
{
    public class ButtonDef
    {
        public string Name { get; set; }
        public string Limiter { get; set; }
        public string Value { get; set; }
        public long Intensity { get; set; }
        public string[] Domains { get; set; }
    }
}
