#define DEBUG
//#undef DEBUG
//第一行註解掉、第二行恢復，就可解除除錯模式(變成不顯示花費時間)

//#define UseBigFloat
#undef UseBigFloat
//第一行註解掉、第二行恢復，就變成不使用浮點大數

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
using Spire.Xls;                //讀取Excel
using System.Drawing.Imaging;   //支援多圖片格式

using System.Diagnostics;   //計時器

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
            //這筆輸入資料和 106北市賽程式(高工組).pdf 中的測資相同，用來測試輸出的方程式是否正確
            /*
            List<PointF> TempPoint = new List<PointF>() { new PointF(1, 0), new PointF(2, 0), new PointF(3, 0) };
            float[] TempOut = GetPolynomial(TempPoint);
            */

            //浮點大數測試程式
            /*
            float ff1 = float.NegativeInfinity, ff2 = (float)0.1;
            BigFloat bb1 = cv_float_BigFloat(ff1), bb2 = cv_float_BigFloat(ff2);
            BigFloat b1, b2;
            b1 = BigFloat_add(bb1, bb2);
            b2 = BigFloat_mul(bb1, bb2);
            ff1 = cv_BigFloat_float(b1);
            ff2 = cv_BigFloat_float(b2);
            MessageBox.Show(ff1.ToString() + "\r\n" + ff2.ToString());
            */

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

        //徹底關閉程式
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FullClose();
        }

        //徹底關閉程式
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
            string about = "版本:2.0\r\n\r\n";
            about += "先載入資料(拖曳也可)，再按執行內插，即可顯示圖片。\r\n";
            about += "可按螢幕畫面來選取點座標。\r\n";
            about += "基礎 xy 座標: 移動螢幕畫面。\r\n";
            about += "可按 輸出圖片 按鈕輸出。\r\n";
            about += "可按 輸出多項式 按鈕輸出。\r\n";
            about += "可按 輸出RGB頻譜 按鈕輸出，檔案會輸出到指定資料夾(同時輸出RGB就會有三個檔案)。\r\n\r\n";
            about += "輸入支援格式: *.csv 、*.xlsx 、 *.xls 、 文字檔(*.txt)\r\n";
            about += "輸出支援格式(波形圖): *.bmp 、 *.png 、 *.jpg\r\n";
            about += "輸出支援格式(多項式): *.csv 、*.xlsx 、 *.xls 、 文字檔(*.txt)\r\n";
            about += "輸出支援格式(RGB頻譜): *.csv";
            MessageBox.Show(about, "說明", MessageBoxButtons.OK);
        }

        //程式更新紀錄
        private void updateLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string about = "版本:1.0 (5/29)\r\n";
            about += "    江大衛:\r\n";
            about += "    官齊笎:\r\n";
            about += "        1. 尋找過去程式，可將散點用線做表達\r\n";
            about += "        2. 前端設計\r\n";
            about += "    洪揮霖:\r\n";
            about += "        1. 尋找最小平方法的方法\r\n";
            about += "版本:1.5 (6/27)\r\n";
            about += "    江大衛:\r\n";
            about += "        1. 說明文件製作\r\n";
            about += "    官齊笎:\r\n";
            about += "        1. 各種顯示和輸入的小錯誤修正\r\n";
            about += "        2. 支援 Excel\r\n";
            about += "    洪揮霖:\r\n";
            about += "版本:2.0 (8/28)\r\n";
            about += "    官齊笎:\r\n";
            about += "        1. 顯示多項式\r\n";
            about += "        2. 輸出RGB頻譜\r\n";
            MessageBox.Show(about, "更新日誌", MessageBoxButtons.OK);
        }

        List<PointF> list_point;
        Bitmap picture;
        Graphics graphic;

        //滑鼠選取點座標
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            float x, y;
            double ex = e.X, ey = picture.Height - e.Y;
            if(checkBoxLockIntegerPoint.Checked)
            {
                x = (int)Math.Round(ex / 5) * 5 / Magnification;
                y = (int)Math.Round(ey / 5) * 5 / Magnification;
            }
            else
            {
                x = (float)Math.Round(ex) / Magnification;
                y = (float)Math.Round(ey) / Magnification;
            }
            x -= x_move;
            y -= y_move;
            if (list_point.Where(v => v.X == x && v.Y == y).ToArray().Length != 0)
                return;
            int insert;
            for (insert = 0; insert < list_point.Count; insert++)
            {
                if (list_point[insert].X > x)
                    break;
            }
            list_point.Insert(insert, new PointF(x, y));
            DrawEllipse(x, y);
            SetImage();
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
            if (openFileDialogFunction.ShowDialog() != DialogResult.OK)
                return;
            string FilePath = openFileDialogFunction.FileName, DefaultExt = Path.GetExtension(FilePath);
            Read_File(FilePath, DefaultExt);
            SetImage();
        }

        //根據檔案名稱抓數值資料，並傳到list_point
        //FilePath:檔案路徑
        public bool Read_File(string FilePath, string DefaultExt)
        {
            try
            {
                if (DefaultExt == ".txt" || DefaultExt == ".csv")
                {
                    filestring(FilePath);
                }
                else if (DefaultExt == ".xlsx" || DefaultExt == ".xls")
                {
                    ExcelConvertCSV(FilePath);
                    filestring(excelCSV);
                }
                else
                {
                    MessageBox.Show("檔案非支援格式!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //讀取文字檔的資料
        float shrink = 1000;
        public void filestring(string FilePath)
        {
            float[] input;
            DrawClear(null, null);
            input = (new StreamReader(FilePath)).ReadToEnd().Replace("\r\n", " ").Replace("\n", " ").Replace(",", " ").Split(' ').Where(v => v != "").Select(v => float.Parse(v)).ToArray();
            for (int i = 0; i < input.Length; i++)
            {
                list_point.Add(new PointF(i, input[i] / shrink));
                DrawEllipse(list_point[i].X, list_point[i].Y);
            }
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

        //點座標放大倍率
        int Magnification = 10;
        //放大倍率更改時，重新繪圖
        private void numericUpDownMagnification_ValueChanged(object sender, EventArgs e)
        {
            Magnification = (int)numericUpDownMagnification.Value;
            if (drawLagrange)
                LagrangeInterpolation(null, null);
            else
            {
                DrawGrid(null, null);
                SetImage();
            }
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

#if (DEBUG)
            //測試時間程式
            Stopwatch Timer_RunLagrange = new Stopwatch();
            TimeSpan time;
            Timer_RunLagrange.Start();
#endif

            //將 list_point 拆成 RGB
            //r_g RG兩色的分隔點，g_b GB兩色的分隔點
            //曲線擬合運算
            List<PointF> LagrangePoint = Interpolation(list_point, false);
            //LagrangePoint = ReducePoint(LagrangePoint, 10);
            
            //找 最小兩點 + 最小兩點中的最大點 的位置
            /*
             * r_g_b: 極值的指標
             * min_point: 極值的三個點
             */
            int[] r_g_b = new int[3];
            PointF[] min_point = find_min_max_min(LagrangePoint, r_g_b);

            //畫格線
            DrawGrid(sender, e);


            labelYellow.Text = labelRed.Text = labelGreen.Text = labelBlue.Text = "";
            if (checkBoxYellowLine.Checked)
            {
                //曲線擬合運算
                List<PointF> LagrangePoint_Y = Interpolation(list_point, true);

                //畫曲線
                drawLagrangeLine(LagrangePoint_Y, Color.Yellow);

                //顯示多項式
                ShowPolynomial(labelYellow, GetPolynomial(list_point));
            }
            if (checkBoxRedLine.Checked)
            {
                //曲線擬合運算
                List<PointF> LagrangePoint_R = LagrangePoint.Skip(r_g_b[2]).Take(LagrangePoint.Count - r_g_b[2]).ToList();
                LagrangePoint_R = ReducePoint(LagrangePoint_R, 10);
                List<PointF> pointf_R = Interpolation(LagrangePoint_R, true);
                all_R = pointf_R.ToList();

                //畫曲線
                drawLagrangeLine(pointf_R, Color.Red);

                //顯示多項式
                ShowPolynomial(labelRed, GetPolynomial(LagrangePoint_R));
            }
            else
                all_R = new List<PointF>();
            if (checkBoxGreenLine.Checked)
            {
                //曲線擬合運算
                List<PointF> LagrangePoint_G = LagrangePoint.Skip(r_g_b[0]).Take(r_g_b[2] - r_g_b[0]).ToList();
                LagrangePoint_G = ReducePoint(LagrangePoint_G, 10);
                List<PointF> pointf_G = Interpolation(LagrangePoint_G, true);
                all_G = pointf_G.ToList();

                //畫曲線
                drawLagrangeLine(pointf_G, Color.Green);

                //顯示多項式
                ShowPolynomial(labelGreen, GetPolynomial(LagrangePoint_G));
            }
            else
                all_G = new List<PointF>();
            if (checkBoxBlueLine.Checked)
            {
                //曲線擬合運算
                List<PointF> LagrangePoint_B = LagrangePoint.Take(r_g_b[0]).ToList();
                LagrangePoint_B = ReducePoint(LagrangePoint_B, 10);
                List<PointF> pointf_B = Interpolation(LagrangePoint_B, true);
                all_B = pointf_B.ToList();

                //畫曲線
                drawLagrangeLine(pointf_B, Color.Blue);

                //顯示多項式
                ShowPolynomial(labelBlue, GetPolynomial(LagrangePoint_B));
            }
            else
                all_B = new List<PointF>();

            //顯示RGB是用哪些點進行曲線擬合運算
            /*
            drawLagrangeLine(LagrangePoint_R, Color.Red);
            drawLagrangeLine(LagrangePoint_G, Color.Green);
            drawLagrangeLine(LagrangePoint_B, Color.Blue);
            */

            SetImage();

#if (DEBUG)
            //測試時間程式
            Timer_RunLagrange.Stop();
            time = Timer_RunLagrange.Elapsed;
            MessageBox.Show("曲線擬合 所需總時間:\r\n" + String.Format("{0:00}:{1:00}:{2:00}.{3:00}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10));
#endif
        }

        //繪製曲線擬合的線
        void drawLagrangeLine(List<PointF> data, Color c)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                try
                {
                    graphic.DrawLine(new Pen(c, 3), data[i], data[i + 1]);
                }
                catch { }
            }
        }

        //顯示曲線擬合多項式
        public void ShowPolynomial(Label lab, float[] pol)
        {
            if (pol.Length == 0)
            {
                lab.Text = "求不出多項式";
                return;
            }

            //是否有顯示出第一個數字
            bool yn_showone = false;
            for (int i = 0; i < pol.Length; i++)
            {
                if (float.IsNaN(pol[i]) || pol[i] == 0)
                    continue;
                if (!yn_showone)
                {
                    lab.Text = (Math.Abs(pol[i])).ToString();
                    yn_showone = true;
                }
                else
                {
                    lab.Text = (Math.Abs(pol[i])).ToString() + " * x^" + i.ToString() + ((pol[i - 1] < 0 ? " - " : " + ")) + lab.Text;
                }
            }
            lab.Text = ((pol[pol.Length - 1] < 0 ? "-" : "")) + lab.Text;
            if (!yn_showone)
                lab.Text = "求不出多項式";
        }

        //取得曲線擬合多項式
        public float[] GetPolynomial(List<PointF> list_point)
        {
            //polynomial[0] 為 x^0 的係數，equation[1] 為 x^1 的係數...以此類推
            List<float> polynomial = new List<float>();
            //第一層將多項式相加
            for (int i = 0; i < list_point.Count; i++)
            {
                //將多項式相乘
                float Denominator = 1;  //分母
                List<float> pol = new List<float>();
                pol.Add(1);
                for (int j = 0; j < list_point.Count; j++)
                {
                    if (i == j)
                        continue;
                    List<float> pol2 = new List<float>();
                    pol2.Add(-1 * list_point[j].X);
                    pol2.Add(1);
                    pol = PolynomialMultiply(pol, pol2).ToList();
                    Denominator *= list_point[i].X - list_point[j].X;
                }

                for (int j = 0; j < pol.Count; j++)
                {
                    //多項式 = yi / 分母
                    pol[j] *= list_point[i].Y / Denominator;
                }

                //多項式相加
                polynomial = PolynomialAdd(polynomial, pol).ToList();
            }
            return polynomial.ToArray();
        }

        //多項式相乘
        /*
         * pol1[0] 為 x^0 的係數，pol1[1] 為 x^1 的係數...以此類推
         * pol2[0] 為 x^0 的係數，pol2[1] 為 x^1 的係數...以此類推
         */
        public List<float> PolynomialMultiply(List<float> pol1, List<float> pol2)
        {
            List<float> polout = new List<float>();
            for (int i = 0; i < pol2.Count; i++)
            {
                for (int j = 0; j < pol1.Count; j++)
                {
                    float add = pol2[i] * pol1[j];
                    if (i + j >= polout.Count)
                        polout.Add(add);
                    else
                        polout[i + j] += add;
                }
            }
            return polout;
        }

        //多項式相加
        /*
         * pol1[0] 為 x^0 的係數，pol1[1] 為 x^1 的係數...以此類推
         * pol2[0] 為 x^0 的係數，pol2[1] 為 x^1 的係數...以此類推
         */
        public List<float> PolynomialAdd(List<float> pol1, List<float> pol2)
        {
            List<float> polout = new List<float>();
            for (int i = 0; i < pol1.Count; i++)
            {
                polout.Add(pol1[i]);
            }
            for (int i = 0; i < pol2.Count; i++)
            {
                if (i >= polout.Count)
                    polout.Add(pol2[i]);
                else
                    polout[i] += pol2[i];
            }
            return polout;
        }

        //尋找極值，用已區分rgb
        PointF[] find_min_max_min(List<PointF> data, int[] r_g_b)
        {
            PointF[] min_point = { data[0], new PointF(-1, -1), new PointF(-1, -1) };
            int i;
            float min_y = data[0].Y, max_y = -1;
            for (i = 1; i < data.Count; i++)
            {
                if (data[i].Y > min_y)
                {
                    if (i == 1)
                    {
                        do
                        {
                            i++;
                            min_y = data[i].Y;
                        }
                        while (data[i].Y > data[i - 1].Y);
                        continue;
                    }
                    min_point[0] = new PointF(data[i - 1].X, data[i - 1].Y);
                    max_y = data[i].Y;
                    r_g_b[0] = i - 1;
                    break;
                }
                else
                    min_y = data[i].Y;
            }
            for (i++; i < data.Count; i++)
            {
                if (data[i].Y < max_y)
                {
                    min_point[1] = new PointF(data[i - 1].X, data[i - 1].Y);
                    r_g_b[1] = i - 1;
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
                    min_point[2] = new PointF(data[i - 1].X, data[i - 1].Y);
                    r_g_b[2] = i - 1;
                    break;
                }
                else
                    min_y = data[i].Y;
            }
            return min_point;
        }

        //曲線擬合運算
        /*
         * input_point: 使用者選取的點串列
         * yn_amplification: 是否將點放大
         * 回傳完整曲線的連續點座標
         */
        List<PointF> Interpolation(List<PointF> input_point, bool yn_amplification)
        {
            List<PointF> pointf = new List<PointF>();
            for (float X_Axis = -x_move; X_Axis < pictureBox1.Width / Magnification - x_move; X_Axis += (float)1 / Magnification)
            {
#if(UseBigFloat)
                float Pn = 0;
                BigFloat Pn_BigFloat = cv_float_BigFloat(Pn);
                for (int n = 0; n < input_point.Count; n++)
                {
                    //註解中的 n 為點的個數
                    //Lj(x) = (x - x_0) / (x_j - x_0) * ... * (x - x_j-1) / (x_j - x_j-1) * (x - x_j+1) / (x_j - x_j+1) * ... * (x - x_n-1) / (x_j - x_n-1)
                    //Lj(x) = 把自己的點座標省略後通通相乘
                    BigFloat Lj_x = cv_float_BigFloat((float)1.0);

                    for (int j = 0; j < input_point.Count; j++)
                        if (n != j)
                        {
                            float Lj_x_float = (X_Axis - input_point[j].X) / (input_point[n].X - input_point[j].X);
                            if (float.IsNaN(Lj_x_float) || float.IsInfinity(Lj_x_float))
                                continue;
                            Lj_x = BigFloat_mul(cv_float_BigFloat(Lj_x_float), Lj_x);
                            /*
                            Lj_x *= rounding((X_Axis - input_point[j].X) / (input_point[n].X - input_point[j].X), 5);
                            Lj_x = rounding(Lj_x, 5);
                            */
                        }
                    //Pn(x) = Y_0 * L_0(x) + Y_1 * L_1(x)... + Y_n-1 * L_n-1(x)
                    Pn_BigFloat = BigFloat_add(Pn_BigFloat, BigFloat_mul(cv_float_BigFloat(input_point[n].Y), Lj_x));
                }
                Pn = cv_BigFloat_float(Pn_BigFloat);
#else
                float Pn = 0;
                for (int n = 0; n < input_point.Count; n++)
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
#endif

                if (float.IsNaN(Pn) || float.IsInfinity(Pn))
                    continue;
                if(yn_amplification)
                    pointf.Add(new PointF((X_Axis + x_move) * Magnification, (Pn + y_move) * Magnification));
                else
                    pointf.Add(new PointF(X_Axis, Pn));
            }
            return pointf;
        }

        //四捨五入取到第 bit 位
        public float rounding(float number, int bit)
        {
            if (float.IsNaN(number) || float.IsInfinity(number))
                return number;
            return float.Parse(number.ToString("f" + bit.ToString()));
        }

        //將曲線擬合第一輪的點減少，以利尋找rgb的波形
        List<PointF> ReducePoint(List<PointF> input_point, int Shrink)
        {
            List<PointF> @out = new List<PointF>();
            for (int i = 0; i < input_point.Count; i+= Shrink)
            {
                @out.Add(input_point[i]);
            }
            return @out;
        }

        //程式啟動時清除畫面
        private void start_clear(object sender, EventArgs e)
        {
            DrawClear(sender, e);
            timer1.Enabled = false;
            SetImage();
        }

        //清除畫面
        private void DrawClear(object sender, EventArgs e)
        {
            drawLagrange = false;
            list_point = new List<PointF>();
            DrawGrid(null, null);
            SetImage();

            labelYellow.Text = labelRed.Text = labelGreen.Text = labelBlue.Text = "";
        }

        //畫面移動的距離
        float x_move = 0, y_move = 0;
        //是否有執行內插
        bool drawLagrange = false;
        //X座標更改時，畫面重繪
        private void numericUpDownXdata_ValueChanged(object sender, EventArgs e)
        {
            x_move = (float)numericUpDownXdata.Value;
            if (drawLagrange)
                LagrangeInterpolation(null, null);
            else
            {
                DrawGrid(null, null);
                SetImage();
            }
        }

        //Y座標更改時，畫面重繪
        private void numericUpDownYdata_ValueChanged(object sender, EventArgs e)
        {
            y_move = (float)numericUpDownYdata.Value;
            if (drawLagrange)
                LagrangeInterpolation(null, null);
            else
            {
                DrawGrid(null, null);
                SetImage();
            }
        }

        //選擇是否畫格線
        private void DrawGrid(object sender, EventArgs e)
        {
            if (checkBoxShowGrid.Checked)
                DrawLine(1);
            else
                DrawLine(0);
            SetImage();
        }

        //畫格線
        public void DrawLine(int yn)
        {
            //畫格線
            Pen pen = new Pen(Color.Black);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pen.DashPattern = new float[] { 3, 3 };
            graphic.Clear(this.BackColor);
            if(yn == 1)
            {
                for (int i = 0; i < pictureBox1.Width; i += Magnification)
                    graphic.DrawLine(pen, i, 0, i, pictureBox1.Height);
                for (int i = 0; i < pictureBox1.Height; i += Magnification)
                    graphic.DrawLine(pen, 0, i, pictureBox1.Width, i);
            }

            //繪製XY軸
            float axis = x_move * Magnification, ayis = y_move * Magnification;
            graphic.DrawLine(new Pen(Color.Black, 5), 0, ayis, pictureBox1.Width, ayis);
            graphic.DrawLine(new Pen(Color.Black, 5), axis, 0, axis, pictureBox1.Height);

            //畫點
            for (int i = 0; i < list_point.Count; i++)
                DrawEllipse(list_point[i].X, list_point[i].Y);
        }

        //畫點
        public void DrawEllipse(float x, float y)
        {
            int diameter = 10;
            Pen pen = new Pen(Color.Cyan, 2);
            graphic.DrawEllipse(pen, Magnification * (x + x_move) - diameter / 2, Magnification * (y + y_move) - diameter / 2, diameter, diameter);
        }

        //顯示圖片
        public void SetImage()
        {
            Bitmap picture2 = new Bitmap(picture);
            picture2.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = picture2;
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

        //輸出多項式
        private void button_OutPolynomial_Click(object sender, EventArgs e)
        {
            if (labelYellow.Text == "" && labelRed.Text == "" && labelGreen.Text == "" && labelBlue.Text == "")
            {
                MessageBox.Show("尚未執行內插!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (checkBoxYellowLine.Checked)
                sw.WriteLine(labelYellowMessage.Text + split + labelYellow.Text);
            if (checkBoxRedLine.Checked)
                sw.WriteLine(labelRedMessage.Text + split + labelRed.Text);
            if (checkBoxGreenLine.Checked)
                sw.WriteLine(labelGreenMessage.Text + split + labelGreen.Text);
            if (checkBoxBlueLine.Checked)
                sw.WriteLine(labelBlueMessage.Text + split + labelBlue.Text);
            sw.Close();
        }

        List<PointF> all_R = new List<PointF>(), all_G = new List<PointF>(), all_B = new List<PointF>();
        //輸出點座標
        private void button_OutPoint_Click(object sender, EventArgs e)
        {
            if (all_R.Count == 0 && all_G.Count == 0 && all_B.Count == 0)
            {
                MessageBox.Show("尚未執行內插!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (all_R.Count != 0)
                {
                    StreamWriter sw = new StreamWriter(folderBrowserDialog.SelectedPath + "//out-R.csv", false, Encoding.UTF8);
                    for (int i = 0; i < all_R.Count; i++)
                    {
                        sw.WriteLine(all_R[i].Y * shrink);
                    }
                    sw.Close();
                }
                if (all_G.Count != 0)
                {
                    StreamWriter sw = new StreamWriter(folderBrowserDialog.SelectedPath + "//out-G.csv", false, Encoding.UTF8);
                    for (int i = 0; i < all_G.Count; i++)
                    {
                        sw.WriteLine(all_G[i].Y * shrink);
                    }
                    sw.Close();
                }
                if (all_B.Count != 0)
                {
                    StreamWriter sw = new StreamWriter(folderBrowserDialog.SelectedPath + "//out-B.csv", false, Encoding.UTF8);
                    for (int i = 0; i < all_B.Count; i++)
                    {
                        sw.WriteLine(all_B[i].Y * shrink);
                    }
                    sw.Close();
                }
            }
        }

#if (UseBigFloat)
        //浮點大數型別
        public struct BigFloat
        {
            /*
             * 整數部分、小數部分
             * 12345.6789
             * = > ii[0] = 5、ii[1] = 4、ii[2] = 3...
             * = > fl[0] = 6、fl[1] = 7、fl[2] = 8...
             */
            public sbyte[] ii, fl;
            //整數長度、小數長度
            public int l1, l2;
            /*
             * 正負號
             * 1 為正，-1 為負
            */
            public int fd;
        }

        //浮點數轉浮點大數
        public BigFloat cv_float_BigFloat(float ipt)
        {
            BigFloat ou;
            ou.ii = new sbyte[1000];
            ou.fl = new sbyte[1000];
            string test = ipt.ToString();
            string[] st;
            if (test.IndexOf("E") != -1)
            {
                //去掉科學記號
                st = decimal.Parse(test, System.Globalization.NumberStyles.Any).ToString().Split('.');
            }
            else
            {
                st = test.Split('.');
            }
            if (ipt >= 0)
                ou.fd = 1;
            else
            {
                ou.fd = -1;
                //字串最前面的 - 要拿掉
                st[0] = st[0].Substring(1);
            }
            if (st[0].Length == 1 && st[0].Substring(0, 1) == "0")
            {
                ou.l1 = 1;
                ou.ii[0] = 0;
            }
            else
            {
                ou.l1 = st[0].Length;
                for (int i = 0; i < ou.l1; i++)
                {
                    ou.ii[i] = sbyte.Parse(st[0].Substring(ou.l1 - i - 1, 1));
                }
            }
            if (st.Length == 1)
            {
                ou.l2 = 1;
                ou.fl[0] = 0;
            }
            else
            {
                ou.l2 = st[1].Length;
                for (int i = 0; i < ou.l2; i++)
                {
                    ou.fl[i] = sbyte.Parse(st[1].Substring(i, 1));
                }
            }
            return ou;
        }

        //浮點大數轉浮點數
        public float cv_BigFloat_float(BigFloat ipt)
        {
            float ou = 0;
            for (int i = 0; i < ipt.l1; i++)
            {
                ou += ipt.ii[i] * (float)Math.Pow(10, i);
            }
            for (int i = 0; i < ipt.l2; i++)
            {
                ou += (float)ipt.fl[i] * (float)Math.Pow(10, -(i + 1));
            }
            return ou * (ipt.fd);
        }

        //重新生成浮點大數
        public BigFloat ToBigFloat(BigFloat ipt)
        {
            BigFloat ou;
            ou.ii = new sbyte[1000];
            ou.fl = new sbyte[1000];
            ou.fd = ipt.fd;
            ou.l1 = ipt.l1;
            ou.l2 = ipt.l2;
            for (int i = 0; i < ipt.l1; i++)
            {
                ou.ii[i] = ipt.ii[i];
            }
            for (int i = 0; i < ipt.l2; i++)
            {
                ou.fl[i] = ipt.fl[i];
            }
            return ou;
        }

        //浮點大數加
        public BigFloat BigFloat_add(BigFloat bb1, BigFloat bb2)
        {
            BigFloat b1, b2;
            b1 = ToBigFloat(bb1);
            b2 = ToBigFloat(bb2);
            BigFloat ou;
            ou.ii = new sbyte[1000];
            ou.fl = new sbyte[1000];
            int i, j;
            if (b1.fd == -1)
            {
                for (i = 0; i < b1.l1; i++)
                    b1.ii[i] *= -1;
                for (i = 0; i < b1.l2; i++)
                    b1.fl[i] *= -1;
            }
            if (b2.fd == -1)
            {
                for (i = 0; i < b2.l1; i++)
                    b2.ii[i] *= -1;
                for (i = 0; i < b2.l2; i++)
                    b2.fl[i] *= -1;
            }

            //清空資料
            int max1 = b1.l1 > b2.l1 ? b1.l1 : b2.l1;
            int max2 = b1.l2 > b2.l2 ? b1.l2 : b2.l2;
            ou.l1 = 0;
            ou.l2 = 0;
            for (i = 0; i < 130; i++)
                ou.ii[i] = 0;
            for (i = 130 - 1; i >= 0; i--)
                ou.fl[i] = 0;

            for (i = max2 - 1; i >= 0; i--)
            {
                if (i < b1.l2)
                    ou.fl[i] += b1.fl[i];
                if (i < b2.l2)
                    ou.fl[i] += b2.fl[i];
                if (ou.fl[i] >= 10)
                {
                    ou.fl[i] -= 10;
                    if (i != 0)
                        ou.fl[i - 1]++;
                    else
                        ou.ii[0]++;
                }
                else if (ou.fl[i] <= -10)
                {
                    ou.fl[i] += 10;
                    if (i != 0)
                        ou.fl[i - 1]--;
                    else
                        ou.ii[0]--;
                }
                if (ou.fl[i] != 0 && i + 1 > ou.l2)
                    ou.l2 = i + 1;
            }
            for (i = 0; i < max1; i++)
            {
                if (i < b1.l1)
                    ou.ii[i] += b1.ii[i];
                if (i < b2.l1)
                    ou.ii[i] += b2.ii[i];
                if (ou.ii[i] >= 10)
                {
                    ou.ii[i] -= 10;
                    ou.ii[i + 1]++;
                    if (ou.ii[i + 1] != 0 && i + 2 > ou.l1)
                        ou.l1 = i + 2;
                }
                else if (ou.ii[i] <= -10)
                {
                    ou.ii[i] += 10;
                    ou.ii[i + 1]--;
                    if (ou.ii[i + 1] != 0 && i + 2 > ou.l1)
                        ou.l1 = i + 2;
                }
                if (ou.ii[i] != 0 && i + 1 > ou.l1)
                    ou.l1 = i + 1;
            }
            if (ou.l1 == 0)
                ou.l1 = 1;
            ou.fd = ou.ii[ou.l1 - 1] >= 0 ? 1 : -1;
            if (ou.fd == 1)
            {
                for (i = ou.l2 - 1; i >= 0; i--)
                {
                    if (ou.fl[i] < 0)
                    {
                        ou.fl[i] += 10;
                        if (i != 0)
                            ou.fl[i - 1]--;
                        else
                            ou.ii[0]--;
                    }
                }
                for (i = 0; i < ou.l1; i++)
                    if (ou.ii[i] < 0)
                    {
                        ou.ii[i] += 10;
                        ou.ii[i + 1]--;
                    }
            }
            else
            {
                //printf("負數\n");
                for (i = ou.l2 - 1; i >= 0; i--)
                {
                    if (ou.fl[i] > 0)
                    {
                        ou.fl[i] -= 10;
                        if (i != 0)
                            ou.fl[i - 1]++;
                        else
                            ou.ii[0]++;
                    }
                }
                for (i = ou.l2 - 1; i >= 0; i--)
                    ou.fl[i] *= -1;
                for (i = 0; i < ou.l1; i++)
                    if (ou.ii[i] > 0)
                    {
                        ou.ii[i] -= 10;
                        ou.ii[i + 1]++;
                    }
                for (i = 0; i < ou.l1; i++)
                    ou.ii[i] *= -1;
            }

            //抓數值最大長度
            int max_l = 0;
            for (i = 0; i < 130; i++)
                if (ou.ii[i] != 0 && i > max_l)
                    max_l = i;
            ou.l1 = max_l + 1;
            max_l = -1;
            for (i = 0; i < 130; i++)
                if (ou.fl[i] != 0 && i > max_l)
                    max_l = i;
            ou.l2 = max_l + 1;

            return ou;
        }

        //浮點大數乘
        public BigFloat BigFloat_mul(BigFloat bb1, BigFloat bb2)
        {
            BigFloat b1, b2;
            b1 = ToBigFloat(bb1);
            b2 = ToBigFloat(bb2);
            BigFloat ou;
            ou.ii = new sbyte[1000];
            ou.fl = new sbyte[1000];
            ou.fd = b1.fd * b2.fd;
            int i, j;
            for (i = 0; i < 130; i++)
                ou.ii[i] = 0;
            for (i = 130 - 1; i >= 0; i--)
                ou.fl[i] = 0;

            //小數運算
            for (i = b1.l2 - 1; i >= 0; i--)
            {
                for (j = b2.l2 - 1; j >= 0; j--)
                {
                    ou.fl[i + j + 1] += (sbyte)(b1.fl[i] * b2.fl[j]);
                    //printf("i:%d, j:%d, ou.fl:%d\n",i,j,ou.fl[i+j+1]);
                    while (ou.fl[i + j + 1] >= 10)
                    {
                        ou.fl[i + j + 1] -= 10;
                        if (i + j + 1 - 1 >= 0)
                            ou.fl[i + j + 1 - 1]++;
                        else
                            ou.ii[0]++;
                    }
                }
                //小數配整數
                for (j = 0; j < b2.l1; j++)
                {
                    if (j > i)
                    {
                        ou.ii[j - i - 1] += (sbyte)(b1.fl[i] * b2.ii[j]);
                        while (ou.ii[j - i - 1] >= 10)
                        {
                            ou.ii[j - i - 1] -= 10;
                            ou.ii[j - i - 1 + 1]++;
                        }
                    }
                    else
                    {
                        ou.fl[i - j] += (sbyte)(b1.fl[i] * b2.ii[j]);
                        while (ou.fl[i - j] >= 10)
                        {
                            ou.fl[i - j] -= 10;
                            if (i - j - 1 >= 0)
                                ou.fl[i - j - 1]++;
                            else
                                ou.ii[0]++;
                        }
                    }
                }
            }

            //整數運算
            for (i = 0; i < b1.l1; i++)
            {
                for (j = 0; j < b2.l1; j++)
                {
                    ou.ii[i + j] += (sbyte)(b1.ii[i] * b2.ii[j]);
                    while (ou.ii[i + j] >= 10)
                    {
                        ou.ii[i + j] -= 10;
                        ou.ii[i + j + 1]++;
                    }
                }
                //整數配小數
                for (j = b2.l2 - 1; j >= 0; j--)
                {
                    if (i > j)
                    {
                        ou.ii[i - j - 1] += (sbyte)(b1.ii[i] * b2.fl[j]);
                        while (ou.ii[i - j - 1] >= 10)
                        {
                            ou.ii[i - j - 1] -= 10;
                            ou.ii[i - j - 1 + 1]++;
                        }
                    }
                    else
                    {
                        ou.fl[j - i] += (sbyte)(b1.ii[i] * b2.fl[j]);
                        while (ou.fl[j - i] >= 10)
                        {
                            ou.fl[j - i] -= 10;
                            if (j - i - 1 >= 0)
                                ou.fl[j - i - 1]++;
                            else
                                ou.ii[0]++;
                        }
                    }
                }
            }

            //小數修正
            ou.l2 = b1.l2 + b2.l2;
            for (i = 129; i >= 0; i--)
                while (ou.fl[i] >= 10)
                {
                    ou.fl[i] -= 10;
                    if (i != 0)
                        ou.fl[i - 1]++;
                    else
                        ou.ii[0]++;
                }

            //整數修正
            ou.l1 = b1.l1 + b2.l1;
            for (i = 0; i < 130; i++)
                while (ou.ii[i] >= 10)
                {
                    ou.ii[i] -= 10;
                    ou.ii[i + 1]++;
                }

            //抓數值最大長度
            int max_l = -1;
            for (i = 130 - 1; i >= 0; i--)
                if (ou.fl[i] != 0 && i > max_l)
                    max_l = i;
            ou.l2 = max_l + 1;

            //抓數值最大長度
            max_l = -1;
            for (i = 0; i < 130; i++)
                if (ou.ii[i] != 0 && i > max_l)
                    max_l = i;
            ou.l1 = max_l + 1;

            return ou;
        }
#endif

    }
}
