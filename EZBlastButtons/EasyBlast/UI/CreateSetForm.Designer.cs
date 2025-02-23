namespace EZBlastButtons.UI
{
    partial class CreateSetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSetForm));
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bConfirm = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.cbUseCore = new System.Windows.Forms.CheckBox();
            this.lblCore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(12, 25);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(300, 20);
            this.tbValue.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // bConfirm
            // 
            this.bConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bConfirm.BackColor = System.Drawing.Color.Gray;
            this.bConfirm.FlatAppearance.BorderSize = 0;
            this.bConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConfirm.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bConfirm.ForeColor = System.Drawing.Color.White;
            this.bConfirm.Location = new System.Drawing.Point(217, 58);
            this.bConfirm.Name = "bConfirm";
            this.bConfirm.Size = new System.Drawing.Size(94, 35);
            this.bConfirm.TabIndex = 2;
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
            this.bCancel.Location = new System.Drawing.Point(153, 58);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(58, 35);
            this.bCancel.TabIndex = 3;
            this.bCancel.Tag = "color:light1";
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // cbUseCore
            // 
            this.cbUseCore.AutoSize = true;
            this.cbUseCore.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUseCore.ForeColor = System.Drawing.Color.White;
            this.cbUseCore.Location = new System.Drawing.Point(12, 58);
            this.cbUseCore.Name = "cbUseCore";
            this.cbUseCore.Size = new System.Drawing.Size(108, 17);
            this.cbUseCore.TabIndex = 4;
            this.cbUseCore.Text = "Restrict to Core:";
            this.cbUseCore.UseVisualStyleBackColor = true;
            // 
            // lblCore
            // 
            this.lblCore.AutoSize = true;
            this.lblCore.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCore.ForeColor = System.Drawing.Color.White;
            this.lblCore.Location = new System.Drawing.Point(29, 77);
            this.lblCore.Name = "lblCore";
            this.lblCore.Size = new System.Drawing.Size(26, 13);
            this.lblCore.TabIndex = 5;
            this.lblCore.Text = "text";
            // 
            // CreateSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(323, 105);
            this.Controls.Add(this.lblCore);
            this.Controls.Add(this.cbUseCore);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bConfirm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbValue);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(339, 144);
            this.MinimumSize = new System.Drawing.Size(339, 144);
            this.Name = "CreateSetForm";
            this.Tag = "color:normal";
            this.Text = "Create new button set";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bConfirm;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.CheckBox cbUseCore;
        private System.Windows.Forms.Label lblCore;
    }
}