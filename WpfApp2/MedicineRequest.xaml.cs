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

        
        private TextBlock CreateDetailBlock(string label, string value, Brush foregroundBrush)
        {
            return new TextBlock
            {
                Inlines =
                {
                    new Run { Text = $"{label}: ", FontWeight = FontWeights.DemiBold, Foreground = foregroundBrush },
                    new Run { Text = value, FontWeight = FontWeights.Normal, Foreground = foregroundBrush }
                },
                FontSize = 13,
                Margin = new Thickness(0, 4, 0, 0),
                TextWrapping = TextWrapping.Wrap
            };
        }

        public void displayMedicine(String querry)
        {
            DataTable dt = new DataTable();
            dt = user.displayRecords(querry);
            StackPanelMedicines.Children.Clear();
            int n = dt.Rows.Count;

            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));


            for (int i = 0; i < n; i++)
            {


                String medicineId = dt.Rows[i][0].ToString();
                String medicineName = dt.Rows[i][1].ToString();
                String dosage = dt.Rows[i][2].ToString(); // This should be the strength (e.g., 500mg)
                String genericName = dt.Rows[i][3].ToString();
                String description = dt.Rows[i][4].ToString();
                DataTable quantity = user.displayRecords("select amount from medicineinventory where medicine_id = '" + medicineId + "'");
                int quant = Convert.ToInt32(quantity.Rows[0][0]);

                // 1. Create the Card Container (Border)
                Border cardBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)), // Very light, transparent border
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(10, 6, 10, 6),
                    Padding = new Thickness(15),
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

                StackPanel medicineContent = new StackPanel();


                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Icon
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Name & Dosage
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Availability Tag
                headerGrid.Margin = new Thickness(0, 0, 0, 10);

                TextBlock iconBlock = new TextBlock
                {
                    Text = "💊",
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalAlignment = VerticalAlignment.Top
                };
                Grid.SetColumn(iconBlock, 0);
                headerGrid.Children.Add(iconBlock);


                StackPanel nameDosagePanel = new StackPanel();

                TextBlock txtName = new TextBlock
                {
                    Text = medicineName,
                    FontWeight = FontWeights.DemiBold,
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

                Border availabilityTag = CreateAvailabilityTag(quant);
                Grid.SetColumn(availabilityTag, 2);
                headerGrid.Children.Add(availabilityTag);

                medicineContent.Children.Add(headerGrid);


                medicineContent.Children.Add(CreateDetailBlock("Generic Name", genericName, darkBlueBrush));
                medicineContent.Children.Add(CreateDetailBlock("Description", description, darkBlueBrush));


                // --- Row 3: Action Button ---
                Border buttonWrapper = new Border
                {
                    CornerRadius = new CornerRadius(6), 
                    Margin = new Thickness(0, 15, 0, 0),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")), // Match button color

                };

                Button requestButton = new Button
                {
                    Content = "Request Medicine",
                    Background = Brushes.Transparent, 
                    Foreground = Brushes.White,
                    BorderThickness = new Thickness(0), 
                    FontWeight = FontWeights.Bold,
                    Padding = new Thickness(10),
                    Cursor = Cursors.Hand
                };
                requestButton.Click += (s, e) => RequestMedicine_Click(medicineId, medicineName, dosage, genericName);

                buttonWrapper.Child = requestButton; // Place button inside the rounded border

                medicineContent.Children.Add(buttonWrapper);

                if (quant == 0)
                {
                    requestButton.IsEnabled = false;
                    requestButton.Content = "Out of Stock";
                    buttonWrapper.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB0B0B0")); // Gray background for disabled state
                }

                cardBorder.Child = medicineContent;

                StackPanelMedicines.Children.Add(cardBorder);
            }
        }

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
                    BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(10, 6, 10, 6),
                    Padding = new Thickness(15),
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

                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Name & Quantity
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Status Tag
                headerGrid.Margin = new Thickness(0, 0, 0, 10);

                StackPanel nameQuantityPanel = new StackPanel();

                TextBlock txtName = new TextBlock
                {
                    Text = medicineName,
                    FontWeight = FontWeights.DemiBold,
                    FontSize = 16,
                    Foreground = darkBlueBrush,
                    TextWrapping = TextWrapping.Wrap
                };
                nameQuantityPanel.Children.Add(txtName);

                TextBlock txtQuantity = new TextBlock
                {
                    Text = $"Quantity Requested: {quantity}",
                    FontWeight = FontWeights.Normal,
                    FontSize = 12,
                    Foreground = Brushes.Gray
                };
                nameQuantityPanel.Children.Add(txtQuantity);

                Grid.SetColumn(nameQuantityPanel, 0);
                headerGrid.Children.Add(nameQuantityPanel);

                Border statusTag = CreateRequestStatusTag(status);
                Grid.SetColumn(statusTag, 1);
                headerGrid.Children.Add(statusTag);

                requestContent.Children.Add(headerGrid);

                requestContent.Children.Add(CreateDetailBlock("Reason", reason, darkBlueBrush));

                requestContent.Children.Add(CreateDetailBlock("Requested On", requestedAt, darkBlueBrush));

                if (!string.IsNullOrWhiteSpace(approvedAt) && status.ToUpper() != "PENDING")
                {
                    requestContent.Children.Add(CreateDetailBlock(
                        status.ToUpper() == "APPROVED" ? "Approved On" : "Handled On",
                        approvedAt,
                        darkBlueBrush));
                }

                cardBorder.Child = requestContent;

                StackPanelMedicineRequests.Children.Add(cardBorder);
            }

        }
    }
} 
