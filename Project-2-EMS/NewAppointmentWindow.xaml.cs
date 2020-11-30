using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_2_EMS {
  public partial class NewAppointmentWindow {
    private DateTime _appointmentDate;
    private Label _appointmentTime;
    private Grid prevPage;

    public NewAppointmentWindow(Label srcLabel, Label timeLabel, DateTime date) {
      InitializeComponent();
      ApptDate.Content = $"{date:ddd dd, yyyy} | {timeLabel.Content}";

      _appointmentDate = date;
      _appointmentTime = timeLabel;
    }

    private bool CheckIfTextEmpty(Grid grid) {
      var isEmpty = false;
      foreach (UIElement child in grid.Children) {
        _ = child as TextBox != null ? (child as TextBox).Text == string.Empty ? isEmpty = true : false : false;
        _ = child as ComboBox != null ? (child as ComboBox).Text == string.Empty ? isEmpty = true : false : false;
        _ = child as DataGrid != null ? (child as DataGrid).Items.Count == 0 ? isEmpty = true : false : false;
      }

      return isEmpty;
    }

    private static void ClearChildren(Grid grid) {
      foreach (UIElement child in grid.Children) {
        _ = child as TextBox != null ? (child as TextBox).Text = string.Empty : null;
        _ = child as ComboBox != null ? (child as ComboBox).Text = string.Empty : null;
      }
    }

    private void NewPatientBtn_Click(object sender, RoutedEventArgs e) {
      InitialPage.Visibility = Visibility.Hidden;
      NewPatientPage.Visibility = Visibility.Visible;
      prevPage = NewPatientPage;
    }

    private void ExistingPatientBtn_Click(object sender, RoutedEventArgs e) {
      InitialPage.Visibility = Visibility.Hidden;
      ExistingPatientPage.Visibility = Visibility.Visible;
      prevPage = ExistingPatientPage;
    }

    private void CancelBtn_Click(object sender, RoutedEventArgs e) {
      ClearChildren(prevPage);

      prevPage.Visibility = Visibility.Hidden;
      InitialPage.Visibility = Visibility.Visible;
    }

    private void ContinueNewPatientBtn_Click(object sender, RoutedEventArgs e) {
      Boolean isEmpty = CheckIfTextEmpty(prevPage);

      if (!isEmpty) {
        prevPage.Visibility = Visibility.Hidden;
        NewAppointmentPage.Visibility = Visibility.Visible;
      }
      else {
        MessageBox.Show("All fields must be filled in before proceeding.");
      }
    }

    private void BackBtn_Click(object sender, RoutedEventArgs e) {
      NewAppointmentPage.Visibility = Visibility.Hidden;
      prevPage.Visibility = Visibility.Visible;
    }
  }
}