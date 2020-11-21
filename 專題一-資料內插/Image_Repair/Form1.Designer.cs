namespace Image_repair
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonReadOpenFileDialog = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.buttonRemoveColumn = new System.Windows.Forms.Button();
            this.buttonRemoveRow = new System.Windows.Forms.Button();
            this.buttonAddColumn = new System.Windows.Forms.Button();
            this.dataGridViewInput = new System.Windows.Forms.DataGridView();
            this.radioButton_InputArrayFormat = new System.Windows.Forms.RadioButton();
            this.radioButton_InputLOPXYFormat = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.dataGridViewOutput = new System.Windows.Forms.DataGridView();
            this.radioButton_OutputArrayFormat = new System.Windows.Forms.RadioButton();
            this.radioButton_OutputLOPXYFormat = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.explanationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
            this.groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReadOpenFileDialog
            // 
            this.buttonReadOpenFileDialog.Location = new System.Drawing.Point(271, 542);
            this.buttonReadOpenFileDialog.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReadOpenFileDialog.Name = "buttonReadOpenFileDialog";
            this.buttonReadOpenFileDialog.Size = new System.Drawing.Size(245, 34);
            this.buttonReadOpenFileDialog.TabIndex = 1;
            this.buttonReadOpenFileDialog.Text = "讀取檔案 ReadFile";
            this.buttonReadOpenFileDialog.UseVisualStyleBackColor = true;
            this.buttonReadOpenFileDialog.Click += new System.EventHandler(this.Read_OpenFileDialog);
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(567, 356);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(172, 44);
            this.buttonConvert.TabIndex = 3;
            this.buttonConvert.Text = "轉換 Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.Convert_Gauss);
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.buttonRemoveColumn);
            this.groupBoxInput.Controls.Add(this.buttonRemoveRow);
            this.groupBoxInput.Controls.Add(this.buttonAddColumn);
            this.groupBoxInput.Controls.Add(this.dataGridViewInput);
            this.groupBoxInput.Controls.Add(this.radioButton_InputArrayFormat);
            this.groupBoxInput.Controls.Add(this.radioButton_InputLOPXYFormat);
            this.groupBoxInput.Controls.Add(this.label1);
            this.groupBoxInput.Controls.Add(this.buttonReadOpenFileDialog);
            this.groupBoxInput.Location = new System.Drawing.Point(19, 33);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(540, 594);
            this.groupBoxInput.TabIndex = 4;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            this.groupBoxInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.groupBoxInput_DragDrop);
            this.groupBoxInput.DragEnter += new System.Windows.Forms.DragEventHandler(this.groupBoxInput_DragEnter);
            // 
            // buttonRemoveColumn
            // 
            this.buttonRemoveColumn.Location = new System.Drawing.Point(281, 486);
            this.buttonRemoveColumn.Name = "buttonRemoveColumn";
            this.buttonRemoveColumn.Size = new System.Drawing.Size(245, 32);
            this.buttonRemoveColumn.TabIndex = 11;
            this.buttonRemoveColumn.Text = "刪除列 Remove Column";
            this.buttonRemoveColumn.UseVisualStyleBackColor = true;
            this.buttonRemoveColumn.Click += new System.EventHandler(this.buttonRemoveColumn_Click);
            // 
            // buttonRemoveRow
            // 
            this.buttonRemoveRow.Location = new System.Drawing.Point(14, 486);
            this.buttonRemoveRow.Name = "buttonRemoveRow";
            this.buttonRemoveRow.Size = new System.Drawing.Size(245, 32);
            this.buttonRemoveRow.TabIndex = 10;
            this.buttonRemoveRow.Text = "刪除行 Remove Row";
            this.buttonRemoveRow.UseVisualStyleBackColor = true;
            this.buttonRemoveRow.Click += new System.EventHandler(this.buttonRemoveRow_Click);
            // 
            // buttonAddColumn
            // 
            this.buttonAddColumn.Location = new System.Drawing.Point(14, 449);
            this.buttonAddColumn.Name = "buttonAddColumn";
            this.buttonAddColumn.Size = new System.Drawing.Size(512, 32);
            this.buttonAddColumn.TabIndex = 9;
            this.buttonAddColumn.Text = "添加列 Add Column";
            this.buttonAddColumn.UseVisualStyleBackColor = true;
            this.buttonAddColumn.Click += new System.EventHandler(this.buttonAddColumn_Click);
            // 
            // dataGridViewInput
            // 
            this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInput.Location = new System.Drawing.Point(14, 50);
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.RowHeadersWidth = 51;
            this.dataGridViewInput.RowTemplate.Height = 27;
            this.dataGridViewInput.Size = new System.Drawing.Size(512, 393);
            this.dataGridViewInput.TabIndex = 7;
            this.dataGridViewInput.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInput_CellEndEdit);
            this.dataGridViewInput.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewInput_RowsAdded);
            // 
            // radioButton_InputArrayFormat
            // 
            this.radioButton_InputArrayFormat.AutoSize = true;
            this.radioButton_InputArrayFormat.Location = new System.Drawing.Point(24, 556);
            this.radioButton_InputArrayFormat.Name = "radioButton_InputArrayFormat";
            this.radioButton_InputArrayFormat.Size = new System.Drawing.Size(150, 24);
            this.radioButton_InputArrayFormat.TabIndex = 6;
            this.radioButton_InputArrayFormat.TabStop = true;
            this.radioButton_InputArrayFormat.Text = "Array Format";
            this.radioButton_InputArrayFormat.UseVisualStyleBackColor = true;
            // 
            // radioButton_InputLOPXYFormat
            // 
            this.radioButton_InputLOPXYFormat.AutoSize = true;
            this.radioButton_InputLOPXYFormat.Location = new System.Drawing.Point(24, 528);
            this.radioButton_InputLOPXYFormat.Name = "radioButton_InputLOPXYFormat";
            this.radioButton_InputLOPXYFormat.Size = new System.Drawing.Size(160, 24);
            this.radioButton_InputLOPXYFormat.TabIndex = 5;
            this.radioButton_InputLOPXYFormat.TabStop = true;
            this.radioButton_InputLOPXYFormat.Text = "LOP XY Format";
            this.radioButton_InputLOPXYFormat.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "InputData 輸入資料:";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.dataGridViewOutput);
            this.groupBoxOutput.Controls.Add(this.radioButton_OutputArrayFormat);
            this.groupBoxOutput.Controls.Add(this.radioButton_OutputLOPXYFormat);
            this.groupBoxOutput.Controls.Add(this.label3);
            this.groupBoxOutput.Controls.Add(this.buttonOutput);
            this.groupBoxOutput.Location = new System.Drawing.Point(747, 33);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(540, 594);
            this.groupBoxOutput.TabIndex = 5;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // dataGridViewOutput
            // 
            this.dataGridViewOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutput.Location = new System.Drawing.Point(14, 50);
            this.dataGridViewOutput.Name = "dataGridViewOutput";
            this.dataGridViewOutput.RowHeadersWidth = 51;
            this.dataGridViewOutput.RowTemplate.Height = 27;
            this.dataGridViewOutput.Size = new System.Drawing.Size(512, 393);
            this.dataGridViewOutput.TabIndex = 9;
            // 
            // radioButton_OutputArrayFormat
            // 
            this.radioButton_OutputArrayFormat.AutoSize = true;
            this.radioButton_OutputArrayFormat.Location = new System.Drawing.Point(22, 558);
            this.radioButton_OutputArrayFormat.Name = "radioButton_OutputArrayFormat";
            this.radioButton_OutputArrayFormat.Size = new System.Drawing.Size(150, 24);
            this.radioButton_OutputArrayFormat.TabIndex = 8;
            this.radioButton_OutputArrayFormat.TabStop = true;
            this.radioButton_OutputArrayFormat.Text = "Array Format";
            this.radioButton_OutputArrayFormat.UseVisualStyleBackColor = true;
            // 
            // radioButton_OutputLOPXYFormat
            // 
            this.radioButton_OutputLOPXYFormat.AutoSize = true;
            this.radioButton_OutputLOPXYFormat.Location = new System.Drawing.Point(22, 528);
            this.radioButton_OutputLOPXYFormat.Name = "radioButton_OutputLOPXYFormat";
            this.radioButton_OutputLOPXYFormat.Size = new System.Drawing.Size(160, 24);
            this.radioButton_OutputLOPXYFormat.TabIndex = 7;
            this.radioButton_OutputLOPXYFormat.TabStop = true;
            this.radioButton_OutputLOPXYFormat.Text = "LOP XY Format";
            this.radioButton_OutputLOPXYFormat.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "OutputData 輸出資料:";
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(274, 544);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(245, 34);
            this.buttonOutput.TabIndex = 4;
            this.buttonOutput.Text = "輸出檔案 OutputFile";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.Write_OpenFileDialog);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolStripMenuItem,
            this.explanationToolStripMenuItem,
            this.updateLogToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1306, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "MenuStrip";
            // 
            // MenuToolStripMenuItem
            // 
            this.MenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readFileToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.outputFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem";
            this.MenuToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.MenuToolStripMenuItem.Text = "主選單 Menu";
            // 
            // readFileToolStripMenuItem
            // 
            this.readFileToolStripMenuItem.Name = "readFileToolStripMenuItem";
            this.readFileToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.readFileToolStripMenuItem.Text = "讀取檔案 ReadFile";
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.convertToolStripMenuItem.Text = "轉換 Convert";
            // 
            // outputFileToolStripMenuItem
            // 
            this.outputFileToolStripMenuItem.Name = "outputFileToolStripMenuItem";
            this.outputFileToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.outputFileToolStripMenuItem.Text = "輸出檔案 OutputFile";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(226, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.exitToolStripMenuItem.Text = "結束 Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // explanationToolStripMenuItem
            // 
            this.explanationToolStripMenuItem.Name = "explanationToolStripMenuItem";
            this.explanationToolStripMenuItem.Size = new System.Drawing.Size(137, 24);
            this.explanationToolStripMenuItem.Text = "說明 Explanation";
            this.explanationToolStripMenuItem.Click += new System.EventHandler(this.explanationToolStripMenuItem_Click);
            // 
            // updateLogToolStripMenuItem
            // 
            this.updateLogToolStripMenuItem.Name = "updateLogToolStripMenuItem";
            this.updateLogToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.updateLogToolStripMenuItem.Text = "更新日誌 Update log";
            this.updateLogToolStripMenuItem.Click += new System.EventHandler(this.updateLogToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 635);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Image repair";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonReadOpenFileDialog;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem explanationToolStripMenuItem;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem outputFileToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButton_InputArrayFormat;
        private System.Windows.Forms.RadioButton radioButton_InputLOPXYFormat;
        private System.Windows.Forms.RadioButton radioButton_OutputArrayFormat;
        private System.Windows.Forms.RadioButton radioButton_OutputLOPXYFormat;
        private System.Windows.Forms.ToolStripMenuItem updateLogToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewInput;
        private System.Windows.Forms.DataGridView dataGridViewOutput;
        private System.Windows.Forms.Button buttonAddColumn;
        private System.Windows.Forms.Button buttonRemoveColumn;
        private System.Windows.Forms.Button buttonRemoveRow;
    }
}

