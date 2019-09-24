using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPayroll.BLL
{
    class salaryBll
    {
        public string SalaryNo { get; set; }
        public string EmpName { get; set; }

        //public string SalaryMonth { get; set; }
        public DateTime SalaryMonth { get; set; }
        public string Salary { get; set; }
        public string Bonus { get; set; }
        public string Total { get; set; }
        public string PaidAmt { get; set; }
        public string SalaryDues { get; set; }
        public DateTime PaidDate { get; set; }
        public string Note { get; set; }
    }
}
