using System;
using System.Text;
using System.Windows.Forms;

using System.IO;            //讀寫檔案
using Spire.Xls;            //讀取Excel

namespace Image_repair
{
    public partial class Form1 : Form
    {
        double[,] OutputData;

        //用對話方塊輸出檔案
        public void Write_OpenFileDialog(object sender, EventArgs e)
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
                else if (DefaultExt == ".csv")
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
