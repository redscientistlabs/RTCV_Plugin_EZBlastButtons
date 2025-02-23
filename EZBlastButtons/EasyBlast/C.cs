using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBlastButtons
{
    internal static class C
    {
        public const string ClusterEngineStr = "Cluster Engine";
        public const string DistortionEngineStr = "Distortion Engine";
        public const string FreezeEngineStr = "Freeze Engine";
        public const string HellgenieEngineStr = "Hellgenie Engine";
        public const string NightmareEngineStr = "Nightmare Engine";
        public const string PipeEngineStr = "Pipe Engine";
        public const string VectorEngineStr = "Vector Engine";
        public const string CustomEngineStr = "Custom Engine";

        public const string EZBlastEngineStr = "EZBlast Engine";

        public const string TARGET_SPEC_NAME = "RTCSpec";

        private static PartialSpec masterSpec = null;

        private static HashSet<int> supportedEngineIndices = new HashSet<int>();
        static bool initialized = false;

        static List<int> indices = new List<int>();
        static List<string> engineComboboxIndicesList = new List<string>();

        static List<EngineSupportInfo> EngineSupportInfos = new List<EngineSupportInfo>();


        public static int EZBlastEngineIndex = -1;

        public static CorruptionEngine EngineFromName(string engineName)
        {

            if (string.IsNullOrWhiteSpace(engineName)) return CorruptionEngine.NONE;

            foreach (var esi in EngineSupportInfos)
            {
                if (esi.EngineName == engineName)
                {
                    return esi.CorruptionEngineType;
                }
            }
            return CorruptionEngine.NONE;
        }

        public static int EngineToIndex(CorruptionEngine engine)
        {

            foreach (var esi in EngineSupportInfos)
            {
                if (esi.CorruptionEngineType == engine)
                {
                    return esi.ComboBoxIndex;
                }
            }

            return -1;
        }

        public static bool IsEngineSupported(CorruptionEngine engine)
        {
            foreach (var esi in EngineSupportInfos)
            {
                if(esi.CorruptionEngineType == engine)
                {
                    return esi.Supported;
                }
            }
            return false;
        }

        public static CorruptionEngine IndexToEngine(int index)
        {
            foreach (var esi in EngineSupportInfos)
            {
                if(esi.ComboBoxIndex == index)
                {
                    return esi.CorruptionEngineType;
                }
            }
            return CorruptionEngine.NONE;
        }


        


        internal static void Regather()
        {

            InitMasterSpec();
            supportedEngineIndices.Clear();

           
            int index = 0;
            engineComboboxIndicesList = S.GET<CorruptionEngineForm>().cbSelectedEngine.Items.Cast<object>().Where(x => {
                bool isStr = x is string;
                if (isStr) indices.Add(index);
                index = index + 1;
                return isStr;
            }).Cast<string>().ToList();

            for (int i = 0; i < EngineSupportInfos.Count; i++)
            {
                EngineSupportInfos[i].ComboBoxIndex = engineComboboxIndicesList.FindIndex(x => x == EngineSupportInfos[i].EngineName);
            }

            var allItems = S.GET<CorruptionEngineForm>().cbSelectedEngine.Items.Cast<object>().ToList();
            EZBlastEngineIndex = allItems.FindIndex(x => x.ToString().Contains(EZBlastEngineStr));

        }

        public static void Init()
        {
            EngineSupportInfos = new List<EngineSupportInfo>
            {
                new EngineSupportInfo(VectorEngineStr, VectorEngine.getDefaultPartial, CorruptionEngine.VECTOR),
                new EngineSupportInfo(NightmareEngineStr, NightmareEngine.getDefaultPartial, CorruptionEngine.NIGHTMARE),
                //new EngineSupportInfo(HellgenieEngineStr, HellgenieEngine.getDefaultPartial, CorruptionEngine.HELLGENIE),
                new EngineSupportInfo(FreezeEngineStr, null, CorruptionEngine.FREEZE),
                new EngineSupportInfo(PipeEngineStr, null, CorruptionEngine.PIPE),
                new EngineSupportInfo(DistortionEngineStr, DistortionEngine.getDefaultPartial, CorruptionEngine.DISTORTION),
                new EngineSupportInfo(ClusterEngineStr, ClusterEngine.getDefaultPartial, CorruptionEngine.CLUSTER),
                new EngineSupportInfo(CustomEngineStr, CustomEngine.getCurrentConfigSpec, CorruptionEngine.CUSTOM)
            };

            InitMasterSpec();
            Regather();
        }

        static void InitMasterSpec()
        {
            masterSpec = new PartialSpec(C.TARGET_SPEC_NAME);
            //masterSpec[RTCSPEC.CORE_INTENSITY] = RtcCore.Intensity;
            masterSpec[RTCSPEC.CORE_CURRENTPRECISION] = RtcCore.CurrentPrecision;
            masterSpec[RTCSPEC.CORE_CURRENTALIGNMENT] = RtcCore.Alignment;
            masterSpec[RTCSPEC.CORE_USEALIGNMENT] = RtcCore.UseAlignment;
            masterSpec[RTCSPEC.CORE_CREATEINFINITEUNITS] = RtcCore.CreateInfiniteUnits;

            var ct = supportedEngineIndices.Count;
            for (int i = 0; i < ct; i++)
            {
                if (EngineSupportInfos[i].DefaultPartialFunc != null)
                {
                    masterSpec.Insert(EngineSupportInfos[i].DefaultPartialFunc.Invoke());
                }
            }

            initialized = true;
        }

        internal static string EngineString(CorruptionEngine engine)
        {
            switch (engine)
            {
                case CorruptionEngine.NIGHTMARE:
                    return NightmareEngineStr;
                //case CorruptionEngine.HELLGENIE:
                //    return HellgenieEngineStr;
                case CorruptionEngine.DISTORTION:
                    return DistortionEngineStr;
                case CorruptionEngine.FREEZE:
                    return FreezeEngineStr;
                case CorruptionEngine.PIPE:
                    return PipeEngineStr;
                case CorruptionEngine.VECTOR:
                    return VectorEngineStr;
                case CorruptionEngine.CLUSTER:
                    return ClusterEngineStr;
                case CorruptionEngine.CUSTOM:
                    return CustomEngineStr;
                default:
                    return "UNSUPPORTED";
            }
        }

        public static void CacheMasterSpec()
        {
            if (!initialized) InitMasterSpec();
            masterSpec.ExtractFrom(AllSpec.CorruptCoreSpec);
        }

        public static void RestoreMasterSpec(bool push = false)
        {
            if (!initialized) InitMasterSpec();
            AllSpec.CorruptCoreSpec.Update(masterSpec, push, push);
        }
        //???
        public static int PrecisionToIndex(int precision)
        {

            switch (precision)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 4:
                    return 2;
                case 8:
                    return 3;
                default:
                    return 0;
            }
        }
        public static int IndexToPrecision(int index)
        {

            switch (index)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 8;
                default:
                    return 1;
            }
        }


        public static void ExtractFrom(this PartialSpec to, PartialSpec from)
        {
            var keys = to.GetKeys();
            foreach (var key in keys)
            {
                to[key] = from[key];
            }
        }

        public static void ExtractFrom(this PartialSpec to, FullSpec from)
        {
            var keys = to.GetKeys();
            foreach (var key in keys)
            {
                to[key] = from[key];
            }
        }

    }

}
