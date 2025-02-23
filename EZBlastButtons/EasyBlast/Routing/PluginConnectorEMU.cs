using EZBlastButtons.Structures;
using EZBlastButtons.UI;
using NLog;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Windows.Forms;

namespace EZBlastButtons
{
    /// <summary>
    /// This lies on the Emulator(Client) side
    /// </summary>
    internal class PluginConnectorEMU : IRoutable
    {
        public PluginConnectorEMU()
        {
            LocalNetCoreRouter.registerEndpoint(this, PluginRouting.Endpoints.EMU_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;

            switch (message.Type)
            {
                case PluginRouting.Commands.UPDATE_SETTINGS:
                    EZBlastButtonsEngineCore.SetSettings(message.objectValue as MultiCorruptSettingsPack);
                    break;
                case PluginRouting.Commands.UPDATE_SHARED_SETTINGS:
                    EZBlastButtonsEngineCore.SetSharedSettings((EZBlastSharedSettings)message.objectValue);
                    break;
                default:
                    break;
            }
            return e.returnMessage;
        }
    }
}
