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
                           "FROM Appointments " +
                           "WHERE ApptDate BETWEEN '" + apptDate + "' AND  '" + apptDate.AddDays(6) + "'";
            return query;
        }

        public string PatientQuerier(int patientId)
        {
            String query = "SELECT * " +
                           "FROM PatientInfo " +
                           "WHERE PatientId = '" + patientId + "'";
            return query;
        }
    }
}