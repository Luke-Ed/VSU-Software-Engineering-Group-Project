using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Project_2_EMS.App_Code;

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
            InitialPage.Visibility = Visibility.Visible;

            ApptDate.Content = String.Format("{0} | {1}", date.ToString("ddd dd, yyyy"), timeLabel.Content);
            apptDate = date;
            apptTime = timeLabel;
        }

        public NewAppointmentWindow(string firstName, string lastName, string receptNote, Label srcLabel, Label timeLabel, DateTime date)
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

        private void InitializeComboBox(){
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

            _ = !NewFirstNameTb.Text.All(Char.IsLetter) || NewFirstNameTb.Text == String.Empty ? isValid = false : false;
            _ = !NewLastNameTb.Text.All(Char.IsLetter) || NewLastNameTb.Text == String.Empty ? isValid = false : false;
;
            _ = !Regex.IsMatch(StreetTb.Text, @"^[a-zA-Z0-9.\s]+$") || StreetTb.Text == String.Empty ? isValid = false : false;
            _ = !CityTb.Text.All(Char.IsLetter) || CityTb.Text == String.Empty ? isValid = false : false;
            _ = StateCb.Text == String.Empty ? isValid = false : false;
            _ = !ZipTb.Text.All(Char.IsDigit) || ZipTb.Text == String.Empty ? isValid = false : false;

            return isValid;
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

        private void ContinueNewPatientBtn_Click(object sender, RoutedEventArgs e) {
          Boolean isValid = ValidNewPatientInfo();

          if (isValid) {
            patientInfoPage.Visibility = Visibility.Hidden;
            NewAppointmentPage.Visibility = Visibility.Visible;
          }
          else {
            MessageBox.Show("One or more fields are filled out incorrectly");
          }
        }

        private void ContinueExistingPatientBtn_Click(object sender, RoutedEventArgs e)
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
    }
}