using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    class RulerRenderer
    {
        Form form;
        public RulerRenderer(Form form)
        {
            this.form = form;
        }

        int BorderSpacing = 15;
        int CursorLastPos = 0;
        int MinSize = 440;
        bool FreezePosition { get; set; }
        bool Direction { get; set; }
        bool Vertical
        {
            get
            {
                return form.Height > form.Width;
            }
        }

        public void ToggleFreezePosition()
        {
            FreezePosition = !FreezePosition;
            form.Invalidate();
        }

        public void SetStart()
        {
            if (Vertical)
            {
                form.Top = Cursor.Position.Y - BorderSpacing;
            }
            else
            {
                form.Left = Cursor.Position.X - BorderSpacing;
            }
        }

        public void ChangeOrientation()
        {
            form.Size = form.MaximumSize = form.MinimumSize = (Vertical ? new Size(MinSize, 40) : new Size(40, MinSize));
            form.Invalidate();
        }

        public void FlipDirection()
        {
            Direction = !Direction;
            form.Invalidate();
        }

        private void Background(Graphics graphics)
        {
            LinearGradientMode gradientMode = Vertical ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical;
            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(form.ClientRectangle, Direction ? Color.FromArgb(145, 212, 255) : Color.FromArgb(0, 130, 254), Direction ? Color.FromArgb(0, 130, 254) : Color.FromArgb(145, 212, 255), gradientMode))
            {
                graphics.FillRectangle(gradientBrush, form.ClientRectangle);
            }
        }

        Pen notchPen = new Pen(Color.Black, 1);
        Pen borderPen = new Pen(Color.White, 1);
        Font numberFont = new Font("Courier New", 9);
        SolidBrush numberBrush = new SolidBrush(Color.White);

        StringFormat horizontalFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private StringFormat VerticalFormat
        {
            get
            {
                return new StringFormat
                {
                    Alignment = Direction ? StringAlignment.Far : StringAlignment.Near,
                    LineAlignment = Direction ? StringAlignment.Far : StringAlignment.Near
                };
            }
        }

        private void Markings(Graphics graphics)
        {
            int notchSize = 10;
            int notchHalfCmSize = 15;
            int notchCmSize = 20;

            for (int i = BorderSpacing; i < (Vertical ? form.Height : form.Width) - BorderSpacing; i += 2)
            {
                int size = notchSize;
                Pen pen = notchPen;
                if ((i % 20) == BorderSpacing)
                {
                    size = notchHalfCmSize;
                }
                if ((i % 100 == BorderSpacing) && (i - BorderSpacing != 0))
                {
                    size = Vertical ? form.Width : notchCmSize;
                    graphics.DrawString((i - BorderSpacing).ToString(), numberFont, numberBrush, Vertical ? new Rectangle(0, i - 15, 40, 15) : new Rectangle(i - 20, Direction ? 22 : 3, 40, 15), Vertical ? VerticalFormat : horizontalFormat);
                }

                if (i == BorderSpacing)
                {
                    pen = borderPen;
                    size = Vertical ? form.Height : form.Width;
                }

                graphics.DrawLine(pen,
                    !Vertical ? i : (Direction ? size : form.Width - size),
                    Vertical ? i : (Direction ? size : form.Height - size),
                    !Vertical ? i : (Direction ? 0 : form.Height),
                    Vertical ? i : (Direction ? 0 : form.Width));
            }
        }

        public void Refresh()
        {
            form.Invalidate();
        }

        private void CursorPosition(Graphics graphics)
        {
            Point cursor = form.PointToClient(Cursor.Position);
            int pos = Vertical ? cursor.Y : cursor.X;
            if (FreezePosition)
            {
                pos = CursorLastPos;
            }

            if (pos >= MinSize)
            {
                form.Size = form.MaximumSize = form.MinimumSize = new Size
                {
                    Width = Vertical ? form.Width : pos + BorderSpacing,
                    Height = Vertical ? pos + BorderSpacing : form.Height
                };
            }
            else
            {
                form.Size = form.MaximumSize = form.MinimumSize = new Size
                {
                    Width = Vertical ? form.Width : MinSize,
                    Height = Vertical ? MinSize : form.Height
                };
            }

            graphics.DrawLine(borderPen,
                Vertical ? form.Width : pos,
                Vertical ? pos : form.Height,
                Vertical ? 0 : pos,
                Vertical ? pos : 0) ;

            SizeF textSize = graphics.MeasureString((pos - BorderSpacing).ToString(), numberFont);

            graphics.FillRectangle(numberBrush, 
                Vertical ? (Direction ? form.Width - textSize.Width + 1 : 1) : pos - (textSize.Width / 2), 
                Vertical ? pos - 14: (Direction ? 22 : 4), 
                textSize.Width, 
                Vertical ? 13 : 14);
            graphics.DrawString((pos - BorderSpacing).ToString(), numberFont, new SolidBrush(Color.Black), 
                Vertical ? new Rectangle(0, pos - 15, 40, 15) : new Rectangle(pos - 20, Direction ? 22 : 3, 40, 15), 
                Vertical ? VerticalFormat : horizontalFormat);

            CursorLastPos = pos;
        }

        public void RenderAll(Graphics graphics)
        {
            Background(graphics);
            Markings(graphics);
            CursorPosition(graphics);
        }

    }
}
