using System;
using System.Drawing;
using System.Windows.Forms;

namespace Image_repair
{
    public partial class Form1 : Form
    {
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
            for (int i = 0; i < OutputData.GetLength(1); i++)
                dataGridViewOutput.Columns.Add("Column" + i.ToString(), (i + 1).ToString());
            for (int i = 0; i < OutputData.GetLength(0); i++)
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
    }
}