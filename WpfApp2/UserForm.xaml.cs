using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Input;

namespace WpfApp2
{
    public partial class UserForm : Window
    {
        private string username;
        Users userForm = new Users();
        ShortCheck shortcheck = new ShortCheck();
        string SQL = "";
        
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
            mainWindow.Show();
            this.Close();
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
            SQL = $"SELECT * FROM appointments WHERE username = '{username}'";
            displayAppointment(SQL);
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

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 1. Extract Data
                string appointmentId = dt.Rows[i]["appointment_id"].ToString();
                string date = dt.Rows[i]["appointment_date"].ToString();
                string time = dt.Rows[i]["appointment_time"].ToString();
                string status = dt.Rows[i]["status"].ToString();
                string purpose = dt.Rows[i]["purpose_of_visit"].ToString();


                // 2. Create the Card Container (Border)
                Border cardBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(10, 4, 10, 4),
                    Padding = new Thickness(12, 8, 12, 8), // Reduced padding to lower height
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Effect = new DropShadowEffect
                    {
                        Color = Colors.LightGray,
                        Direction = 315,
                        ShadowDepth = 2,
                        BlurRadius = 5,
                        Opacity = 0.5
                    }
                };


                // 3. Main StackPanel to hold all content vertically
                StackPanel appointmentContent = new StackPanel();

                // --- Row 1: Title and Status Tag (using Grid for alignment) ---
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Fixed: Use GridUnitType.Star
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                headerGrid.Margin = new Thickness(0, 0, 0, 5);

                // Title (Purpose)
                TextBlock txtTitle = new TextBlock
                {
                    Text = purpose, // Using Purpose as the main title, similar to the doctor's name
                    FontWeight = FontWeights.DemiBold,
                    FontSize = 15,
                    Foreground = darkBlueBrush,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(txtTitle, 0);
                headerGrid.Children.Add(txtTitle);

                // Status Tag
                Border statusTag = CreateStatusTag(status);
                Grid.SetColumn(statusTag, 1);
                headerGrid.Children.Add(statusTag);

                appointmentContent.Children.Add(headerGrid);


                // --- Row 2: Date and Time Details (using StackPanel for inline elements) ---
                StackPanel dateTimePanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 8)
                };

                // Date Block (📅 2024-12-15)
                TextBlock txtDate = new TextBlock
                {
                    Text = $"📅 {date}",
                    FontSize = 12,
                    Foreground = Brushes.Gray,
                    Margin = new Thickness(0, 0, 15, 0) // Space between date and time
                };
                dateTimePanel.Children.Add(txtDate);

                // Time Block (🕒 10:00 AM)
                TextBlock txtTime = new TextBlock
                {
                    Text = $"🕒 {time}",
                    FontSize = 12,
                    Foreground = Brushes.Gray
                };
                dateTimePanel.Children.Add(txtTime);

                appointmentContent.Children.Add(dateTimePanel);


                // --- Row 3: Detail Subtext (Using Appointment ID for context) ---
                TextBlock txtDetail = new TextBlock
                {
                    Text = $"Appointment ID: {appointmentId}", // Or use a static description if a subtext column isn't available
                    FontSize = 13,
                    Foreground = darkBlueBrush,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                appointmentContent.Children.Add(txtDetail);


                // 4. Attach Content to Card Border
                cardBorder.Child = appointmentContent;


                // 5. Context Menu Functionality (Right-Click)
                string currentAppointmentId = appointmentId;

                cardBorder.MouseRightButtonDown += (s, e) =>
                {
                    ContextMenu contextMenu = new ContextMenu();

                    // Only allow update/cancel if the status is not already completed/cancelled
                    if (!status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase) && !status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                    {
                        MenuItem updateItem = new MenuItem { Header = "Update Appointment" };
                        updateItem.Click += (s2, e2) =>
                        {
                            // Create and show the Update Appointment Window
                            UpdateAppointment updateAppointmentWindow = new UpdateAppointment(
                                currentAppointmentId,
                                date,
                                time,
                                purpose
                            );
                            bool? result = updateAppointmentWindow.ShowDialog();
                            if (result == true)
                            {
                                displayAppointment(SQL);
                            }
                        };
                        contextMenu.Items.Add(updateItem);

                        MenuItem deleteItem = new MenuItem { Header = "Cancel Appointment" };
                        deleteItem.Click += (s3, e3) =>
                        {
                            cancelAppointment(currentAppointmentId);
                        };
                        contextMenu.Items.Add(deleteItem);
                    }
                    else
                    {
                        // Add a disabled item if actions aren't allowed
                        MenuItem disabledItem = new MenuItem { Header = $"Actions restricted (Status: {status})", IsEnabled = false };
                        contextMenu.Items.Add(disabledItem);
                    }


                    cardBorder.ContextMenu = contextMenu;
                    contextMenu.IsOpen = true;
                    e.Handled = true; // Mark the event as handled to prevent propagation
                };

                AppointmentStackPanel.Children.Add(cardBorder);
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
                displayAppointment(SQL);
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
    }
}
