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
            this.imgWarning = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // bRun
            // 
            this.bRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.bRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bRun.Cursor = System.Windows.Forms.Cursors.Hand;
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
            // imgWarning
            // 
            this.imgWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.imgWarning.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgWarning.Location = new System.Drawing.Point(132, 3);
            this.imgWarning.Name = "imgWarning";
            this.imgWarning.Size = new System.Drawing.Size(15, 16);
            this.imgWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgWarning.TabIndex = 175;
            this.imgWarning.TabStop = false;
            this.imgWarning.Visible = false;
            // 
            // EzBlastButtonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imgWarning);
            this.Controls.Add(this.bRun);
            this.Name = "EzBlastButtonControl";
            this.Size = new System.Drawing.Size(150, 50);
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.PictureBox imgWarning;
    }
}
