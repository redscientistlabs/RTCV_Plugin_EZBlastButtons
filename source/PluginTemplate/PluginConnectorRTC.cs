using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBlast;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.Common;
using EasyBlast.UI;

namespace EasyBlast
{
    /// <summary>
    /// This lies on the RTC(Server) side
    /// </summary>
    class PluginConnectorRTC : IRoutable
    {
        public PluginConnectorRTC()
        {
            LocalNetCoreRouter.registerEndpoint(this, Endpoint.RTC_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;
            switch (message.Type)
            {
                case Commands.SHOW_WINDOW:
                    try
                    {
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            if (S.GET<PluginForm>() == null || S.GET<PluginForm>().IsDisposed)
                            {
                                S.SET<PluginForm>(new PluginForm());
                            }
                            var form = S.GET<PluginForm>();
                            form.Show();
                            form.Activate();
                        });
                        break;
                    }
                    catch
                    {
                        Logging.GlobalLogger.Error($"Template command {Commands.SHOW_WINDOW} failed. Reason:\r\n" + e.ToString());
                        break;
                    }
                default:
                    break;
            }
            return e.returnMessage;
        }
    }
}
