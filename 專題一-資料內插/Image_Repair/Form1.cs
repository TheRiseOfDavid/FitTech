using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;            //讀寫檔案
using Spire.Xls;            //讀取Excel

using System.Diagnostics;   //計時器

//轉換高斯
//using david_math;
using Gauss;

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

        double[,] LoadData;
        double[,] OutputData;

        //用拖曳方式讀取檔案
        //在將物件拖入控制項的邊界時發生
        private void groupBoxInput_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        //拖放操作完成時發生
        private void groupBoxInput_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (e.Data.GetData("FileNameW") as string[]);
            //FilePath:檔案路徑、DefaultExt:附檔名
            string FilePath = data[0], DefaultExt = Path.GetExtension(FilePath);
            Read_File(FilePath, DefaultExt);
        }

        //用對話方塊讀取檔案
        private void Read_OpenFileDialog(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogFunction = new OpenFileDialog();
            string windowFilter = "Excel files(.csv)|*.csv" + "|Excel files(.xlsx)|*.xlsx" + "|Excel files(.xls)|*.xls" + "|文字檔(*.txt)|*.txt";
            string windowTitle = "匯入資料";

            openFileDialogFunction.Filter = windowFilter;   //開窗搜尋副檔名
            openFileDialogFunction.Title = windowTitle;     //開窗標題
            if (openFileDialogFunction.ShowDialog() == DialogResult.OK)
            {
                //FilePath:檔案路徑、DefaultExt:附檔名
                string FilePath = openFileDialogFunction.FileName, DefaultExt = Path.GetExtension(FilePath);
                Read_File(FilePath, DefaultExt);
            }
            else
                return;
        }

        //根據檔案名稱抓數值資料，並傳到LoadData
        //FilePath:檔案路徑、DefaultExt:附檔名
        public bool Read_File(string FilePath, string DefaultExt)
        {
            try
            {
                if (DefaultExt == ".txt" || DefaultExt== ".csv")
                {
                    StreamReader sr = new StreamReader(FilePath);
                    LoadData = Convert_Txt(sr.ReadToEnd());
                    sr.Close();
                    dataGridViewInput_ShowTxt();
                }
                else if (DefaultExt == ".xlsx" || DefaultExt == ".xls")
                {
                    ExcelConvertCSV(FilePath);
                    StreamReader sr = new StreamReader(excelCSV);
                    LoadData = Convert_Txt(sr.ReadToEnd());
                    sr.Close();
                    dataGridViewInput_ShowTxt();
                }
                else
                {
                    MessageBox.Show("檔案非支援格式!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //消除行列
                dataGridViewOutput.Rows.Clear();
                dataGridViewOutput.Columns.Clear();
                OutputData = null;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        string excelCSV = @"ExcelToCSV.csv";
        //讀取Excel
        public void ExcelConvertCSV(string FilePath)
        {
            //載入xlsx文檔
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(FilePath);
            //獲取第一張工作表
            Worksheet sheet = workbook.Worksheets[0];
            //保存爲csv格式
            sheet.SaveToFile(excelCSV, ",", Encoding.UTF8);
        }

        //資料寫入中，是否讀取
        bool dataGridViewInput_notRead = false;
        //輸入數值改變時
        private void dataGridViewInput_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewInput_notRead)
                return;
            LoadData = Convert_Txt(dataGridViewInput);
        }

        //添加資料列
        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            dataGridViewInput.Columns.Add("Column" + dataGridViewInput.Columns.Count.ToString(), (dataGridViewInput.Columns.Count + 1).ToString());
            dataGridViewInput.Columns[dataGridViewInput.Columns.Count - 1].FillWeight = 1;
            dataGridViewInput_CellEndEdit(null, null);
        }
        //添加資料行時
        private void dataGridViewInput_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridViewInput.Rows.Count - 2 < 0)
                return;
            dataGridViewInput.Rows[dataGridViewInput.Rows.Count - 2].HeaderCell.Value = (dataGridViewInput.Rows.Count - 1).ToString();
        }
        //刪除資料行
        private void buttonRemoveRow_Click(object sender, EventArgs e)
        {
            //-2 是因為行有最後的使用者添加行
            if (dataGridViewInput.Rows.Count - 3 < 0)
            {
                MessageBox.Show("無法再刪除資料行!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridViewInput.Rows.RemoveAt(dataGridViewInput.Rows.Count - 2);
            dataGridViewInput_CellEndEdit(null, null);
        }
        //刪除資料列
        private void buttonRemoveColumn_Click(object sender, EventArgs e)
        {
            //只 -1 是因為列沒有最後的使用者添加列
            if (dataGridViewInput.Columns.Count - 2 < 0)
            {
                MessageBox.Show("無法再刪除資料列!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridViewInput.Columns.RemoveAt(dataGridViewInput.Columns.Count - 1);
            dataGridViewInput_CellEndEdit(null, null);
        }

        //將輸入字串轉成二維陣列
        int output_height_min, output_width_min;
        public double[,] Convert_Txt(string txt)
        {
            string[] input;
            double[][] input_line;
            int width;
            double[,] Data;
            //讀取 LOP XY Format
            if (radioButton_InputLOPXYFormat.Checked)
            {
                input = txt.Replace("\r\n", "\n").Replace(',', ' ').Replace('\t', ' ').Split('\n').Skip(1).ToArray();
                input_line = input.Select(v => v.Split(' ').Take(3).Select(w => w == "" ? 0 : double.Parse(w)).ToArray()).ToArray();
                input_line = input_line.Where(v => v.Length == 3).ToArray();
                int height_min = (int)input_line.Select(v => v[2]).ToArray().Min(), width_min = (int)input_line.Select(v => v[1]).ToArray().Min();
                input_line = input_line.Select(v => new double[] { v[0], v[1] - width_min, v[2] - height_min }).ToArray();
                int height_max = (int)input_line.Select(v => v[2]).ToArray().Max() + 1, width_max = (int)input_line.Select(v => v[1]).ToArray().Max() + 1;
                Data = new double[height_max, width_max];
                for (int i = 0; i < height_max; i++)
                    for (int j = 0; j < width_max; j++)
                        Data[i, j] = 0;
                int getlengeh = input_line.GetLength(0);
                for (int i = 0; i < getlengeh; i++)
                    Data[(int)input_line[i][2], (int)input_line[i][1]] = input_line[i][0];
                output_height_min = height_min;
                output_width_min = width_min;
            }
            //讀取 Array Format
            else
            {
                input = txt.Replace("\r\n", "\n").Replace(',', ' ').Replace('\t', ' ').Split('\n');
                input_line = input.Select(v => v.Split(' ').Select(w => w == "" ? 0 : double.Parse(w)).ToArray()).ToArray();
                width = input_line.Select(v => v.Length).Max();
                Data = new double[input.Length, width];
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input_line[i].Length; j++)
                        Data[i, j] = input_line[i][j];
                    for (int j = input_line.Length; j < width; j++)
                        Data[i, j] = 0;
                }
                output_height_min = 0;
                output_width_min = 0;
            }
            return Data;
        }
        public double[,] Convert_Txt(DataGridView dataGrid)
        {
            int row = dataGrid.Rows.Count - 1, col = dataGrid.Columns.Count;
            double[,] Data = new double[row, col];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    if (dataGridViewInput.Rows[i].Cells[j].Value == null || !double.TryParse(dataGridViewInput.Rows[i].Cells[j].Value.ToString(), out Data[i, j]))
                        Data[i, j] = 0;
            return Data;
        }

        //在dataGridViewInput顯示文字
        //顯示的最大支援數目
        int MaxLength = 1500 * 1500;
        public void dataGridViewInput_ShowTxt()
        {
            if (LoadData.Length < MaxLength)
            {
                dataGridViewInput.Enabled = true;
            }
            else
            {
                dataGridViewInput.Enabled = false;
                MessageBox.Show("因為輸入資料過多，故不在程式中顯示。", "通知");
                return;
            }

            dataGridViewInput_notRead = true;

            //消除行列
            dataGridViewInput.Rows.Clear();
            dataGridViewInput.Columns.Clear();

            //增加行列
            for (int i = 0; i < LoadData.GetLength(1); i++)
            {
                dataGridViewInput.Columns.Add("Column" + i.ToString(), (i + 1).ToString());
                dataGridViewInput.Columns[i].FillWeight = 1;
            }
            for (int i = 0; i < LoadData.GetLength(0); i++)
            {
                dataGridViewInput.Rows.Add();
                dataGridViewInput.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            //加入資料
            for (int i = 0; i < LoadData.GetLength(0); i++)
            {
                int UpperBound = LoadData.GetLength(1);
                for (int j = 0; j < UpperBound; j++)
                    dataGridViewInput.Rows[i].Cells[j].Value = LoadData[i, j].ToString();
            }

            dataGridViewInput_notRead = false;
        }

        //將LoadData轉換成矩陣，用高斯消去法求解後輸出
        private void Convert_Gauss(object sender, EventArgs e)
        {
            if (LoadData == null)
            {
                MessageBox.Show("尚未輸入資料!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int height = LoadData.GetLength(0), width = LoadData.GetLength(1);
            OutputData = new double[height, width]; //輸出陣列
            int[,] goed_map = new int[height, width];   //走過的格子
            //資料清為0
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    OutputData[i, j] = LoadData[i, j];
                    goed_map[i, j] = 0;
                }

            //測試時間程式
            Stopwatch Timer_RunBreak = new Stopwatch(), Timer_Gauss = new Stopwatch();
            TimeSpan time;

            //尋找損毀的資料
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    if (!DataBreak(OutputData, i, j))   //資料損毀
                    {
                        List<Point> point_goed = new List<Point>(); //走過的損毀點
                        List<int> point_walk_sum = new List<int>(); //損毀點可走的方向數

                        //測試時間程式
                        Timer_RunBreak.Start();

                        //尋找可走訪的損毀點
                        List<Point> po_new = new List<Point>(); //紀錄第 n 輪損毀點
                        po_new.Add(new Point(i, j));
                        int[,] move = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
                        while (po_new.Count != 0)
                        {
                            List<Point> po_new_copy = new List<Point>();    //紀錄第 n + 1 輪損毀點
                            for (int k = 0; k < po_new.Count; k++)
                            {
                                int po_walk_sum_copy = 0;
                                goed_map[po_new[k].X, po_new[k].Y] = 1;
                                for (int l = 0; l < move.GetLength(0); l++)
                                {
                                    Point p_new = new Point(po_new[k].X + move[l, 0], po_new[k].Y + move[l, 1]);
                                    if (p_new.X < 0 || p_new.X >= height || p_new.Y < 0 || p_new.Y >= width)
                                        continue;
                                    po_walk_sum_copy++;
                                    if (goed_map[p_new.X, p_new.Y] == 1)
                                        continue;
                                    if (!DataBreak(OutputData, p_new.X, p_new.Y))   //資料損毀
                                    {
                                        goed_map[p_new.X, p_new.Y] = 1;
                                        po_new_copy.Add(p_new);
                                    }
                                }
                                point_goed.Add(po_new[k]);
                                point_walk_sum.Add(po_walk_sum_copy);
                            }
                            po_new = po_new_copy.ToList();
                        }

                        //判別資料是否全部損毀
                        if (point_goed.Count == height * width)
                        {
                            dataGridViewOutput.Rows.Clear();
                            dataGridViewOutput.Columns.Clear();
                            OutputData = null;
                            MessageBox.Show("資料全部損毀，無法修復!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //完成高斯消取法所需矩陣
                        int gauss_height = point_goed.Count, gauss_width = gauss_height + 1;
                        double[,] gauss_map = new double[gauss_height, gauss_width];
                        double[] gauss_answer = new double[gauss_height];
                        for (int k = 0; k < gauss_height; k++)
                        {   
                            gauss_answer[k] = 0;
                            for (int l = 0; l < gauss_width; l++)
                                gauss_map[k, l] = 0;
                        }
                        for (int k = 0; k < gauss_height; k++)
                        {
                            gauss_map[k, k] = point_walk_sum[k];
                            for (int l = 0; l < move.GetLength(0); l++)
                            {
                                Point p_new = new Point(point_goed[k].X + move[l, 0], point_goed[k].Y + move[l, 1]);
                                if (p_new.X < 0 || p_new.X >= height || p_new.Y < 0 || p_new.Y >= width)
                                    continue;
                                int index = point_goed.IndexOf(p_new);
                                if (index == -1)    //常數
                                    gauss_map[k, gauss_width - 1] += OutputData[p_new.X, p_new.Y];
                                else                //未知數
                                    gauss_map[k, index] = -1;
                            }
                        }

                        //測試時間程式
                        Timer_RunBreak.Stop();

                        //測試用程式
                        /*
                        string tt = "gauss_map_old:\r\n";
                        for (int k = 0; k < gauss_width; k++)
                            for (int l = 0; l < gauss_width; l++)
                                tt += gauss_map[k, l] + (l == gauss_width - 1 ? "\r\n" : ", ");
                        MessageBox.Show(tt);
                        tt = "gauss_number_old:\r\n";
                        for (int k = 0; k < gauss_width; k++)
                            tt += gauss_answer[k] + "\r\n";
                        MessageBox.Show(tt);
                        */

                        //測試時間程式
                        Timer_Gauss.Start();

                        //算高斯矩陣(舊)
                        /*
                        gauss gauss_run = new gauss(gauss_height, gauss_map, gauss_answer);
                        //test.prepare();
                        gauss_run.elimindation();
                        gauss_run.back();
                        */
                        //算高斯矩陣(新)
                        gauss gauss_run = new gauss(gauss_height, gauss_map, gauss_answer);
                        //test.prepare();
                        //gauss_run.elimindation();   //高斯消元法
                        gauss_run.Seidel_method(1.1,-1);  //賽德爾疊代
                        //gauss_run.Conjugate_Gradient_Method();

                        //測試時間程式
                        Timer_Gauss.Stop();

                        //修復損毀點
                        for (int k = 0; k < gauss_height; k++)
                            OutputData[point_goed[k].X, point_goed[k].Y] = (int)gauss_answer[k];

                        //測試用程式
                        /*
                        tt = "gauss_map_new:\r\n";
                        for (int k = 0; k < gauss_width; k++)
                            for (int l = 0; l < gauss_width; l++)
                                tt += gauss_map[k, l] + (l == gauss_width - 1 ? "\r\n" : ", ");
                        MessageBox.Show(tt);
                        tt = "gauss_number_new:\r\n";
                        for (int k = 0; k < gauss_width; k++)
                            tt += gauss_answer[k] + "\r\n";
                        MessageBox.Show(tt);
                        */
                    }
                }
            dataGridViewOutput_ShowTxt(goed_map);

            //測試時間程式
            time = Timer_RunBreak.Elapsed;
            MessageBox.Show("陣列大小: [" + height.ToString() + ", " + width.ToString() + "]\r\n" +
                "尋找可走訪的損毀點 + 完成高斯消取法所需矩陣 所需總時間:\r\n" + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10));
            time = Timer_Gauss.Elapsed;
            MessageBox.Show("陣列大小: [" + height.ToString() + ", " + width.ToString() + "]\r\n" +
                "算高斯矩陣 所需總時間:\r\n" + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10));
        }

        //true 資料沒事，false資料損毀
        public bool DataBreak(double[,] data, int x, int y)
        {
            if (data[x, y] <= 0)
                return false;
            return true;
        }

        //在dataGridViewOutput顯示文字
        public void dataGridViewOutput_ShowTxt(int[,] goed_map)
        {

            if (OutputData.Length < MaxLength)
            {
                dataGridViewOutput.Enabled = true;
            }
            else
            {
                dataGridViewOutput.Enabled = false;
                MessageBox.Show("因為輸入資料過多，故不在程式中顯示。", "通知");
                return;
            }

            //消除行列
            int height = OutputData.GetLength(0), width = OutputData.GetLength(1);
            dataGridViewOutput.Rows.Clear();
            dataGridViewOutput.Columns.Clear();

            //增加行列
            for (int i = 0; i < width; i++)
                dataGridViewOutput.Columns.Add("Column" + i.ToString(), (i + 1).ToString());
            for (int i = 0; i < height; i++)
            {
                dataGridViewOutput.Rows.Add();
                dataGridViewOutput.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

            //加入資料
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    dataGridViewOutput.Rows[i].Cells[j].Value = OutputData[i, j].ToString();
                    if (goed_map[i, j] == 1)
                        dataGridViewOutput.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                    else
                        dataGridViewOutput.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                }
        }

        //用對話方塊輸出檔案
        private void Write_OpenFileDialog(object sender, EventArgs e)
        {
            if (OutputData == null)
            {
                MessageBox.Show("尚未轉換資料!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialogFunction = new SaveFileDialog();
            string windowFilter = "Excel files(.csv)|*.csv" + "|Excel files(.xlsx)|*.xlsx" + "|Excel files(.xls)|*.xls" + "|文字檔(*.txt)|*.txt";
            string windowTitle = "匯出資料";

            saveFileDialogFunction.Filter = windowFilter;   //開窗搜尋副檔名
            saveFileDialogFunction.Title = windowTitle;     //開窗標題
            if (saveFileDialogFunction.ShowDialog() == DialogResult.OK)
            {
                //FilePath:檔案路徑、DefaultExt:附檔名
                string FilePath = saveFileDialogFunction.FileName, DefaultExt = Path.GetExtension(FilePath);
                if (Write_File(FilePath, DefaultExt))
                    MessageBox.Show("輸出完成", "通知");
                else
                    MessageBox.Show("輸出失敗", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                return;
        }

        //根據檔案名稱抓目標類型
        public bool Write_File(string FilePath, string DefaultExt)
        {
            try
            {
                string CSVexcel = @"CSVToExcel.csv";
                if (DefaultExt == ".txt")
                {
                    WriteTXT(FilePath, "\t");
                }
                else if(DefaultExt == ".csv")
                    WriteTXT(FilePath, ",");
                else if (DefaultExt == ".xlsx")
                {
                    WriteTXT(CSVexcel, ",");

                    //CSV轉XLSX
                    //XLSX格式可選Excel 2007，2010和2013。
                    //載入csv文檔
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(CSVexcel, ",", 1, 1);
                    //保存爲xlsx格式
                    workbook.SaveToFile(FilePath, ExcelVersion.Version2013);
                }
                else if (DefaultExt == ".xls")
                {
                    WriteTXT(CSVexcel, ",");

                    //CSV轉XLS
                    //載入csv文檔
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile(CSVexcel, ",", 1, 1);
                    //保存爲xls格式
                    workbook.SaveToFile(FilePath, ExcelVersion.Version97to2003);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //將OutData中的資料寫入
        public void WriteTXT(string FilePath, string split)
        {
            StreamWriter sw = new StreamWriter(FilePath, false, Encoding.UTF8);
            int height = OutputData.GetLength(0), width = OutputData.GetLength(1);
            if (radioButton_OutputLOPXYFormat.Checked)
            {
                sw.Write("LOP(亮度)" + split + "x" + split + "y\r\n");
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        string OutputStr = OutputData[i, j].ToString();
                        sw.Write(OutputStr + split + (j + output_width_min).ToString() + split + (i + output_height_min).ToString() + (i == height - 1 && j == width - 1 ? "" : "\r\n"));
                    }
            }
            else
            {
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        string OutputStr = OutputData[i, j].ToString();
                        sw.Write(OutputStr + (j == width - 1 ? (i == height - 1 ? "" : "\r\n") : split));
                    }
            }
            sw.Close();
        }
    }
}
