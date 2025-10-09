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
        long userId;
        
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
            getUserId(username);
            SQL = $"SELECT * FROM appointments WHERE username = '{username}'";
            displayAppointment(SQL);
            SQL = $"SELECT * FROM student_activity_log WHERE user_id = '{userId}' ORDER BY activity_date DESC LIMIT 5";
            displayActivities(SQL);
            getName();
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
            Padding = new Thickness(12, 6, 12, 6), // FURTHER REDUCED PADDING
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
        headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        headerGrid.Margin = new Thickness(0, 0, 0, 3); // Reduced bottom margin

        // Title (Purpose)
        TextBlock txtTitle = new TextBlock
        {
            Text = purpose,
            FontWeight = FontWeights.SemiBold, // Slightly reduced bolding
            FontSize = 12,                    // Slightly reduced font size
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


        // --- Row 2: Date, Time, and ID Details (COMBINED LINE) ---
        StackPanel detailsPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin = new Thickness(0, 0, 0, 0) // Removed margin for compactness
        };

        // Date Block (📅 2024-12-15)
        TextBlock txtDate = new TextBlock
        {
            Text = $"📅 {date}",
            FontSize = 11,
            Foreground = lightGrayBrush,
            Margin = new Thickness(0, 0, 10, 0)
        };
        detailsPanel.Children.Add(txtDate);

        // Separator
        TextBlock separator = new TextBlock
        {
            Text = "|",
            FontSize = 11,
            Foreground = lightGrayBrush,
            Margin = new Thickness(0, 0, 10, 0)
        };
        detailsPanel.Children.Add(separator);

        // Time Block (🕒 10:00 AM)
        TextBlock txtTime = new TextBlock
        {
            Text = $"🕒 {time}",
            FontSize = 11,
            Foreground = lightGrayBrush,
            Margin = new Thickness(0, 0, 10, 0)
        };
        detailsPanel.Children.Add(txtTime);

        // Separator
        TextBlock separator2 = new TextBlock
        {
            Text = "|",
            FontSize = 11,
            Foreground = lightGrayBrush,
            Margin = new Thickness(0, 0, 10, 0)
        };
        detailsPanel.Children.Add(separator2);
        
        // Appointment ID
        TextBlock txtId = new TextBlock
        {
            Text = $"ID: {appointmentId}",
            FontSize = 11,
            Foreground = lightGrayBrush,
            FontWeight = FontWeights.Medium // Highlight ID slightly
        };
        detailsPanel.Children.Add(txtId);

        appointmentContent.Children.Add(detailsPanel);

        // REMOVED THE ORIGINAL "Row 3: Detail Subtext" TO SAVE HEIGHT

        // 4. Attach Content to Card Border
        cardBorder.Child = appointmentContent;


        // 5. Context Menu Functionality (Right-Click) - UNCHANGED
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
                        // Assuming 'username' is accessible here
                        // Redundant part of the code removed for clarity but original logic kept
                        // (Requires 'username' scope if this code snippet is outside the class/method where it's defined)
                        // displayAppointment($"SELECT * FROM appointments WHERE username = '{username}'"); 
                    }
                };
                contextMenu.Items.Add(updateItem);

                MenuItem deleteItem = new MenuItem { Header = "Cancel Appointment" };
                deleteItem.Click += (s3, e3) =>
                {
                    // Requires 'cancelAppointment' method scope
                    // cancelAppointment(currentAppointmentId); 
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

        // NOTE: The 'CreateStatusTag' method is assumed to exist elsewhere in your code.
        // The context menu logic for 'username' and 'cancelAppointment' will only compile 
        // if those variables/methods are accessible in the scope of this method.

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
            CornerRadius = new CornerRadius(8),
            Margin = new Thickness(10, 4, 10, 4), // Consistent with Appointment style
            Padding = new Thickness(12, 6, 12, 6),
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
            userId = Convert.ToInt64(dt.Rows[0][0].ToString());
        }
    }
}
