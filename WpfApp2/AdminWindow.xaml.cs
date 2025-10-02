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
    public partial class AdminWindow : Window

    {
        StudentManagement studentManagement = new StudentManagement();

        public AdminWindow()

        {

            InitializeComponent();

        }



        private void Button_Click(object sender, RoutedEventArgs e)

        {



        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //studentManagement.Show();
            studentManagement.Show();
            this.Hide();
        }
    }

}