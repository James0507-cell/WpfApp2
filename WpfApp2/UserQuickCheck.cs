using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;

namespace WpfApp2
{
    internal class UserQuickCheck
    {

        dbManager dbManager = new dbManager();

        public static class BmiKnowledgeBase
{
   
    public record BmiRule(double MaxValue, string Category, Brush Color);

    private static readonly List<BmiRule> Rules = new List<BmiRule>
    {
        new BmiRule(18.5, "Underweight", Brushes.Blue),
        new BmiRule(24.9, "Normal weight", Brushes.Green),
        new BmiRule(29.9, "Overweight", Brushes.Orange),
        new BmiRule(double.MaxValue, "Obese", Brushes.Red) 
    };


    public static bool IsInvalid(double heightCm, double weightKg)
    {
        return heightCm <= 0 || weightKg <= 0;
    }


    public static double DeriveBmi(double heightCm, double weightKg)
    {
        double heightM = heightCm / 100.0;
        return weightKg / (heightM * heightM);
    }

    public static (string Category, Brush Color) InferCategory(double bmi)
    {
        var rule = Rules
            .FirstOrDefault(r => bmi <= r.MaxValue);

        if (rule != null)
        {
            return (rule.Category, rule.Color);
        }

        return ("Unknown Category", Brushes.Black);
    }
}
public (string resultText, Brush color, double bmiValue) CalculateBmi(double heightCm, double weightKg)
{
    if (BmiKnowledgeBase.IsInvalid(heightCm, weightKg))
    {
        return ("Error: Invalid input values.", Brushes.Red, 0);
    }

    double bmi = BmiKnowledgeBase.DeriveBmi(heightCm, weightKg);

    var (category, color) = BmiKnowledgeBase.InferCategory(bmi);

    string resultText = $"Your BMI: {Math.Round(bmi, 1)} ({category})";

    return (resultText, color, bmi);
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
