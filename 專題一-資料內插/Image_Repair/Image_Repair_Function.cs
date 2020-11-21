using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Diagnostics;   //計時器

//轉換高斯
//using david_math;
using Gauss;

namespace Image_repair
{
    public partial class Form1 : Form
    {
        //將LoadData轉換成矩陣，用高斯消去法求解後輸出
        public void Convert_Gauss(object sender, EventArgs e)
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
                                    if (p_new.X < 0 || p_new.X >= height || p_new.Y < 0 || p_new.Y >= width || OutputData[p_new.X,p_new.Y] == -1 )
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
                                if (p_new.X < 0 || p_new.X >= height || p_new.Y < 0 || p_new.Y >= width || OutputData[p_new.X,p_new.Y] == -1 )
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
                        gauss_run.Seidel_method(1.1, -1);  //賽德爾疊代
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
            //dataGridViewOutput_ShowTxt(goed_map);

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
            if (data[x, y] == 0)
                return false;
            return true;
        }
    }
}
