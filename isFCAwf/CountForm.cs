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

            LoadCbox_with_nmU();
        }
        private bool cbIsLoad = false;
        private bool lbofNmaIsLoad = false;
        private int selected_nmU;
        private int selected_nmA;
        private void LoadCbox_with_nmU()
        {
            var dtcbOFnmU = DBConnectCreator.GetnmuONorderNum(sqlStringConn, orderNum);
            cbnmU.DataSource = dtcbOFnmU; //.DefaultView
            cbnmU.ValueMember = "Код_М_U";
            cbnmU.DisplayMember = "Описание";// "Код_М_U"
            cbnmU.DataBindings.Add("SelectedValue", dtcbOFnmU.DefaultView, "Код_М_U");
            cbnmU.SelectedIndex = 0;
            LoadListNMAs();
            cbIsLoad = true;
            SomeShitMethodGovna();
        }

        DataTable dt_nmAs;
        List<NmA_or_nmLP> collectOFnma = new List<NmA_or_nmLP>();
        private void LoadListNMAs()
        {
            lbofNmaIsLoad = false;
            dt_nmAs = DBConnectCreator.GetNmAonMnU(sqlStringConn, int.Parse(cbnmU.SelectedValue.ToString()));
            lbofNma.DataSource = dt_nmAs;
            lbofNma.ValueMember = "Код_НМ";
            lbofNma.DisplayMember = "Имя_НМ";
            lbofNmaIsLoad = true;
            //cbnmU.DataBindings.Add("SelectedValue", dtcbOFnmU.DefaultView, "Код_М_U");
            var drtNmA = dt_nmAs.Rows;
            for (int i = 0; drtNmA.Count != 0 & i < drtNmA.Count; i++)
            {
                NmA_or_nmLP nma = new NmA_or_nmLP();
                nma.ID_nmA_nmLP = (int)drtNmA[i]["Код_НМ"];
                nma.ID_nmU_or_nmX = (int)drtNmA[i]["Код_М_U"];
                nma.Name = (string)drtNmA[i]["Имя_НМ"];
                nma.M1 = (int)drtNmA[i]["m1"];
                nma.M2 = (int)drtNmA[i]["m2"];
                nma.A = (int)drtNmA[i]["a"];
                nma.B = (int)drtNmA[i]["b"];
                collectOFnma.Add(nma);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbresCount.Items.Clear();
            //AnalizatorStroki(textBox1.Text);
            FuzzyCounter fc = new FuzzyCounter();
            fc.nmu = selected_nmU;
            fc.sqlstringConn = sqlStringConn;
            double[] dmas = fc.GetRezult(textBox1.Text, out List<string> resultsOfCounts);
            string s = "";
            for (int i = 0; i < dmas.Length; i++)
            {
                s += dmas[i].ToString() + "; ";
                variableNM.Add(dmas[i]);
            }
            textBox2.Text = s;
            for (int i = 0; fc.resCount != null & i < fc.resCount.Count; i++)
            {
                lbresCount.Items.Add(fc.resCount[i]);
            }
        }

        private void cbnmU_SelectedIndexChanged(object sender, EventArgs e)
        {
            SomeShitMethodGovna();
        }

        public void SomeShitMethodGovna()
        {
            if (cbIsLoad)
            {
                selected_nmU = int.Parse(cbnmU.SelectedValue.ToString());
                LoadListNMAs();
            }
        }

        private void lbofNma_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbofNmaIsLoad)
            {

                selected_nmA = int.Parse(lbofNma.SelectedValue.ToString());
                tbAval.Text = "$" + collectOFnma[lbofNma.SelectedIndex].Name + "$";
                tbAconName.Text = "{ " + collectOFnma[lbofNma.SelectedIndex].M1 + "; " + collectOFnma[lbofNma.SelectedIndex].M2 + "; " + collectOFnma[lbofNma.SelectedIndex].A + "; " + collectOFnma[lbofNma.SelectedIndex].B + " }";
               // lbofNma.SelectedValue  + "; " + 
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
