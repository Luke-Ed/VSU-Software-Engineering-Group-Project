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
                           "WHERE FirstName LIKE @firstName AND LastName LIKE @lastName;";

            return query;
        }

        public string PerscriptionQuerier()
        {
            string query = "SELECT * " +
                           "FROM Prescription " +
                           "WHERE PatientID = @patientID";

            return query;
        }

        public string UpdatePatientPrecriptionQuerier()
        {
            String query = "INSERT INTO Preciption (" +
                            "VALUES ";

            return query;
        }

        public string PrescriptionNumberQuerier()
        {
            String query = "SELECT COUNT(*) " +
                            "FROM Prescription";
            return query;
        }
    }
}
