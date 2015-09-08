using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListBoxControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            checkedListBoxPossibleValues.Items.Add("Ten");
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            if (checkedListBoxPossibleValues.CheckedItems.Count > 0)
            {
                listBoxSelected.Items.Clear();
                foreach (string item in checkedListBoxPossibleValues.CheckedItems)
                {
                    listBoxSelected.Items.Add(item.ToString());
                }
                for (int i = 0; i < checkedListBoxPossibleValues.Items.Count; i++)
                    checkedListBoxPossibleValues.SetItemChecked(i, false);
            }
        }
    }
}
