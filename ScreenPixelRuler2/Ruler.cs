using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    public partial class Ruler : Form
    {
        KeyboardHook hook = new KeyboardHook();

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
            renderer.UseTheme(Theming.GetThemeByName(Program.appConfig.Theme));

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

        private void Ruler_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!isRulerMoving && Program.appConfig.ClickToRotate)
                    {
                        renderer.ChangeOrientation();
                    }
                    isRulerMoving = false;
                    isRulerBeingMoved = false;
                    break;
                case MouseButtons.Middle:
                    renderer.FlipDirection();
                    break;
            }
        }

        #endregion

        #region "Context Menu"

        private void CreateContextMenuEvents()
        {
            ExitMenu.Click += ExitMenu_Click;
            OptionsMenu.Click += OptionsMenu_Click;
            AboutMenu.Click += AboutMenu_Click;
            RotateMenu.Click += RotateMenu_Click;
            FlipDirectionMenu.Click += FlipDirectionMenu_Click;
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
                    renderer.UseTheme(Theming.GetThemeByName(Program.appConfig.Theme));
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
