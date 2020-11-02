using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
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

namespace Project_2_EMS {
  /// <summary>
  /// Interaction logic for NurseView.xaml
  /// </summary>
  public partial class NurseView {
    private readonly Window _parentWindow;

    public NurseView(Window parentWindow) {
      _parentWindow = parentWindow;
      InitializeComponent();
      Closing += OnWindowClosing;
    }

    private void LogOutButton_Click(object sender, RoutedEventArgs e) {
      Window mainWindow = _parentWindow;
      mainWindow.Show();
      Close();
    }

    private void OnWindowClosing(object sender, CancelEventArgs e) {
      Window mainWindow = _parentWindow;
      mainWindow.Close();
    }
  }
}
