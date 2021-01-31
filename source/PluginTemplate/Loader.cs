using NLog;
using EasyBlast.UI;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace EasyBlast
{
    [Export(typeof(IPlugin))]
    public class Loader : IPlugin, IDisposable
    {
        public static RTCSide CurrentSide = RTCSide.Both;
        internal static PluginConnectorEMU connectorEMU = null;
        internal static PluginConnectorRTC connectorRTC = null;

        public string Name => "EZManualBlasts";
        public string Description => "A board so you only push one button to do the funny";

        public string Author => "NullShock78";

        public Version Version => new Version(1, 0, 0);

        public RTCSide SupportedSide => RTCSide.Both;

        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {
            Logging.GlobalLogger.Info($"{Name} v{Version} initializing.");
            if (side == RTCSide.Client)
            {

            }
            else if (side == RTCSide.Server)
            {
                connectorRTC = new PluginConnectorRTC();
                S.GET<RTC_OpenTools_Form>().RegisterTool("Easy Manual Blasts", "Open Easy Manual Blasts", () => { 
                    //This is the method you use to route commands between the RTC side and the Emulator side
                    LocalNetCoreRouter.Route(Endpoint.RTC_SIDE, Commands.SHOW_WINDOW, true); 
                });
            }
            Logging.GlobalLogger.Info($"{Name} v{Version} initialized.");
            CurrentSide = side;
            return true;
        }

        public bool Stop()
        {
            if (Loader.CurrentSide == RTCSide.Server && !S.ISNULL<PluginForm>() && !S.GET<PluginForm>().IsDisposed)
            {
                S.GET<PluginForm>().Close();
            }
            return true;
        }
    }
}
