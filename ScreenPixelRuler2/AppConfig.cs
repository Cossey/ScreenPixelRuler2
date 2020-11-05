using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ScreenPixelRuler2
{
    public class AppConfig
    {
        public CPoint Position { get; set; }
        [DefaultValue(false)]
        public bool Vertical { get; set; }
        [DefaultValue(false)]
        public bool Direction { get; set; }
        [DefaultValue(Theming.DefaultTheme)]
        public string Theme { get; set; }
        [DefaultValue(true)]
        public bool ClickToRotate { get; set; }

        public static string AppLocation()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static bool IsPackageDeployed()
        {
            //Current directory
            return File.Exists(string.Format(@"{0}\package.yes", AppLocation()));
        }

        [YamlIgnore]
        const string ConfigFileName = "app.cfg";

        public static AppConfig LoadConfig()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configPath = string.Format(@"{0}\screenpixelruler\{1}", userPath, ConfigFileName);

            if (IsPackageDeployed())
            {
                configPath = string.Format(@"{0}\{1}", AppLocation(), ConfigFileName);
            }

            AppConfig def = new AppConfig
            {
                Vertical = false,
                Position = new CPoint
                {
                    X = 100,
                    Y = 100
                },
                Theme = Theming.DefaultTheme
            };

            try
            {
                using (StreamReader reader = new StreamReader(File.Open(configPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    IDeserializer deserializer = new DeserializerBuilder()
                        .WithNamingConvention(PascalCaseNamingConvention.Instance)
                        .IgnoreUnmatchedProperties()
                        .Build();
                    return deserializer.Deserialize<AppConfig>(reader);
                }
            }
            catch (YamlDotNet.Core.SemanticErrorException)
            {
                File.Delete(configPath);
                MessageBox.Show("The configuration file is not valid. Loading Default Configuration.", "Screen Pixel Ruler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return def;
            }
            catch (FileNotFoundException)
            {
                return def;
            }
            catch (DirectoryNotFoundException) //Create directory if does not exist
            {
                Directory.CreateDirectory(string.Format(@"{0}\screenpixelruler", userPath));
                return def;
            }
        }

        public static void SaveConfig(AppConfig appConfig)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configPath = userPath + @"\screenpixelruler";

            Directory.CreateDirectory(configPath); //Create folders if they do not exist

            configPath += string.Format(@"\{0}", ConfigFileName);

            if (IsPackageDeployed())
            {
                configPath = string.Format(@"{0}\{1}", AppLocation(), ConfigFileName);
            }

            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            using (StreamWriter writer = new StreamWriter(File.Open(configPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
            {

                //writer.Write(JsonSerializer.Serialize<AppConfig>(appConfig));
                //yml.Serialize(writer, appConfig);
                serializer.Serialize(writer, appConfig, typeof(AppConfig));
            }
        }
    }

    public class CPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point Point()
        {
            return new Point(X < 0 ? 0 : X, Y < 0 ? 0 : Y);
        }

        public void Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
    }
}
