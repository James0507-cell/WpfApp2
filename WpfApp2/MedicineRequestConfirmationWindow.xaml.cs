using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public partial class MedicineRequestConfirmation : Window
    {
        Users user = new Users();
        dbManager dbManager = new dbManager();
        UserMedicine userMedicine = new UserMedicine();
        private String dose = "";
        private String medicineName = "";
        private String genericName = "";

        public MedicineRequestConfirmation(string dose, string medicineName, string genericName)
        {
            InitializeComponent();
            this.dose = dose;
            this.medicineName = medicineName;
            this.genericName = genericName;
        }

        private void btnConfirmRequest_Click(object sender, RoutedEventArgs e)
        {
            if (AreAllFieldsFilled())
            {
                userMedicine.InsertMedicineRequest(user.getID().ToString(), medicineName, txtPurpose.Text, txtQuantity.Text);

                MedicineRequest activeMedicineRequest = Application.Current.Windows.OfType<MedicineRequest>().SingleOrDefault(x => x.IsActive || x.IsVisible);
                if (activeMedicineRequest != null)
                {
                    activeMedicineRequest.displayMedicineRequest("SELECT * FROM medicinerequests WHERE user_id = " + user.getID() + " ORDER BY request_id DESC");
                        ;
                }
                MessageBox.Show("Request is send!");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblDose.Text = dose;
            lblGenericName.Text = genericName;
            lblMedicineName.Text = medicineName;

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtQuantity_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;

            string input = textBox.Text;

            if (!Regex.IsMatch(input, @"^[1-9][0-9]*$"))
            {
                string digitsOnly = new string(input.Where(char.IsDigit).ToArray());

                if (digitsOnly.StartsWith("0"))
                    digitsOnly = digitsOnly.TrimStart('0');

                textBox.Text = digitsOnly;
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private bool AreAllFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(txtQuantity.Text)) return false;
            if (string.IsNullOrWhiteSpace(txtPurpose.Text)) return false;

            return true;
        }

    }
}
