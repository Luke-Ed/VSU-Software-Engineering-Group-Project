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

        private Boolean CheckIfTextEmpty(Grid grid)
        {
            Boolean isEmpty = false;
            foreach (UIElement child in grid.Children)
            {
                if (child as TextBox != null)
                {
                    if ((child as TextBox).Text == String.Empty)
                    {
                        isEmpty = true;
                    }
                }
                else if (child as ComboBox != null)
                {
                    if ((child as ComboBox).Text == String.Empty)
                    {
                        isEmpty = true;
                    }
                }
                else if (child as DataGrid != null)
                {
                    if ((child as DataGrid).Items.Count == 0)
                    {
                        isEmpty = true;
                    }
                }
            }
            return isEmpty;
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
            Boolean isEmpty = CheckIfTextEmpty(prevPage);

            if (isEmpty == false)
            {
                prevPage.Visibility = Visibility.Hidden;
                NewAppointmentPage.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("All fields must be filled in before proceeding.");
            }
}

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NewAppointmentPage.Visibility = Visibility.Hidden;
            prevPage.Visibility = Visibility.Visible;
        }
    }
}