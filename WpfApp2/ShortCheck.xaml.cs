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
        dbManager dbManager = new dbManager();
        String username = MainWindow.Username;
        String SQL = "";
        private int userId;
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

            double heightM = heightCm / 100.0;


            double bmi = weightKg / (heightM * heightM);

            string roundedBmi = Math.Round(bmi, 1).ToString();

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

            SQL = $"INSERT INTO checkups (user_id, height_cm, weight_kg, bmi) VALUES ('{userId}', '{heightCm}', '{weightKg}', '{bmi}')";
            dbManager.sqlManager(SQL);
            SQL = $"Insert into student_activity_log(user_id, activity_type, activity_desc) values ('{userId}', 'Vitals Check', 'BMI Check Up')";
            dbManager.sqlManager(SQL);

        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            
            Regex regex = new Regex("[^0-9,.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

           

        }
        private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void WeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        public void getID (String username)
        {
            SQL = $"Select user_id from users where username = '{username}'";
            DataTable dt = new DataTable();
            dt = dbManager.displayRecords(SQL);
            userId = Convert.ToInt32(dt.Rows[0][0].ToString());
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getID(username);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserForm userform = new UserForm(username);
            this.Close();
            userform.Show();
        }
    }
}
