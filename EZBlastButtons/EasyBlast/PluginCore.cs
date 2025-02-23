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
        public static Version Ver => new Version(3, 1, 0);
        //public static int EngineIndex { get; private set; }
        public RTCSide SupportedSide => RTCSide.Both;
        internal static EZBlastEngineImplementation EngineImplementation { get; private set;}
        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {
            C.Init();

            Logging.GlobalLogger.Info($"{Name} v{Version} initializing.");
            if (side == RTCSide.Client)
            {
                connectorEMU = new PluginConnectorEMU();
            }
            else if (side == RTCSide.Server)
            {
                connectorRTC = new PluginConnectorRTC();

                var form = new EZBlastEngineForm();
                S.SET<EZBlastEngineForm>(form);
                form.TopLevel = false;
                EngineImplementation = new EZBlastEngineImplementation(form);

                S.GET<CorruptionEngineForm>().RegisterPluginEngine(EngineImplementation);

                S.GET<OpenToolsForm>().RegisterTool("EZ Blast Buttons", "Open EZ Blast Buttons", () => { 
                    LocalNetCoreRouter.Route(PluginRouting.Endpoints.RTC_SIDE, PluginRouting.Commands.SHOW_WINDOW, true); 
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

            if (CurrentSide == RTCSide.Server && !S.ISNULL<EZBlastEngineForm>() && !S.GET<EZBlastEngineForm>().IsDisposed)
            {
                S.GET<EZBlastEngineForm>().Close();
            }

            return true;
        }
    }
}
