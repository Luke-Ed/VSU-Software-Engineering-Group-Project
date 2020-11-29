using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class PatientAppointment
    {
        private int VisitId { get; }
        private int PatientId { get; }
        private string ApptDate { get; }
        private string ApptTime { get; }
        private double Cost { get; }
        private string ReceptNote { get; }

        public PatientAppointment(int visitId, int patientId, string apptDate, string apptTime, double cost, string receptNote)
        {
            VisitId = visitId;
            PatientId = patientId;
            ApptDate = apptDate;
            ApptTime = apptTime;
            Cost = cost;
            ReceptNote = receptNote;
        }
    }
}
