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
using System.Windows.Media.Effects; 

namespace WpfApp2
{
    
    public partial class MedicineRequest : Window
    {
        public string username = MainWindow.Username;
        Users user = new Users();
        String SQL = "";
        int userId = 0;
        public MedicineRequest()
        {
            InitializeComponent();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == "Search medicines..." || string.IsNullOrWhiteSpace(txtSearch.Text))
                return;

            string SQL = "SELECT * FROM medicine_info WHERE medicine_name LIKE '%" + txtSearch.Text + "%' OR generic_name LIKE '%" + txtSearch.Text + "%'";
            displayMedicine(SQL);
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
            UserMedicine medicine = new UserMedicine();
            DataTable dt = new DataTable();
            dt = user.displayRecords(querry);
            StackPanelMedicines.Children.Clear();
            int n = dt.Rows.Count;

            

            for (int i = 0; i < n; i++)
            {
                String medicineId = dt.Rows[i][0].ToString();
                String medicineName = dt.Rows[i][1].ToString();
                String dosage = dt.Rows[i][2].ToString();
                String genericName = dt.Rows[i][3].ToString();
                String description = dt.Rows[i][4].ToString();
                DataTable quantity = user.displayRecords("select amount from medicineinventory where medicine_id = '" + medicineId + "'");
                int quant = Convert.ToInt32(quantity.Rows[0][0]);

                Border cardBorder = medicine.medicineOrderPanels(medicineId, medicineName, dosage, genericName, description, quant);
                StackPanelMedicines.Children.Add(cardBorder);
            }
        }
        

        private void RequestMedicine_Click(string medicineId, string medicineName, string dose, string genericName)
        {
            MedicineRequestConfirmation requestConfirm = new MedicineRequestConfirmation(dose, medicineName, genericName);
            requestConfirm.Show();
        }

        public void RequestMedicine()
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user.dbConnection();
            displayMedicine("Select * from medicine_info");

            DataTable dt = new DataTable();
            dt = user.displayRecords($"select user_id from users where username = '{username}'");
            userId = Convert.ToInt32(dt.Rows[0][0].ToString());
            displayMedicineRequest("SELECT * FROM medicinerequests WHERE user_id = " + userId);
        }
   
        public void displayMedicineRequest(String querry)
        {
            UserMedicine medicine = new UserMedicine();
            DataTable dt = new DataTable();
            dt = user.displayRecords(querry);

            StackPanelMedicineRequests.Children.Clear();

            int n = dt.Rows.Count;
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230)); // Background for icon

            for (int i = 0; i < n; i++)
            {

                String medicineName = dt.Rows[i][2].ToString();
                String reason = dt.Rows[i][3].ToString();
                String quantity = dt.Rows[i][4].ToString();
                String status = dt.Rows[i][5].ToString();
                String requestedAt = dt.Rows[i][6].ToString();
                String approvedAt = dt.Rows[i][7].ToString();
                String rejectReason = dt.Rows[i][8].ToString();

                Border cardBorder = medicine.medicineRequestPanels(medicineName, reason, quantity, status, requestedAt, approvedAt, rejectReason);

                StackPanelMedicineRequests.Children.Add(cardBorder);
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm(username); 
            this.Close();
            userForm.Show();
        }
    }
} 
