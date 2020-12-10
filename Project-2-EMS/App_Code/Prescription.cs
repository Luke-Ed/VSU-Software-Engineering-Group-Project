using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class Prescription
    {
        public int PrescriptionID { get; }
        public int PatientID { get; }
        public int VisitID { get; }
        public String PrescriptionName { get; }
        public String PrescriptionNotes { get; }
        public int Refills { get; }

        public Prescription(int prescriptionID, int patientID, int visitID, String prescriptionName, String presciprtionNotes,int refills)
        {
            PrescriptionID = prescriptionID;
            PatientID = patientID;
            VisitID = visitID;
            PrescriptionName = prescriptionName;
            PrescriptionNotes =  presciprtionNotes;
            Refills = refills;

        }

        public override string ToString()
        {
            return "Prescription Name: " + PrescriptionName +
                   "\n Doctor's Notes: " + PrescriptionNotes +
                   "\n Refills Remaining: " + Refills + "\n\n\n";
        }
    }
}
