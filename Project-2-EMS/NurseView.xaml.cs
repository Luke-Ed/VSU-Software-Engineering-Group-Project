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
  public partial class NurseView : Window {
    public NurseView() {
      InitializeComponent();
      Closing += OnWindowClosing;
    }

    private void OnWindowClosing(object sender, CancelEventArgs e) {

    }
  }
}
