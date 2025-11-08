using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System.Data;


namespace WpfApp2
{
    public partial class ShortCheck : Window
    {
        Users user = new Users();
        UserQuickCheck userShortCheck = new UserQuickCheck();
        private String username = MainWindow.Username;
        public ShortCheck()
        {
            InitializeComponent();
        }   

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(HeightTextBox.Text, out double heightCm))
            {
                BmiResultTextBlock.Text = "Error: Invalid Height.";
                BmiResultTextBlock.Foreground = Brushes.Red;
                return;
            }

            if (!double.TryParse(WeightTextBox.Text, out double weightKg))
            {
                BmiResultTextBlock.Text = "Error: Invalid Weight.";
                BmiResultTextBlock.Foreground = Brushes.Red;
                return;
            }

            var (resultText, color, bmiValue) = userShortCheck.CalculateBmi(heightCm, weightKg);

            BmiResultTextBlock.Text = resultText;
            BmiResultTextBlock.Foreground = color;

            if (color != Brushes.Red) 
            {

                userShortCheck.InsertBmiCheckup(user.getID().ToString(), heightCm, weightKg, bmiValue);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserForm userform = new UserForm();
            this.Close();
            userform.Show();
        }
    }
}
