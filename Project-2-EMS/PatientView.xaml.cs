using Project_2_EMS.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;


namespace Project_2_EMS {
  /// <summary>
  ///   Interaction logic for PatientView.xaml
  /// </summary>
  public partial class PatientView {
    private readonly Window _parentWindow;
    private readonly Patient _patient;

    public PatientView(Window parentWindow, Patient patient) {
      _parentWindow = parentWindow;
      _patient = patient;
      InitializeComponent();
      FillAppointmentList();
      SetGreeting();
    }
    
    //Causes the "Return to Appointments" button to hide and clear the prescriptions and make visible the appointments.
    private void GoBackButton_Click(object sender, RoutedEventArgs e) {
      if (!AppointmentList.IsVisible) {
        AppointmentList.SelectedValue = "";
        PrescriptionList.Visibility = Visibility.Hidden;
        PrescriptionList.ItemsSource = null;
        GoBackButton.Visibility = Visibility.Hidden;
        AppointmentList.Visibility = Visibility.Visible;
      }
    }

    //Fills the Appointment List with appointments for the logged-in patient.
    private void FillAppointmentList(){
      var appointmentList = new List<PatientAppointment>();
      String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
      SqlConnection connection = new SqlConnection(connectionString);
      PatientAppointment appointment;
            
      SqlCommand cmd = new SqlCommand(){
        Connection = connection,
        CommandText = "SELECT * FROM Appointments WHERE PatientID = " + _patient.PatientId
      };
            
      connection.Open();

      SqlDataReader dataReader = cmd.ExecuteReader();

      while (dataReader.Read()){
        int visitId = dataReader.GetInt32(0);
        int patientId = dataReader.GetInt32(1);
        DateTime apptDate = dataReader.GetDateTime(2);
        TimeSpan apptTime = dataReader.GetTimeSpan(3);
        decimal cost = dataReader.GetDecimal(4);
        String receptionistNote = dataReader.GetString(5);
        String nurseNote = dataReader.GetString(6);
        String doctorNote = dataReader.GetString(7);

        appointment = new PatientAppointment(visitId, patientId, apptDate, apptTime, cost, receptionistNote, nurseNote, doctorNote);
        appointmentList.Add(appointment);
      }

      connection.Close();

      AppointmentList.ItemsSource = appointmentList;
    }

    //Fills the Prescription List for medicines prescribed from the selected appointment.
    private void FillPrescriptionList(int visitID){
      var items = new List<Prescription>();
      String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
      SqlConnection connection = new SqlConnection(connectionString);
      Prescription prescription;

      SqlCommand cmd = new SqlCommand(){
        Connection = connection,
        CommandText = "SELECT * FROM Prescription WHERE VisitID = " + visitID
      };

      connection.Open();

      SqlDataReader dataReader = cmd.ExecuteReader();

      while (dataReader.Read()){
        int prescriptionId = dataReader.GetInt32(0);
        int patientId = dataReader.GetInt32(1);
        int visitId = dataReader.GetInt32(2);
        String prescriptionName = dataReader.GetString(3);
        String prescriptionNotes = dataReader.GetString(4);
        byte refills = dataReader.GetByte(5);

        prescription = new Prescription(prescriptionId, patientId, visitId, prescriptionName, prescriptionNotes, refills);
        items.Add(prescription);
      }

      connection.Close();

      PrescriptionList.ItemsSource = items;

      GoBackButton.Visibility = Visibility.Visible;
      AppointmentList.Visibility = Visibility.Hidden;
      PrescriptionList.Visibility = Visibility.Visible;
    }

    //Event handler that fills and shows the Prescription List upon clicking an appointment from the list.
    private void AppointmentList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e){
      PatientAppointment appt = (PatientAppointment)AppointmentList.SelectedItem;
      if (appt != null){
        int visitId = appt.VisitId;
        FillPrescriptionList(visitId);
      }
    }

    //Returns the user to the main menu.
    private void LogOutButton_Click(object sender, RoutedEventArgs e) {
      var mainWindow = _parentWindow;
      mainWindow.Show();
      Close();
    }

    //Closes the program if the window is closed.
    private void PatientView_FormClosed(object sender, CancelEventArgs e){
      var mainWindow = _parentWindow;
      mainWindow.Close();
    }

    //Personalizes the greeting with the patient's name.
    private void SetGreeting(){
      if(_patient.FirstName != "")
        WelcomeLabel.Content = "Welcome, " + _patient.FirstName + "!";
    }
  }
}