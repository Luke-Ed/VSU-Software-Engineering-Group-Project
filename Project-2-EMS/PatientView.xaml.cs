using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using Project_2_EMS.App_Code;

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
      var items = new List<SimpleAppointment>();
      String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
      SqlConnection connection = new SqlConnection(connectionString);
      SimpleAppointment appointment;
            
      SqlCommand cmd = new SqlCommand(){
        Connection = connection,
        CommandText = "SELECT ApptDate, Cost, ReceptNote, NurseNote, DoctorNote, VisitID FROM Appointments WHERE PatientID = " + _patient.PatientId
      };
            
      connection.Open();

      SqlDataReader dataReader = cmd.ExecuteReader();

      while (dataReader.Read()){
        DateTime apptDate = dataReader.GetDateTime(0);
        String apptDateString = apptDate.ToString();
        decimal cost = dataReader.GetDecimal(1);
        String receptionistNote = dataReader.GetString(2);
        String nurseNote = dataReader.GetString(3);
        String doctorNote = dataReader.GetString(4);
        int visitID = dataReader.GetInt32(5);

        appointment = new SimpleAppointment() {ApptDate = apptDateString, Cost = cost, ReceptionistNotes = receptionistNote,
          NurseNotes = nurseNote, DoctorNotes = doctorNote, VisitID = visitID};
        items.Add(appointment);
      }

      connection.Close();

      AppointmentList.ItemsSource = items;

      AppointmentList.Visibility = Visibility.Hidden;
      AppointmentList.Visibility = Visibility.Visible;
    }

    //Fills the Prescription List for medicines prescribed from the selected appointment.
    private void FillPrescriptionList(int visitID){
      var items = new List<Prescription>();
      String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
      SqlConnection connection = new SqlConnection(connectionString);
      Prescription prescription;

      SqlCommand cmd = new SqlCommand(){
        Connection = connection,
        CommandText = "SELECT PrescriptionName, PrescriptionNotes, Refills FROM Prescription WHERE VisitID = " + visitID
      };

      connection.Open();

      SqlDataReader dataReader = cmd.ExecuteReader();

      while (dataReader.Read()){
        String prescriptionName = dataReader.GetString(0);
        String prescriptionNotes = dataReader.GetString(1);
        int refills = dataReader.GetByte(2);

        prescription = new Prescription() {Name = prescriptionName, Notes = prescriptionNotes, Refills = refills };
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
      SimpleAppointment appt = (SimpleAppointment)AppointmentList.SelectedItem;
      if (appt != null){
        int visitID = appt.VisitID;
        FillPrescriptionList(visitID);
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

    public class Prescription {
      public string Name { get; set; }
      public string Notes { get; set; }
      public int Refills { get; set; }

      public override string ToString() {
        return "Prescription Name: " + Name +
               "\n Doctor's Notes: " + Notes +
               "\n Refills Remaining: " + Refills + "\n\n\n";
      }
    }

    public class SimpleAppointment{
      public int VisitID { get; set; }
      public string ApptDate { get; set;  }
      public decimal Cost { get; set; }
      public string ReceptionistNotes { get; set; }
      public string NurseNotes { get; set; }
      public string DoctorNotes { get; set; }

      public override string ToString(){
        return "Date: " + ApptDate +
               "\n Cost: $" + Cost +
               "\n Receptionist Notes: " + ReceptionistNotes +
               "\n Nurse Notes: " + NurseNotes +
               "\n Doctor Notes: " + DoctorNotes + "\n\n\n";    
      }
    }
  }
}