using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp2
{
    internal class UserShortCheck
    {
        dbManager dbManager = new dbManager();
        public (string resultText, Brush color, double bmiValue) CalculateBmi(double heightCm, double weightKg)
        {
            if (heightCm <= 0 || weightKg <= 0)
            {
                return ("Error: Invalid input values.", Brushes.Red, 0);
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
            else if (bmi <= 24.9)
            {
                category = "Normal Weight";
                categoryColor = Brushes.Green;
            }
            else if (bmi <= 29.9)
            {
                category = "Overweight";
                categoryColor = Brushes.Orange;
            }
            else
            {
                category = "Obese";
                categoryColor = Brushes.Red;
            }

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
        public int setID(String username)
        {
            String SQL = $"Select user_id from users where username = '{username}'";
            DataTable dt = new DataTable();
            dt = dbManager.displayRecords(SQL);
            return Convert.ToInt32(dt.Rows[0][0].ToString());

        }
    }
}
