using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for NewAppointmentWindow.xaml
    /// </summary>
    public partial class NewAppointmentWindow : Window
    {
        private DateTime apptDate;
        private Label apptTime;
        private Grid prevPage;

        public NewAppointmentWindow(Label srcLabel, Label timeLabel, DateTime date)
        {
            InitializeComponent();
            ApptDate.Content = String.Format("{0} | {1}", date.ToString("ddd dd, yyyy"), timeLabel.Content);

            apptDate = date;
            apptTime = timeLabel;
        }

        private static void ClearChildren(Grid grid)
        {
            foreach (UIElement child in grid.Children)
            {
                if (child as TextBox != null)
                {
                    (child as TextBox).Text = String.Empty;
                }
                else if (child as ComboBox != null)
                {
                    (child as ComboBox).Text = String.Empty;
                }
            }
        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            InitialPage.Visibility = Visibility.Hidden;
            NewPatientPage.Visibility = Visibility.Visible;
            prevPage = NewPatientPage;
        }

        private void ExistingPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            InitialPage.Visibility = Visibility.Hidden;
            ExistingPatientPage.Visibility = Visibility.Visible;
            prevPage = ExistingPatientPage;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearChildren(prevPage);

            prevPage.Visibility = Visibility.Hidden;
            InitialPage.Visibility = Visibility.Visible;
        }

        private void ContinueNewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            prevPage.Visibility = Visibility.Hidden;
            NewAppointmentPage.Visibility = Visibility.Visible;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NewAppointmentPage.Visibility = Visibility.Hidden;
            prevPage.Visibility = Visibility.Visible;
        }
    }
}