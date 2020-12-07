using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class ReceptionSqlHandler
    {
        public string AppointmentQuerier()
        {
            String query = "SELECT * " +
                           "FROM Appointments " +
                           "WHERE ApptDate BETWEEN @ApptStartDate AND @ApptEndDate;";
            return query;
        }

        public string PatientNameQuerier()
        {
            string query = "SELECT * " +
                           "FROM PatientInfo " +
                           "WHERE FirstName LIKE @firstName OR LastName LIKE @lastName;";

            return query;
        }

        public string PatientIdQuerier()
        {
            string query = "SELECT * " +
                           "FROM PatientInfo " +
                           "WHERE PatientID = @PatientId;";

            return query;
        }

        public string NumberOfPatientsQuerier()
        {
            String query = "SELECT COUNT(*) " +
                           "FROM PatientInfo;";

            return query;
        }

        public string NumberOfAppointmentsQuerier()
        {
            String query = "SELECT COUNT(*) " +
                           "FROM Appointments;";

            return query;
        }
    }
}