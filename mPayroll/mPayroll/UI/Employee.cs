using mPayroll.BLL;
using mPayroll.DAL;
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
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
            GenarateEmpNo();
        }

        employeeBll u = new employeeBll();
        employeeDAL dal = new employeeDAL();

        private void GenarateEmpNo()
        {
            var lastId = dal.LastId();
            var empId = "EMP-" + (lastId + 1);
            txtEmpNo.Text = empId;
        }

        //Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get data from UI
            if (Validation())
            {
                if (!dal.CheckIsExistEmpByEmpMobile(txtEmpMobile.Text))
                {
                    //u.EmpId = Convert.ToInt32(txtEmpId.Text);
                    u.EmpNo = txtEmpNo.Text.ToString();
                    u.EmpName = txtEmpName.Text;
                    u.EmpMobile = txtEmpMobile.Text;
                    u.EmpNid = txtNID.Text;
                    u.EmpAddress = txtAddress.Text;
                    u.EmpDesignation = txtDesignation.Text;
                    u.EmpSalary = txtSalary.Text;

                    //Inserting Data into Database

                    bool success = dal.Insert(u);

                    if (success == true)
                    {
                        MessageBox.Show("Successfully Saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add new user");
                    }
                }
                else
                {
                    MessageBox.Show("This Employee Records is Already Exist");
                }
               

                DataTable dt = dal.Select();
                dgvEmp.DataSource = dt;

            }


        }
        
        private void frmEmployee_Load(object sender, EventArgs e)
        {  //from loaded with data

            btnUpdate.Enabled = false;
            this.ActiveControl = txtEmpName;

            DataTable dt = dal.Select();
            dgvEmp.DataSource = dt;
            
        }

        private void clear()
        {
            txtEmpNo.Text = "";
            GenarateEmpNo();
            txtEmpName.Text = "";
            txtEmpMobile.Text = "";
            txtNID.Text = "";
            txtAddress.Text = "";
            txtDesignation.Text = "";
            txtSalary.Text = "";
            
        }
        // Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //txtEmpId.Clear();
            txtEmpName.Clear();
            txtEmpMobile.Clear();
            txtNID.Clear();
            txtAddress.Clear();
            txtDesignation.Clear();
            txtSalary.Clear();
            txtEmpNo.Text = "";
            GenarateEmpNo();
            btnUpdate.Enabled = false;
            btnAdd.Enabled = true;
        }

        // keydown event 
        private void txtEmpName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtEmpName.Text.Length > 0)
                {
                    txtEmpMobile.Focus();
                }
                else
                {
                    txtEmpName.Focus();
                }
            }
        }

        private void txtEmpMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtEmpMobile.Text.Length > 0)
                {
                    txtNID.Focus();
                }
                else
                {
                    txtEmpMobile.Focus();
                }
              
            }
        }

        private void txtNID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNID.Text.Length > 0)
                {
                    txtAddress.Focus();
                }
                else
                {
                    txtNID.Focus();
                }
                
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtAddress.Text.Length > 0)
                {
                    txtDesignation.Focus();
                }
                else
                {
                    txtAddress.Focus();
                }
                
            }
        }

        private void txtDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDesignation.Text.Length > 0)
                {
                    txtSalary.Focus();
                }
                else
                {
                    txtDesignation.Focus();
                }
                
            }
        }

        //..

        //take only int character
        private void txtEmpMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtNID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        //..
        private void dgvEmp_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
          
            txtEmpNo.Text = dgvEmp.Rows[rowIndex].Cells[1].Value.ToString();
            txtEmpName.Text = dgvEmp.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmpMobile.Text = dgvEmp.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dgvEmp.Rows[rowIndex].Cells[4].Value.ToString();
            txtNID.Text = dgvEmp.Rows[rowIndex].Cells[5].Value.ToString();
            txtDesignation.Text = dgvEmp.Rows[rowIndex].Cells[6].Value.ToString();
            txtSalary.Text = dgvEmp.Rows[rowIndex].Cells[7].Value.ToString();

            btnUpdate.Enabled = true;
            //btnDelete.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {   //get the values from ui

            u.EmpNo = txtEmpNo.Text.ToString();
            u.EmpName = txtEmpName.Text;
            u.EmpMobile = txtEmpMobile.Text;
            u.EmpNid = txtNID.Text;
            u.EmpAddress = txtAddress.Text;
            u.EmpDesignation = txtDesignation.Text;
            u.EmpSalary = txtSalary.Text;

            DialogResult result = MessageBox.Show("Are you Sure you want to Update", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool success = dal.Update(u);
                if (success == true)
                {
                    MessageBox.Show("Successfully Updated");
                    clear();
                }
                else
                {
                    MessageBox.Show("Failed to Update");
                }

                DataTable dt = dal.Select();
                dgvEmp.DataSource = dt;

            }

        }
        //Validation
        private bool Validation()
        {
            bool result = false;

            if (string.IsNullOrEmpty(txtEmpName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmpName, "Name is Required");
            }

            else if (string.IsNullOrEmpty(txtEmpMobile.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmpMobile, "Mobile No. is Required");
            }

            else if (string.IsNullOrEmpty(txtDesignation.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDesignation, "Designation is Required");
            }
            else if (string.IsNullOrEmpty(txtSalary.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtSalary, "Salary is Required");
            }

            else
            {
                errorProvider1.Clear();
                result = true;
            }
            return result;
        
        }
        
    }
}
