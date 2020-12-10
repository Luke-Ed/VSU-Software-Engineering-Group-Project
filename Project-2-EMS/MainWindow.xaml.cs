using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_2_EMS {
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow {
    public MainWindow() {
      InitializeComponent();
    }

    private void MoveToNurseView() {
      Window nurseWindow = new NurseView(this);
      nurseWindow.Show();
      Hide();
    }

    private void MoveToReceptionView(StaffMember staffMember) {
      Window receptionWindow = new ReceptionistView(this, staffMember.FirstName);
      receptionWindow.Show();
      Hide();
    }

    private void MoveToPatientView(Patient patient) {
      Window patientWindow = new PatientView(this, patient);
      patientWindow.Show();
      Hide();
    }

    private void MoveToDoctorView()
        {
            Window doctorWindow = new DoctorView(this);
            doctorWindow.Show();
            Hide();
        }

    private void StaffLoginButton_Click(object sender, RoutedEventArgs e) {
      String username = Staff_TbUsername.Text;
      String password = Staff_TbPassword.Password;
      Label errorLabel = StaffLoginError_Label;

      LoginHandler("StaffLogin", username, password, errorLabel);
    }

    private void PatientLoginButton_Click(object sender, RoutedEventArgs e) {
      String username = Patient_TbUsername.Text;
      String password = Patient_TbPassword.Password;
      Label errorLabel = PatientLoginError_Label;

      LoginHandler("PatientLogin", username, password, errorLabel);
    }

    private void LoginHandler(String loginType, String username, String password, Label errorLabel) {
      if (username.Equals(String.Empty)) {
        errorLabel.Visibility = Visibility.Hidden;
        errorLabel.Content = "Username cannot be empty";
        errorLabel.Visibility = Visibility.Visible;
      }

      if (password.Equals(String.Empty)) {
        errorLabel.Visibility = Visibility.Hidden;
        errorLabel.Content = "Password cannot be empty";
        errorLabel.Visibility = Visibility.Visible;
      }

      switch (loginType) {
        case "StaffLogin": {
          StaffMember staffMember = (StaffMember) SearchForUser(loginType, username);
          if (staffMember != null) {
            if (staffMember.Password.Equals(password)) {
              switch (staffMember.AccessLevel) {
                case 1:
                  MoveToReceptionView(staffMember);
                  break;
                case 2:
                  MoveToNurseView();
                  break;
                case 3:
                  MoveToDoctorView();
                  break;
                default:
                  throw new InvalidOperationException("You broke it");
              }
            }
            else {
              errorLabel.Visibility = Visibility.Hidden;
              errorLabel.Content = "Incorrect Username or Password";
              errorLabel.Visibility = Visibility.Visible;
            }
          }
          else {
            errorLabel.Visibility = Visibility.Hidden;
            errorLabel.Content = "Incorrect Username or Password";
            errorLabel.Visibility = Visibility.Visible;
          }
          break;
        }
        case "PatientLogin": {
          PatientLogin patientLogin = (PatientLogin) SearchForUser(loginType, username);
          if (patientLogin != null) {
            if (patientLogin.Password.Equals(password)) {
              Patient patient = FindPatient(patientLogin);
              MoveToPatientView(patient);
            }
            else {
              errorLabel.Visibility = Visibility.Hidden;
              errorLabel.Content = "Incorrect Username or Password";
              errorLabel.Visibility = Visibility.Visible;
            }
          }
          else {
            errorLabel.Visibility = Visibility.Hidden;
            errorLabel.Content = "Incorrect Username or Password";
            errorLabel.Visibility = Visibility.Visible;
          }
          break;
        }
        default:
          throw new InvalidOperationException();
      }
    }

    private Patient FindPatient(PatientLogin login) {
      String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
      SqlConnection connection = new SqlConnection(connectionString);

      Patient patient = null;

      SqlCommand cmd = new SqlCommand() {
        Connection = connection,

        CommandText = "SELECT * FROM PatientInfo WHERE PatientID = " + login.PatientId
      };

      connection.Open();

      SqlDataReader dataReader = cmd.ExecuteReader();

      while (dataReader.Read()) {
        int drPatientId = dataReader.GetInt32(0);
        String drLastName = dataReader.GetString(1);
        String drFirstName = dataReader.GetString(2);
        String drAddress = dataReader.GetString(3);
        Decimal drBalance = dataReader.GetDecimal(4);

        patient = new Patient(drPatientId, drFirstName, drLastName, drAddress, drBalance);
      }

      return patient;
    }

    private Object SearchForUser(String loginType, String username) {
      String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
      SqlConnection connection = new SqlConnection(connectionString);

      SqlCommand cmd = new SqlCommand {
        Connection = connection,

        CommandText = "SELECT * FROM " + loginType + " WHERE username = '" + username + "'"
      };

      connection.Open();

      SqlDataReader dataReader = cmd.ExecuteReader();

      if (loginType.Equals("StaffLogin")) {
        StaffMember login = null;

        while (dataReader.Read()) {
          int drId = dataReader.GetInt32(0);
          String drUsername = dataReader.GetString(1);
          String drPassword = dataReader.GetString(2);
          int drAccessLevel = dataReader.GetByte(3);
          String drFirstName = dataReader.GetString(4);
          String drLastName = dataReader.GetString(5);

          login = new StaffMember(drId, drUsername, drPassword, drAccessLevel, drFirstName, drLastName);
        }
        connection.Close();
        return login;
      }
      else {
        PatientLogin login = null;

        while (dataReader.Read()) {
          int drId = dataReader.GetInt32(0);
          String drUsername = dataReader.GetString(1);
          String drPassword = dataReader.GetString(2);
          int drPatientId = dataReader.GetInt32(3);

          login = new PatientLogin(drId, drUsername, drPassword, drPatientId);
        }
        connection.Close();
        return login;
      }
      
    }
  }
}