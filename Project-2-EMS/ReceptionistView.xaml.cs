using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_2_EMS {
    /// <summary>
    /// Interaction logic for ReceptionistView.xaml
    /// </summary>
    public partial class ReceptionistView : Window {
      private readonly Window _parentWindow;
      public ReceptionistView(Window parentWindow) {
        _parentWindow = parentWindow;
        InitializeComponent();
        AppointmentDate.Content = DateTime.Now.ToShortDateString();
        Closing += OnWindowClosing;
      }
      private void LogOutButton_Click(object sender, RoutedEventArgs e) {
        Close();
        Window mainWindow = _parentWindow;
        mainWindow.Show();
      }
      
      private void OnWindowClosing(object sender, CancelEventArgs e) {
        Window mainWindow = _parentWindow;
        mainWindow.Close();
      }

      private void CalendarButton_Click(object sender, RoutedEventArgs e) {
          if (!CalendarPanel.IsVisible) {
              GreetingGrid.Visibility = Visibility.Hidden;
              CalendarPanel.Visibility = Visibility.Visible;
              SigninPanel.Visibility = Visibility.Hidden;
              BillingPanel.Visibility = Visibility.Hidden;
          }
      }

      private void SigninButton_Click(object sender, RoutedEventArgs e) {
          if (!SigninPanel.IsVisible) {
              GreetingGrid.Visibility = Visibility.Hidden;
              CalendarPanel.Visibility = Visibility.Hidden;
              SigninPanel.Visibility = Visibility.Visible;
              BillingPanel.Visibility = Visibility.Hidden;
          }
      }

      private void BillingButton_Click(object sender, RoutedEventArgs e) {
          if (!BillingPanel.IsVisible) {
              GreetingGrid.Visibility = Visibility.Hidden;
              CalendarPanel.Visibility = Visibility.Hidden;
              SigninPanel.Visibility = Visibility.Hidden;
              BillingPanel.Visibility = Visibility.Visible;
          }
      }

        // This region holds the code/event handlers for Calendar view
        #region
        private void ApptCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            if (ApptCalendar.SelectedDate.HasValue) {
                AppointmentDate.Content = ApptCalendar.SelectedDate.Value.ToString("MM/dd/yyyy");
                if (ApptCalendar.SelectedDate.Value.ToString("dddd").Equals("Saturday") || ApptCalendar.SelectedDate.Value.ToString("dddd").Equals("Sunday")) {
                    ApptDateViewer.Visibility = Visibility.Hidden;
                    ApptDateViewerEmpty.Visibility = Visibility.Visible;
                }
                else {
                    ApptDateViewerEmpty.Visibility = Visibility.Hidden;
                    ApptDateViewer.Visibility = Visibility.Visible;
                }
            }
            PatientQuickViewLabel.Content = "No appointment selected";
        }

        private void ApptCalendar_GotMouseCapture(object sender, MouseEventArgs e) {
            // Code obtained from https://stackoverflow.com/questions/25352961/have-to-click-away-twice-from-calendar-in-wpf
            // to prevent mouse click from being stuck in calendar
            UIElement originalElement = e.OriginalSource as UIElement;
            if (originalElement is CalendarDayButton || originalElement is CalendarItem)
            {
                originalElement.ReleaseMouseCapture();
            }
        }

        private void Appt1_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "9:00 appointment available";
        }

        private void Appt2_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "9:30 appointment available";
        }

        private void Appt3_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "10:00 appointment available";
        }

        private void Appt4_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "10:30 appointment available";
        }

        private void Appt5_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "11:00 appointment available";
        }

        private void Appt6_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "11:30 appointment available";
        }

        private void Appt7_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "1:00 appointment available";
        }

        private void Appt8_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "1:30 appointment available";
        }

        private void Appt9_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "2:00 appointment available";
        }

        private void Appt10_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "2:30 appointment available";
        }

        private void Appt11_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "3:00 appointment available";
        }

        private void Appt12_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "3:30 appointment available";
        }

        private void Appt13_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "4:00 appointment available";
        }

        private void Appt14_Click(object sender, RoutedEventArgs e) {
            PatientQuickViewLabel.Content = "4:30 appointment available";
        }

        // Setup a helper method to revert buttons to previous colors if I decide to change it

        #endregion
    }
}
