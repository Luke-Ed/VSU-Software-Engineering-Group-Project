using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Project_2_EMS.App_Code;

namespace Project_2_EMS {
  /// <summary>
  ///   Interaction logic for NurseView.xaml
  /// </summary>
  public partial class NurseView {
    private readonly Window _parentWindow;
    private readonly DatabaseConnectionManager _dbConnMgr = new DatabaseConnectionManager();
    private readonly SharedSqlHandler _sharedSqlHandler = new SharedSqlHandler();

    public NurseView(Window parentWindow) {
      _parentWindow = parentWindow;
      InitializeComponent();
      Closing += OnWindowClosing;
    }

    private void LogOutButton_Click(object sender, RoutedEventArgs e) {
      Hide();
      Window mainWindow = _parentWindow;
      mainWindow.Show();
    }

    private void OnWindowClosing(object sender, CancelEventArgs e) {
      Window mainWindow = _parentWindow;
      mainWindow.Close();
    }
    
    private void Today_PatientSearch_TB_OnTextChanged(object sender, TextChangedEventArgs e) {
      DataGrid todayPatientsDg = Lookup_Dg_Patients;
      
      todayPatientsDg.ItemsSource = null;
      todayPatientsDg.Items.Refresh();
      
      PopulateDataGrid(todayPatientsDg, "Name");
    }

    private void Lookup_Dg_Patients_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
      DataGrid appointmentDataGrid = Lookup_Dg_Appointments;

      Patient patient = (Patient) Lookup_Dg_Patients.SelectedItem;
      
      appointmentDataGrid.ItemsSource = null;
      appointmentDataGrid.Items.Refresh();
      
      PopulateDataGrid(appointmentDataGrid, "AppointmentLookup", patient);
    }
    
    private void Lookup_Dg_Appointments_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
      PatientAppointment appointment = (PatientAppointment) Lookup_Dg_Appointments.SelectedItem;

      ReceptNote_TA.Text = appointment.ReceptNote;
      NurseNoteTb.Text = appointment.NurseNote;
      DoctorNote_TA.Text = appointment.DoctorNote;
    }

    private void Save_Changes_Button_OnClick(object sender, RoutedEventArgs e) {
      throw new NotImplementedException();
    }

    private void PopulateDataGrid(DataGrid dataGrid, String filter, Patient patient = null) {
      SqlConnection connection = _dbConnMgr.ConnectToDatabase();
      SqlCommand cmd = new SqlCommand {Connection = connection};
      SqlDataReader dataReader;
      
      switch (filter) {
        case "AppointmentLookup": {
          List<PatientAppointment> appointments = new List<PatientAppointment>();
          if (patient == null) {
            throw new Exception("No Patient Selected");
          }
          cmd.CommandText = _sharedSqlHandler.AppointmentQuerier("PatientId");
          cmd.Parameters.Add("@PatientId", SqlDbType.Int).Value = patient.PatientId;

          connection.Open();
          dataReader = cmd.ExecuteReader();

          while (dataReader.Read()) {
            int visitId = dataReader.GetInt32(0);
            int patientId = dataReader.GetInt32(1);
            DateTime apptDate = dataReader.GetDateTime(2);
            TimeSpan apptTime = dataReader.GetTimeSpan(3);
            decimal cost = dataReader.GetDecimal(4);
            string receptNote = dataReader.GetString(5);
            string nurseNote = dataReader.GetString(6);
            string doctorNote = dataReader.GetString(7);

            PatientAppointment appointment = new PatientAppointment(visitId, patientId, apptDate, apptTime, cost, receptNote, nurseNote, doctorNote);
            appointments.Add(appointment);
          }
          dataReader.Close();
          dataGrid.ItemsSource = appointments;
          
          break;
        }
        case "Date":
          cmd.CommandText = _sharedSqlHandler.AppointmentQuerier("DateRange");
          break;
        case "Name":
          List<Patient> patientsNames = new List<Patient>();
          String input = "%" + Today_PatientSearch_TB.Text + "%";
          
          cmd.CommandText = _sharedSqlHandler.PatientNameQuerier();
          cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = input;
          cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = input;
          
          connection.Open();
          dataReader = cmd.ExecuteReader();

          while (dataReader.Read()) {
            int patientId = dataReader.GetInt32(0);
            String lastName = dataReader.GetString(1);
            String firstName = dataReader.GetString(2);
            
            Patient patientFromDb = new Patient(patientId, firstName, lastName);
            patientsNames.Add(patientFromDb);
          }
          
          dataReader.Close();
          dataGrid.ItemsSource = patientsNames;
          break;
        default:
          throw new Exception("This shouldn't be possible.");
      }
    }


    
  }
}