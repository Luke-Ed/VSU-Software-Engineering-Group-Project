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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_2_EMS {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow {
    public MainWindow() {
      InitializeComponent();
    }
    //String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
    //SqlConnection connection = new SqlConnection(connectionString);
    //connection.Open();
    private void NurseButton_Click(object sender, RoutedEventArgs e) {
      Window nurseWindow = new NurseView(this);
      nurseWindow.Show();
      Hide();
    }

    private void PatientButton_Click(object sender, RoutedEventArgs e) {
      Window patientWindow = new PatientView(this);
      patientWindow.Show();
      Hide();
    }

    private void ReceptionButton_Click(object sender, RoutedEventArgs e) {
      Window receptionWindow = new ReceptionistView(this);
      receptionWindow.Show();
      Hide();
    }
  }
}
