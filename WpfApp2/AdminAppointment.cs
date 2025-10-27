using ControlzEx.Standard;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using YourNamespace;

namespace WpfApp2
{
    internal class AdminAppointment
    {
        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;
        private String username = MainWindow.Username;
        private int id;

        private string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";

        public AdminAppointment(int adminId)
        {
            id = adminId;
        }
        public void dbConnection()
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            MessageBox.Show("Connection Successful");
            dbConn.Close();
        }

        public DataTable displayRecords(string query)
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            da = new MySqlDataAdapter(query, dbConn);
            dt = new DataTable();
            da.Fill(dt);
            dbConn.Close();
            return dt;
        }

        public void sqlManager(string query)
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            dbCommand = new MySqlCommand(query, dbConn);
            dbCommand.ExecuteNonQuery();
            dbConn.Close();
        }
        
        private void Card_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border cardBorder && cardBorder.Tag is string appointmentID)
            {
                ContextMenu contextMenu = new ContextMenu
                {
                    FontSize = 14,
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")), // Dark Blue border
                    BorderThickness = new Thickness(2),
                    Effect = new System.Windows.Media.Effects.DropShadowEffect // Subtle shadow
                    {
                        Color = Colors.Gray,
                        Direction = 315,
                        ShadowDepth = 3,
                        BlurRadius = 5,
                        Opacity = 0.5
                    }
                };

                MenuItem approveItem = new MenuItem
                {
                    Header = "✅ Approve Appointment",
                    Tag = appointmentID,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50")), // Green text
                    FontWeight = FontWeights.DemiBold,
                    BorderThickness = new Thickness(0)
                };
                approveItem.Click += Approve_Click;

                MenuItem rejectItem = new MenuItem
                {
                    Header = "❌ Reject Appointment",
                    Tag = appointmentID,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF44336")), // Red text
                    FontWeight = FontWeights.DemiBold,
                    BorderThickness = new Thickness(0)
                };
                rejectItem.Click += Reject_Click;

                contextMenu.Items.Add(approveItem);
                contextMenu.Items.Add(rejectItem);

                contextMenu.PlacementTarget = cardBorder;
                contextMenu.IsOpen = true;

                e.Handled = true;
            }
        }
        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string appointmentID)
            {
                approveAppointment(appointmentID);
            }
        }
        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string appointmentID)
            {
                RejectionReasonDialog dialog = new RejectionReasonDialog();

                bool? dialogResult = dialog.ShowDialog();

                if (dialogResult == true)
                {
                    string rejectionReason = dialog.RejectionReason;

                    rejectAppointment(appointmentID, rejectionReason);
                }

            }
        }
        public void approveAppointment(String appointmentID)
        {
            String querry = $"UPDATE appointments SET status = 'Approved', handled_time = NOW() WHERE appointment_id = {appointmentID}";
            sqlManager(querry);

            querry = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{username}', 'Appointment Approved', 'Approved appointment ID {appointmentID}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            sqlManager(querry);
            TriggerAppointmentActivityPanelReload();
        }
        public void rejectAppointment(String appointmentID, String reason)
        {
            String querry = $"UPDATE appointments SET status = 'Rejected', reason = '{reason}', handled_time = NOW() WHERE appointment_id = {appointmentID}";
            sqlManager(querry);
            querry = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{username}', 'Appointment Rejected', 'Rejected appointment ID {appointmentID}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            sqlManager(querry);
            TriggerAppointmentActivityPanelReload();
        }

        public Border AppointmentPanel (String appointmentID, String patientID, String username, String studentID, String date, String time, String email, String phone, String purpose, String allergies, String medication, String previousVisit, String ecn, String ecp, String status, String symptoms, String handleTime) {
            TextBlock CreateDetailBlock(string label, string value, FontWeight weight = default)
            {
                if (weight == default) weight = FontWeights.Normal;
                return new TextBlock
                {
                    Text = $"{label}: {value}",
                    FontSize = 12,
                    Margin = new Thickness(0, 2, 0, 2),
                    FontWeight = weight,
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")) // Dark blue
                };
            }
            Border cardBorder = new Border
            {
                BorderBrush = new SolidColorBrush(Colors.LightGray),
                BorderThickness = new Thickness(1),
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xF7, 0xFA)),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(10, 6, 10, 6),
                Padding = new Thickness(15),
                Width = double.NaN,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Tag = appointmentID,
                ContextMenu = null
            };


            cardBorder.MouseRightButtonUp += Card_MouseRightButtonUp;



            Grid mainLayoutGrid = new Grid();
            mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            StackPanel primaryDetails = new StackPanel { Orientation = Orientation.Vertical };

            TextBlock txtNameID = new TextBlock
            {
                Text = $"{username} (ID: {patientID})",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")),
                Margin = new Thickness(0, 0, 0, 4)
            };
            primaryDetails.Children.Add(txtNameID);

            TextBlock txtSymptoms = new TextBlock
            {
                Text = $"Symptoms: {symptoms}",
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4D7399")),
                Margin = new Thickness(0, 0, 0, 4)
            };
            primaryDetails.Children.Add(txtSymptoms);

            TextBlock txtDateTime = new TextBlock
            {
                Text = $"Requested for: {date} at {time}",
                FontSize = 14,
                FontWeight = FontWeights.DemiBold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4D7399")),
                Margin = new Thickness(0, 0, 0, 8)
            };
            primaryDetails.Children.Add(txtDateTime);

            TextBlock txtHandledTime = new TextBlock
            {
                Text = handleTime != "" ? $"Handled Time: {handleTime}" : "Handled Time: N/A",
                FontSize = 12,
                FontWeight = FontWeights.Normal,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4D7399")),
                Margin = new Thickness(0, 0, 0, 0)
            };
            primaryDetails.Children.Add(txtHandledTime);

            Grid.SetColumn(primaryDetails, 0);
            headerGrid.Children.Add(primaryDetails);

            Border statusBorder = new Border
            {
                Padding = new Thickness(8, 4, 8, 4),
                CornerRadius = new CornerRadius(15),
                Margin = new Thickness(10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top
            };

            Color statusColor;
            switch (status.ToLower())
            {
                case "confirmed":
                case "approved":
                    statusColor = (Color)ColorConverter.ConvertFromString("#FF4CAF50");
                    break;
                case "pending":
                    statusColor = (Color)ColorConverter.ConvertFromString("#FFFFC107");
                    break;
                case "cancelled":
                case "rejected":
                    statusColor = (Color)ColorConverter.ConvertFromString("#FFF44336");
                    break;
                default:
                    statusColor = Colors.LightGray;
                    break;
            }
            statusBorder.Background = new SolidColorBrush(statusColor);

            TextBlock txtStatusTag = new TextBlock
            {
                Text = status.ToUpper(),
                Foreground = new SolidColorBrush(Colors.White),
                FontWeight = FontWeights.Bold,
                FontSize = 12
            };
            statusBorder.Child = txtStatusTag;

            Grid.SetColumn(statusBorder, 1);
            headerGrid.Children.Add(statusBorder);

            Grid.SetRow(headerGrid, 0);
            mainLayoutGrid.Children.Add(headerGrid);


            Separator separator = new Separator
            {
                Margin = new Thickness(0, 8, 0, 8),
                Foreground = new SolidColorBrush(Colors.LightGray)
            };
            Grid.SetRow(separator, 1);
            mainLayoutGrid.Children.Add(separator);


            Grid detailsGrid = new Grid
            {
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
            };

            StackPanel leftPanel = new StackPanel { Margin = new Thickness(0, 0, 10, 0) };
            leftPanel.Children.Add(CreateDetailBlock("Purpose", purpose, FontWeights.DemiBold));
            leftPanel.Children.Add(CreateDetailBlock("Student ID", studentID));
            leftPanel.Children.Add(CreateDetailBlock("Email", email));
            leftPanel.Children.Add(CreateDetailBlock("Phone", phone));
            leftPanel.Children.Add(CreateDetailBlock("Appointment ID", appointmentID));

            Grid.SetColumn(leftPanel, 0);
            detailsGrid.Children.Add(leftPanel);

            StackPanel rightPanel = new StackPanel { Margin = new Thickness(10, 0, 0, 0) };
            rightPanel.Children.Add(CreateDetailBlock("Previous Visit", previousVisit, FontWeights.DemiBold));
            rightPanel.Children.Add(CreateDetailBlock("Allergies", allergies));
            rightPanel.Children.Add(CreateDetailBlock("Medication", medication));
            rightPanel.Children.Add(CreateDetailBlock("Emergency Contact", ecn));
            rightPanel.Children.Add(CreateDetailBlock("EC Phone", ecp));

            Grid.SetColumn(rightPanel, 1);
            detailsGrid.Children.Add(rightPanel);

            Grid.SetRow(detailsGrid, 2);
            mainLayoutGrid.Children.Add(detailsGrid);


            cardBorder.Child = mainLayoutGrid;

            return cardBorder;

        }
        public void TriggerAppointmentActivityPanelReload()
        {
            AdminWindow activeAdminWindow = Application.Current.Windows
                .OfType<AdminWindow>()
                .SingleOrDefault(x => x.IsActive || x.IsVisible);

            if (activeAdminWindow != null)
            {
                activeAdminWindow.displayAppointments("Select * from appointments");
                activeAdminWindow.displayActivity("Select * from admin_activity_log");
            }
            else
            {
                MessageBox.Show("Could not find the User Dashboard to refresh.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
    }
