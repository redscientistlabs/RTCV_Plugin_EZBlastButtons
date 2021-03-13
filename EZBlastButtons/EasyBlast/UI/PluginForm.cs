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
        SysDefHolder SetsJson = null;

        public static string PluginFolderPath = Path.Combine(RTCV.CorruptCore.RtcCore.PluginDir,nameof(EZBlastButtons));
        public static string PluginConfigPath = Path.Combine(PluginFolderPath, "EZBlastButtons.json");

        public static PluginForm pForm;

        public PluginForm()
        {
            InitializeComponent();
            pForm = this;

            //Bind up multitb
            multiTB_Intensity.ValueChanged += (sender, args) => RTCV.CorruptCore.RtcCore.Intensity = multiTB_Intensity.Value;
            multiTB_Intensity.registerSlave(S.GET<GeneralParametersForm>().multiTB_Intensity);

            try
            {
                if (!Directory.Exists(PluginFolderPath))
                {
                    Directory.CreateDirectory(PluginFolderPath);
                    Process.Start(PluginFolderPath);
                }

                SetsJson = JsonConvert.DeserializeObject<SysDefHolder>(File.ReadAllText(PluginConfigPath));
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show("Missing config file for Easy Manual Blast Buttons", "ERROR: Missing Config File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start(PluginFolderPath);
                throw ex;
            }
            catch(JsonSerializationException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR: Easy Manual Blast Buttons config deserialization error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }

            foreach (var item in SetsJson.Systems)
            {
                cbSelectedEngine.Items.Add(item.Key);
            }

            if (cbSelectedEngine.Items.Count > 0)
            {
                //var ind = cbSelectedEngine.Items.IndexOf("GCN / Wii General");
                cbSelectedEngine.SelectedIndex = 0;//ind > -1 ? ind : 0;
                curSys = SetsJson.Systems[cbSelectedEngine.SelectedItem.ToString()];
            }
            else
            {
                throw new Exception("Easy Manual Blast Buttons config file is empty");
            }

            this.Load += PluginForm_Load;
            this.Shown += PluginForm_Shown;
        }

        private void PluginForm_Shown(object sender, EventArgs e)
        {
            object paramValue = RTCV.NetCore.AllSpec.VanguardSpec[VSPEC.OVERRIDE_DEFAULTMAXINTENSITY];

            if (paramValue != null && paramValue is int maxintensity)
            {
                //var prevState = multiTB_Intensity.FirstLoadDone;
                //multiTB_Intensity.FirstLoadDone = false;
                multiTB_Intensity.Maximum = maxintensity;
                //multiTB_Intensity.FirstLoadDone = prevState;
            }
        }

        private void PluginForm_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void AddButton(string text, string limiterName, string valueName, long amt, string[] domains)
        {

            var hashNameDic = Filtering.Hash2NameDico;
            foreach (var item in hashNameDic)
            {
                if(item.Value == limiterName)
                {
                    //found
                    Button b = new Button()
                    {
                        Name = $"Add{limiterName}",
                        Text = text,
                        Height = 50,
                        Width = 150,
                        BackColor = pForm.cbSelectedEngine.BackColor,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 8),
                        UseVisualStyleBackColor = false,
                        FlatStyle = FlatStyle.Flat,
                        Tag = "color:light1"
                    };
                    string lnC = limiterName;
                    string lnCv = valueName;
                    long intensity = amt;
                    //b.FlatStyle = FlatStyle.Standard;
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
                            if(((ComboBoxItem<string>)cblItem).Name == lnC)
                            {
                                foreach (var cbvItem in cbvItems)
                                {
                                    if (((ComboBoxItem<string>)cbvItem).Name == lnCv)
                                    {
                                        //Set domains
                                        S.GET<MemoryDomainsForm>().SetMemoryDomainsSelectedDomains(domains);

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
                                            catch(Exception ex)
                                            {
                                                ((Button)o).Enabled = true;
                                                throw ex;
                                            }
                                            //try
                                            //{
                                            //    //int ind = S.GET<RTC_StockpilePlayer_Form>().dgvStockpile.SelectedRows[0].Index;
                                            //    //typeof(RTC_StockpilePlayer_Form).GetMethod("dgvStockpile_CellClick").Invoke(S.GET<RTC_StockpilePlayer_Form>(),
                                            //    //    new object[] { S.GET<RTC_StockpilePlayer_Form>().dgvStockpile, new DataGridViewCellEventArgs(0, S.GET<RTC_StockpilePlayer_Form>().dgvStockpile.SelectedRows[0].Index) });

                                            //    //S.GET<RTC_StockpilePlayer_Form>().dgvStockpile_CellClick(S.GET<RTC_StockpilePlayer_Form>().dgvStockpile, new DataGridViewCellEventArgs(0, S.GET<RTC_StockpilePlayer_Form>().dgvStockpile.SelectedRows[0].Index));
                                            //}
                                            //catch
                                            //{
                                            //    S.GET<UI_CoreForm>().btnManualBlast_Click(null, null);
                                            //}
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
                    gbButtons.Controls.Add(b);
                    break;
                }
            }
            //else do nothing
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
                        AddButton(item.Name, item.Limiter, item.Value, item.Intensity, item.Domains);
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
                string[] selectedDomains = (string[])AllSpec.UISpec["SELECTEDDOMAINS"];
                var selectedLim = ((ComboBoxItem<string>)S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorLimiterList.SelectedItem)?.Name.ToString();
                var selectedVal = ((ComboBoxItem<string>)S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorValueList.SelectedItem)?.Name.ToString();
                var intensity = RTCV.CorruptCore.RtcCore.Intensity;

                ((Button)sender).Click -= AddCurrentConfigAsButton;
                gbButtons.Controls.Remove((Control)sender);

                curSys.Buttons.Add(new ButtonDef() { Name = s, Limiter = selectedLim, Value = selectedVal, Intensity = intensity, Domains = selectedDomains });
                AddButton(s, selectedLim, selectedVal, intensity, selectedDomains);
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
                    serializer.Serialize(writer, SetsJson);
                }
            }
        }

        private bool updating = false;
        private void cbSelectedEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                if (SetsJson.Systems.ContainsKey(cbSelectedEngine.SelectedItem.ToString()))
                {
                    curSys = SetsJson.Systems[cbSelectedEngine.SelectedItem.ToString()];
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
                SetsJson.Systems.Add(s, new SystemDef() { Buttons = new List<ButtonDef>() });
                Save();
                updating = true;
                cbSelectedEngine.Items.Clear();
                foreach (var item in SetsJson.Systems)
                {
                    cbSelectedEngine.Items.Add(item.Key);
                }
                updating = false;
                cbSelectedEngine.SelectedIndex = cbSelectedEngine.Items.IndexOf(s);
                //AddAddButton();
                //curSys = SetsJson.Systems[s];
                //Populate();
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

    }
}
