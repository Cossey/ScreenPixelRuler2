using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    public partial class Ruler : Form
    {
        RulerRenderer renderer;
        public Ruler()
        {
            InitializeComponent();

            renderer = new RulerRenderer(this);

            ExitMenu.Click += ExitMenu_Click;
            OptionsMenu.Click += OptionsMenu_Click;
            AboutMenu.Click += AboutMenu_Click;

            this.MouseMove += Ruler_MouseMove;
            this.MouseDown += Ruler_MouseDown;
            this.MouseUp += Ruler_MouseUp;

        }

        int cursorPosition = 0;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            renderer.RenderAll(e.Graphics);
        }

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

        private Point mouseDownPosition;
        private bool isRulerBeingMoved = false;
        private bool isRulerMoving = false;

        private void Ruler_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRulerBeingMoved)
            {
                isRulerMoving = true;

                this.Location = new Point
                {
                    X = this.Location.X + (e.X - mouseDownPosition.X),
                    Y = this.Location.Y + (e.Y - mouseDownPosition.Y)
                };
            }
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OptionsMenu_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Position_Tick(object sender, EventArgs e)
        {
            renderer.Refresh();
        }
    }
}
