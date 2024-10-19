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

        //public static EngineSettings Settings
        //{
        //    get
        //    {
        //        if (_pack == null)
        //            Settings = new EngineSettings();

        //        return _pack;
        //    }
        //    set { _pack = value; }
        //}
        //private static EngineSettings _pack = null;

        ////private static bool corrupting = false;

        //private static CerasSerializer saveSerializer;


        //private CorruptionEngineForm originalEngineForm = null;

        public EZBlastEngineForm()
        {
            InitializeComponent();
            //saveSerializer = CreateSerializer();

            //FormClosing += MultiEngineForm_FormClosing;
        }



        //public void PushSettings()
        //{
        //    LocalNetCoreRouter.Route(PluginRouting.Endpoints.EMU_SIDE, PluginRouting.Commands.UPDATE_SETTINGS, Settings, true);
        //}

        //private void MultiEngineForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    corrupting = false;
        //}



        //////Copied from TCPLink.cs line 235
        //private static CerasSerializer CreateSerializer()
        //{
        //    var config = new SerializerConfig();
        //    config.Advanced.PersistTypeCache = false;
        //    config.Advanced.UseReinterpretFormatter = false; //While faster, leads to some weird bugs due to threading abuse
        //    config.Advanced.RespectNonSerializedAttribute = false;
        //    config.OnResolveFormatter.Add((c, t) =>
        //    {
        //        if (t == typeof(HashSet<byte[]>))
        //        {
        //            return new HashSetFormatterThatKeepsItsComparer();
        //        }
        //        else if (t == typeof(HashSet<byte?[]>))
        //        {
        //            return new NullableByteHashSetFormatterThatKeepsItsComparer();
        //        }

        //        return null; // continue searching
        //    });
        //    return new CerasSerializer(config);
        //}


        public void OnEngineSelected()
        {
            
        }

        public void OnEngineDeselected()
        {

        }

    }


}
