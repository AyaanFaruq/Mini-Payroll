using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPayroll.DAL
{
    public class CommonGateway
    {
        private readonly string _connectionString = ConfigurationManager
                                                    .ConnectionStrings["DataBaseContext"]
                                                    .ConnectionString;
        protected SqlConnection Connection { get; set; }
        protected string Query { get; set; }
        protected SqlCommand Command { get; set; }
        protected SqlDataReader Reader { get; set; }
        protected CommonGateway()
        {
            Connection = new SqlConnection(_connectionString);
        }
    }
}
