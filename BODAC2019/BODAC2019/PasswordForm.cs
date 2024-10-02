using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BODAC2019
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "hello")
            {
                // The password is ok.
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                // The password is invalid.
                txtPassword.Clear();
                MessageBox.Show("Invalid password !");
                txtPassword.Focus();
            }
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
