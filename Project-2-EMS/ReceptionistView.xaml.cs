﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    public partial class ReceptionistView : Window {
        private readonly Window _parentWindow;
        private Window newApptWindow;
        private DateTime weekDate;
        private DateTime prevDate;

      public ReceptionistView(Window parentWindow) {
        _parentWindow = parentWindow;
        InitializeComponent();

        weekDate = DateTime.Now.AddDays(Convert.ToDouble(DateTime.Now.DayOfWeek.ToString("d")) * -1.0);
        AppointmentWeek.Content = weekDate.ToString("Week o\\f MMMM dd, yyyy");

        Closing += OnWindowClosing;
      }

      private void LogOutButton_Click(object sender, RoutedEventArgs e) {
        Hide();

        if (newApptWindow != null) 
        {
            newApptWindow.Close();
        }

        Window mainWindow = _parentWindow;
        mainWindow.Show();
        }
      
      private void OnWindowClosing(object sender, CancelEventArgs e) {
        Window mainWindow = _parentWindow;

        if (newApptWindow != null)
        {
            newApptWindow.Close();
        }

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

        private void ApptCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ApptCalendar.SelectedDate.HasValue)
            {
                DateTime date = ApptCalendar.SelectedDate.Value;
                Double dayNum = Convert.ToDouble(ApptCalendar.SelectedDate.Value.DayOfWeek.ToString("d"));

                weekDate = date.AddDays(dayNum * -1.0);
                AppointmentWeek.Content = weekDate.ToString("Week o\\f MMMM dd, yyyy");

                if (prevDate != date) {
                    prevDate = date;
                    int row = 0;

                    foreach (Label l in AppointmentGrids.Children) {
                        if (Grid.GetRow(l) == row && Grid.GetColumn(l) == (int)dayNum - 1) {
                            if (l.Background == Brushes.White)
                            {
                                l.Background = Brushes.LightGray;
                            }
                            row += 1;
                        }
                        else if (l.Background == Brushes.LightGray) {
                            l.Background = Brushes.White;
                        }
                    }
                }
            }
        }

        private void ApptCalendar_GotMouseCapture(object sender, MouseEventArgs e) {
            // Code obtained from https://stackoverflow.com/questions/25352961/have-to-click-away-twice-from-calendar-in-wpf
            // to prevent needing to double click outside of calendar after clicking inside of it
            UIElement originalElement = e.OriginalSource as UIElement;
            if (originalElement is CalendarDayButton || originalElement is CalendarItem)
            {
                originalElement.ReleaseMouseCapture();
            }
        }

        #endregion

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //var str = e.Source as Label;
            //str.Background = Brushes.White;

            //int row = Grid.GetRow(str);
            //int col = Grid.GetColumn(str);

            //MessageBox.Show(GetChildren(SundayColumn, row, col).ToString());
            
            //str.Content = Grid.GetColumn(str);
        }

        private static Label GetChild(Grid grid, int row, int column)
        {
            foreach (Label child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            return null;
        }

        private void ApptDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (newApptWindow != null)
            {
                newApptWindow.Close();
            }
            
            Label srcLabel = e.Source as Label;
            Label timeLabel = GetChild(AppointmentTimes, Grid.GetRow(srcLabel), 0) as Label;
            DateTime date = weekDate.AddDays(Grid.GetColumn(srcLabel) + 1);

            //ToString("ddd dd, yyyy")
            newApptWindow = new NewAppointmentWindow(srcLabel, timeLabel, date);
            newApptWindow.Show();
        }
    }
}
