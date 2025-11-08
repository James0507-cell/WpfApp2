using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp2
{
    internal class UserQuickCheck
    {

        dbManager dbManager = new dbManager();

        public (string resultText, Brush color, double bmiValue) CalculateBmi(double heightCm, double weightKg) =>
        (heightCm <= 0 || weightKg <= 0)
        ? ("Error: Invalid input values.", Brushes.Red, 0)
        : CalculateBmiResult(heightCm, weightKg);

        private static (string resultText, Brush color, double bmiValue) CalculateBmiResult(double heightCm, double weightKg)
        {
            double heightM = heightCm / 100.0;
            double bmi = weightKg / (heightM * heightM);
            string roundedBmi = Math.Round(bmi, 1).ToString();

            var (category, categoryColor) = bmi switch
            {
                < 18.5 => ("Underweight", Brushes.Blue),
                <= 24.9 => ("Normal Weight", Brushes.Green),
                <= 29.9 => ("Overweight", Brushes.Orange),
                _ => ("Obese", Brushes.Red)
            };

            string resultText = $"Your BMI: {roundedBmi} ({category})";
            return (resultText, categoryColor, bmi);
        }

        public void InsertBmiCheckup(string userId, double heightCm, double weightKg, double bmi)
        {
            string insertCheckup = $"INSERT INTO checkups (user_id, height_cm, weight_kg, bmi) " +
                                   $"VALUES ('{userId}', '{heightCm}', '{weightKg}', '{bmi}')";

            string insertLog = $"INSERT INTO student_activity_log (user_id, activity_type, activity_desc) " +
                               $"VALUES ('{userId}', 'Vitals Check', 'BMI Check Up')";

            dbManager.sqlManager(insertCheckup);
            dbManager.sqlManager(insertLog);
        }
        
    }
}
