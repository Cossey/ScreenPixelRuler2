using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    class RulerRenderer : IDisposable
    {
        readonly Form form;
        Theme theme;
        readonly System.Timers.Timer redrawer = new System.Timers.Timer
        {
            AutoReset = true,
            Interval = 20
        };
        public RulerRenderer(Form form)
        {
            this.form = form;

            this.theme = new Theme(); //Set default theme

            redrawer.Elapsed += Redrawer_Elapsed;
            redrawer.Start();
        }


        public void UseTheme(Theme theme)
        {
            this.theme = theme;
            form.Size = form.MaximumSize = form.MinimumSize = !Vertical ? new Size(MinSize, theme.GetRulerSize()) : new Size(theme.GetRulerSize(), MinSize);
            form.Invalidate();
        }

        private void Redrawer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            form.Invalidate();
        }

        int CursorLastPos = 0;
        readonly int MinSize = 440;
        bool FreezePosition { get; set; }
        public bool Direction { get; private set; }
        public bool Vertical => form.Height > form.Width;

        bool LastFreezePosition = false;

        public void DialogDisplay()
        {
            LastFreezePosition = FreezePosition;
            FreezePosition = true;
            form.TopMost = false;
            form.Invalidate();
        }

        public void NoDialogDisplay()
        {
            FreezePosition = LastFreezePosition;
            form.TopMost = true;
            form.Invalidate();
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
                form.Top = Cursor.Position.Y - theme.GetBorderSpacing();
            }
            else
            {
                form.Left = Cursor.Position.X - theme.GetBorderSpacing();
            }
        }

        public void ChangeOrientation()
        {
            form.Size = form.MaximumSize = form.MinimumSize = Vertical ? new Size(MinSize, theme.GetRulerSize()) : new Size(theme.GetRulerSize(), MinSize);
            form.Invalidate();
        }

        public void FlipDirection()
        {
            Direction = !Direction;
            form.Invalidate();
        }

        private void Background(Graphics graphics)
        {
            graphics.FillRectangle(theme.GetBackgroundBrush(form.ClientRectangle, Vertical, Direction), form.ClientRectangle);
        }

        readonly StringFormat horizontalFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private StringFormat VerticalFormat => new StringFormat
        {
            Alignment = Direction ? StringAlignment.Far : StringAlignment.Near,
            LineAlignment = Direction ? StringAlignment.Far : StringAlignment.Near
        };

        private void Markings(Graphics graphics)
        {
            int notchSize = 10;
            int notchHalfCmSize = 15;
            int notchCmSize = 20;

            for (int i = theme.GetBorderSpacing(); i < (Vertical ? form.Height : form.Width) - theme.GetBorderSpacing(); i += 2)
            {
                int size = notchSize;
                Pen pen = theme.GetLinesPen();
                if ((i % 20) == theme.GetBorderSpacing())
                {
                    size = notchHalfCmSize;
                }
                if ((i % 100 == theme.GetBorderSpacing()) && (i - theme.GetBorderSpacing() != 0))
                {
                    //Draw the numbers
                    size = Vertical ? form.Width : notchCmSize;

                    SizeF measureSize = graphics.MeasureString((i - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont());
                    Size textSize = new Size((int)Math.Ceiling(measureSize.Width), (int)Math.Ceiling(measureSize.Height));

                    graphics.DrawString((i - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont(), theme.GetNumberBrush(),
                        Vertical ?
                            new Rectangle(new Point(
                                Direction ?
                                    form.Width - (theme.GetNumberPadding(Vertical) + textSize.Width) :
                                    theme.GetNumberPadding(Vertical),
                                i - textSize.Height),
                                textSize) :
                            new Rectangle(new Point(
                                i - (textSize.Width / 2),
                                Direction ?
                                    form.Height - (theme.GetNumberPadding(Vertical) + textSize.Height) :
                                    theme.GetNumberPadding(Vertical)),
                                textSize),
                        Vertical ? VerticalFormat : horizontalFormat);
                }

                if (i == theme.GetBorderSpacing())
                {
                    pen = theme.GetBorderPen();
                    size = Vertical ? form.Height : form.Width;
                }

                graphics.DrawLine(pen,
                    !Vertical ? i : (Direction ? size : form.Width - size),
                    Vertical ? i : (Direction ? size : form.Height - size),
                    !Vertical ? i : (Direction ? 0 : form.Height),
                    Vertical ? i : (Direction ? 0 : form.Width));
            }
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
                    Width = Vertical ? form.Width : pos + theme.GetBorderSpacing(),
                    Height = Vertical ? pos + theme.GetBorderSpacing() : form.Height
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

            graphics.DrawLine(theme.GetCursorLinePen(),
                Vertical ? form.Width : pos,
                Vertical ? pos : form.Height,
                Vertical ? 0 : pos,
                Vertical ? pos : 0);

            SizeF textSize = graphics.MeasureString((pos - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont());

            Rectangle cursorBackgroundArea = new Rectangle(
                Vertical ? (int)(Direction ? form.Width - textSize.Width + 1 : 1) : (int)(pos - (textSize.Width / 2)),
                Vertical ? pos - 14 : (Direction ? 22 : 4),
                (int)textSize.Width,
                Vertical ? 13 : 14);

            graphics.FillRectangle(theme.GetCursorBackground(cursorBackgroundArea, Vertical, Direction), cursorBackgroundArea);

            graphics.DrawString((pos - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont(), theme.GetCursorFontBrush(),
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

        public void Dispose()
        {
            redrawer.Stop();
        }
    }
}
