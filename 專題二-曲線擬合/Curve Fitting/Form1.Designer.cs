namespace Curve_Fitting
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.explanationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_OutImage = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Lagrange = new System.Windows.Forms.Button();
            this.button_Read = new System.Windows.Forms.Button();
            this.checkBoxShowGrid = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownMagnification = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxLockIntegerPoint = new System.Windows.Forms.CheckBox();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownYdata = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownXdata = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMagnification)).BeginInit();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYdata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXdata)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.explanationToolStripMenuItem,
            this.updateLogToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1231, 27);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readFileToolStripMenuItem,
            this.outputImageToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(112, 23);
            this.menuToolStripMenuItem.Text = "主選單 Menu";
            // 
            // readFileToolStripMenuItem
            // 
            this.readFileToolStripMenuItem.Name = "readFileToolStripMenuItem";
            this.readFileToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.readFileToolStripMenuItem.Text = "讀取檔案 ReadFile";
            this.readFileToolStripMenuItem.Click += new System.EventHandler(this.Read_OpenFileDialog);
            // 
            // outputImageToolStripMenuItem
            // 
            this.outputImageToolStripMenuItem.Name = "outputImageToolStripMenuItem";
            this.outputImageToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.outputImageToolStripMenuItem.Text = "輸出圖片 OutputImage";
            this.outputImageToolStripMenuItem.Click += new System.EventHandler(this.Write_OpenFileDialog);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(246, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.exitToolStripMenuItem.Text = "結束 Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // explanationToolStripMenuItem
            // 
            this.explanationToolStripMenuItem.Name = "explanationToolStripMenuItem";
            this.explanationToolStripMenuItem.Size = new System.Drawing.Size(137, 23);
            this.explanationToolStripMenuItem.Text = "說明 Explanation";
            this.explanationToolStripMenuItem.Click += new System.EventHandler(this.explanationToolStripMenuItem_Click);
            // 
            // updateLogToolStripMenuItem
            // 
            this.updateLogToolStripMenuItem.Name = "updateLogToolStripMenuItem";
            this.updateLogToolStripMenuItem.Size = new System.Drawing.Size(165, 23);
            this.updateLogToolStripMenuItem.Text = "更新日誌 Update log";
            this.updateLogToolStripMenuItem.Click += new System.EventHandler(this.updateLogToolStripMenuItem_Click);
            // 
            // button_OutImage
            // 
            this.button_OutImage.Location = new System.Drawing.Point(15, 125);
            this.button_OutImage.Margin = new System.Windows.Forms.Padding(4);
            this.button_OutImage.Name = "button_OutImage";
            this.button_OutImage.Size = new System.Drawing.Size(100, 29);
            this.button_OutImage.TabIndex = 14;
            this.button_OutImage.Text = "輸出圖片";
            this.button_OutImage.UseVisualStyleBackColor = true;
            this.button_OutImage.Click += new System.EventHandler(this.Write_OpenFileDialog);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 31);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(837, 482);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(15, 88);
            this.button_Clear.Margin = new System.Windows.Forms.Padding(4);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(100, 29);
            this.button_Clear.TabIndex = 12;
            this.button_Clear.Text = "清除螢幕";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.DrawClear);
            // 
            // button_Lagrange
            // 
            this.button_Lagrange.Location = new System.Drawing.Point(15, 51);
            this.button_Lagrange.Margin = new System.Windows.Forms.Padding(4);
            this.button_Lagrange.Name = "button_Lagrange";
            this.button_Lagrange.Size = new System.Drawing.Size(100, 29);
            this.button_Lagrange.TabIndex = 11;
            this.button_Lagrange.Text = "執行內插";
            this.button_Lagrange.UseVisualStyleBackColor = true;
            this.button_Lagrange.Click += new System.EventHandler(this.LagrangeInterpolation);
            // 
            // button_Read
            // 
            this.button_Read.Location = new System.Drawing.Point(15, 41);
            this.button_Read.Margin = new System.Windows.Forms.Padding(4);
            this.button_Read.Name = "button_Read";
            this.button_Read.Size = new System.Drawing.Size(100, 29);
            this.button_Read.TabIndex = 10;
            this.button_Read.Text = "讀檔";
            this.button_Read.UseVisualStyleBackColor = true;
            this.button_Read.Click += new System.EventHandler(this.Read_OpenFileDialog);
            // 
            // checkBoxShowGrid
            // 
            this.checkBoxShowGrid.AutoSize = true;
            this.checkBoxShowGrid.Checked = true;
            this.checkBoxShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowGrid.Location = new System.Drawing.Point(130, 57);
            this.checkBoxShowGrid.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxShowGrid.Name = "checkBoxShowGrid";
            this.checkBoxShowGrid.Size = new System.Drawing.Size(89, 19);
            this.checkBoxShowGrid.TabIndex = 9;
            this.checkBoxShowGrid.Text = "顯示格線";
            this.checkBoxShowGrid.UseVisualStyleBackColor = true;
            this.checkBoxShowGrid.CheckedChanged += new System.EventHandler(this.DrawGrid);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.start_clear);
            // 
            // numericUpDownMagnification
            // 
            this.numericUpDownMagnification.Location = new System.Drawing.Point(223, 93);
            this.numericUpDownMagnification.Name = "numericUpDownMagnification";
            this.numericUpDownMagnification.Size = new System.Drawing.Size(120, 25);
            this.numericUpDownMagnification.TabIndex = 15;
            this.numericUpDownMagnification.ValueChanged += new System.EventHandler(this.numericUpDownMagnification_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "放大倍率:";
            // 
            // checkBoxLockIntegerPoint
            // 
            this.checkBoxLockIntegerPoint.AutoSize = true;
            this.checkBoxLockIntegerPoint.Checked = true;
            this.checkBoxLockIntegerPoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLockIntegerPoint.Location = new System.Drawing.Point(130, 47);
            this.checkBoxLockIntegerPoint.Name = "checkBoxLockIntegerPoint";
            this.checkBoxLockIntegerPoint.Size = new System.Drawing.Size(104, 19);
            this.checkBoxLockIntegerPoint.TabIndex = 17;
            this.checkBoxLockIntegerPoint.Text = "鎖定整數點";
            this.checkBoxLockIntegerPoint.UseVisualStyleBackColor = true;
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.button_Read);
            this.groupBoxInput.Controls.Add(this.checkBoxLockIntegerPoint);
            this.groupBoxInput.Location = new System.Drawing.Point(858, 31);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(361, 239);
            this.groupBoxInput.TabIndex = 18;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "輸入 Input";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.label3);
            this.groupBoxOutput.Controls.Add(this.label2);
            this.groupBoxOutput.Controls.Add(this.numericUpDownYdata);
            this.groupBoxOutput.Controls.Add(this.numericUpDownXdata);
            this.groupBoxOutput.Controls.Add(this.button_OutImage);
            this.groupBoxOutput.Controls.Add(this.numericUpDownMagnification);
            this.groupBoxOutput.Controls.Add(this.checkBoxShowGrid);
            this.groupBoxOutput.Controls.Add(this.button_Lagrange);
            this.groupBoxOutput.Controls.Add(this.button_Clear);
            this.groupBoxOutput.Controls.Add(this.label1);
            this.groupBoxOutput.Location = new System.Drawing.Point(858, 276);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(361, 237);
            this.groupBoxOutput.TabIndex = 19;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "輸出 Output";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "基礎 y 座標:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "基礎 x 座標:";
            // 
            // numericUpDownYdata
            // 
            this.numericUpDownYdata.Location = new System.Drawing.Point(223, 160);
            this.numericUpDownYdata.Name = "numericUpDownYdata";
            this.numericUpDownYdata.Size = new System.Drawing.Size(120, 25);
            this.numericUpDownYdata.TabIndex = 18;
            this.numericUpDownYdata.ValueChanged += new System.EventHandler(this.numericUpDownYdata_ValueChanged);
            // 
            // numericUpDownXdata
            // 
            this.numericUpDownXdata.Location = new System.Drawing.Point(223, 128);
            this.numericUpDownXdata.Name = "numericUpDownXdata";
            this.numericUpDownXdata.Size = new System.Drawing.Size(120, 25);
            this.numericUpDownXdata.TabIndex = 17;
            this.numericUpDownXdata.ValueChanged += new System.EventHandler(this.numericUpDownXdata_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 524);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "1600,800 利用Lagrange內插進行曲線擬合";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMagnification)).EndInit();
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYdata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXdata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem explanationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateLogToolStripMenuItem;
        private System.Windows.Forms.Button button_OutImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button button_Lagrange;
        private System.Windows.Forms.Button button_Read;
        private System.Windows.Forms.CheckBox checkBoxShowGrid;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDownMagnification;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxLockIntegerPoint;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownYdata;
        private System.Windows.Forms.NumericUpDown numericUpDownXdata;
    }
}

