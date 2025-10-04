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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        ShortCheck shortcheck = new ShortCheck();
        public UserForm()
        {
            InitializeComponent();
        }

        private void dfs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            shortcheck.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            BookingAppointment bookingAppointment = new BookingAppointment();
            bookingAppointment.Show();
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
