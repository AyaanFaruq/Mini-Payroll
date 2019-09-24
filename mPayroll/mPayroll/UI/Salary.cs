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
    public partial class frmSalary : Form
    {
        public frmSalary()
        {
            InitializeComponent();
            //cmbSalaryMonth.SelectedItem = "January";
            
            dateTimePickerPayDate.Value = DateTime.Today;
            dateTimePickerSalaryMonth.Format = DateTimePickerFormat.Custom;
            dateTimePickerSalaryMonth.CustomFormat = "MMM/yyyy";
            dateTimePickerSalaryMonth.ShowUpDown = true;

            LoadEmpName();
            GenarateSalaryNo();
            cmbEmployeeName.SelectedIndex = -1;
            txtSalary.Text = "0";
            txtTotal.Text = "0";
            txtSalaryDues.Text = "0";
        }
        salaryBll s = new salaryBll();
        SalaryDAL dal = new SalaryDAL();
        employeeDAL employeeDAL = new employeeDAL();
        private void GenarateSalaryNo()
        {
            var lastId = dal.LastId();
            var salaryId = "SAL-" + (lastId + 1);
            txtSalaryId.Text = salaryId;
        }
        
        public void Clear()
        {
            txtSalary.Text="";
            txtBonus.Text="";
            txtTotal.Text="";
            txtPaidAmt.Text="";
            txtSalaryDues.Text="";
            txtNotes.Clear();
        }

        private void frmSalary_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'miniPayrollDataSet.Employee' table. You can move, or remove it, as needed.
            //this.employeeTableAdapter.Fill(this.miniPayrollDataSet.Employee);
            //btnUpdate.Enabled = false;
            //this.ActiveControl = cmbEmployeeName;

            var dt = dal.Select();
            dgvSalary.DataSource = dt;
        }
        public void LoadEmpName()
        {
            var dt = employeeDAL.Select();
            cmbEmployeeName.DataSource = new BindingSource(dt, null);
            cmbEmployeeName.ValueMember = "EmpId";
            cmbEmployeeName.DisplayMember = "EmpName";
        }
        private void cmbEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //cmbSalaryMonth.Focus();
            }
        }

        private void dateTimePickerSalaryMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBonus.Focus();
            }
        }
        

        private void txtBonus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerPayDate.Focus();
            }
        }

        private void dateTimePickerPayDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPaidAmt.Focus();
            }
        }

        private void txtPaidAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNotes.Focus();
            }
        }

        private void txtBonus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtPaidAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtBonus_Leave(object sender, EventArgs e)
        {
            if (txtBonus.Text.Trim() == "")
            {
                txtBonus.Text = "00";
            }

            //var count = Convert.ToInt32(txtSalary.Text) + Convert.ToInt32(txtBonus.Text);
            //txtTotal.Text = count.ToString("N");
        }

        private void txtPaidAmt_Leave(object sender, EventArgs e)
        {
            if (txtPaidAmt.Text.Trim() == "")
            {
                txtPaidAmt.Text = "00";
            }
            //var count = Convert.ToInt32(txtTotal.Text) - Convert.ToInt32(txtPaidAmt.Text);
            //txtSalaryDues.Text = count.ToString("N");
        }
        
        SalaryDAL dalObj = new SalaryDAL();

        // salary
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (IsValidSalaryTrans())
            {
                    salaryBll b = new salaryBll();
                    b.SalaryNo = txtSalaryId.Text;
                    b.EmpName = cmbEmployeeName.Text.ToString();
                    b.SalaryMonth = dateTimePickerSalaryMonth.Value;
                    b.Salary = txtSalary.Text;
                    b.Bonus = txtBonus.Text;
                    b.PaidDate = dateTimePickerPayDate.Value;
                    b.Total = txtTotal.Text;
                    b.PaidAmt = txtPaidAmt.Text;
                    b.SalaryDues = txtSalaryDues.Text;
                    b.Note = txtNotes.Text;

                    bool success = dalObj.Insert(b);

                    if (success == true)
                    {
                        MessageBox.Show("Successfully Save Salary Information",
                                        "Message", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        Clear();
                        txtSalaryId.Clear();
                        dateTimePickerSalaryMonth.Value = DateTime.Now;

                      //  cmbSalaryMonth.SelectedItem = "January";
                        dateTimePickerPayDate.Value = DateTime.Now;
                        GenarateSalaryNo();
                        LoadEmpName();
                        cmbEmployeeName.SelectedIndex = -1;
                        var dt = dal.Select();
                        dgvSalary.DataSource = dt;
                    }
                
                    else
                    {
                        MessageBox.Show("Failed to Save Salary Information");
                    }
            }
           
        }

        private bool IsValidSalaryTrans()
        {
            if (txtSalaryId.Text.Trim() == "")
            {
                MessageBox.Show("Click Refresh Button");
                return false;
            }
            else if(cmbEmployeeName.Text == "")
            {
                MessageBox.Show("Select Employee Name");
                return false;
            }
            else if (txtPaidAmt.Text=="")
            {
                MessageBox.Show("Write Paid Amt.");
                return false;
            }
            else if(txtPaidAmt.Text != "")
            {
                var paidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                if(paidAmt <= 0)
                {
                    MessageBox.Show("Write Valid Paid Amt.");
                    return false;
                }
            }
            return true;
        }

        private void cmbEmployeeName_SelectedValueChanged(object sender, EventArgs e)
        {
            Clear();

            if (cmbEmployeeName.SelectedValue != null && cmbEmployeeName.SelectedIndex >= 0)
            {
                int empId = Convert.ToInt32(cmbEmployeeName.SelectedValue);
                var dt = employeeDAL.GetEmpInfoByEmpId(empId);
                Clear();
                var row = dt.Select();
                txtSalary.Text = row[0]["EmpSalary"].ToString();
                txtTotal.Text = row[0]["EmpSalary"].ToString();
                txtSalaryDues.Text = row[0]["EmpSalary"].ToString();

            }
        }

        private void txtBonus_TextChanged(object sender, EventArgs e)
        {
            var salary = txtSalary.Text;
            var bonus = txtBonus.Text;
            if(salary != "" && bonus != "")
            {
                txtTotal.Text = (Convert.ToDecimal(salary) + Convert.ToDecimal(bonus)).ToString();
                txtSalaryDues.Text = (Convert.ToDecimal(salary) + Convert.ToDecimal(bonus)).ToString();
            }
            else
            {
                txtBonus.Text = "0";
            }
        }

        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
            var total = txtTotal.Text;
            var paidAmt = txtPaidAmt.Text;
            if (total != "" && paidAmt != "")
            {
                txtSalaryDues.Text = (Convert.ToDecimal(total) - Convert.ToDecimal(paidAmt)).ToString();
            }
        }

        private void dgvSalary_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            txtSalaryId.Text = dgvSalary.Rows[rowIndex].Cells["SalaryNo"].Value.ToString();
            cmbEmployeeName.Text = dgvSalary.Rows[rowIndex].Cells["EmpName"].Value.ToString();
           // cmbSalaryMonth.SelectedItem = dgvSalary.Rows[rowIndex].Cells["SalaryMonth"].Value.ToString();
            dateTimePickerSalaryMonth.Text = dgvSalary.Rows[rowIndex].Cells["SalaryMonth"].Value.ToString();

            txtSalary.Text = dgvSalary.Rows[rowIndex].Cells["Salary"].Value.ToString();
            txtBonus.Text = dgvSalary.Rows[rowIndex].Cells["Bonus"].Value.ToString();
            txtTotal.Text = dgvSalary.Rows[rowIndex].Cells["Total"].Value.ToString();
            txtPaidAmt.Text = dgvSalary.Rows[rowIndex].Cells["PaidAmt"].Value.ToString();
            txtSalaryDues.Text = dgvSalary.Rows[rowIndex].Cells["SalaryDues"].Value.ToString();
            dateTimePickerPayDate.Text = dgvSalary.Rows[rowIndex].Cells["PaidDate"].Value.ToString();
            txtNotes.Text = dgvSalary.Rows[rowIndex].Cells["Note"].Value.ToString();
            btnUpdate.Enabled = true;
            //btnDelete.Enabled = true;
            btnPay.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValidSalaryTrans())
            {
                salaryBll b = new salaryBll();
                b.SalaryNo = txtSalaryId.Text;
                b.EmpName = cmbEmployeeName.Text.ToString();
                b.SalaryMonth = dateTimePickerSalaryMonth.Value;

                b.Salary = txtSalary.Text;
                b.Bonus = txtBonus.Text;
                b.PaidDate = dateTimePickerPayDate.Value;
                b.Total = txtTotal.Text;
                b.PaidAmt = txtPaidAmt.Text;
                b.SalaryDues = txtSalaryDues.Text;
                b.Note = txtNotes.Text;

                DialogResult result = MessageBox.Show("Are you Sure you want to Update", 
                                                      "Update", MessageBoxButtons.YesNo, 
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var success = dalObj.Update(b);
                    if (success == true)
                    {
                        MessageBox.Show("Successfully Updated Salary Information", 
                                        "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        txtSalaryId.Clear();
                       // cmbSalaryMonth.SelectedItem = "January";
                        dateTimePickerSalaryMonth.Value = DateTime.Now;
                        dateTimePickerPayDate.Value = DateTime.Now;
                        GenarateSalaryNo();
                        LoadEmpName();
                        cmbEmployeeName.SelectedIndex = -1;
                        var dt = dal.Select();
                        dgvSalary.DataSource = dt;
                        btnUpdate.Enabled = false;
                        //btnDelete.Enabled = true;
                        btnPay.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to Update Salary Information");
                    }
                }
                   


            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Clear();
            txtSalaryId.Clear();
            //.SelectedItem = "January";
            dateTimePickerSalaryMonth.Value = DateTime.Now;
            dateTimePickerPayDate.Value = DateTime.Now;
            GenarateSalaryNo();
            LoadEmpName();
            cmbEmployeeName.SelectedIndex = -1;
            btnUpdate.Enabled = true;
            //btnDelete.Enabled = true;
            btnPay.Enabled = true;

        }

        
    }
}
