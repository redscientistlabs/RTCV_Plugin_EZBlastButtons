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
    public partial class EZBlastButtonConfigForm : ComponentForm, IColorize
    {

        public MultiCorruptSettingsPack Pack
        {
            get
            {
                if (_pack == null)
                    Pack = new MultiCorruptSettingsPack();

                return _pack;
            }
            set { _pack = value; }
        }
        private MultiCorruptSettingsPack _pack = null;

        private bool corrupting = false;

        private static CerasSerializer saveSerializer;


        private CorruptionEngineForm originalEngineForm = null;

        static EZBlastButtonConfigForm()
        {
            saveSerializer = CreateSerializer();
        }

        public EZBlastButtonConfigForm()
        {
            InitializeComponent();
            C.Regather();

            ContextMenu menu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Edit Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {

                        if (!C.IsEngineSupported(((EngineSettings)lbEngines.SelectedItem).EngineType))
                        {
                            MessageBox.Show("Unable to open selected engine setting for editing, open RTC with a non-stub emulator to edit this engine setting","Unable to Edit Engine Setting",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return;
                        }

                        int multiEngineIndex = S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex;
                        C.CacheMasterSpec();
                        gbMain.Enabled = false;
                        var item = lbEngines.SelectedItem as EngineSettings;
                        var esf = new EngineSettingsForm(item);

                        if (esf.ShowDialog() == DialogResult.OK)
                        {
                            int idx = Pack.Settings.IndexOf(item);
                            Pack.Settings.RemoveAt(idx);
                            Pack.Settings.Insert(idx, esf.OutputSettings);
                            UpdateList();
                            S.GET<MemoryDomainsForm>().RefreshDomainsAndKeepSelected();
                        }
                        C.RestoreMasterSpec(true);
                        S.GET<CorruptionEngineForm>().ResyncAllEngines();
                        S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex = multiEngineIndex;
                        gbMain.Enabled = true;
                    }
                }),
                new MenuItem("Delete Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {
                        var item = lbEngines.SelectedItem as EngineSettings;
                        Pack.RemoveSetting(item);
                        lbEngines.Items.Remove(item);
                        UpdateList();
                    }
                })
            });
            originalEngineForm = S.GET<CorruptionEngineForm>();
            lbEngines.ContextMenu = menu;
        }




        public EZBlastButtonConfigForm(MultiCorruptSettingsPack editPack)
        {
            InitializeComponent();


            C.Regather();


            _pack = editPack;

            ContextMenu menu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Edit Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {


                        if (!C.IsEngineSupported(((EngineSettings)lbEngines.SelectedItem).EngineType))
                        {
                            MessageBox.Show("Unable to open selected engine setting for editing, open RTC with a non-stub emulator to edit this engine setting","Unable to Edit Engine Setting",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            return;
                        }

                        int multiEngineIndex = S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex;
                        C.CacheMasterSpec();
                        gbMain.Enabled = false;
                        var item = lbEngines.SelectedItem as EngineSettings;
                        var esf = new EngineSettingsForm(item);

                        if (esf.ShowDialog() == DialogResult.OK)
                        {
                            int idx = Pack.Settings.IndexOf(item);
                            Pack.Settings.RemoveAt(idx);
                            Pack.Settings.Insert(idx, esf.OutputSettings);
                            UpdateList();
                            //PushSettings();
                            S.GET<MemoryDomainsForm>().RefreshDomainsAndKeepSelected();
                        }
                        C.RestoreMasterSpec(true);
                        S.GET<CorruptionEngineForm>().ResyncAllEngines();
                        S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex = multiEngineIndex;
                        gbMain.Enabled = true;
                    }
                }),
                new MenuItem("Delete Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {
                        var item = lbEngines.SelectedItem as EngineSettings;
                        Pack.RemoveSetting(item);
                        lbEngines.Items.Remove(item);
                        //PushSettings();
                        UpdateList();
                    }
                })
            });
            originalEngineForm = S.GET<CorruptionEngineForm>();
            lbEngines.ContextMenu = menu;
            bConfirm.Text = "Save Changes";
            tbName.Text = editPack.Name;
            UpdateList();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            //Cache before changes
            C.CacheMasterSpec();
            int cachedIndex = S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex;
            var f = new EngineSettingsForm();
            gbMain.Enabled = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                Pack.AddSetting(f.OutputSettings);
                UpdateList();
                //PushSettings();
            }
            //Restore spec
            C.RestoreMasterSpec(true);
            //Resync UI
            S.GET<CorruptionEngineForm>().ResyncAllEngines();
            //Set us back to previous engine
            S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex = cachedIndex;
            gbMain.Enabled = true;
        }

        void UpdateList()
        {
            lbEngines.SuspendLayout();
            lbEngines.Items.Clear();

            for (int i = 0; i < Pack.Settings.Count; i++)
            {
                lbEngines.Items.Add(Pack.Settings[i]);
            }
            lbEngines.ResumeLayout();
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


        private void bLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() {Filter = "Multi Engine Files|*.men" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try 
                    {
                        var pack = saveSerializer.Deserialize<MultiCorruptSettingsPack>(File.ReadAllBytes(ofd.FileName));

                        bool ok = true;
                        List<string> errors = new List<string>();

                        foreach(var setting in pack.Settings)
                        {
                            setting.EnsureSpecUpdated();
                        }

                        foreach (var item in pack.Settings.Where(x => x.EngineType == CorruptionEngine.VECTOR))
                        {
                            //Verify lists

                            var ll = item.CachedSpec[RTCSPEC.VECTOR_LIMITERLISTHASH].ToString();
                            if (!Filtering.Hash2LimiterDico.ContainsKey(ll))
                            {
                                ok = false;
                                errors.Add(item.ExtraData[0]);
                            }
                            var vl = item.CachedSpec[RTCSPEC.VECTOR_VALUELISTHASH].ToString();
                            if (!Filtering.Hash2LimiterDico.ContainsKey(vl))
                            {
                                ok = false;
                                errors.Add(item.ExtraData[1]);
                            }
                        }

                        if (!ok)
                        {
                            var errstr = $"Cannot load file {ofd.FileName}, required limiter lists [ {string.Join(", ", errors.Select(x => x + ".txt"))} ] are not loaded in RTC.";
                            throw new Exception(errstr);
                        }

                        Pack = pack;
                        UpdateList();
                        //PushSettings();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"MultiEngine file load error{Environment.NewLine}", ex);
                    }
                }
            }

        }

        private void bUp_Click(object sender, EventArgs e)
        {
            if (lbEngines.SelectedItem != null)
            {
                var idx = lbEngines.SelectedIndex;
                if (idx > 0)
                {
                    var item = Pack.Settings[idx];
                    Pack.Settings.RemoveAt(idx);
                    Pack.Settings.Insert(idx - 1, item);
                    UpdateList();
                    //PushSettings();
                    lbEngines.SelectedIndex = idx - 1;
                }
            }
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            if (lbEngines.SelectedItem != null)
            {
                var idx = lbEngines.SelectedIndex;
                if (idx < (lbEngines.Items.Count-1))
                {
                    var item = Pack.Settings[idx];
                    Pack.Settings.RemoveAt(idx);
                    Pack.Settings.Insert(idx + 1, item);
                    UpdateList();
                    //PushSettings();
                    lbEngines.SelectedIndex = idx + 1;
                }
            }
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            if(Pack.Settings.Count == 0)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            Pack.Name = tbName.Text.Trim();
        }
    }


}
