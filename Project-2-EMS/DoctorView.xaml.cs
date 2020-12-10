﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Configuration;
using System.ComponentModel;
using Project_2_EMS.App_Code;
using System.Data.SqlClient;
using System.Data;


namespace Project_2_EMS
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        private List<Patient> patients = new List<Patient>();
        private Patient patient;

        private readonly Window _parentWindow;

        public DoctorView (Window parentWindow)
        {
            _parentWindow = parentWindow;
            InitializeComponent();
            Closing += OnWindowClosing;
            
        }



        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var mainWindow = _parentWindow;
            mainWindow.Show();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var mainWindow = _parentWindow;
            mainWindow.Close();
        }
        private Boolean IsPatientSelected()
        {
            Boolean isValid = true;

            foreach (UIElement child in Patient_Information_Grid.Children)
            {
                _ = child as DataGrid != null ? (child as DataGrid).SelectedIndex < 0 ? isValid = false : true : true;
            }

            return isValid;
        }


        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            String first_Name = First_Name.Text;
            String last_Name = Last_Name.Text;
            DoctorSqlHandler doctorSqlHandler = new DoctorSqlHandler();
            String query = doctorSqlHandler.PatientNameQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = first_Name;
                cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = last_Name;

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
                        decimal balance = dataReader.GetDecimal(4);

                        Patient patient = new Patient(patientId, lastName, firstName, address, balance);
                        patients.Add(patient);
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading from database.");
                }
            }
            DataGrid dataGrid = new DataGrid();
            foreach(UIElement child in Patient_Information_Grid.Children)
            {
                if(child as DataGrid != null)
                {
                    dataGrid = child as DataGrid;
                }
            }
            dataGrid.ItemsSource = patients;

            Patient p = (Patient)dataGrid.SelectedItem;
            

        }



        private void ViewPatient_Click(object sender, RoutedEventArgs e)
        {
            if (IsPatientSelected())
            {
                Patient_Information_Grid.Visibility = Visibility.Hidden;
                ViewPatientInformation_Grid.Visibility = Visibility.Visible;
 

                DataGrid dataGrid = new DataGrid();
                foreach (UIElement child in Patient_Information_Grid.Children)
                {
                    if (child as DataGrid != null)
                    {
                        dataGrid = child as DataGrid;
                    }
                }
                patient = (Patient)dataGrid.SelectedItem;
                firstName.Content = patient.FirstName;
                lastName.Content = patient.LastName;
                address.Content = patient.Address;
                patientID.Content = patient.PatientId;

                PopulatePrescriptionDataGrid(patient.PatientId);



            }



        }
        private int GeneratePrescriptionId()
        {
            int prescriptionId = 0;

            DoctorSqlHandler doctorSql = new DoctorSqlHandler();
            String query = doctorSql.PrescriptionNumberQuerier();

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
                        prescriptionId = dataReader.GetInt32(0);
                    }
                    dataReader.Close();

                    return (prescriptionId + 1);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error reading from database.");
                }
            }
            return prescriptionId - 1;
        }

        private void AddPerscription_Click(object sender, RoutedEventArgs e)
        {
            int prescriptionID = GeneratePrescriptionId();
            int patientId = patient.PatientId ;
            PatientAppointment pA = GetPatientAppointment(patientId);
            if(pA != null)
            {
                int visitID = pA.VisitId;
                string prescriptionName = PrescriptionName_Text.Text;
                string prescriptionNotes = PrescriptionNotes_Text.Text;
                int refills = Convert.ToInt32(PrescriptionRefills_Text.Text);
                Prescription prescription = new Prescription(prescriptionID, patientId, visitID, prescriptionName, prescriptionNotes, refills);
                AddNewPrescription(prescription);
            }
            else
            {
                MessageBox.Show("No Appointment");            
            }


 


        }


        private void PopulatePrescriptionDataGrid(int pID)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            DoctorSqlHandler doctorSqlHandler = new DoctorSqlHandler();
            String query = doctorSqlHandler.PerscriptionQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@patientID", SqlDbType.Int).Value = pID;
                

                //try
                //{
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int prescriptionID = dataReader.GetInt32(0);
                        int patientId = dataReader.GetInt32(1);
                        int visitID = dataReader.GetInt32(2);
                        string prescriptionName = dataReader.GetString(3);
                        string prescriptionNotes = dataReader.GetString(4);
                        int refills = dataReader.GetByte(5);

                        Prescription prescription = new Prescription(prescriptionID, patientId, visitID, prescriptionName, prescriptionNotes, refills);
                        prescriptions.Add(prescription);
                        
                    }

                //}
                //catch (Exception ex)
                //{
                    //MessageBox.Show("Error reading from database.");
                //}
                DataGrid dataGrid = new DataGrid();
                foreach (UIElement child in PrescriptionDataGridParent.Children)
                {
                    if (child as DataGrid != null)
                    {
                        dataGrid = child as DataGrid;
                    }
                }
                dataGrid.ItemsSource = prescriptions;
            }
        }

        private PatientAppointment GetPatientAppointment(int pID)
        {
            PatientAppointment patientAppointment = null;
            DoctorSqlHandler doctorSql = new DoctorSqlHandler();
            String query = doctorSql.AppointmentQuerier();

            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@apptDate", SqlDbType.DateTime).Value = DateTime.Now.Date;
                cmd.Parameters.Add("@patientID", SqlDbType.Int).Value = pID;
                try
                {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
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
                catch (Exception e)
                {
                    MessageBox.Show("Error reading from database.");
                }


            }
            return patientAppointment;
        }

        private void AddNewPrescription(Prescription prescription)
        {
            DoctorSqlHandler doctorSql = new DoctorSqlHandler();
            string query = doctorSql.UpdatePatientPrecriptionQuerier();
            DatabaseConnectionManager dbConn = new DatabaseConnectionManager();

            using (SqlConnection connection = dbConn.ConnectToDatabase())
            {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@prescriptionID", SqlDbType.Int).Value = prescription.PrescriptionID;
                cmd.Parameters.Add("@patientID", SqlDbType.Int).Value = prescription.PatientID;
                cmd.Parameters.Add("@visitID", SqlDbType.Int).Value = prescription.VisitID;
                cmd.Parameters.Add("@prescriptionName", SqlDbType.Text).Value = prescription.PrescriptionName;
                cmd.Parameters.Add("@prescriptionNotes", SqlDbType.Text).Value = prescription.PresciprtionNotes;
                cmd.Parameters.Add("@refills", SqlDbType.TinyInt).Value = prescription.Refills;



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
    }
}
