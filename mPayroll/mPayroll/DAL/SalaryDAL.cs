using mPayroll.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mPayroll.DAL
{
    class SalaryDAL :CommonGateway
    {


        #region Select
        public DataTable Select()
        {
            DataTable dt = new DataTable();
            try
            {
                Connection.Open();
                Query = "SELECT * FROM Salary";
                Command= new SqlCommand(Query,Connection);
                Reader = Command.ExecuteReader();
                dt.Load(Reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Reader.Close();
                Connection.Close();
            }
            return dt;

        }
        #endregion

        #region insert

        public bool Insert(salaryBll b)
        {
            bool isSuccess = false;
            try
            {
                Connection.Open();
                Query = "INSERT INTO Salary(SalaryNo,EmpName, SalaryMonth, Salary, Bonus, Total, PaidAmt, SalaryDues, PaidDate, Note) VALUES (@SalaryNo,@EmpName, @SalaryMonth, @Salary, @Bonus, @Total, @PaidAmt, @SalaryDues, @PaidDate, @Note)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@SalaryNo", b.SalaryNo);
                Command.Parameters.AddWithValue("@EmpName", b.EmpName);
                Command.Parameters.AddWithValue("@SalaryMonth", b.SalaryMonth);
                Command.Parameters.AddWithValue("@Salary",b.Salary);
                Command.Parameters.AddWithValue("@Bonus", b.Bonus);
                Command.Parameters.AddWithValue("@PaidDate", b.PaidDate);
                Command.Parameters.AddWithValue("@Total", b.Total);
                Command.Parameters.AddWithValue("@PaidAmt", b.PaidAmt);
                Command.Parameters.AddWithValue("@SalaryDues", b.SalaryDues);
                Command.Parameters.AddWithValue("@Note", b.Note);
                                                          
                int rows = Command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Update
        public bool Update(salaryBll b)
        {
            bool isSuccess = false;
            try
            {
                Connection.Open();
                Query = "UPDATE Salary SET EmpName=@EmpName, SalaryMonth=@SalaryMonth, Salary=@Salary, Bonus=@Bonus, Total=@Total, PaidAmt=@PaidAmt, SalaryDues=@SalaryDues, PaidDate=@PaidDate, Note=@Note WHERE SalaryNo=@SalaryNo";
                Command= new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@SalaryNo", b.SalaryNo);
                Command.Parameters.AddWithValue("@EmpName", b.EmpName);
                Command.Parameters.AddWithValue("@SalaryMonth", b.SalaryMonth);
                Command.Parameters.AddWithValue("@Salary", b.Salary);
                Command.Parameters.AddWithValue("@Bonus", b.Bonus);
                Command.Parameters.AddWithValue("@PaidDate", b.PaidDate);
                Command.Parameters.AddWithValue("@Total", b.Total);
                Command.Parameters.AddWithValue("@PaidAmt", b.PaidAmt);
                Command.Parameters.AddWithValue("@SalaryDues", b.SalaryDues);
                Command.Parameters.AddWithValue("@Note", b.Note);
                int rows = Command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return isSuccess;
        }

        #endregion
        public int LastId()
        {
            Connection.Open();
            Query = "SELECT TOP 1 Id FROM Salary ORDER BY Id DESC";
            Command = new SqlCommand(Query, Connection);
            Reader = Command.ExecuteReader();
            int lastId = 0;
            bool hasRow = Reader.HasRows;
            if (hasRow)
            {
                Reader.Read();
                lastId = Convert.ToInt32(Reader["Id"]);
            }
            Reader.Close();
            Connection.Close();
            return lastId;
        }

        // TODO: This application has a bug.To solve it IfSalaryExists method have to use.
        
    }
    
}

