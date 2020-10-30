using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using YamlDotNet.Serialization;

namespace ScreenPixelRuler2
{
    class Theme
    {
        readonly int minSize = 20;
        readonly int maxSize = 100;
        public Theme()
        {
            Name = Theming.DefaultTheme;
            Cursor = new TCursor
            {
                Font = new TTextAspect
                {
                    Colour = Color.White,
                    Padding = new TVertHoriz
                    {
                        Horizontal = 2,
                        Vertical = 0
                    },
                    Font = new TFont
                    {
                        Bold = false,
                        Italic = false,
                        Strikeout = false,
                        Underline = false,
                        Family = "Courier New",
                        Size = 9
                    }
                },
                Line = Color.Black,
                Background = new List<Color>
                {
                    Color.Black
                }
            };
            Ruler = new TRuler
            {
                Background = new List<Color>
                {
                    Color.White
                },
                Size = 40,
                Lines = new TLines
                {
                    Zero = new TZero
                    {
                        Size = new TVertHoriz
                        {
                            Horizontal = 20,
                            Vertical = 40
                        },
                        NumberVisible = true
                    },
                    Colour = Color.Black,
                    Size = new TVertHoriz
                    {
                        Horizontal = 8,
                        Vertical = 8
                    },
                    Sizes = new List<TLineSizes>
                    {
                        new TLineSizes
                        {
                            Colour = Color.Black,
                            Interval = 10,
                            Size = new TVertHoriz
                            {
                                 Horizontal = 13,
                                 Vertical = 13
                            }
                        },
                        new TLineSizes
                        {
                            Colour = Color.Black,
                            Interval = 50,
                            Size = new TVertHoriz
                            {
                                Horizontal = 20,
                                Vertical = 40
                            }
                        }
                    }
                },
                Numbers = new TNumbers
                {
                    Colour = Color.Black,
                    Padding = new TVertHoriz
                    {
                        Horizontal = 2,
                        Vertical = 0
                    },
                    Font = new TFont
                    {
                        Bold = false,
                        Italic = false,
                        Strikeout = false,
                        Underline = false,
                        Family = "Courier New",
                        Size = 9
                    },
                    Display = new TNumberDisplay
                    {
                        Interval = 50
                    }
                },
                Border = new TBorder
                {
                    Spacing = 15,
                    Colour = Color.Black
                }
            };
        }

        public override string ToString()
        {
            return Name;
        }

        [YamlIgnore]
        public string Path { get; set; }

        public string Name { get; set; }

        public TCursor Cursor { get; set; }
        public TRuler Ruler { get; set; }

        public Brush GetBackgroundBrush(Rectangle clientArea, bool verticality, bool direction)
        {
            if (Ruler.Background.Count == 0)
            {
                return new SolidBrush(Color.White);
            }
            else if (Ruler.Background.Count > 1)
            {
                LinearGradientMode gradientMode = verticality ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical;
                return new LinearGradientBrush(clientArea, direction ? Ruler.Background[0] : Ruler.Background[1], direction ? Ruler.Background[1] : Ruler.Background[0], gradientMode);
            }
            else
            {
                return new SolidBrush(Ruler.Background[0]);
            }
        }

        public Pen GetLinesPen(int count)
        {
            List<TLineSizes> sorted = Ruler.Lines.Sizes.OrderByDescending(o => o.Interval).ToList();
            TLineSizes lineSize = sorted.Find(f => count % f.Interval == GetBorderSpacing());

            if (lineSize != null)
            {
                return new Pen(lineSize.Colour.IsEmpty ? Ruler.Lines.Colour : lineSize.Colour, 1);
            }
            else
            {
                return new Pen(Ruler.Lines.Colour, 1);
            }
        }

        public int GetLinesSize(int count, bool vertical)
        {
            List<TLineSizes> sorted = Ruler.Lines.Sizes.OrderByDescending(o => o.Interval).ToList();
            TLineSizes lineSize = sorted.Find(f => (count - GetBorderSpacing()) % f.Interval == 0);

            if (lineSize != null)
            {
                return lineSize.Size.GetVH(vertical);
            }
            return Ruler.Lines.Size.GetVH(vertical);
        }

        public bool GetShowNumberZero()
        {
            return Ruler.Lines != null && Ruler.Lines.Zero != null ? Ruler.Lines.Zero.NumberVisible : false;
        }

        public Brush GetNumberBrush()
        {
            return new SolidBrush(Ruler.Numbers.Colour);
        }

        public int GetNumberPadding(bool vertical)
        {
            return vertical ? Ruler.Numbers.Padding.Vertical : Ruler.Numbers.Padding.Horizontal;
        }

        public Pen GetZeroPen()
        {
            return new Pen(Ruler.Lines.Zero == null || Ruler.Lines.Zero.Colour.IsEmpty ? Ruler.Lines.Colour : Ruler.Lines.Zero.Colour, 1);
        }

        public int GetZeroLineSize(bool vertical)
        {
            return Ruler.Lines.Zero == null ? Ruler.Lines.Size.GetVH(vertical) : Ruler.Lines.Zero.Size.GetVH(vertical);
        }

        public int GetBorderSpacing()
        {
            return Ruler != null && Ruler.Border != null ? Ruler.Border.Spacing : 15;
        }

        public Color GetBorderColour()
        {
            return Ruler != null && Ruler.Border != null && !Ruler.Border.Colour.IsEmpty ? Ruler.Border.Colour : Color.Transparent;
        }

        public Pen GetCursorLinePen()
        {
            return new Pen(Cursor.Line, 1);
        }

        public Brush GetCursorFontBrush()
        {
            return new SolidBrush(Cursor.Font.Colour);
        }

        public Brush GetCursorBackground(Rectangle clientArea, bool verticality, bool direction)
        {
            if (Cursor.Background.Count == 0)
            {
                return new SolidBrush(Color.Black);
            }
            else if (Cursor.Background.Count > 1)
            {
                LinearGradientMode gradientMode = verticality ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical;
                return new LinearGradientBrush(clientArea, direction ? Cursor.Background[0] : Cursor.Background[1], direction ? Cursor.Background[1] : Cursor.Background[0], gradientMode);
            }
            else
            {
                return new SolidBrush(Cursor.Background[0]);
            }
        }
        public int GetRulerSize()
        {
            return Ruler != null && Ruler.Size >= minSize && Ruler.Size <= maxSize ? Ruler.Size : (Ruler.Size < minSize ? minSize : maxSize);
        }
    }

    class TNumberDisplay
    {
        public int Interval { get; set; }
    }

    class TRuler
    {
        public int Size { get; set; }
        public TLines Lines { get; set; }
        public TNumbers Numbers { get; set; }
        public TBorder Border { get; set; }
        public List<Color> Background { get; set; }
    }

    class TLines
    {
        public Color Colour { get; set; }
        public TVertHoriz Size { get; set; }
        public List<TLineSizes> Sizes { get; set; }
        public TZero Zero { get; set; }
    }

    class TZero
    {
        public Color Colour { get; set; }
        public TVertHoriz Size { get; set; }
        public bool NumberVisible { get; set; }
    }

    class TLineSizes
    {
        public int Interval { get; set; }
        public TVertHoriz Size { get; set; }
        public Color Colour { get; set; }
    }

    class TTextAspect
    {
        public TVertHoriz Padding { get; set; }
        public Color Colour { get; set; }
        public TFont Font { get; set; }
    }

    class TNumbers : TTextAspect
    {
        [DefaultValue(100)]
        public TNumberDisplay Display { get; set; }
    }

    class TFont
    {
        [DefaultValue("Courier New")]
        public string Family { get; set; }
        [DefaultValue(9)]
        public float Size { get; set; }
        public bool Bold { get; set; }
        public bool Underline { get; set; }
        public bool Italic { get; set; }
        public bool Strikeout { get; set; }

        public Font GetFont()
        {
            FontStyle fontStyle = FontStyle.Regular;
            if (Bold)
            {
                fontStyle |= FontStyle.Bold;
            }
            if (Italic)
            {
                fontStyle |= FontStyle.Italic;
            }
            if (Underline)
            {
                fontStyle |= FontStyle.Underline;
            }
            if (Strikeout)
            {
                fontStyle |= FontStyle.Strikeout;
            }
            return new Font(Family, Size, fontStyle, GraphicsUnit.Point);
        }
    }

    class TVertHoriz
    {
        public int Horizontal { get; set; }
        public int Vertical { get; set; }

        public int GetVH(bool vertical)
        {
            return vertical ? Vertical : Horizontal;
        }
    }

    class TBorder
    {
        public Color Colour { get; set; }
        public int Spacing { get; set; }
    }

    class TCursor
    {
        public Color Line { get; set; }
        public TTextAspect Font { get; set; }
        public List<Color> Background { get; set; }
    }
}