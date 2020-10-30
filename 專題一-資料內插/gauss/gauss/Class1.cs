using System;
using System.Globalization;

namespace Gauss {
    public class gauss
    {
        public int n;
        public double[,] douGauss;
        public double[] ans;

        public gauss(int n_set, double[,] douGauss_set, double[] ans_set)
        {
            n = n_set;
            douGauss = douGauss_set;
            ans = ans_set;
        }

        private void prepare()
        {
            //prepare
            Console.Write("請輸入階數\n");

            n = Convert.ToInt32(Console.ReadLine());
            douGauss = new double[n, n + 1];
            ans = new double[n + 1];
            Console.Write("輸入每行係數\n");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    douGauss[i, j] = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("{0} {1} {2}", i, j, douGauss[i, j]);
                }
                Console.Write("此行輸入完成!\n");
            }
        }

        public void elimindation()
        { //消元
            for (int k = 0; k < n - 1; k++)
            { //以第 k 行為主消元
                for (int i = k + 1; i < n; i++)
                { // 其他行消元
                    double gk = douGauss[i, k] / douGauss[k, k];
                    for (int j = k; j <= n; j++)
                    { // j 元消去中
                        douGauss[i, j] -= douGauss[k, j] * gk;
                    }
                }
                //print_array(k) ;
            }
            for (int k = n - 1; k >= 0; k--)
            {
                double sum = 0;
                for (int i = k + 1; i < n; i++)
                {
                    sum += douGauss[k, i] * ans[i];
                }
                ans[k] = (douGauss[k, n] - sum) / douGauss[k, k];
            }
            //print_ans();
        }

        private Boolean check_iter(double[,] iter)
        {
            for (int i = 0; i < n; i++)
            {
                double calcu = Math.Abs(iter[1, i] - iter[0, i]);
                if (calcu <= 1e-6)
                    return false;
            }
            return true;
        }


        public void Seidel_method(double omega = 1.1 , int it = -1)
        {
            double[,] iter = new double[10, n + 5];
            if (it == -1)
                it = 2147483647;
            int now_k, old_k, k = 1;
            for (int i = 0; i <= n; i++)
                iter[0, i] = 0;

            double sum = 0;
            do
            {
                now_k = k % 2;
                old_k = (k + 1) % 2;
                for (int i = 0; i < n; i++)
                {
                    sum = douGauss[i, n];

                    //用 負號是移項問題
                    for (int j = 0; j < i; j++)
                        sum -= iter[now_k, j] * douGauss[i, j];

                    for (int j = i + 1; j < n; j++)
                        sum -= iter[old_k, j] * douGauss[i, j];

                    iter[now_k, i] = (1-omega) * iter[old_k, i]  + omega * sum / douGauss[i, i] ;
                    //Console.Write(" {0,10:f4} /", sum);
                    //Console.Write(" {0,8:f4} \n", douGauss[i, i]);
                }

                //print_iter(iter, k , now_k);
                k += 1;
            } while (check_iter(iter) && k < it);
            //&& check_iter(iter , k)
            //print_ans();
            for (int i = 0; i < n; i++)
                ans[i] = iter[now_k, i];
        }

        private void print_iter(double[,] iter, int k, int now_k)
        {
            Console.WriteLine("第 {0} 次消元 \n", k);
            for (int i = 0; i < n; i++)
                Console.Write("{0,10:f2} ", iter[now_k, i]);
            Console.Write("\n");
        }

        private void print_array(int k)
        {
            Console.WriteLine("第 {0} 次消元 \n", k + 1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    Console.Write("{0,10:f2} ", douGauss[i, j]);
                }
                Console.Write("\n");
            }
        }

        private void print_ans()
        {
            for (int i = 0; i < n; i++)
                Console.Write("x{0} = {1}\n", i + 1, ans[i]);
            Console.Write("\n");
        }

        /* CG 共厄梯度法並未研究完成
        double[] b , x , r , d , ad ;
        
        private double dot(double[] x , double[] y)
        {
            double sum = 0 ; 
            for(int i = 0 ; i < n ; i++) {
                sum += x[i] * y[i] ;
            }
            return sum ;
        }

        private void evaluate(double[] x , double[] y)
        {
            for(int i = 0 ; i < n ; i++){ 
                y[i] = 0 ;
                for(int j = 0 ; j < n ; j++) {
                    y[i] += douGauss[i,j] * x[j] ;
                }
            }
        }

        public void Conjugate_Gradient_Method()
        {
            // b = gauss組的常數
            ans = new double[n] ;
            r = new double[n] ;
            d = new double[n] ;
            ad = new double[n] ;
            Console.Write("進入CG\n");
            for(int i = 0 ; i < n ; i++){ 
                d[i] = douGauss[i,n] ;
                r[i] = douGauss[i,n] ;
            }
            double rr = dot(r,r);
            if(rr < 1e-10)
                return ;
            Console.Write("準備進入iter\n");
            for(int k = 0 ; k < n ; k++) {
                evaluate(d,ad);
                double alpha = rr / dot(d,ad);
                for(int i = 0 ; i < n ; i++) {
                    ans[i] += alpha * d[i] ;
                    r[i] -= alpha *ad[i] ;
                }
                double rrnew = dot(r,r);
                Console.Write("rrnew = {0}\n" , rrnew);
                if(rrnew <1e-10){ 
                    for(int i = 0;  i < n ; i++) {
                        Console.Write("{0} " , ans[i] );
                    }
                    Console.WriteLine();
                    return ;
                 }
                    
                double beta = rrnew / rr; 
                for(int i = 0 ; i < n ; i++)
                    d[i] = r[i] + beta * d[i] ;
                rr = rrnew ;
            }
        }
        //CG 共厄梯度法並未研究完成
        */ 

        

    }
}
