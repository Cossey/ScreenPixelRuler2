using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

            PrimaryClick.DisplayMember = MiddleClick.DisplayMember = X1Click.DisplayMember = X2Click.DisplayMember = "Description";
            PrimaryClick.ValueMember = MiddleClick.ValueMember = X1Click.ValueMember = X2Click.ValueMember = "Value";


            var buttonList = Enum.GetValues(typeof(AppConfig.MouseClick))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            PrimaryClick.DataSource = buttonList;

            MiddleClick.DataSource = buttonList.ToList();
            X1Click.DataSource = buttonList.ToList();
            X2Click.DataSource = buttonList.ToList();

            PrimaryClick.SelectedValue = appConfig.PrimaryClick;
            MiddleClick.SelectedValue = appConfig.MiddleClick;
            X1Click.SelectedValue = appConfig.X1Click;
            X2Click.SelectedValue = appConfig.X2Click;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            AppConfig.Theme = comboTheme.Text;
            AppConfig.PrimaryClick = (AppConfig.MouseClick)PrimaryClick.SelectedValue;
            AppConfig.MiddleClick = (AppConfig.MouseClick)MiddleClick.SelectedValue;
            AppConfig.X1Click = (AppConfig.MouseClick)X1Click.SelectedValue;
            AppConfig.X2Click = (AppConfig.MouseClick)X2Click.SelectedValue;
        }
    }
}
