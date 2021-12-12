using System;
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

            Microsoft.Win32.SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;

            this.theme = new Theme(); //Set default theme

            redrawer.Elapsed += Redrawer_Elapsed;
            redrawer.Start();
        }

        //Reload the Default theme if the User has potentially changed their Colour theme
        private void SystemEvents_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            if (theme?.Name == Theming.DefaultTheme)
            {
                UseTheme(null);
            }
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
            //Don't enable lock when there are no guidelines.
            if (Guidelines.Count == 0)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            };

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

        private StringFormat HorizontalFormat => new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = !theme.Ruler?.Numbers?.Display?.Horizontal?.Rotate ?? true ? 0 : StringFormatFlags.DirectionVertical
        };

        private StringFormat VerticalFormat => new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = !theme.Ruler?.Numbers?.Display?.Vertical?.Rotate ?? true ? 0 : StringFormatFlags.DirectionVertical
        };

        private TNumberDisplayArrangement defaultArrangement ()
        {
            return new TNumberDisplayArrangement() { Alignment = Vertical ? StringAlignment.Near : StringAlignment.Center, Rotate = false };
        }

        private int CalcOffset(int size, int number)
        {
            TNumberDisplayArrangement arrange = (Vertical ? theme.Ruler?.Numbers?.Display?.Vertical : theme.Ruler?.Numbers?.Display?.Horizontal) ?? defaultArrangement();
            int position = number;
            switch (arrange.Alignment)
            {
                case StringAlignment.Near:
                    return position - size;

                case StringAlignment.Center:
                    return position - (size / 2);

                case StringAlignment.Far:
                    return position;
            }
            return 0;
        }

        private Rectangle CalcMarkText(Size textSize, int number)
        {
            Rectangle alignment = Rectangle.Empty;

            TNumberDisplayArrangement arrange = (Vertical ? theme.Ruler?.Numbers?.Display?.Vertical : theme.Ruler?.Numbers?.Display?.Horizontal) ?? defaultArrangement();

            alignment.Width = arrange.Rotate ? textSize.Height : textSize.Width;
            alignment.Height = arrange.Rotate ? textSize.Width : textSize.Height;

            if (Vertical)
            {
                alignment.X = Direction ? form.Width - (theme.GetNumberPadding(Vertical, Direction) + alignment.Width) : theme.GetNumberPadding(Vertical, Direction);
                alignment.Y = CalcOffset(alignment.Height, number);
            }
            else
            {
                alignment.X = CalcOffset(alignment.Width, number);
                alignment.Y = Direction ? form.Height - (theme.GetNumberPadding(Vertical, Direction) + alignment.Height) : theme.GetNumberPadding(Vertical, Direction);
            }

            return alignment;
        }

        private void Markings(Graphics graphics)
        {
            for (int i = theme.GetBorderSpacing(); i < (Vertical ? form.Height : form.Width) - theme.GetBorderSpacing(); i += 2)
            {
                Pen pen = theme.GetLinesPen(i);
                int size = theme.GetLinesSize(i, Vertical);

                //Display the number based off the interval theme setting
                if (((i - theme.GetBorderSpacing()) % theme.Ruler?.Numbers?.Display?.Interval == 0) && (theme.GetShowNumberZero() || i - theme.GetBorderSpacing() != 0))
                {
                    SizeF measureSize = graphics.MeasureString((i - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont());
                    Size textSize = new Size((int)Math.Ceiling(measureSize.Width), (int)Math.Ceiling(measureSize.Height));

                    Rectangle numberDisplay = TextOffset(CalcMarkText(textSize, i));

                    graphics.DrawString((i - theme.GetBorderSpacing()).ToString(), theme.Ruler.Numbers.Font.GetFont(), theme.GetNumberBrush(), numberDisplay, Vertical ? VerticalFormat : HorizontalFormat);
                }

                if (i == theme.GetBorderSpacing())
                {
                    pen = theme.GetZeroPen();
                    size = theme.GetZeroLineSize(Vertical);
                }

                if (pen != null)
                {
                    graphics.DrawLine(pen,
                        !Vertical ? i : (Direction ? size : form.Width - size),
                        Vertical ? i : (Direction ? size : form.Height - size),
                        !Vertical ? i : (Direction ? 0 : form.Height),
                        Vertical ? i : (Direction ? 0 : form.Width));
                }
            }
        }

        private Rectangle CalcCursor(Size textSize, int number)
        {
            int pad = theme.Cursor?.Font?.Padding?.GetVH(Vertical) ?? 0;

            Rectangle alignment = Rectangle.Empty;

            TNumberDisplayArrangement arrange = (Vertical ? theme.Ruler?.Numbers?.Display?.Vertical : theme.Ruler?.Numbers?.Display?.Horizontal) ?? defaultArrangement();

            alignment.Width = arrange.Rotate ? textSize.Height : textSize.Width;
            alignment.Height = arrange.Rotate ? textSize.Width : textSize.Height;

            if (!textSize.IsEmpty)
            {

                if (Vertical)
                {
                    alignment.X = Direction ? form.Width - (alignment.Width + pad) : pad;
                    alignment.Y = CalcOffset(alignment.Height, number);
                }
                else
                {
                    alignment.X = CalcOffset(alignment.Width, number);
                    alignment.Y = Direction ? form.Height - (alignment.Height + pad) : pad;
                }
            }
            return alignment;
        }

        private void CursorPosition(Graphics graphics)
        {
            Point cursor = form.PointToClient(Cursor.Position);
            int pos = Vertical ? cursor.Y : cursor.X;
            if (FreezePosition)
            {
                pos = CursorLastPos;
            }


            SizeF measureSize = SizeF.Empty;
            Size textSize = Size.Empty;

            if (theme.Cursor?.Font?.Font?.GetFont() != null)
            {
                measureSize = graphics.MeasureString((pos - theme.GetBorderSpacing()).ToString(), theme.Cursor.Font.Font.GetFont());
                textSize = new Size((int)Math.Ceiling(measureSize.Width), (int)Math.Ceiling(measureSize.Height));

            }

            if (!textSize.IsEmpty)
            {
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
            }

            graphics.DrawLine(theme.GetCursorLinePen(FreezePosition, guidelineLock),
                Vertical ? form.Width : pos,
                Vertical ? pos : form.Height,
                Vertical ? 0 : pos,
                Vertical ? pos : 0);

            Rectangle cursorBackgroundArea = CalcCursor(textSize, pos);

            if (!cursorBackgroundArea.IsEmpty)
            {
                graphics.FillRectangle(theme.GetCursorBackground(cursorBackgroundArea, Vertical, Direction, FreezePosition, guidelineLock), cursorBackgroundArea);
            }

            Rectangle offsetText = TextOffset(cursorBackgroundArea);

            if (!textSize.IsEmpty)
            {
                graphics.DrawString((pos - theme.GetBorderSpacing()).ToString(), theme.Cursor.Font.Font.GetFont(), theme.GetCursorFontBrush(FreezePosition, guidelineLock),
                    offsetText, Vertical ? VerticalFormat : HorizontalFormat);
            }
            CursorLastPos = pos;
        }

        private Rectangle TextOffset(Rectangle rect)
        {
            Rectangle offset = new Rectangle(rect.Location, rect.Size);
            int textOffset = theme.Ruler?.Numbers?.Offset?.GetVH(Vertical) ?? 0;
            bool rotated = Vertical ? (theme.Ruler?.Numbers?.Display?.Vertical?.Rotate ?? defaultArrangement().Rotate) : (theme.Ruler?.Numbers?.Display?.Horizontal?.Rotate ?? defaultArrangement().Rotate);

            if (rotated)
            {
                offset.X -= textOffset;
            }
            else
            {
                offset.Y += textOffset;
            }
            return offset;
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
