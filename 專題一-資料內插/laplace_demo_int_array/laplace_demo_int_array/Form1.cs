using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace laplace_demo_int_array
{
    public partial class Form1 : Form
    {
        const long Maxn = 2000;
        long[] miss_Num = new long[Maxn ];
        const long All_elem = Maxn * Maxn ;
        int miss_elem = 0 ;

        public void randow_array()
        {
            Random rnd = new Random();
            StreamWriter cout = new StreamWriter("./in2002.txt");
            for (int i = 0; i < Maxn; i++)
            {
                for (int j = 0; j < Maxn; j++) {
                    int LOP;
                    LOP = rnd.Next(100);
                    if (LOP % 2 == 0)
                        LOP = 0;
                    cout.Write(LOP + " ");
                }
                cout.WriteLine( );
            }
            cout.Close();
        }

        public Form1()
        {
            InitializeComponent();
            randow_array();
            before_text.Text = "OK";
            
        }

        private void Form1_Load(object sender, EventArgs e) {

        }
    }
}
