using ScreenPixelRuler2.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    public partial class Ruler : Form
    {
        readonly KeyboardHook hook = new KeyboardHook();

        readonly RulerRenderer renderer;
        public Ruler()
        {
            InitializeComponent();

            renderer = new RulerRenderer(this);

            RestoreConfiguration();
            CreateContextMenuEvents();
            CreateRulerEvents();
            RegisterGlobalHotkeys();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                hook.Dispose();
                renderer.Dispose();
            }

            base.Dispose(disposing);
        }

        private void RestoreConfiguration()
        {
            renderer.UseTheme(Theming.Instance.GetThemeByName(Program.appConfig.Theme));

            if (Program.appConfig.Direction)
            {
                renderer.FlipDirection();
            }

            if (Program.appConfig.Vertical)
            {
                renderer.ChangeOrientation();
            }
        }

        private void CreateRulerEvents()
        {
            MouseMove += Ruler_MouseMove;
            MouseDown += Ruler_MouseDown;
            MouseUp += Ruler_MouseUp;

            Shown += Ruler_Shown;
        }

        private void Ruler_Shown(object sender, EventArgs e)
        {
            Location = Program.appConfig.Position.Point(); //Display ruler at last position on shutdown
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            renderer.RenderAll(e.Graphics);
        }

        #region "Global Hotkeys"
        private void RegisterGlobalHotkeys()
        {
            hook.KeyPressed += Hook_KeyPressed;
            ModifierKeys csa = ScreenPixelRuler2.ModifierKeys.Control | ScreenPixelRuler2.ModifierKeys.Alt | ScreenPixelRuler2.ModifierKeys.Shift;
            hook.RegisterHotKey(csa, Keys.R);
            hook.RegisterHotKey(csa, Keys.E);
            hook.RegisterHotKey(csa, Keys.S);
            hook.RegisterHotKey(csa, Keys.X);
            hook.RegisterHotKey(csa, Keys.F);
            hook.RegisterHotKey(csa, Keys.G);
            hook.RegisterHotKey(csa, Keys.D);
            hook.RegisterHotKey(csa, Keys.A);
        }

        private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.R:
                    renderer.ChangeOrientation();
                    break;
                case Keys.E:
                    renderer.FlipDirection();
                    break;
                case Keys.S:
                    renderer.SetStart();
                    break;
                case Keys.F:
                    renderer.ToggleFreezePosition();
                    break;
                case Keys.X:
                    Application.Exit();
                    break;
                case Keys.G:
                    renderer.LockToNearestGuideline();
                    break;
                case Keys.D:
                    renderer.RemoveNearestGuideline();
                    break;
                case Keys.A:
                    renderer.AddGuideline();
                    break;
            }
        }

        #endregion

        #region "Ruler Mouse Events"

        private Point mouseDownPosition;
        private bool isRulerBeingMoved = false;
        private bool isRulerMoving = false;

        private void Ruler_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isRulerBeingMoved = true;
                mouseDownPosition = new Point
                {
                    X = e.X,
                    Y = e.Y
                };
            }
        }

        private void Ruler_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRulerBeingMoved)
            {
                isRulerMoving = true;

                Location = new Point
                {
                    X = Location.X + (e.X - mouseDownPosition.X),
                    Y = Location.Y + (e.Y - mouseDownPosition.Y)
                };
            }
        }

        private void PrimaryClick()
        {
            PerformTask(Program.appConfig.PrimaryClick);
        }

        public void MiddleClick()
        {
            PerformTask(Program.appConfig.MiddleClick);
        }

        public void X1Click()
        {
            PerformTask(Program.appConfig.X1Click);
        }

        public void X2Click()
        {
            PerformTask(Program.appConfig.X2Click);
        }

        private void PerformTask(AppConfig.MouseClick mouseClickObject)
        {
            switch (mouseClickObject)
            {
                case AppConfig.MouseClick.Rotate:
                    renderer.ChangeOrientation();
                    break;

                case AppConfig.MouseClick.Flip:
                    renderer.FlipDirection();
                    break;

                case AppConfig.MouseClick.ToggleGuide:
                    renderer.ToggleGuidelineAtPosition();
                    break;

                case AppConfig.MouseClick.AddGuide:
                    renderer.AddGuideline();
                    break;

                case AppConfig.MouseClick.RemoveNearestGuide:
                    renderer.RemoveNearestGuideline();
                    break;

                case AppConfig.MouseClick.RemoveAllGuides:
                    renderer.RemoveAllGuidelines();
                    break;

                case AppConfig.MouseClick.LockToNearestGuide:
                    renderer.LockToNearestGuideline();
                    break;

            }
        }

        private void Ruler_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!isRulerMoving)
                    {
                        PrimaryClick();
                    }
                    isRulerMoving = false;
                    isRulerBeingMoved = false;
                    break;
                case MouseButtons.Middle:
                    MiddleClick();
                    break;
                case MouseButtons.XButton1:
                    X1Click();
                    break;
                case MouseButtons.XButton2:
                    X2Click();
                    break;
            }
        }

        #endregion

        #region "File Load/Save"

        private void SaveGuidelines(string path)
        {
            StringBuilder output = new StringBuilder();
            renderer.Guidelines.ForEach(each => output.AppendLine(each.ToString()));
            File.WriteAllText(path, output.ToString());
        }

        private void LoadGuidelines(string path)
        {
            string data = File.ReadAllText(path);

            string[] items;
            if (data.Contains(','))
            {
                items = data.Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                items = data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            }

            renderer.Guidelines.Clear();
            foreach (string item in items)
            {
                if (item.All(char.IsDigit))
                {
                    renderer.Guidelines.Add(Convert.ToInt32(item));
                }
            }
            this.Invalidate();
        }

        #endregion

        #region "Context Menu"

        private void CreateContextMenuEvents()
        {
            RulerMenu.Opening += RulerMenu_Opening;
            RulerMenu.Closing += RulerMenu_Closing;

            ExitMenu.Click += ExitMenu_Click;
            OptionsMenu.Click += OptionsMenu_Click;
            AboutMenu.Click += AboutMenu_Click;
            RotateMenu.Click += RotateMenu_Click;
            FlipDirectionMenu.Click += FlipDirectionMenu_Click;
            RemoveAllGuidelinesMenu.Click += RemoveAllGuidelinesMenu_Click;
            AddGuidelineMenu.Click += AddGuidelineMenu_Click;
            RemoveNearestGuidelineMenu.Click += RemoveNearestGuidelineMenu_Click;
            EditGuidelinesMenu.Click += EditGuidelinesMenu_Click;
            ImportMenu.Click += ImportMenu_Click;
            ExportMenu.Click += ExportMenu_Click;
            HelpMenu.Click += HelpMenu_Click;
        }

        private void HelpMenu_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = string.Format("https://cossey.github.io/ScreenPixelRuler2/help/{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString())
            });
        }

        private void ExportMenu_Click(object sender, EventArgs e)
        {
            if (renderer.Guidelines.Count > 0)
            {
                using (SaveFileDialog save = new SaveFileDialog())
                {
                    save.Title = "Export Guidelines";
                    save.OverwritePrompt = true;
                    save.Filter = "All Files|*.*";
                    save.AutoUpgradeEnabled = true;
                    renderer.DialogDisplay();
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        SaveGuidelines(save.FileName);
                    }
                    renderer.NoDialogDisplay();
                }
            }
            else
            {
                MessageBox.Show("There are no Guidelines to export.", "Export Guidelines", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportMenu_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Title = "Import Guidelines";
                open.CheckFileExists = true;
                open.Filter = "All Files|*.*";
                open.Multiselect = false;
                open.AutoUpgradeEnabled = true;
                renderer.DialogDisplay();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    LoadGuidelines(open.FileName);
                }
                renderer.NoDialogDisplay();
            }
        }

        private void EditGuidelinesMenu_Click(object sender, EventArgs e)
        {
            renderer.DialogDisplay();
            using (Guidelines guidelines = new Guidelines(renderer.Guidelines))
            {
                renderer.Guidelines = guidelines.ShowDialog();
            }
            renderer.NoDialogDisplay();
        }

        private void RemoveNearestGuidelineMenu_Click(object sender, EventArgs e)
        {
            renderer.RemoveNearestGuideline();
        }

        private void AddGuidelineMenu_Click(object sender, EventArgs e)
        {
            renderer.AddGuideline();
        }

        private void RulerMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            renderer.NoDialogDisplay();
        }

        private void RulerMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            renderer.DialogDisplay();
        }

        private void RemoveAllGuidelinesMenu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear all Guidelines?", "Clear Guidelines", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                renderer.RemoveAllGuidelines();
            }
        }

        private void FlipDirectionMenu_Click(object sender, EventArgs e)
        {
            renderer.FlipDirection();
        }

        private void RotateMenu_Click(object sender, EventArgs e)
        {
            renderer.ChangeOrientation();
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            using (About about = new About())
            {
                renderer.DialogDisplay();
                about.ShowDialog();
                renderer.NoDialogDisplay();
            }
        }

        private void OptionsMenu_Click(object sender, EventArgs e)
        {
            using (Options options = new Options(ref Program.appConfig))
            {
                renderer.DialogDisplay();
                if (options.ShowDialog() == DialogResult.OK)
                {
                    renderer.UseTheme(Theming.Instance.GetThemeByName(Program.appConfig.Theme));
                }
                renderer.NoDialogDisplay();
            }
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Program.appConfig.Position.Point(Location);
            Program.appConfig.Vertical = renderer.Vertical;
            Program.appConfig.Direction = renderer.Direction;
            Application.Exit();
        }

        #endregion

    }
}
