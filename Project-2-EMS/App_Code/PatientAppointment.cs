using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class PatientAppointment
    {
        public int VisitId { get; }
        public int PatientId { get; }
        public DateTime ApptDate { get; }
        public TimeSpan ApptTime { get; }
        public decimal Cost { get; }
        public string ReceptNote { get; }
        public string NurseNote { get; }
        public string DoctorNote { get; }

        public PatientAppointment(int visitId, int patientId, DateTime apptDate, TimeSpan apptTime, decimal cost, string receptNote, string nurseNote, string doctorNote)
        {
            VisitId = visitId;
            PatientId = patientId;
            ApptDate = apptDate;
            ApptTime = apptTime;
            Cost = cost;
            ReceptNote = receptNote;
            NurseNote = nurseNote;
            DoctorNote = doctorNote;
        }
        public override string ToString()
        {
            return "Date: " + ApptDate +
                   "\n Cost: $" + Cost +
                   "\n Receptionist Notes: " + ReceptNote +
                   "\n Nurse Notes: " + NurseNote +
                   "\n Doctor Notes: " + DoctorNote + "\n\n\n";
        }
    }
}
