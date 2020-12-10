using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

using Project_2_EMS.App_Code;
using System.ComponentModel;

namespace Project_2_EMS {
  public partial class NewAppointmentWindow {
        private readonly ReceptionistView parentWindow;
        private DateTime apptDate;
        private Label apptTime;
        private int visitId;
        private Grid patientInfoPage;
        private readonly SharedSqlHandler sharedSqlHandler = new SharedSqlHandler();
        private readonly ReceptionSqlHandler receptionSqlHandler = new ReceptionSqlHandler();

        public NewAppointmentWindow(ReceptionistView parent, Label timeLabel, DateTime date) {
            InitializeComponent();
            InitializeComboBox();
            InitializeAppointmentDateTime(timeLabel, date);

            InitialPage.Visibility = Visibility.Visible;

            parentWindow = parent;
            Closing += OnWindowClosing;
        }

        public NewAppointmentWindow(ReceptionistView parent, int id, string firstName, string lastName, string receptNote, Label timeLabel, DateTime date)
        {
            InitializeComponent();
            InitializeAppointmentInfo(firstName, lastName, receptNote);
            InitializeAppointmentDateTime(timeLabel, date);

            ViewApptPage.Visibility = Visibility.Visible;

            visitId = id;
            parentWindow = parent;
            Closing += OnWindowClosing;
        }

        private void OnWindowClosing(object sender, CancelEventArgs e) {
            parentWindow.UpdateReceptionistView();
        }

        private void InitializeAppointmentDateTime(Label timeLabel, DateTime date) {
            ApptDate.Content = String.Format("{0} | {1}", date.ToString("ddd dd, yyyy"), timeLabel.Content);
            apptDate = date;
            apptTime = timeLabel;
        }

        private void InitializeAppointmentInfo(string firstName, string lastName, string receptNote) {
            ViewApptFirstName.Content = firstName;
            ViewApptLastName.Content = lastName;
            ViewApptNotes.Text = receptNote;
        }

        private void InitializeComboBox() {
            String[] states = { "Alabama, AL", "Alaska, AK", "Arizona, AZ", "Arkansas, AR", "California, CA", "Colorado, CO", "Connecticut, CT",
                                             "Delaware, DE", "Florida, FL", "Georgia, GA", "Hawaii, HI", "Idaho, ID", "Illinois, IL", "Indiana, IN", "Iowa, IA",
                                             "Kansas, KS", "Kentucky, KY", "Louisiana, LA", "Maine, ME", "Maryland, MD", "Massachusetts, MA", "Michigan, MI",
                                             "Minnesota, MN", "Mississippi, MS", "Missouri, MO", "Montana, MT", "Nebraska, NE", "Nevada, NV", "New Hampshire, NH",
                                             "New Jersey, NJ", "New Mexico, NM", "New York, NY", "North Carolina, NC", "North Dakota, ND", "Ohio, OH", "Oklahoma, OK",
                                             "Oregon, OR", "Pennsylvania, PA", "Rhode Island, RI", "South Carolina, SC", "South Dakota, SD", "Tennessee, TN", "Texas, TX",
                                             "Utah, UT", "Vermont, VT", "Virginia, VA", "Washington, WA", "West Virginia, WV", "Wisconsin, WI", "Wyoming, WY" };

            StateCb.ItemsSource = states;
        }

        private Boolean ValidNewPatientInfo() {
            Boolean isValid = true;

            bool firstValid = !NewFirstNameTb.Text.All(Char.IsLetter) || NewFirstNameTb.Text == String.Empty;
            bool lastValid = !NewLastNameTb.Text.All(Char.IsLetter) || NewLastNameTb.Text == String.Empty;
            bool streetValid = !Regex.IsMatch(StreetTb.Text, @"^[a-zA-Z0-9.\-\s]+$") || StreetTb.Text == String.Empty;
            bool cityValid = !CityTb.Text.All(Char.IsLetter) || CityTb.Text == String.Empty;
            bool zipValid = !ZipTb.Text.All(Char.IsDigit) || ZipTb.Text.Length < 5 || ZipTb.Text == String.Empty;

            _ = firstValid ? (isValid = false, FirstNameInvalid.Visibility = Visibility.Visible) : (true, FirstNameInvalid.Visibility = Visibility.Hidden);
            _ = lastValid ? (isValid = false, LastNameInvalid.Visibility = Visibility.Visible) : (true, LastNameInvalid.Visibility = Visibility.Hidden);
            _ = streetValid ? (isValid = false, StreetInvalid.Visibility = Visibility.Visible) : (true, StreetInvalid.Visibility = Visibility.Hidden);
            _ = cityValid ? (isValid = false, CityInvalid.Visibility = Visibility.Visible) : (true, CityInvalid.Visibility = Visibility.Hidden);
            _ = StateCb.Text == String.Empty ? isValid = false : true;
            _ = zipValid ? (isValid = false, ZipInvalid.Visibility = Visibility.Visible) : (true, ZipInvalid.Visibility = Visibility.Hidden);

            return isValid;
        }

        private Boolean IsPatientSelected() {
            Boolean isValid = true;

            foreach (UIElement child in patientInfoPage.Children) {
                _ = child as DataGrid != null ? (child as DataGrid).SelectedIndex < 0 ? isValid = false : true : true;
            }

            return isValid;
        }

        private void ClearChildren(Grid grid) {
          foreach (UIElement child in grid.Children) {
            _ = child as TextBox != null ? (child as TextBox).Text = string.Empty : null;
            _ = child as ComboBox != null ? (child as ComboBox).Text = string.Empty : null;

            if (child as DataGrid != null)
            {
              (child as DataGrid).ItemsSource = null;
              (child as DataGrid).Items.Refresh();
            }
          }

          FirstNameInvalid.Visibility = Visibility.Hidden;
          LastNameInvalid.Visibility = Visibility.Hidden;
          StreetInvalid.Visibility = Visibility.Hidden;
          CityInvalid.Visibility = Visibility.Hidden;
          ZipInvalid.Visibility = Visibility.Hidden;
        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e) {
          InitialPage.Visibility = Visibility.Hidden;
          NewPatientPage.Visibility = Visibility.Visible;
          patientInfoPage = NewPatientPage;
        }

        private void ExistingPatientBtn_Click(object sender, RoutedEventArgs e) {
          InitialPage.Visibility = Visibility.Hidden;
          ExistingPatientPage.Visibility = Visibility.Visible;
          patientInfoPage = ExistingPatientPage;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
          ClearChildren(patientInfoPage);
          patientInfoPage.Visibility = Visibility.Hidden;
          InitialPage.Visibility = Visibility.Visible;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e) {
            _ = patientInfoPage == NewPatientPage ? NewAppointmentNewPatient() : NewAppointmentExistingPatient();
        }

        private Boolean NewAppointmentNewPatient() {
            if (ValidNewPatientInfo()) {
                patientInfoPage.Visibility = Visibility.Hidden;
                NewAppointmentPage.Visibility = Visibility.Visible;

                FillNewAppointmentInfo();
            }
            else {
                MessageBox.Show("One or more fields are filled out incorrectly.");
            }
            return true;
        }

        private Boolean NewAppointmentExistingPatient() {
            if (IsPatientSelected()) {
                patientInfoPage.Visibility = Visibility.Hidden;
                NewAppointmentPage.Visibility = Visibility.Visible;

                FillNewAppointmentInfo();
            }
            else {
                MessageBox.Show("No patient selected, please select a patient before continuing.");
            }
            return true;
        }

        private void FillNewAppointmentInfo() {
            if (patientInfoPage == NewPatientPage) {
                FirstNameLabel.Content = NewFirstNameTb.Text;
                LastNameLabel.Content = NewLastNameTb.Text;

                string state = StateCb.Text.Substring(StateCb.Text.Length - 2);
                string address = String.Format("{0}, {1}, {2} {3}", StreetTb.Text, CityTb.Text, state, ZipTb.Text);
                AddressLabel.Content = address;
            }
            else {
              DataGrid patientDataGrid = ExistingPatients_Dg;
              Patient patient = (Patient)patientDataGrid.SelectedItem;

              FirstNameLabel.Content = patient.FirstName;
              LastNameLabel.Content = patient.LastName;
              AddressLabel.Content = patient.Address;
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e) {
          DataGrid patientDataGrid = ExistingPatients_Dg; 

            patientDataGrid.ItemsSource = null;
            patientDataGrid.Items.Refresh();

            PopulateDataGrid(patientDataGrid);
        }

        private void PopulateDataGrid(DataGrid patientDataGrid) {
            List<Patient> patients = new List<Patient>();

            string findFirstName = FirstNameExistingTextbox.Text;
            string findLastName = LastNameExistingTextbox.Text;

            _ = findFirstName == String.Empty && findLastName == String.Empty ? (findFirstName = "%%", findLastName = "%%") : (null, null);

            string query = sharedSqlHandler.PatientNameQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = findFirstName.Trim(' ');
                cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = findLastName.Trim(' ');

                try {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read()) {
                        int patientId = dataReader.GetInt32(0);
                        string lastName = dataReader.GetString(1);
                        string firstName = dataReader.GetString(2);
                        string address = dataReader.GetString(3);

                        Patient patient = new Patient(patientId, firstName, lastName, address);
                        patients.Add(patient);
                    }

                    dataReader.Close();
                    patientDataGrid.ItemsSource = patients;
                }
                catch (Exception) {
                   MessageBox.Show("Error reading from database.");
                }
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e) {
            int patientId = GeneratePatientId();
            int visitId = GenerateVisitId();

            if (patientId > 0 && visitId > 0) {
                // Remove AM/PM and space from apptTime in order to create a new PatientAppointment
                string appointmentTime = apptTime.Content.ToString().Trim(' ', 'A', 'P', 'M');
                TimeSpan time = TimeSpan.Parse(appointmentTime);

                if (apptTime.Content.ToString().Contains("PM") && apptTime.Content.ToString() != "12:00 PM") {
                    time = time.Add(TimeSpan.FromHours(12));
                }

                string receptNote = ReceptionNotesTb.Text;

                if (receptNote != String.Empty) {
                    if (patientInfoPage == NewPatientPage) {
                        string firstName = FirstNameLabel.Content.ToString();
                        string lastName = LastNameLabel.Content.ToString();
                        string address = AddressLabel.Content.ToString();

                        Patient patient = new Patient(patientId, firstName, lastName, address);
                        PatientAppointment appointment = new PatientAppointment(visitId, patientId, apptDate, time, (decimal)50, receptNote, "", "");

                        ConfirmNewPatientAndAppointment(patient, appointment);
                    }
                    else if (patientInfoPage == ExistingPatientPage) {
                      DataGrid patientDataGrid = ExistingPatients_Dg;
                      Patient patient = (Patient)patientDataGrid.SelectedItem;
                      PatientAppointment appointment = new PatientAppointment(visitId, patient.PatientId, apptDate, time, (decimal)50, receptNote, "", "");

                        ConfirmNewAppointment(appointment);
                    }
                }
                else {
                    MessageBox.Show("Please fill out the reason for this visit.");
                }
            }
            else {
                MessageBox.Show("An error occurred when trying to generate Id's");
            }            
        }

        private void ConfirmNewAppointment(PatientAppointment appointment) {
            MessageBoxResult result = MessageBox.Show("Confirm new appointment?", "Appointment Confirmation", MessageBoxButton.YesNo);

            switch (result) {
                case MessageBoxResult.Yes:
                    AddNewAppointmentToDb(appointment);
                    UpdateDbPatientBalance(appointment.VisitId, appointment.Cost);
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void ConfirmNewPatientAndAppointment(Patient patient, PatientAppointment appointment)
        {
            MessageBoxResult result = MessageBox.Show("Confirm new patient and appointment?", "Appointment Confirmation", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    AddNewPatientToDb(patient);
                    AddNewAppointmentToDb(appointment);
                    UpdateDbPatientBalance(appointment.VisitId, appointment.Cost);
                    Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void AddNewPatientToDb(Patient patient) {
          string query = receptionSqlHandler.AddNewPatientToDb();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@patientId", SqlDbType.Int).Value = patient.PatientId;
                cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = patient.LastName;
                cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = patient.FirstName;
                cmd.Parameters.Add("@address", SqlDbType.Text).Value = patient.Address;
                cmd.Parameters.Add("@balance", SqlDbType.Decimal).Value = patient.Balance;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error when attempting to add new patient to database.");
                }
            }
        }

        private void AddNewAppointmentToDb(PatientAppointment appointment) {
          string query = receptionSqlHandler.AddNewAppointmentToDb();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@visitId", SqlDbType.Int).Value = appointment.VisitId;
                cmd.Parameters.Add("@patientId", SqlDbType.Int).Value = appointment.PatientId;
                cmd.Parameters.Add("@apptDate", SqlDbType.Date).Value = appointment.ApptDate;
                cmd.Parameters.Add("@apptTime", SqlDbType.Time).Value = appointment.ApptTime;
                cmd.Parameters.Add("@cost", SqlDbType.Decimal).Value = appointment.Cost;
                cmd.Parameters.Add("@receptNote", SqlDbType.Text).Value = appointment.ReceptNote;
                cmd.Parameters.Add("@nurseNote", SqlDbType.Text).Value = appointment.NurseNote;
                cmd.Parameters.Add("@doctorNote", SqlDbType.Text).Value = appointment.DoctorNote;

                try {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) {
                    MessageBox.Show("Error when attempting to add new appointment to database.");
                }
            }
        }

        private void UpdateDbPatientBalance(int visitId, decimal cost) {
          string query = receptionSqlHandler.UpdatePatientBalanceNewAppointment();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@cost", SqlDbType.Decimal).Value = cost;
                cmd.Parameters.Add("@visitId", SqlDbType.Int).Value = visitId;

                try {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) {
                    MessageBox.Show("Error when attempting to update patient balance.");
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) {
          NewAppointmentPage.Visibility = Visibility.Hidden;
          patientInfoPage.Visibility = Visibility.Visible;
        }

        private void CloseApptView_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private int GeneratePatientId() {
            int patientId = 0;

            string query = sharedSqlHandler.NumberOfPatientsQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };

                try {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read()) {
                        patientId = dataReader.GetInt32(0);
                    }
                    dataReader.Close();

                    return (patientId + 1);
                }
                catch (Exception) {
                    MessageBox.Show("Error reading from database.");
                }
            }
            return patientId - 1;
        }

        private int GenerateVisitId() {
            int VisitId = 0;
            
            string query = sharedSqlHandler.NumberOfAppointmentsQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };

                try {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        VisitId = dataReader.GetInt32(0);
                    }
                    dataReader.Close();

                    return (VisitId + 1);
                }
                catch (Exception) {
                    MessageBox.Show("Error reading from database.");
                }
            }
            return VisitId - 1;
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.Compare(apptDate.Date, DateTime.Now.Date) >= 0) 
            {
                MessageBoxResult result = MessageBox.Show("Do you really want to remove appointment?", "Remove Appointment", MessageBoxButton.YesNo);

                PatientAppointment appointment = GetAppointment();
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        UpdateDbPatientBalance(appointment.VisitId, Decimal.Negate(appointment.Cost));
                        DeleteAppointment();
                        this.Close();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Cannot remove past appointments.");
            }
        }

        private PatientAppointment GetAppointment()
        {
          
            string query = sharedSqlHandler.AppointmentQuerier("VisitId");

            PatientAppointment appointment = null;

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@visitId", SqlDbType.Int).Value = visitId;

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

                        appointment = new PatientAppointment(visitId, patientId, apptDate, apptTime, cost, receptNote, nurseNote, doctorNote);
                    }
                }
                catch (Exception) {
                    MessageBox.Show("Error reading from database.");
                }
            }
            return appointment;
        }

        private void DeleteAppointment() {
          string query = receptionSqlHandler.DeleteAppointmentFromDb();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@visitId", SqlDbType.Int).Value = visitId;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception) {
                    MessageBox.Show("Error when attempting to remove appointment.");
                }
            }
        }
    }
}