using System;
using System.Windows.Forms;

namespace Image_repair
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //主選單事件
            readFileToolStripMenuItem.Click += Read_OpenFileDialog;
            convertToolStripMenuItem.Click += Convert_Gauss;
            outputFileToolStripMenuItem.Click += Write_OpenFileDialog;

            //拖曳事件
            buttonReadOpenFileDialog.DragEnter += groupBoxInput_DragEnter;
            buttonReadOpenFileDialog.DragDrop += groupBoxInput_DragDrop;
            //取得或設定值，指出控制項是否能接受使用者拖放上來的資料。
            groupBoxInput.AllowDrop = buttonReadOpenFileDialog.AllowDrop = true;

            radioButton_InputLOPXYFormat.Checked = radioButton_OutputLOPXYFormat.Checked = true;

            //增加最左方的直排寬度
            dataGridViewInput.RowHeadersWidth = dataGridViewInput.RowHeadersWidth + 10;
            dataGridViewOutput.RowHeadersWidth = dataGridViewOutput.RowHeadersWidth  + 10;
            //dataGridViewOutput設置成唯讀
            dataGridViewOutput.ReadOnly = true;
            //禁止用戶添加行，就不會一直多出最下方那行
            dataGridViewOutput.AllowUserToAddRows = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FullClose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FullClose();
        }

        //徹底關閉程式
        public void FullClose()
        {
            System.Environment.Exit(0);
        }

        //說明文件
        private void explanationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string about = "版本:1.1\r\n\r\n";
            about += "1. LOP XY 格式:序列格式。\r\n";
            about += "2. Array XY 格式:二維陣列格式。\r\n";
            about += "3. 列為直排，行為橫排。\r\n\r\n";
            about += "a. 添加列要按按鈕，添加行則是直接輸入數值。\r\n";
            about += "b. 先載入資料(拖曳也可)，再按轉換，即可修復損毀區域。\r\n";
            about += "c. 可按 輸出檔案 按鈕輸出資料。\r\n\r\n";
            about += "支援格式: *.csv 、*.xlsx 、 *.xls 、 文字檔(*.txt)";
            MessageBox.Show(about, "說明", MessageBoxButtons.OK);
        }

        //程式更新紀錄
        private void updateLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string about = "版本:1.0 (5/24)\r\n";
            about += "    江大衛:\r\n";
            about += "        1. 完成高斯消去法\r\n";
            about += "    官齊笎:\r\n";
            about += "        1. 尋找損毀點並修複\r\n";
            about += "        2. 前端設計\r\n";
            about += "版本:1.1 (6/5)\r\n";
            about += "    江大衛:\r\n";
            about += "        1. 高斯優化、使用迭代法\r\n";
            about += "        2. 量子網站註冊並嘗試\r\n";
            about += "    官齊笎:\r\n";
            about += "        1. 前端改變\r\n";
            about += "        2. Excel 檔案輸入輸出\r\n";
            about += "        3. 添加防呆處理\r\n";
            MessageBox.Show(about, "更新日誌", MessageBoxButtons.OK);
        }
    }
}
