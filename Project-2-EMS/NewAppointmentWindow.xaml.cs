﻿using System;
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
        }

        private void ExistingPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            InitialPage.Visibility = Visibility.Hidden;
            ExistingPatientPage.Visibility = Visibility.Visible;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((e.Source as Button).Name.Equals("NewPatientCancel"))
            {
                ClearChildren(NewPatientPage);

                NewPatientPage.Visibility = Visibility.Hidden;
                InitialPage.Visibility = Visibility.Visible;
            }
            else if ((e.Source as Button).Name.Equals("ExistingPatientCancel"))
            {
                ClearChildren(ExistingPatientPage);

                ExistingPatientPage.Visibility = Visibility.Hidden;
                InitialPage.Visibility = Visibility.Visible;
            }
        }

        private void ConfirmNewPatientBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}