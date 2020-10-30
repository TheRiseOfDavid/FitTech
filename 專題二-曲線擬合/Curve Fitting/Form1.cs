using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;                //讀取檔案
using System.Drawing.Imaging;   //支援多圖片格式

namespace Curve_Fitting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list_point = new List<PointF>();
            picture = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphic = Graphics.FromImage(picture);
            timer1.Interval = 10;
            timer1.Enabled = true;

            //取得或設定值，指出控制項是否能接受使用者拖放上來的資料。
            this.AllowDrop = true;

            numericUpDownMagnification.Value = Magnification;
            numericUpDownMagnification.Maximum = 200;
            numericUpDownMagnification.Minimum = 2;

            int max_move = 5000;
            numericUpDownXdata.Maximum = numericUpDownYdata.Maximum = max_move;
            numericUpDownXdata.Minimum = numericUpDownYdata.Minimum = -max_move;
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
            string about = "版本:1.0\r\n\r\n";
            about += "先載入資料(拖曳也可)，再按執行內插，即可顯示圖片。\r\n";
            about += "可按 輸出圖片 按鈕輸出。\r\n\r\n";
            about += "輸入支援格式: 文字檔(*.txt)\r\n";
            about += "輸出支援格式: *.bmp 、 *.png 、 *.jpg";
            MessageBox.Show(about, "說明", MessageBoxButtons.OK);
        }

        //程式更新紀錄
        private void updateLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string about = "版本:1.0 (5/29)\r\n";
            about += "    江大衛:\r\n";
            about += "    官齊笎:\r\n";
            about += "        1. 尋找過去程式，可將散點用線做表達。\r\n";
            about += "        2. 前端設計\r\n";
            about += "    洪揮霖:\r\n";
            about += "        1. 尋找最小平方法的方法";
            MessageBox.Show(about, "更新日誌", MessageBoxButtons.OK);
        }

        List<PointF> list_point;
        Bitmap picture;
        Graphics graphic;

        //滑鼠選取點座標
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            float x, y;
            if(checkBoxLockIntegerPoint.Checked)
            {
                x = (int)Math.Round(((double)e.X) / 5) * 5 / Magnification;
                y = (int)Math.Round(((double)e.Y) / 5) * 5 / Magnification;
            }
            else
            {
                x = (float)Math.Round((double)e.X) / Magnification;
                y = (float)Math.Round((double)e.Y) / Magnification;
            }
            if (list_point.Where(v => v.X == x && v.Y == y).ToArray().Length != 0)
                return;
            list_point.Add(new PointF(x, y));
            DrawEllipse(x, y);
        }

        //滑鼠移動
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = (e.X).ToString() + "," + (e.Y).ToString() + " 利用Lagrange內插進行曲線擬合 ";
        }

        //用拖曳方式讀取檔案
        //在將物件拖入控制項的邊界時發生
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        //拖放操作完成時發生
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (e.Data.GetData("FileNameW") as string[]);
            //FilePath:檔案路徑、DefaultExt:附檔名
            string FilePath = data[0];
            Read_File(FilePath);
        }

        //用對話方塊讀取檔案
        private void Read_OpenFileDialog(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogFunction = new OpenFileDialog();
            string windowFilter = "文字檔(*.txt)|*.txt";
            string windowTitle = "匯入資料";
            openFileDialogFunction.Filter = windowFilter;   //開窗搜尋副檔名
            openFileDialogFunction.Title = windowTitle;     //開窗標題
            if (openFileDialogFunction.ShowDialog() != DialogResult.OK)
                return;
            Read_File(openFileDialogFunction.FileName);
        }

        //根據檔案名稱抓數值資料，並傳到list_point
        //FilePath:檔案路徑
        public bool Read_File(string FilePath)
        {
            int[] input;
            DrawClear(null, null);
            input = (new StreamReader(FilePath)).ReadToEnd().Replace("\r\n", " ").Replace("\n", " ").Split(' ').Where(v => v != "").Select(v => int.Parse(v)).ToArray();
            for (int i = 0; i < input.Length; i += 2)
            {
                list_point.Add(new Point(input[i], input[i + 1]));
                DrawEllipse(list_point[i / 2].X, list_point[i / 2].Y);
            }
            return true;
        }

        //點座標放大倍率
        int Magnification = 10;
        private void numericUpDownMagnification_ValueChanged(object sender, EventArgs e)
        {
            Magnification = (int)numericUpDownMagnification.Value;
            if (drawLagrange)
                LagrangeInterpolation(null, null);
            else
                DrawGrid(null, null);
        }
        //利用Lagrange內插進行曲線擬合
        private void LagrangeInterpolation(object sender, EventArgs e)
        {
            drawLagrange = true;
            if (list_point.Count == 0)
            {
                MessageBox.Show("尚未選取任何點座標!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //將 list_point 拆成 RGB
            //r_g RG兩色的分隔點，g_b GB兩色的分隔點
            //曲線擬合運算
            List<PointF> pointf = Interpolation(list_point, 0, list_point.Count, false);
            //找最小兩點的位置
            PointF[] min_point = find_min(list_point);
            int[] r_g_b = insert_rgb(list_point, min_point);


            //曲線擬合運算
            List<PointF> pointf_R = Interpolation(list_point, 0, r_g_b[0], true);
            List<PointF> pointf_G = Interpolation(list_point, r_g_b[0], r_g_b[1], true);
            List<PointF> pointf_B = Interpolation(list_point, r_g_b[1], list_point.Count, true);

            //畫格線
            DrawGrid(sender, e);
            //畫曲線
            pointf = Interpolation(list_point, 0, list_point.Count, true);
            draw(pointf, Color.Yellow);
            draw(pointf_R, Color.Red);
            draw(pointf_G, Color.Green);
            draw(pointf_B, Color.Blue);
        }

        void draw(List<PointF> data, Color c)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                try
                {
                    graphic.DrawLine(new Pen(c, 3), data[i], data[i + 1]);
                    SetImage();
                }
                catch { }
            }
        }

        PointF[] find_min(List<PointF> data)
        {
            PointF[] min_point = { data[0], new PointF(-1, -1) };
            int i;
            float min_y = data[0].Y, max_y = -1;
            for (i = 1; i < data.Count; i++)
            {
                if (data[i].Y > min_y)
                {
                    min_point[0] = new PointF(data[i - 1].X, data[i - 1].Y);
                    max_y = data[i].Y;
                    break;
                }
                else
                    min_y = data[i].Y;
            }
            for (i++; i < data.Count; i++)
            {
                if (data[i].Y < max_y)
                {
                    break;
                }
                else
                    max_y = data[i].Y;
            }
            min_y = data[i].Y;
            for (; i < data.Count; i++)
            {
                if (data[i].Y > min_y)
                {
                    min_point[1] = new PointF(data[i - 1].X, data[i - 1].Y);
                    break;
                }
                else
                    min_y = data[i].Y;
            }
            return min_point;
        }

        int[] insert_rgb(List<PointF> data, PointF[] ins)
        {
            int[] ou = { -1, -1 };
            int idx = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].X > ins[idx].X)
                {
                    //data.Insert(i, ins[idx]);
                    ou[idx] = i;
                    idx++;
                    if (idx == 2)
                        break;
                }
            }
            return ou;
        }

        //曲線擬合運算
        List<PointF> Interpolation(List<PointF> input_point, int start, int end, bool yn_amplification)
        {
            List<PointF> pointf = new List<PointF>();
            for (float X_Axis = 0; X_Axis < pictureBox1.Width / Magnification; X_Axis += (float)0.1)
            {
                float Pn = 0;
                for (int n = start; n < end; n++)
                {
                    //註解中的 n 為點的個數
                    //Lj(x) = (x - x_0) / (x_j - x_0) * ... * (x - x_j-1) / (x_j - x_j-1) * (x - x_j+1) / (x_j - x_j+1) * ... * (x - x_n-1) / (x_j - x_n-1)
                    //Lj(x) = 把自己的點座標省略後通通相乘
                    float Lj_x = 1;
                    for (int j = 0; j < input_point.Count; j++)
                        if (n != j && input_point[n].X - input_point[j].X != 0)
                            Lj_x *= (X_Axis - input_point[j].X) / (input_point[n].X - input_point[j].X);
                    //Pn(x) = Y_0 * L_0(x) + Y_1 * L_1(x)... + Y_n-1 * L_n-1(x)
                    Pn += input_point[n].Y * Lj_x;
                }
                if(yn_amplification)
                    pointf.Add(new PointF(X_Axis * Magnification + x_move, Pn * Magnification + y_move));
                else
                    pointf.Add(new PointF(X_Axis, Pn));
            }
            return pointf;
        }

        //程式啟動時清除畫面
        private void start_clear(object sender, EventArgs e)
        {
            DrawClear(sender, e);
            timer1.Enabled = false;
        }

        //清除畫面
        private void DrawClear(object sender, EventArgs e)
        {
            drawLagrange = false;
            list_point = new List<PointF>();
            DrawGrid(null, null);
        }

        //畫面移動的距離
        float x_move = 0, y_move = 0;
        //是否有執行內插
        bool drawLagrange = false;
        private void numericUpDownXdata_ValueChanged(object sender, EventArgs e)
        {
            x_move = (float)numericUpDownXdata.Value;
            if (drawLagrange)
                LagrangeInterpolation(null, null);
            else
                DrawGrid(null, null);
        }

        private void numericUpDownYdata_ValueChanged(object sender, EventArgs e)
        {
            y_move = (float)numericUpDownYdata.Value;
            if (drawLagrange)
                LagrangeInterpolation(null, null);
            else
                DrawGrid(null, null);
        }

        //選擇是否畫格線
        private void DrawGrid(object sender, EventArgs e)
        {
            if (checkBoxShowGrid.Checked)
                DrawLine(1);
            else
                DrawLine(0);
        }

        //畫格線
        public void DrawLine(int yn)
        {
            //畫格線
            Pen pe = new Pen(Color.Black);
            graphic.Clear(this.BackColor);
            for (int i = 0; i < (yn == 1 ? pictureBox1.Width : 0); i += Magnification)
                graphic.DrawLine(pe, i, 0, i, pictureBox1.Height);
            for (int i = 0; i < (yn == 1 ? pictureBox1.Height : 0); i += Magnification)
                graphic.DrawLine(pe, 0, i, pictureBox1.Width, i);

            //畫點
            for (int i = 0; i < list_point.Count; i++)
                DrawEllipse(list_point[i].X, list_point[i].Y);
            SetImage();
        }

        //畫點
        public void DrawEllipse(float x, float y)
        {
            int diameter = 10;
            Pen pe = new Pen(Color.Blue, 2);
            graphic.DrawEllipse(pe, Magnification * x - diameter / 2 + x_move, Magnification * y - diameter / 2 + y_move, diameter, diameter);
            SetImage();
        }

        //顯示圖片
        public void SetImage()
        {
            pictureBox1.Image = picture;
        }

        //用對話方塊寫入圖片
        private void Write_OpenFileDialog(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogFunction = new SaveFileDialog();
            string windowFilter = "*.bmp|.bmp" + "|*.png|.png" + "|*.jpg|.jpg";
            string windowTitle = "匯出資料";

            saveFileDialogFunction.Filter = windowFilter;   //開窗搜尋副檔名
            saveFileDialogFunction.Title = windowTitle;     //開窗標題
            if (saveFileDialogFunction.ShowDialog() == DialogResult.OK)
            {
                //FilePath:檔案路徑、DefaultExt:附檔名
                string FilePath = saveFileDialogFunction.FileName, DefaultExt = Path.GetExtension(FilePath);
                if (Write_Image(FilePath, DefaultExt))
                    MessageBox.Show("輸出完成", "通知");
                else
                    MessageBox.Show("輸出失敗", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                return;
        }

        //寫入圖片
        public bool Write_Image(string FilePath, string DefaultExt)
        {
            try
            {
                if (DefaultExt == ".bmp")
                    picture.Save(FilePath, ImageFormat.Bmp);
                else if (DefaultExt == ".png")
                    picture.Save(FilePath, ImageFormat.Png);
                else if (DefaultExt == ".jpg")
                    picture.Save(FilePath, ImageFormat.Jpeg);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
