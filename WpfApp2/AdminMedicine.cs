using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
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
    internal class AdminMedicine
    {
        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;
        String username = MainWindow.Username;
        int id;

        private string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";


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
                // 1. Fetch details (medicine name and quantity) from the request
                DataTable requestDetails = displayRecords(
                    $"SELECT medicine_name, quantity FROM medicinerequests WHERE request_id = '{requestID}'");

                if (requestDetails.Rows.Count > 0)
                {
                    string medicineName = requestDetails.Rows[0]["medicine_name"].ToString();
                    // Convert quantity to an integer for safe SQL execution later
                    if (int.TryParse(requestDetails.Rows[0]["quantity"].ToString(), out int quantityRequested))
                    {
                        // 2. Fetch inventory ID for the medicine
                        DataTable inventoryidDt = displayRecords(
                            $"SELECT inventory_id FROM medicineinventory WHERE medicine_name = '{medicineName}'");

                        if (inventoryidDt.Rows.Count > 0)
                        {
                            int inventoryID = Convert.ToInt32(inventoryidDt.Rows[0]["inventory_id"]);

                            // 3. Update the request status
                            approveMedicineRequest(requestID);

                            // 4. Log the approval activity
                            string logSQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                                            $"VALUES ({id}, '{username}', 'Medicine Request Approved', 'Approved medicine request ID {requestID}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                           sqlManager(logSQL);

                            // 5. CORRECTED SQL: Update the inventory amount
                            // The correct SQL syntax is SET column_name = value
                            // Also, 'ammount' is likely a typo for 'amount' or 'quantity' in your database. I'm assuming 'amount'.
                            // Use the fetched quantityRequested variable and the fetched inventoryID.
                            string updateInventorySQL = $"UPDATE medicineinventory SET amount = amount - {quantityRequested} WHERE inventory_id = {inventoryID}";

                            // Note: If your column is actually named 'ammount', change 'amount' to 'ammount'
                            // string updateInventorySQL = $"UPDATE medicineinventory SET ammount = ammount - {quantityRequested} WHERE inventory_id = {inventoryID}";

                            sqlManager(updateInventorySQL);
                        }
                        else
                        {
                            // Handle case where medicine is not found in inventory
                            System.Windows.MessageBox.Show($"Error: Medicine '{medicineName}' not found in inventory.");
                        }
                    }
                    else
                    {
                        // Handle case where quantity is not a valid number
                        System.Windows.MessageBox.Show($"Error: Invalid quantity for request ID {requestID}.");
                    }
                }
                else
                {
                    // Handle case where request ID is not found
                    System.Windows.MessageBox.Show($"Error: Medicine request ID {requestID} not found.");
                }
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


                    String SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                          $"VALUES ({id}, '{username}', 'Medicine Request Rejected', 'Rejected medicine request ID {requestID}. Reason: {rejectionReason}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                    sqlManager(SQL);
                }
            }
        }
        public void approveMedicineRequest(String requestID)
        {
            String querry = $"UPDATE medicinerequests SET status = 'Approved', approved_date = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' WHERE request_id = {requestID}";
            sqlManager(querry);
            TriggerAppointmentActivityPanelReload();
        }

        public void rejectMedicineRequest(String requestID, String reason)
        {
            String querry = $"UPDATE medicinerequests SET status = 'Rejected', reject_reason = '{reason}', approved_date = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' WHERE request_id = {requestID}";
            sqlManager(querry);
            TriggerAppointmentActivityPanelReload();
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
        public Border medicineRequestPanel (String requestID, String userID, String medicineName, String reason, String quantity, String status, String requestDate, String approvedDate, int inventoryID)
        {
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
                Tag = requestID
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

            return cardBorder;

        }
        public void TriggerAppointmentActivityPanelReload()
        {
            AdminWindow activeAdminWindow = Application.Current.Windows
                .OfType<AdminWindow>()
                .SingleOrDefault(x => x.IsActive || x.IsVisible);

            if (activeAdminWindow != null)
            {
                activeAdminWindow.displayMedicineRequest("Select * from medicinerequests");
                activeAdminWindow.displayActivity("Select * from admin_activity_log");
            }
            else
            {
                MessageBox.Show("Could not find the User Dashboard to refresh.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public class MedicineRequestData
        {
            public string RequestID { get; set; }
            public int InventoryID { get; set; }
            public int Quantity { get; set; }
        }

    }
}
