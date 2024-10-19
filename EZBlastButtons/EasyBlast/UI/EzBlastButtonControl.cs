using EZBlastButtons.Structures;
using RTCV.NetCore;
using RTCV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EZBlastButtons.UI
{
    public partial class EzBlastButtonControl : UserControl
    {
        public MultiCorruptSettingsPack Pack { get; private set; }

        public event Action<EzBlastButtonControl> Deleted;
        public event Action<EzBlastButtonControl> Edit;
        public event Action<EzBlastButtonControl> Clicked;
        public EzBlastButtonControl()
        {
            InitializeComponent();
        }

        public EzBlastButtonControl(MultiCorruptSettingsPack pack, Color color)
        {
            InitializeComponent();
            Pack = pack;
            bRun.BackColor = color;
            bRun.Text = pack.Name;
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Edit", new EventHandler((o, e) =>
            {
                Edit?.Invoke(this);
            }));
            cm.MenuItems.Add("Delete", new EventHandler((o, e) =>
            {
                Deleted?.Invoke(this);
                Pack = null;
            }));
            //curSys.Buttons.Remove(EngineSettings);
            //gbButtons.Controls.Remove(b);
            //Save();

            ContextMenu = cm;
        }


        public void UpdatePack(MultiCorruptSettingsPack pack)
        {
            bRun.Text = pack.Name;
            Pack.Extract(pack);
        }


        private void bRun_Click(object sender, EventArgs e)
        {
            Clicked?.Invoke(this);
        }
    }
}
