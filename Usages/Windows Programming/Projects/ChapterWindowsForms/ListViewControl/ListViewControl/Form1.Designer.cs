namespace ListViewControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelCurrentPath = new System.Windows.Forms.Label();
            this.listViewFilesAndFolders = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTile = new System.Windows.Forms.RadioButton();
            this.radioButtonDetails = new System.Windows.Forms.RadioButton();
            this.radioButtonList = new System.Windows.Forms.RadioButton();
            this.radioButtonSmallIcon = new System.Windows.Forms.RadioButton();
            this.radioButtonLargeIcon = new System.Windows.Forms.RadioButton();
            this.buttonBack = new System.Windows.Forms.Button();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCurrentPath
            // 
            this.labelCurrentPath.Location = new System.Drawing.Point(12, 26);
            this.labelCurrentPath.Name = "labelCurrentPath";
            this.labelCurrentPath.Size = new System.Drawing.Size(391, 27);
            this.labelCurrentPath.TabIndex = 0;
            // 
            // listViewFilesAndFolders
            // 
            this.listViewFilesAndFolders.Location = new System.Drawing.Point(12, 68);
            this.listViewFilesAndFolders.Name = "listViewFilesAndFolders";
            this.listViewFilesAndFolders.Size = new System.Drawing.Size(391, 311);
            this.listViewFilesAndFolders.TabIndex = 1;
            this.listViewFilesAndFolders.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonTile);
            this.groupBox1.Controls.Add(this.radioButtonDetails);
            this.groupBox1.Controls.Add(this.radioButtonList);
            this.groupBox1.Controls.Add(this.radioButtonSmallIcon);
            this.groupBox1.Controls.Add(this.radioButtonLargeIcon);
            this.groupBox1.Location = new System.Drawing.Point(439, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 187);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ViewMode";
            // 
            // radioButtonTile
            // 
            this.radioButtonTile.AutoSize = true;
            this.radioButtonTile.Location = new System.Drawing.Point(19, 115);
            this.radioButtonTile.Name = "radioButtonTile";
            this.radioButtonTile.Size = new System.Drawing.Size(47, 16);
            this.radioButtonTile.TabIndex = 4;
            this.radioButtonTile.TabStop = true;
            this.radioButtonTile.Text = "Tile";
            this.radioButtonTile.UseVisualStyleBackColor = true;
            // 
            // radioButtonDetails
            // 
            this.radioButtonDetails.AutoSize = true;
            this.radioButtonDetails.Checked = true;
            this.radioButtonDetails.Location = new System.Drawing.Point(18, 93);
            this.radioButtonDetails.Name = "radioButtonDetails";
            this.radioButtonDetails.Size = new System.Drawing.Size(65, 16);
            this.radioButtonDetails.TabIndex = 3;
            this.radioButtonDetails.TabStop = true;
            this.radioButtonDetails.Text = "Details";
            this.radioButtonDetails.UseVisualStyleBackColor = true;
            // 
            // radioButtonList
            // 
            this.radioButtonList.AutoSize = true;
            this.radioButtonList.Location = new System.Drawing.Point(18, 71);
            this.radioButtonList.Name = "radioButtonList";
            this.radioButtonList.Size = new System.Drawing.Size(47, 16);
            this.radioButtonList.TabIndex = 2;
            this.radioButtonList.TabStop = true;
            this.radioButtonList.Text = "List";
            this.radioButtonList.UseVisualStyleBackColor = true;
            // 
            // radioButtonSmallIcon
            // 
            this.radioButtonSmallIcon.AutoSize = true;
            this.radioButtonSmallIcon.Location = new System.Drawing.Point(19, 49);
            this.radioButtonSmallIcon.Name = "radioButtonSmallIcon";
            this.radioButtonSmallIcon.Size = new System.Drawing.Size(77, 16);
            this.radioButtonSmallIcon.TabIndex = 1;
            this.radioButtonSmallIcon.TabStop = true;
            this.radioButtonSmallIcon.Text = "SmallIcon";
            this.radioButtonSmallIcon.UseVisualStyleBackColor = true;
            // 
            // radioButtonLargeIcon
            // 
            this.radioButtonLargeIcon.AutoSize = true;
            this.radioButtonLargeIcon.Location = new System.Drawing.Point(18, 27);
            this.radioButtonLargeIcon.Name = "radioButtonLargeIcon";
            this.radioButtonLargeIcon.Size = new System.Drawing.Size(77, 16);
            this.radioButtonLargeIcon.TabIndex = 0;
            this.radioButtonLargeIcon.TabStop = true;
            this.radioButtonLargeIcon.Text = "LargeIcon";
            this.radioButtonLargeIcon.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(238, 407);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(165, 40);
            this.buttonBack.TabIndex = 4;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "Folder 16x16.ICO");
            this.imageListSmall.Images.SetKeyName(1, "Text 16x16.ICO");
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "Folder32x32.ICO");
            this.imageListLarge.Images.SetKeyName(1, "Text 32x32.ICO");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 459);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewFilesAndFolders);
            this.Controls.Add(this.labelCurrentPath);
            this.Name = "Form1";
            this.Text = "ListView";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelCurrentPath;
        private System.Windows.Forms.ListView listViewFilesAndFolders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTile;
        private System.Windows.Forms.RadioButton radioButtonDetails;
        private System.Windows.Forms.RadioButton radioButtonList;
        private System.Windows.Forms.RadioButton radioButtonSmallIcon;
        private System.Windows.Forms.RadioButton radioButtonLargeIcon;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ImageList imageListLarge;
    }
}

