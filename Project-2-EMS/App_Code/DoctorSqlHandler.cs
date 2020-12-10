using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS.App_Code
{
    class DoctorSqlHandler {
      
        public string PerscriptionQuerier()
        {
            string query = "SELECT * " +
                           "FROM Prescription " +
                           "WHERE PatientID = @patientID";

            return query;
        }

        public string UpdatePatientPrecriptionQuerier()
        {
            String query = "INSERT INTO PatientInfo ([PrescriptionID], [PatientID], [VisitID], [PrescriptionName], [PrescriptionNotes], [Refills]) " +
                           "VALUES (@prescriptionID,@patientID,@visitID,@prescriptionName,@prescriptionNotes,@refills)";

            return query;
        }

        public string PrescriptionNumberQuerier()
        {
            String query = "SELECT COUNT(*) " +
                            "FROM Prescription";
            return query;
        }

        public string AppointmentQuerier()
        {
            String query = "SELECT * " +
                           "FROM Appointments " +
                           "WHERE ApptDate = @apptDate AND PatientID = @patientID";
            return query;
        }


    }
}
