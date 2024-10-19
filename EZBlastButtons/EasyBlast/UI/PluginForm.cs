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
        SystemDef curSys = null;
        SysDefHolder AllSets = null;

        public static string PluginFolderPath = Path.Combine(RTCV.CorruptCore.RtcCore.PluginDir,nameof(EZBlastButtons));
        public static string PluginConfigPath = Path.Combine(PluginFolderPath, "EZBlastButtons.json");

        public static PluginForm pForm;
        string prevDoms = "";
        //int ezBlastEngineIndex = 0;
        Button addButton= null;
        CerasSerializer saveSerializer = null;

        HashSet<string> validDomains = new HashSet<string>();
        CorruptionEngineForm settingsControl;
        public PluginForm()
        {
            InitializeComponent();
            saveSerializer = CreateSerializer();

            settingsControl = S.GET<CorruptionEngineForm>();
            settingsControl.cbSelectedEngine.SelectedIndex = C.NightmareEngineIndex;
            settingsControl.cbSelectedEngine.SelectedIndexChanged += CbSelectedEngine_SelectedIndexChanged;

            //cbSelectedEngine.Items.Add(new { Text = engine.ToString(), Value = engine });
            
            pForm = this;
            //Bind up multitb
            multiTB_Intensity.ValueChanged += (sender, args) => RTCV.CorruptCore.RtcCore.Intensity = multiTB_Intensity.Value;
            multiTB_Intensity.registerSlave(S.GET<GeneralParametersForm>().multiTB_Intensity);

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Import...", new EventHandler((o, e) => {
                try
                {
                    using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "*.json|*.json" })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            Import(ofd.FileName);
                        }
                    }
                    
                }
                catch(JsonSerializationException ex)
                {
                    MessageBox.Show("ERROR: Import deserialization error", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));

            cm.MenuItems.Add("Rename", new EventHandler((o, e) => {

                string newName = Prompt.ShowDialog("Name", "Enter New Set Name");
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    if (AllSets.Systems.ContainsKey(newName))
                    {
                        MessageBox.Show("Set name already exists", "Set name already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string key = cbSelectedEngine.SelectedItem.ToString();
                    var set = AllSets.Systems[key];
                    AllSets.Systems.Remove(key);
                    AllSets.Systems[newName] = set;
                    Save();
                    UpdateSetsComboBox(newName);
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
                    AllSets = new SysDefHolder { Systems = new Dictionary<string, SystemDef>() };
                    AllSets.Systems.Add("Default", new SystemDef() { Buttons = new List<MultiCorruptSettingsPack>() });
                    Save();
                }
                else
                {

                    AllSets = saveSerializer.Deserialize<SysDefHolder>(File.ReadAllBytes(PluginConfigPath));
                    //AllSets = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(PluginConfigPath));
                    var extraFiles = Directory.GetFiles(PluginFolderPath).Where(x => x != PluginConfigPath && Path.GetExtension(x)?.ToLower() == ".json");
                    foreach(var f in extraFiles)
                    {
                        try
                        {
                            ImportSilent(f);
                            File.Delete(f);
                        }
                        catch
                        {
                            MessageBox.Show($"File {f} is not a valid ez blast file", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            UpdateSetsComboBox();



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
                addButton.Enabled = C.IsEngineSupported(cbSelectedEngine.SelectedIndex);
            }
        }

        void Import(string file)
        {

            //List<string> duplicates = new List<string>();
            //bool hasDuplicates = false;
            //var import = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(file));
            //foreach (var item in import.Systems)
            //{
            //    if (!AllSets.Systems.ContainsKey(item.Key))
            //    {
            //        AllSets.Systems.Add(item.Key, item.Value);
            //    }
            //    else
            //    {
            //        hasDuplicates = true;
            //        duplicates.Add(item.Key);
            //    }
            //}
            //Save();
            //UpdateSetsComboBox(cbSelectedEngine.SelectedItem.ToString());
            //Populate();

            //if (hasDuplicates)
            //{
            //    MessageBox.Show($"The following sets were duplicates and not imported:{string.Join(", ", duplicates)}", "Duplicate Sets", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        void ImportSilent(string file)
        {
            //return;
            //var import = saveSerializer.Deserialize<SysDefHolder>(File.ReadAllBytes(file));
            ////var import = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(file));
            //foreach (var item in import.Systems)
            //{
            //    if (!AllSets.Systems.ContainsKey(item.Key))
            //    {
            //        AllSets.Systems.Add(item.Key, item.Value);
            //    }
            //}
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

        private void UpdateSetsComboBox(string prevItem = null)
        {
            updating = true;
            cbSelectedEngine.Items.Clear();
            foreach (var item in AllSets.Systems)
            {
                cbSelectedEngine.Items.Add(item.Key);
            }
            updating = false;
            if (cbSelectedEngine.Items.Count > 0)
            {
                if (prevItem == null)
                {
                    cbSelectedEngine.SelectedIndex = 0;//ind > -1 ? ind : 0;
                    curSys = AllSets.Systems[cbSelectedEngine.SelectedItem.ToString()];
                }
                else
                {
                    cbSelectedEngine.SelectedIndex = cbSelectedEngine.Items.IndexOf(prevItem);
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
        }

        private void PluginForm_Load(object sender, EventArgs e)
        {
            prevDoms = DomHash(S.GET<MemoryDomainsForm>().lbMemoryDomains.Items);//.GetHashCode();
            S.GET<MemoryDomainsForm>().lbMemoryDomains.SelectedIndexChanged += LbMemoryDomains_SelectedIndexChanged;
            Populate();
        }


        private bool IsEngineSettingValid(EngineSettings EngineSettings)
        {
            return true;
            //TODO: validation for new system


            //var hashNameDic = Filtering.Hash2NameDico; //cache from allspec



            //if (EngineSettings.EngineType == CorruptionEngine.VECTOR)
            //    {
            //        EngineSettings.CachedSpec[RTCSPEC.VECTOR_LIMITERLISTHASH]

            //}

            //if (hashNameDic.Values.Contains(EngineSettings.Limiter) && hashNameDic.Values.Contains(EngineSettings.Value))
            //{
            //    var doms = S.GET<MemoryDomainsForm>().lbMemoryDomains.Items;
            //    bool hasValidDomain = false;
            //    foreach (var dom in EngineSettings.Domains)
            //    {
            //        if (doms.Contains(dom))
            //        {
            //            hasValidDomain = true;
            //            break;
            //        }
            //    }
            //    return hasValidDomain;
            //}
            //else
            //{
            //    return false;
            //}
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

            bool hiddenAllowed = cbViewHidden.Checked;

            if (isValid || hiddenAllowed)
            {
                EzBlastButtonControl b = new EzBlastButtonControl(engineSettings, isValid ? pForm.cbSelectedEngine.BackColor : Color.DarkRed);
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
            curSys.Buttons.Remove(ebb.Pack);
            gbButtons.Controls.Remove(ebb);
            Save();
        }

        private void EzBlastButton_Click(EzBlastButtonControl ebb)
        {

            try
            {
                ebb.Enabled = false;

                var cbE = S.GET<CorruptionEngineForm>().cbSelectedEngine;
                cbE.SelectedIndex = C.NightmareEngineIndex;
                cbE.SelectedIndex = PluginCore.EngineIndex;
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
            if (curSys != null)
            {
                gbButtons.Controls.Clear();
                if (curSys.Buttons.Count > 0)
                {
                    foreach (var item in curSys.Buttons)
                    {
                        AddButton(item);//, item.Name, item.Limiter, item.Value, item.Intensity, item.Domains);
                    }
                }
                AddAddButton();
            }
        }

        private void AddAddButton()
        {
            Button b = new Button()
            {
                Name = $"Add Button",
                Text = "Add Button..",
                Height = 50,
                Width = 150,
                BackColor = pForm.cbSelectedEngine.BackColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                UseVisualStyleBackColor = false,
                FlatStyle = FlatStyle.Flat,
                Tag = "color:light1"
            };

            ContextMenu c = new ContextMenu(new MenuItem[] { new MenuItem("Quick Add\r\n(Current Engine Settings)", QuickAdd) });
            b.ContextMenu = c;


            b.Click += CreateNewConfig;
            gbButtons.Controls.Add(b);
            addButton = b;
        }

        private void QuickAdd(object sender, EventArgs e)
        {
            EngineSettings quickSetting;

            switch (RtcCore.SelectedEngine)
            {
                case CorruptionEngine.NIGHTMARE:
                    quickSetting = new EngineSettings(NightmareEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.HELLGENIE:
                    quickSetting = new EngineSettings(HellgenieEngine.getDefaultPartial());
                    break;
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
                    MessageBox.Show("Cannot add unsupported engine type");
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

            curSys.Buttons.Add(pack);
            AddButton(pack);
            Save();
            RestoreAddButton();
        }

        private void RestoreAddButton()
        {
            if(addButton == null)
            {
                AddAddButton();
            }
            else
            {
                gbButtons.Controls.Add(addButton);

            }
            //addButton = b;
        }


        private void CreateNewConfig(object sender, EventArgs e)
        {
            EZBlastButtonConfigForm buttonConfigForm = new EZBlastButtonConfigForm();

            if(buttonConfigForm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Click -= CreateNewConfig;
                gbButtons.Controls.Remove((Control)sender);
                curSys.Buttons.Add(buttonConfigForm.Pack);
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
                if (AllSets.Systems.ContainsKey(cbSelectedEngine.SelectedItem.ToString()))
                {
                    curSys = AllSets.Systems[cbSelectedEngine.SelectedItem.ToString()];
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
            string s = Prompt.ShowDialog("Name", "Enter Set Name");
            if (!string.IsNullOrWhiteSpace(s))
            {
                if (AllSets.Systems.ContainsKey(s))
                {
                    MessageBox.Show("Set already exists", "Set already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AllSets.Systems.Add(s, new SystemDef() { Buttons = new List<MultiCorruptSettingsPack>() });
                Save();
                updating = true;
                cbSelectedEngine.Items.Clear();
                foreach (var item in AllSets.Systems)
                {
                    cbSelectedEngine.Items.Add(item.Key);
                }
                updating = false;
                cbSelectedEngine.SelectedIndex = cbSelectedEngine.Items.IndexOf(s);
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
            if (MessageBox.Show($"Permanently delete set {cbSelectedEngine.SelectedItem.ToString()}?", "Delete Set", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                string key = cbSelectedEngine.SelectedItem.ToString();
                var set = AllSets.Systems[key];
                AllSets.Systems.Remove(key);
                if (AllSets.Systems.Count == 0)
                {
                    AllSets.Systems.Add("Default", new SystemDef() { Buttons = new List<MultiCorruptSettingsPack>() });
                }
                Save();
                UpdateSetsComboBox();
                Populate();
            }
        }

        private void cbViewHidden_CheckedChanged(object sender, EventArgs e)
        {
            Populate();
        }

    }
}
