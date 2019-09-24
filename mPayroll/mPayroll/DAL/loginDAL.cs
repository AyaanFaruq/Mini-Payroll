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
    class loginDAL : CommonGateway
    {
       
        public bool loginCheck(string email, string password)
        {
            //Create a boolean variable and set its value false and return it

            bool isSuccess = false;
            //Connecting to Database
            Connection.Open();

            try
            {
                //sql query to check login
                Query = "Select * From [User] Where Email=@Email AND Password =@Password";

                //Createing sql command to pass value
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@Email", email);
                Command.Parameters.AddWithValue("@Password", password);
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    //Login Successful
                    isSuccess = true;
                }
                else
                {
                    //Login Failed
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
    }
}
