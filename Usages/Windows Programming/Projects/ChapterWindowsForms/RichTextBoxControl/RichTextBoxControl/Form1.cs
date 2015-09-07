using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RichTextBoxControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBold_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;
            oldFont = this.richTextBoxText.SelectionFont;

            if (oldFont.Bold)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);

            this.richTextBoxText.SelectionFont = newFont;
            this.richTextBoxText.Focus();
        }

        private void buttonItalic_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;
            oldFont = this.richTextBoxText.SelectionFont;

            if (oldFont.Italic)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);

            this.richTextBoxText.SelectionFont = newFont;
            this.richTextBoxText.Focus();
        }

        private void buttonUnderline_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;
            oldFont = this.richTextBoxText.SelectionFont;

            if (oldFont.Underline)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);

            this.richTextBoxText.SelectionFont = newFont;
            this.richTextBoxText.Focus();
        }

        private void buttonCenter_Click(object sender, EventArgs e)
        {
            if (this.richTextBoxText.SelectionAlignment == HorizontalAlignment.Center)
                this.richTextBoxText.SelectionAlignment = HorizontalAlignment.Left;
            else
                this.richTextBoxText.SelectionAlignment = HorizontalAlignment.Center;

            this.richTextBoxText.Focus();
        }

        //设置文本大小
        //为文本框textBoxSize添加两个事件处理程序，
        //一个控制输入
        //另一个控制检测输入完一个值的时间
        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13)
                e.Handled = true;
            else if (e.KeyChar == 13)
            {
                TextBox txt = (TextBox)sender;

                if (txt.Text.Length > 0)
                    ApplyTextSize(txt.Text);

                e.Handled = true;
                this.richTextBoxText.Focus();
            }
        }

        private void textBoxSize_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            ApplyTextSize(txt.Text);
            this.richTextBoxText.Focus();
        }

        private void ApplyTextSize(string textSize)
        {
            float newSize = Convert.ToSingle(textSize);
            FontFamily currentFontFamily;
            Font newFont;

            currentFontFamily = this.richTextBoxText.SelectionFont.FontFamily;
            newFont = new Font(currentFontFamily, newSize);

            this.richTextBoxText.SelectionFont = newFont;        
        }

        //添加富文本中对链接的处理
        //点击链接可以直接打开一个网页，需要添加LinkClicked事件
        private void richTextBoxText_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxText.LoadFile("Test.rtf");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("没有文件可加载");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxText.SaveFile("Test.rtf");
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
