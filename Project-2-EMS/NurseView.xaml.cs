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
    private DatabaseConnectionManager dbConnMgr = new DatabaseConnectionManager();
    private SharedSqlHandler sharedSqlHandler = new SharedSqlHandler();

    public NurseView(Window parentWindow) {
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
      Close();
    }
    
    private void Today_PatientSearch_TB_OnTextChanged(object sender, TextChangedEventArgs e) {
      DataGrid todayPatientsDg = Today_Dg_Patients;
      
      todayPatientsDg.ItemsSource = null;
      todayPatientsDg.Items.Refresh();
      
      PopulateDataGrid(todayPatientsDg, "Name");
    }

    private void PopulateDataGrid(DataGrid dataGrid, String filter) {
      String query = String.Empty;
      SqlConnection connection = dbConnMgr.ConnectToDatabase();
      List<Patient> patients = new List<Patient>();
      
      switch (filter) {
        case "Date":
          query = sharedSqlHandler.AppointmentQuerier();
          break;
        case "Name":
          query = sharedSqlHandler.PatientNameQuerier();
          SqlCommand cmd = new SqlCommand {Connection = connection, CommandText = query};
          cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = Today_PatientSearch_TB.Text;
          cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = Today_PatientSearch_TB;
          
          connection.Open();
          SqlDataReader dataReader = cmd.ExecuteReader();

          while (dataReader.Read()) {
            int patientId = dataReader.GetInt32(0);
            String lastName = dataReader.GetString(1);
            String firstName = dataReader.GetString(2);
            
            Patient patient = new Patient(patientId, firstName, lastName);
            patients.Add(patient);
          }
          
          dataReader.Close();
          dataGrid.ItemsSource = patients;
          break;
        default:
          throw new Exception("This shouldn't be possible.");
      }
    }
  }
}