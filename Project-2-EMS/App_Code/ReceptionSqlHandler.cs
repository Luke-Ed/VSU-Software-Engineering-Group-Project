using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2_EMS {
    class ReceptionSqlHandler {
      
        public string PatientNameExactQuerier() {
            string query = "SELECT * " +
                           "FROM PatientInfo " +
                           "WHERE FirstName LIKE @firstName AND LastName LIKE   @lastName;";
            return query;
        }

        public string NumberOfAppointmentsQuerier() {
            String query = "SELECT COUNT(*) " +
                           "FROM Appointments;";

            return query;
        }

        public string AddNewPatientToDb() {
            String query = "INSERT INTO PatientInfo ([PatientID], [LastName], [FirstName], [Address], [Balance]) " +
                           "VALUES (@patientId,@lastName,@firstName,@address,@balance)";

            return query;
        }

        public string AddNewAppointmentToDb() {
            String query = "INSERT INTO Appointments ([VisitID], [PatientID], [ApptDate], [ApptTime], [Cost], [ReceptNote], [NurseNote], [DoctorNote]) " +
                           "VALUES (@visitId,@patientId,@apptDate,@apptTime,@cost,@receptNote,@nurseNote,@doctorNote)";

            return query;
        }

        public string UpdatePatientBalance() {
            String query = "UPDATE PatientInfo " +
                           "SET Balance = Balance + @cost " +
                           "FROM PatientInfo " +
                           "WHERE PatientID = @patientId";

            return query;
        }

        public string UpdatePatientBalanceNewAppointment() {
            String query = "UPDATE PatientInfo " +
                           "SET Balance = Balance + @cost " +
                           "FROM PatientInfo p, Appointments a " +
                           "WHERE p.PatientID = a.PatientID AND a.VisitID = @visitId";

            return query;
        }

        public string DeleteAppointmentFromDb() {
          String query = "DELETE FROM Appointments " +
                         "WHERE VisitID = @visitId";

            return query;
        }
    }
}