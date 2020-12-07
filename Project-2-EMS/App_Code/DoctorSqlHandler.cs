using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class DoctorSqlHandler
    {
        public string PatientQuerier(String firstName, String lastName)
        {
            String query = "SELECT * " +
                           "FROM PatientInfo " +
                           "WHERE FirstName = '" + firstName + "' OR LastName = '" + lastName + "'";

            return query;
        }
    }
}
