using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isFCAwf
{
    public partial class CountForm : Form
    {
        string sqlStringConn;
        private int orderNum;
        public CountForm( string sqlStringConn, int orderNum)
        {
            InitializeComponent();
            this.sqlStringConn = sqlStringConn;
            this.orderNum = orderNum;
        }
        private List<double> variableNM = new List<double>(10);
        private void CountForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "$Источник A$+$Источник B$+$Источник C$-$Долги D$";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //AnalizatorStroki(textBox1.Text);
            FuzzyCounter fc = new FuzzyCounter();
            double[] dmas = fc.GetRezult(textBox1.Text);
            string s = "";
            for (int i = 0; i < dmas.Length; i++)
            {
                s += dmas[i].ToString() + "; ";
                variableNM.Add(dmas[i]);
            }
            textBox2.Text = s;

        }
    }
}
