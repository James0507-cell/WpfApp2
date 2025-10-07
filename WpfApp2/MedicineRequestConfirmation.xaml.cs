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
    /// Interaction logic for MedicineRequestConfirmation.xaml
    /// </summary>
    public partial class MedicineRequestConfirmation : Window
    {
        String dose = "";
        String medicineName = "";
        String genericName = "";
        String SQL = "";
        String username = MainWindow.Username;
        int userId =0;
        Users user = new Users();
        public MedicineRequestConfirmation(string dose, string medicineName, string genericName)
        {
            InitializeComponent();
            this.dose = dose;
            this.medicineName = medicineName;
            this.genericName = genericName;
        }

        private void btnConfirmRequest_Click(object sender, RoutedEventArgs e)
        {
            SQL = $"INSERT INTO `medicinerequests` (`user_id`, `medicine_name`, `reason`, `quantity`, `status`) " +
                  $"VALUES ('{userId}', '{medicineName}', '{txtPurpose.Text}', '{txtQuantity.Text}', 'Pending')";
            user.sqlManager(SQL);
            MessageBox.Show("Request is send!");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user.dbConnection();
            lblDose.Text = dose;
            lblGenericName.Text = genericName;
            lblMedicineName.Text = medicineName;

            DataTable dt = user.displayRecords($"SELECT * FROM users WHERE username = '{username}'");
            userId = Convert.ToInt32(dt.Rows[0]["user_id"].ToString());


        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
