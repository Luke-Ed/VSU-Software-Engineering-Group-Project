using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    public class DatabaseConnectionManager
    {
        public DatabaseConnectionManager() {
        }

        private string GetDBConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
        }

        public SqlConnection ConnectToDatabase()
        {
            return new SqlConnection(GetDBConnectionString());
        }
    }
}