using System;
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
                        MessageBox.Show("It Worked!!!!!!!!!!!!");
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

    }
}
