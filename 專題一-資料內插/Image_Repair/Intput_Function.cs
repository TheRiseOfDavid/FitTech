using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;            //讀寫檔案
using Spire.Xls;            //讀取Excel

namespace Image_repair
{
    public partial class Form1 : Form
    {
        double[,] LoadData;

        //用拖曳方式讀取檔案
        //在將物件拖入控制項的邊界時發生
        public void groupBoxInput_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        //拖放操作完成時發生
        public void groupBoxInput_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (e.Data.GetData("FileNameW") as string[]);
            //FilePath:檔案路徑、DefaultExt:附檔名
            string FilePath = data[0], DefaultExt = Path.GetExtension(FilePath);
            Read_File(FilePath, DefaultExt);
        }

        //用對話方塊讀取檔案
        public void Read_OpenFileDialog(object sender, EventArgs e)
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
                if (DefaultExt == ".txt" || DefaultExt == ".csv")
                {
                    StreamReader sr = new StreamReader(FilePath);
                    LoadData = Convert_Txt(sr.ReadToEnd());
                    sr.Close();
                    //dataGridViewInput_ShowTxt();
                }
                else if (DefaultExt == ".xlsx" || DefaultExt == ".xls")
                {
                    ExcelConvertCSV(FilePath);
                    StreamReader sr = new StreamReader(excelCSV);
                    LoadData = Convert_Txt(sr.ReadToEnd());
                    sr.Close();
                    //dataGridViewInput_ShowTxt();
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
    }
}
