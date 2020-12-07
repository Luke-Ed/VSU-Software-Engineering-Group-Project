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

namespace Project_2_EMS
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        private readonly Window _parentWindow;
        private SqlConnection connection;

        public DoctorView (Window parentWindow)
        {
            _parentWindow = parentWindow;
            try
            {
                InitializeDBConnection();
            }
            catch (Exception e)
            {

            }
            InitializeComponent();
            Closing += OnWindowClosing;
            
        }

        private void InitializeDBConnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var mainWindow = _parentWindow;
            connection.Close();
            mainWindow.Show();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var mainWindow = _parentWindow;
            connection.Close();
            mainWindow.Close();
        }

        private void Patient_Information_Click(object sender, RoutedEventArgs e)
        {
            Grid1.Visibility = Visibility.Visible;
            Grid2.Visibility = Visibility.Hidden;

            //First_Name.Text = " ";
            //Last_Name.Text = " ";
            String firstName = First_Name.Text;
            String lastName = Last_Name.Text;
            DoctorSqlHandler doctorSqlHandler = new DoctorSqlHandler();
            String query = doctorSqlHandler.PatientQuerier(firstName, lastName);
            
            


        }

        private void Update_Patient_Info_Click(object sender, RoutedEventArgs e)
        {
            Grid1.Visibility = Visibility.Hidden;
            Grid2.Visibility = Visibility.Visible;
        }

    }
}
