using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons
{

    internal static class PluginRouting
    {
        internal const string PREFIX = "EZBLAST";
        internal static class Endpoints
        {

            public const string EMU_SIDE = PREFIX + "_" + "EMU";
            public const string RTC_SIDE = PREFIX + "_" + "RTC";
        }

        /// <summary>
        /// Add your commands here
        /// </summary>
        internal static class Commands
        {
            public const string CORRUPT = PREFIX + "_" + nameof(CORRUPT);
            public const string UPDATE_SETTINGS = PREFIX + "_" + nameof(UPDATE_SETTINGS);
            public const string UPDATE_SHARED_SETTINGS = PREFIX + "_" + nameof(UPDATE_SHARED_SETTINGS);
            public const string SHOW_WINDOW = PREFIX + "_" + nameof(SHOW_WINDOW);
        }
    }
}
