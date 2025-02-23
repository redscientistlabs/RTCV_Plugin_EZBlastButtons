using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.UI;
using EZBlastButtons.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RTCV.UI.Modular;
using System.Diagnostics;
using RTCV.NetCore;
using System.Xml.Linq;
using Ceras;
using RTCV.NetCore.NetCoreExtensions;
using System.Dynamic;

namespace EZBlastButtons.UI
{
    public partial class PluginForm : ComponentForm, IColorize
    {
        ButtonSet curButtonSet = null;
        EzBlastData AllSets = null;

        public static string PluginFolderPath = Path.Combine(RTCV.CorruptCore.RtcCore.PluginDir,nameof(EZBlastButtons));
        public static string PluginConfigPath = Path.Combine(PluginFolderPath, "EZBlastButtons.ezb");
        public static string CustomEngineTempPath = Path.Combine(PluginFolderPath, "TEMP_EZBLAST_CustomEngine.cet");
        public static string CustomEngineCachePath = Path.Combine(PluginFolderPath, "TEMP_Cached_CustomEngine.cet");

        public static PluginForm pForm;
        string prevDoms = "";
        //int ezBlastEngineIndex = 0;
        Button addButton= null;
        CerasSerializer saveSerializer = null;

        HashSet<string> validDomains = new HashSet<string>();
        CorruptionEngineForm settingsControl;
        ContextMenu addButtonContextMenu = null;
        public PluginForm()
        {
            InitializeComponent();
            saveSerializer = CreateSerializer();
            C.Regather();//Ensure our engine info is ok

            settingsControl = S.GET<CorruptionEngineForm>();
            settingsControl.cbSelectedEngine.SelectedIndex = 0;
            settingsControl.cbSelectedEngine.SelectedIndexChanged += CbSelectedEngine_SelectedIndexChanged;

            //cbSelectedEngine.Items.Add(new { Text = engine.ToString(), Value = engine });
            
            pForm = this;
            multiTB_Intensity.ValueChanged += (sender, args) => RTCV.CorruptCore.RtcCore.Intensity = multiTB_Intensity.Value;
            multiTB_Intensity.registerSlave(S.GET<GeneralParametersForm>().multiTB_Intensity);

            ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("Import...", new EventHandler((o, e) => {
            //    try
            //    {
            //        using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "*.json|*.json" })
            //        {
            //            if (ofd.ShowDialog() == DialogResult.OK)
            //            {
            //                Import(ofd.FileName);
            //            }
            //        }
            //    }
            //    catch(JsonSerializationException ex)
            //    {
            //        MessageBox.Show("ERROR: Import deserialization error", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}));

            cm.MenuItems.Add("Rename", new EventHandler((o, e) => {

                string newName = Prompt.ShowDialog("Name", "Enter New Set Name");
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    if (AllSets.Sets.ContainsKey(newName))
                    {
                        MessageBox.Show("Set name already exists", "Set name already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string key = cbSelectedSet.SelectedItem.ToString();
                    var set = AllSets.Sets[key];
                    AllSets.Sets.Remove(key);
                    AllSets.Sets[newName] = set;
                    Save();
                    UpdateSetsComboBox(newName, cbViewHiddenSets.Checked);
                }
            }));

            bNewSet.ContextMenu = cm;

            try
            {
                if (!Directory.Exists(PluginFolderPath))
                {
                    Directory.CreateDirectory(PluginFolderPath);
                }

                if (!File.Exists(PluginConfigPath))
                {
                    AllSets = new EzBlastData { Sets = new Dictionary<string, ButtonSet>() };
                    AllSets.Sets.Add("Default", new ButtonSet() { Buttons = new List<MultiCorruptSettingsPack>() });
                    Save();
                }
                else
                {
                    try
                    {
                        AllSets = saveSerializer.Deserialize<EzBlastData>(File.ReadAllBytes(PluginConfigPath));
                    }
                    catch (Exception ex)
                    {
                        var dlr = MessageBox.Show($"ERROR: Easy Manual Blast Buttons config deserialization error.{Environment.NewLine}Create a new button config? A copy of the old file will be saved.", 
                            "Ez Blast Buttons config load error.", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if(dlr == DialogResult.Yes)
                        {
                            File.Copy(PluginConfigPath, Path.Combine(Path.GetDirectoryName(PluginConfigPath),$"{Path.GetFileNameWithoutExtension(PluginConfigPath)}_{string.Format("{0:X}", DateTime.Now.ToFileTimeUtc())}.bkup"));
                            File.Delete(PluginConfigPath);
                            AllSets = new EzBlastData { Sets = new Dictionary<string, ButtonSet>() };
                            AllSets.Sets.Add("Default", new ButtonSet() { Buttons = new List<MultiCorruptSettingsPack>() });
                            Save();
                        }
                    }

                    Save();
                }
            }
            catch(JsonSerializationException ex)
            {
                MessageBox.Show("ERROR: Easy Manual Blast Buttons config deserialization error", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }

            UpdateSetsComboBox(null, cbViewHiddenSets.Checked);



            this.Load += PluginForm_Load;
            this.FormClosed += PluginForm_FormClosed;
            this.Shown += PluginForm_Shown;

            this.version.Text = $"{PluginCore.Ver.ToString()}"; //automatic window title
        }

        private void CbSelectedEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(addButton != null)
            {
                //TODO: warning for unsupported buttons
                if (C.IsEngineSupported(RtcCore.SelectedEngine))
                {
                    addButton.ContextMenu = addButtonContextMenu;
                }
                else
                {
                    addButton.ContextMenu = null;
                }
            }
        }

        void Import(string file)
        {
        }

        void ImportSilent(string file)
        {
        }

        private string[] GetValidDomains(params string[] doms)
        {
            return doms.Where(x => MemoryDomains.AllMemoryInterfaces.ContainsKey(x)).ToArray();
        }

        private void PluginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                S.GET<MemoryDomainsForm>().lbMemoryDomains.SelectedIndexChanged -= LbMemoryDomains_SelectedIndexChanged;
            }
        }

        private string DomHash(ListBox.ObjectCollection collection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in collection)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }

        private void LbMemoryDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            string h = DomHash(S.GET<MemoryDomainsForm>().lbMemoryDomains.Items);
            if (prevDoms != h)
            {
                prevDoms = h;
                Populate();
            }
        }

        private void UpdateSetsComboBox(string prevItem = null, bool showHidden = false)
        {
            updating = true;
            cbSelectedSet.SuspendLayout();
            int restrictCt = 0;
            //List<string> cores = new List<string>();
            cbSelectedSet.Items.Clear();
            var curCore = (AllSpec.VanguardSpec[VSPEC.SYSTEM] as string);
            //cores.Add(showHidden.ToString());

            foreach (var item in AllSets.Sets)
            {
                if (item.Value.RestrictCore)
                {
                    if (showHidden || item.Value.EmulatorCore == curCore)
                    {
                        cbSelectedSet.Items.Add(item.Key);
                        //cores.Add(item.Value.EmulatorCore);
                    }
                    else
                    {
                        restrictCt++;
                    }
                }
                else
                {
                    cbSelectedSet.Items.Add(item.Key);
                }
            }
            //lblCurCore.Text = $"Current Core: {curCore}, Restricted: {restrictCt}, Extra:{string.Join(", ",cores)}";

            cbSelectedSet.ResumeLayout();
            updating = false;
            if (cbSelectedSet.Items.Count > 0)
            {
                if (prevItem == null)
                {
                    cbSelectedSet.SelectedIndex = 0;
                    curButtonSet = AllSets.Sets[cbSelectedSet.SelectedItem.ToString()];
                }
                else
                {
                    cbSelectedSet.SelectedIndex = cbSelectedSet.Items.IndexOf(prevItem);
                }
            }
            else
            {
                throw new Exception("Easy Manual Blast Buttons config file is empty");
            }           
        }

        private void PluginForm_Shown(object sender, EventArgs e)
        {
            object paramValue = RTCV.NetCore.AllSpec.VanguardSpec[VSPEC.OVERRIDE_DEFAULTMAXINTENSITY];

            if (paramValue != null && paramValue is int maxintensity)
            {
                multiTB_Intensity.Maximum = maxintensity;
            }
            if(S.GET<SavestateManagerForm>().CurrentSaveStateStashKey == null)
            {
                cbGH.Checked = false;
            }

        }

        private void PluginForm_Load(object sender, EventArgs e)
        {
            prevDoms = DomHash(S.GET<MemoryDomainsForm>().lbMemoryDomains.Items);
            S.GET<MemoryDomainsForm>().lbMemoryDomains.SelectedIndexChanged += LbMemoryDomains_SelectedIndexChanged;
            Populate();
        }


        private bool IsEngineSettingValid(EngineSettings EngineSettings)
        {
            return true;
            //TODO: validation for new system
        }

        private void AddButton(MultiCorruptSettingsPack engineSettings)
        {
            bool isValid = engineSettings.Settings.Count > 0;
            
            for (int i = 0; i < engineSettings.Settings.Count; i++)
            {
                if (!IsEngineSettingValid(engineSettings.Settings[i]))
                {
                    isValid = false;
                    break;
                }
            }

            bool hiddenAllowed = cbViewHiddenButtons.Checked;

            if (isValid || hiddenAllowed)
            {
                EzBlastButtonControl b = new EzBlastButtonControl(engineSettings, isValid ? pForm.cbSelectedSet.BackColor : Color.DarkRed);
                b.Clicked += EzBlastButton_Click;
                b.Deleted += EzBlastButton_Deleted;
                b.Edit += EzBlastButton_Edit;
                gbButtons.Controls.Add(b);
               
            }
        }

        private void EzBlastButton_Edit(EzBlastButtonControl ebb)
        {
            EZBlastButtonConfigForm buttonConfigForm = new EZBlastButtonConfigForm(ebb.Pack.Duplicate());

            if (buttonConfigForm.ShowDialog() == DialogResult.OK)
            {
                gbButtons.Controls.Remove(addButton);

                ebb.UpdatePack(buttonConfigForm.Pack);
                Save();
                RestoreAddButton();
            }
        }

        private void EzBlastButton_Deleted(EzBlastButtonControl ebb)
        {
            curButtonSet.Buttons.Remove(ebb.Pack);
            gbButtons.Controls.Remove(ebb);
            Save();
        }


        float GetMultiplier()
        {
            if (rbSizeLarge.Checked) return 2.0f;
            else if (rbSizeSmall.Checked) return 0.5f;
            else return 1.0f;
        }

        private void EzBlastButton_Click(EzBlastButtonControl ebb)
        {

            try
            {
                ebb.Enabled = false;
                var cbE = S.GET<CorruptionEngineForm>().cbSelectedEngine;
                //cbE.SelectedIndex = 0;
                cbE.SelectedIndex = C.EZBlastEngineIndex;

                LocalNetCoreRouter.Route(PluginRouting.Endpoints.EMU_SIDE, PluginRouting.Commands.UPDATE_SHARED_SETTINGS, new EZBlastSharedSettings(GetMultiplier(),-1), true);
                LocalNetCoreRouter.Route(PluginRouting.Endpoints.EMU_SIDE, PluginRouting.Commands.UPDATE_SETTINGS, ebb.Pack, true);
                if (this.cbGH.Checked)
                {
                    S.GET<GlitchHarvesterBlastForm>().Corrupt(null, null);
                }
                else
                {
                    LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.CorruptCore, RTCV.NetCore.Commands.Basic.ManualBlast, true);
                }

            }
            finally
            {
                ebb.Enabled = true;
            }

           
        }

        private void Populate()
        {
            if (curButtonSet != null)
            {
                gbButtons.Controls.Clear();
                if (curButtonSet.Buttons.Count > 0)
                {
                    foreach (var item in curButtonSet.Buttons)
                    {
                        AddButton(item);
                    }
                }
                CreateAddButton();
            }
        }

        private void CreateAddButton()
        {
            Button b = new Button()
            {
                Name = $"Add Button",
                Text = "Add Button..",
                Height = 50,
                Width = 150,
                BackColor = pForm.cbSelectedSet.BackColor,
                ForeColor = Color.Lime,
                Font = new Font("Segoe UI", 8),
                UseVisualStyleBackColor = false,
                FlatStyle = FlatStyle.Flat,
                Tag = "color:light1",
                Cursor = Cursors.Hand
            };

            addButtonContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Quick Add (Dynamic Intensity)", QuickAddPercentage),
                new MenuItem("Quick Add (Keep Current Intensity)", QuickAddForced),
                new MenuItem("Add Advanced..", CreateNewConfig),
            });
            b.ContextMenu = addButtonContextMenu;

            addButton = b;
            b.Click += (o,e) => {
                Button btnSender = (Button)o;
                Point ptLowerLeft = new Point(0, btnSender.Height);
                addButtonContextMenu.Show(btnSender,ptLowerLeft);
            };
            gbButtons.Controls.Add(b);
        }

        private void QuickAddForced(object sender, EventArgs e)
        {
            EngineSettings quickSetting;

            switch (RtcCore.SelectedEngine)
            {
                case CorruptionEngine.NIGHTMARE:
                    quickSetting = new EngineSettings(NightmareEngine.getDefaultPartial());
                    break;
                //case CorruptionEngine.HELLGENIE:
                //    quickSetting = new EngineSettings(HellgenieEngine.getDefaultPartial());
                //    break;
                case CorruptionEngine.DISTORTION:
                    quickSetting = new EngineSettings(DistortionEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.FREEZE:
                    quickSetting = new EngineSettings(null);
                    break;
                case CorruptionEngine.PIPE:
                    quickSetting = new EngineSettings(null);
                    break;
                case CorruptionEngine.VECTOR:
                    quickSetting = new EngineSettings(VectorEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.CLUSTER:
                    quickSetting = new EngineSettings(ClusterEngine.getDefaultPartial());
                    break;
                default:
                    MessageBox.Show("Current engine type is not supported for EzBlast Buttons", "Unable To Add Button", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
            }

            var buttonName = Prompt.ShowDialog("Choose Button Name", "EZ Button Name");
            

            if (string.IsNullOrWhiteSpace(buttonName))
            {
                return;
            }

            quickSetting.DisplayName = C.EngineString(RtcCore.SelectedEngine) + " (Quick Add)";
            quickSetting.Extract(settingsControl);
            string[] domainList = new List<string>(S.GET<MemoryDomainsForm>().lbMemoryDomains.SelectedItems.Cast<string>()).ToArray();
            quickSetting.Domains = domainList;

            quickSetting.ForcedIntensity = RtcCore.Intensity;
            MultiCorruptSettingsPack pack = new MultiCorruptSettingsPack();
            pack.AddSetting(quickSetting);
            pack.Name = buttonName;

            curButtonSet.Buttons.Add(pack);
            AddButton(pack);
            Save();
            RestoreAddButton();
        }


        private void QuickAddPercentage(object sender, EventArgs e)
        {
            EngineSettings quickSetting;

            switch (RtcCore.SelectedEngine)
            {
                case CorruptionEngine.NIGHTMARE:
                    quickSetting = new EngineSettings(NightmareEngine.getDefaultPartial());
                    break;
                //case CorruptionEngine.HELLGENIE:
                //    quickSetting = new EngineSettings(HellgenieEngine.getDefaultPartial());
                //    break;
                case CorruptionEngine.DISTORTION:
                    quickSetting = new EngineSettings(DistortionEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.FREEZE:
                    quickSetting = new EngineSettings(null);
                    break;
                case CorruptionEngine.PIPE:
                    quickSetting = new EngineSettings(null);
                    break;
                case CorruptionEngine.VECTOR:
                    quickSetting = new EngineSettings(VectorEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.CLUSTER:
                    quickSetting = new EngineSettings(ClusterEngine.getDefaultPartial());
                    break;
                default:
                    MessageBox.Show("Current engine type is not supported for EzBlast Buttons", "Unable To Add Button", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            var buttonName = Prompt.ShowDialog("Choose Button Name", "EZ Button Name");


            if (string.IsNullOrWhiteSpace(buttonName))
            {
                return;
            }

            quickSetting.DisplayName = C.EngineString(RtcCore.SelectedEngine) + " (Quick Add)";
            quickSetting.Extract(settingsControl);
            string[] domainList = new List<string>(S.GET<MemoryDomainsForm>().lbMemoryDomains.SelectedItems.Cast<string>()).ToArray();
            quickSetting.Domains = domainList;

            quickSetting.Percentage = 1.0;
            MultiCorruptSettingsPack pack = new MultiCorruptSettingsPack();
            pack.AddSetting(quickSetting);
            pack.Name = buttonName;

            curButtonSet.Buttons.Add(pack);
            AddButton(pack);
            Save();
            RestoreAddButton();
        }

        private void RestoreAddButton()
        {
            if(addButton == null)
            {
                CreateAddButton();
            }
            else
            {
                gbButtons.Controls.Add(addButton);
                addButton.ContextMenu = addButtonContextMenu;

            }
            //addButton = b;
        }


        private void CreateNewConfig(object sender, EventArgs e)
        {
            EZBlastButtonConfigForm buttonConfigForm = new EZBlastButtonConfigForm();

            if(buttonConfigForm.ShowDialog() == DialogResult.OK)
            {
                //((Button)sender).Click -= CreateNewConfig;
                gbButtons.Controls.Remove(addButton);// (Control)sender);
                curButtonSet.Buttons.Add(buttonConfigForm.Pack);
                AddButton(buttonConfigForm.Pack);
                Save();
                RestoreAddButton();
            }
        }

        ////Copied from TCPLink.cs line 235
        private static CerasSerializer CreateSerializer()
        {
            var config = new SerializerConfig();
            config.Advanced.PersistTypeCache = false;
            config.Advanced.UseReinterpretFormatter = false; //While faster, leads to some weird bugs due to threading abuse
            config.Advanced.RespectNonSerializedAttribute = false;
            config.OnResolveFormatter.Add((c, t) =>
            {
                if (t == typeof(HashSet<byte[]>))
                {
                    return new HashSetFormatterThatKeepsItsComparer();
                }
                else if (t == typeof(HashSet<byte?[]>))
                {
                    return new NullableByteHashSetFormatterThatKeepsItsComparer();
                }

                return null; // continue searching
            });
            return new CerasSerializer(config);
        }

        private void Save()
        {
            try
            {
                var bytes = saveSerializer.Serialize(AllSets);
                File.WriteAllBytes(PluginConfigPath, bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save EzBlast Engine file: " + ex.Message);
            }
        }

        private bool updating = false;
        private void cbSelectedEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                if (AllSets.Sets.ContainsKey(cbSelectedSet.SelectedItem.ToString()))
                {
                    curButtonSet = AllSets.Sets[cbSelectedSet.SelectedItem.ToString()];
                    lblCoreRestrict.Text = $"Restrict: {curButtonSet.RestrictCore}, Core: {curButtonSet.EmulatorCore}";
                    Populate();
                }
            }
        }

        private void cbManualIntensity_CheckedChanged(object sender, EventArgs e)
        {
            if (cbManualIntensity.Checked)
            {
                pRadioButtons.Enabled = false;
                multiTB_Intensity.Enabled = true;
            }
            else
            {
                pRadioButtons.Enabled = true;
                multiTB_Intensity.Enabled = false;
            }
        }

        private void bNewSet_Click(object sender, EventArgs e)
        {
            var csf = new CreateSetForm();
            if(csf.ShowDialog() == DialogResult.OK)
            {
                string s = csf.Value;// Prompt.ShowDialog("Name", "Enter Set Name");
                if (!string.IsNullOrWhiteSpace(s))
                {
                    if (AllSets.Sets.ContainsKey(s))
                    {
                        MessageBox.Show("Set already exists", "Set already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var set = new ButtonSet() { Buttons = new List<MultiCorruptSettingsPack>() };
                    if (csf.RestrictCore) {
                        set.AssignCore(AllSpec.VanguardSpec[VSPEC.SYSTEM] as string);
                    }
                    AllSets.Sets.Add(s, set);
                    Save();
                    updating = true;
                    cbSelectedSet.Items.Clear();
                    foreach (var item in AllSets.Sets)
                    {
                        cbSelectedSet.Items.Add(item.Key);
                    }
                    updating = false;
                    cbSelectedSet.SelectedIndex = cbSelectedSet.Items.IndexOf(s);
                }
            }
        }

        //https://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = 400 };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "OK", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        private void bDeleteSet_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Permanently delete set {cbSelectedSet.SelectedItem.ToString()}?", "Delete Set", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                string key = cbSelectedSet.SelectedItem.ToString();
                var set = AllSets.Sets[key];
                AllSets.Sets.Remove(key);
                if (AllSets.Sets.Count == 0)
                {
                    AllSets.Sets.Add("Default", new ButtonSet() { Buttons = new List<MultiCorruptSettingsPack>() });
                }
                Save();
                UpdateSetsComboBox(null, cbViewHiddenSets.Checked);
                Populate();
            }
        }

        private void cbViewHidden_CheckedChanged(object sender, EventArgs e)
        {
            Populate();
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            foreach (var button in gbButtons.Controls)
            {
                if(button is EzBlastButtonControl b)
                {
                    b.ValidateEngines();
                }
            }

            string key = cbSelectedSet.SelectedItem?.ToString();
            UpdateSetsComboBox(key, cbViewHiddenSets.Checked);
        }

        private void cbViewHiddenSets_CheckedChanged(object sender, EventArgs e)
        {
            string key = cbSelectedSet.SelectedItem?.ToString();
            UpdateSetsComboBox(key, cbViewHiddenSets.Checked);
        }
    }
}
