using System;
using System.Collections.Generic;
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
      TextBlock outputTextBlock = OutputTextBlock;
      String dbFile = Environment.CurrentDirectory + "\\AppData\\EMR_DB.mdf";
      
      SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
        IntegratedSecurity = true,
        AttachDBFilename = @dbFile,
        //DataSource = (LocalDB)\\
      };
      String connectionString = sqlConnectionString.ToString();
      SqlConnection connection = new SqlConnection(connectionString);
      connection.Open();
    }
  }
}
