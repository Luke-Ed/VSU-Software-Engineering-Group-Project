using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class ReceptionSqlHandler
    {
        public string AppointmentQuerier(DateTime apptDate)
        {
            String query = "SELECT * " +
                           "FROM PatientInfo p FULL JOIN Appointments a ON p.PatientID = a.PatientID " +
                           "WHERE ApptDate BETWEEN '" + apptDate + "' AND '" + apptDate.AddDays(6) + "'";
            return query;
        }

        public string PatientQuerier()
        {
            string query = "SELECT PatientID, LastName, FirstName, Address " +
                           "FROM PatientInfo ";

            return query;
        }

        public string NumberOfPatientsQuerier()
        {
            String query = "SELECT COUNT(*) " +
                           "FROM PatientInfo";

            return query;
        }

        public string NumberOfAppointmentsQuerier()
        {
            String query = "SELECT COUNT(*) " +
                           "FROM Appointments";

            return query;
        }
    }
}