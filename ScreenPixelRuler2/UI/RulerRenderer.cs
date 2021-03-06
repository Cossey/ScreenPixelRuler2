﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            if (theme == null)
            {
                theme = new Theme();
            }
            this.theme = theme;
            form.Size = form.MaximumSize = form.MinimumSize = !Vertical ? new Size(GetMinSize(), theme.GetRulerSize()) : new Size(theme.GetRulerSize(), GetMinSize());
            form.Invalidate();
        }

        private void Redrawer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            form.Invalidate();
            GuidelineMouseLocker();
        }

        int CursorLastPos = 0;
        readonly int VertMinSize = 410;
        readonly int HorizMinSize = 620;
        int GetMinSize()
        {

            return Vertical ?
                (VertMinSize < GetMaxSize() ? GetMaxSize() : VertMinSize) + (theme.GetBorderSpacing() * 2) :
                (HorizMinSize < GetMaxSize() ? GetMaxSize() : HorizMinSize) + (theme.GetBorderSpacing() * 2);
        }
        int GetMaxSize()
        {
            if (Guidelines.Count > 0)
            {
                return Guidelines.Max() + theme.GetBorderSpacing();
            }
            return 0;
        }
        bool FreezePosition { get; set; }
        public bool Direction { get; private set; }
        public bool Vertical => form.Height > form.Width;

        bool LastFreezePosition = false;
        public List<int> Guidelines = new List<int>();

        public void ToggleGuidelineAtPosition()
        {
            int pos = CursorLastPos - theme.GetBorderSpacing();
            if (Guidelines.Contains(pos) && pos != 0)
            {
                Guidelines.RemoveAll(m => m.Equals(pos));
            }
            else
            {
                Guidelines.Add(pos);
                Guidelines.Sort();
            }
            form.Invalidate();
        }

        public void RemoveNearestGuideline()
        {
            Point cursor = form.PointToClient(Cursor.Position);
            Guidelines.Remove(Vertical ? Guidelines.ClosestTo(cursor.Y - theme.GetBorderSpacing()) : Guidelines.ClosestTo(cursor.X - theme.GetBorderSpacing()));
        }

        public void RemoveAllGuidelines()
        {
            Guidelines.Clear();
            form.Invalidate();
        }

        bool guidelineLock = false;
        int guidelineNearestPos = 0;
        public void LockToNearestGuideline()
        {
            guidelineLock = !guidelineLock;

            Point cursor = form.PointToClient(Cursor.Position);
            guidelineNearestPos = Vertical ? Guidelines.ClosestTo(cursor.Y) : Guidelines.ClosestTo(cursor.X);

            if (!guidelineLock)
            {
                Cursor.Clip = Rectangle.Empty;
            }
        }

        private void GuidelineMouseLocker()
        {
            if (guidelineLock)
            {
                if (Vertical)
                {
                    Cursor.Clip = new Rectangle(0, guidelineNearestPos + form.Top + theme.GetBorderSpacing(), Screen.GetBounds(Cursor.Position).Width, 1);
                }
                else
                {
                    Cursor.Clip = new Rectangle(guidelineNearestPos + form.Left + theme.GetBorderSpacing(), 0, 1, Screen.GetBounds(Cursor.Position).Height);
                }
            }
        }

        public void AddGuideline()
        {
            int pos = CursorLastPos - theme.GetBorderSpacing();
            if (!Guidelines.Contains(pos))
            {
                Guidelines.Add(pos);
                Guidelines.Sort();
                form.Invalidate();
            }
        }

        private void LineGuides(Graphics graphics)
        {
            Guidelines.ForEach(pos =>
            {
                int renderPos = pos + theme.GetBorderSpacing();
                int calcPos = pos - theme.GetBorderSpacing();
                Point cursor = form.PointToClient(Cursor.Position);
                bool isNearest = pos == (Vertical ? Guidelines.ClosestTo(cursor.Y - theme.GetBorderSpacing()) : Guidelines.ClosestTo(cursor.X - theme.GetBorderSpacing()));
                bool isLockedTo = guidelineLock && guidelineNearestPos == calcPos;

                Point startPoint = new Point
                {
                    X = Vertical ?
                        (Direction ? 0 : form.Width) :
                        renderPos,
                    Y = Vertical ?
                        renderPos :
                        (Direction ? 0 : form.Height)
                };
                Point endPoint = new Point
                {
                    X = Vertical ?
                        (Direction ? theme.GetGuidelineSize(Vertical, isLockedTo, isNearest) : form.Width - theme.GetGuidelineSize(Vertical, isLockedTo, isNearest)) :
                        renderPos,
                    Y = Vertical ?
                        renderPos :
                        (Direction ? theme.GetGuidelineSize(Vertical, isLockedTo, isNearest) : form.Height - theme.GetGuidelineSize(Vertical, isLockedTo, isNearest))
                };

                graphics.DrawLine(theme.GetGuidelinePen(isLockedTo, isNearest), startPoint, endPoint);
            });
        }

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
            form.Size = form.MaximumSize = form.MinimumSize = Vertical ? new Size(GetMinSize(), theme.GetRulerSize()) : new Size(theme.GetRulerSize(), GetMinSize());
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
            graphics.DrawRectangle(new Pen(theme.GetBorderColour(), 1), new Rectangle(0, 0, form.Width - 1, form.Height - 1));
        }

        readonly StringFormat horizontalFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private StringFormat VerticalFormat => new StringFormat
        {
            Alignment = Direction ? StringAlignment.Far : StringAlignment.Near,
            LineAlignment = StringAlignment.Far
        };

        private void Markings(Graphics graphics)
        {
            for (int i = theme.GetBorderSpacing(); i < (Vertical ? form.Height : form.Width) - theme.GetBorderSpacing(); i += 2)
            {
                Pen pen = theme.GetLinesPen(i);
                int size = theme.GetLinesSize(i, Vertical);

                //Display the number based off the interval theme setting
                if (((i - theme.GetBorderSpacing()) % theme.Ruler.Numbers.Display.Interval == 0) && (theme.GetShowNumberZero() || i - theme.GetBorderSpacing() != 0))
                {
                    SizeF measureSize = graphics.MeasureString((i - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont());
                    Size textSize = new Size((int)Math.Ceiling(measureSize.Width), (int)Math.Ceiling(measureSize.Height));

                    Rectangle numberDisplay;

                    if (Vertical)
                    {
                        numberDisplay = new Rectangle(new Point(
                            Direction ?
                                form.Width - (theme.GetNumberPadding(Vertical) + textSize.Width) :
                                theme.GetNumberPadding(Vertical),
                            i - textSize.Height),
                            textSize);
                    }
                    else
                    {
                        numberDisplay = new Rectangle(new Point(
                            i - (textSize.Width / 2),
                            Direction ?
                                form.Height - (theme.GetNumberPadding(Vertical) + textSize.Height) :
                                theme.GetNumberPadding(Vertical)),
                            textSize);
                    }

                    graphics.DrawString((i - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont(), theme.GetNumberBrush(),
                        numberDisplay, Vertical ? VerticalFormat : horizontalFormat);
                }

                if (i == theme.GetBorderSpacing())
                {
                    pen = theme.GetZeroPen();
                    size = theme.GetZeroLineSize(Vertical);
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

            SizeF measureSize = graphics.MeasureString((pos - theme.GetBorderSpacing()).ToString(), theme.Cursor.Font.Font.GetFont());
            Size textSize = new Size((int)Math.Ceiling(measureSize.Width), (int)Math.Ceiling(measureSize.Height));

            if (pos >= (GetMinSize() - ((!Vertical ? (textSize.Width / 2) : 0) + theme.GetBorderSpacing())))
            {
                form.Size = form.MaximumSize = form.MinimumSize = new Size
                {
                    Width = Vertical ? form.Width : pos + (textSize.Width / 2) + theme.GetBorderSpacing(),
                    Height = Vertical ? pos + theme.GetBorderSpacing() : form.Height
                };
            }
            else
            {
                form.Size = form.MaximumSize = form.MinimumSize = new Size
                {
                    Width = Vertical ? form.Width : GetMinSize(),
                    Height = Vertical ? GetMinSize() : form.Height
                };
            }

            graphics.DrawLine(theme.GetCursorLinePen(),
                Vertical ? form.Width : pos,
                Vertical ? pos : form.Height,
                Vertical ? 0 : pos,
                Vertical ? pos : 0);

            int padVertical = theme.Cursor.Font.Padding.Vertical;
            int padHoriz = theme.Cursor.Font.Padding.Horizontal;

            Rectangle cursorBackgroundArea = new Rectangle(
                Vertical ?
                    new Point( //Vertical
                        Direction ?
                            form.Width - (textSize.Width + padVertical) :
                            padVertical,
                        pos - textSize.Height) :
                    new Point( //Horizontal
                        pos - (textSize.Width / 2),
                        Direction ?
                            form.Height - (padHoriz + textSize.Height) :
                            padHoriz),
                textSize);

            graphics.FillRectangle(theme.GetCursorBackground(cursorBackgroundArea, Vertical, Direction), cursorBackgroundArea);

            graphics.DrawString((pos - theme.GetBorderSpacing()).ToString(), theme.Cursor.Font.Font.GetFont(), theme.GetCursorFontBrush(), new Rectangle(
                Vertical ?
                    new Point( //Vertical
                        Direction ?
                            form.Width - (textSize.Width + padVertical) :
                            padVertical,
                        pos - textSize.Height) :
                    new Point( //Horizontal
                        pos - ((textSize.Width + padHoriz) / 2),
                        Direction ?
                            form.Height - (padHoriz + textSize.Height) :
                            padHoriz),
                textSize), Vertical ? VerticalFormat : horizontalFormat);

            CursorLastPos = pos;
        }

        public void RenderAll(Graphics graphics)
        {
            Background(graphics);
            Markings(graphics);
            LineGuides(graphics);
            CursorPosition(graphics);
        }

        public void Dispose()
        {
            redrawer.Dispose();
        }
    }
}
