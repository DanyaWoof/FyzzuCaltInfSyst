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
        public SetsExpertForm(string stringConn, int id, AuthorizationForm prevForm)
        {
            InitializeComponent();
            formToOpen = prevForm;
        }

        private void SetsExpertForm_Load(object sender, EventArgs e)
        {

        }

        private void BackToTheAuthorizeBTN_Click(object sender, EventArgs e)
        {
            formToOpen.Show();
            this.Close();
        }
    }
}
