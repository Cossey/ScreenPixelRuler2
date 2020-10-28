using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    public partial class Ruler : Form
    {
        readonly RulerRenderer renderer;
        public Ruler()
        {
            InitializeComponent();

            renderer = new RulerRenderer(this);

            renderer.UseTheme(Theming.GetThemeByName(Program.appConfig.Theme));

            if (Program.appConfig.Direction)
            {
                renderer.FlipDirection();
            }

            if (Program.appConfig.Vertical)
            {
                renderer.ChangeOrientation();
            }

            ExitMenu.Click += ExitMenu_Click;
            OptionsMenu.Click += OptionsMenu_Click;
            AboutMenu.Click += AboutMenu_Click;

            MouseMove += Ruler_MouseMove;
            MouseDown += Ruler_MouseDown;
            MouseUp += Ruler_MouseUp;

            Shown += Ruler_Shown;
        }

        private void Ruler_Shown(object sender, EventArgs e)
        {
            Location = Program.appConfig.Position.Point(); //Display ruler at last position on shutdown
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                renderer.SetStart();
            }
            if (keyData == (Keys.Control | Keys.F))
            {
                renderer.ToggleFreezePosition();
            }
            if (keyData == (Keys.Control | Keys.R))
            {
                renderer.ChangeOrientation();
            }
            if (keyData == (Keys.Control | Keys.E))
            {
                renderer.FlipDirection();
            }
            if (keyData == (Keys.Control | Keys.X))
            {
                Application.Exit();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            renderer.RenderAll(e.Graphics);
        }

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
                    if (!isRulerMoving)
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
            //Theme thm = Theming.LoadTheme(@"C:\Users\Stewart\source\repos\ScreenPixelRuler2\Resources\Chocolate.thm");
            //renderer.UseTheme(thm);

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
    }
}
