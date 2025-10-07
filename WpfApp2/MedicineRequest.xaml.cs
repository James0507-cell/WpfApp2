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
using System.Windows.Media.Effects; // Added for DropShadowEffect

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MedicineRequest.xaml
    /// </summary>
    public partial class MedicineRequest : Window
    {
        public string username = MainWindow.Username;
        Users user = new Users();
        String SQL = "";
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

        /// <summary>
        /// Creates the styled availability pill/tag element.
        /// Since availability data (stock) isn't in the provided SQL structure,
        /// this function simulates a value and styles it based on that value (e.g., > 50 is green).
        /// </summary>
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

        /// <summary>
        /// Creates a TextBlock for the descriptive details (Generic Name, Description).
        /// </summary>
        private TextBlock CreateDetailBlock(string label, string value, Brush foregroundBrush)
        {
            return new TextBlock
            {
                // This uses an Inline Run to style the label bold and the value normal
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

            // NOTE: Assuming medicine_id is dt.Rows[i][0] and medicine_name is dt.Rows[i][1]
            
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

                // 2. Main StackPanel to hold all content vertically
                StackPanel medicineContent = new StackPanel();


                // --- Row 1: Medicine Icon, Title, Dosage, and Availability Tag (using Grid) ---
                Grid headerGrid = new Grid();
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Icon
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Name & Dosage
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Availability Tag
                headerGrid.Margin = new Thickness(0, 0, 0, 10);

                // Icon (Simulated using TextBlock for a vector look)
                TextBlock iconBlock = new TextBlock
                {
                    Text = "💊", // Medicine icon
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 10, 0),
                    VerticalAlignment = VerticalAlignment.Top
                };
                Grid.SetColumn(iconBlock, 0);
                headerGrid.Children.Add(iconBlock);


                // Name and Dosage StackPanel
                StackPanel nameDosagePanel = new StackPanel();

                // Name
                TextBlock txtName = new TextBlock
                {
                    Text = medicineName,
                    FontWeight = FontWeights.DemiBold,
                    FontSize = 16,
                    Foreground = darkBlueBrush,
                    TextWrapping = TextWrapping.Wrap
                };
                nameDosagePanel.Children.Add(txtName);

                // Dosage/Strength
                TextBlock txtDosage = new TextBlock
                {
                    Text = $"{dosage} Tablet", // Assuming 'dosage' column holds the strength like '500mg'
                    FontWeight = FontWeights.Normal,
                    FontSize = 12,
                    Foreground = Brushes.Gray
                };
                nameDosagePanel.Children.Add(txtDosage);

                Grid.SetColumn(nameDosagePanel, 1);
                headerGrid.Children.Add(nameDosagePanel);

                // Replace 'simulatedStock' with the actual stock value 'quant' which is already defined in the loop
                Border availabilityTag = CreateAvailabilityTag(quant);
                Grid.SetColumn(availabilityTag, 2);
                headerGrid.Children.Add(availabilityTag);

                medicineContent.Children.Add(headerGrid);


                // --- Row 2: Generic Name and Description ---
                medicineContent.Children.Add(CreateDetailBlock("Generic Name", genericName, darkBlueBrush));
                medicineContent.Children.Add(CreateDetailBlock("Description", description, darkBlueBrush));


                // --- Row 3: Action Button ---
                Border buttonWrapper = new Border
                {
                    CornerRadius = new CornerRadius(6), // Apply rounded corners here
                    Margin = new Thickness(0, 15, 0, 0),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCC0000")), // Match button color

                };

                Button requestButton = new Button
                {
                    Content = "Request Medicine",
                    Background = Brushes.Transparent, // Make button background transparent so the Border color shows through
                    Foreground = Brushes.White,
                    BorderThickness = new Thickness(0), // Remove button border
                    FontWeight = FontWeights.Bold,
                    Padding = new Thickness(10),
                    Cursor = Cursors.Hand
                };
                requestButton.Click += (s, e) => RequestMedicine_Click(medicineId, medicineName);

                buttonWrapper.Child = requestButton; // Place button inside the rounded border

                medicineContent.Children.Add(buttonWrapper);


                // 3. Attach Content to Card Border
                cardBorder.Child = medicineContent;

                StackPanelMedicines.Children.Add(cardBorder);
            }
        }

        // Placeholder for the actual request logic
        private void RequestMedicine_Click(string medicineId, string medicineName)
        {
            DataTable dt = user.displayRecords("select * from users where username = '" + username + "'");
            String userId = dt.Rows[0][0].ToString();



        }

        public void RequestMedicine()
        {
            // Placeholder for main request flow if needed
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user.dbConnection();
            displayMedicine("Select * from medicine_info");
        }
    }
}
