namespace WallpaperDownloaderForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            numericUpDown1 = new NumericUpDown();
            contextMenuStrip1 = new ContextMenuStrip(components);
            下载1080PToolStripMenuItem = new ToolStripMenuItem();
            下载4KToolStripMenuItem = new ToolStripMenuItem();
            下载OriginalToolStripMenuItem = new ToolStripMenuItem();
            保存到D盘根目录ToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(79, 25);
            label1.Name = "label1";
            label1.Size = new Size(184, 31);
            label1.TabIndex = 0;
            label1.Text = "天前 | 选择日期:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(269, 26);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(174, 32);
            comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.ForeColor = Color.White;
            label2.Location = new Point(450, 23);
            label2.Name = "label2";
            label2.Size = new Size(94, 36);
            label2.TabIndex = 2;
            label2.Text = "label2";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(12, 26);
            numericUpDown1.Maximum = new decimal(new int[] { 7, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(61, 30);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 下载1080PToolStripMenuItem, 下载4KToolStripMenuItem, 下载OriginalToolStripMenuItem, 保存到D盘根目录ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(221, 124);
            // 
            // 下载1080PToolStripMenuItem
            // 
            下载1080PToolStripMenuItem.Name = "下载1080PToolStripMenuItem";
            下载1080PToolStripMenuItem.Size = new Size(220, 30);
            下载1080PToolStripMenuItem.Text = "下载(1080P)";
            下载1080PToolStripMenuItem.Click += 下载1080PToolStripMenuItem_Click;
            // 
            // 下载4KToolStripMenuItem
            // 
            下载4KToolStripMenuItem.Name = "下载4KToolStripMenuItem";
            下载4KToolStripMenuItem.Size = new Size(220, 30);
            下载4KToolStripMenuItem.Text = "下载(4K)";
            下载4KToolStripMenuItem.Click += 下载4KToolStripMenuItem_Click;
            // 
            // 下载OriginalToolStripMenuItem
            // 
            下载OriginalToolStripMenuItem.Name = "下载OriginalToolStripMenuItem";
            下载OriginalToolStripMenuItem.Size = new Size(220, 30);
            下载OriginalToolStripMenuItem.Text = "下载(Original)";
            下载OriginalToolStripMenuItem.Click += 下载OriginalToolStripMenuItem_Click;
            // 
            // 保存到D盘根目录ToolStripMenuItem
            // 
            保存到D盘根目录ToolStripMenuItem.Name = "保存到D盘根目录ToolStripMenuItem";
            保存到D盘根目录ToolStripMenuItem.Size = new Size(220, 30);
            保存到D盘根目录ToolStripMenuItem.Text = "保存到D盘根目录";
            保存到D盘根目录ToolStripMenuItem.Click += 保存到D盘根目录ToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1686, 1061);
            ContextMenuStrip = contextMenuStrip1;
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 下载1080PToolStripMenuItem;
        private ToolStripMenuItem 下载4KToolStripMenuItem;
        private ToolStripMenuItem 下载OriginalToolStripMenuItem;
        private ToolStripMenuItem 保存到D盘根目录ToolStripMenuItem;
        private NumericUpDown numericUpDown1;
    }
}
