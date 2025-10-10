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
        String strconn = "server=localhost;user id=root;password=;database=db_medicaremmcm";
        private int _userId;
        public ShortCheck(int userId)
        {
            InitializeComponent();
            _userId = userId;
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

            
            BmiResultTextBlock.Text = $"Your BMI: {roundedBmi} ({category})";
            BmiResultTextBlock.Foreground = categoryColor;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(strconn))
                {
                    conn.Open();


                    int userId = 1; //sample lang na userid

                    string sql = "INSERT INTO checkups (user_id, height_cm, weight_kg, bmi, recorded_at) " +
                                 "VALUES (@user_id, @height_cm, @weight_kg, @bmi, NOW())";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@height_cm", heightCm);
                    cmd.Parameters.AddWithValue("@weight_kg", weightKg);
                    cmd.Parameters.AddWithValue("@bmi", Math.Round(bmi, 2));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            
            Regex regex = new Regex("[^0-9,.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           //UserForm userForm = new UserForm(username);
           //userForm.Show();
           //this.Close();

          

        }
        private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Add your logic here if needed
        }
        private void WeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: any code when text changes
        }





    }
}
