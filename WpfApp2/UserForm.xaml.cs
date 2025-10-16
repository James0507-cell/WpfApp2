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

        /// <summary>
        /// Gets the main background color for the status tag.
        /// </summary>
        private Color GetStatusBackgroundColor(string status)
        {
            string lowerStatus = status.ToLower();
            if (lowerStatus == "approved" || lowerStatus == "confirmed")
                return (Color)ColorConverter.ConvertFromString("#E8F5E9"); // Light Green
            if (lowerStatus == "pending" || lowerStatus == "waitlist")
                return (Color)ColorConverter.ConvertFromString("#FFFDE7"); // Light Yellow
            if (lowerStatus == "cancelled" || lowerStatus == "rejected")
                return (Color)ColorConverter.ConvertFromString("#FFEBEE"); // Light Red

            return (Color)ColorConverter.ConvertFromString("#F0F0F0"); // Default Light Gray
        }

        /// <summary>
        /// Gets the foreground text color for the status tag.
        /// </summary>
        private Brush GetStatusForegroundColor(string status)
        {
            string lowerStatus = status.ToLower();
            if (lowerStatus == "approved" || lowerStatus == "confirmed")
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#388E3C")); // Dark Green
            if (lowerStatus == "pending" || lowerStatus == "waitlist")
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107")); // Amber/Dark Yellow
            if (lowerStatus == "cancelled" || lowerStatus == "rejected")
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D32F2F")); // Dark Red

            return Brushes.Gray;
        }

        /// <summary>
        /// Creates the styled status pill/tag element.
        /// </summary>
        private Border CreateStatusTag(string status)
        {
            return new Border
            {
                Background = new SolidColorBrush(GetStatusBackgroundColor(status)),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(8, 2, 8, 2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Child = new TextBlock
                {
                    Text = status.ToLower(),
                    Foreground = GetStatusForegroundColor(status),
                    FontWeight = FontWeights.Bold,
                    FontSize = 10
                }
            };
        }


        private void displayAppointment(string query)
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


                Border cardBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(5),
                    Margin = new Thickness(10, 4, 10, 4),
                    Padding = new Thickness(12, 6, 12, 6),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    
                };


                StackPanel appointmentContent = new StackPanel();

                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                headerGrid.Margin = new Thickness(0, 0, 0, 3);

                TextBlock txtTitle = new TextBlock
                {
                    Text = purpose,
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 12,
                    Foreground = darkBlueBrush,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(txtTitle, 0);
                headerGrid.Children.Add(txtTitle);

                Border statusTag = CreateStatusTag(status); 
                Grid.SetColumn(statusTag, 1);
                headerGrid.Children.Add(statusTag);

                appointmentContent.Children.Add(headerGrid);


                StackPanel detailsPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 0)
                };

                TextBlock txtDate = new TextBlock
                {
                    Text = $"📅 {date}",
                    FontSize = 11,
                    Foreground = lightGrayBrush,
                    Margin = new Thickness(0, 0, 10, 0)
                };
                detailsPanel.Children.Add(txtDate);

                TextBlock separator = new TextBlock
                {
                    Text = "|",
                    FontSize = 11,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")),
                    Margin = new Thickness(0, 0, 10, 0)
                };

                detailsPanel.Children.Add(separator);

                TextBlock txtTime = new TextBlock
                {
                    Text = $"🕒 {time}",
                    FontSize = 11,
                    Foreground = lightGrayBrush,
                    Margin = new Thickness(0, 0, 10, 0)
                };
                detailsPanel.Children.Add(txtTime);

                TextBlock separator2 = new TextBlock
                {
                    Text = "|",
                    FontSize = 11,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")),
                    Margin = new Thickness(0, 0, 10, 0)
                };
                detailsPanel.Children.Add(separator2);

                TextBlock txtId = new TextBlock
                {
                    Text = $"ID: {appointmentId}",
                    FontSize = 11,
                    Foreground = lightGrayBrush,
                    FontWeight = FontWeights.Medium
                };
                detailsPanel.Children.Add(txtId);

                appointmentContent.Children.Add(detailsPanel);

                cardBorder.Child = appointmentContent;


                string currentAppointmentId = appointmentId;
                string currentStatus = status;

                cardBorder.MouseRightButtonDown += (s, e) =>
                {
                    ContextMenu contextMenu = new ContextMenu();

                    MenuItem cancelItem = new MenuItem { Header = "Cancel Appointment" };
                    cancelItem.Click += (s3, e3) =>
                    {
                        
                        MessageBox.Show($"Request to Cancel Appointment ID: {currentAppointmentId}");
                        SQL = "Delete from appointments where appointment_id = '" + currentAppointmentId + "'";
                        userForm.sqlManager(SQL);
                        
                    };

                    if (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                    {
                        MenuItem updateItem = new MenuItem { Header = "Update Appointment" };
                        updateItem.Click += (s2, e2) =>
                        {
                            
                            MessageBox.Show($"Opening Update Form for ID: {currentAppointmentId}");
                            UpdateAppointment update = new UpdateAppointment(appointmentId);
                            this.Hide();
                            update.Show();
                        };
                        contextMenu.Items.Add(updateItem);
                        contextMenu.Items.Add(cancelItem);



                    }
                    else if (currentStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                    {
                        MenuItem noUpdateItem = new MenuItem { Header = "Update is disabled (Approved)", IsEnabled = false };
                        contextMenu.Items.Add(noUpdateItem);

                        contextMenu.Items.Add(cancelItem);
                    }
                    else
                    {
                        MenuItem disabledItem = new MenuItem { Header = $"Actions restricted (Status: {currentStatus})", IsEnabled = false };
                        contextMenu.Items.Add(disabledItem);

                    }

                    if (contextMenu.Items.Count > 0)
                    {
                        cardBorder.ContextMenu = contextMenu;
                        contextMenu.IsOpen = true;
                        e.Handled = true; 
                    }
                };

                AppointmentStackPanel.Children.Add(cardBorder);
            }
        }

       
        public void displayActivities(String query)
        {
            // Use the class-level userForm object to fetch data
            DataTable dt = userForm.displayRecords(query);

            // Clear the target panel before adding new items (assuming 'StackPanelActivities' is the target)
            // Make sure 'StackPanelActivities' is defined in your XAML.
            StackPanelActivities.Children.Clear();

            // Define consistent colors (matching the dark blue from displayAppointment)
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 1. Correct Data Extraction (using column names and current row index 'i')
                // NOTE: Column names are inferred from the original method's variable names.
                string activityId = dt.Rows[i]["activity_id"].ToString();
                string type = dt.Rows[i]["activity_type"].ToString();
                string description = dt.Rows[i]["activity_desc"].ToString();
                string dateTime = dt.Rows[i]["activity_date"].ToString(); // Typically a DateTime, but treating as string for display

                // 2. Create the Card Container (Border) - Replicating Appointment Style
                Border cardBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(5),
                    Margin = new Thickness(10, 4, 10, 4), // Consistent with Appointment style
                    Padding = new Thickness(12, 6, 12, 6),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    
                };

                // 3. Main StackPanel to hold all content vertically
                StackPanel activityContent = new StackPanel();

                // --- Row 1: Activity Type and ID ---
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                headerGrid.Margin = new Thickness(0, 0, 0, 3);

                // Activity Type (as Title)
                TextBlock txtType = new TextBlock
                {
                    Text = type, // e.g., "Login", "Appointment Booked"
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 12,
                    Foreground = darkBlueBrush,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(txtType, 0);
                headerGrid.Children.Add(txtType);

                // Activity Tag (Using a simplified tag for the ID)
                Border idTag = new Border
                {
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0F0F0")), // Light Gray background
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(8, 2, 8, 2),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                    Child = new TextBlock
                    {
                        Text = $"ID: {activityId}",
                        Foreground = Brushes.Gray,
                        FontWeight = FontWeights.Medium,
                        FontSize = 10
                    }
                };
                Grid.SetColumn(idTag, 1);
                headerGrid.Children.Add(idTag);

                activityContent.Children.Add(headerGrid);

                // --- Row 2: Date/Time and Description ---

                // Date/Time Block (⌚ 2025-10-09 10:00:00)
                TextBlock txtDateTime = new TextBlock
                {
                    Text = $"⌚ {dateTime}",
                    FontSize = 11,
                    Foreground = lightGrayBrush,
                    Margin = new Thickness(0, 0, 0, 2)
                };
                activityContent.Children.Add(txtDateTime);

                // Description
                TextBlock txtDescription = new TextBlock
                {
                    Text = description, // e.g., "User logged in successfully"
                    FontSize = 11,
                    Foreground = darkBlueBrush,
                    TextWrapping = TextWrapping.Wrap // Ensure long text wraps
                };
                activityContent.Children.Add(txtDescription);


                // 4. Attach Content to Card Border
                cardBorder.Child = activityContent;

                // 5. Add to the main StackPanel
                StackPanelActivities.Children.Add(cardBorder);
            }
        }

        public void cancelAppointment(string appointmentId)
        {
            // IMPORTANT: Since we cannot use alert/confirm in the environment, you would need a custom WPF dialog implementation here.
            // For now, I will use MessageBox as a placeholder until a custom dialog UI can be implemented.

            MessageBoxResult result = MessageBox.Show(
            "Are you sure you want to cancel this appointment? This action cannot be undone.",
            "Confirm Cancellation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                string deleteQuery = $"DELETE FROM appointments WHERE appointment_id = {appointmentId}";
                userForm.sqlManager(deleteQuery);
                MessageBox.Show("Appointment cancelled successfully!", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
                displayAppointment($"SELECT * FROM appointments WHERE username = '{username}'");
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
    }
}
