using System.ComponentModel;
using System.Windows;

namespace Project_2_EMS {
  /// <summary>
  ///   Interaction logic for NurseView.xaml
  /// </summary>
  public partial class NurseView {
    private readonly Window _parentWindow;

    public NurseView(Window parentWindow) {
      _parentWindow = parentWindow;
      InitializeComponent();
      Closing += OnWindowClosing;
    }

    private void LogOutButton_Click(object sender, RoutedEventArgs e) {
      Hide();
      var mainWindow = _parentWindow;
      mainWindow.Show();
    }

    private void OnWindowClosing(object sender, CancelEventArgs e) {
      var mainWindow = _parentWindow;
      mainWindow.Close();
      Close();
    }
  }
}