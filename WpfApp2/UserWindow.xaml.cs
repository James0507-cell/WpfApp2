using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using static WpfApp2.Users;

namespace WpfApp2
{
    public partial class UserForm : Window
    {
        dbManager dbManager = new dbManager();
        UserHomePage userHomePage = new UserHomePage();
        Users user = new Users();
        

        public UserForm()
        {
            InitializeComponent();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShortCheck shortCheck = new ShortCheck();
            shortCheck.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MedicineRequest medicineRequest = new MedicineRequest();
            medicineRequest.Show();
            this.Close();
        }
        private void btnAppoint_Click(object sender, RoutedEventArgs e)
        {
            BookingAppointment bookingAppointment = new BookingAppointment();

            bookingAppointment.Show();
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BookingAppointment bookingAppointment = new BookingAppointment();

            bookingAppointment.Show();
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            displayAppointment($"SELECT * FROM appointments WHERE username = '{user.getUsername()}' AND CONCAT(appointment_date, ' ', appointment_time) >= NOW()");
            displayActivities($"SELECT * FROM student_activity_log WHERE user_id = '{user.getID()}'");
            getName();
            displayBMI();
            displayCheckupDate();
            displayUpcommingAppointment();
            displayHeight();
            displayWeight();
            displayProgressBar();
            displaySixMonthsProgress();
        }

        public void displayAppointment(string query)
        {
            AppointmentStackPanel.Children.Clear();
            DataTable dt = dbManager.displayRecords(query);
            AppointmentStackPanel.Children.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string appointmentId = dt.Rows[i]["appointment_id"].ToString();
                string date = dt.Rows[i]["appointment_date"].ToString();
                string time = dt.Rows[i]["appointment_time"].ToString();
                string status = dt.Rows[i]["status"].ToString();
                string purpose = dt.Rows[i]["purpose_of_visit"].ToString();
                string reason = dt.Rows[i]["reason"].ToString();

                Border cardBorder = userHomePage.appointmentPanel(appointmentId, date, time, status, purpose, reason);
                AppointmentStackPanel.Children.Add(cardBorder);
            }
        }
       
        public void displayActivities(String query)
        {
            StackPanelActivities.Children.Clear();
            DataTable dt = dbManager.displayRecords(query);
            StackPanelActivities.Children.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string activityId = dt.Rows[i]["activity_id"].ToString();
                string type = dt.Rows[i]["activity_type"].ToString();
                string description = dt.Rows[i]["activity_desc"].ToString();
                string dateTime = dt.Rows[i]["activity_date"].ToString(); 

                Border cardBorder = userHomePage.activityPanel(activityId, type, description, dateTime);
                StackPanelActivities.Children.Add(cardBorder);
            }
        }
        
        private void getName()
        {
            String name = user.getName();
            lblName.Content = name+"!";
        }

        public void displayBMI()
        {
            String BMI = user.getBMI();
            lblbmi.Content = BMI;
        }
        public void displayCheckupDate()
        {
            String checkupDate = userHomePage.CheckUpDate();
            lblCheck.Content = checkupDate;
        }
        public void displayUpcommingAppointment()
        {
            String AppoitnemntDate = userHomePage.upcommingAppointments();
            lblIncomingAppointment.Content = AppoitnemntDate;
        }
        public void displayHeight()
        {
           String heigt = user.getHeight();
            lblHeight.Content = heigt;
        }
        public void displayWeight()
        {
            String weigt = user.getWeight();
            lblWeight.Content = weigt;
        }
        public void displayProgressBar()
        {
            try
            {
                lblTargetRange.Content = "Range: 0 - 40";
                pbBMI.Minimum = 0;
                pbBMI.Maximum = 40;

                double? bmi = Convert.ToDouble(user.getBMI());

                if (bmi.HasValue)
                {
                    pbBMI.Value = bmi.Value;
                    lblBMIStatus.Content = $"BMI: {bmi.Value:F2}";
                    UpdateBMIUI(bmi.Value);
                }
                else
                {
                    pbBMI.Value = 0;
                    pbBMI.Foreground = new SolidColorBrush(Colors.Gray);
                    lblBMIStatus.Content = "BMI: N/A";
                    lblBMIRange.Content = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying BMI progress: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                pbBMI.Value = 0;
                pbBMI.Foreground = new SolidColorBrush(Colors.Gray);
                lblBMIStatus.Content = "BMI: N/A";
            }
        }
        private void UpdateBMIUI(double bmi)
        {
            string colorHex, bgHex, label;

            if (bmi < 18.5)
            {
                colorHex = "#2196F3"; bgHex = "#E3F2FD"; label = "Underweight";
            }
            else if (bmi < 24.9)
            {
                colorHex = "#4CAF50"; bgHex = "#E8F5E9"; label = "Normal range";
            }
            else if (bmi < 29.9)
            {
                colorHex = "#FF9800"; bgHex = "#FFF3E0"; label = "Overweight";
            }
            else
            {
                colorHex = "#F44336"; bgHex = "#FFEBEE"; label = "Obese range";
            }

            var color = (Color)ColorConverter.ConvertFromString(colorHex);
            var bgColor = (Color)ColorConverter.ConvertFromString(bgHex);

            pbBMI.Foreground = new SolidColorBrush(color);
            lblBMIRange.Content = label;
            brdStatus.BorderBrush = new SolidColorBrush(color);
            brdStatus.Background = new SolidColorBrush(bgColor);
        }


        public void displaySixMonthsProgress()
        {
            List<UserHomePage.MonthlyProgress> progressList = userHomePage.GetSixMonthsProgress(user.getID());

            for (int i = 0; i < 6; i++)
            {
                ProgressBar pb = null;
                Label lblMonth = null;
                Label lblBmi = null;
                Label lblWeight = null;

                switch (i)
                {
                    case 0: pb = pbMonth1; lblMonth = lblMonth1; lblBmi = lblBmi1; lblWeight = lblWeight1; break;
                    case 1: pb = pbMonth2; lblMonth = lblMonth2; lblBmi = lblBmi2; lblWeight = lblWeight2; break;
                    case 2: pb = pbMonth3; lblMonth = lblMonth3; lblBmi = lblBmi3; lblWeight = lblWeight3; break;
                    case 3: pb = pbMonth4; lblMonth = lblMonth4; lblBmi = lblBmi4; lblWeight = lblWeight4; break;
                    case 4: pb = pbMonth5; lblMonth = lblMonth5; lblBmi = lblBmi5; lblWeight = lblWeight5; break;
                    case 5: pb = pbMonth6; lblMonth = lblMonth6; lblBmi = lblBmi6; lblWeight = lblWeight6; break;
                }

                if (i < progressList.Count)
                {
                    var progress = progressList[i];

                    string shortMonthName = progress.MonthName.Length > 3
                        ? progress.MonthName.Substring(0, 3)
                        : progress.MonthName;

                    lblMonth.Content = shortMonthName;
                    lblBmi.Content = $"{progress.BMI:F2}";
                    lblWeight.Content = $"{progress.Weight:F1}";
                    pb.Value = Math.Min(Math.Max(progress.BMI, 0), 40);

                    string colorHex = progress.BMI < 18.5 ? "#2196F3" :
                                      progress.BMI < 24.9 ? "#4CAF50" :
                                      progress.BMI < 29.9 ? "#FF9800" : "#F44336";

                    pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
                }
                else
                {
                    lblMonth.Content = "—";
                    lblBmi.Content = "BMI: N/A";
                    lblWeight.Content = "";
                    pb.Value = 0;
                    pb.Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
        }
    }
}
