using System;
using System.Collections;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YourNamespace;

namespace WpfApp2
{
    public partial class AdminWindow : Window
    {
        dbManager dbManager = new dbManager();  
        private String SQL = "";
        Admin admin = new Admin();
        private String username = MainWindow.Username;
        StudentManagement studentManagement = new StudentManagement();
        AdminOverview adminOverview = new AdminOverview();
        AdminAppointment adminAppointment = new AdminAppointment();
        AdminMedicine adminMedicine = new AdminMedicine();
        AdminInventory adminInventory = new AdminInventory();


        public AdminWindow()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            studentManagement.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            int totalStudents = adminOverview.GetActiveStudentCount();
            lblActiveStatus.Content = totalStudents;

            int totalmedicinereq = adminOverview.GetMedicineStatusCount();
            lblMedicine.Content = totalmedicinereq;

            int totalAppoinment = adminOverview.GetAppointmenCount();
            lblPending.Content = totalAppoinment;

            int totalLowStock = adminOverview.getMedicineCount();
            lblLowStack.Content = totalLowStock;

           

            displayAppointments("SELECT * FROM appointments");
            displayMedicineRequest("SELECT * FROM medicinerequests");
            displayMedicineInv("select * from medicine_info");
            displayActivity("SELECT * FROM admin_activity_log ORDER BY activity_date DESC");
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedStatus = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedStatus == "All")
            {
                displayAppointments("SELECT * FROM appointments");
            }
            else
            {
                displayAppointments($"SELECT * FROM appointments WHERE status = '{selectedStatus}'");
            }
        }

        public void displayAppointments(String querry)
        {

            DataTable dt = new DataTable();

            dt = dbManager.displayRecords(querry);

            int n = dt.Rows.Count;
            AppointmentStackPanel.Children.Clear();


            for (int i = 0; i < n; i++)
            {
                String appointmentID = dt.Rows[i][0].ToString();
                String patientID = dt.Rows[i][1].ToString();
                String username = dt.Rows[i][2].ToString();
                String studentId = dt.Rows[i][3].ToString();
                String date = dt.Rows[i][4].ToString();
                String time = dt.Rows[i][5].ToString();
                String email = dt.Rows[i][6].ToString();
                String phone = dt.Rows[i][7].ToString();
                String purpose = dt.Rows[i][8].ToString();
                String allergies = dt.Rows[i][9].ToString();
                String medication = dt.Rows[i][10].ToString();
                String previousVisit = dt.Rows[i][11].ToString();
                String ecn = dt.Rows[i][12].ToString();
                String ecp = dt.Rows[i][13].ToString();
                String status = dt.Rows[i][14].ToString();
                String symptoms = dt.Rows[i][16].ToString();
                String handledTime = dt.Rows[i]["handled_time"].ToString();

                Border cardBorder = adminAppointment.AppointmentPanel(appointmentID, patientID, username, studentId, date, time, email, phone, purpose, allergies, medication, previousVisit, ecn, ecp, status, symptoms, handledTime);

                AppointmentStackPanel.Children.Add(cardBorder);
            }
        }

        public void displayMedicineRequest(String strquerry)
        {
            StackPanel targetStackPanel = this.StackPanelMedicineReuqests;
            DataTable dt = new DataTable();
            dt = dbManager.displayRecords(strquerry);
            int n = dt.Rows.Count;
            targetStackPanel.Children.Clear();

            for (int i = 0; i < n; i++)
            {
                // Data from DataTable
                String requestID = dt.Rows[i][0].ToString();
                String userID = dt.Rows[i][1].ToString();
                String medicineName = dt.Rows[i][2].ToString();
                String reason = dt.Rows[i][3].ToString();
                String quantity = dt.Rows[i][4].ToString();
                String status = dt.Rows[i][5].ToString();
                String requestDate = dt.Rows[i][6].ToString();
                String approvedDate = dt.Rows[i][7].ToString();

                
               DataTable inventoryid = dbManager.displayRecords(
                    "SELECT inventory_id FROM medicineinventory WHERE medicine_name = '" + medicineName + "'");
                int inventoryID = Convert.ToInt32(inventoryid.Rows[0][0]);

                Border cardBorder = adminMedicine.medicineRequestPanel(requestID, userID, medicineName, reason, quantity, status, requestDate, approvedDate, inventoryID);
                
                targetStackPanel.Children.Add(cardBorder);
            }
        }
        
        public void displayMedicineInv(String query)
        {
            DataTable dt = new DataTable();
            dt = dbManager.displayRecords(query);

            wrapPanelInventory.Children.Clear();
            int n = dt.Rows.Count;

            for (int i = 0; i < n; i++)
            {
                String medicineId = dt.Rows[i][0].ToString();
                String medicineName = dt.Rows[i][1].ToString();
                String dosage = dt.Rows[i][2].ToString();
                String genericName = dt.Rows[i][3].ToString();
                String description = dt.Rows[i][4].ToString();

                DataTable quantityDt = dbManager.displayRecords(
                    "SELECT amount, inventory_id FROM medicineinventory WHERE medicine_id = '" + medicineId + "'");
                int quant = Convert.ToInt32(quantityDt.Rows[0][0]);
                String inventoryId = quantityDt.Rows[0][1].ToString();
                
                Border cardBorder = adminInventory.inventoryPanel(medicineId, medicineName, dosage, genericName, description, quant, inventoryId);
                wrapPanelInventory.Children.Add(cardBorder);
            }
        }

        public void displayActivity(String SQL)
        {
            StackPanelActivities.Children.Clear();
            DataTable dt = dbManager.displayRecords(SQL);

            StackPanelActivities.Children.Clear();

            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray);
            Brush lightBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xF7, 0xFA));
            Brush idTagBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0F0F0"));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string activityID = dt.Rows[i]["activity_id"].ToString();
                string username = dt.Rows[i]["username"].ToString();
                string type = dt.Rows[i]["activity_type"].ToString();
                string description = dt.Rows[i]["activity_desc"].ToString();
                string dateTime = dt.Rows[i]["activity_date"].ToString();
                String id = dt.Rows[i]["admin_id"].ToString();

                Border cardBorder = adminOverview.activityPanel(activityID,username, type, description, dateTime, id);
                StackPanelActivities.Children.Add(cardBorder);
            }
        }

        private void TabControl_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            int totalStudents = adminOverview.GetActiveStudentCount();
            lblActiveStatus.Content = totalStudents;

            int totalmedicinereq = adminOverview.GetMedicineStatusCount();
            lblMedicine.Content = totalmedicinereq;

            int totalAppoinment = adminOverview.GetAppointmenCount();
            lblPending.Content = totalAppoinment;

            int totalLowStock = adminOverview.getMedicineCount();
            lblLowStack.Content = totalLowStock;

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (wrapPanelInventory == null)
                return; 
            if (txtSearch.Text == "Search Inventory ID..." || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                displayMedicineInv("SELECT * FROM medicine_info");
            }
            else
            {
                string SQL = "SELECT mi.* FROM medicine_info mi " +
                             "JOIN medicineinventory miv ON mi.medicine_id = miv.medicine_id " +
                             "WHERE miv.inventory_id LIKE '%" + txtSearch.Text + "%'";
                displayMedicineInv(SQL);
            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search Inventory ID...")
                txtSearch.Text = "";
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                txtSearch.Text = "Search Inventory ID...";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbFilterInv.SelectedIndex == null)
            {
                displayMedicineInv("select * from medicine_info");

            }
            else if (cmbFilterInv.SelectedIndex == 0)
            {
                displayMedicineInv("SELECT mi.* FROM medicine_info mi " +
                                   "JOIN medicineinventory miv ON mi.medicine_id = miv.medicine_id " +
                                   "WHERE miv.amount < 20");
            }
            else if (cmbFilterInv.SelectedIndex == 1)
            {
                displayMedicineInv("SELECT mi.* FROM medicine_info mi " +
                                   "JOIN medicineinventory miv ON mi.medicine_id = miv.medicine_id " +
                                   "WHERE miv.amount >= 20");
            }
            else
            {
                displayMedicineInv("select * from medicine_info");
            }
        }

        private void txtSearchIDapp_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchIDapp.Text == "Search appointment ID...")
                txtSearchIDapp.Text = "";
        }

        private void txtSearchIDapp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchIDapp.Text))
                txtSearchIDapp.Text = "Search appointment ID...";
        }

        private void txtSearchIDapp_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (AppointmentStackPanel == null)
                return; 

            if (txtSearchIDapp.Text == "Search appointment ID..." || string.IsNullOrWhiteSpace(txtSearchIDapp.Text))
            {
                displayAppointments("SELECT * FROM appointments");
            }
            else
            {
                displayAppointments($"SELECT * FROM appointments WHERE appointment_id LIKE '%{txtSearchIDapp.Text}%'");
            }
        }

        private void txtSearchMedID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchMedID.Text == "Search medicine request ID...")
                txtSearchMedID.Text = "";
        }

        private void txtSearchMedID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchMedID.Text))
                txtSearchMedID.Text = "Search medicine request ID...";
        }

        private void txtSearchMedID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StackPanelMedicineReuqests == null)
                return; 
            if (txtSearchMedID.Text == "Search medicine request ID..." || string.IsNullOrWhiteSpace(txtSearchMedID.Text))
            {
                displayMedicineRequest("SELECT * FROM medicinerequests");
            }
            else
            {
                displayMedicineRequest($"SELECT * FROM medicinerequests WHERE request_id LIKE '%{txtSearchMedID.Text}%'");
            }
        }

        private void cboMedFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedStatus = (cboMedFilter.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(selectedStatus) || selectedStatus == "All")
            {
                displayMedicineRequest("SELECT * FROM medicinerequests");
            }
            else
            {
                displayMedicineRequest($"SELECT * FROM medicinerequests WHERE status = '{selectedStatus}'");
            }
        }

        private void txtSearchActivity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StackPanelActivities == null)
                return; 
            if (txtSearchActivity.Text == "Search activity ID..." || string.IsNullOrWhiteSpace(txtSearchActivity.Text))
            {
                displayActivity("SELECT * FROM admin_activity_log ORDER BY activity_date DESC");
            }
            else
            {
                displayActivity($"SELECT * FROM admin_activity_log where activity_id like '%{txtSearchActivity.Text}%'");
            }
        }

        private void txtSearchActivity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchActivity.Text))
                txtSearchActivity.Text = "Search activity ID...";

        }

        private void txtSearchActivity_GotFocus(object sender, RoutedEventArgs e)
        {

            if (txtSearchActivity.Text == "Search activity ID...")
                txtSearchActivity.Text = "";
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string selectedType = (cboFilterActivity.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedType == "All")
            {
                displayActivity("SELECT * FROM admin_activity_log ORDER BY activity_date DESC");
            }
            else
            {
                displayActivity($"SELECT * FROM admin_activity_log WHERE activity_type = '{selectedType}' ORDER BY activity_date DESC");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddNewMedicine newMedicine = new AddNewMedicine();
            newMedicine.Show();
            
        }
    }
}

