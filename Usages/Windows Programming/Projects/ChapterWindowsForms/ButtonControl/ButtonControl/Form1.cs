using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ButtonControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonEnglish_Click(object sender, EventArgs e)
        {
            Text = "Do you speak English?";
        }

        private void buttonChinese_Click(object sender, EventArgs e)
        {
            Text = "你会说中国话吗？";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
