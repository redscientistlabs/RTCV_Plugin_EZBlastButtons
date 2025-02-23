using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI.Modular;
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
    public partial class CreateSetForm : ComponentForm, IColorize
    {
        public string Value => tbValue.Text.Trim();
        public bool RestrictCore => cbUseCore.Checked;

        public CreateSetForm()
        {
            InitializeComponent();
            Shown += CreateSetForm_Shown;
        }

        private void CreateSetForm_Shown(object sender, EventArgs e)
        {
            lblCore.Text = (AllSpec.VanguardSpec[VSPEC.SYSTEM] as string);
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
