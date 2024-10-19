using Ceras;
using RTCV.Common;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EZBlastButtons
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class EZBlastEngineImplementation : ICorruptionEngine
    {
        public Form Control => throw new NotImplementedException();

        bool ICorruptionEngine.SupportsCustomPrecision => true;
        bool ICorruptionEngine.SupportsAutoCorrupt => true;
        bool ICorruptionEngine.SupportsGeneralParameters => true;
        bool ICorruptionEngine.SupportsMemoryDomains => true;

        [Exclude]
        Form ICorruptionEngine.Control { get { return engineForm; } }
        [Exclude]
        EZBlastEngineForm engineForm { get; set; } = null;

        public EZBlastEngineImplementation() //empty constructor required by ceras
        {

        }

        public EZBlastEngineImplementation(EZBlastEngineForm _control)
        {
            engineForm = _control;
        }

        public BlastLayer GetBlastLayer(long intensity)
        {
            try
            {
                //Cache spec
                C.CacheMasterSpec();

                var domains = RTCV.NetCore.AllSpec.UISpec["SELECTEDDOMAINS"] as string[];
                if (domains == null || domains.Length == 0)
                {
                    MessageBox.Show("Can't corrupt with no domains selected.");
                    return null;
                }

                //EZBlastButtonsEngineCore.OverrideIntensity = intensity;
                return EZBlastButtonsEngineCore.Corrupt();
            }
            catch
            {
                throw;
            }
            finally
            {
                //Revert entire spec on this side, no update
                C.RestoreMasterSpec(false);
            }
        }

        string ICorruptionEngine.ToString() => engineForm?.ToString();

        void ICorruptionEngine.OnSelect()
        {
            S.GET<EZBlastEngineForm>().OnEngineSelected();
        }

        void ICorruptionEngine.OnDeselect()
        {
            S.GET<EZBlastEngineForm>().OnEngineDeselected();
        }
    }
}
