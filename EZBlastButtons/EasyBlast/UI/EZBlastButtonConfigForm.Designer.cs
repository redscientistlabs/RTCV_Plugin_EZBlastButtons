
namespace EZBlastButtons
{
    partial class EZBlastButtonConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EZBlastButtonConfigForm));
            this.bAdd = new System.Windows.Forms.Button();
            this.lbEngines = new System.Windows.Forms.ListBox();
            this.bLoad = new System.Windows.Forms.Button();
            this.bUp = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.bConfirm = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // bAdd
            // 
            this.bAdd.BackColor = System.Drawing.Color.Gray;
            this.bAdd.FlatAppearance.BorderSize = 0;
            this.bAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bAdd.ForeColor = System.Drawing.Color.White;
            this.bAdd.Location = new System.Drawing.Point(12, 31);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(122, 64);
            this.bAdd.TabIndex = 158;
            this.bAdd.TabStop = false;
            this.bAdd.Tag = "color:light1";
            this.bAdd.Text = "Add Engine Setting";
            this.bAdd.UseVisualStyleBackColor = false;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // lbEngines
            // 
            this.lbEngines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEngines.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lbEngines.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEngines.ForeColor = System.Drawing.Color.White;
            this.lbEngines.FormattingEnabled = true;
            this.lbEngines.HorizontalScrollbar = true;
            this.lbEngines.IntegralHeight = false;
            this.lbEngines.Location = new System.Drawing.Point(140, 31);
            this.lbEngines.Name = "lbEngines";
            this.lbEngines.Size = new System.Drawing.Size(236, 105);
            this.lbEngines.TabIndex = 160;
            this.lbEngines.Tag = "color:dark2";
            // 
            // bLoad
            // 
            this.bLoad.BackColor = System.Drawing.Color.Gray;
            this.bLoad.FlatAppearance.BorderSize = 0;
            this.bLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLoad.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bLoad.ForeColor = System.Drawing.Color.White;
            this.bLoad.Location = new System.Drawing.Point(12, 101);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(122, 35);
            this.bLoad.TabIndex = 164;
            this.bLoad.TabStop = false;
            this.bLoad.Tag = "color:light1";
            this.bLoad.Text = "Import MultiEngine File";
            this.bLoad.UseVisualStyleBackColor = false;
            this.bLoad.Visible = false;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bUp
            // 
            this.bUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bUp.BackColor = System.Drawing.Color.Gray;
            this.bUp.FlatAppearance.BorderSize = 0;
            this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bUp.ForeColor = System.Drawing.Color.White;
            this.bUp.Location = new System.Drawing.Point(381, 31);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(26, 50);
            this.bUp.TabIndex = 165;
            this.bUp.TabStop = false;
            this.bUp.Tag = "color:light1";
            this.bUp.Text = "↑";
            this.bUp.UseVisualStyleBackColor = false;
            this.bUp.Click += new System.EventHandler(this.bUp_Click);
            // 
            // bDown
            // 
            this.bDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDown.BackColor = System.Drawing.Color.Gray;
            this.bDown.FlatAppearance.BorderSize = 0;
            this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDown.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDown.ForeColor = System.Drawing.Color.White;
            this.bDown.Location = new System.Drawing.Point(381, 86);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(26, 50);
            this.bDown.TabIndex = 166;
            this.bDown.TabStop = false;
            this.bDown.Tag = "color:light1";
            this.bDown.Text = "↓";
            this.bDown.UseVisualStyleBackColor = false;
            this.bDown.Click += new System.EventHandler(this.bDown_Click);
            // 
            // gbMain
            // 
            this.gbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMain.Controls.Add(this.label1);
            this.gbMain.Controls.Add(this.label14);
            this.gbMain.Controls.Add(this.bAdd);
            this.gbMain.Controls.Add(this.bDown);
            this.gbMain.Controls.Add(this.bUp);
            this.gbMain.Controls.Add(this.bLoad);
            this.gbMain.Controls.Add(this.lbEngines);
            this.gbMain.Location = new System.Drawing.Point(6, 3);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(420, 151);
            this.gbMain.TabIndex = 167;
            this.gbMain.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(370, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 169;
            this.label1.Text = "Reorder";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(137, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 13);
            this.label14.TabIndex = 168;
            this.label14.Text = "Engine Settings List:";
            // 
            // bConfirm
            // 
            this.bConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bConfirm.BackColor = System.Drawing.Color.Gray;
            this.bConfirm.FlatAppearance.BorderSize = 0;
            this.bConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConfirm.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bConfirm.ForeColor = System.Drawing.Color.White;
            this.bConfirm.Location = new System.Drawing.Point(332, 160);
            this.bConfirm.Name = "bConfirm";
            this.bConfirm.Size = new System.Drawing.Size(94, 35);
            this.bConfirm.TabIndex = 0;
            this.bConfirm.Tag = "color:light1";
            this.bConfirm.Text = "Add";
            this.bConfirm.UseVisualStyleBackColor = false;
            this.bConfirm.Click += new System.EventHandler(this.bConfirm_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.BackColor = System.Drawing.Color.Gray;
            this.bCancel.FlatAppearance.BorderSize = 0;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bCancel.ForeColor = System.Drawing.Color.White;
            this.bCancel.Location = new System.Drawing.Point(268, 160);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(58, 35);
            this.bCancel.TabIndex = 1;
            this.bCancel.Tag = "color:light1";
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.ForeColor = System.Drawing.Color.White;
            this.tbName.Location = new System.Drawing.Point(6, 173);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(134, 22);
            this.tbName.TabIndex = 168;
            this.tbName.Tag = "color:normal";
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 170;
            this.label2.Text = "Name:";
            // 
            // EZBlastButtonConfigForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(434, 201);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bConfirm);
            this.Controls.Add(this.gbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 240);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 240);
            this.Name = "EZBlastButtonConfigForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "color:dark1";
            this.Text = "EzBlast Button Config";
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.ListBox lbEngines;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bConfirm;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
    }
}