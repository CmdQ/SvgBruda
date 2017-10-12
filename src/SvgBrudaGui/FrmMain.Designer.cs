namespace SvgBrudaGui
{
    partial class FrmMain
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
            if (disposing)
            {
                _target?.Dispose();
                _settings.Dispose();
                _settingsForm?.Dispose();
                components?.Dispose();
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
            this.stsStatus = new System.Windows.Forms.StatusStrip();
            this.stsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.picDraw = new System.Windows.Forms.PictureBox();
            this.stsStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).BeginInit();
            this.SuspendLayout();
            // 
            // stsStatus
            // 
            this.stsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsLabel});
            this.stsStatus.Location = new System.Drawing.Point(0, 425);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(661, 22);
            this.stsStatus.TabIndex = 0;
            this.stsStatus.Text = "statusStrip1";
            // 
            // stsLabel
            // 
            this.stsLabel.Name = "stsLabel";
            this.stsLabel.Size = new System.Drawing.Size(118, 17);
            this.stsLabel.Text = "toolStripStatusLabel1";
            this.stsLabel.Click += new System.EventHandler(this.stsLabel_Click);
            // 
            // picDraw
            // 
            this.picDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDraw.Location = new System.Drawing.Point(0, 0);
            this.picDraw.Name = "picDraw";
            this.picDraw.Size = new System.Drawing.Size(661, 425);
            this.picDraw.TabIndex = 1;
            this.picDraw.TabStop = false;
            this.picDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.picDraw_Paint);
            this.picDraw.Resize += new System.EventHandler(this.picDraw_Resize);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 447);
            this.Controls.Add(this.picDraw);
            this.Controls.Add(this.stsStatus);
            this.Name = "FrmMain";
            this.Text = "BrudaSvg";
            this.stsStatus.ResumeLayout(false);
            this.stsStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsStatus;
        private System.Windows.Forms.ToolStripStatusLabel stsLabel;
        private System.Windows.Forms.PictureBox picDraw;
    }
}

