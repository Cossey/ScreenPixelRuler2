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
            this.GuidelinesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.GuidelineMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddGuidelineMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveNearestGuidelineMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveAllGuidelinesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.EditGuidelinesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SepMenu = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RulerMenu.SuspendLayout();
            this.GuidelineMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // RulerMenu
            // 
            this.RulerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RotateMenu,
            this.FlipDirectionMenu,
            this.GuidelinesMenu,
            this.toolStripSeparator1,
            this.OptionsMenu,
            this.HelpMenu,
            this.AboutMenu,
            this.SepMenu,
            this.ExitMenu});
            this.RulerMenu.Name = "RulerMenu";
            this.RulerMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.RulerMenu.ShowImageMargin = false;
            this.RulerMenu.Size = new System.Drawing.Size(215, 192);
            // 
            // RotateMenu
            // 
            this.RotateMenu.Name = "RotateMenu";
            this.RotateMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.RotateMenu.Size = new System.Drawing.Size(214, 22);
            this.RotateMenu.Text = "&Rotate";
            // 
            // FlipDirectionMenu
            // 
            this.FlipDirectionMenu.Name = "FlipDirectionMenu";
            this.FlipDirectionMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.FlipDirectionMenu.Size = new System.Drawing.Size(214, 22);
            this.FlipDirectionMenu.Text = "Flip Dir&ection";
            // 
            // GuidelinesMenu
            // 
            this.GuidelinesMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.GuidelinesMenu.DropDown = this.GuidelineMenu;
            this.GuidelinesMenu.Name = "GuidelinesMenu";
            this.GuidelinesMenu.Size = new System.Drawing.Size(214, 22);
            this.GuidelinesMenu.Text = "&Guidelines";
            // 
            // GuidelineMenu
            // 
            this.GuidelineMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddGuidelineMenu,
            this.RemoveNearestGuidelineMenu,
            this.RemoveAllGuidelinesMenu,
            this.toolStripSeparator2,
            this.EditGuidelinesMenu,
            this.toolStripSeparator3,
            this.ImportMenu,
            this.ExportMenu});
            this.GuidelineMenu.Name = "contextMenuStrip1";
            this.GuidelineMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.GuidelineMenu.ShowImageMargin = false;
            this.GuidelineMenu.Size = new System.Drawing.Size(136, 148);
            // 
            // AddGuidelineMenu
            // 
            this.AddGuidelineMenu.Name = "AddGuidelineMenu";
            this.AddGuidelineMenu.Size = new System.Drawing.Size(135, 22);
            this.AddGuidelineMenu.Text = "&Add";
            // 
            // RemoveNearestGuidelineMenu
            // 
            this.RemoveNearestGuidelineMenu.Name = "RemoveNearestGuidelineMenu";
            this.RemoveNearestGuidelineMenu.Size = new System.Drawing.Size(135, 22);
            this.RemoveNearestGuidelineMenu.Text = "Remove &Nearest";
            // 
            // RemoveAllGuidelinesMenu
            // 
            this.RemoveAllGuidelinesMenu.Name = "RemoveAllGuidelinesMenu";
            this.RemoveAllGuidelinesMenu.Size = new System.Drawing.Size(135, 22);
            this.RemoveAllGuidelinesMenu.Text = "C&lear All";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(132, 6);
            // 
            // EditGuidelinesMenu
            // 
            this.EditGuidelinesMenu.Name = "EditGuidelinesMenu";
            this.EditGuidelinesMenu.Size = new System.Drawing.Size(135, 22);
            this.EditGuidelinesMenu.Text = "&Edit Guidelines";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(132, 6);
            // 
            // ImportMenu
            // 
            this.ImportMenu.Name = "ImportMenu";
            this.ImportMenu.Size = new System.Drawing.Size(135, 22);
            this.ImportMenu.Text = "&Import";
            // 
            // ExportMenu
            // 
            this.ExportMenu.Name = "ExportMenu";
            this.ExportMenu.Size = new System.Drawing.Size(135, 22);
            this.ExportMenu.Text = "E&xport";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
            // 
            // OptionsMenu
            // 
            this.OptionsMenu.Name = "OptionsMenu";
            this.OptionsMenu.Size = new System.Drawing.Size(214, 22);
            this.OptionsMenu.Text = "&Options";
            // 
            // AboutMenu
            // 
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.Size = new System.Drawing.Size(214, 22);
            this.AboutMenu.Text = "&About";
            // 
            // SepMenu
            // 
            this.SepMenu.Name = "SepMenu";
            this.SepMenu.Size = new System.Drawing.Size(211, 6);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.ExitMenu.Size = new System.Drawing.Size(214, 22);
            this.ExitMenu.Text = "E&xit";
            // 
            // HelpMenu
            // 
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.ShortcutKeyDisplayString = "";
            this.HelpMenu.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.HelpMenu.Size = new System.Drawing.Size(214, 22);
            this.HelpMenu.Text = "&Help";
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
            this.GuidelineMenu.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem GuidelinesMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveAllGuidelinesMenu;
        private System.Windows.Forms.ToolStripMenuItem AddGuidelineMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveNearestGuidelineMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem EditGuidelinesMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ImportMenu;
        private System.Windows.Forms.ToolStripMenuItem ExportMenu;
        private System.Windows.Forms.ContextMenuStrip GuidelineMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
    }
}

