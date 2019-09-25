using mPayroll.BLL;
using mPayroll.DAL;
using mPayroll.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mPayroll
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        loginBll lbo = new loginBll();
        loginDAL dal = new loginDAL();
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (IsValidLoginInfo())
            {
                bool success = dal.loginCheck(txtEmail.Text.Trim(), txtPassword.Text.Trim());
                if (success == true)
                {
                    // MessageBox.Show("Login Successful.");

                    this.Hide();
                    frmMain obj = new frmMain();
                    obj.Show();
                }
                else
                {
                    MessageBox.Show(@"Email/Password is Wrong.Try Again..!");
                }
            }


        }

        private bool IsValidLoginInfo()
        {
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show(@"Enter Your Email");
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show(@"Enter Your Password");
                return false;
            }
            return true;
        }
    }

}
