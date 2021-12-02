using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScreenPixelRuler2.Forms
{
    public partial class Guidelines : Form
    {
        public Guidelines(List<int> guidelines)
        {
            InitializeComponent();

            NumberBox.Controls[0].Visible = false;
            NumberBox.ResetText();

            guidelines.Sort();

            guidelines.ForEach(each =>
            {
                GuidelineList.Items.Add(each);
            });

            SortListBox();

            this.Shown += Guidelines_Shown;
        }

        private void SortListBox()
        {
            ArrayList temp = new ArrayList();
            foreach (object o in GuidelineList.Items)
            {
                temp.Add(o);
            }
            temp.Sort();
            GuidelineList.Items.Clear();
            foreach (object o in temp)
            {
                GuidelineList.Items.Add(o);
            }
        }

        private void Guidelines_Shown(object sender, EventArgs e)
        {
            NumberBox.Focus();
        }

        public new List<int> ShowDialog()
        {
            base.ShowDialog();
            List<int> guidelines = new List<int>();
            foreach (int item in GuidelineList.Items)
            {
                guidelines.Add(item);
            }

            return guidelines;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            GuidelineList.Items.Remove((int)GuidelineList.SelectedItem);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            GuidelineList.Items.Add((int)NumberBox.Value);
            NumberBox.ResetText();
            SortListBox();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            GuidelineList.Items.Clear();
        }

        private void NumberBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AddButton_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
