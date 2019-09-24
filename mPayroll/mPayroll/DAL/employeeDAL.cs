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
    class employeeDAL : CommonGateway
    {
     
        #region Select Data from Database
        public DataTable Select()
        {
            DataTable dt = new DataTable();
            try
            {
                Connection.Open();
                Query = "SELECT * FROM Employee";
                Command = new SqlCommand(Query, Connection);
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

        #region Isert Data in DataBase
        public bool Insert(employeeBll b)
        {
            bool isSuccess = false;
            try
            {
                Connection.Open();
                Query = "INSERT INTO Employee(EmpNo,EmpName, EmpMobile, EmpAddress, EmpNid, EmpDesignation, EmpSalary) VALUES (@EmpNo,@EmpName, @EmpMobile, @EmpAddress, @EmpNid, @EmpDesignation, @EmpSalary)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@EmpNo", b.EmpNo);
                Command.Parameters.AddWithValue("@EmpName", b.EmpName);
                Command.Parameters.AddWithValue("@EmpMobile", b.EmpMobile);
                Command.Parameters.AddWithValue("@EmpAddress", b.EmpAddress);
                Command.Parameters.AddWithValue("@EmpNid", b.EmpNid);
                Command.Parameters.AddWithValue("@EmpDesignation", b.EmpDesignation);
                Command.Parameters.AddWithValue("@EmpSalary", b.EmpSalary);
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

        #region Update Data in DataBase
        public bool Update(employeeBll b)
        {
            bool isSuccess = false;
            try
            {
                Connection.Open();
                Query = "UPDATE Employee SET EmpName=@EmpName, EmpMobile=@EmpMobile, EmpAddress=@EmpAddress, EmpNid=@EmpNid, EmpDesignation=@EmpDesignation, EmpSalary=@EmpSalary WHERE EmpNo=@EmpNo";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@EmpNo", b.EmpNo);
                Command.Parameters.AddWithValue("@EmpName", b.EmpName);
                Command.Parameters.AddWithValue("@EmpMobile", b.EmpMobile);
                Command.Parameters.AddWithValue("@EmpAddress", b.EmpAddress);
                Command.Parameters.AddWithValue("@EmpNid", b.EmpNid);
                Command.Parameters.AddWithValue("@EmpDesignation", b.EmpDesignation);
                Command.Parameters.AddWithValue("@EmpSalary", b.EmpSalary);
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

        #region Delete Data in DataBase
        public bool Delete(employeeBll b)
        {
            bool isSuccess = false;
            try
            {
                Connection.Open();
                Query = "DELETE FROM Employee WHERE EmpId=@EmpId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@EmpId", b.EmpId);
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
            Query = "SELECT TOP 1 EmpId FROM Employee ORDER BY EmpId DESC";
            Command = new SqlCommand(Query, Connection);
            Reader = Command.ExecuteReader();
            int lastId = 0;
            bool hasRow = Reader.HasRows;
            if (hasRow)
            {
                Reader.Read();
                lastId = Convert.ToInt32(Reader["EmpId"]);
            }
            Reader.Close();
            Connection.Close();
            return lastId;
        }

        internal DataTable GetEmpInfoByEmpId(int empId)
        {
            DataTable dt = new DataTable();
            Connection.Open();
            Query = "SELECT * FROM Employee WHERE EmpId = @EmpId";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@EmpId", empId);
            Reader = Command.ExecuteReader();
            dt.Load(Reader);
            Reader.Close();
            Connection.Close();
            return dt;
        }

        public bool CheckIsExistEmpByEmpMobile(string mobile)
        {
            Connection.Open();
            Query = "SELECT * FROM Employee WHERE EmpMobile = @EmpMobile";
            Command= new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@EmpMobile", mobile);
            Reader = Command.ExecuteReader();
            bool hasRow = Reader.HasRows;
            Reader.Close();
            Connection.Close();
            return hasRow;
        }

    }
}
