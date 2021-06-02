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
    public partial class SetsExpertForm : Form
    {
        public Form formToOpen;
        private string sqlConnString;
        private int id;
        //int selectedOrderNum = -1;
        public SetsExpertForm(string stringConn, int id, AuthorizationForm authForm)
        {
            InitializeComponent();
            formToOpen = authForm;
            sqlConnString = stringConn;
            this.id = id;
        }

        private void SetsExpertForm_Load(object sender, EventArgs e)
        {
            //groupBox1.Enabled = false;


            //dgvOrders.DataSource = bsOrders;
            dgvSelectedOrderData.DataSource = bsSelectedOrderData;
            dgvSetsData.DataSource = bsSetsData;

            dgvOrders.ReadOnly = true;
            dgvOrders.ReadOnly = true;
            dgvOrders.ReadOnly = true;

            dgvOrders.AutoGenerateColumns = true;
            dgvSelectedOrderData.AutoGenerateColumns = true;
            dgvSetsData.AutoGenerateColumns = true;

            dgvOrders.AutoResizeColumns();
            //либо это (для столбцов по содержимому) dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.Text = DBConnectCreator.GetSpecialistData(sqlConnString, id);
            dgvOrders_Update();
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
        }



        public void dgvOrders_Update()
        {
            int sqlqueryPar = 0;
            bool isDateFilter = false;
            DateTime minDate = dateTimePicker1.Value;
            DateTime maxDate = dateTimePicker2.Value;
            if (radioButton1.Checked)
                sqlqueryPar = 1;
            if (radioButton2.Checked)
                sqlqueryPar = 2;
            if (radioButton4.Checked)
                sqlqueryPar = 3;
            if (radioButton5.Checked)
                sqlqueryPar = 4;
            if (checkBox1.Checked)
                isDateFilter = true;
            gbWithFirstDGV_Update(sqlqueryPar);
            var ordersTable = DBConnectCreator.GetOrdersTableToMaster(sqlConnString, id, sqlqueryPar, minDate, maxDate, isDateFilter);
            bsOrders.DataSource = ordersTable;
            dgvOrders.AutoResizeColumns();
        }
        public void gbWithFirstDGV_Update(int dgvChecked_nom)
        {
            switch (dgvChecked_nom)
            {
                case 1:
                    UnFreeOrderBtn.Enabled = true;
                    UnFreeOrderBtn.Text = "Принять";
                    CloseOrderBtn.Enabled = false;
                    break;
                case 2:
                    UnFreeOrderBtn.Enabled = true;
                    UnFreeOrderBtn.Text = "Выбрать";
                    CloseOrderBtn.Enabled = true;
                    break;
                case 3:
                    UnFreeOrderBtn.Enabled = true;
                    UnFreeOrderBtn.Text = "Смотреть";
                    CloseOrderBtn.Enabled = false;
                    break;
                default:
                    UnFreeOrderBtn.Enabled = false;
                    UnFreeOrderBtn.Text = "";
                    CloseOrderBtn.Enabled = false;
                    break;
            }
            dgv1stat = dgvChecked_nom;
        }
        public int dgv1stat = new int();
        private void BackToTheAuthorizeBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetsExpertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            formToOpen.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dgvOrders_Update();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dgvOrders_Update();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            dgvOrders_Update();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            dgvOrders_Update();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dgvOrders_Update();
        }

        private void dgwFrOrUpdaterBtn_Click(object sender, EventArgs e)
        {
            dgvOrders_Update();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            dgvOrders_Update();
        }

        private void BackToTheAuthorizeBTN_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedOrderNum;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && Int32.TryParse((dgvOrders[0, e.RowIndex].Value.ToString()), out selectedOrderNum))
            {
                dgvSelectedOrderData_Update(selectedOrderNum);
                savedSelectedOrderNum = selectedOrderNum;
            }
        }
        int savedSelectedOrderNum = new int();
        int gb2Status = -1;
        public void dgvSelectedOrderData_Update(int selectedOrderNum)
        {
            DataTable dtFor_dgvSelectedOrderData = new DataTable();
            if (radioButton6.Checked)
            {
                dtFor_dgvSelectedOrderData = DBConnectCreator.GetCompanyDataToMaster(sqlConnString, selectedOrderNum);
                gb2Status = 1;
            }
            if (radioButton7.Checked)
            {
                dtFor_dgvSelectedOrderData = DBConnectCreator.GetFIOsTable(sqlConnString, selectedOrderNum);
                gb2Status = 2;
            }
            if (radioButton8.Checked)
            {
                dtFor_dgvSelectedOrderData = DBConnectCreator.GetFuzzySet_U(sqlConnString, selectedOrderNum);
                gb2Status = 3;
            }
            bsSelectedOrderData.DataSource = dtFor_dgvSelectedOrderData;
            dgvSelectedOrderData.DataSource = bsSelectedOrderData;
            dgvSelectedOrderData.AutoResizeColumns();
            groupBox1.Text = "Выбрана заявка № :" + selectedOrderNum.ToString();
            //bsOrders.DataSource = ordersTable;
            dgvOrders.AutoResizeColumns();
            //UpdateGB2();
        }
        public void Checkgb2Status()
        {
            switch (gb2Status)
            {
                case 1:
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            //dataGridViewFIO.DataSource = bindingSourceSecTab;
        }
        public void UpdateGB2(bool b)
        {
            if (b)
            {

            }

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            dgvSelectedOrderData_Update(savedSelectedOrderNum);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            dgvSelectedOrderData_Update(savedSelectedOrderNum);
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            dgvSelectedOrderData_Update(savedSelectedOrderNum);
        }

        int ID_U = new int();
        private void dgvSelectedOrderData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && radioButton8.Checked && Int32.TryParse((dgvSelectedOrderData[0, e.RowIndex].Value.ToString()), out int kodM_U))
            {
                ID_U = kodM_U;
                Update_dgvSetsData(kodM_U);
            }
        }

        public void Update_dgvSetsData(int kodM_U)
        {
            // if()...
            DataTable dtFor_dgvSetsData = new DataTable();
            if (radioButton9.Checked)
            {
                dtFor_dgvSetsData = DBConnectCreator.GetFuzzySetsOfSet_A(sqlConnString, kodM_U);
            }
            else if (radioButton10.Checked)
            {
                dtFor_dgvSetsData = DBConnectCreator.GetFuzzySetsOfLingVar_X(sqlConnString, kodM_U);
            }
            dgvSetsData.DataSource = dtFor_dgvSetsData;
            dgvSetsData.AutoResizeColumns();
        }

        private void UnFreeOrderBtn_Click(object sender, EventArgs e)
        {
            if (dgv1stat == 1)
            {
                DBConnectCreator.OrderOwnerToDungeonMaster(sqlConnString, id, savedSelectedOrderNum); //savedSelectedOrderNum;
                dgvOrders_Update();
            }
            if (dgv1stat == 2)
            {
                gbFreeOrders.Enabled = false;
                groupBox1.Enabled = true;
            }

        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Update_dgvSetsData(ID_U);
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            Update_dgvSetsData(ID_U);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gbFreeOrders.Enabled = true;
            groupBox1.Enabled = false;
        }

        private void CloseOrderBtn_Click(object sender, EventArgs e)
        {
          //  DBConnectCreator.OrderClose(sqlConnString, id, savedSelectedOrderNum); //savedSelectedOrderNum;
        }
    }
}
