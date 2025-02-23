
namespace EZBlastButtons
{
    partial class EZBlastEngineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EZBlastEngineForm));
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.bOpenPlugin = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.gbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.bOpenPlugin);
            this.gbMain.Controls.Add(this.label14);
            this.gbMain.Location = new System.Drawing.Point(0, 3);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(420, 148);
            this.gbMain.TabIndex = 167;
            this.gbMain.TabStop = false;
            // 
            // bOpenPlugin
            // 
            this.bOpenPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpenPlugin.BackColor = System.Drawing.Color.Gray;
            this.bOpenPlugin.FlatAppearance.BorderSize = 0;
            this.bOpenPlugin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpenPlugin.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOpenPlugin.ForeColor = System.Drawing.Color.White;
            this.bOpenPlugin.Location = new System.Drawing.Point(12, 37);
            this.bOpenPlugin.Name = "bOpenPlugin";
            this.bOpenPlugin.Size = new System.Drawing.Size(396, 99);
            this.bOpenPlugin.TabIndex = 168;
            this.bOpenPlugin.TabStop = false;
            this.bOpenPlugin.Tag = "color:light1";
            this.bOpenPlugin.Text = "Open Ez Blast Buttons";
            this.bOpenPlugin.UseVisualStyleBackColor = false;
            this.bOpenPlugin.Click += new System.EventHandler(this.bOpenPlugin_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(322, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 167;
            this.label14.Text = "Emmanuel Blast";
            // 
            // EZBlastEngineForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(420, 151);
            this.Controls.Add(this.gbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 151);
            this.Name = "EZBlastEngineForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "color:dark1";
            this.Text = "EZBlast Engine";
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button bOpenPlugin;
    }
}