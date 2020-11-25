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
        public NewAppointmentWindow(Label srcLabel, Label timeLabel, DateTime date)
        {
            InitializeComponent();
            ApptDate.Content = String.Format("{0} | {1}", date.ToString("ddd dd, yyyy"), timeLabel.Content);
        }

        private static List<UIElement> GetChildren(Grid grid)
        {
            List<UIElement> children = new List<UIElement>();
            foreach (UIElement child in grid.Children)
            {
                children.Add(child);
            }
            return children;
        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            InitialPage.Visibility = Visibility.Hidden;
            NewPatientPage.Visibility = Visibility.Visible;
        }

        private void ExistingPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            InitialPage.Visibility = Visibility.Hidden;
            ExistingPatientPage.Visibility = Visibility.Visible;
        }

        private void CancelNewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Grid child in NewPatientPage.Children) 
            { 
                foreach (UIElement element in child.Children) 
                {
                    if (element as TextBox != null) {
                        (element as TextBox).Text = String.Empty;
                    }
                    else if (element as ComboBox != null)
                    {
                        (element as ComboBox).Text = String.Empty;
                    }
                }
            }

            NewPatientPage.Visibility = Visibility.Hidden;
            InitialPage.Visibility = Visibility.Visible;
        }

        private void ConfirmNewPatientBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}