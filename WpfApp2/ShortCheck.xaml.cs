using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Media;
using MySql.Data.MySqlClient;


namespace WpfApp2
{
    public partial class ShortCheck : Window
    {
        String username = MainWindow.Username;
        public ShortCheck()
        {
            InitializeComponent();
        }

        
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(HeightTextBox.Text, out double heightCm) || heightCm <= 0)
            {
                BmiResultTextBlock.Text = "Error: Invalid Height.";
                BmiResultTextBlock.Foreground = Brushes.Red;
                return;
            }

            if (!double.TryParse(WeightTextBox.Text, out double weightKg) || weightKg <= 0)
            {
                BmiResultTextBlock.Text = "Error: Invalid Weight.";
                BmiResultTextBlock.Foreground = Brushes.Red;
                return;
            }

            // 2. BMI Calculation
            // Convert height from centimeters (cm) to meters (m)
            double heightM = heightCm / 100.0;

            // BMI Formula: weight (kg) / height (m)^2
            double bmi = weightKg / (heightM * heightM);

            // Round the BMI for display
            string roundedBmi = Math.Round(bmi, 1).ToString();

            // 3. Determine Category and Set UI
            string category;
            Brush categoryColor;

            if (bmi < 18.5)
            {
                category = "Underweight";
                categoryColor = Brushes.Blue;
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                category = "Normal Weight";
                categoryColor = Brushes.Green;
            }
            else if (bmi >= 25.0 && bmi <= 29.9)
            {
                category = "Overweight";
                categoryColor = Brushes.Orange;
            }
            else
            {
                category = "Obese";
                categoryColor = Brushes.Red;
            }

            // 4. Update the Result TextBlock
            BmiResultTextBlock.Text = $"Your BMI: {roundedBmi} ({category})";
            BmiResultTextBlock.Foreground = categoryColor;
        }

      
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Regex that allows digits and a single decimal point (based on current culture)
            Regex regex = new Regex("[^0-9,.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           UserForm userForm = new UserForm(username);
            userForm.Show();
            this.Close();
        }


    }
}
