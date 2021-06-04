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
            gbNMU_X.Visible = false;
            addnmA_or_LP.Visible = false;
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
        int savedSelectedOrderNum = -1;
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

        int ID_U = -1;
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
                groupBox3.Enabled = true;
                dgvSetsData.Enabled = true;
            }

        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Update_dgvSetsData(ID_U);
            selected_A = -1;
            selected_X = -1;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            Update_dgvSetsData(ID_U);
            selected_A = -1;
            selected_X = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cansel();
            ID_U = -1;
            selected_A = -1;
            selected_X = -1;
        }
        public void Cansel()
        {
            gbFreeOrders.Enabled = true;
            groupBox1.Enabled = false;
            dgvSetsData.Enabled = false;
            groupBox3.Enabled = false;
        }

        private void CloseOrderBtn_Click(object sender, EventArgs e)
        {
            if (savedSelectedOrderNum == -1)
            {
                MessageBox.Show("Не выбран заказ");
                return;
            }
            DBConnectCreator.OrderClose(sqlConnString, savedSelectedOrderNum); //savedSelectedOrderNum;
        }

        public int selected_A = -1;
        public int selected_X = -1;
        private void dgvSetsData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && radioButton8.Checked && Int32.TryParse((dgvSetsData[1, e.RowIndex].Value.ToString()), out int selected_val))
            {
                if (radioButton9.Checked)
                    selected_A = selected_val;
                else
                    selected_X = selected_val;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            U_btnStatChange = true;
            gbNMU_XCheckChanged();
        }
        public bool isSelectToAdd_notUpdate_nmaLp;

        private void button7_Click(object sender, EventArgs e)
        {
            isSelectToAdd_notUpdate_nmaLp = true;
            StatChanger_addnmA_or_LP();
            chb(true);
        }
        private NmA_or_nmLP S_nmA_or_nmLP(out bool exept)
        {
            exept = false;
            NmA_or_nmLP nmA_Or_NmLP = new NmA_or_nmLP();

            if (ismnA)
            {
                nmA_Or_NmLP.Is_nmA = true;
                nmA_Or_NmLP.ID_nmU_or_nmX = ID_U;
            }
            else
            {
                nmA_Or_NmLP.ID_nmU_or_nmX = selected_X;
                nmA_Or_NmLP.Is_nmA = false;
            }

            if (isSelectToAdd_notUpdate_nmaLp)
                nmA_Or_NmLP.Is_needToAdd_notUpdate = true;
            else
            {
                nmA_Or_NmLP.Is_needToAdd_notUpdate = false;
                if (ismnA)
                    nmA_Or_NmLP.ID_nmA_nmLP = selected_A;
                else
                    nmA_Or_NmLP.ID_nmA_nmLP = selected_X;
            }

            if (cbnma_fromFree.Checked && tbfreeNmuID.Text != "")
            {
                nmA_Or_NmLP.FromFree_nmulp = false;
                int.TryParse(tbfreeNmuID.Text, out int idFree);
                nmA_Or_NmLP.IDfree = idFree;
            }
            else
            {
                if (int.TryParse(tbm1.Text, out _) & int.TryParse(tbm2.Text, out _) & int.TryParse(tba.Text, out _) & int.TryParse(tbb.Text, out _))
                {
                    nmA_Or_NmLP.Name = tbNMname.Text;
                    nmA_Or_NmLP.M1 = int.Parse(tbm1.Text);
                    nmA_Or_NmLP.M2 = int.Parse(tbm2.Text);
                    nmA_Or_NmLP.A = int.Parse(tba.Text);
                    nmA_Or_NmLP.B = int.Parse(tbb.Text);
                }
                else
                {
                    MessageBox.Show("Одно из значений трапеции не задано");
                    return nmA_Or_NmLP;
                    exept = true;
                }
            }

            return nmA_Or_NmLP;
        }
 
        public void nmaAdderAdnUpdater(NmA_or_nmLP S_nmA_or_nmLP)
        {
            if ((selected_A == -1 & selected_X == -1) && !isSelectToAdd_notUpdate_nmaLp)
            {
                MessageBox.Show("Не выбрано множество");
                return;
            }
            if (true)
            {

            }
            DBConnectCreator.AddorUpdate_nmA(sqlConnString, S_nmA_or_nmLP);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SetsDeleter(false);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SetsDeleter(true);
        }
        public void SetsDeleter(bool deleteAll)
        {

            //selectedLP_or_A
        }


        private void button12_Click(object sender, EventArgs e)
        {
            if (WasNotSelected_OrderOrFuzSet())
                return;
            SetsParams setsParams = new SetsParams();
            DialogResult dResult = MessageBox.Show("Вы уверены что ходите удалить выбранное множество U?", "!Подтвердите действие!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ;
            if (dResult == DialogResult.No)
                return;
            else
            {
                dResult = MessageBox.Show("Удалить набор ЛП и все множества?  _NO_ удалит набор X и связи. _Canles_ Удалить связь хранилища U c набором ЛП X", "Удаление ЛП", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dResult == DialogResult.Yes)
                    setsParams.dResult_X = 2;
                else if (dResult == DialogResult.No)
                    setsParams.dResult_X = 1;
                else
                    setsParams.dResult_X = 0;

                dResult = MessageBox.Show("Удалить множества? _NO_ удалит только связь с множеством", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dResult == DialogResult.No)
                    setsParams.dRresults_onA = false;
                else
                    setsParams.dRresults_onA = true;
            }
            DBConnectCreator.DeleteSetsU_U(sqlConnString, savedSelectedOrderNum, ID_U, setsParams);
            dgvSelectedOrderData_Update(savedSelectedOrderNum);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private bool U_btnStatChange = false;
        private void button11_Click(object sender, EventArgs e)
        {
            U_btnStatChange = false;
            gbNMU_XCheckChanged();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            gbNMU_X.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            gbNMU_XCheckChanged();
            bool idWasParsed = int.TryParse(ID_mU.Text, out int freeIDnmU);
            if (rbFree.Checked && rbFromFree.Checked)
            {
                MessageBox.Show("Нет смысла переносить свободное мноество/набор из свободного в свободное :|","Логическая ошибка");
                return;
            }
            if (rbFromFree.Checked && !idWasParsed)
            {
                MessageBox.Show("Неправильно введен ID", "ID?");
                return;
            }
            if (rbmX.Checked)
                DBConnectCreator.InsertOrUpdate_nmX(sqlConnString, savedSelectedOrderNum, ID_U, tbDesc.Text, U_btnStatChange, addLikeFree, fromFree_oldnmU, freeIDnmU);
            else
                DBConnectCreator.InsertOrUpdate_nmU(sqlConnString, savedSelectedOrderNum, ID_U, tbDesc.Text, U_btnStatChange, addLikeFree, fromFree_oldnmU, freeIDnmU);
            dgvSelectedOrderData_Update(savedSelectedOrderNum);
            gbNMU_X.Visible = false;
            tbDesc.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CountForm countForm = new CountForm(sqlConnString, savedSelectedOrderNum);
            countForm.Show();
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            gbNMU_XCheckChanged();
        }

        public bool addLikeFree;
        public bool fromFree_oldnmU;
        public bool isSelect_U_notX;

        private bool WasNotSelected_OrderOrFuzSet()
        {
            if (savedSelectedOrderNum == -1)
            {
                MessageBox.Show("Не выбрана заявка");
                Cansel();
                return true;
            }

            if (ID_U == -1 && !U_btnStatChange)
            {
                MessageBox.Show("Не выбрано множество для изменения");
                gbNMU_X.Visible = false;
                return true;
            }
            return false;
        }
        private void gbNMU_XCheckChanged()
        {
            if (WasNotSelected_OrderOrFuzSet())
                return;
            gbNMU_X.Visible = true;
            if (U_btnStatChange) // если добавить
            {
                gbNMU_X.Text = "Добавить";
                rbFree.Enabled = true;
                rbToOrder.Enabled = true;
                rbNew.Enabled = true;
                rbFromFree.Enabled = true;
                if (rbmU.Checked)
                {
                    rbFree.Text = "свободное множество";
                    rbToOrder.Text = "к заявке";
                    rbNew.Text = "новое";
                    lbID.Text = "ID_mU";
                    isSelect_U_notX = true;
                    gbNMU_X.Text += " множество U";
                }
                else//rbmX
                {
                    rbFree.Text = "свободный набор";
                    rbToOrder.Text = "к множеству";
                    rbNew.Text = "новый";
                    lbID.Text = "ID_mX";
                    isSelect_U_notX = false;
                    gbNMU_X.Text += " набор ЛП X";
                }
                if (rbFree.Checked)
                    addLikeFree = true;
                else
                    addLikeFree = false;
                if (rbFromFree.Checked)
                {
                    fromFree_oldnmU = true;
                    ID_mU.Enabled = true;
                    tbDesc.Enabled = false;
                }

                else
                {
                    fromFree_oldnmU = false;
                    ID_mU.Enabled = false;
                    tbDesc.Enabled = true;
                }

            }
            else //если изменить
            {
                gbNMU_X.Text = "Изменить";
                rbFree.Enabled = false;
                rbToOrder.Enabled = false;
                rbNew.Enabled = false;
                rbFromFree.Enabled = false;
                if (rbmU.Checked)
                {
                    isSelect_U_notX = true;
                    gbNMU_X.Text += " множество U";
                }
                else//rbmX
                {
                    isSelect_U_notX = false;
                    gbNMU_X.Text += " набор ЛП X";
                }

            }

        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            gbNMU_XCheckChanged();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gbNMU_XCheckChanged();
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            gbNMU_XCheckChanged(); ;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            gbNMU_XCheckChanged();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dgvSelectedOrderData_Update(savedSelectedOrderNum);
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            gbNMU_XCheckChanged();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FreeSets freeSets = new FreeSets(sqlConnString);
            freeSets.Show();
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            //textBox5.Enabled = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            addnmA_or_LP.Visible = false;
            selected_A = -1;
            selected_X = -1; 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            isSelectToAdd_notUpdate_nmaLp = false;
            StatChanger_addnmA_or_LP();
            chb(false);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var param = S_nmA_or_nmLP(out bool exept);
            if (exept)
                return;
            nmaAdderAdnUpdater(param);
            selected_A = -1;
            selected_X = -1;
            if (ID_U != -1)
                Update_dgvSetsData(ID_U);
        }

        private void cbnma_fromFree_CheckedChanged(object sender, EventArgs e)
        {
            StatChanger_addnmA_or_LP();
        }

        public bool ismnA;
        private void StatChanger_addnmA_or_LP()
        {
            addnmA_or_LP.Visible = true;
            if (radioButton9.Checked)
                ismnA = true;
            else
                ismnA = false;

            if (cbnma_fromFree.Checked)
                ch(false);
            else
                ch(true);

            void ch(bool b)
            {
                tbfreeNmuID.Enabled = !b;

                tbNMname.Enabled = b;
                gbOldVal.Enabled = b;
                tbm1.Enabled = b;
                tbm2.Enabled = b;
                tba.Enabled = b;
                tbb.Enabled = b;
            }

        }
        void chb(bool b)
        {
            cbnma_fromFree.Checked = b;
            cbnma_fromFree.Enabled = b;
            if (b)
                cbnma_fromFree.Checked = !b;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (ID_U != -1)
                Update_dgvSetsData(ID_U);
        }
    }
}
