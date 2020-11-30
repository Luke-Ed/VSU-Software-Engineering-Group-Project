using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace Project_2_EMS {
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow {
    public MainWindow() {
      InitializeComponent();
      LabelLoginError.Visibility = Visibility.Hidden;
    }
    /*
    private void NurseButton_Click(object sender, RoutedEventArgs e) {
      Window nurseWindow = new NurseView(this);
      nurseWindow.Show();
      Hide();
    }

    private void ReceptionButton_Click(object sender, RoutedEventArgs e) {
      Window receptionWindow = new ReceptionistView(this);
      receptionWindow.Show();
      Hide();
    }
    */
    private void PatientButton_Click(object sender, RoutedEventArgs e) {
      Window patientWindow = new PatientView(this);
      patientWindow.Show();
      Hide();
    }

    private void MoveToNurseView() {
      Window nurseWindow = new NurseView(this);
      nurseWindow.Show();
      Hide();
    }

    private void MoveToReceptionView() {
      Window receptionWindow = new ReceptionistView(this);
      receptionWindow.Show();
      Hide();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e) {
      String username = TextBoxStaffUsername.Text;
      String password = PasswordBoxStaff.Password;
      StaffMember staffMember;
      if (username == String.Empty) {
        LabelLoginError.Content = "Username cannot be empty";
        LabelLoginError.Visibility = Visibility.Visible;
      }
      else if (password == String.Empty) {
        LabelLoginError.Content = "Password cannot be empty";
        LabelLoginError.Visibility = Visibility.Visible;
      }
      else {
        staffMember = SearchForUser(username);
        if (staffMember != null) {
          if (staffMember.Password == password) {
            switch (staffMember.AccessLevel) {
              case 1:
                MoveToReceptionView();
                break;
              case 2:
                MoveToNurseView();
                break;
              default:
                throw new InvalidOperationException("You broke it");
            }
          }
          else {
            LabelLoginError.Content = "Incorrect Username or Password";
            LabelLoginError.Visibility = Visibility.Visible;
          }
        }
        else {
          LabelLoginError.Content = "Incorrect Username or Password";
          LabelLoginError.Visibility = Visibility.Visible;
        }
      }
    }

    private StaffMember SearchForUser(String username) {
      String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
      SqlConnection connection = new SqlConnection(connectionString);

      SqlCommand cmd = new SqlCommand {
        Connection = connection,

        CommandText = "SELECT * FROM StaffLogin WHERE username = '" + username + "'"
      };

      connection.Open();

      SqlDataReader dataReader = cmd.ExecuteReader();
      StaffMember staffMember = null;
      while (dataReader.Read()) {
        int drId = dataReader.GetInt32(0);
        String drUsername = dataReader.GetString(1);
        String drPassword = dataReader.GetString(2);
        int drAccessLevel = dataReader.GetByte(3);
        String drFirstName = dataReader.GetString(4);
        String drLastName = dataReader.GetString(5);

        staffMember = new StaffMember(drId, drUsername, drPassword, drAccessLevel, drFirstName, drLastName);
      }

      return staffMember;
    }
  }
}