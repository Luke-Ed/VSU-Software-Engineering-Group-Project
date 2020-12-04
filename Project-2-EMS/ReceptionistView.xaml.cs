using Project_2_EMS.App_Code;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_2_EMS
{
    public partial class ReceptionistView : Window
    {
        private readonly Window _parentWindow;
        private Window newApptWindow;
        private SqlConnection connection;

        private List<PatientAppointment> appointments = new List<PatientAppointment>();
        private List<Patient> patients = new List<Patient>();

        private DateTime weekDate;
        private DateTime prevWeekDate;

        public ReceptionistView(Window parentWindow)
        {
            _parentWindow = parentWindow;
            InitializeComponent();
            InitializeDBConnection();

            weekDate = DateTime.Now.AddDays(Convert.ToDouble(DateTime.Now.DayOfWeek.ToString("d")) * -1.0);
            AppointmentWeek.Content = weekDate.ToString("Week o\\f MMMM dd, yyyy");

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

            if (newApptWindow != null) newApptWindow.Close();

            connection.Close();
            var mainWindow = _parentWindow;
            mainWindow.Show();
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var mainWindow = _parentWindow;

            if (newApptWindow != null) newApptWindow.Close();

            connection.Close();
            mainWindow.Close();
        }

        // Change which view is visible when you select buttons from the control panel
        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            foreach (Grid grid in ViewPanel.Children)
            {
                _ = grid.Name.Contains(btn.Name) ? grid.Visibility = Visibility.Visible : grid.Visibility = Visibility.Hidden;
            }
            ApptButtonGrid.Visibility = Visibility.Hidden;
        }

        // Change the displayed date when you select a date on the calendar gui, highlight the day on the appointments calendar
        private void ApptCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ApptCalendar.SelectedDate.HasValue)
            {
                var date = ApptCalendar.SelectedDate.Value;
                var dayNum = Convert.ToDouble(ApptCalendar.SelectedDate.Value.DayOfWeek.ToString("d"));

                weekDate = date.AddDays(dayNum * -1.0);
                AppointmentWeek.Content = weekDate.ToString("Week o\\f MMMM dd, yyyy");

                //var apptDays = GetChildren(AppointmentDays);
                HighlightCalendarDay(AppointmentDays, 0, (int)dayNum + 1);

                if (prevWeekDate != weekDate)
                {
                    prevWeekDate = weekDate;
                    ClearAppointmentGrid();

                    appointments.Clear();
                    patients.Clear();

                    ReceptionSqlHandler rcsql = new ReceptionSqlHandler();
                    string query = rcsql.AppointmentQuerier(weekDate);

                    SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int patientId = dataReader.GetInt32(0);
                        string lastName = dataReader.GetString(1);
                        string firstName = dataReader.GetString(2);
                        string address = dataReader.GetString(3);
                        //decimal balance = dataReader.GetDecimal(4);

                        Patient p = new Patient(patientId, lastName, firstName, address, (decimal)1.0);
                        patients.Add(p);

                        int visitId = dataReader.GetInt32(5);
                        patientId = dataReader.GetInt32(6);
                        DateTime apptDate = dataReader.GetDateTime(7);
                        TimeSpan apptTime = dataReader.GetTimeSpan(8);
                        decimal cost = dataReader.GetDecimal(9);
                        string receptNote = dataReader.GetString(10);
                        string nurseNote = dataReader.GetString(11);
                        string doctorNote = dataReader.GetString(12);

                        PatientAppointment pa = new PatientAppointment(visitId, patientId, apptDate, apptTime, cost, receptNote, nurseNote, doctorNote);
                        appointments.Add(pa);
                    }

                    dataReader.Close();
                    PopulateAppointmentGrid(patients, appointments);
                }
            }
        }

        // Code obtained from https://stackoverflow.com/questions/25352961/have-to-click-away-twice-from-calendar-in-wpf
        /**
         *  When clicking inside the calendar view, you would need to double click outside of it before being able
         *  to click on something outside of it. This code prevents that from happening
         */
        private void ApptCalendar_GotMouseCapture(object sender, MouseEventArgs e)
        {
            UIElement originalElement = e.OriginalSource as UIElement;
            if (originalElement is CalendarDayButton || originalElement is CalendarItem)
            {
                originalElement.ReleaseMouseCapture();
            }
        }

        // Populate the appointment grids with appropriate appointments
        private void PopulateAppointmentGrid(List<Patient> patients, List<PatientAppointment> appointments)
        {
            foreach (PatientAppointment appt in appointments)
            {
                string apptTime = string.Format("{0:h\\:mm}", appt.ApptTime);
                //List<UIElement> apptTimes = GetChildren(AppointmentTimes);

                double day = Convert.ToDouble(appt.ApptDate.DayOfWeek.ToString("d"));

                int diff = apptTime.CompareTo("12:00");

                _ = diff > 0 ? apptTime = string.Format("{0:h\\:mm} PM", appt.ApptTime.Subtract(TimeSpan.FromHours(12))) : null;
                _ = diff == 0 ? apptTime += " PM" : null;
                _ = diff < 0 ? apptTime += " AM" : null;

                foreach (Label child in AppointmentTimes.Children)
                {
                    if (apptTime.CompareTo(child.Content.ToString()) == 0)
                    {
                        Label apptLabel = GetChild(AppointmentGrids, Grid.GetRow(child), (int)day - 1) as Label;

                        // Grab the current appt index to be able to get the patient at the same index
                        int index = appointments.IndexOf(appt);

                        string firstName = patients.ElementAt(index).FirstName;
                        string lastInitial = patients.ElementAt(index).LastName;
                        string visitId = appt.VisitId.ToString();

                        apptLabel.Content = String.Format("{0} {1}.\nVisit Id: {2}", firstName, lastInitial.Substring(0,1), visitId);
                        apptLabel.Background = Brushes.LightGreen;
                    }
                }
            }
        }

        // Clear the appointment grids (Used when changing week view)
        private void ClearAppointmentGrid()
        {
            foreach (Label child in AppointmentGrids.Children)
            {
                child.Background = Brushes.White;
                child.Content = String.Empty;
            }
        }

        // Get individual child from UIElement
        private static UIElement GetChild(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            return null;
        }

        // Highlight the selected day on the appointments calendar
        private static void HighlightCalendarDay(Grid grid, int row, int column)
        {
            foreach (Label label in grid.Children)
            {
                Boolean labelMatch = Grid.GetRow(label) == row && Grid.GetColumn(label) == column;
                _ = labelMatch ? label.Background = Brushes.CornflowerBlue : label.Background = Brushes.LightCyan;
            }
        }

        // Highlight the selected cell on the appointments calendar     
        private static void HighlightCalendarCell(Grid grid, int row, int column)
        {
            foreach (Label child in grid.Children)
            {
                Boolean childMatch = Grid.GetRow(child) == row && Grid.GetColumn(child) == column;
                _ = childMatch ? child.Margin = new Thickness(2) : child.Margin = new Thickness(0.5);
            }
        }

        // Called when a cell on the appointments calendar is selected
        private void ApptDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
        private void ApptDate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (newApptWindow != null)
            {
                newApptWindow.Close();
            }

            Label srcLabel = e.Source as Label;

            OpenAppointmentView(srcLabel);
        }

        private void ViewApptButton_Click(object sender, RoutedEventArgs e)
        {
            if (newApptWindow != null)
            {
                newApptWindow.Close();
            }

            Label srcLabel = null;
            Thickness thc = new Thickness(2);

            foreach (Label child in AppointmentGrids.Children)
            {
                _ = child.Margin.Equals(thc) ? srcLabel = child : null;
            }

            OpenAppointmentView(srcLabel);
        }

        private void OpenAppointmentView(Label srcLabel)
        {
            Label timeLabel = GetChild(AppointmentTimes, Grid.GetRow(srcLabel), 0) as Label;
            DateTime date = weekDate.AddDays(Grid.GetColumn(srcLabel) + 1);

            if (srcLabel.Content.ToString() != String.Empty)
            {
                int patientIndex = 0;
                int visitId = Convert.ToInt32(string.Join("", srcLabel.Content.ToString().ToCharArray().Where(Char.IsDigit)));

                foreach (PatientAppointment pa in appointments)
                {
                    if (pa.VisitId == visitId)
                    {
                        patientIndex = appointments.IndexOf(pa);
                        break;
                    }
                }

                Patient pat = patients.ElementAt(patientIndex);
                PatientAppointment appt = appointments.ElementAt(patientIndex);

                string firstName = pat.FirstName;
                string lastName = pat.LastName;
                string notes = appt.ReceptNote;

                newApptWindow = new NewAppointmentWindow(firstName, lastName, notes, srcLabel, timeLabel, date);
                newApptWindow.Show();
            }
            else
            {
                newApptWindow = new NewAppointmentWindow(srcLabel, timeLabel, date);
                newApptWindow.Show();
            }
        }
    }
}