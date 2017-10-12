namespace SvgBrudaGui
{
    partial class FrmSettings
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
            this.prpGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // prpGrid
            // 
            this.prpGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prpGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.prpGrid.Location = new System.Drawing.Point(0, 0);
            this.prpGrid.Name = "prpGrid";
            this.prpGrid.Size = new System.Drawing.Size(245, 421);
            this.prpGrid.TabIndex = 0;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 421);
            this.Controls.Add(this.prpGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid prpGrid;
    }
}