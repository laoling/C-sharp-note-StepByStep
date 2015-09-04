using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //这几个事件加的没什么意义，后面我们再重新调试
        private void label1_Click(object sender, EventArgs e)
        {
            Text = "设置外边框成功";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Text = "设置显示方式成功";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //这里不了解点击打开网页的属性和事件，这里也简单写个文本替换
            Text = "这里的链接进不去";
        }

    }
}
