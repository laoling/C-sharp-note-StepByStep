namespace ButtonControl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonEnglish = new System.Windows.Forms.Button();
            this.buttonChinese = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonEnglish
            // 
            this.buttonEnglish.Location = new System.Drawing.Point(20, 23);
            this.buttonEnglish.Name = "buttonEnglish";
            this.buttonEnglish.Size = new System.Drawing.Size(115, 27);
            this.buttonEnglish.TabIndex = 0;
            this.buttonEnglish.Text = "English";
            this.buttonEnglish.UseVisualStyleBackColor = true;
            this.buttonEnglish.Click += new System.EventHandler(this.buttonEnglish_Click);
            // 
            // buttonChinese
            // 
            this.buttonChinese.Location = new System.Drawing.Point(189, 23);
            this.buttonChinese.Name = "buttonChinese";
            this.buttonChinese.Size = new System.Drawing.Size(115, 27);
            this.buttonChinese.TabIndex = 1;
            this.buttonChinese.Text = "Chinese";
            this.buttonChinese.UseVisualStyleBackColor = true;
            this.buttonChinese.Click += new System.EventHandler(this.buttonChinese_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(107, 56);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(115, 27);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 88);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonChinese);
            this.Controls.Add(this.buttonEnglish);
            this.Name = "Form1";
            this.Text = "Do you speak English?";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEnglish;
        private System.Windows.Forms.Button buttonChinese;
        private System.Windows.Forms.Button buttonOK;
    }
}

