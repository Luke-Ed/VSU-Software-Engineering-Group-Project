using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

using Project_2_EMS.App_Code;

namespace Project_2_EMS {

    public partial class NewAppointmentWindow {
        private DateTime apptDate;
        private Label apptTime;
        private Grid patientInfoPage;

        public NewAppointmentWindow(Label srcLabel, Label timeLabel, DateTime date) {
            InitializeComponent();
            InitializeComboBox();
            InitialPage.Visibility = Visibility.Visible;

            ApptDate.Content = String.Format("{0} | {1}", date.ToString("ddd dd, yyyy"), timeLabel.Content);
            apptDate = date;
            apptTime = timeLabel;
        }

        public NewAppointmentWindow(string firstName, string lastName, string receptNote, Label timeLabel, DateTime date)
        {
            InitializeComponent();
            InitializeAppointmentInfo(firstName, lastName, receptNote);
            ViewApptPage.Visibility = Visibility.Visible;

            ApptDate.Content = String.Format("{0} | {1}", date.ToString("ddd dd, yyyy"), timeLabel.Content);
        }

        private void InitializeAppointmentInfo(string firstName, string lastName, string receptNote)
        {
            ViewApptFirstName.Content = firstName;
            ViewApptLastName.Content = lastName;
            ViewApptNotes.Text = receptNote;
        }

        private void InitializeComboBox() {
            String[] states = new String[] { "Alabama, AL", "Alaska, AK", "Arizona, AZ", "Arkansas, AR", "California, CA", "Colorado, CO", "Connecticut, CT",
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

        private Boolean IsPatientSelected()
        {
            Boolean isValid = true;

            foreach (UIElement child in patientInfoPage.Children)
            {
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

        private Boolean NewAppointmentNewPatient()
        {
            if (ValidNewPatientInfo())
            {
                patientInfoPage.Visibility = Visibility.Hidden;
                NewAppointmentPage.Visibility = Visibility.Visible;

                FillNewAppointmentInfo();
            }
            else
            {
                MessageBox.Show("One or more fields are filled out incorrectly.");
            }
            return true;
        }

        private Boolean NewAppointmentExistingPatient()
        {
            if (IsPatientSelected())
            {
                patientInfoPage.Visibility = Visibility.Hidden;
                NewAppointmentPage.Visibility = Visibility.Visible;

                FillNewAppointmentInfo();
            }
            else
            {
                MessageBox.Show("No patient selected, please select a patient before continuing.");
            }
            return true;
        }

        private void FillNewAppointmentInfo()
        {
            if (patientInfoPage == NewPatientPage)
            {
                FirstNameLabel.Content = NewFirstNameTb.Text;
                LastNameLabel.Content = NewLastNameTb.Text;

                string state = StateCb.Text.Substring(StateCb.Text.Length - 2);
                string address = String.Format("{0}, {1}, {2} {3}", StreetTb.Text, CityTb.Text, state, ZipTb.Text);
                AddressLabel.Content = address;
            }
            else
            {
                DataGrid patientDataGrid = GetDataGrid();
                Patient patient = (Patient)patientDataGrid.SelectedItem;

                FirstNameLabel.Content = patient.FirstName;
                LastNameLabel.Content = patient.LastName;
                AddressLabel.Content = patient.Address;
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid patientDataGrid = GetDataGrid();

            patientDataGrid.ItemsSource = null;
            patientDataGrid.Items.Refresh();

            PopulateDataGrid(patientDataGrid);
        }

        private DataGrid GetDataGrid()
        {
            DataGrid dataGrid = new DataGrid();
            foreach (UIElement child in ExistingPatientPage.Children)
            {
                if (child as DataGrid != null)
                {
                    dataGrid = (child as DataGrid);
                    break;
                }
            }
            return dataGrid;
        }

        private void PopulateDataGrid(DataGrid patientDataGrid)
        {
            List<Patient> patients = new List<Patient>();

            string findFirstName = FirstNameExistingTextbox.Text.ToString();
            string findLastName = LastNameExistingTextbox.Text.ToString();

            _ = findFirstName == String.Empty && findLastName == String.Empty ? (findFirstName = "%%", findLastName = "%%") : (null, null);

            ReceptionSqlHandler rcsql = new ReceptionSqlHandler();
            string query = rcsql.PatientNameQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = findFirstName;
                cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = findLastName;

                try
                {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
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
                catch (Exception e)
                {
                   MessageBox.Show("Error reading from database.");
                }
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            int patientId = GeneratePatientId();
            int visitId = GenerateVisitId();

            if (patientId > 0 && visitId > 0) {
                // Remove AM/PM and space from apptTime in order to create a new PatientAppointment
                string appointmentTime = apptTime.Content.ToString().Trim(' ', 'A', 'P', 'M');
                TimeSpan time = TimeSpan.Parse(appointmentTime);
                string receptNote = ReceptionNotesTb.Text;

                if (receptNote != String.Empty)
                {
                    if (patientInfoPage == NewPatientPage)
                    {
                        string firstName = FirstNameLabel.Content.ToString();
                        string lastName = LastNameLabel.Content.ToString();
                        string address = AddressLabel.Content.ToString();

                        Patient patient = new Patient(patientId, firstName, lastName, address, (decimal)0.0);
                        PatientAppointment appointment = new PatientAppointment(visitId, patientId, apptDate, time, (decimal)50, receptNote, "", "");

                        ConfirmNewPatientAndAppointment(patient, appointment);
                    }
                    else if (patientInfoPage == ExistingPatientPage)
                    {
                        DataGrid patientDataGrid = GetDataGrid();
                        Patient patient = (Patient)patientDataGrid.SelectedItem;
                        PatientAppointment appointment = new PatientAppointment(visitId, patient.PatientId, apptDate, time, (decimal)50, receptNote, "", "");

                        ConfirmNewAppointment(appointment);
                    }
                }
                else
                {
                    MessageBox.Show("Please fill out the reason for this visit.");
                }
            }
            else
            {
                MessageBox.Show("An error occurred when trying to generate Id's");
            }            
        }

        private void ConfirmNewAppointment(PatientAppointment appointment)
        {
            MessageBoxResult result = MessageBox.Show("Confirm new appointment?", "Appointment Confirmation", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    AddNewAppointmentToDB(appointment);
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
                    AddNewPatientToDB(patient);
                    AddNewAppointmentToDB(appointment);
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void AddNewPatientToDB(Patient patient)
        {

        }

        private void AddNewAppointmentToDB(PatientAppointment appointment)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) {
          NewAppointmentPage.Visibility = Visibility.Hidden;
          patientInfoPage.Visibility = Visibility.Visible;
        }

        private void CloseApptView_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private int GeneratePatientId()
        {
            int patientId = 0;

            ReceptionSqlHandler rcsql = new ReceptionSqlHandler();
            string query = rcsql.NumberOfPatientsQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };

                try
                {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        patientId = dataReader.GetInt32(0);
                    }
                    dataReader.Close();

                    return (patientId + 1);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error reading from database.");
                }
            }
            return patientId - 1;
        }

        private int GenerateVisitId()
        {
            int VisitId = 0;

            ReceptionSqlHandler rcsql = new ReceptionSqlHandler();
            string query = rcsql.NumberOfPatientsQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };

                try
                {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        VisitId = dataReader.GetInt32(0);
                    }
                    dataReader.Close();

                    return (VisitId + 1);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error reading from database.");
                }
            }
            return VisitId - 1;
        }
    }
}