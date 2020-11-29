using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class ReceptionSqlHandler
    {
        public string AppointmentQuerier(string apptDate)
        {
            String query = "SELECT * FROM Appointments WHERE ApptDate = " + apptDate;
            return query;
        }
    }
}