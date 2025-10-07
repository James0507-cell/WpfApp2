using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for MedicineRequest.xaml
    /// </summary>
    public partial class MedicineRequest : Window
    {
        public string username = MainWindow.Username;
        Users user = new Users();
        public MedicineRequest()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search medicines...")
                txtSearch.Text = "";
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                txtSearch.Text = "Search medicines...";
        }
        public void displayMedicine(String querry)
        {
            DataTable dt = new DataTable();
            dt = user.displayRecords(querry);
            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++) {
                
                StackPanel medicinePanel = new StackPanel();

            }
    }
}
