﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_2_EMS {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
    }
    //String connectionString = ConfigurationManager.ConnectionStrings["MDR_ConnStr"].ConnectionString;
    //SqlConnection connection = new SqlConnection(connectionString);
    //connection.Open();
    private void NurseButton_Click(object sender, RoutedEventArgs e) {
      Window nurseWindow = new NurseView();
      nurseWindow.Show();
      Close();
    }

    private void PatientButton_Click(object sender, RoutedEventArgs e) {
    }

    private void ReceptionButton_Click(object sender, RoutedEventArgs e) {
        Window receptionWindow = new ReceptionistView();
        receptionWindow.Show();
        Close();
    }
  }
}
