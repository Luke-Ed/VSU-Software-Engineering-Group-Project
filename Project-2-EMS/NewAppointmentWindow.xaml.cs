using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_2_EMS {
    /// <summary>
    /// Interaction logic for NewAppointmentWindow.xaml
    /// </summary>
    public partial class NewAppointmentWindow {
        private DateTime apptDate;
        private Label apptTime;
        private Grid patientInfoPage;

        public NewAppointmentWindow(Label srcLabel, Label timeLabel, DateTime date){
            InitializeComponent();
            InitializeComboBox();

            ApptDate.Content = String.Format("{0} | {1}", date.ToString("ddd dd, yyyy"), timeLabel.Content);
            apptDate = date;
            apptTime = timeLabel;
        }

        private void InitializeComboBox(){
            String[] states = new String[] { "Alabama, AL", "Alaska, AK", "Arizona, AZ", "Arkansas, AR", "California, CA", "Colorado, CO", "Connecticut, CT",
                                             "Delaware, DE", "Florida, FL", "Georgia, GA", "Hawaii, HI", "Idaho, ID", "Illinois, IL", "Indiana, IN", "Iowa, IA",
                                             "Kansas, KS", "Kentucky, KY", "Louisiana, LA", "Maine, ME", "Maryland, MD", "Massachusetts, MA", "Michigan, MI",
                                             "Minnesota, MN", "Mississippi, MS", "Missouri, MO", "Montana, MT", "Nebraska, NE", "Nevada, NV", "New Hampshire, NH",
                                             "New Jersey, NJ", "New Mexico, NM", "New York, NY", "North Carolina, NC", "North Dakota, ND", "Ohio, OH", "Oklahoma, OK",
                                             "Oregon, OR", "Pennsylvania, PA", "Rhode Island, RI", "South Carolina, SC", "South Dakota, SD", "Tennessee, TN", "Texas, TX",
                                             "Utah, UT", "Vermont, VT", "Virginia, VA", "Washington, WA", "West Virginia, WV", "Wisconsin, WI", "Wyoming, WY" };

            StateComboBox.ItemsSource = states;
        }

        private Boolean CheckIfTextEmpty(Grid grid) {
            Boolean isEmpty = false;
            foreach (UIElement child in grid.Children){
                _ = child as TextBox != null ? (child as TextBox).Text == String.Empty ? isEmpty = true : false : false;
                _ = child as ComboBox != null ? (child as ComboBox).Text == String.Empty ? isEmpty = true : false : false;
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