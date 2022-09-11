using EZBlastButtons.UI;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace EZBlastButtons
{
    [Export(typeof(IPlugin))]
    public class PluginCore : IPlugin, IDisposable
    {
        public static RTCSide CurrentSide = RTCSide.Both;
        internal static PluginConnectorEMU connectorEMU = null;
        internal static PluginConnectorRTC connectorRTC = null;

        public string Name => "EZ Blast Buttons";
        public string Description => "A board so you only push one button to do the funny";

        public string Author => "NullShock78";

        public Version Version => Ver;
        public static Version Ver => new Version(2, 0, 1);

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
                S.GET<OpenToolsForm>().RegisterTool("EZ Blast Buttons", "Open EZ Blast Buttons", () => { 
                    //This is the method you use to route commands between the RTC side and the Emulator side
                    LocalNetCoreRouter.Route(Endpoint.RTC_SIDE, Commands.SHOW_WINDOW, true); 
                });
            }
            Logging.GlobalLogger.Info($"{Name} v{Version} initialized.");
            CurrentSide = side;
            return true;
        }

        public bool StopPlugin()
        {
            if (!S.ISNULL<PluginForm>() && !S.GET<PluginForm>().IsDisposed)
            {
                SyncObjectSingleton.FormExecute(() =>
                {
                    S.GET<PluginForm>().Close();
                });
            }

            return true;
        }
    }
}
