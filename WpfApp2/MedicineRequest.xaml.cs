using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Effects; 

namespace WpfApp2
{
    
    public partial class MedicineRequest : Window
    {
        public string username = MainWindow.Username;
        Users user = new Users();
        String SQL = "";
        int userId = 0;
        public MedicineRequest()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == "Search medicines..." || string.IsNullOrWhiteSpace(txtSearch.Text))
                return;

            string SQL = "SELECT * FROM medicine_info WHERE medicine_name LIKE '%" + txtSearch.Text + "%' OR generic_name LIKE '%" + txtSearch.Text + "%'";
            displayMedicine(SQL);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search medicines...")
                txtSearch.Text = "";
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                txtSearch.Text = "Search medicines...";
        }

        
        private Border CreateAvailabilityTag(int stock)
        {
            string stockText = $"{stock} available";
            Color bgColor = stock > 50 ? (Color)ColorConverter.ConvertFromString("#E8F5E9") : // Light Green (High Stock)
                             stock > 10 ? (Color)ColorConverter.ConvertFromString("#FFFDE7") : // Light Yellow (Medium Stock)
                             (Color)ColorConverter.ConvertFromString("#FFEBEE"); // Light Red (Low Stock)

            Brush fgColor = stock > 50 ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#388E3C")) : // Dark Green
                              stock > 10 ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107")) : // Amber/Dark Yellow
                              new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D32F2F")); // Dark Red

            return new Border
            {
                Background = new SolidColorBrush(bgColor),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(8, 2, 8, 2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Child = new TextBlock
                {
                    Text = stockText,
                    Foreground = fgColor,
                    FontWeight = FontWeights.Bold,
                    FontSize = 10
                }
            };
        }


        // Helper Method Definition (should be outside the displayMedicine method)
        private StackPanel CreateDetailBlock(string title, string content, Brush colorBrush)
        {
            StackPanel panel = new StackPanel(); // <--- This is the object being returned

            TextBlock titleBlock = new TextBlock
            {
                Text = title,
                FontWeight = FontWeights.SemiBold,
                FontSize = 13,
                Foreground = Brushes.Black,
                Margin = new Thickness(0, 0, 0, 2)
            };
            panel.Children.Add(titleBlock);

            TextBlock contentBlock = new TextBlock
            {
                Text = content,
                FontWeight = FontWeights.Normal,
                FontSize = 13,
                Foreground = Brushes.DarkGray,
                TextWrapping = TextWrapping.Wrap
            };
            panel.Children.Add(contentBlock);

            return panel; // <--- The method must explicitly return StackPanel
        }

        public void displayMedicine(String querry)
        {
            DataTable dt = new DataTable();
            dt = user.displayRecords(querry);
            StackPanelMedicines.Children.Clear();
            int n = dt.Rows.Count;

            // Define the color for consistency (Dark Blue/Red)
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush buttonRedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD0021B"));

            for (int i = 0; i < n; i++)
            {
                // Data Retrieval (Simplified for presentation)
                String medicineId = dt.Rows[i][0].ToString();
                String medicineName = dt.Rows[i][1].ToString();
                String dosage = dt.Rows[i][2].ToString();
                String genericName = dt.Rows[i][3].ToString();
                String description = dt.Rows[i][4].ToString();
                DataTable quantity = user.displayRecords("select amount from medicineinventory where medicine_id = '" + medicineId + "'");
                int quant = Convert.ToInt32(quantity.Rows[0][0]);

                // 1. Create the Card Container (Border)
                Border cardBorder = new Border
                {
                    Width = 280,
                    Height = 300, // Fixed height is essential for DockPanel to work
                    Margin = new Thickness(10),

                    // Styling remains the same...
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(15),
                    Effect = new DropShadowEffect
                    {
                        Color = Colors.LightGray,
                        Direction = 315,
                        ShadowDepth = 2,
                        BlurRadius = 5,
                        Opacity = 0.5
                    }
                };

                // *** KEY FIX: Use DockPanel instead of StackPanel ***
                DockPanel medicineContent = new DockPanel();

                // --- Row 3: Action Button (Docked to the Bottom) ---
                Border buttonWrapper = new Border
                {
                    CornerRadius = new CornerRadius(6),
                    Margin = new Thickness(0, 15, 0, 0), // Top margin separates it from content
                    Background = buttonRedBrush,
                };

                // *** DOCKING THE BUTTON TO THE BOTTOM ***
                DockPanel.SetDock(buttonWrapper, Dock.Bottom);

                Button requestButton = new Button
                {
                    Content = "Request Medicine",
                    Background = Brushes.Transparent,
                    Foreground = Brushes.White,
                    BorderThickness = new Thickness(0),
                    FontWeight = FontWeights.Bold,
                    FontSize = 13,
                    Padding = new Thickness(10),
                    Cursor = Cursors.Hand,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                requestButton.Click += (s, e) => RequestMedicine_Click(medicineId, medicineName, dosage, genericName);

                buttonWrapper.Child = requestButton;

                // Add the button wrapper FIRST, so it takes the bottom slot
                medicineContent.Children.Add(buttonWrapper);

                // Handle out of stock state
                if (quant == 0)
                {
                    requestButton.IsEnabled = false;
                    requestButton.Content = "Out of Stock";
                    buttonWrapper.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB0B0B0"));
                }

                // --- Row 1 & 2: Content (Header, Separator, Details) ---
                // All remaining content must be added inside a StackPanel so it flows
                // correctly and takes up the remaining space left by the DockPanel.
                StackPanel mainContentStack = new StackPanel();

                // Header Grid (existing code)
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                headerGrid.Margin = new Thickness(0, 0, 0, 10);

                // Icon 
                Border iconWrapper = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(255, 255, 230, 230)),
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(5),
                    Width = 36,
                    Height = 36,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                TextBlock iconBlock = new TextBlock
                {
                    Text = "💊",
                    FontSize = 18,
                    Foreground = Brushes.Red,
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
                nameDosagePanel.Children.Add(txtName);

                TextBlock txtDosage = new TextBlock
                {
                    Text = $"{dosage} Tablet",
                    FontWeight = FontWeights.Normal,
                    FontSize = 12,
                    Foreground = Brushes.Gray
                };
                nameDosagePanel.Children.Add(txtDosage);

                Grid.SetColumn(nameDosagePanel, 1);
                headerGrid.Children.Add(nameDosagePanel);

                // Availability Tag
                Border availabilityTag = CreateAvailabilityTag(quant);
                Grid.SetColumn(availabilityTag, 2);
                headerGrid.Children.Add(availabilityTag);

                mainContentStack.Children.Add(headerGrid);

                // Separator Line 
                Rectangle separator = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    Height = 1,
                    Margin = new Thickness(0, 0, 0, 10)
                };
                mainContentStack.Children.Add(separator);


                // Details (Generic Name & Description)
                mainContentStack.Children.Add(CreateDetailBlock("Generic Name", genericName, darkBlueBrush));

                StackPanel descriptionBlock = CreateDetailBlock("Description", description, darkBlueBrush);
                descriptionBlock.Margin = new Thickness(0, 8, 0, 0);
                mainContentStack.Children.Add(descriptionBlock);

                // Add the main content stack to the DockPanel.
                // The last element added to a DockPanel automatically fills the remaining space.
                medicineContent.Children.Add(mainContentStack);


                cardBorder.Child = medicineContent;
                StackPanelMedicines.Children.Add(cardBorder);
            }
        }
        // You should ensure CreateDetailBlock and CreateAvailabilityTag helper methods are defined
        // in your class as they were in the previous response's context.

        private void RequestMedicine_Click(string medicineId, string medicineName, string dose, string genericName)
        {
            MedicineRequestConfirmation requestConfirm = new MedicineRequestConfirmation(dose, medicineName, genericName);
            requestConfirm.Show();
        }

        public void RequestMedicine()
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user.dbConnection();
            displayMedicine("Select * from medicine_info");

            DataTable dt = new DataTable();
            dt = user.displayRecords($"select user_id from users where username = '{username}'");
            userId = Convert.ToInt32(dt.Rows[0][0].ToString());
            displayMedicineRequest("SELECT * FROM medicinerequests WHERE user_id = " + userId);
        }
        private Border CreateRequestStatusTag(string status)
        {
            Color bgColor;
            Brush fgColor;
            string statusText = status.ToUpper();

            switch (statusText)
            {
                case "PENDING":
                    bgColor = (Color)ColorConverter.ConvertFromString("#FFFDE7"); // Light Yellow
                    fgColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107")); // Amber
                    break;
                case "APPROVED":
                    bgColor = (Color)ColorConverter.ConvertFromString("#E8F5E9"); // Light Green
                    fgColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#388E3C")); // Dark Green
                    break;
                case "DENIED":
                    bgColor = (Color)ColorConverter.ConvertFromString("#FFEBEE"); // Light Red
                    fgColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D32F2F")); // Dark Red
                    break;
                default:
                    bgColor = (Color)ColorConverter.ConvertFromString("#E0E0E0"); // Light Gray
                    fgColor = Brushes.Gray;
                    break;
            }

            return new Border
            {
                Background = new SolidColorBrush(bgColor),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(8, 2, 8, 2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Child = new TextBlock
                {
                    Text = statusText,
                    Foreground = fgColor,
                    FontWeight = FontWeights.Bold,
                    FontSize = 10
                }
            };
        }


        public void displayMedicineRequest(String querry)
        {
            DataTable dt = new DataTable();
            dt = user.displayRecords(querry);

            StackPanelMedicineRequests.Children.Clear();

            int n = dt.Rows.Count;
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230)); // Background for icon

            for (int i = 0; i < n; i++)
            {

                String medicineName = dt.Rows[i][2].ToString();
                String reason = dt.Rows[i][3].ToString();
                String quantity = dt.Rows[i][4].ToString();
                String status = dt.Rows[i][5].ToString();
                String requestedAt = dt.Rows[i][6].ToString();
                String approvedAt = dt.Rows[i][7].ToString();

                // 1. Create the Card Container (Border)
                Border cardBorder = new Border
                {
                    // Adjusted Margin/Padding for a slightly more compact look
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(10), // Slightly rounder
                    Margin = new Thickness(0, 8, 0, 8), // Adjusted vertical margin
                    Padding = new Thickness(20), // Increased internal padding
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

                StackPanel requestContent = new StackPanel();

                // --- 1. Header Section ---
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Icon
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Name & Quantity
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Status Tag
                headerGrid.Margin = new Thickness(0, 0, 0, 15); // Increased margin for separator

                // 1a. Icon (New Addition)
                Border iconWrapper = new Border
                {
                    Background = lightGrayBrush,
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(5),
                    Width = 36,
                    Height = 36,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 10, 0) // Space from name
                };
                TextBlock medicineIcon = new TextBlock
                {
                    Text = "💊", // Medicine Icon
                    FontSize = 18,
                    Foreground = darkBlueBrush,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                iconWrapper.Child = medicineIcon;
                Grid.SetColumn(iconWrapper, 0);
                headerGrid.Children.Add(iconWrapper);


                // 1b. Name & Quantity
                StackPanel nameQuantityPanel = new StackPanel();

                TextBlock txtName = new TextBlock
                {
                    Text = medicineName,
                    FontWeight = FontWeights.ExtraBold, // Increased emphasis
                    FontSize = 18,
                    Foreground = darkBlueBrush,
                    TextWrapping = TextWrapping.Wrap
                };
                nameQuantityPanel.Children.Add(txtName);

                TextBlock txtQuantity = new TextBlock
                {
                    Text = $"Quantity Requested: {quantity}",
                    FontWeight = FontWeights.SemiBold, // Slightly more emphasis
                    FontSize = 13,
                    Foreground = Brushes.Gray
                };
                nameQuantityPanel.Children.Add(txtQuantity);

                Grid.SetColumn(nameQuantityPanel, 1);
                headerGrid.Children.Add(nameQuantityPanel);

                // 1c. Status Tag
                Border statusTag = CreateRequestStatusTag(status);
                Grid.SetColumn(statusTag, 2);
                headerGrid.Children.Add(statusTag);

                requestContent.Children.Add(headerGrid);

                // Separator Line
                Rectangle separator = new Rectangle
                {
                    Fill = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    Height = 1,
                    Margin = new Thickness(0, 0, 0, 15)
                };
                requestContent.Children.Add(separator);

                // --- 2. Details Section (Using Icon Detail Blocks) ---

                // Reason (Icon: 💬 or 📝)
                requestContent.Children.Add(CreateIconDetailBlock("Reason", reason, darkBlueBrush, "📝"));

                // Requested On (Icon: 🗓️ or 🕒)
                StackPanel requestedOnBlock = CreateIconDetailBlock("Requested On", requestedAt, darkBlueBrush, "🗓️");
                requestedOnBlock.Margin = new Thickness(0, 8, 0, 0); // Reduced margin
                requestContent.Children.Add(requestedOnBlock);

                // Approved/Handled On (Icon: ✅ or ⚙️)
                if (!string.IsNullOrWhiteSpace(approvedAt) && status.ToUpper() != "PENDING")
                {
                    string detailTitle;
                    string icon;

                    if (status.ToUpper() == "APPROVED")
                    {
                        detailTitle = "Approved On";
                        icon = "✅";
                    }
                    else // Denied/Handled
                    {
                        detailTitle = "Handled On";
                        icon = "⚙️";
                    }

                    StackPanel handledOnBlock = CreateIconDetailBlock(detailTitle, approvedAt, darkBlueBrush, icon);
                    handledOnBlock.Margin = new Thickness(0, 8, 0, 0); // Reduced margin
                    requestContent.Children.Add(handledOnBlock);
                }

                cardBorder.Child = requestContent;

                StackPanelMedicineRequests.Children.Add(cardBorder);
            }

        }

        // --- NEW HELPER METHOD FOR ICON DETAIL BLOCKS ---
        private StackPanel CreateIconDetailBlock(string title, string content, Brush colorBrush, string icon)
        {
            Grid grid = new Grid();
            // Grid columns: Icon | Text Details
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // 1. Icon (Emoji)
            TextBlock iconBlock = new TextBlock
            {
                Text = icon,
                FontSize = 16,
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(iconBlock, 0);
            grid.Children.Add(iconBlock);

            // 2. Text Details
            StackPanel textPanel = new StackPanel();

            TextBlock titleBlock = new TextBlock
            {
                Text = title,
                FontWeight = FontWeights.SemiBold,
                FontSize = 13,
                Foreground = Brushes.Black,
                Margin = new Thickness(0, 0, 0, 0) // Removed margin for tighter grouping
            };
            textPanel.Children.Add(titleBlock);

            TextBlock contentBlock = new TextBlock
            {
                Text = content,
                FontWeight = FontWeights.Normal,
                FontSize = 13,
                Foreground = Brushes.DarkGray,
                TextWrapping = TextWrapping.Wrap
            };
            textPanel.Children.Add(contentBlock);

            Grid.SetColumn(textPanel, 1);
            grid.Children.Add(textPanel);

            // Return the grid, wrapped in a StackPanel for consistent margin application in the calling loop
            StackPanel wrapperPanel = new StackPanel();
            wrapperPanel.Children.Add(grid);
            return wrapperPanel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm(username); 
            this.Close();
            userForm.Show();
        }
    }
} 
