using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mPayroll.UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            dateLabel.Text = DateTime.Now.ToString("D");
            timeLabel.Text = DateTime.Now.ToString("t");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
            frmEmployee obj = new frmEmployee();
            obj.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            frmSalary obj = new frmSalary();
            obj.Show();
        }

        private bool close = true;
        private void frmMainClosing(object sender, FormClosingEventArgs e)
        {
            if (close == true)
            {

                DialogResult result = MessageBox.Show("Are you Sure you want to Exit", "Exit", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
