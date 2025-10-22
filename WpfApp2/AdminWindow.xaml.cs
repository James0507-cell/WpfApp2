using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YourNamespace;

namespace WpfApp2
{
    public partial class AdminWindow : Window
    {
        String SQL = "";
        Admin admin = new Admin();
        String username = MainWindow.Username;
        StudentManagement studentManagement = new StudentManagement();
        int id;

        public AdminWindow()
        {
            InitializeComponent();
        }
        public void setId(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = admin.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            displayMedicineInv("select * from medicine_info");
            displayAppointments("SELECT * FROM appointments");
            displayMedicineRequest("SELECT * FROM medicinerequests");
            displayActivity("SELECT * FROM admin_activity_log ORDER BY activity_date DESC");
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            studentManagement.Show();
            this.Hide();
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
                // 1. Instantiate the custom dialog
                RejectionReasonDialog dialog = new RejectionReasonDialog();

                // 2. Show the dialog and check if the user clicked "Reject"
                bool? dialogResult = dialog.ShowDialog();

                // 3. If DialogResult is true (meaning the user entered a reason and clicked "Reject")
                if (dialogResult == true)
                {
                    string rejectionReason = dialog.RejectionReason;

                    // 4. Pass the appointment ID AND the rejection reason to the rejection logic
                    rejectAppointment(appointmentID, rejectionReason);
                }
                // If dialogResult is false or null, the user clicked "Cancel" or closed the dialog, 
                // and no action is taken.
            }
        }


        public void displayAppointments(String querry)
        {

            DataTable dt = new DataTable();

            dt = admin.displayRecords(querry);

            int n = dt.Rows.Count;
            AppointmentStackPanel.Children.Clear();

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

            for (int i = 0; i < n; i++)
            {
                String appointmentID = dt.Rows[i][0].ToString();
                String patientID = dt.Rows[i][1].ToString();
                String username = dt.Rows[i][2].ToString();
                String studentId = dt.Rows[i][3].ToString();
                String date = dt.Rows[i][4].ToString();
                String time = dt.Rows[i][5].ToString();
                String email = dt.Rows[i][6].ToString();
                String phone = dt.Rows[i][7].ToString();
                String purpose = dt.Rows[i][8].ToString();
                String allergies = dt.Rows[i][9].ToString();
                String medication = dt.Rows[i][10].ToString();
                String previousVisit = dt.Rows[i][11].ToString();
                String ecn = dt.Rows[i][12].ToString();
                String ecp = dt.Rows[i][13].ToString();
                String status = dt.Rows[i][14].ToString();
                String symptoms = dt.Rows[i][16].ToString();



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
                leftPanel.Children.Add(CreateDetailBlock("Student ID", studentId));
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
                AppointmentStackPanel.Children.Add(cardBorder);
            }
        }
        public void approveAppointment(String appointmentID)
        {
            String querry = $"UPDATE appointments SET status = 'Approved' WHERE appointment_id = {appointmentID}";
            admin.sqlManager(querry);
            querry = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{username}', 'Appointment Approved', 'Approved appointment ID {appointmentID}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            admin.sqlManager(querry);
            displayAppointments("SELECT * FROM appointments");
        }

        public void rejectAppointment(String appointmentID, String reason)
        {
            String querry = $"UPDATE appointments SET status = 'Rejected', reason = '{reason}' WHERE appointment_id = {appointmentID}";
            admin.sqlManager(querry);
            displayAppointments("SELECT * FROM appointments");
            querry = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{username}', 'Appointment Rejected', 'Rejected appointment ID {appointmentID}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            admin.sqlManager(querry);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            int totalStudents = admin.GetActiveStudentCount();
            lblActiveStatus.Content = totalStudents;

            int totalmedicinereq = admin.GetMedicineStatusCount();
            lblMedicine.Content = totalmedicinereq;

            int totalAppoinment = admin.GetAppointmenCount();
            lblPending.Content = totalAppoinment;

            int totalLowStock = admin.getMedicineCount();
            lblLowStack.Content = totalLowStock;

            setId(username);


            displayAppointments("SELECT * FROM appointments");
            displayMedicineRequest("SELECT * FROM medicinerequests");
            displayMedicineInv("select * from medicine_info");
            displayActivity("SELECT * FROM admin_activity_log ORDER BY activity_date DESC");
        }

        private void txtSearchIDapp_TextChanged(object sender, TextChangedEventArgs e)
        {

            //if (txtSearchIDapp.Text == "Search appointment ID..." || string.IsNullOrWhiteSpace(txtSearch.Text))
            //{
            //    displayAppointments("SELECT * FROM appointments");
            //}
            //else
            //{
            //    displayAppointments($"SELECT * FROM appointments WHERE appointment_id LIKE '%{txtSearchIDapp.Text}%'");

            //}
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStatus.SelectedIndex == 0)
            {
                displayAppointments("SELECT * FROM appointments");

            }
            else if (cmbStatus.SelectedIndex == 1)
            {
                displayAppointments("select * from appointments where status = 'Pending'");

            }
            else if (cmbStatus.SelectedIndex == 2)
            {
                displayAppointments("select * from appointments where status = 'Approved'");

            }
            else if (cmbStatus.SelectedIndex == 3)
            {
                displayAppointments("select * from appointments where status = 'Rejected'");

            }
        }
        private TextBlock CreateDetailBlock(string label, string value, FontWeight weight = default)
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

        public void displayMedicineRequest(String strquerry)
        {
            // Assuming StackPanelMedicineReuqests is correctly named in your XAML
            StackPanel targetStackPanel = this.StackPanelMedicineReuqests;
            DataTable dt = new DataTable();
            dt = admin.displayRecords(strquerry);
            int n = dt.Rows.Count;
            targetStackPanel.Children.Clear();

            for (int i = 0; i < n; i++)
            {
                // Data from DataTable
                String requestID = dt.Rows[i][0].ToString();
                String userID = dt.Rows[i][1].ToString();
                String medicineName = dt.Rows[i][2].ToString();
                String reason = dt.Rows[i][3].ToString();
                String quantity = dt.Rows[i][4].ToString();
                String status = dt.Rows[i][5].ToString();
                String requestDate = dt.Rows[i][6].ToString();
                String approvedDate = dt.Rows[i][7].ToString();

                // 1. Card Container (Border)
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
                    Tag = requestID // Set the request ID as the tag
                };

                // Add the right-click handler
                cardBorder.MouseRightButtonUp += CardRequest_MouseRightButtonUp;


                // 2. Main Layout Grid (Header | Separator | Details)
                Grid mainLayoutGrid = new Grid();
                mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                // --- Row 0: Header Grid (Medicine Name, Quantity | Status Tag) ---
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                StackPanel primaryDetails = new StackPanel { Orientation = Orientation.Vertical };

                // Medicine Name
                TextBlock txtMedicineName = new TextBlock
                {
                    Text = medicineName,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")),
                    Margin = new Thickness(0, 0, 0, 4)
                };
                primaryDetails.Children.Add(txtMedicineName);

                // Quantity
                TextBlock txtQuantity = new TextBlock
                {
                    Text = $"Quantity: {quantity} pcs",
                    FontSize = 14,
                    FontWeight = FontWeights.DemiBold,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4D7399")),
                    Margin = new Thickness(0, 0, 0, 8)
                };
                primaryDetails.Children.Add(txtQuantity);

                Grid.SetColumn(primaryDetails, 0);
                headerGrid.Children.Add(primaryDetails);

                // Status Tag (Replicated from displayAppointments)
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
                    case "approved":
                        statusColor = (Color)ColorConverter.ConvertFromString("#FF4CAF50");
                        break;
                    case "pending":
                        statusColor = (Color)ColorConverter.ConvertFromString("#FFFFC107");
                        break;
                    case "rejected":
                    case "denied":
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

                // --- Row 1: Separator ---
                Separator separator = new Separator
                {
                    Margin = new Thickness(0, 8, 0, 8),
                    Foreground = new SolidColorBrush(Colors.LightGray)
                };
                Grid.SetRow(separator, 1);
                mainLayoutGrid.Children.Add(separator);

                // --- Row 2: Details Grid (Two Columns) ---
                Grid detailsGrid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    }
                };

                StackPanel leftPanel = new StackPanel { Margin = new Thickness(0, 0, 10, 0) };
                leftPanel.Children.Add(CreateDetailBlock("Reason", reason, FontWeights.DemiBold));
                leftPanel.Children.Add(CreateDetailBlock("Requested By User ID", userID));
                leftPanel.Children.Add(CreateDetailBlock("Request ID", requestID));

                Grid.SetColumn(leftPanel, 0);
                detailsGrid.Children.Add(leftPanel);

                StackPanel rightPanel = new StackPanel { Margin = new Thickness(10, 0, 0, 0) };
                rightPanel.Children.Add(CreateDetailBlock("Request Date", requestDate, FontWeights.DemiBold));

                if (status.ToLower() != "pending" && !string.IsNullOrWhiteSpace(approvedDate))
                {
                    rightPanel.Children.Add(CreateDetailBlock("Handled Date", approvedDate));
                }
                rightPanel.Children.Add(CreateDetailBlock("Status", status));


                Grid.SetColumn(rightPanel, 1);
                detailsGrid.Children.Add(rightPanel);

                Grid.SetRow(detailsGrid, 2);
                mainLayoutGrid.Children.Add(detailsGrid);


                cardBorder.Child = mainLayoutGrid;
                targetStackPanel.Children.Add(cardBorder);
            }
        }
        private void CardRequest_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border cardBorder && cardBorder.Tag is string requestID)
            {


                ContextMenu contextMenu = new ContextMenu
                {
                    FontSize = 14,
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")),
                    BorderThickness = new Thickness(2),
                    Effect = new System.Windows.Media.Effects.DropShadowEffect
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
                    Header = "✅ Approve Request",
                    Tag = requestID,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4CAF50")),
                    FontWeight = FontWeights.DemiBold,
                    BorderThickness = new Thickness(0)
                };
                approveItem.Click += ApproveMedicineRequest_Click;

                MenuItem rejectItem = new MenuItem
                {
                    Header = "❌ Reject Request",
                    Tag = requestID,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF44336")),
                    FontWeight = FontWeights.DemiBold,
                    BorderThickness = new Thickness(0)
                };
                rejectItem.Click += RejectMedicineRequest_Click;

                contextMenu.Items.Add(approveItem);
                contextMenu.Items.Add(rejectItem);

                contextMenu.PlacementTarget = cardBorder;
                contextMenu.IsOpen = true;

                e.Handled = true;
            }
        }

        private void ApproveMedicineRequest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string requestID)
            {
                approveMedicineRequest(requestID);
                SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{username}', 'Medicine Request Approved', 'Approved medicine request ID {requestID}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                admin.sqlManager(SQL);
            }
        }

        private void RejectMedicineRequest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string requestID)
            {
                RejectionReasonDialog dialog = new RejectionReasonDialog();

                bool? dialogResult = dialog.ShowDialog();

                if (dialogResult == true)
                {
                    string rejectionReason = dialog.RejectionReason;

                    rejectMedicineRequest(requestID, rejectionReason);


                    SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                          $"VALUES ({id}, '{username}', 'Medicine Request Rejected', 'Rejected medicine request ID {requestID}. Reason: {rejectionReason}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                    admin.sqlManager(SQL);
                }
            }
        }

        public void approveMedicineRequest(String requestID)
        {
            String querry = $"UPDATE medicinerequests SET status = 'Approved', approved_date = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' WHERE request_id = {requestID}";
            admin.sqlManager(querry);
            displayMedicineRequest("SELECT * FROM medicinerequests");
        }

        public void rejectMedicineRequest(String requestID, String reason)
        {
            String querry = $"UPDATE medicinerequests SET status = 'Rejected', reject_reason = '{reason}', approved_date = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' WHERE request_id = {requestID}";
            admin.sqlManager(querry);
            displayMedicineRequest("SELECT * FROM medicinerequests");
        }
        public void displayMedicineInv(String query)
        {
            DataTable dt = new DataTable();
            dt = admin.displayRecords(query);

            // Clearing the children of the XAML-defined StackPanel
            wrapPanelInventory.Children.Clear();
            int n = dt.Rows.Count;

            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush buttonBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF014BFF"));
            Brush lowStockRedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFA0A0"));

            for (int i = 0; i < n; i++)
            {
                // Extract data
                String medicineId = dt.Rows[i][0].ToString();
                String medicineName = dt.Rows[i][1].ToString();
                String dosage = dt.Rows[i][2].ToString();
                String genericName = dt.Rows[i][3].ToString();
                String description = dt.Rows[i][4].ToString();

                // Fetch quantity
                DataTable quantityDt = admin.displayRecords(
                    "SELECT amount, inventory_id FROM medicineinventory WHERE medicine_id = '" + medicineId + "'");
                int quant = Convert.ToInt32(quantityDt.Rows[0][0]);
                String inventoryId = quantityDt.Rows[0][1].ToString();

                // Default Border Setup
                Border cardBorder = new Border
                {
                    Width = 280,
                    Height = 300,
                    Margin = new Thickness(10),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(15),
                    HorizontalAlignment = HorizontalAlignment.Center,
                };

                // Conditional Low Stock Highlight
                if (quant < 20)
                {
                    cardBorder.BorderBrush = lowStockRedBrush;
                    cardBorder.BorderThickness = new Thickness(3);
                }

                DockPanel medicineContent = new DockPanel();

                // --- Button Wrapper (Bottom Dock) ---
                Border buttonWrapper = new Border
                {
                    CornerRadius = new CornerRadius(6),
                    Margin = new Thickness(0, 15, 0, 0),
                    Background = buttonBlueBrush,
                };
                DockPanel.SetDock(buttonWrapper, Dock.Bottom);

                Button updateButton = new Button
                {
                    Content = "Update Quantity",
                    Background = Brushes.Transparent,
                    Foreground = Brushes.White,
                    BorderThickness = new Thickness(0),
                    FontWeight = FontWeights.Bold,
                    FontSize = 13,
                    Padding = new Thickness(10),
                    Cursor = Cursors.Hand,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                updateButton.Tag = new { MedicineId = medicineId, InventoryId = inventoryId };
                updateButton.Click += UpdateMedicineInventory_Click;

                buttonWrapper.Child = updateButton;
                medicineContent.Children.Add(buttonWrapper);

                // --- Main Content StackPanel (Top Dock) ---
                StackPanel mainContentStack = new StackPanel();

                // Header Grid setup
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                headerGrid.Margin = new Thickness(0, 0, 0, 10);

                // Icon
                Border iconWrapper = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(255, 230, 230, 255)),
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(5),
                    Width = 36,
                    Height = 36,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                TextBlock iconBlock = new TextBlock
                {
                    Text = "📦",
                    FontSize = 18,
                    Foreground = darkBlueBrush,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                iconWrapper.Child = iconBlock;
                Grid.SetColumn(iconWrapper, 0);
                headerGrid.Children.Add(iconWrapper);

                // Name and Dosage
                StackPanel nameDosagePanel = new StackPanel
                {
                    Margin = new Thickness(8, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };
                TextBlock txtName = new TextBlock
                {
                    Text = medicineName,
                    FontWeight = FontWeights.ExtraBold,
                    FontSize = 16,
                    Foreground = darkBlueBrush,
                    TextWrapping = TextWrapping.Wrap
                };
                TextBlock txtDosage = new TextBlock
                {
                    Text = $"{dosage} Tablet",
                    FontWeight = FontWeights.Normal,
                    FontSize = 12,
                    Foreground = Brushes.Gray
                };
                nameDosagePanel.Children.Add(txtName);
                nameDosagePanel.Children.Add(txtDosage);
                Grid.SetColumn(nameDosagePanel, 1);
                headerGrid.Children.Add(nameDosagePanel);

                // Inventory ID
                TextBlock txtInvId = new TextBlock
                {
                    Text = $"ID: {inventoryId}",
                    FontWeight = FontWeights.Normal,
                    FontSize = 10,
                    Foreground = Brushes.Gray,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 5, 0)
                };

                // Availability Tag
                Border availabilityTag = CreateAvailabilityTag(quant);

                // Stack them vertically in column 2
                StackPanel rightPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right
                };
                rightPanel.Children.Add(txtInvId);
                rightPanel.Children.Add(availabilityTag);
                Grid.SetColumn(rightPanel, 2);
                headerGrid.Children.Add(rightPanel);

                mainContentStack.Children.Add(headerGrid);

                // Separator Line
                Rectangle separator = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    Height = 1,
                    Margin = new Thickness(0, 0, 0, 10)
                };
                mainContentStack.Children.Add(separator);

                // Generic Name and Description Detail Blocks
                mainContentStack.Children.Add(CreateDetailBlock("Generic Name", genericName, darkBlueBrush));
                StackPanel descriptionBlock = CreateDetailBlock("Description", description, darkBlueBrush);
                descriptionBlock.Margin = new Thickness(0, 8, 0, 0);
                mainContentStack.Children.Add(descriptionBlock);

                // Final assembly
                medicineContent.Children.Add(mainContentStack);
                cardBorder.Child = medicineContent;

                // ADDING THE CARD TO THE XAML-DEFINED STACKPANEL
                wrapPanelInventory.Children.Add(cardBorder);
            }

        }

        // --- Helper Methods Required for the Above Code ---

        private Border CreateAvailabilityTag(int quant)
        {
            string text;
            Color backgroundColor;
            Color foregroundColor = Colors.White;

            if (quant > 10)
            {
                text = $"QTY: {quant}";
                backgroundColor = (Color)ColorConverter.ConvertFromString("#FF00994D"); // Greenish
            }
            else if (quant > 0)
            {
                text = $"QTY: {quant}";
                backgroundColor = (Color)ColorConverter.ConvertFromString("#FFFFD700"); // Yellow/Gold
                foregroundColor = Colors.Black;
            }
            else
            {
                text = "Out of Stock";
                backgroundColor = (Color)ColorConverter.ConvertFromString("#FFB0B0B0"); // Gray
            }

            return new Border
            {
                Background = new SolidColorBrush(backgroundColor),
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(6, 3, 6, 3),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Child = new TextBlock
                {
                    Text = text,
                    FontSize = 10,
                    Foreground = new SolidColorBrush(foregroundColor),
                    FontWeight = FontWeights.Bold
                }
            };
        }

        private StackPanel CreateDetailBlock(string label, string value, Brush foregroundBrush)
        {
            StackPanel panel = new StackPanel();

            TextBlock labelBlock = new TextBlock
            {
                Text = label,
                FontWeight = FontWeights.SemiBold,
                FontSize = 11,
                Foreground = Brushes.Gray,
                Margin = new Thickness(0, 0, 0, 2)
            };

            TextBlock valueBlock = new TextBlock
            {
                Text = value,
                FontWeight = FontWeights.Medium,
                FontSize = 14,
                Foreground = foregroundBrush,
                TextWrapping = TextWrapping.Wrap
            };

            panel.Children.Add(labelBlock);
            panel.Children.Add(valueBlock);

            return panel;
        }
        private void UpdateMedicineInventory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is not null)
            {
                dynamic tagData = clickedButton.Tag;
                string medId = tagData.MedicineId;
                string inventoryId = tagData.InventoryId;

                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Enter the quantity to ADD to medicine '{medId}'",
                    "Add Inventory Stock",
                    "0");

                if (int.TryParse(input, out int amountToAdd) && amountToAdd > 0)
                {
                    string SQL = $"UPDATE medicineinventory SET amount = amount + {amountToAdd} WHERE inventory_id = {inventoryId}";
                    admin.sqlManager(SQL);

                    MessageBox.Show($"Successfully added {amountToAdd} units to inventory for ID {medId}.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);

                    // Log the update
                    SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                         $"VALUES ({id}, '{username}', 'Medicine Inventory Updated', 'Added {amountToAdd} units to medicine ID {medId}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                    admin.sqlManager(SQL);

                    // Refresh the display
                    displayMedicineInv("SELECT * FROM medicine_info");
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a positive number for the quantity.", "Input Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public void displayActivity(String SQL)
        {
            // --- 1. Data Fetching ---
            // Assuming 'userForm' is an instance that handles database calls
            StackPanelActivities.Children.Clear();
            DataTable dt = admin.displayRecords(SQL);

            // --- 2. UI Initialization ---
            // Clear the StackPanel where activities are displayed
            StackPanelActivities.Children.Clear();

            // Color definitions for consistent styling
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray);
            Brush lightBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xF7, 0xFA));
            Brush idTagBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0F0F0"));


            // --- 3. Iterate and Build UI Cards ---
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Extract all fields from the DataTable row
                string activityID = dt.Rows[i]["activity_id"].ToString();
                string username = dt.Rows[i]["username"].ToString();
                string type = dt.Rows[i]["activity_type"].ToString();
                string description = dt.Rows[i]["activity_desc"].ToString();
                string dateTime = dt.Rows[i]["activity_date"].ToString();

                // --- Activity Card Container (Border) ---
                Border cardBorder = new Border
                {
                    BorderBrush = Brushes.Transparent,
                    BorderThickness = new Thickness(1),
                    Background = lightBackground,
                    CornerRadius = new CornerRadius(5),
                    Margin = new Thickness(10, 4, 10, 4),
                    Padding = new Thickness(12, 8, 12, 8), // Increased padding for a better look
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };

                StackPanel activityContent = new StackPanel();


                // --- Header Grid (Username and Admin ID) ---
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // For Username
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // For ID Tag
                headerGrid.Margin = new Thickness(0, 0, 0, 4);

                // 1. Username (Bold and Primary)
                TextBlock txtUsername = new TextBlock
                {
                    Text = username,
                    FontWeight = FontWeights.Bold,
                    FontSize = 10,
                    Foreground = darkBlueBrush,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(txtUsername, 0);
                headerGrid.Children.Add(txtUsername);

                // 2. Admin ID Tag
                Border idTag = new Border
                {
                    Background = idTagBackground,
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(8, 2, 8, 2),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Child = new TextBlock
                    {
                        Text = $"ID: {activityID}",
                        Foreground = Brushes.Gray,
                        FontWeight = FontWeights.Normal,
                        FontSize = 9
                    }
                };
                Grid.SetColumn(idTag, 1);
                headerGrid.Children.Add(idTag);

                activityContent.Children.Add(headerGrid);

                // --- Activity Type (Secondary detail) ---
                TextBlock txtActivityType = new TextBlock
                {
                    Text = $"Action: {type}",
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 9,
                    Foreground = darkBlueBrush,
                    Margin = new Thickness(0, 0, 0, 2)
                };
                activityContent.Children.Add(txtActivityType);

                // --- Date Time ---
                TextBlock txtDateTime = new TextBlock
                {
                    Text = $"⌚ {dateTime}",
                    FontSize = 9,
                    Foreground = lightGrayBrush,
                    Margin = new Thickness(0, 0, 0, 6)
                };
                activityContent.Children.Add(txtDateTime);

                // --- Description ---
                TextBlock txtDescription = new TextBlock
                {
                    Text = description,
                    FontSize = 9,
                    Foreground = darkBlueBrush,
                    TextWrapping = TextWrapping.Wrap
                };
                activityContent.Children.Add(txtDescription);

                // Assign the content to the card border
                cardBorder.Child = activityContent;

                // --- 4. Add card to the main StackPanel ---
                StackPanelActivities.Children.Add(cardBorder);
            }

        }


        private void TabControl_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            int totalStudents = admin.GetActiveStudentCount();
            lblActiveStatus.Content = totalStudents;

            int totalmedicinereq = admin.GetMedicineStatusCount();
            lblMedicine.Content = totalmedicinereq;

            int totalAppoinment = admin.GetAppointmenCount();
            lblPending.Content = totalAppoinment;

            int totalLowStock = admin.getMedicineCount();
            lblLowStack.Content = totalLowStock;

            displayActivity("SELECT * FROM admin_activity_log ORDER BY activity_date DESC");
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If search box is empty or has placeholder text, show all records
            if (txtSearch.Text == "Search Inventory ID..." || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                displayMedicineInv("SELECT * FROM medicine_info");
            }
            else
            {
                // Otherwise, perform search
                string SQL = "SELECT mi.* FROM medicine_info mi " +
                             "JOIN medicineinventory miv ON mi.medicine_id = miv.medicine_id " +
                             "WHERE miv.inventory_id LIKE '%" + txtSearch.Text + "%'";
                displayMedicineInv(SQL);
            }
        }


        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search Inventory ID...")
                txtSearch.Text = "";

        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                txtSearch.Text = "Search Inventory ID...";

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbFilterInv.SelectedIndex == null)
            {
                displayMedicineInv("select * from medicine_info");
                
            }
            else if (cmbFilterInv.SelectedIndex == 0)
            {
                displayMedicineInv("SELECT mi.* FROM medicine_info mi " +
                                   "JOIN medicineinventory miv ON mi.medicine_id = miv.medicine_id " +
                                   "WHERE miv.amount < 20");
            }
            else if (cmbFilterInv.SelectedIndex == 1)
            {
                displayMedicineInv("SELECT mi.* FROM medicine_info mi " +
                                   "JOIN medicineinventory miv ON mi.medicine_id = miv.medicine_id " +
                                   "WHERE miv.amount >= 20");
            }
            else
            {
                displayMedicineInv("select * from medicine_info");
            }
        }

        private void txtSearchIDapp_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchIDapp.Text == "Search appointment ID...")
                txtSearchIDapp.Text = "";
        }

        private void txtSearchIDapp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchIDapp.Text))
                txtSearchIDapp.Text = "Search appointment ID...";
        }

        private void txtSearchIDapp_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (AppointmentStackPanel == null)
                return; // UI not ready yet

            if (txtSearchIDapp.Text == "Search appointment ID..." || string.IsNullOrWhiteSpace(txtSearchIDapp.Text))
            {
                displayAppointments("SELECT * FROM appointments");
            }
            else
            {
                displayAppointments($"SELECT * FROM appointments WHERE appointment_id LIKE '%{txtSearchIDapp.Text}%'");
            }
        }

        private void txtSearchMedID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchMedID.Text == "Search medicine request ID...")
                txtSearchMedID.Text = "";
        }

        private void txtSearchMedID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchMedID.Text))
                txtSearchMedID.Text = "Search medicine request ID...";
        }

        private void txtSearchMedID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StackPanelMedicineReuqests == null)
                return; // UI not ready yet
            if (txtSearchMedID.Text == "Search medicine request ID..." || string.IsNullOrWhiteSpace(txtSearchMedID.Text))
            {
                displayMedicineRequest("SELECT * FROM medicinerequests");
            }
            else
            {
                displayMedicineRequest($"SELECT * FROM medicinerequests WHERE request_id LIKE '%{txtSearchMedID.Text}%'");
            }
        }

        private void cboMedFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMedFilter.SelectedIndex == null)
            {
                displayMedicineRequest("SELECT * FROM medicinerequests");
            }
            else if (cboMedFilter.SelectedIndex == 0)
            {
                displayMedicineRequest("SELECT * FROM medicinerequests");
            }
            else if (cboMedFilter.SelectedIndex == 1)
            {
                displayMedicineRequest("SELECT * FROM medicinerequests WHERE status = 'Pending'");
            }
            else if (cboMedFilter.SelectedIndex == 2)
            {
                displayMedicineRequest("SELECT * FROM medicinerequests WHERE status = 'Approved'");
            }
            else
            {
                displayMedicineRequest("SELECT * FROM medicinerequests Where status = 'Rejected'");
            }
        }

        private void cboActivityFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboActivityFilter.SelectedItem is ComboBoxItem selectedItem)
            {
                string filter = selectedItem.Content.ToString();
                string query;

                if (filter == "All")
                {
                    query = "SELECT * FROM admin_activity_log ORDER BY activity_date DESC";
                }
                else
                {
                    query = $@"
                SELECT * 
                FROM admin_activity_log 
                WHERE TRIM(activity_type) = '{filter}' 
                ORDER BY activity_date DESC";
                }

                displayActivity(query); 
            }
        }





        private void txtSearchActivity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (StackPanelActivities == null)
                return; // UI not ready yet
            if (txtSearchActivity.Text == "Search activity ID..." || string.IsNullOrWhiteSpace(txtSearchActivity.Text))
            {
                displayActivity("SELECT * FROM admin_activity_log ORDER BY activity_date DESC");
            }
            else
            {
                displayActivity($"SELECT * FROM admin_activity_log where activity_id like '%{txtSearchActivity.Text}%'");
            }
        }

        private void txtSearchActivity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchActivity.Text))
                txtSearchActivity.Text = "Search activity ID...";

        }

        private void txtSearchActivity_GotFocus(object sender, RoutedEventArgs e)
        {

            if (txtSearchActivity.Text == "Search activity ID...")
                txtSearchActivity.Text = "";
        }
    }
}

