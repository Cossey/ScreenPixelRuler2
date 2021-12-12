using System;
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
            bool dark = IsDark();
            Color highlight = W10Accent().IsEmpty ? (dark ? Color.White : Color.Black) : W10Accent();
            Color tintHighlight = W10Accent(3).IsEmpty ? Color.Red : W10Accent(4);

            Name = Theming.DefaultTheme;
            Cursor = new TCursor
            {
                Font = new TTextAspect
                {
                    Colour = dark ? Color.Black : Color.White,
                    Padding = new TVertHoriz
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
                Line = highlight,
                Background = new List<Color>
                {
                    highlight
                },
                Frozen = new TCursorAspect
                {
                    Background = new List<Color>
                    {
                        tintHighlight
                    },
                    Line = tintHighlight
                }
            };
            Ruler = new TRuler
            {
                Background = new List<Color>
                {
                    dark ? Color.Black : Color.White
                },
                Size = 40,
                Marks = new TLines
                {
                    Zero = new TZero
                    {
                        Size = new TVertHoriz
                        {
                            Horizontal = 20,
                            Vertical = 20
                        },
                        NumberVisible = true
                    },
                    Colour = dark ? Color.White : Color.Black,
                    Size = new TVertHoriz
                    {
                        Horizontal = 8,
                        Vertical = 8
                    },
                    Sizes = new List<TLineSizes>
                    {
                        new TLineSizes
                        {
                            Colour = dark ? Color.White : Color.Black,
                            Interval = 10,
                            Size = new TVertHoriz
                            {
                                 Horizontal = 13,
                                 Vertical = 13
                            }
                        },
                        new TLineSizes
                        {
                            Colour = dark ? Color.White : Color.Black,
                            Interval = 50,
                            Size = new TVertHoriz
                            {
                                Horizontal = 20,
                                Vertical = 20
                            }
                        }
                    }
                },
                Numbers = new TNumbers
                {
                    Colour = dark ? Color.White : Color.Black,
                    Padding = new TVertHoriz
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
                    },
                    Display = new TNumberDisplay
                    {
                        Interval = 50,
                        Vertical = new TNumberDisplayArrangement
                        {
                            Rotate = true,
                            Alignment = StringAlignment.Center
                        },
                        Horizontal = new TNumberDisplayArrangement
                        {
                            Rotate = false,
                            Alignment = StringAlignment.Center
                        }
                    },
                    OppositeOffsetPadding = new TVertHoriz
                    {
                        Horizontal = 0,
                        Vertical = 2
                    }
                },
                Border = new TBorder
                {
                    Spacing = 15,
                    Colour = dark ? Color.White : Color.Black
                },
                Guidelines = new TGuidelines
                {
                    Nearest = new SizeAndColour
                    {
                        Colour = highlight
                    },
                    Guideline = new SizeAndColour
                    {
                        Colour = tintHighlight
                    }
                }
            };
        }

        private bool IsDark()
        {
            string appsUseLightThemePath = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            string appsUseDarkThemeKey = "AppsUseLightTheme";
            int value = (int)Microsoft.Win32.Registry.GetValue(appsUseLightThemePath, appsUseDarkThemeKey, 1);
            return value == 0 ? true : false;
        }

        private Color W10Accent(int index = 0)
        {
            string accentPath = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent";
            string key = "AccentColorMenu";
            byte[] rawHex = new byte[4];
            int textColour;
            int red;
            int green;
            int blue;

            if (index != 0)
            {
                key = "AccentPalette";
                byte[] vals = (byte[])Microsoft.Win32.Registry.GetValue(accentPath, key, Color.Empty);
                Buffer.BlockCopy(vals, (index - 1) * 4, rawHex, 0, 4);
                textColour = (int)rawHex[3];
                red = (int)rawHex[0];
                green = (int)rawHex[1];
                blue = (int)rawHex[2];
            }
            else
            {
                rawHex = BitConverter.GetBytes((int)Microsoft.Win32.Registry.GetValue(accentPath, key, Color.Empty));
                textColour = (int)rawHex[3];
                red = (int)rawHex[0];
                green = (int)rawHex[1];
                blue = (int)rawHex[2];
            }

            //Will return Empty Colour if no Accent specified or if non-W10
            return Color.FromArgb(red, green, blue);
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

        #region "Shortcut Functions"

        public int GetGuidelineSize(bool vertical, bool locked, bool nearest)
        {
            int size = Ruler?.Guidelines?.Guideline?.Size?.GetVH(vertical) ?? Ruler.Size;
            if (nearest)
            {
                size = Ruler?.Guidelines?.Nearest?.Size?.GetVH(vertical) ?? size;
            }
            if (locked)
            {
                size = Ruler?.Guidelines?.Locked?.Size?.GetVH(vertical) ?? size;
            }
            return size;
        }
        public Pen GetGuidelinePen(bool locked, bool nearest)
        {
            Color penColour = Ruler?.Guidelines?.Guideline?.Colour.IsEmpty ?? true ? Ruler.Marks.Colour : Ruler.Guidelines.Guideline.Colour;
            if (nearest)
            {
                penColour = Ruler?.Guidelines?.Nearest?.Colour.IsEmpty ?? true ? penColour : Ruler.Guidelines.Nearest.Colour;
            }
            if (locked)
            {
                penColour = Ruler?.Guidelines?.Locked?.Colour.IsEmpty ?? true ? penColour : Ruler.Guidelines.Locked.Colour;
            }
            return new Pen(penColour, 1);
        }

        public Brush GetBackgroundBrush(Rectangle clientArea, bool verticality, bool direction)
        {
            if (Ruler.Background.Count == 0)
            {
                return new SolidBrush(Color.White);
            }
            else if (Ruler.Background.Count > 1)
            {
                return new LinearGradientBrush(clientArea, direction ? Ruler.Background[0] : Ruler.Background[1], direction ? Ruler.Background[1] : Ruler.Background[0], verticality ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical);
            }
            else
            {
                return new SolidBrush(Ruler.Background[0]);
            }
        }

        public Pen GetLinesPen(int count)
        {
            List<TLineSizes> sorted = Ruler?.Marks?.Sizes?.OrderByDescending(o => o.Interval).ToList();
            if (sorted == null) return null;
            TLineSizes lineSize = sorted.Find(f => count % f.Interval == GetBorderSpacing());

            if (lineSize != null)
            {
                return new Pen(lineSize.Colour.IsEmpty ? Ruler.Marks.Colour : lineSize.Colour, 1);
            }
            else
            {
                return new Pen(Ruler.Marks.Colour, 1);
            }
        }

        public int GetLinesSize(int count, bool vertical)
        {
            List<TLineSizes> sorted = Ruler?.Marks?.Sizes?.OrderByDescending(o => o.Interval).ToList();
            if (sorted == null) return 0;
            TLineSizes lineSize = sorted.Find(f => (count - GetBorderSpacing()) % f.Interval == 0);

            if (lineSize != null)
            {
                return lineSize.Size?.GetVH(vertical) ?? Ruler.Marks?.Size?.GetVH(vertical) ?? 0;
            }
            return Ruler.Marks.Size.GetVH(vertical);
        }

        public bool GetShowNumberZero()
        {
            return Ruler?.Marks?.Zero?.NumberVisible ?? false;
        }

        public Brush GetNumberBrush()
        {
            return new SolidBrush(Ruler.Numbers.Colour);
        }

        public int GetNumberPadding(bool vertical, bool direction)
        {
            return (Ruler?.Numbers?.Padding?.GetVH(vertical) ?? 0) + (direction ? Ruler?.Numbers?.OppositeOffsetPadding?.GetVH(vertical) ?? 0 : 0);
        }

        public Pen GetZeroPen()
        {
            return new Pen(Ruler?.Marks?.Zero?.Colour.IsEmpty ?? true ? Ruler?.Marks?.Colour ?? Color.Transparent : Ruler.Marks.Zero.Colour, 1);
        }

        public int GetZeroLineSize(bool vertical)
        {
            return Ruler?.Marks?.Zero?.Size?.GetVH(vertical) ?? Ruler?.Marks?.Size?.GetVH(vertical) ?? 0;
        }

        public int GetBorderSpacing()
        {
            return Ruler?.Border?.Spacing ?? 15;
        }

        public Color GetBorderColour()
        {
            return Ruler?.Border?.Colour.IsEmpty ?? true ? Color.Transparent : Ruler.Border.Colour;
        }

        public Pen GetCursorLinePen(bool frozen, bool locked)
        {
            Color col = Cursor?.Line ?? Color.Transparent;
            if (locked)
            {
                col = Cursor?.Locked?.Line ?? Cursor?.Frozen?.Line ?? Cursor?.Line ?? Color.Transparent;
            }
            if (frozen)
            {
                col = Cursor?.Frozen?.Line ?? Cursor?.Line ?? Color.Transparent;
            }
            return new Pen(col, 1);
        }

        public Brush GetCursorFontBrush(bool frozen, bool locked)
        {
            Color col = Cursor?.Font?.Colour ?? Color.Transparent;
            if (locked)
            {
                col = Cursor?.Locked?.Font?.Colour ?? Cursor?.Frozen?.Font?.Colour ?? Cursor?.Font?.Colour ?? Color.Transparent;
            }
            if (frozen)
            {
                col = Cursor?.Frozen?.Font?.Colour ?? Cursor?.Font?.Colour ?? Color.Transparent;
            }
            return new SolidBrush(col);
        }

        public Brush GetCursorBackground(Rectangle clientArea, bool verticality, bool direction, bool frozen, bool locked)
        {
            List<Color> colors = Cursor.Background;
            if (locked)
            {
                colors = Cursor?.Locked?.Background ?? Cursor?.Frozen?.Background ?? Cursor?.Background;
            }
            if (frozen)
            {
                colors = Cursor?.Frozen?.Background ?? Cursor?.Background;
            }
            if (colors.Count == 0)
            {
                return new SolidBrush(Color.Transparent);
            }
            else if (colors.Count > 1)
            {
                return new LinearGradientBrush(clientArea, direction ? colors[0] : colors[1], direction ? colors[1] : colors[0], verticality ? LinearGradientMode.Horizontal : LinearGradientMode.Vertical);
            }
            else
            {
                return new SolidBrush(colors[0]);
            }
        }

        public int GetRulerSize()
        {
            int size = Ruler?.Size ?? 40;
            return Ruler != null && size >= minSize && size <= maxSize ? size : (size < minSize ? minSize : maxSize);
        }

        #endregion

    }

    class TNumberDisplay
    {
        public int Interval { get; set; }
 
        public TNumberDisplayArrangement Vertical { get; set; }

        public TNumberDisplayArrangement Horizontal { get; set; }
    }

    class TNumberDisplayArrangement
    {
        [DefaultValue(false)]
        public bool Rotate { get; set; }
        [DefaultValue(StringAlignment.Center)]
        public StringAlignment Alignment { get; set; }
    }

    class TRuler
    {
        public int Size { get; set; }
        public TLines Marks { get; set; }
        public TNumbers Numbers { get; set; }
        public TBorder Border { get; set; }
        public List<Color> Background { get; set; }

        public TGuidelines Guidelines { get; set; }
    }

    class TGuidelines
    {
        public SizeAndColour Guideline { get; set; }
        public SizeAndColour Locked { get; set; }
        public SizeAndColour Nearest { get; set; }
    }

    class SizeAndColour
    {
        public Color Colour { get; set; }
        public TVertHoriz Size { get; set; }
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

        public TVertHoriz OppositeOffsetPadding { get; set; }

        public TVertHoriz Offset { get; set; }
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

    class TCursorAspect 
    {
        public Color Line { get; set; }
        public TTextAspect Font { get; set; }
        public List<Color> Background { get; set; }
    }

    class TCursor : TCursorAspect
    {
        public TCursorAspect Frozen { get; set; }
        public TCursorAspect Locked { get; set; }
    }
}