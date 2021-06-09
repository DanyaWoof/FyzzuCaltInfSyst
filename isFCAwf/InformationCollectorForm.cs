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

    public partial class InformationCollectorForm : Form
    {
        public int orderNum = -1;
        public int orderNumToAddClient = -1;
        public Form formToOpen;
        public int id;
        public string sqlConnString;
        int changeParams = 0;
        int dataGirdSecTabStatus = 0; //для удаления
        List<int> clientsIDBofferList = new List<int>(2);
        public InformationCollectorForm(string stringConn, int id, AuthorizationForm prevForm)
        {
            InitializeComponent();
            formToOpen = prevForm;
            this.id = id;
            sqlConnString = stringConn;
        }

        private void InformationCollectorForm_Load(object sender, EventArgs e)
        {
            this.Text = DBConnectCreator.GetSpecialistData(sqlConnString, id);
            OrdersDataGridView.AutoGenerateColumns = true;
            dataGridViewFIO.AutoGenerateColumns = true;
            UpdateMainTable();
            OrdersDataGridView.ReadOnly = true;
            FormAction(true, 410, true);
        }

        Size MyFormSize;
        Size dtgrwHeight;
        public void FormAction(bool b, int Height, bool firstTry)
        {
            int dtgrw = 46;
            int fz = 250;
            if (b)
                fz = -fz;
            else
                dtgrw = -dtgrw;

            if (firstTry)
            {
                MyFormSize.Height += Height;
                dtgrwHeight.Height = 313;
                dtgrwHeight.Width = 386;
                dtgrw = 0;
                fz = 0;
            }
            AddOrderBtn.Enabled = b;
            CangeOrderBtn.Enabled = b;
            groupBoxOrdersView.Visible = !b;
            MyFormSize.Height += fz;
            MyFormSize.Width = Size.Width;
            this.Size = MyFormSize;
            ReloaderSecontTableBtn.Visible = !b;
            DeleterClientFromOrders.Visible = !b;
            tbClientFioForSearch.Visible = !b;
            FinderClientsOnFIOBtn.Visible = !b;
            dtgrwHeight.Height += dtgrw;

            dataGridViewFIO.Size = dtgrwHeight;


            /*
            AddOrderBtn.Enabled = true;
            CangeOrderBtn.Enabled = true;
            groupBoxOrdersView.Visible = false;
            MyFormSize.Height += Height;
            MyFormSize.Width = Size.Width;
            this.Size = MyFormSize;
            ReloaderSecontTableBtn.Visible = false;
            DeleterClientFromOrders.Visible = false;
            tbClientFioForSearch.Visible = false;
            FinderClientsOnFIOBtn.Visible = false;
            dtgrwHeight.Height = 313;
            dtgrwHeight.Width = OrdersDataGridView.Size.Width;
            dataGridViewFIO.Size = dtgrwHeight;
                      */
        }
 

        private void AddOrderBtn_Click(object sender, EventArgs e)
        {
            dataGirdSecTabStatus = 2;
            CheckedChanged();
            rbComNotReplace.Visible = false;
            CheckBoxValueSetter();
            ClearGroupBoxCells();
            rbNewCompany.Checked = true;
            rbNewClient.Checked = true;
            changeParams = 1;
            groupBoxOrdersView.Text = "Создание новой заявки. Номер новой завки " + (OrdersDataGridView.Rows.Count + 1).ToString();
            OrdersChangerBtn.Text = "Сгенерировать заявку";
            tbCompanyName.Text = "";
            tbComAdress.Text = "";
            tbComOldID.Text = "";
            tbDocID.Text = "";
            FormAction(false, 0, false);
            //
            UpdateMainTable();
        }
        private void CheckBoxValueSetter()
        {
            var comTypesDT = DBConnectCreator.GetCompanyTypes(sqlConnString);
            cbComType.DataSource = comTypesDT.DefaultView;
            cbComType.DisplayMember = "Тип_предприятия";
            cbComType.ValueMember = "Код_типа_предприятия";

            var clientRolesDT = DBConnectCreator.GetClientRoles(sqlConnString);
            cbClientRole.DataSource = clientRolesDT.DefaultView;
            cbClientRole.DisplayMember = "Роль";
            cbClientRole.ValueMember = "Код_роли";

            var ordersStatus = DBConnectCreator.GetOrdersStatuses(sqlConnString);
            cbOrdersStatus.DataSource = ordersStatus.DefaultView;
            cbOrdersStatus.DisplayMember = "Статус_заявки";
            cbOrdersStatus.ValueMember = "Код_статуса_заявки";
        }

        private void OrdersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && Int32.TryParse((OrdersDataGridView[0, e.RowIndex].Value.ToString()), out orderNum))
                UpdateSecondFIOTable();

        }
        private void UpdateMainTable()
        {
            var ordersTable = DBConnectCreator.GetOrdersTable(sqlConnString);
            bindingSourceIC.DataSource = ordersTable;
        }
        private void UpdateSecondFIOTable()
        {
            dataGirdSecTabStatus = 1;
            var secTable = DBConnectCreator.GetFIOsTable(sqlConnString, orderNum);
            bindingSourceSecTab.DataSource = secTable;
            dataGridViewFIO.DataSource = bindingSourceSecTab;
            dataGridViewFIO.AutoResizeColumns();

        }


        private void BackToTheAuthorizeBTN_Click(object sender, EventArgs e)
        {
           //formToOpen.Show();
            Close();
        }

        private void InformationCollectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            formToOpen.Show();
        }

        private void ReloarOrdersBtn_Click(object sender, EventArgs e)
        {
            UpdateMainTable();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CheckedRBClientChanged();
        }

        public Order orderPrewData = new Order();
        private void CangeOrderBtn_Click(object sender, EventArgs e)
        {
            if (orderNum >= 0)
            {
                FormAction(false, 0, false);
                dataGirdSecTabStatus = 2;
                orderNumToAddClient = orderNum;
                rbComNotReplace.Visible = rbComNotReplace.Checked = true;
                CheckedChanged();
                rbOldClient.Checked = true;
                groupBoxOrdersView.Text = "Изменение заявки. Номер изменяемой завки " + orderNum.ToString();
                OrdersChangerBtn.Text = "Подтвердить изменения заявки № " + orderNum.ToString();
                changeParams = 2;
                CheckBoxValueSetter();


                var selectedOrderDT = DBConnectCreator.GetOrdersValuesONNum(sqlConnString, orderNum);
                var rowSelectedOrderDT = selectedOrderDT.Rows[0];
                var orderDetailsView = rowSelectedOrderDT.Table.DefaultView;
                tbCompanyName.Text = (string)rowSelectedOrderDT["Название_предприятия"];
                tbComAdress.Text = (string)rowSelectedOrderDT["Адрес_предприятия"];
                tbComOldID.Text = ((int)rowSelectedOrderDT["ID_предприятия"]).ToString();
                tbDocID.Text = ((int)rowSelectedOrderDT["Код_документа_описания"]).ToString();


                cbComType.DataBindings.Add("SelectedValue", orderDetailsView, "Код_типа_предпрития");
                cbComType.DataBindings.Clear();

                cbOrdersStatus.DataBindings.Add("SelectedValue", orderDetailsView, "Код_статуса_заявки");
                cbOrdersStatus.DataBindings.Clear();

                int tasksID = (int)rowSelectedOrderDT["Код_задачи"];
                switch (tasksID)
                {
                    case 1:
                        chbArCouner.Checked = true;
                        chbFyzFilter.Checked = false;
                        break;
                    case 2:
                        chbArCouner.Checked = false;
                        chbFyzFilter.Checked = true;
                        break;
                    case 3:
                        chbArCouner.Checked = true;
                        chbFyzFilter.Checked = true;
                        break;
                    default:
                        chbArCouner.Checked = false;
                        chbFyzFilter.Checked = false;
                        break;
                }
                orderPrewData = OrderFiller();
                //cbComType.SelectedItem = (string)rowSelectedOrderDT["Адрес_предприятия"];
                //cbComType.DisplayMember = "Тип_предприятия";
                //cbComType.ValueMember = "Код_типа_предприятия";



                //this.Text = ((string)row["Должность"]);
            }

        }

        public Order OrderFiller()
        {
            int task = 0;
            Order order = new Order();
            order.ComIsNeedToReplase = false;
            order.ComIsNeedToAdd = false;
            //блок предприятия
            if (rbNewCompany.Checked )
            {
                order.CompanyName = tbCompanyName.Text;
                order.CompanyAdress = tbComAdress.Text;
                order.CompanyType = (int)cbComType.SelectedValue;
                order.CompanyNewID = -1;
                order.ComIsNeedToReplase = true;
                order.ComIsNeedToAdd = true;
            }
            else if (rbOldCompany.Checked)
                order.CompanyNewID = Int32.Parse(tbComOldID.Text);
            order.CompanyOldID = Int32.Parse(tbComOldID.Text);
            //блок задач
            if (chbArCouner.Checked)
                task = 1;
            if (chbFyzFilter.Checked)
                task = 2;
            if (chbArCouner.Checked && chbFyzFilter.Checked)
                task = 3;
            order.OrderTask = task;
            //блок описания
            order.DescriptionOfTheSubjectArea = tbDesc_ot_SubjArea.Text;
            order.IDdocOfDescriptionOfTheSubjectArea = Int32.Parse(tbDocID.Text);
            order.IdIC = this.id;
            order.orderStatus = (int)cbOrdersStatus.SelectedValue; //cbOrdersStatus.ValueMember; ??
            return order;
        }

        private void OrdersChangerBtn_Click(object sender, EventArgs e)
        {
            if (false == chbArCouner.Checked && false == chbFyzFilter.Checked)
            {
                MessageBox.Show("Не выдраны задачи");
                return;
            }
            if (tbDocID.Text == "")
            {
                MessageBox.Show("А ком_КОД?");
                return;
            }
            if (changeParams == 1)
            {
                if (clientsIDBofferList.Count == 0)
                    if (MessageBox.Show("Создать заказ без прикрепленных клиентов?", "Набор клиентов заявки пуст", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        MessageBox.Show("Добавьте клиентов перед генерацией заявки в правой нижней части окна", "Отмена генерации закза", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                List<int> abortedClients = new List<int>(1);
                DBConnectCreator.OrderInsertInBase(sqlConnString, OrderFiller(), clientsIDBofferList, out abortedClients);
                if (abortedClients.Count != 0)
                {
                    string message = "";
                    for (int i = 0; i < abortedClients.Count; i++)
                        message = abortedClients[i].ToString();
                    MessageBox.Show(message);
                }

                clientsIDBofferList.Clear();
            }
            if (changeParams == 2)
            {
                Int32.TryParse(tbComOldID.Text, out int varib);
                orderPrewData.CompanyNewID = varib;
                DialogResult result = DialogResult.No;
                if (orderPrewData.CompanyNewID != orderPrewData.CompanyOldID)
                    result = MessageBox.Show("Заменить предприятие?", "Подтверждение", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    orderPrewData = OrderFiller();
                if (result == DialogResult.Cancel)
                    return;
                varib = orderPrewData.CompanyOldID;
                orderPrewData = OrderFiller();
                orderPrewData.CompanyOldID = varib;
                DBConnectCreator.UpdateOrder(sqlConnString, orderPrewData, clientsIDBofferList, orderNumToAddClient);
                clientsIDBofferList.Clear();
            }
            UpdateMainTable();
            changeParams = 0;
            ClearGroupBoxCells();
            FormAction(true, 0, false);
        }

        private void rbNewCompany_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }

        private void CheckedChanged()
        {
            if (rbNewCompany.Checked)
            {
                CheckedChanged(true, 0);
                orderPrewData.ComIsNeedToAdd = true;
            }
            if (rbOldCompany.Checked)
                CheckedChanged(false, 0);
            if (rbComNotReplace.Checked)
            {
                CheckedChanged(false, 1);
                orderPrewData.ComIsNeedToReplase = false;//
            }
        }
        private void CheckedRBClientChanged()
        {
            bool b;
            if (rbNewClient.Checked)
                b = true;
            else b = false;
            cbClientRole.Enabled = b;
            tbClientFamilyName.Enabled = b;
            tbClientFirstName.Enabled = b;
            tbClientLastName.Enabled = b;
            tbClientPhoneNum.Enabled = b;

            tbClientOldID.Enabled = !b;
        }
        private void CheckedChanged(bool enable, int c)
        {
            orderPrewData.ComIsNeedToReplase = true;
            tbCompanyName.Enabled = enable;
            tbComAdress.Enabled = enable;
            cbComType.Enabled = enable;

            if (c != 0) enable = !enable;

            tbComOldID.Enabled = !enable;
            tbComNameForSearch.Enabled = !enable;
        }

        private void rbOldCompany_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }

        private void CanselBtn_Click(object sender, EventArgs e)
        {
            ClearGroupBoxCells();
            FormAction(true, 0, false);
        }
        private void ClearGroupBoxCells()
        {
            tbCompanyName.Text = "";
            tbComAdress.Text = "";
            tbComOldID.Text = "";
            tbDocID.Text = "";
            cbOrdersStatus.SelectedIndex = 0;
            cbComType.SelectedIndex = 0;
        }

        private void ReloaderSecontTableBtn_Click(object sender, EventArgs e)
        {
            UpdateSecondFIOTable();
        }

        private void IDComSearcherBtn_Click(object sender, EventArgs e)
        {
            string nameToSearch = tbComNameForSearch.Text;
            dataGirdSecTabStatus = 0;
            var secTable = DBConnectCreator.GetSearchResultCompanyOnName(sqlConnString, nameToSearch);
            if (secTable.Rows.Count == 0)
                MessageBox.Show("По запросу ничего не найдено");
            binSourceSecTabSearchResult.DataSource = secTable;
            dataGridViewFIO.DataSource = binSourceSecTabSearchResult;
        }

        private void FinderClientsOnFIOBtn_Click(object sender, EventArgs e)
        {
            string familClientToSearch = tbClientFioForSearch.Text;
            dataGirdSecTabStatus = 0;
            var secTable = DBConnectCreator.GetClientsOnFamil(sqlConnString, familClientToSearch);
            if (secTable.Rows.Count == 0)
                MessageBox.Show("По запросу ничего не найдено");
            binSourceSecTabSearchResult.DataSource = secTable;
            dataGridViewFIO.DataSource = binSourceSecTabSearchResult;
        }

        private void ClientAdd_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.OldID = -1;
            if (rbNewClient.Checked) //rbOldClient
            {
                client.Family = tbClientFamilyName.Text;
                client.Name = tbClientFirstName.Text;
                client.SecondName = tbClientLastName.Text;
                client.PhoneNumber = tbClientPhoneNum.Text;
                client.ClientRole = cbClientRole.SelectedIndex + 1; //&
            }
            else if (tbClientOldID.Text != "")
                client.OldID = Int32.Parse(tbClientOldID.Text);
            client.OrderNum = orderNumToAddClient;
            //DBConnectCreator.ClientInsertInBase(sqlConnString, client);
            var clientID = DBConnectCreator.ClientInsertInBase(sqlConnString, client);
            var cidr = DBConnectCreator.GetClientsID(sqlConnString).Rows;
            List<int> listids = new List<int>(1);
            for (int i = 0; i < cidr.Count; i++)
            {
                listids.Add((int)cidr[i][0]);

            }

            if (clientsIDBofferList.Contains(clientID))
            {
                MessageBox.Show("Клиент уже добавлен");
                tbClientOldID.Text = "";
            }
            else if (!listids.Contains(clientID))
                MessageBox.Show("Такого клиента неть");
            else
                clientsIDBofferList.Add(clientID);
            if (changeParams == 2)
            {
                DBConnectCreator.OrderAndClientsRelationCreator(sqlConnString, clientsIDBofferList, orderNumToAddClient);
                if (listids.Contains(clientID))
                    clientsIDBofferList.RemoveAt(clientsIDBofferList.Count - 1);
            }
            UpdateSecondFIOTable(clientsIDBofferList);
            tbClientFamilyName.Clear();
            tbClientFioForSearch.Clear();
            tbClientFirstName.Clear();
            tbClientLastName.Clear();
            tbClientOldID.Clear();
            tbClientPhoneNum.Clear();
            cbClientRole.SelectedIndex = 0;
        }
        public void UpdateSecondFIOTable(List<int> clients)
        {
            dataGirdSecTabStatus = 3;
            DataTable secTable;
            switch (changeParams)
            {
                case 2:
                    secTable = DBConnectCreator.GetClientsOnOrders(sqlConnString, orderNumToAddClient);
                    break;
                default:
                    secTable = DBConnectCreator.GetClientsOnOrders(sqlConnString, clients);
                    break;
            }
            bindSourceClient.DataSource = secTable;
            dataGridViewFIO.DataSource = bindSourceClient;
        }

        int clientIDToDelete = -1;
        private void DeleterClientFromOrders_Click(object sender, EventArgs e)
        {
            if (dataGirdSecTabStatus < 2)
                MessageBox.Show("Обновите таблицу по заявке и ваыберете клиента для удаления из заявки");
            else
            {
                DBConnectCreator.DeleteClientFromOrder(sqlConnString, orderNumToAddClient, clientIDToDelete);
                UpdateSecondFIOTable(clientsIDBofferList);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateSecondFIOTable(clientsIDBofferList);
        }

        private void dataGridViewFIO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGirdSecTabStatus >=2 && e.RowIndex >= 0 && e.ColumnIndex >= 0)
                Int32.TryParse((dataGridViewFIO[0, e.RowIndex].Value.ToString()), out clientIDToDelete);
        }

        private void rbComNotReplace_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }

        private void rbNewClient_CheckedChanged(object sender, EventArgs e)
        {
            CheckedRBClientChanged();
        }
    }
}
