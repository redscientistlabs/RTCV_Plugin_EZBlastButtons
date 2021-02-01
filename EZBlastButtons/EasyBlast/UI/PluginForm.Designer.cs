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
            this.cbSelectedEngine = new System.Windows.Forms.ComboBox();
            this.lblSystem = new System.Windows.Forms.Label();
            this.lblBlastSize = new System.Windows.Forms.Label();
            this.pRadioButtons = new System.Windows.Forms.Panel();
            this.rbSizeLarge = new System.Windows.Forms.RadioButton();
            this.rbSizeMedium = new System.Windows.Forms.RadioButton();
            this.rbSizeSmall = new System.Windows.Forms.RadioButton();
            this.gbButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.cbManualIntensity = new System.Windows.Forms.CheckBox();
            this.multiTB_Intensity = new RTCV.UI.Components.Controls.MultiTrackBar();
            this.cbGH = new System.Windows.Forms.CheckBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.pRadioButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSelectedEngine
            // 
            this.cbSelectedEngine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.cbSelectedEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedEngine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSelectedEngine.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSelectedEngine.ForeColor = System.Drawing.Color.White;
            this.cbSelectedEngine.FormattingEnabled = true;
            this.cbSelectedEngine.Location = new System.Drawing.Point(12, 35);
            this.cbSelectedEngine.Name = "cbSelectedEngine";
            this.cbSelectedEngine.Size = new System.Drawing.Size(165, 21);
            this.cbSelectedEngine.TabIndex = 139;
            this.cbSelectedEngine.Tag = "color:dark2";
            this.cbSelectedEngine.SelectedIndexChanged += new System.EventHandler(this.cbSelectedEngine_SelectedIndexChanged);
            // 
            // lblSystem
            // 
            this.lblSystem.AutoSize = true;
            this.lblSystem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSystem.ForeColor = System.Drawing.Color.White;
            this.lblSystem.Location = new System.Drawing.Point(9, 19);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(64, 13);
            this.lblSystem.TabIndex = 140;
            this.lblSystem.Text = "Button Set:";
            // 
            // lblBlastSize
            // 
            this.lblBlastSize.AutoSize = true;
            this.lblBlastSize.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBlastSize.ForeColor = System.Drawing.Color.White;
            this.lblBlastSize.Location = new System.Drawing.Point(199, 13);
            this.lblBlastSize.Name = "lblBlastSize";
            this.lblBlastSize.Size = new System.Drawing.Size(57, 13);
            this.lblBlastSize.TabIndex = 141;
            this.lblBlastSize.Text = "Blast Size:";
            // 
            // pRadioButtons
            // 
            this.pRadioButtons.BackColor = System.Drawing.Color.Transparent;
            this.pRadioButtons.Controls.Add(this.rbSizeLarge);
            this.pRadioButtons.Controls.Add(this.rbSizeMedium);
            this.pRadioButtons.Controls.Add(this.rbSizeSmall);
            this.pRadioButtons.Location = new System.Drawing.Point(262, 3);
            this.pRadioButtons.Name = "pRadioButtons";
            this.pRadioButtons.Size = new System.Drawing.Size(85, 85);
            this.pRadioButtons.TabIndex = 142;
            // 
            // rbSizeLarge
            // 
            this.rbSizeLarge.AutoSize = true;
            this.rbSizeLarge.ForeColor = System.Drawing.Color.White;
            this.rbSizeLarge.Location = new System.Drawing.Point(4, 55);
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
            this.rbSizeMedium.Location = new System.Drawing.Point(4, 32);
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
            this.rbSizeSmall.Location = new System.Drawing.Point(4, 10);
            this.rbSizeSmall.Name = "rbSizeSmall";
            this.rbSizeSmall.Size = new System.Drawing.Size(45, 17);
            this.rbSizeSmall.TabIndex = 0;
            this.rbSizeSmall.Text = "0.5x";
            this.rbSizeSmall.UseVisualStyleBackColor = true;
            // 
            // gbButtons
            // 
            this.gbButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbButtons.AutoScroll = true;
            this.gbButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.gbButtons.Location = new System.Drawing.Point(12, 110);
            this.gbButtons.Name = "gbButtons";
            this.gbButtons.Size = new System.Drawing.Size(630, 261);
            this.gbButtons.TabIndex = 143;
            this.gbButtons.Tag = "color:dark1";
            // 
            // cbManualIntensity
            // 
            this.cbManualIntensity.AutoSize = true;
            this.cbManualIntensity.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbManualIntensity.ForeColor = System.Drawing.Color.White;
            this.cbManualIntensity.Location = new System.Drawing.Point(353, 9);
            this.cbManualIntensity.Name = "cbManualIntensity";
            this.cbManualIntensity.Size = new System.Drawing.Size(137, 17);
            this.cbManualIntensity.TabIndex = 144;
            this.cbManualIntensity.Text = "Use Manual Blast Size";
            this.cbManualIntensity.UseVisualStyleBackColor = true;
            this.cbManualIntensity.CheckedChanged += new System.EventHandler(this.cbManualIntensity_CheckedChanged);
            // 
            // multiTB_Intensity
            // 
            this.multiTB_Intensity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiTB_Intensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.multiTB_Intensity.Checked = false;
            this.multiTB_Intensity.DisplayCheckbox = false;
            this.multiTB_Intensity.Enabled = false;
            this.multiTB_Intensity.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.multiTB_Intensity.Hexadecimal = false;
            this.multiTB_Intensity.Label = "Intensity";
            this.multiTB_Intensity.Location = new System.Drawing.Point(353, 28);
            this.multiTB_Intensity.Maximum = ((long)(65535));
            this.multiTB_Intensity.Minimum = ((long)(1));
            this.multiTB_Intensity.Name = "multiTB_Intensity";
            this.multiTB_Intensity.Size = new System.Drawing.Size(181, 60);
            this.multiTB_Intensity.TabIndex = 145;
            this.multiTB_Intensity.Tag = "color:normal";
            this.multiTB_Intensity.UncapNumericBox = false;
            this.multiTB_Intensity.Value = ((long)(1));
            // 
            // cbGH
            // 
            this.cbGH.AutoSize = true;
            this.cbGH.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbGH.ForeColor = System.Drawing.Color.White;
            this.cbGH.Location = new System.Drawing.Point(12, 71);
            this.cbGH.Name = "cbGH";
            this.cbGH.Size = new System.Drawing.Size(121, 17);
            this.cbGH.TabIndex = 146;
            this.cbGH.Text = "Load GH Savestate";
            this.cbGH.UseVisualStyleBackColor = true;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTip.ForeColor = System.Drawing.Color.LightGray;
            this.lblTip.Location = new System.Drawing.Point(540, 10);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(110, 65);
            this.lblTip.TabIndex = 147;
            this.lblTip.Text = "Tip:\r\nSome games don\'t \r\ncorrupt well with all\r\nlists, try different \r\nsets for t" +
    "he console";
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(654, 383);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.cbGH);
            this.Controls.Add(this.multiTB_Intensity);
            this.Controls.Add(this.cbManualIntensity);
            this.Controls.Add(this.gbButtons);
            this.Controls.Add(this.pRadioButtons);
            this.Controls.Add(this.lblBlastSize);
            this.Controls.Add(this.lblSystem);
            this.Controls.Add(this.cbSelectedEngine);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(670, 422);
            this.Name = "PluginForm";
            this.Tag = "color:normal";
            this.Text = "EZ Blast Buttons";
            this.pRadioButtons.ResumeLayout(false);
            this.pRadioButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox cbSelectedEngine;
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
    }
}