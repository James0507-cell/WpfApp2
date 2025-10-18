using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;

namespace WpfApp2
{
    public partial class UserForm : Window
    {
        private string username;
        Users userForm = new Users();
        ShortCheck shortcheck = new ShortCheck();
        string SQL = "";
        int userId;

        public UserForm(string username)
        {
            InitializeComponent();
            this.username = username;

            
        }
        // In UserForm.cs
        public string GetUsername()
        {
            return this.username;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            shortcheck.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            BookingAppointment bookingAppointment = new BookingAppointment
            {
                username = this.username
            };
            bookingAppointment.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getUserId(username);
            SQL = $"SELECT * FROM appointments WHERE username = '{username}'";
            displayAppointment(SQL);
            SQL = $"SELECT * FROM student_activity_log WHERE user_id = '{userId}' ORDER BY activity_date DESC LIMIT 5";
            displayActivities(SQL);
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

            DataTable dt = userForm.displayRecords(query);
            AppointmentStackPanel.Children.Clear();

            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray); // For better readability

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string appointmentId = dt.Rows[i]["appointment_id"].ToString();
                string date = dt.Rows[i]["appointment_date"].ToString();
                string time = dt.Rows[i]["appointment_time"].ToString();
                string status = dt.Rows[i]["status"].ToString();
                string purpose = dt.Rows[i]["purpose_of_visit"].ToString();



                Border cardBorder = userForm.appointmentPanel(appointmentId, date, time, status, purpose);

                AppointmentStackPanel.Children.Add(cardBorder);
            }
        }

       
        public void displayActivities(String query)
        {
            
            DataTable dt = userForm.displayRecords(query);

            
            StackPanelActivities.Children.Clear();

            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                string activityId = dt.Rows[i]["activity_id"].ToString();
                string type = dt.Rows[i]["activity_type"].ToString();
                string description = dt.Rows[i]["activity_desc"].ToString();
                string dateTime = dt.Rows[i]["activity_date"].ToString(); 

                Border cardBorder = userForm.activityPanel(activityId, type, description, dateTime);

                StackPanelActivities.Children.Add(cardBorder);
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MedicineRequest medicineRequest = new MedicineRequest();
            medicineRequest.Show();
            this.Close();
        }

        private void ___No_Name__IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private void getName()
        {
            SQL = $"select * from users where username = '{username}'";
            DataTable dt = userForm.displayRecords(SQL);
            String name = dt.Rows[0]["first_name"].ToString() + " " + dt.Rows[0]["last_name"].ToString();
            lblName.Content = "Welcome back " + name;
        }

        private void btnAppoint_Click(object sender, RoutedEventArgs e)
        {
            BookingAppointment bookingAppointment = new BookingAppointment
            {
                username = this.username
            };
            bookingAppointment.Show();
            this.Close();
        }
        public void getUserId(String username)
        {
            SQL = $"Select user_id from users where username = '{username}'";
            DataTable dt = new DataTable();
            dt = userForm.displayRecords(SQL);
            userId = Convert.ToInt32(dt.Rows[0][0].ToString());
        }
        public void displayBMI()
        {
            SQL = $"Select * from checkups where user_id = '{userId}' order by checkup_id desc limit 1";
            DataTable dt = userForm.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                String bmi = dt.Rows[0]["bmi"].ToString();
                lblbmi.Content = bmi;
            }
            else
            {
                lblbmi.Content = "No BMI record found.";
            }

        }
        public void displayCheckupDate()
        {
            SQL = $"SELECT * FROM checkups WHERE user_id = '{userId}' ORDER BY checkup_id DESC LIMIT 1";
            DataTable dt = userForm.displayRecords(SQL);

            if (dt.Rows.Count > 0)
            {
                DateTime recordedDate = Convert.ToDateTime(dt.Rows[0]["recorded_at"]);
                lblCheck.Content = recordedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                lblCheck.Content = "No Checkup record found.";
            }
        }
        public void displayUpcommingAppointment()
        {
            SQL = $"SELECT * FROM appointments " +
                  $"WHERE user_id = '{userId}' " +
                  $"AND status = 'Approved' " +
                  $"AND TIMESTAMP(appointment_date, appointment_time) >= NOW() " +
                  $"ORDER BY TIMESTAMP(appointment_date, appointment_time) ASC " +
                  $"LIMIT 1";
            DataTable dt = userForm.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                DateTime date = Convert.ToDateTime(dt.Rows[0]["appointment_date"]);
                TimeSpan time = (TimeSpan)dt.Rows[0]["appointment_time"];

                DateTime fullDateTime = date.Add(time);

                lblIncomingAppointment.Content = fullDateTime.ToString("MMMM dd, yyyy") + "\n" +
                                         fullDateTime.ToString("hh:mm tt");
            }
            else
            {
                lblIncomingAppointment.Content = "No upcoming appointment";
            }

        }
        public void displayHeight()
        {
            SQL = $"Select * from checkups where user_id = '{userId}' order by checkup_id desc limit 1";
            DataTable dt = userForm.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                String height = dt.Rows[0]["height_cm"].ToString();
                lblHeight.Content = height + " cm";
            }
            else
            {
                lblHeight.Content = "No Height record found.";
            }
        }
        public void displayWeight()
        {
            SQL = $"Select * from checkups where user_id = '{userId}' order by checkup_id desc limit 1";
            DataTable dt = userForm.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                String weight = dt.Rows[0]["weight_kg"].ToString();
                lblWeight.Content = weight + " kg";
            }
            else
            {
                lblWeight.Content = "No Weight record found.";
            }

        }
        public void displayProgressBar()
        {
            try
            {
                lblTargetRange.Content = "Range: 0 - 40";
                SQL = $"SELECT bmi FROM checkups WHERE user_id = '{userId}' ORDER BY checkup_id DESC LIMIT 1";
                DataTable dt = userForm.displayRecords(SQL);

                pbBMI.Minimum = 0;
                pbBMI.Maximum = 40;

                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    double bmi = Convert.ToDouble(dt.Rows[0][0]);

                    if (bmi > pbBMI.Maximum)
                        bmi = pbBMI.Maximum;
                    if (bmi < pbBMI.Minimum)
                        bmi = pbBMI.Minimum;

                    pbBMI.Value = bmi;

                    if (bmi < 18.5) 
                    {
                        pbBMI.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2196F3")); 
                        lblBMIRange.Content = "Underweight range";
                        brdStatus.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2196F3"));
                        brdStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3F2FD"));
                    }
                    else if (bmi >= 18.5 && bmi < 24.9) 
                    {
                        pbBMI.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50")); 
                        lblBMIRange.Content = "Normal range";
                        brdStatus.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
                        brdStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8F5E9"));

                    }
                    else if (bmi >= 25 && bmi < 29.9) 
                    {
                        pbBMI.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9800")); 
                        lblBMIRange.Content = "Overweight range";
                        brdStatus.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9800"));
                        brdStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF3E0"));
                    }
                    else if (bmi >= 30)
                    {
                        pbBMI.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336")); 
                        lblBMIRange.Content = "Obese range";
                        brdStatus.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336"));
                        brdStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEBEE"));
                    }

                    lblBMIStatus.Content = $"BMI: {bmi:F1}";
                }
                else
                {
                   
                    pbBMI.Value = 0;
                    pbBMI.Foreground = new SolidColorBrush(Colors.Gray);
                    lblBMIStatus.Content = "BMI: N/A";
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
        public void displaySixMonthsProgress()
        {
            SQL = @"
                SELECT 
                    DATE_FORMAT(c.recorded_at, '%M') AS month_name,   -- Full month name (e.g., January)
                    c.bmi
                FROM checkups c
                INNER JOIN (
                    SELECT 
                        YEAR(recorded_at) AS y,
                        MONTH(recorded_at) AS m,
                        MAX(recorded_at) AS max_date
                    FROM checkups
                    WHERE user_id = 8
                      AND recorded_at >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH)
                    GROUP BY YEAR(recorded_at), MONTH(recorded_at)
                ) latest
                ON YEAR(c.recorded_at) = latest.y
                AND MONTH(c.recorded_at) = latest.m
                AND c.recorded_at = latest.max_date
                WHERE c.user_id = 8
                ORDER BY latest.y, latest.m ASC;
                ";

            DataTable dt = userForm.displayRecords(SQL);

            for (int i = 0; i < 6; i++)
            {
                ProgressBar pb = null;
                Label lblMonth = null;
                Label lblBmi = null;

                switch (i)
                {
                    case 0:
                        pb = pbMonth1; lblMonth = lblMonth1; lblBmi = lblBmi1;
                        break;
                    case 1:
                        pb = pbMonth2; lblMonth = lblMonth2; lblBmi = lblBmi2;
                        break;
                    case 2:
                        pb = pbMonth3; lblMonth = lblMonth3; lblBmi = lblBmi3;
                        break;
                    case 3:
                        pb = pbMonth4; lblMonth = lblMonth4; lblBmi = lblBmi4;
                        break;
                    case 4:
                        pb = pbMonth5; lblMonth = lblMonth5; lblBmi = lblBmi5;
                        break;
                    case 5:
                        pb = pbMonth6; lblMonth = lblMonth6; lblBmi = lblBmi6;
                        break;
                }

                if (i < dt.Rows.Count)
                {
                    string fullMonthName = dt.Rows[i]["month_name"].ToString();
                    string shortMonthName = fullMonthName.Length > 3
                        ? fullMonthName.Substring(0, 3)
                        : fullMonthName;

                    double bmi = Convert.ToDouble(dt.Rows[i]["bmi"].ToString());

                    lblMonth.Content = shortMonthName;
                    lblBmi.Content = $"{bmi:F1}";
                    pb.Value = Math.Min(Math.Max(bmi, 0), 40);

                    if (bmi < 18.5)
                    {
                        pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2196F3")); 
                    }
                    else if (bmi >= 18.5 && bmi < 24.9)
                    {
                        pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
                    }
                    else if (bmi >= 25 && bmi < 29.9)
                    {
                        pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9800"));
                    }
                    else if (bmi >= 30)
                    {
                        pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336")); 
                    }
                }
                else
                {
                    lblMonth.Content = "—";
                    lblBmi.Content = "BMI: N/A";
                    pb.Value = 0;
                    pb.Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BookingAppointment bookingAppointment = new BookingAppointment
            {
                username = this.username
            };
            bookingAppointment.Show();
            this.Close();
        }

        public void reloadAppointments()
        {
            AppointmentStackPanel.Children.Clear();
            displayAppointment($"SELECT * FROM appointments WHERE username = '{username}'");
        }
        public void reloadActivities()
        {
            StackPanelActivities.Children.Clear();
            displayActivities($"SELECT * FROM student_activity_log WHERE user_id = '{userId}' ORDER BY activity_date DESC LIMIT 5");
        }
    }
}
