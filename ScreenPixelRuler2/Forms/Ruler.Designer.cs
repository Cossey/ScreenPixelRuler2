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
            components = new System.ComponentModel.Container();
            RulerMenu = new System.Windows.Forms.ContextMenuStrip(components);
            RotateMenu = new System.Windows.Forms.ToolStripMenuItem();
            FlipDirectionMenu = new System.Windows.Forms.ToolStripMenuItem();
            GuidelinesMenu = new System.Windows.Forms.ToolStripMenuItem();
            GuidelineMenu = new System.Windows.Forms.ContextMenuStrip(components);
            AddGuidelineMenu = new System.Windows.Forms.ToolStripMenuItem();
            RemoveNearestGuidelineMenu = new System.Windows.Forms.ToolStripMenuItem();
            RemoveAllGuidelinesMenu = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            EditGuidelinesMenu = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            ImportMenu = new System.Windows.Forms.ToolStripMenuItem();
            ExportMenu = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            OptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            SepMenu = new System.Windows.Forms.ToolStripSeparator();
            ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            RulerMenu.SuspendLayout();
            GuidelineMenu.SuspendLayout();
            SuspendLayout();
            // 
            // RulerMenu
            // 
            RulerMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            RulerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { RotateMenu, FlipDirectionMenu, GuidelinesMenu, toolStripSeparator1, OptionsMenu, HelpMenu, AboutMenu, SepMenu, ExitMenu });
            RulerMenu.Name = "RulerMenu";
            RulerMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            RulerMenu.ShowImageMargin = false;
            RulerMenu.Size = new System.Drawing.Size(262, 184);
            // 
            // RotateMenu
            // 
            RotateMenu.Name = "RotateMenu";
            RotateMenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.R;
            RotateMenu.Size = new System.Drawing.Size(261, 24);
            RotateMenu.Text = "&Rotate";
            // 
            // FlipDirectionMenu
            // 
            FlipDirectionMenu.Name = "FlipDirectionMenu";
            FlipDirectionMenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.E;
            FlipDirectionMenu.Size = new System.Drawing.Size(261, 24);
            FlipDirectionMenu.Text = "Flip Dir&ection";
            // 
            // GuidelinesMenu
            // 
            GuidelinesMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            GuidelinesMenu.DropDown = GuidelineMenu;
            GuidelinesMenu.Name = "GuidelinesMenu";
            GuidelinesMenu.Size = new System.Drawing.Size(261, 24);
            GuidelinesMenu.Text = "&Guidelines";
            // 
            // GuidelineMenu
            // 
            GuidelineMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            GuidelineMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { AddGuidelineMenu, RemoveNearestGuidelineMenu, RemoveAllGuidelinesMenu, toolStripSeparator2, EditGuidelinesMenu, toolStripSeparator3, ImportMenu, ExportMenu });
            GuidelineMenu.Name = "contextMenuStrip1";
            GuidelineMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            GuidelineMenu.ShowImageMargin = false;
            GuidelineMenu.Size = new System.Drawing.Size(285, 188);
            // 
            // AddGuidelineMenu
            // 
            AddGuidelineMenu.Name = "AddGuidelineMenu";
            AddGuidelineMenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.A;
            AddGuidelineMenu.Size = new System.Drawing.Size(284, 24);
            AddGuidelineMenu.Text = "&Add";
            // 
            // RemoveNearestGuidelineMenu
            // 
            RemoveNearestGuidelineMenu.Name = "RemoveNearestGuidelineMenu";
            RemoveNearestGuidelineMenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.D;
            RemoveNearestGuidelineMenu.Size = new System.Drawing.Size(284, 24);
            RemoveNearestGuidelineMenu.Text = "Remove &Nearest";
            // 
            // RemoveAllGuidelinesMenu
            // 
            RemoveAllGuidelinesMenu.Name = "RemoveAllGuidelinesMenu";
            RemoveAllGuidelinesMenu.Size = new System.Drawing.Size(284, 24);
            RemoveAllGuidelinesMenu.Text = "C&lear All";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(281, 6);
            // 
            // EditGuidelinesMenu
            // 
            EditGuidelinesMenu.Name = "EditGuidelinesMenu";
            EditGuidelinesMenu.Size = new System.Drawing.Size(284, 24);
            EditGuidelinesMenu.Text = "&Edit Guidelines";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(281, 6);
            // 
            // ImportMenu
            // 
            ImportMenu.Name = "ImportMenu";
            ImportMenu.Size = new System.Drawing.Size(284, 24);
            ImportMenu.Text = "&Import";
            // 
            // ExportMenu
            // 
            ExportMenu.Name = "ExportMenu";
            ExportMenu.Size = new System.Drawing.Size(284, 24);
            ExportMenu.Text = "E&xport";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // OptionsMenu
            // 
            OptionsMenu.Name = "OptionsMenu";
            OptionsMenu.Size = new System.Drawing.Size(261, 24);
            OptionsMenu.Text = "&Options";
            // 
            // HelpMenu
            // 
            HelpMenu.Name = "HelpMenu";
            HelpMenu.ShortcutKeyDisplayString = "";
            HelpMenu.ShortcutKeys = System.Windows.Forms.Keys.F1;
            HelpMenu.Size = new System.Drawing.Size(261, 24);
            HelpMenu.Text = "&Help";
            // 
            // AboutMenu
            // 
            AboutMenu.Name = "AboutMenu";
            AboutMenu.Size = new System.Drawing.Size(261, 24);
            AboutMenu.Text = "&About";
            // 
            // SepMenu
            // 
            SepMenu.Name = "SepMenu";
            SepMenu.Size = new System.Drawing.Size(258, 6);
            // 
            // ExitMenu
            // 
            ExitMenu.Name = "ExitMenu";
            ExitMenu.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.X;
            ExitMenu.Size = new System.Drawing.Size(261, 24);
            ExitMenu.Text = "E&xit";
            // 
            // Ruler
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(503, 53);
            ContextMenuStrip = RulerMenu;
            ControlBox = false;
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            KeyPreview = true;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Ruler";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Ruler";
            TopMost = true;
            RulerMenu.ResumeLayout(false);
            GuidelineMenu.ResumeLayout(false);
            ResumeLayout(false);

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

