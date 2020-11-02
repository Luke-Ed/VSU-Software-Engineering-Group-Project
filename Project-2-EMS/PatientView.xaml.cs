using System;
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
using System.Windows.Shapes;

namespace Project_2_EMS{
  /// <summary>
  /// Interaction logic for PatientView.xaml
  /// </summary>

  public partial class PatientView : Window{
    private readonly Window _parentWindow;
    public PatientView(Window parentWindow){
      _parentWindow = parentWindow;
      InitializeComponent();

    }

    private void ViewApptButton_Click(object sender, RoutedEventArgs e){
      if (!AppointmentsGrid.IsVisible)
      {
        AppointmentsGrid.Visibility = Visibility.Visible;
        PrescriptionsGrid.Visibility = Visibility.Hidden;
        WelcomeGrid.Visibility = Visibility.Hidden;
      }
    }

    private void ViewPrescButton_Click(object sender, RoutedEventArgs e){
      if (!PrescriptionsGrid.IsVisible){
        List<Prescription> items = new List<Prescription>();
        items.Add(new Prescription() { Id = 20, Name = "Carbon Capsule", Dosage = "1 pill weekly.", Refills = 2 });
        items.Add(new Prescription() { Id = 42, Name = "Krypton", Dosage = "2 pills daily, preferably with a meal.", Refills = 0 });
        items.Add(new Prescription() { Id = 77, Name = "Unobtanium", Dosage = "1 pill daily.", Refills = 1});

        PrescriptionLB.ItemsSource = items;

        AppointmentsGrid.Visibility = Visibility.Hidden;
        PrescriptionsGrid.Visibility = Visibility.Visible;
        WelcomeGrid.Visibility = Visibility.Hidden;
      }
    }

    public class Prescription{
      public int Id { get; set; }
      public string Name { get; set; }
      public String Dosage { get; set; }
      public int Refills { get; set; }
      public override string ToString()
      {
        return this.Name + " --- Dosage = " + this.Dosage + " --- Refills Remaining = " + this.Refills;
      }

    }

    private void LogOutButton_Click(object sender, RoutedEventArgs e){
      Window mainWindow = _parentWindow;
      mainWindow.Show();
      Close();
    }
  }
}
