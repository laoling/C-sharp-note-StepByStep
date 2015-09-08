namespace ListBoxControl
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
            this.checkedListBoxPossibleValues = new System.Windows.Forms.CheckedListBox();
            this.buttonMove = new System.Windows.Forms.Button();
            this.listBoxSelected = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // checkedListBoxPossibleValues
            // 
            this.checkedListBoxPossibleValues.CheckOnClick = true;
            this.checkedListBoxPossibleValues.FormattingEnabled = true;
            this.checkedListBoxPossibleValues.Items.AddRange(new object[] {
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine"});
            this.checkedListBoxPossibleValues.Location = new System.Drawing.Point(27, 19);
            this.checkedListBoxPossibleValues.Name = "checkedListBoxPossibleValues";
            this.checkedListBoxPossibleValues.Size = new System.Drawing.Size(174, 228);
            this.checkedListBoxPossibleValues.TabIndex = 0;
            // 
            // buttonMove
            // 
            this.buttonMove.Location = new System.Drawing.Point(221, 135);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(83, 22);
            this.buttonMove.TabIndex = 1;
            this.buttonMove.Text = "Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // listBoxSelected
            // 
            this.listBoxSelected.FormattingEnabled = true;
            this.listBoxSelected.ItemHeight = 12;
            this.listBoxSelected.Location = new System.Drawing.Point(318, 24);
            this.listBoxSelected.Name = "listBoxSelected";
            this.listBoxSelected.Size = new System.Drawing.Size(154, 220);
            this.listBoxSelected.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 262);
            this.Controls.Add(this.listBoxSelected);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.checkedListBoxPossibleValues);
            this.Name = "Form1";
            this.Text = "Lists";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxPossibleValues;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.ListBox listBoxSelected;
    }
}

