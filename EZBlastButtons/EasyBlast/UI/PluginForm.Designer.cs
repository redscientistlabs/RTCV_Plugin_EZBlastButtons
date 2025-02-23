namespace EZBlastButtons.UI
{
    partial class PluginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginForm));
            this.bRefresh = new System.Windows.Forms.Button();
            this.version = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbViewHiddenButtons = new System.Windows.Forms.CheckBox();
            this.bDeleteSet = new System.Windows.Forms.Button();
            this.bNewSet = new System.Windows.Forms.Button();
            this.lblTip = new System.Windows.Forms.Label();
            this.cbGH = new System.Windows.Forms.CheckBox();
            this.multiTB_Intensity = new RTCV.UI.Components.Controls.MultiTrackBar();
            this.cbManualIntensity = new System.Windows.Forms.CheckBox();
            this.gbButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pRadioButtons = new System.Windows.Forms.Panel();
            this.rbSizeLarge = new System.Windows.Forms.RadioButton();
            this.rbSizeMedium = new System.Windows.Forms.RadioButton();
            this.rbSizeSmall = new System.Windows.Forms.RadioButton();
            this.lblBlastSize = new System.Windows.Forms.Label();
            this.lblSystem = new System.Windows.Forms.Label();
            this.cbSelectedSet = new System.Windows.Forms.ComboBox();
            this.cbViewHiddenSets = new System.Windows.Forms.CheckBox();
            this.lblCoreRestrict = new System.Windows.Forms.Label();
            this.lblCurCore = new System.Windows.Forms.Label();
            this.pRadioButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // bRefresh
            // 
            this.bRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bRefresh.FlatAppearance.BorderSize = 0;
            this.bRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRefresh.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRefresh.ForeColor = System.Drawing.Color.White;
            this.bRefresh.Location = new System.Drawing.Point(295, 56);
            this.bRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(61, 35);
            this.bRefresh.TabIndex = 153;
            this.bRefresh.TabStop = false;
            this.bRefresh.Tag = "color:light1";
            this.bRefresh.Text = "Refresh";
            this.bRefresh.UseVisualStyleBackColor = false;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.ForeColor = System.Drawing.Color.White;
            this.version.Location = new System.Drawing.Point(178, 22);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(14, 13);
            this.version.TabIndex = 152;
            this.version.Text = "v";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 18F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 32);
            this.label1.TabIndex = 151;
            this.label1.Text = "EZ Blast Buttons";
            // 
            // cbViewHiddenButtons
            // 
            this.cbViewHiddenButtons.AutoSize = true;
            this.cbViewHiddenButtons.Checked = true;
            this.cbViewHiddenButtons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbViewHiddenButtons.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbViewHiddenButtons.ForeColor = System.Drawing.Color.White;
            this.cbViewHiddenButtons.Location = new System.Drawing.Point(199, 116);
            this.cbViewHiddenButtons.Margin = new System.Windows.Forms.Padding(4);
            this.cbViewHiddenButtons.Name = "cbViewHiddenButtons";
            this.cbViewHiddenButtons.Size = new System.Drawing.Size(135, 17);
            this.cbViewHiddenButtons.TabIndex = 150;
            this.cbViewHiddenButtons.Text = "View Hidden Buttons";
            this.cbViewHiddenButtons.UseVisualStyleBackColor = true;
            this.cbViewHiddenButtons.Visible = false;
            this.cbViewHiddenButtons.CheckedChanged += new System.EventHandler(this.cbViewHidden_CheckedChanged);
            // 
            // bDeleteSet
            // 
            this.bDeleteSet.BackColor = System.Drawing.Color.DarkRed;
            this.bDeleteSet.FlatAppearance.BorderSize = 0;
            this.bDeleteSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDeleteSet.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDeleteSet.ForeColor = System.Drawing.Color.White;
            this.bDeleteSet.Location = new System.Drawing.Point(257, 57);
            this.bDeleteSet.Margin = new System.Windows.Forms.Padding(4);
            this.bDeleteSet.Name = "bDeleteSet";
            this.bDeleteSet.Size = new System.Drawing.Size(34, 34);
            this.bDeleteSet.TabIndex = 149;
            this.bDeleteSet.TabStop = false;
            this.bDeleteSet.Tag = "";
            this.bDeleteSet.Text = "🗑";
            this.bDeleteSet.UseVisualStyleBackColor = false;
            this.bDeleteSet.Click += new System.EventHandler(this.bDeleteSet_Click);
            // 
            // bNewSet
            // 
            this.bNewSet.BackColor = System.Drawing.Color.Gray;
            this.bNewSet.FlatAppearance.BorderSize = 0;
            this.bNewSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNewSet.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bNewSet.ForeColor = System.Drawing.Color.White;
            this.bNewSet.Location = new System.Drawing.Point(199, 57);
            this.bNewSet.Margin = new System.Windows.Forms.Padding(4);
            this.bNewSet.Name = "bNewSet";
            this.bNewSet.Size = new System.Drawing.Size(50, 34);
            this.bNewSet.TabIndex = 148;
            this.bNewSet.TabStop = false;
            this.bNewSet.Tag = "color:light1";
            this.bNewSet.Text = "New..";
            this.bNewSet.UseVisualStyleBackColor = false;
            this.bNewSet.Click += new System.EventHandler(this.bNewSet_Click);
            // 
            // lblTip
            // 
            this.lblTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTip.ForeColor = System.Drawing.Color.LightGray;
            this.lblTip.Location = new System.Drawing.Point(720, 12);
            this.lblTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(110, 65);
            this.lblTip.TabIndex = 147;
            this.lblTip.Text = "Tip:\r\nSome games don\'t \r\ncorrupt well with all\r\nlists, try different \r\nsets for t" +
    "he console";
            this.lblTip.Visible = false;
            // 
            // cbGH
            // 
            this.cbGH.AutoSize = true;
            this.cbGH.Checked = true;
            this.cbGH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGH.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbGH.ForeColor = System.Drawing.Color.White;
            this.cbGH.Location = new System.Drawing.Point(16, 113);
            this.cbGH.Margin = new System.Windows.Forms.Padding(4);
            this.cbGH.Name = "cbGH";
            this.cbGH.Size = new System.Drawing.Size(121, 17);
            this.cbGH.TabIndex = 146;
            this.cbGH.Text = "Load GH Savestate";
            this.cbGH.UseVisualStyleBackColor = true;
            // 
            // multiTB_Intensity
            // 
            this.multiTB_Intensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.multiTB_Intensity.Checked = false;
            this.multiTB_Intensity.DisplayCheckbox = false;
            this.multiTB_Intensity.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.multiTB_Intensity.Hexadecimal = false;
            this.multiTB_Intensity.Label = "Intensity";
            this.multiTB_Intensity.Location = new System.Drawing.Point(422, 7);
            this.multiTB_Intensity.Margin = new System.Windows.Forms.Padding(4);
            this.multiTB_Intensity.Maximum = ((long)(65535));
            this.multiTB_Intensity.Minimum = ((long)(1));
            this.multiTB_Intensity.Name = "multiTB_Intensity";
            this.multiTB_Intensity.Size = new System.Drawing.Size(241, 74);
            this.multiTB_Intensity.TabIndex = 145;
            this.multiTB_Intensity.Tag = "color:normal";
            this.multiTB_Intensity.UncapNumericBox = false;
            this.multiTB_Intensity.Value = ((long)(1));
            // 
            // cbManualIntensity
            // 
            this.cbManualIntensity.AutoSize = true;
            this.cbManualIntensity.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbManualIntensity.ForeColor = System.Drawing.Color.White;
            this.cbManualIntensity.Location = new System.Drawing.Point(422, 74);
            this.cbManualIntensity.Margin = new System.Windows.Forms.Padding(4);
            this.cbManualIntensity.Name = "cbManualIntensity";
            this.cbManualIntensity.Size = new System.Drawing.Size(137, 17);
            this.cbManualIntensity.TabIndex = 144;
            this.cbManualIntensity.Text = "Use Manual Blast Size";
            this.cbManualIntensity.UseVisualStyleBackColor = true;
            this.cbManualIntensity.Visible = false;
            this.cbManualIntensity.CheckedChanged += new System.EventHandler(this.cbManualIntensity_CheckedChanged);
            // 
            // gbButtons
            // 
            this.gbButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbButtons.AutoScroll = true;
            this.gbButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.gbButtons.Location = new System.Drawing.Point(16, 135);
            this.gbButtons.Margin = new System.Windows.Forms.Padding(4);
            this.gbButtons.Name = "gbButtons";
            this.gbButtons.Size = new System.Drawing.Size(840, 321);
            this.gbButtons.TabIndex = 143;
            this.gbButtons.Tag = "color:dark1";
            // 
            // pRadioButtons
            // 
            this.pRadioButtons.BackColor = System.Drawing.Color.Transparent;
            this.pRadioButtons.Controls.Add(this.rbSizeLarge);
            this.pRadioButtons.Controls.Add(this.rbSizeMedium);
            this.pRadioButtons.Controls.Add(this.rbSizeSmall);
            this.pRadioButtons.Location = new System.Drawing.Point(360, 22);
            this.pRadioButtons.Margin = new System.Windows.Forms.Padding(4);
            this.pRadioButtons.Name = "pRadioButtons";
            this.pRadioButtons.Size = new System.Drawing.Size(54, 105);
            this.pRadioButtons.TabIndex = 142;
            // 
            // rbSizeLarge
            // 
            this.rbSizeLarge.AutoSize = true;
            this.rbSizeLarge.ForeColor = System.Drawing.Color.White;
            this.rbSizeLarge.Location = new System.Drawing.Point(5, 68);
            this.rbSizeLarge.Margin = new System.Windows.Forms.Padding(4);
            this.rbSizeLarge.Name = "rbSizeLarge";
            this.rbSizeLarge.Size = new System.Drawing.Size(36, 17);
            this.rbSizeLarge.TabIndex = 2;
            this.rbSizeLarge.Text = "2x";
            this.rbSizeLarge.UseVisualStyleBackColor = true;
            // 
            // rbSizeMedium
            // 
            this.rbSizeMedium.AutoSize = true;
            this.rbSizeMedium.Checked = true;
            this.rbSizeMedium.ForeColor = System.Drawing.Color.White;
            this.rbSizeMedium.Location = new System.Drawing.Point(5, 39);
            this.rbSizeMedium.Margin = new System.Windows.Forms.Padding(4);
            this.rbSizeMedium.Name = "rbSizeMedium";
            this.rbSizeMedium.Size = new System.Drawing.Size(36, 17);
            this.rbSizeMedium.TabIndex = 1;
            this.rbSizeMedium.TabStop = true;
            this.rbSizeMedium.Text = "1x";
            this.rbSizeMedium.UseVisualStyleBackColor = true;
            // 
            // rbSizeSmall
            // 
            this.rbSizeSmall.AutoSize = true;
            this.rbSizeSmall.ForeColor = System.Drawing.Color.White;
            this.rbSizeSmall.Location = new System.Drawing.Point(5, 12);
            this.rbSizeSmall.Margin = new System.Windows.Forms.Padding(4);
            this.rbSizeSmall.Name = "rbSizeSmall";
            this.rbSizeSmall.Size = new System.Drawing.Size(45, 17);
            this.rbSizeSmall.TabIndex = 0;
            this.rbSizeSmall.Text = "0.5x";
            this.rbSizeSmall.UseVisualStyleBackColor = true;
            // 
            // lblBlastSize
            // 
            this.lblBlastSize.AutoSize = true;
            this.lblBlastSize.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBlastSize.ForeColor = System.Drawing.Color.White;
            this.lblBlastSize.Location = new System.Drawing.Point(357, 5);
            this.lblBlastSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlastSize.Name = "lblBlastSize";
            this.lblBlastSize.Size = new System.Drawing.Size(57, 13);
            this.lblBlastSize.TabIndex = 141;
            this.lblBlastSize.Text = "Blast Size:";
            // 
            // lblSystem
            // 
            this.lblSystem.AutoSize = true;
            this.lblSystem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSystem.ForeColor = System.Drawing.Color.White;
            this.lblSystem.Location = new System.Drawing.Point(12, 45);
            this.lblSystem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(64, 13);
            this.lblSystem.TabIndex = 140;
            this.lblSystem.Text = "Button Set:";
            // 
            // cbSelectedSet
            // 
            this.cbSelectedSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cbSelectedSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSelectedSet.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSelectedSet.ForeColor = System.Drawing.Color.White;
            this.cbSelectedSet.FormattingEnabled = true;
            this.cbSelectedSet.Location = new System.Drawing.Point(16, 63);
            this.cbSelectedSet.Margin = new System.Windows.Forms.Padding(4);
            this.cbSelectedSet.Name = "cbSelectedSet";
            this.cbSelectedSet.Size = new System.Drawing.Size(175, 21);
            this.cbSelectedSet.TabIndex = 139;
            this.cbSelectedSet.Tag = "color:dark2";
            this.cbSelectedSet.SelectedIndexChanged += new System.EventHandler(this.cbSelectedEngine_SelectedIndexChanged);
            // 
            // cbViewHiddenSets
            // 
            this.cbViewHiddenSets.AutoSize = true;
            this.cbViewHiddenSets.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbViewHiddenSets.ForeColor = System.Drawing.Color.White;
            this.cbViewHiddenSets.Location = new System.Drawing.Point(199, 99);
            this.cbViewHiddenSets.Margin = new System.Windows.Forms.Padding(4);
            this.cbViewHiddenSets.Name = "cbViewHiddenSets";
            this.cbViewHiddenSets.Size = new System.Drawing.Size(116, 17);
            this.cbViewHiddenSets.TabIndex = 154;
            this.cbViewHiddenSets.Text = "View Hidden Sets";
            this.cbViewHiddenSets.UseVisualStyleBackColor = true;
            this.cbViewHiddenSets.CheckedChanged += new System.EventHandler(this.cbViewHiddenSets_CheckedChanged);
            // 
            // lblCoreRestrict
            // 
            this.lblCoreRestrict.AutoSize = true;
            this.lblCoreRestrict.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCoreRestrict.ForeColor = System.Drawing.Color.White;
            this.lblCoreRestrict.Location = new System.Drawing.Point(13, 88);
            this.lblCoreRestrict.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCoreRestrict.Name = "lblCoreRestrict";
            this.lblCoreRestrict.Size = new System.Drawing.Size(47, 13);
            this.lblCoreRestrict.TabIndex = 155;
            this.lblCoreRestrict.Text = "Set Info";
            // 
            // lblCurCore
            // 
            this.lblCurCore.AutoSize = true;
            this.lblCurCore.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCurCore.ForeColor = System.Drawing.Color.White;
            this.lblCurCore.Location = new System.Drawing.Point(196, 36);
            this.lblCurCore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurCore.Name = "lblCurCore";
            this.lblCurCore.Size = new System.Drawing.Size(66, 13);
            this.lblCurCore.TabIndex = 156;
            this.lblCurCore.Text = "Debug Info";
            this.lblCurCore.Visible = false;
            // 
            // PluginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(872, 471);
            this.Controls.Add(this.lblCurCore);
            this.Controls.Add(this.lblCoreRestrict);
            this.Controls.Add(this.cbViewHiddenSets);
            this.Controls.Add(this.bRefresh);
            this.Controls.Add(this.version);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbViewHiddenButtons);
            this.Controls.Add(this.bDeleteSet);
            this.Controls.Add(this.bNewSet);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.cbGH);
            this.Controls.Add(this.multiTB_Intensity);
            this.Controls.Add(this.cbManualIntensity);
            this.Controls.Add(this.gbButtons);
            this.Controls.Add(this.pRadioButtons);
            this.Controls.Add(this.lblBlastSize);
            this.Controls.Add(this.lblSystem);
            this.Controls.Add(this.cbSelectedSet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(888, 510);
            this.Name = "PluginForm";
            this.Tag = "color:normal";
            this.Text = "EZ Blast Buttons";
            this.pRadioButtons.ResumeLayout(false);
            this.pRadioButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox cbSelectedSet;
        private System.Windows.Forms.Label lblSystem;
        private System.Windows.Forms.Label lblBlastSize;
        private System.Windows.Forms.Panel pRadioButtons;
        private System.Windows.Forms.RadioButton rbSizeLarge;
        private System.Windows.Forms.RadioButton rbSizeMedium;
        private System.Windows.Forms.RadioButton rbSizeSmall;
        private System.Windows.Forms.FlowLayoutPanel gbButtons;
        public System.Windows.Forms.CheckBox cbManualIntensity;
        public RTCV.UI.Components.Controls.MultiTrackBar multiTB_Intensity;
        public System.Windows.Forms.CheckBox cbGH;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Button bNewSet;
        private System.Windows.Forms.Button bDeleteSet;
        public System.Windows.Forms.CheckBox cbViewHiddenButtons;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bRefresh;
        public System.Windows.Forms.CheckBox cbViewHiddenSets;
        private System.Windows.Forms.Label lblCoreRestrict;
        private System.Windows.Forms.Label lblCurCore;
    }
}