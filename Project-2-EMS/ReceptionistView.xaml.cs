using Project_2_EMS.App_Code;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_2_EMS {
    public partial class ReceptionistView {
        private readonly Window _parentWindow;
        private Window newApptWindow;
        private readonly SharedSqlHandler sharedSqlHandler = new SharedSqlHandler();
        private readonly DatabaseConnectionManager dbConnMan = new DatabaseConnectionManager();

        private List<PatientAppointment> appointments = new List<PatientAppointment>();
        private List<Patient> patients = new List<Patient>();

        private DateTime weekDate;
        private DateTime prevWeekDate;

        public ReceptionistView(Window parentWindow, String staffMember) {
            InitializeComponent();
            InitializeHeadLabels();
            UpdateReceptionistView();

            GreetingGrid.Content = "Greetings " + staffMember + "!";

            _parentWindow = parentWindow;
            Closing += OnWindowClosing;
        }

        private void InitializeHeadLabels()
        {
            weekDate = DateTime.Now.AddDays(Convert.ToDouble(DateTime.Now.DayOfWeek.ToString("d")) * -1.0);
            ApptCalendar.SelectedDate = DateTime.Now.Date;
            AppointmentWeek.Content = weekDate.ToString("Week o\\f MMMM dd, yyyy");
            SignInDate.Content = DateTime.Now.Date.ToLongDateString();
        }

        public void UpdateReceptionistView()
        {
            ClearAppointmentGrid();
            PopulateAppointmentGrid(patients, appointments);
            PopulateSignInView();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            Hide();

            if (newApptWindow != null) newApptWindow.Close();

            var mainWindow = _parentWindow;
            mainWindow.Show();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e) {
            var mainWindow = _parentWindow;

            if (newApptWindow != null) newApptWindow.Close();

            mainWindow.Close();
        }

        // Change which view is visible when you select buttons from the control panel
        private void ControlButton_Click(object sender, RoutedEventArgs e) {
            Button btn = e.Source as Button;
            foreach (Grid grid in ViewPanel.Children) {
                _ = grid.Name.Contains(btn.Name) ? grid.Visibility = Visibility.Visible : grid.Visibility = Visibility.Hidden;
            }
            ApptButtonGrid.Visibility = Visibility.Hidden;
            ClearPatientBilling();
        }

        // Change the displayed date when you select a date on the calendar gui, highlight the day on the appointments calendar
        private void ApptCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            if (ApptCalendar.SelectedDate.HasValue) {
                var date = ApptCalendar.SelectedDate.Value;
                var dayNum = Convert.ToDouble(ApptCalendar.SelectedDate.Value.DayOfWeek.ToString("d"));

                weekDate = date.AddDays(dayNum * -1.0);
                AppointmentWeek.Content = weekDate.ToString("Week o\\f MMMM dd, yyyy");

                HighlightCalendarDay(AppointmentDays, 0, (int)dayNum + 1);

                if (prevWeekDate != weekDate) {
                    prevWeekDate = weekDate;
                    UpdateReceptionistView();
                }
            }
        }

        // Code obtained from https://stackoverflow.com/questions/25352961/have-to-click-away-twice-from-calendar-in-wpf
        /**
         *  When clicking inside the calendar view, you would need to double click outside of it before being able
         *  to click on something outside of it. This code prevents that from happening
         */
        private void ApptCalendar_GotMouseCapture(object sender, MouseEventArgs e) {
            UIElement originalElement = e.OriginalSource as UIElement;
            if (originalElement is CalendarDayButton || originalElement is CalendarItem) {
                originalElement.ReleaseMouseCapture();
            }
        }

        private void GetPatientAppointments() {
          string query = sharedSqlHandler.AppointmentQuerier("DateRange");
          using (SqlConnection connection = dbConnMan.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@ApptStartDate", SqlDbType.DateTime).Value = weekDate;
                cmd.Parameters.Add("@ApptEndDate", SqlDbType.DateTime).Value = weekDate.AddDays(6);
           
                try {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read()) {
                        int visitId = dataReader.GetInt32(0);
                        int patientId = dataReader.GetInt32(1);
                        DateTime apptDate = dataReader.GetDateTime(2);
                        TimeSpan apptTime = dataReader.GetTimeSpan(3);
                        decimal cost = dataReader.GetDecimal(4);
                        string receptNote = dataReader.GetString(5);
                        string nurseNote = dataReader.GetString(6);
                        string doctorNote = dataReader.GetString(7);

                        PatientAppointment appointment = new PatientAppointment(visitId, patientId, apptDate, apptTime, cost, receptNote, nurseNote, doctorNote);
                        appointments.Add(appointment);
                    }
                }
                catch (Exception) {
                    MessageBox.Show("Error reading from database.");
                } 
          }
        }

        private void GetPatientById(int patId) {
          string query = sharedSqlHandler.PatientIdQuerier();
          using (SqlConnection connection = dbConnMan.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@PatientId", SqlDbType.Int).Value = patId;

                try {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read()) {
                        int patientId = dataReader.GetInt32(0);
                        string lastName = dataReader.GetString(1);
                        string firstName = dataReader.GetString(2);
                        string address = dataReader.GetString(3);
                        decimal balance = dataReader.GetDecimal(4);

                        Patient patient = new Patient(patientId, firstName, lastName, address, balance);
                        patients.Add(patient);
                    }
                }
                catch (Exception) {
                    MessageBox.Show("Error reading from database.");
                }
          }
        }

        private Patient GetPatientByName() {
            Patient patient = null;

            string findFirstName = BillingFirstNameTb.Text;
            string findLastName = BillingLastNameTb.Text;

            ReceptionSqlHandler rcsql = new ReceptionSqlHandler();
            string query = rcsql.PatientNameExactQuerier();

            using (SqlConnection connection = dbConnMan.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@firstName", SqlDbType.Text).Value = findFirstName.Trim(' ');
                cmd.Parameters.Add("@lastName", SqlDbType.Text).Value = findLastName.Trim(' ');

                try {
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read()) {
                        int patientId = dataReader.GetInt32(0);
                        string lastName = dataReader.GetString(1);
                        string firstName = dataReader.GetString(2);
                        string address = dataReader.GetString(3);
                        Decimal balance = dataReader.GetDecimal(4);

                        patient = new Patient(patientId, firstName, lastName, address, balance);
                    }

                    dataReader.Close();
                }
                catch (Exception) {
                    MessageBox.Show("Error reading from database.");
                }
            }
            return patient;
        }

        private void PopulateSignInView() {
            ClearSigninView();
            int rowIndex = 0;
            foreach (PatientAppointment pa in appointments) {
                if (pa.ApptDate == DateTime.Now.Date) {
                    foreach (Patient p in patients) {
                        if (pa.PatientId == p.PatientId) {
                            Label visitId = GetChild(SignInVisitId, rowIndex, 0) as Label;
                            Label lastName = GetChild(SignInLastName, rowIndex, 0) as Label;
                            Label firstName = GetChild(SignInFirstName, rowIndex, 0) as Label;

                            visitId.Content = "[ " + pa.VisitId + " ]";
                            lastName.Content = p.LastName;
                            firstName.Content = p.FirstName;

                            rowIndex += 1;
                            break;
                        }
                    }
                }
            }
        }

        private void UpdateDbPatientBalance(int patientId, decimal cost) {
            ReceptionSqlHandler rcsql = new ReceptionSqlHandler();
            string query = rcsql.UpdatePatientBalance();
            
            using (SqlConnection connection = dbConnMan.ConnectToDatabase()) {
                SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                cmd.Parameters.Add("@cost", SqlDbType.Decimal).Value = cost;
                cmd.Parameters.Add("@patientId", SqlDbType.Int).Value = patientId;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when attempting to update patient balance.");
                }
            }
        }

        private void ClearSigninView()
        {
            foreach (Label label in SignInVisitId.Children) { label.Content = String.Empty; }
            foreach (Label label in SignInLastName.Children) { label.Content = String.Empty; }
            foreach (Label label in SignInFirstName.Children) { label.Content = String.Empty; }
        }

        // Populate the appointment grids with appropriate appointments
        private void PopulateAppointmentGrid(List<Patient> patients, List<PatientAppointment> appointments) {
            GetPatientAppointments();

            foreach (PatientAppointment pa in appointments) {
                GetPatientById(pa.PatientId);
            }

            foreach (PatientAppointment appt in appointments) {
                string apptTime = String.Empty;
                double day = Convert.ToDouble(appt.ApptDate.DayOfWeek.ToString("d"));

                int diff = TimeSpan.Compare(appt.ApptTime, new TimeSpan(12,0,0));

                _ = diff > 0 ? apptTime = string.Format("{0:h\\:mm} PM", appt.ApptTime.Subtract(TimeSpan.FromHours(12))) : null;
                _ = diff == 0 ? apptTime = string.Format("{0:h\\:mm} PM", appt.ApptTime) : null;
                _ = diff < 0 ? apptTime = string.Format("{0:h\\:mm} AM", appt.ApptTime) : null;


                foreach (Label child in AppointmentTimes.Children) {
                    if (apptTime.CompareTo(child.Content.ToString()) == 0) {
                        Label apptLabel = GetChild(AppointmentGrids, Grid.GetRow(child), (int)day - 1) as Label;

                        // Grab the current appt index to be able to get the patient at the same index
                        int index = appointments.IndexOf(appt);

                        string firstName = patients.ElementAt(index).FirstName;
                        string lastInitial = patients.ElementAt(index).LastName;
                        string visitId = appt.VisitId.ToString();

                        apptLabel.Content = String.Format("{0} {1}.\nVisit Id: {2}", firstName, lastInitial.Substring(0,1), visitId);
                        apptLabel.Background = Brushes.DarkGreen;
                    }
                }
            }
        }

        // Clear the appointment grids (Used when changing week view)
        private void ClearAppointmentGrid() {
            appointments.Clear();
            patients.Clear();

            foreach (Label child in AppointmentGrids.Children) {
                var bc = new BrushConverter();
                child.Background = (Brush)bc.ConvertFrom("#FF30373E");
                child.Content = String.Empty;
            }
        }

        // Get individual child from UIElement
        private static UIElement GetChild(Grid grid, int row, int column) {
            foreach (UIElement child in grid.Children) {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column) {
                    return child;
                }
            }
            return null;
        }

        // Highlight the selected day on the appointments calendar
        private static void HighlightCalendarDay(Grid grid, int row, int column) {
            foreach (Label label in grid.Children) {
                Boolean labelMatch = Grid.GetRow(label) == row && Grid.GetColumn(label) == column;
                if (labelMatch) {
                    var bc = new BrushConverter();
                    label.Background = (Brush)bc.ConvertFrom("#FF4669B0");
                }
                else {
                    var bc = new BrushConverter();
                    label.Background = (Brush)bc.ConvertFrom("#FF26282C");
                }
            }
        }

        // Highlight the selected cell on the appointments calendar     
        private static void HighlightCalendarCell(Grid grid, int row, int column) {
            foreach (Label child in grid.Children) {
                Boolean childMatch = Grid.GetRow(child) == row && Grid.GetColumn(child) == column;
                _ = childMatch ? child.Margin = new Thickness(2) : child.Margin = new Thickness(0.5);
            }
        }

        // Called when a cell on the appointments calendar is selected
        private void ApptDate_MouseDown(object sender, MouseButtonEventArgs e) {
            Label srcLabel = e.Source as Label;

            // Highlight the selected cell and day
            HighlightCalendarCell(AppointmentGrids, Grid.GetRow(srcLabel), Grid.GetColumn(srcLabel));
            HighlightCalendarDay(AppointmentDays, 0, Grid.GetColumn(srcLabel) + 2);

            // Show the selected date on the calendar view
            DateTime date = weekDate.AddDays(Grid.GetColumn(srcLabel) + 1);
            ApptCalendar.SelectedDate = date;

            // Show a new/view appointment button whenever a cell is selected
            ApptButtonGrid.Visibility = Visibility.Visible;
            Boolean srcLabelEmpty = srcLabel.Content.ToString() == String.Empty;
            _ = srcLabelEmpty ? ViewApptButton.Content = "New Appointment" : ViewApptButton.Content = "View Appointment";
        }

        // Called when a cell on the appointments calendar has been double clicked
        private void ApptDate_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (newApptWindow != null) {
                newApptWindow.Close();
            }

            Label srcLabel = e.Source as Label;

            OpenAppointmentView(srcLabel);
        }

        // Button with similar functionality to double clicking a cell on the appointments calendar
        private void ViewApptButton_Click(object sender, RoutedEventArgs e) {
            if (newApptWindow != null) {
                newApptWindow.Close();
            }

            Label srcLabel = null;
            Thickness thc = new Thickness(2);

            // Find the child corresponding to the selected cell on the appointments calendar
            foreach (Label child in AppointmentGrids.Children)
            {
                _ = child.Margin.Equals(thc) ? srcLabel = child : null;
            }

            OpenAppointmentView(srcLabel);
        }

        // Open a separate window when viewing/adding appointments
        private void OpenAppointmentView(Label srcLabel) {
            Label timeLabel = GetChild(AppointmentTimes, Grid.GetRow(srcLabel), 0) as Label;
            DateTime date = weekDate.AddDays(Grid.GetColumn(srcLabel) + 1);

            if (srcLabel.Content.ToString() != String.Empty) {
                int patientIndex = 0;
                int visitId = Convert.ToInt32(string.Join("", srcLabel.Content.ToString().ToCharArray().Where(Char.IsDigit)));

                foreach (PatientAppointment pa in appointments) {
                    if (pa.VisitId == visitId) {
                        patientIndex = appointments.IndexOf(pa);
                        break;
                    }
                }

                Patient patient = patients.ElementAt(patientIndex);
                PatientAppointment appointment = appointments.ElementAt(patientIndex);

                string firstName = patient.FirstName;
                string lastName = patient.LastName;
                string notes = appointment.ReceptNote;

                ReceptionistView recView = this;
                newApptWindow = new NewAppointmentWindow(recView, visitId, firstName, lastName, notes, timeLabel, date);
                newApptWindow.Show();
            }
            else {   
                if (DateTime.Compare(date.Date, DateTime.Now.Date) >= 0) {
                    ReceptionistView recView = this;
                    newApptWindow = new NewAppointmentWindow(recView, timeLabel, date);
                    newApptWindow.Show();
                }
            }
        }

        private void SearchPatientBtn_Click(object sender, RoutedEventArgs e) {
            UpdateBillingInformation();
        }

        private void UpdateBillingInformation() {
            Patient patient = GetPatientByName();

            if (patient != null) {
                BillingPatient.Content = patient.FirstName + " " + patient.LastName;
                BillingPatientId.Content = patient.PatientId;

                string balance = string.Format("${0:N2}", patient.Balance);

                BillingPatientBalance.Content = balance;
                BillingOwedAmount.Content = balance;
            }
            else {
                ClearPatientBilling();
            }
        }

        private void ClearPatientBillingBtn_Click(object sender, RoutedEventArgs e) {
            ClearPatientBilling();
        }

        private void ClearPatientBilling() {
            BillingFirstNameTb.Text = String.Empty;
            BillingLastNameTb.Text = String.Empty;
            BillingPatient.Content = String.Empty;
            BillingPatientId.Content = String.Empty;
            BillingPatientBalance.Content = String.Empty;
            BillingOwedAmount.Content = String.Empty;
            BillingPayAmount.Text = String.Empty;
            BillingChange.Content = String.Empty;
        }

        private void PayPatientBillingBtn_Click(object sender, RoutedEventArgs e) {
            if (BillingPatientId.Content.ToString() != String.Empty) {
                decimal oweAmount = Convert.ToDecimal(BillingOwedAmount.Content.ToString().Substring(1));

                if (oweAmount > 0) {
                    PaymentVerificationHandling(oweAmount);
                }
            }
        }

        private void PaymentVerificationHandling(decimal oweAmount) {
            if (IsValidPayment()) {
                string stringPayAmount = string.Format("{0:N2}", Convert.ToDecimal(BillingPayAmount.Text)).Trim(' ');
                decimal payAmount = Convert.ToDecimal(stringPayAmount);

                decimal amountPaid = 0;
                _ = oweAmount > payAmount ? (amountPaid = payAmount, null) : (amountPaid = oweAmount, BillingChange.Content = "$" + (payAmount - oweAmount).ToString());

                int patientId = Convert.ToInt32(new String(BillingPatientId.Content.ToString().Where(Char.IsDigit).ToArray()).Trim(' '));

                ConfirmPayment(patientId, amountPaid);
                UpdateBillingInformation();
            }
            else {
                MessageBox.Show("Invalid payment.");
            }
        }

        private Boolean IsValidPayment() {
            bool isValid = true;

            _ = !Regex.IsMatch(BillingPayAmount.Text, @"^[0-9.]+$") ? isValid = false : true;

            return isValid;
        }

        private void ConfirmPayment(int patientId, Decimal amountPaid) {
            string confirmation = "Confirm payment amount of " + amountPaid.ToString() + "?";
            MessageBoxResult result = MessageBox.Show(confirmation, "Payment Confirmation", MessageBoxButton.YesNo);

            switch (result) {
                case MessageBoxResult.Yes:
                    BillingPayAmount.Text = String.Empty;
                    UpdateDbPatientBalance(patientId, Decimal.Negate(amountPaid));
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void NextPrevWeekBtn_Click(object sender, RoutedEventArgs e) {
            Button btn = e.Source as Button;

            DateTime date = (DateTime)ApptCalendar.SelectedDate;
            DateTime nextWeek = date.AddDays(7);
            DateTime prevWeek = date.AddDays(-7);

            if (btn.Content.ToString() == "Next") {
                ApptCalendar.SelectedDate = nextWeek;
                ApptCalendar.DisplayDate = nextWeek;
            }
            else {
                ApptCalendar.SelectedDate = prevWeek;
                ApptCalendar.DisplayDate = prevWeek;
            }
        }
    }
}