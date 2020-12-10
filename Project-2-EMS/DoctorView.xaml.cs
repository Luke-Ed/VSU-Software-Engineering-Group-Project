using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

using Project_2_EMS.App_Code;




namespace Project_2_EMS {
  public partial class DoctorView { 
    private List<Patient> patients = new List<Patient>();
    private Patient patient;
    private readonly SharedSqlHandler sharedSqlHandler = new SharedSqlHandler();
    private readonly DatabaseConnectionManager dbConnectionManager = new DatabaseConnectionManager();
    private readonly Window _parentWindow;

    public DoctorView (Window parentWindow) {
      _parentWindow = parentWindow;
      InitializeComponent();
      Closing += OnWindowClosing;
    }

    private void LogOutButton_Click(object sender, RoutedEventArgs e) {
      Hide();
      var mainWindow = _parentWindow;
      mainWindow.Show();
    }

      private void OnWindowClosing(object sender, CancelEventArgs e) {
          var mainWindow = _parentWindow;
          mainWindow.Close();
      }
      
      private Boolean IsPatientSelected() {
        Boolean isValid = true;

        foreach (UIElement child in Patient_Information_Grid.Children) {
            _ = child as DataGrid != null ? (child as DataGrid).SelectedIndex < 0 ? isValid = false : true : true;
        }

        return isValid;
      }

    private void SearchBtn_Click(object sender, RoutedEventArgs e) {
        String firstName = "%" + FirstName_Tb.Text + "%";
        String lastName = "%" + LastName_Tb.Text + "%";
        String query = sharedSqlHandler.PatientNameQuerier();

        using (SqlConnection connection = dbConnectionManager.ConnectToDatabase()) {
          SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
          cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = firstName;
          cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = lastName;

            try {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read()) {
                    int patientId = dataReader.GetInt32(0);
                    string drLastName = dataReader.GetString(1);
                    string drFirstName = dataReader.GetString(2);
                    string address = dataReader.GetString(3);
                    decimal balance = dataReader.GetDecimal(4);

                    Patient patient = new Patient(patientId, drLastName, drFirstName, address, balance);
                    patients.Add(patient);
                   
                }
            }
            catch (Exception) {
                MessageBox.Show("Error reading from database.");
            }
        }
        DataGrid dataGrid = PatientLookup_Dg;
        dataGrid.ItemsSource = patients;
    }



      private void ViewPatient_Click(object sender, RoutedEventArgs e) {
        if (IsPatientSelected()) {
          Patient_Information_Grid.Visibility = Visibility.Hidden;
          ViewPatientInformation_Grid.Visibility = Visibility.Visible;
                
          DataGrid dataGrid = PatientLookup_Dg;
                
          patient = (Patient)dataGrid.SelectedItem;
          firstName_Lbl.Content = patient.FirstName;
          lastName_Lbl.Content = patient.LastName;
          address_Lbl.Content = patient.Address;
          patientID_Lbl.Content = patient.PatientId;

          PopulatePrescriptionDataGrid(patient.PatientId);
        }
      }
    private int GeneratePrescriptionId() {
      int prescriptionId = 0;

      DoctorSqlHandler doctorSql = new DoctorSqlHandler();
      String query = doctorSql.PrescriptionNumberQuerier();

      using (SqlConnection connection = dbConnectionManager.ConnectToDatabase()) {
        SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
        try {
          connection.Open();
          SqlDataReader dataReader = cmd.ExecuteReader();

          while (dataReader.Read()) {
              prescriptionId = dataReader.GetInt32(0);
          }
          dataReader.Close();

          return (prescriptionId + 1);
        }
        
        catch (Exception) {
          MessageBox.Show("Error reading from database.");
        } 
      } 
      return prescriptionId - 1;
    }

    private void AddPerscription_Click(object sender, RoutedEventArgs e) {
      int prescriptionId = GeneratePrescriptionId();
      int patientId = patient.PatientId ;
      PatientAppointment pA = GetPatientAppointment(patientId);
      if(pA != null) {
        int visitId = pA.VisitId;
        string prescriptionName = PrescriptionName_Tb.Text;
        string prescriptionNotes = PrescriptionNotes_Tb.Text;
        int refills = Convert.ToInt32(PrescriptionRefills_Tb.Text);
        Prescription prescription = new Prescription(prescriptionId, patientId, visitId, prescriptionName, prescriptionNotes, refills);
        AddNewPrescription(prescription);
      }
      else {
        MessageBox.Show("No Appointment");            
      }
    }


  private void PopulatePrescriptionDataGrid(int pId) {
    List<Prescription> prescriptions = new List<Prescription>();
    DoctorSqlHandler doctorSqlHandler = new DoctorSqlHandler();
    String query = doctorSqlHandler.PerscriptionQuerier();
    
    using (SqlConnection connection = dbConnectionManager.ConnectToDatabase()) {
      SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
      cmd.Parameters.Add("@patientID", SqlDbType.Int).Value = pId;
    
      connection.Open();
      SqlDataReader dataReader = cmd.ExecuteReader();

      while (dataReader.Read()) {
        int prescriptionId = dataReader.GetInt32(0);
        int patientId = dataReader.GetInt32(1);
        int visitId = dataReader.GetInt32(2);
        string prescriptionName = dataReader.GetString(3);
        string prescriptionNotes = dataReader.GetString(4);
        int refills = dataReader.GetByte(5);

        Prescription prescription = new Prescription(prescriptionId, patientId, visitId, prescriptionName, prescriptionNotes, refills);
        prescriptions.Add(prescription);
      }


      DataGrid dataGrid = Prescription_Dg;
      dataGrid.ItemsSource = prescriptions;
    }
  }

    private PatientAppointment GetPatientAppointment(int pId) {
      PatientAppointment patientAppointment = null;
      DoctorSqlHandler doctorSql = new DoctorSqlHandler();
      String query = doctorSql.AppointmentQuerier();

      using (SqlConnection connection = dbConnectionManager.ConnectToDatabase()) {
        SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
        cmd.Parameters.Add("@apptDate", SqlDbType.DateTime).Value = DateTime.Now.Date;
        cmd.Parameters.Add("@patientID", SqlDbType.Int).Value = pId;
        try {
          connection.Open();
          SqlDataReader dataReader = cmd.ExecuteReader();

          while (dataReader.Read()) {
              int visitId = dataReader.GetInt32(0);
              int patientId = dataReader.GetInt32(1);
              DateTime apptDate = dataReader.GetDateTime(2);
              TimeSpan apptTime = dataReader.GetTimeSpan(3);
              decimal cost = dataReader.GetDecimal(4);
              string receptNote = dataReader.GetString(5);
              string nurseNote = dataReader.GetString(6);
              string doctorNote = dataReader.GetString(7);

              patientAppointment = new PatientAppointment(visitId, patientId, apptDate, apptTime, cost, receptNote, nurseNote, doctorNote);
          }
        }
        catch (Exception) {
          MessageBox.Show("Error reading from database.");
        }


      }
      return patientAppointment;
    }

    private void AddNewPrescription(Prescription prescription) {
      DoctorSqlHandler doctorSql = new DoctorSqlHandler();
      string query = doctorSql.UpdatePatientPrecriptionQuerier();

      using (SqlConnection connection = dbConnectionManager.ConnectToDatabase()) {
        SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
        cmd.Parameters.Add("@prescriptionID", SqlDbType.Int).Value = prescription.PrescriptionID;
        cmd.Parameters.Add("@patientID", SqlDbType.Int).Value = prescription.PatientID;
        cmd.Parameters.Add("@visitID", SqlDbType.Int).Value = prescription.VisitID;
        cmd.Parameters.Add("@prescriptionName", SqlDbType.Text).Value = prescription.PrescriptionName;
        cmd.Parameters.Add("@prescriptionNotes", SqlDbType.Text).Value = prescription.PrescriptionNotes;
        cmd.Parameters.Add("@refills", SqlDbType.TinyInt).Value = prescription.Refills;
        
        try {
          connection.Open();
          cmd.ExecuteNonQuery();
        }
        catch (Exception) {
          MessageBox.Show("Error when attempting to add new patient to database.");
        }
      }
    }
    }
}
