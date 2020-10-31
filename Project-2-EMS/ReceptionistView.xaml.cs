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
using System.Windows.Shapes;

namespace Project_2_EMS
{
    /// <summary>
    /// Interaction logic for ReceptionistView.xaml
    /// </summary>
    public partial class ReceptionistView : Window {
        public ReceptionistView() {
            InitializeComponent();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            Window login = new MainWindow();
            login.Show();
            Close();
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

        private void ApptCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            if (ApptCalendar.SelectedDate.HasValue) {
                AppointmentDate.Content = ApptCalendar.SelectedDate.Value.ToString("MM/dd/yyyy");
            }
        }
    }
}
