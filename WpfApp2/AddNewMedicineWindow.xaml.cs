using System;
using System.Data; // Required for DataTable
using System.Windows;
// ... (other using statements) ...

namespace WpfApp2
{
    public partial class AddNewMedicine : Window
    {
        AdminInventory inventory = new AdminInventory();


        public AddNewMedicine()
        {
            InitializeComponent();
        }


        private void BtnAddMedicine_Click_1(object sender, RoutedEventArgs e)
        {
            if (AreAllFieldsFilled())
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

                inventory.AddMedicine(medicineName, genericName, milligrams, description, inventoryAmount);
                AdminWindow activeMedicineRequest = Application.Current.Windows.OfType<AdminWindow>().SingleOrDefault(x => x.IsActive || x.IsVisible);
                if (activeMedicineRequest != null)
                {
                    activeMedicineRequest.displayMedicineInv("select * from medicine_info");
                }
                this.Close();
            } else
            {
                MessageBox.Show("Please fill out all required fields before proceeding.",
                                "Incomplete Information",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void TxtInventoryAmount_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private bool AreAllFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(TxtMedicineName.Text)) return false;
            if (string.IsNullOrWhiteSpace(TxtGenericName.Text)) return false;
            if (string.IsNullOrWhiteSpace(TxtDescription.Text)) return false;
            if (string.IsNullOrWhiteSpace (TxtInventoryAmount.Text)) return false;
            if (string.IsNullOrWhiteSpace (TxtMilligrams.Text)) return false;
            

            return true;
        }

    }
}