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

    public partial class UpdateMedicine : Window
    {
        AdminInventory inventory = new AdminInventory();
        private String medicineId = "";
        private String medicineName = "";
        private String genericName = "";
        private String dosage = "";
        private String description = "";
        public UpdateMedicine(string medicineId, string medicineName, string genericName, string dosage, string description)
        {
            InitializeComponent();
            this.medicineId = medicineId;
            this.medicineName = medicineName;
            this.genericName = genericName;
            this.dosage = dosage;
            this.description = description;

            populateMedicine();
        }

        private void BtnUpdateMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (AreAllFieldsFilled())
            {
                inventory.UpdateMedicineInfo(Convert.ToInt32(medicineId), TxtMedicineName.Text, TxtGenericName.Text, TxtMilligrams.Text, TxtDescription.Text);

                AdminWindow activeMedicineRequest = Application.Current.Windows.OfType<AdminWindow>().SingleOrDefault(x => x.IsActive || x.IsVisible);
                if (activeMedicineRequest != null)
                {
                    activeMedicineRequest.displayMedicineInv("select * from medicine_info");
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill out all required fields before proceeding.",
                                "Incomplete Information",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        public void populateMedicine()
        {
            TxtMedicineName.Text = medicineName;
            TxtGenericName.Text = genericName;
            TxtDescription.Text = description;
            TxtMilligrams.Text = dosage;
        }
        private bool AreAllFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(TxtMedicineName.Text)) return false;
            if (string.IsNullOrWhiteSpace(TxtGenericName.Text)) return false;
            if (string.IsNullOrWhiteSpace(TxtDescription.Text)) return false;
            if (string.IsNullOrWhiteSpace(TxtMilligrams.Text)) return false;


            return true;

        }

        
    }
}
