namespace RichTextBoxControl
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
            this.buttonBold = new System.Windows.Forms.Button();
            this.buttonUnderline = new System.Windows.Forms.Button();
            this.buttonItalic = new System.Windows.Forms.Button();
            this.buttonCenter = new System.Windows.Forms.Button();
            this.labelSize = new System.Windows.Forms.Label();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.richTextBoxText = new System.Windows.Forms.RichTextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBold
            // 
            this.buttonBold.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonBold.Location = new System.Drawing.Point(33, 16);
            this.buttonBold.Name = "buttonBold";
            this.buttonBold.Size = new System.Drawing.Size(95, 30);
            this.buttonBold.TabIndex = 0;
            this.buttonBold.Text = "加粗";
            this.buttonBold.UseVisualStyleBackColor = true;
            this.buttonBold.Click += new System.EventHandler(this.buttonBold_Click);
            // 
            // buttonUnderline
            // 
            this.buttonUnderline.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonUnderline.Location = new System.Drawing.Point(170, 16);
            this.buttonUnderline.Name = "buttonUnderline";
            this.buttonUnderline.Size = new System.Drawing.Size(95, 30);
            this.buttonUnderline.TabIndex = 1;
            this.buttonUnderline.Text = "下划线";
            this.buttonUnderline.UseVisualStyleBackColor = true;
            this.buttonUnderline.Click += new System.EventHandler(this.buttonUnderline_Click);
            // 
            // buttonItalic
            // 
            this.buttonItalic.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonItalic.Location = new System.Drawing.Point(308, 16);
            this.buttonItalic.Name = "buttonItalic";
            this.buttonItalic.Size = new System.Drawing.Size(95, 30);
            this.buttonItalic.TabIndex = 2;
            this.buttonItalic.Text = "斜体";
            this.buttonItalic.UseVisualStyleBackColor = true;
            this.buttonItalic.Click += new System.EventHandler(this.buttonItalic_Click);
            // 
            // buttonCenter
            // 
            this.buttonCenter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCenter.Location = new System.Drawing.Point(450, 16);
            this.buttonCenter.Name = "buttonCenter";
            this.buttonCenter.Size = new System.Drawing.Size(95, 30);
            this.buttonCenter.TabIndex = 3;
            this.buttonCenter.Text = "居中";
            this.buttonCenter.UseVisualStyleBackColor = true;
            this.buttonCenter.Click += new System.EventHandler(this.buttonCenter_Click);
            // 
            // labelSize
            // 
            this.labelSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(223, 67);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(29, 12);
            this.labelSize.TabIndex = 4;
            this.labelSize.Text = "Size";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxSize.Location = new System.Drawing.Point(273, 64);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(129, 21);
            this.textBoxSize.TabIndex = 5;
            this.textBoxSize.Text = "20";
            this.textBoxSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSize_KeyPress);
            this.textBoxSize.Validated += new System.EventHandler(this.textBoxSize_Validated);
            // 
            // richTextBoxText
            // 
            this.richTextBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxText.Location = new System.Drawing.Point(25, 92);
            this.richTextBoxText.Name = "richTextBoxText";
            this.richTextBoxText.Size = new System.Drawing.Size(519, 167);
            this.richTextBoxText.TabIndex = 6;
            this.richTextBoxText.Text = "";
            this.richTextBoxText.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxText_LinkClicked);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonLoad.Location = new System.Drawing.Point(153, 280);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(111, 30);
            this.buttonLoad.TabIndex = 7;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonSave.Location = new System.Drawing.Point(308, 280);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(111, 30);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 333);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.richTextBoxText);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.buttonCenter);
            this.Controls.Add(this.buttonItalic);
            this.Controls.Add(this.buttonUnderline);
            this.Controls.Add(this.buttonBold);
            this.MinimumSize = new System.Drawing.Size(598, 371);
            this.Name = "Form1";
            this.Text = "Simple Text Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBold;
        private System.Windows.Forms.Button buttonUnderline;
        private System.Windows.Forms.Button buttonItalic;
        private System.Windows.Forms.Button buttonCenter;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.RichTextBox richTextBoxText;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
    }
}

