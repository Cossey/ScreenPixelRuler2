namespace ScreenPixelRuler2
{
    partial class Ruler
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RulerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RotateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FlipDirectionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SepMenu = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RulerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // RulerMenu
            // 
            this.RulerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RotateMenu,
            this.FlipDirectionMenu,
            this.toolStripSeparator1,
            this.OptionsMenu,
            this.AboutMenu,
            this.SepMenu,
            this.ExitMenu});
            this.RulerMenu.Name = "RulerMenu";
            this.RulerMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.RulerMenu.Size = new System.Drawing.Size(240, 126);
            // 
            // RotateMenu
            // 
            this.RotateMenu.Name = "RotateMenu";
            this.RotateMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.RotateMenu.Size = new System.Drawing.Size(239, 22);
            this.RotateMenu.Text = "&Rotate";
            // 
            // FlipDirectionMenu
            // 
            this.FlipDirectionMenu.Name = "FlipDirectionMenu";
            this.FlipDirectionMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.FlipDirectionMenu.Size = new System.Drawing.Size(239, 22);
            this.FlipDirectionMenu.Text = "Flip Dir&ection";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // OptionsMenu
            // 
            this.OptionsMenu.Name = "OptionsMenu";
            this.OptionsMenu.Size = new System.Drawing.Size(239, 22);
            this.OptionsMenu.Text = "&Options";
            // 
            // AboutMenu
            // 
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.Size = new System.Drawing.Size(239, 22);
            this.AboutMenu.Text = "&About";
            // 
            // SepMenu
            // 
            this.SepMenu.Name = "SepMenu";
            this.SepMenu.Size = new System.Drawing.Size(236, 6);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.ExitMenu.Size = new System.Drawing.Size(239, 22);
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
        private System.Windows.Forms.ContextMenuStrip RulerMenu;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.ToolStripSeparator SepMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
        private System.Windows.Forms.ToolStripMenuItem RotateMenu;
        private System.Windows.Forms.ToolStripMenuItem FlipDirectionMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

