using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isFCAwf
{
    public partial class FreeSets : Form
    {
        public FreeSets(string sqlConnString)
        {
            InitializeComponent();
            this.sqlConnString = sqlConnString;
        }
        private string sqlConnString;

        private void FreeSets_Load(object sender, EventArgs e)
        {
            dgvU.AutoGenerateColumns = true;
            dgvX.AutoGenerateColumns = true;
            dgvnmA.AutoGenerateColumns = true;
            dgvnmLPX.AutoGenerateColumns = true;
            bindingSourceU.DataSource = DBConnectCreator.GetFreeFuzzySets_U(sqlConnString);
        }
        public int selected_U;
        public int selected_X;
        private void dgvU_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected_U;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && Int32.TryParse((dgvU[0, e.RowIndex].Value.ToString()), out selected_U))
            {
                dgvX_Update();
                dgvnmA_Update();
                this.selected_U = selected_U;
            }
        }

        public void dgvX_Update()
        {
            bindingSourceU.DataSource = DBConnectCreator.GetFreeFuzzySets_ofSet_X(sqlConnString, selected_U, checkBox1.Checked);
        }
        public void dgvnmA_Update()
        {
            bindingSourceNMA.DataSource = DBConnectCreator.GetFreeFuzzySets_A(sqlConnString, selected_U, checkBox2.Checked);
        }
        public void dgvnmLPX_Update()
        {
            bindingSourceNML_PX.DataSource = DBConnectCreator.GetFreeFuzzySets_LPX(sqlConnString, selected_X, checkBox3.Checked);
        }
        private void dgvX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected_X;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && Int32.TryParse((dgvU[0, e.RowIndex].Value.ToString()), out selected_X))
            {
                dgvnmLPX_Update();
                this.selected_X = selected_X;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dgvX_Update();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            dgvnmA_Update();
        }
    }
}
