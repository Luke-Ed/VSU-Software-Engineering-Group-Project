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
    }
}