namespace EZBlastButtons.UI
{
    partial class EzBlastButtonControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bRun
            // 
            this.bRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.bRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRun.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRun.ForeColor = System.Drawing.Color.White;
            this.bRun.Location = new System.Drawing.Point(0, 0);
            this.bRun.Margin = new System.Windows.Forms.Padding(0);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(150, 50);
            this.bRun.TabIndex = 0;
            this.bRun.Text = "button1";
            this.bRun.UseVisualStyleBackColor = false;
            this.bRun.Click += new System.EventHandler(this.bRun_Click);
            // 
            // EzBlastButtonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bRun);
            this.Name = "EzBlastButtonControl";
            this.Size = new System.Drawing.Size(150, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bRun;
    }
}
