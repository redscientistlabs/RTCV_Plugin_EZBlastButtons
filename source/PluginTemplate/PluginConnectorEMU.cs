using EasyBlast.UI;
using NLog;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Windows.Forms;

namespace EasyBlast
{
    /// <summary>
    /// This lies on the Emulator(Client) side
    /// </summary>
    internal class PluginConnectorEMU : IRoutable
    {
        public PluginConnectorEMU()
        {
            LocalNetCoreRouter.registerEndpoint(this, Endpoint.EMU_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;

            switch (message.Type)
            {
                default:
                    break;
            }
            return e.returnMessage;
        }
    }
}
