using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using isFCAwf.Properties;

namespace isFCAwf
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }



        private void AuthorizationForm_Load(object sender, EventArgs e)
        {
            
        }

        private void authBtn_Click(object sender, EventArgs e)
        {
            string strSQLConn = Settings.Default.FuzzyCalcConnectionString;
            string login = textBox1.Text;
            string password = textBox2.Text;

            var authTable = DBConnectCreator.Authorize(strSQLConn, login, password);
            if (authTable.Rows.Count == 0)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var row = authTable.Rows[0];
            int код_должности = (int)row["Код_должности"];
            switch (код_должности)
            {
                case 1:
                    new InformationCollectorForm(strSQLConn, (int)row["ID_Сотрудника"], this).Show();
                    break;
                case 2:
                    new SetsExpertForm(strSQLConn, (int)row["ID_Сотрудника"], this).Show();
                    break;
                case 3:
                    new AdminPanelForm(this).Show();
                    break;
                default:
                    break;
            }
            /*if (код_должности == 1)
                new InformationCollectorForm(strSQLConn, (int)row["ID_Сотрудника"]).Show();
            else if (код_должности == 2)
                new SetsExpertForm(strSQLConn, (int)row["ID_Сотрудника"]).Show();
            else if (код_должности == 3)
                new AdminPanelForm().Show();
*/
            textBox1.Text = textBox2.Text = ""; //string.Empty
            Hide();
        }
    }
}
