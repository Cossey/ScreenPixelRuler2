using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using YamlDotNet.Serialization;

namespace ScreenPixelRuler2
{
    class Theme
    {
        public Theme()
        {
            Name = Theming.DefaultTheme;
            Cursor = new TCursor
            {
                Font = new TTextAspect
                {
                    Colour = Color.White,
                    Padding = new TPadding
                    {
                        Horizontal = 2,
                        Vertical = 2
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
                Lines = Color.Black,
                Numbers = new TTextAspect
                {
                     Colour = Color.Black,
                     Padding = new TPadding
                     {
                         Horizontal = 3,
                         Vertical = 2
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

        public Pen GetLinesPen()
        {
            return new Pen(Ruler.Lines, 1);
        }

        public Brush GetNumberBrush()
        {
            return new SolidBrush(Ruler.Numbers.Colour);
        }

        public int GetNumberPadding(bool vertical)
        {
            return vertical ? Ruler.Numbers.Padding.Vertical : Ruler.Numbers.Padding.Horizontal;
        }

        public Pen GetBorderPen()
        {
            return new Pen(Ruler.Border.Colour, 1);
        }

        public int GetBorderSpacing()
        {
            return Ruler != null && Ruler.Border != null ? Ruler.Border.Spacing : 15;
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
            return Ruler != null && Ruler.Size > 20 && Ruler.Size <= 100 ? Ruler.Size : 40;
        }
    }

    class TRuler
    {
        public int Size { get; set; }
        public Color Lines { get; set; }
        public TTextAspect Numbers { get; set; }
        public TBorder Border { get; set; }
        public List<Color> Background { get; set; }
    }

    class TTextAspect
    {
        public TPadding Padding { get; set; }
        public Color Colour { get; set; }
        public TFont Font { get; set; }
    }

    class TFont
    {
        [DefaultValue("Courier New")]
        public string Family { get; set; }
        [DefaultValue(9)]
        public int Size { get; set; }
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
            return new Font(Family, Size, fontStyle);
        }
    }

    class TPadding
    {
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
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