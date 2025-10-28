using System;
using System.Data; // Required for DataTable
using System.Windows;
// ... (other using statements) ...

namespace WpfApp2
{
    public partial class AddNewMedicine : Window
    {
        dbManager dbManager = new dbManager();
        Admin admin = new Admin(); 
        private String username = MainWindow.Username;
        AdminInventory inventory = new AdminInventory();


        public AddNewMedicine()
        {
            InitializeComponent();
        }


        private void BtnAddMedicine_Click_1(object sender, RoutedEventArgs e)
        {
            string medicineName = TxtMedicineName.Text.Trim().Replace("'", "''");
            string genericName = TxtGenericName.Text.Trim().Replace("'", "''");
            string milligrams = TxtMilligrams.Text.Trim().Replace("'", "''");
            string description = TxtDescription.Text.Trim().Replace("'", "''");

            if (!int.TryParse(TxtInventoryAmount.Text.Trim(), out int inventoryAmount) || inventoryAmount < 0)
            {
                MessageBox.Show("Please enter a valid, non-negative number for the Inventory Amount.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(medicineName))
            {
                MessageBox.Show("Medicine Name cannot be empty.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string adminUsername = username;

            inventory.AddMedicine(medicineName, genericName, milligrams, description, inventoryAmount, adminUsername);
            AdminWindow activeMedicineRequest = Application.Current.Windows.OfType<AdminWindow>().SingleOrDefault(x => x.IsActive || x.IsVisible);
            if (activeMedicineRequest != null)
            {
                activeMedicineRequest.displayMedicineInv("select * from medicine_info");
            }
            this.Close();
        }
    }
}