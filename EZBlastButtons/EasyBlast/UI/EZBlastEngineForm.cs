using Ceras;
using EZBlastButtons.Structures;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.NetCore.NetCoreExtensions;
using RTCV.UI;
using RTCV.UI.Modular;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EZBlastButtons
{
    public partial class EZBlastEngineForm : ComponentForm, IColorize
    {

        public EZBlastEngineForm()
        {
            InitializeComponent();
        }



        public void OnEngineSelected()
        {
            
        }

        public void OnEngineDeselected()
        {

        }

        private void bOpenPlugin_Click(object sender, EventArgs e)
        {
            LocalNetCoreRouter.Route(PluginRouting.Endpoints.RTC_SIDE, PluginRouting.Commands.SHOW_WINDOW, true);
        }
    }


}
