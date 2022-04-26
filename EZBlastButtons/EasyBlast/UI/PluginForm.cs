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

        HashSet<string> validDomains = new HashSet<string>();

        public PluginForm()
        {
            InitializeComponent();
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
                            //List<string> duplicates = new List<string>();
                            //bool hasDuplicates = false;
                            //var import = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(ofd.FileName));
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
                    //cbSelectedEngine.SelectedIndex = cbSelectedEngine.Items.IndexOf(newName);
                }
            }));

            bNewSet.ContextMenu = cm;

            try
            {
                if (!Directory.Exists(PluginFolderPath))
                {
                    Directory.CreateDirectory(PluginFolderPath);
                    //Process.Start(PluginFolderPath);
                }

                if (!File.Exists(PluginConfigPath))
                {
                    AllSets = new SysDefHolder { Systems = new Dictionary<string, SystemDef>() };
                    AllSets.Systems.Add("Default", new SystemDef() { Buttons = new List<ButtonDef>() });
                    Save();
                }
                else
                {
                    AllSets = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(PluginConfigPath));
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
                    //UpdateSetsComboBox(cbSelectedEngine.SelectedItem.ToString());
                    //Populate();
                }
            }
            //catch(FileNotFoundException ex)
            //{
            //    MessageBox.Show("Missing config file for Easy Manual Blast Buttons", "ERROR: Missing Config File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Process.Start(PluginFolderPath);
            //    throw ex;
            //}
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

        void Import(string file)
        {
            List<string> duplicates = new List<string>();
            bool hasDuplicates = false;
            var import = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(file));
            foreach (var item in import.Systems)
            {
                if (!AllSets.Systems.ContainsKey(item.Key))
                {
                    AllSets.Systems.Add(item.Key, item.Value);
                }
                else
                {
                    hasDuplicates = true;
                    duplicates.Add(item.Key);
                }
            }
            Save();
            UpdateSetsComboBox(cbSelectedEngine.SelectedItem.ToString());
            Populate();

            if (hasDuplicates)
            {
                MessageBox.Show($"The following sets were duplicates and not imported:{string.Join(", ", duplicates)}", "Duplicate Sets", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void ImportSilent(string file)
        {
            var import = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(file));
            foreach (var item in import.Systems)
            {
                if (!AllSets.Systems.ContainsKey(item.Key))
                {
                    AllSets.Systems.Add(item.Key, item.Value);
                }
            }
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


        private bool IsButtonValid(ButtonDef buttonDef)
        {
            var hashNameDic = Filtering.Hash2NameDico; //cache from allspec
            if(hashNameDic.Values.Contains(buttonDef.Limiter) && hashNameDic.Values.Contains(buttonDef.Value))
            {
                var doms = S.GET<MemoryDomainsForm>().lbMemoryDomains.Items;
                bool hasValidDomain = false;
                foreach (var dom in buttonDef.Domains)
                {
                    if (doms.Contains(dom))
                    {
                        hasValidDomain = true;
                        break;
                    }
                }
                return hasValidDomain;
            }
            else
            {
                return false;
            }
        }

        private void AddButton(ButtonDef buttonDef)
        {
            bool isValid = IsButtonValid(buttonDef);
            bool hiddenAllowed = cbViewHidden.Checked;

            if (isValid || hiddenAllowed)
            {
                Button b = new Button()
                {
                    Name = buttonDef.Name,
                    Text = buttonDef.Name,
                    Height = 50,
                    Width = 150,
                    BackColor = isValid ? pForm.cbSelectedEngine.BackColor : Color.DarkRed,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 8),
                    UseVisualStyleBackColor = false,
                    FlatStyle = FlatStyle.Flat,
                    Tag = isValid ? "color:light1" : "color:dark1"
                };
                string lnC = buttonDef.Limiter;
                string lnCv = buttonDef.Value;
                long intensity = buttonDef.Intensity;

                //If valid, add logic, else do not
                if (isValid)
                {
                    b.Click += (o, e) =>
                    {
                        var cbE = S.GET<CorruptionEngineForm>().cbSelectedEngine;
                        cbE.SelectedItem = "Vector Engine";
                        RTCV.CorruptCore.RtcCore.SelectedEngine = CorruptionEngine.VECTOR;

                        var cbL = S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorLimiterList;
                        var cbV = S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorValueList;


                        var cblItems = cbL.Items;
                        var cbvItems = cbV.Items;
                        foreach (var cblItem in cblItems)
                        {
                            if (((ComboBoxItem<string>)cblItem).Name == lnC)
                            {
                                foreach (var cbvItem in cbvItems)
                                {
                                    if (((ComboBoxItem<string>)cbvItem).Name == lnCv)
                                    {
                                    //Set domains
                                    S.GET<MemoryDomainsForm>().SetMemoryDomainsSelectedDomains(GetValidDomains(buttonDef.Domains));

                                        cbL.SelectedItem = cblItem;
                                        VectorEngine.LimiterListHash = ((ComboBoxItem<string>)cblItem).Value;
                                        cbV.SelectedItem = cbvItem;
                                        VectorEngine.ValueListHash = ((ComboBoxItem<string>)cbvItem).Value;
                                        if (!cbManualIntensity.Checked)
                                        {
                                            long finalIntensity = (long)((double)intensity * (rbSizeSmall.Checked ? 0.5 : rbSizeMedium.Checked ? 1 : 2));
                                        //S.GET<RTC_GlitchHarvesterIntensity_Form>().multiTB_Intensity.Value = finalIntensity;
                                        multiTB_Intensity.Value = finalIntensity;
                                            RTCV.CorruptCore.RtcCore.Intensity = finalIntensity;
                                        }
                                        if (cbGH.Checked)
                                        {
                                            try
                                            {
                                                ((Button)o).Enabled = false;
                                                S.GET<GlitchHarvesterBlastForm>().Corrupt(null, null);
                                            //.btnCorrupt_MouseDown(null, null);
                                            ((Button)o).Enabled = true;
                                            }
                                            catch (Exception ex)
                                            {
                                                ((Button)o).Enabled = true;
                                                throw ex;
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                ((Button)o).Enabled = false;
                                                S.GET<CoreForm>().ManualBlast(null, null);// .btnManualBlast_MouseDown(null, null);
                                            ((Button)o).Enabled = true;
                                            }
                                            catch (Exception ex)
                                            {
                                                ((Button)o).Enabled = true;
                                                throw ex;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    };
                }

                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("Remove Button", new EventHandler((o, e) =>
                {
                    curSys.Buttons.Remove(buttonDef);
                    gbButtons.Controls.Remove(b);
                    Save();
                }));
                b.ContextMenu = cm;

                gbButtons.Controls.Add(b);
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

            b.Click += AddCurrentConfigAsButton;
            gbButtons.Controls.Add(b);
        }


        private void AddCurrentConfigAsButton(object sender, EventArgs e)
        {
            string s = Prompt.ShowDialog("Name", "Enter Button Name");
            if (!string.IsNullOrWhiteSpace(s))
            {
                string[] selectedDomains = (string[])AllSpec.UISpec[UISPEC.SELECTEDDOMAINS];
                var selectedLim = ((ComboBoxItem<string>)S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorLimiterList.SelectedItem)?.Name.ToString();
                var selectedVal = ((ComboBoxItem<string>)S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorValueList.SelectedItem)?.Name.ToString();
                var intensity = RTCV.CorruptCore.RtcCore.Intensity;

                ((Button)sender).Click -= AddCurrentConfigAsButton;
                gbButtons.Controls.Remove((Control)sender);
                var bdef = new ButtonDef() { Name = s, Limiter = selectedLim, Value = selectedVal, Intensity = intensity, Domains = selectedDomains };
                curSys.Buttons.Add(bdef);
                AddButton(bdef);//, s, selectedLim, selectedVal, intensity, selectedDomains);
                Save();
                AddAddButton();

            }
        }

        private void Save()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(PluginConfigPath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, AllSets);
                }
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

                AllSets.Systems.Add(s, new SystemDef() { Buttons = new List<ButtonDef>() });
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
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
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
                    AllSets.Systems.Add("Default", new SystemDef() { Buttons = new List<ButtonDef>() });
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
