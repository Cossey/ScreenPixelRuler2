using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScreenPixelRuler2
{
    public partial class Options : Form
    {
        private readonly AppConfig AppConfig;
        public Options(ref AppConfig appConfig)
        {
            InitializeComponent();

            AppConfig = appConfig;

            try
            {
                List<Theme> themes = Theming.LoadThemes();
                comboTheme.DisplayMember = "ToString";
                comboTheme.DataSource = themes;

                comboTheme.SelectedItem = Theming.GetThemeByName(themes, appConfig.Theme);
            }
            catch (System.IO.DirectoryNotFoundException) //No Config folder
            {
                comboTheme.Text = Theming.DefaultTheme;
                comboTheme.Enabled = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            AppConfig.Theme = comboTheme.Text;
        }
    }
}
