using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextBoxControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //按钮属性设置为false，这样输入完信息之前按钮都不会生效
            buttonOK.Enabled = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //定义输入字符串
            string output;

            //获取四个TextBoxes的Value值
            output = "Name: " + this.textBoxName.Text + "\r\n";
            output += "Address: " + this.textBoxAddress.Text + "\r\n";
            output += "Occupation: " + this.textBoxOccupation.Text + "\r\n";
            output += "Age: " + this.textBoxAge.Text + "\r\n";

            //将output输出到text属性中
            this.textBoxOutput.Text = output;
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            //定义输入字符串，这里我们将一个简短描述输出到output中
            string output;

            //这里写四行简短描述，描述各栏的用途
            output = "Name: 输入名字\r\n";
            output += "Address: 输入地址\r\n";
            output += "Occupation: 这里只能输入‘程序员’\r\n";
            output += "Age: 输入年龄\r\n";

            this.textBoxOutput.Text = output;
        }

        private void textBoxEmpty_Validating(object sender, CancelEventArgs e)
        {
            //因为有多个文本框使用了这个方法来处理事件，所以我们不知道那个控件调用了函数，
            //但无论哪个使用，其结果是一样的，所以对传递给文本框的sender参数进行转换。
            TextBox tb = (TextBox)sender;

            //如果文本框为空，文本框背景色设为红色；否则保持系统设置的颜色
            if (tb.Text.Length == 0)
                tb.BackColor = Color.Red;
            else
                tb.BackColor = System.Drawing.SystemColors.Window;

            ValidateOK();
        }

        private void textBoxAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0~9的ASCII值是48~57，8是退格键
            //Handled属性设置为true，表示按键时对此动作跳过，不做任何处理
            //达到的效果：只能输入删除数字
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBoxOccupation_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            //检查填入的是否程序员
            if (tb.Text == "程序员")
                tb.BackColor = System.Drawing.SystemColors.Window;
            else
                tb.BackColor = Color.Red;

            ValidateOK();
        }

        private void ValidateOK()
        { 
            this.buttonOK.Enabled = (textBoxName.BackColor != Color.Red &&
                                     textBoxAddress.BackColor != Color.Red &&
                                     textBoxAge.BackColor != Color.Red &&
                                     textBoxOccupation.BackColor != Color.Red);
        }
    }
}
