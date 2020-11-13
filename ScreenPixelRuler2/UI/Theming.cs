using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ScreenPixelRuler2
{

    static class Theming
    {
        public const string DefaultTheme = "<Default>";
        public static List<Theme> LoadThemes()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            List<Theme> themes = new List<Theme>();
            DirectoryInfo directory = new DirectoryInfo(userPath + @"\screenpixelruler");

            if (AppConfig.IsPackageDeployed())
            {
                directory = new DirectoryInfo(AppConfig.AppLocation());
            }

            FileInfo[] themeFiles = directory.GetFiles("*.thm");
            themes.Add(new Theme()); //Add Default Theme
            themeFiles.ToList().ForEach(each =>
            {
                try
                {
                    Theme theme = LoadTheme(each.FullName);
                    themes.Add(theme);
                }
                catch
                {
                    MessageBox.Show(string.Format("Could not load theme file \"{0}\".", each.Name), "Theme Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            return themes;
        }

        public static Theme GetThemeByName(List<Theme> themes, string name)
        {
            if (name.Equals(DefaultTheme))
            {
                return new Theme();
            }
            Theme theme = themes.Find(m => m.Name.Equals(name, StringComparison.Ordinal));
            if (theme == null)
            {
                MessageBox.Show(string.Format("The theme \"{0}\" could not be found. Loading default theme.", name), "Theme Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                theme = new Theme();
            }
            return theme;
        }

        public static Theme GetThemeByName(string name)
        {
            List<Theme> themes = LoadThemes();
            return GetThemeByName(themes, name);
        }

        public static Theme LoadTheme(string filePath)
        {
            using (StreamReader reader = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(PascalCaseNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();
                Theme theme = deserializer.Deserialize<Theme>(reader);
                theme.Path = filePath;
                return theme;
            }
        }
    }
}
