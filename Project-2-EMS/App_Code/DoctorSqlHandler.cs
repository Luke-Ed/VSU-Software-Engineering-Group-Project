using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class DoctorSqlHandler
    {
        public string PatientNameQuerier()
        {
            string query = "SELECT * " +
                           "FROM PatientInfo " +
                           "WHERE FirstName LIKE @firstName OR LastName LIKE @lastName;";

            return query;
        }
    }
}
