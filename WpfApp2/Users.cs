using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    internal class Users
    {

        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;
        private string username = MainWindow.Username;
        int id = 0;
        String SQL = "";

        string strConn = "server=localhost; user id=root; password=; database=db_medicaremmcm";

        public void dbConnection()
        {
            using (dbConn = new MySqlConnection(strConn))
            {
                dbConn.Open();
                dbConn.Close();
            }
        }

        public DataTable displayRecords(string query)
        {
            using (dbConn = new MySqlConnection(strConn))
            {
                dbConn.Open();
                da = new MySqlDataAdapter(query, dbConn);
                dt = new DataTable();
                da.Fill(dt);
            }
            return dt;
        }

        public void sqlManager(string query)
        {
            using (dbConn = new MySqlConnection(strConn))
            {
                dbConn.Open();
                dbCommand = new MySqlCommand(query, dbConn);
                dbCommand.ExecuteNonQuery();
            }
        }
        public void setId(String username)
        {
            String SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }
        private Color GetStatusBackgroundColor(string status)
        {
            string lowerStatus = status.ToLower();
            if (lowerStatus == "approved" || lowerStatus == "confirmed")
                return (Color)ColorConverter.ConvertFromString("#E8F5E9");
            if (lowerStatus == "pending" || lowerStatus == "waitlist")
                return (Color)ColorConverter.ConvertFromString("#FFFDE7");
            if (lowerStatus == "cancelled" || lowerStatus == "rejected")
                return (Color)ColorConverter.ConvertFromString("#FFEBEE");

            return (Color)ColorConverter.ConvertFromString("#F0F0F0");
        }

        private Brush GetStatusForegroundColor(string status)
        {
            string lowerStatus = status.ToLower();
            if (lowerStatus == "approved" || lowerStatus == "confirmed")
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007E4C"));
            if (lowerStatus == "pending" || lowerStatus == "waitlist")
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE0C200"));
            if (lowerStatus == "cancelled" || lowerStatus == "rejected")
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D32F2F"));

            return Brushes.Gray;
        }

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
                    FontWeight = FontWeights.Normal,
                    FontSize = 9
                }
            };
        }

        public void cancelAppointment(string appointmentId)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to cancel this appointment? This action cannot be undone.",
                "Confirm Cancellation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                string deleteQuery = $"DELETE FROM appointments WHERE appointment_id = {appointmentId}";
                sqlManager(deleteQuery);
                MessageBox.Show("Appointment cancelled successfully!", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
                setId(username);
                String SQL = $@"
                            INSERT INTO student_activity_log (user_id, activity_type, activity_desc)
                            VALUES ({id}, 'Appointment', 'Cancel appointment')";
                sqlManager(SQL);


            }
        }

        public Border appointmentPanel(string appointmentId, string date, string time, string status, string purpose, string reason)
        {
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray);
            Brush redBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD70000")); // Define a red color for the reason

            Border cardBorder = new Border
            {
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCCCCCC")),
                BorderThickness = new Thickness(1),
                Background = Brushes.White,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10, 4, 10, 4),
                Padding = new Thickness(12, 6, 12, 6),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            StackPanel appointmentContent = new StackPanel();

            // Header
            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            headerGrid.Margin = new Thickness(0, 0, 0, 3);

            TextBlock txtTitle = new TextBlock
            {
                Text = purpose,
                FontWeight = FontWeights.SemiBold,
                FontSize = 9,
                Foreground = darkBlueBrush,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(txtTitle, 0);
            headerGrid.Children.Add(txtTitle);

            Border statusTag = CreateStatusTag(status);
            Grid.SetColumn(statusTag, 1);
            headerGrid.Children.Add(statusTag);
            appointmentContent.Children.Add(headerGrid);

            // Details
            StackPanel detailsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            detailsPanel.Children.Add(new TextBlock
            {
                Text = $"📅 {date}",
                FontSize = 9,
                Foreground = lightGrayBrush,
                Margin = new Thickness(0, 0, 10, 0)
            });

            detailsPanel.Children.Add(new TextBlock
            {
                Text = "|",
                FontSize = 9,
                Foreground = darkBlueBrush,
                Margin = new Thickness(0, 0, 10, 0)
            });

            detailsPanel.Children.Add(new TextBlock
            {
                Text = $"🕒 {time}",
                FontSize = 9,
                Foreground = lightGrayBrush,
                Margin = new Thickness(0, 0, 10, 0)
            });

            detailsPanel.Children.Add(new TextBlock
            {
                Text = "|",
                FontSize = 9,
                Foreground = darkBlueBrush,
                Margin = new Thickness(0, 0, 10, 0)
            });

            detailsPanel.Children.Add(new TextBlock
            {
                Text = $"ID: {appointmentId}",
                FontSize = 9,
                Foreground = lightGrayBrush,
                FontWeight = FontWeights.Medium
            });

            appointmentContent.Children.Add(detailsPanel);

            // =============================================================
            // NEW: ADD REASON TEXT BLOCK CONDITIONALLY
            // =============================================================
            if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(reason))
            {
                // Add a separator space
                appointmentContent.Children.Add(new Separator { Margin = new Thickness(0, 5, 0, 0) });

                // TextBlock for the reason label
                appointmentContent.Children.Add(new TextBlock
                {
                    Text = "Rejection Reason:",
                    FontWeight = FontWeights.Bold,
                    FontSize = 9,
                    Foreground = redBrush,
                    Margin = new Thickness(0, 0, 0, 2)
                });

                // TextBlock for the actual reason content
                appointmentContent.Children.Add(new TextBlock
                {
                    Text = reason,
                    FontSize = 9,
                    Foreground = redBrush,
                    TextWrapping = TextWrapping.Wrap // Allows long reasons to wrap
                });
            }
            // =============================================================

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
                    string SQL = $"DELETE FROM appointments WHERE appointment_id = '{currentAppointmentId}'";
                    sqlManager(SQL);
                    setId(username);
                    SQL = $@"
                            INSERT INTO student_activity_log (user_id, activity_type, activity_desc)
                            VALUES ({id}, 'Appointment', 'Cancel appointment ID: {appointmentId}')";
                    sqlManager(SQL);
                    TriggerAppointmentActivityPanelReload();
                };

                if (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                {
                    MenuItem updateItem = new MenuItem { Header = "Update Appointment" };
                    updateItem.Click += (s2, e2) =>
                    {
                        MessageBox.Show($"Opening Update Form for ID: {currentAppointmentId}");
                        UpdateAppointment update = new UpdateAppointment(appointmentId);

                        var activeWindow = Application.Current.Windows
                            .OfType<Window>()
                            .SingleOrDefault(x => x.IsActive);

                        if (activeWindow != null)
                        {
                            activeWindow.Hide();
                        }

                        update.Show();
                    };
                    contextMenu.Items.Add(updateItem);
                    contextMenu.Items.Add(cancelItem);
                }
                else if (currentStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                {
                    contextMenu.Items.Add(new MenuItem { Header = "Update is disabled (Approved)", IsEnabled = false });
                    contextMenu.Items.Add(cancelItem);
                }
                else
                {
                    contextMenu.Items.Add(new MenuItem { Header = $"Actions restricted (Status: {currentStatus})", IsEnabled = false });
                }

                if (contextMenu.Items.Count > 0)
                {
                    cardBorder.ContextMenu = contextMenu;
                    contextMenu.IsOpen = true;
                    e.Handled = true;
                }
            };

            return cardBorder;
        }

        public void TriggerAppointmentActivityPanelReload()
        {
            UserForm activeUserForm = Application.Current.Windows
                .OfType<UserForm>()
                .SingleOrDefault(x => x.IsActive || x.IsVisible);

            if (activeUserForm != null)
            {
                activeUserForm.displayAppointment($"SELECT * FROM appointments WHERE username = '{username}' AND CONCAT(appointment_date, ' ', appointment_time) >= NOW()");
                activeUserForm.displayActivities($"SELECT * FROM student_activity_log WHERE user_id = '{id}' ORDER BY activity_date DESC LIMIT 5");
            }
            else
            {
                MessageBox.Show("Could not find the User Dashboard to refresh.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Border activityPanel(String activityId, String type, String description, String dateTime)
        {
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray);

            Border cardBorder = new Border
            {
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xF7, 0xFA)),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10, 4, 10, 4),
                Padding = new Thickness(12, 6, 12, 6),
                HorizontalAlignment = HorizontalAlignment.Stretch,

            };

            StackPanel activityContent = new StackPanel();

            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            headerGrid.Margin = new Thickness(0, 0, 0, 3);

            TextBlock txtType = new TextBlock
            {
                Text = type,
                FontWeight = FontWeights.SemiBold,
                FontSize = 9,
                Foreground = darkBlueBrush,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(txtType, 0);
            headerGrid.Children.Add(txtType);

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
                    FontWeight = FontWeights.Normal,
                    FontSize = 9
                }
            };
            Grid.SetColumn(idTag, 1);
            headerGrid.Children.Add(idTag);

            activityContent.Children.Add(headerGrid);


            TextBlock txtDateTime = new TextBlock
            {
                Text = $"⌚ {dateTime}",
                FontSize = 9,
                Foreground = lightGrayBrush,
                Margin = new Thickness(0, 0, 0, 2)
            };
            activityContent.Children.Add(txtDateTime);

            TextBlock txtDescription = new TextBlock
            {
                Text = description,
                FontSize = 9,
                Foreground = darkBlueBrush,
                TextWrapping = TextWrapping.Wrap
            };
            activityContent.Children.Add(txtDescription);


            cardBorder.Child = activityContent;

            return cardBorder;
        }

        public String upcommingAppointments()
        {
            setId(username);
            SQL = $"SELECT * FROM appointments " +
                  $"WHERE user_id = '{id}' " +
                  $"AND status = 'Approved' " +
                  $"AND TIMESTAMP(appointment_date, appointment_time) >= NOW() " +
                  $"ORDER BY TIMESTAMP(appointment_date, appointment_time) ASC " +
                  $"LIMIT 1";
            DataTable dt = displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                DateTime date = Convert.ToDateTime(dt.Rows[0]["appointment_date"]);
                TimeSpan time = (TimeSpan)dt.Rows[0]["appointment_time"];

                DateTime fullDateTime = date.Add(time);

                return fullDateTime.ToString("MMMM dd, yyyy") + "\n" +
                                         fullDateTime.ToString("hh:mm tt");
            }
            else
            {
                return "No upcoming appointment";
            }
        }
        public String CheckUpDate()
        {
            setId (username);
            SQL = $"SELECT * FROM checkups WHERE user_id = '{id}' ORDER BY checkup_id DESC LIMIT 1";
            DataTable dt = displayRecords(SQL);

            if (dt.Rows.Count > 0)
            {
                DateTime recordedDate = Convert.ToDateTime(dt.Rows[0]["recorded_at"]);
                return recordedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                return "No Checkup record found.";
            }
        }
        public String height()
        {
            setId(username);
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                String height = dt.Rows[0]["height_cm"].ToString();
                return height + " cm";
            }
            else
            {
                return "No Height record found.";
            }
        }
        public String weight()
        {
            setId(username);
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                String weight = dt.Rows[0]["weight_kg"].ToString();
                return weight + " kg";
            }
            else
            {
                return "No Weight record found.";
            }
        }
        public String bmi()
        {
            setId(username);
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                String bmi = dt.Rows[0]["bmi"].ToString();
                return bmi;
            }
            else
            {
                return "No BMI record found.";
            }
        }
        public class MonthlyProgress
        {
            public string MonthName { get; set; }
            public double BMI { get; set; }
            public double Weight { get; set; }
        }
        public String name ()
        {
            SQL = $"select * from users where username = '{username}'";
            DataTable dt = displayRecords(SQL);
            String name = dt.Rows[0]["first_name"].ToString() + " " + dt.Rows[0]["last_name"].ToString();
            return "Welcome back " + name;
        }
        public List<MonthlyProgress> GetSixMonthsProgress(int userId)
        {
            try
            {
                string SQL = $@"
            SELECT 
                DATE_FORMAT(c.recorded_at, '%M') AS month_name, 
                c.bmi,
                c.weight_kg
            FROM checkups c
            INNER JOIN (
                SELECT 
                    YEAR(recorded_at) AS y,
                    MONTH(recorded_at) AS m,
                    MAX(recorded_at) AS max_date
                FROM checkups
                WHERE user_id = {userId}
                  AND recorded_at >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH)
                GROUP BY YEAR(recorded_at), MONTH(recorded_at)
            ) latest
            ON YEAR(c.recorded_at) = latest.y
            AND MONTH(c.recorded_at) = latest.m
            AND c.recorded_at = latest.max_date
            WHERE c.user_id = {userId}
            ORDER BY latest.y, latest.m ASC;
        ";

                DataTable dt = displayRecords(SQL);
                List<MonthlyProgress> list = new List<MonthlyProgress>();

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new MonthlyProgress
                    {
                        MonthName = row["month_name"].ToString(),
                        BMI = Convert.ToDouble(row["bmi"]),
                        Weight = Convert.ToDouble(row["weight_kg"])
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching six-month progress: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<MonthlyProgress>();
            }
        }


    }
}
