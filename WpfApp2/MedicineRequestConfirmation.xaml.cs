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
            userMedicine.InsertMedicineRequest(user.getID().ToString(), medicineName, txtPurpose.Text, txtQuantity.Text);

            MedicineRequest activeMedicineRequest = Application.Current.Windows.OfType<MedicineRequest>().SingleOrDefault(x => x.IsActive || x.IsVisible);
            if (activeMedicineRequest != null)
            {
                activeMedicineRequest.displayMedicineRequest("SELECT * FROM medicinerequests WHERE user_id = " + user.getID());
            }
            MessageBox.Show("Request is send!");
            this.Close();
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

        
    }
}
