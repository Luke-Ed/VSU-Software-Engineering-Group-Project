﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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

namespace Project_2_EMS {
  /// <summary>
  /// Interaction logic for ReceptionistView.xaml
  /// </summary>
  public partial class ReceptionistView {
    private readonly Window _parentWindow;
    public ReceptionistView(Window parentWindow) {
      _parentWindow = parentWindow;
      InitializeComponent();
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
  }
}
