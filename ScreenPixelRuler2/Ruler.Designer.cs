namespace ScreenPixelRuler2
{
    partial class Ruler
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Position = new System.Windows.Forms.Timer(this.components);
            this.RulerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SepMenu = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RulerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Position
            // 
            this.Position.Enabled = true;
            this.Position.Interval = 30;
            this.Position.Tick += new System.EventHandler(this.Position_Tick);
            // 
            // RulerMenu
            // 
            this.RulerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsMenu,
            this.AboutMenu,
            this.SepMenu,
            this.ExitMenu});
            this.RulerMenu.Name = "RulerMenu";
            this.RulerMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.RulerMenu.Size = new System.Drawing.Size(117, 76);
            // 
            // OptionsMenu
            // 
            this.OptionsMenu.Name = "OptionsMenu";
            this.OptionsMenu.Size = new System.Drawing.Size(116, 22);
            this.OptionsMenu.Text = "&Options";
            // 
            // AboutMenu
            // 
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.Size = new System.Drawing.Size(116, 22);
            this.AboutMenu.Text = "&About";
            // 
            // SepMenu
            // 
            this.SepMenu.Name = "SepMenu";
            this.SepMenu.Size = new System.Drawing.Size(113, 6);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.Size = new System.Drawing.Size(116, 22);
            this.ExitMenu.Text = "E&xit";
            // 
            // Ruler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 40);
            this.ContextMenuStrip = this.RulerMenu;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ruler";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Ruler";
            this.TopMost = true;
            this.RulerMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Position;
        private System.Windows.Forms.ContextMenuStrip RulerMenu;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.ToolStripSeparator SepMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
    }
}

