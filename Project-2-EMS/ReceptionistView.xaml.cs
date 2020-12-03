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
        SqlConnection connection;
        private DateTime prevDate;
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
            List<UIElement> views = GetChildren(ViewPanel);
            Button btn = e.Source as Button;
            foreach (Grid grid in views)
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

                if (prevDate != date)
                {
                    prevDate = date;
                    var apptDays = GetChildren(AppointmentDays);
                    HighlightDay(apptDays, 0, (int)dayNum + 1);
                }

                if (prevWeekDate != weekDate)
                {
                    prevWeekDate = weekDate;
                    ClearAppointmentGrid();

                    List<PatientAppointment> appointments = GetPatientAppointments(date);

                    foreach (PatientAppointment appt in appointments)
                    {
                        string apptTime = string.Format("{0:h\\:mm}", appt.ApptTime);
                        List<UIElement> apptTimes = GetChildren(AppointmentTimes);
                        int diff = apptTime.CompareTo("12:00");

                        _ = diff > 0 ? apptTime = string.Format("{0:h\\:mm} PM", appt.ApptTime.Subtract(TimeSpan.FromHours(12))) : null;
                        _ = diff == 0 ? apptTime += " PM" : null;
                        _ = diff < 0 ? apptTime += " AM" : null;

                        MessageBox.Show(apptTime);

                        foreach (Label child in apptTimes)
                        {
                            if (apptTime.CompareTo(child.Content.ToString()) == 0)
                            {
                                Label apptLabel = GetChild(AppointmentGrids, Grid.GetRow(child), (int)dayNum - 1) as Label;
                                apptLabel.Background = Brushes.LightGreen;
                            }
                        }
                    }

                    /**
                    if (patAppts.Count > 0)
                        MessageBox.Show(patAppts.Count.ToString());
                    if (patAppts.Count > 0)
                    {
                        MessageBox.Show(string.Format("{0:h\\:mm}", patAppts.ElementAt(1).ApptTime.Subtract(TimeSpan.FromHours(12))));
                    }
                    */
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

        private List<PatientAppointment> GetPatientAppointments(DateTime date)
        {
            List<PatientAppointment> appointments = new List<PatientAppointment>();

            ReceptionSqlHandler rcsql = new ReceptionSqlHandler();
            string query = rcsql.AppointmentQuerier(date);

            SqlCommand cmd = new SqlCommand { Connection = connection, CommandText = query };
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                PatientAppointment pa;

                int visitId = dataReader.GetInt32(0);
                int patientId = dataReader.GetInt32(1);
                DateTime apptDate = dataReader.GetDateTime(2);
                TimeSpan apptTime = dataReader.GetTimeSpan(3);
                decimal cost = dataReader.GetDecimal(4);
                string receptNote = dataReader.GetString(5);
                string nurseNote = dataReader.GetString(6);
                string doctorNote = dataReader.GetString(7);

                pa = new PatientAppointment(visitId, patientId, apptDate, apptTime, cost, receptNote, nurseNote, doctorNote);
                appointments.Add(pa);
            }

            dataReader.Close();
            return appointments;
        }

        private void ClearAppointmentGrid()
        {
            foreach (Label child in AppointmentGrids.Children)
            {
                child.Background = Brushes.White;
                child.Content = String.Empty;
            }
        }

        // Return a list of the given grid's children
        private static List<UIElement> GetChildren(Grid grid)
        {
            List<UIElement> children = new List<UIElement>();
            foreach (UIElement child in grid.Children)
            {
                children.Add(child);
            }
            return children;
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
        private static void HighlightDay(List<UIElement> days, int row, int column)
        {
            foreach (Label l in days)
            {
                if (Grid.GetRow(l) == row && Grid.GetColumn(l) == column)
                {
                    var bc = new BrushConverter();
                    l.Background = (Brush)bc.ConvertFrom("#FF6BBBBD");
                }
                else
                {
                    l.Background = Brushes.LightCyan;
                }
            }
        }

        // Highlight the selected cell on the appointments calendar     
        private static void HighlightAppointment(Grid grid, int row, int column)
        {
            foreach (Label child in grid.Children)
            {
                _ = Grid.GetRow(child) == row && Grid.GetColumn(child) == column ? child.Margin = new Thickness(2) : child.Margin = new Thickness(0.5);
            }
        }

        // Called when a cell on the appointments calendar is selected
        private void ApptDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Highlight the selected cell
            Label srcLabel = e.Source as Label;
            HighlightAppointment(AppointmentGrids, Grid.GetRow(srcLabel), Grid.GetColumn(srcLabel));

            // Highlight the day corresponding to the selected cell
            List<UIElement> apptDays = GetChildren(AppointmentDays);
            HighlightDay(apptDays, 0, Grid.GetColumn(srcLabel) + 2);

            // Show the selected date on the calendar view
            DateTime date = weekDate.AddDays(Grid.GetColumn(srcLabel) + 1);
            ApptCalendar.SelectedDate = date;

            // Show a new/view appointment button whenver a cell is selected
            ApptButtonGrid.Visibility = Visibility.Visible;
            _ = srcLabel.Content.ToString() == String.Empty ? ViewApptButton.Content = "New Appointment" : ViewApptButton.Content = "View Appointment";
        }

        // Called when a cell on the appointments calendar has been double clicked
        private void ApptDate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (newApptWindow != null) newApptWindow.Close();

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

            //ToString("ddd dd, yyyy")
            newApptWindow = new NewAppointmentWindow(srcLabel, timeLabel, date);
            newApptWindow.Show();
        }
    }
}