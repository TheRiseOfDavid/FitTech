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
            this.button_OutPolynomial = new System.Windows.Forms.Button();
            this.checkBoxBlueLine = new System.Windows.Forms.CheckBox();
            this.checkBoxGreenLine = new System.Windows.Forms.CheckBox();
            this.checkBoxRedLine = new System.Windows.Forms.CheckBox();
            this.checkBoxYellowLine = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownYdata = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownXdata = new System.Windows.Forms.NumericUpDown();
            this.labelYellowMessage = new System.Windows.Forms.Label();
            this.labelRedMessage = new System.Windows.Forms.Label();
            this.labelGreenMessage = new System.Windows.Forms.Label();
            this.labelBlueMessage = new System.Windows.Forms.Label();
            this.labelYellow = new System.Windows.Forms.Label();
            this.labelRed = new System.Windows.Forms.Label();
            this.labelGreen = new System.Windows.Forms.Label();
            this.labelBlue = new System.Windows.Forms.Label();
            this.button_OutPoint = new System.Windows.Forms.Button();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1422, 29);
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
            this.button_OutImage.Location = new System.Drawing.Point(19, 139);
            this.button_OutImage.Margin = new System.Windows.Forms.Padding(5);
            this.button_OutImage.Name = "button_OutImage";
            this.button_OutImage.Size = new System.Drawing.Size(143, 39);
            this.button_OutImage.TabIndex = 14;
            this.button_OutImage.Text = "輸出圖片";
            this.button_OutImage.UseVisualStyleBackColor = true;
            this.button_OutImage.Click += new System.EventHandler(this.Write_OpenFileDialog);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 41);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1046, 554);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(19, 93);
            this.button_Clear.Margin = new System.Windows.Forms.Padding(5);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(143, 39);
            this.button_Clear.TabIndex = 12;
            this.button_Clear.Text = "清除螢幕";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.DrawClear);
            // 
            // button_Lagrange
            // 
            this.button_Lagrange.Location = new System.Drawing.Point(19, 47);
            this.button_Lagrange.Margin = new System.Windows.Forms.Padding(5);
            this.button_Lagrange.Name = "button_Lagrange";
            this.button_Lagrange.Size = new System.Drawing.Size(143, 39);
            this.button_Lagrange.TabIndex = 11;
            this.button_Lagrange.Text = "執行內插";
            this.button_Lagrange.UseVisualStyleBackColor = true;
            this.button_Lagrange.Click += new System.EventHandler(this.LagrangeInterpolation);
            // 
            // button_Read
            // 
            this.button_Read.Location = new System.Drawing.Point(19, 55);
            this.button_Read.Margin = new System.Windows.Forms.Padding(5);
            this.button_Read.Name = "button_Read";
            this.button_Read.Size = new System.Drawing.Size(125, 39);
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
            this.checkBoxShowGrid.Location = new System.Drawing.Point(182, 72);
            this.checkBoxShowGrid.Margin = new System.Windows.Forms.Padding(5);
            this.checkBoxShowGrid.Name = "checkBoxShowGrid";
            this.checkBoxShowGrid.Size = new System.Drawing.Size(111, 24);
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
            this.numericUpDownMagnification.Location = new System.Drawing.Point(146, 301);
            this.numericUpDownMagnification.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownMagnification.Name = "numericUpDownMagnification";
            this.numericUpDownMagnification.Size = new System.Drawing.Size(95, 31);
            this.numericUpDownMagnification.TabIndex = 15;
            this.numericUpDownMagnification.ValueChanged += new System.EventHandler(this.numericUpDownMagnification_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 304);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "放大倍率:";
            // 
            // checkBoxLockIntegerPoint
            // 
            this.checkBoxLockIntegerPoint.AutoSize = true;
            this.checkBoxLockIntegerPoint.Checked = true;
            this.checkBoxLockIntegerPoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLockIntegerPoint.Location = new System.Drawing.Point(182, 63);
            this.checkBoxLockIntegerPoint.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxLockIntegerPoint.Name = "checkBoxLockIntegerPoint";
            this.checkBoxLockIntegerPoint.Size = new System.Drawing.Size(131, 24);
            this.checkBoxLockIntegerPoint.TabIndex = 17;
            this.checkBoxLockIntegerPoint.Text = "鎖定整數點";
            this.checkBoxLockIntegerPoint.UseVisualStyleBackColor = true;
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.button_Read);
            this.groupBoxInput.Controls.Add(this.checkBoxLockIntegerPoint);
            this.groupBoxInput.Location = new System.Drawing.Point(1072, 41);
            this.groupBoxInput.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxInput.Size = new System.Drawing.Size(340, 117);
            this.groupBoxInput.TabIndex = 18;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "輸入 Input";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.button_OutPoint);
            this.groupBoxOutput.Controls.Add(this.button_OutPolynomial);
            this.groupBoxOutput.Controls.Add(this.checkBoxBlueLine);
            this.groupBoxOutput.Controls.Add(this.checkBoxGreenLine);
            this.groupBoxOutput.Controls.Add(this.checkBoxRedLine);
            this.groupBoxOutput.Controls.Add(this.checkBoxYellowLine);
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
            this.groupBoxOutput.Location = new System.Drawing.Point(1072, 167);
            this.groupBoxOutput.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxOutput.Size = new System.Drawing.Size(340, 428);
            this.groupBoxOutput.TabIndex = 19;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "輸出 Output";
            // 
            // button_OutPolynomial
            // 
            this.button_OutPolynomial.Location = new System.Drawing.Point(19, 185);
            this.button_OutPolynomial.Margin = new System.Windows.Forms.Padding(5);
            this.button_OutPolynomial.Name = "button_OutPolynomial";
            this.button_OutPolynomial.Size = new System.Drawing.Size(143, 39);
            this.button_OutPolynomial.TabIndex = 25;
            this.button_OutPolynomial.Text = "輸出多項式";
            this.button_OutPolynomial.UseVisualStyleBackColor = true;
            this.button_OutPolynomial.Click += new System.EventHandler(this.button_OutPolynomial_Click);
            // 
            // checkBoxBlueLine
            // 
            this.checkBoxBlueLine.AutoSize = true;
            this.checkBoxBlueLine.Checked = true;
            this.checkBoxBlueLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBlueLine.Location = new System.Drawing.Point(182, 211);
            this.checkBoxBlueLine.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxBlueLine.Name = "checkBoxBlueLine";
            this.checkBoxBlueLine.Size = new System.Drawing.Size(111, 24);
            this.checkBoxBlueLine.TabIndex = 24;
            this.checkBoxBlueLine.Text = "顯示藍線";
            this.checkBoxBlueLine.UseVisualStyleBackColor = true;
            // 
            // checkBoxGreenLine
            // 
            this.checkBoxGreenLine.AutoSize = true;
            this.checkBoxGreenLine.Checked = true;
            this.checkBoxGreenLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGreenLine.Location = new System.Drawing.Point(182, 176);
            this.checkBoxGreenLine.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxGreenLine.Name = "checkBoxGreenLine";
            this.checkBoxGreenLine.Size = new System.Drawing.Size(111, 24);
            this.checkBoxGreenLine.TabIndex = 23;
            this.checkBoxGreenLine.Text = "顯示綠線";
            this.checkBoxGreenLine.UseVisualStyleBackColor = true;
            // 
            // checkBoxRedLine
            // 
            this.checkBoxRedLine.AutoSize = true;
            this.checkBoxRedLine.Checked = true;
            this.checkBoxRedLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRedLine.Location = new System.Drawing.Point(182, 141);
            this.checkBoxRedLine.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRedLine.Name = "checkBoxRedLine";
            this.checkBoxRedLine.Size = new System.Drawing.Size(111, 24);
            this.checkBoxRedLine.TabIndex = 22;
            this.checkBoxRedLine.Text = "顯示紅線";
            this.checkBoxRedLine.UseVisualStyleBackColor = true;
            // 
            // checkBoxYellowLine
            // 
            this.checkBoxYellowLine.AutoSize = true;
            this.checkBoxYellowLine.Checked = true;
            this.checkBoxYellowLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxYellowLine.Location = new System.Drawing.Point(182, 107);
            this.checkBoxYellowLine.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxYellowLine.Name = "checkBoxYellowLine";
            this.checkBoxYellowLine.Size = new System.Drawing.Size(111, 24);
            this.checkBoxYellowLine.TabIndex = 21;
            this.checkBoxYellowLine.Text = "顯示黃線";
            this.checkBoxYellowLine.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 392);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "基礎 y 座標:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 348);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "基礎 x 座標:";
            // 
            // numericUpDownYdata
            // 
            this.numericUpDownYdata.Location = new System.Drawing.Point(146, 389);
            this.numericUpDownYdata.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownYdata.Name = "numericUpDownYdata";
            this.numericUpDownYdata.Size = new System.Drawing.Size(95, 31);
            this.numericUpDownYdata.TabIndex = 18;
            this.numericUpDownYdata.ValueChanged += new System.EventHandler(this.numericUpDownYdata_ValueChanged);
            // 
            // numericUpDownXdata
            // 
            this.numericUpDownXdata.Location = new System.Drawing.Point(146, 345);
            this.numericUpDownXdata.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownXdata.Name = "numericUpDownXdata";
            this.numericUpDownXdata.Size = new System.Drawing.Size(95, 31);
            this.numericUpDownXdata.TabIndex = 17;
            this.numericUpDownXdata.ValueChanged += new System.EventHandler(this.numericUpDownXdata_ValueChanged);
            // 
            // labelYellowMessage
            // 
            this.labelYellowMessage.AutoSize = true;
            this.labelYellowMessage.Location = new System.Drawing.Point(13, 609);
            this.labelYellowMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelYellowMessage.Name = "labelYellowMessage";
            this.labelYellowMessage.Size = new System.Drawing.Size(114, 20);
            this.labelYellowMessage.TabIndex = 25;
            this.labelYellowMessage.Text = "黃線多項式:";
            // 
            // labelRedMessage
            // 
            this.labelRedMessage.AutoSize = true;
            this.labelRedMessage.Location = new System.Drawing.Point(13, 649);
            this.labelRedMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRedMessage.Name = "labelRedMessage";
            this.labelRedMessage.Size = new System.Drawing.Size(114, 20);
            this.labelRedMessage.TabIndex = 26;
            this.labelRedMessage.Text = "紅線多項式:";
            // 
            // labelGreenMessage
            // 
            this.labelGreenMessage.AutoSize = true;
            this.labelGreenMessage.Location = new System.Drawing.Point(13, 689);
            this.labelGreenMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGreenMessage.Name = "labelGreenMessage";
            this.labelGreenMessage.Size = new System.Drawing.Size(114, 20);
            this.labelGreenMessage.TabIndex = 27;
            this.labelGreenMessage.Text = "綠線多項式:";
            // 
            // labelBlueMessage
            // 
            this.labelBlueMessage.AutoSize = true;
            this.labelBlueMessage.Location = new System.Drawing.Point(13, 729);
            this.labelBlueMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlueMessage.Name = "labelBlueMessage";
            this.labelBlueMessage.Size = new System.Drawing.Size(114, 20);
            this.labelBlueMessage.TabIndex = 28;
            this.labelBlueMessage.Text = "藍線多項式:";
            // 
            // labelYellow
            // 
            this.labelYellow.AutoSize = true;
            this.labelYellow.Location = new System.Drawing.Point(127, 609);
            this.labelYellow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelYellow.Name = "labelYellow";
            this.labelYellow.Size = new System.Drawing.Size(99, 20);
            this.labelYellow.TabIndex = 29;
            this.labelYellow.Text = "labelYellow";
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.Location = new System.Drawing.Point(127, 649);
            this.labelRed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(74, 20);
            this.labelRed.TabIndex = 30;
            this.labelRed.Text = "labelRed";
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.Location = new System.Drawing.Point(127, 689);
            this.labelGreen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(89, 20);
            this.labelGreen.TabIndex = 31;
            this.labelGreen.Text = "labelGreen";
            // 
            // labelBlue
            // 
            this.labelBlue.AutoSize = true;
            this.labelBlue.Location = new System.Drawing.Point(127, 729);
            this.labelBlue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlue.Name = "labelBlue";
            this.labelBlue.Size = new System.Drawing.Size(79, 20);
            this.labelBlue.TabIndex = 32;
            this.labelBlue.Text = "labelBlue";
            // 
            // button_OutPoint
            // 
            this.button_OutPoint.Location = new System.Drawing.Point(19, 231);
            this.button_OutPoint.Margin = new System.Windows.Forms.Padding(5);
            this.button_OutPoint.Name = "button_OutPoint";
            this.button_OutPoint.Size = new System.Drawing.Size(143, 39);
            this.button_OutPoint.TabIndex = 26;
            this.button_OutPoint.Text = "輸出RGB頻譜";
            this.button_OutPoint.UseVisualStyleBackColor = true;
            this.button_OutPoint.Click += new System.EventHandler(this.button_OutPoint_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1422, 762);
            this.Controls.Add(this.labelBlue);
            this.Controls.Add(this.labelGreen);
            this.Controls.Add(this.labelRed);
            this.Controls.Add(this.labelYellow);
            this.Controls.Add(this.labelBlueMessage);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.labelGreenMessage);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.labelRedMessage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelYellowMessage);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label labelBlueMessage;
        private System.Windows.Forms.Label labelGreenMessage;
        private System.Windows.Forms.Label labelRedMessage;
        private System.Windows.Forms.Label labelYellowMessage;
        private System.Windows.Forms.CheckBox checkBoxBlueLine;
        private System.Windows.Forms.CheckBox checkBoxGreenLine;
        private System.Windows.Forms.CheckBox checkBoxRedLine;
        private System.Windows.Forms.CheckBox checkBoxYellowLine;
        private System.Windows.Forms.Label labelYellow;
        private System.Windows.Forms.Label labelRed;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.Label labelBlue;
        private System.Windows.Forms.Button button_OutPolynomial;
        private System.Windows.Forms.Button button_OutPoint;
    }
}

