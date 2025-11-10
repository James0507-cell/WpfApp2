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
using System.Windows.Shapes;

namespace WpfApp2
{
    internal class UserMedicine
    {
        
        dbManager dbManager = new dbManager();

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
                Margin = new Thickness(0, 0, 0, 0) 
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

            StackPanel wrapperPanel = new StackPanel();
            wrapperPanel.Children.Add(grid);
            return wrapperPanel;
        }

        public Border medicineRequestPanels(String medicineName, String reason, String quantity, String status, String requestedAt, String approvedAt, String rejectReason, String requestID)
        {
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230)); // Background for icon
            Brush redBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD70000")); // Red color for rejection reason

            Border cardBorder = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                BorderThickness = new Thickness(1),
                Background = Brushes.White,
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(0, 8, 20, 8),
                Padding = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Stretch,

            };

            StackPanel requestContent = new StackPanel();

            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Icon
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Name & Quantity
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Status Tag
            headerGrid.Margin = new Thickness(0, 0, 0, 15);

            Border iconWrapper = new Border
            {
                Background = lightGrayBrush,
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(5),
                Width = 36,
                Height = 36,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 10, 0)
            };
            TextBlock medicineIcon = new TextBlock
            {
                Text = "💊",
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
                FontWeight = FontWeights.ExtraBold,
                FontSize = 18,
                Foreground = darkBlueBrush,
                TextWrapping = TextWrapping.Wrap
            };
            nameQuantityPanel.Children.Add(txtName);

            TextBlock txtQuantity = new TextBlock
            {
                Text = $"Quantity Requested: {quantity}",
                FontWeight = FontWeights.SemiBold,
                FontSize = 13,
                Foreground = Brushes.Gray
            };

            nameQuantityPanel.Children.Add(txtQuantity);
            TextBlock txtRequestID = new TextBlock
            {
                Text = $"Request ID: {requestID}",
                FontWeight = FontWeights.SemiBold,
                FontSize = 13,
                Foreground = Brushes.Gray
            };
            nameQuantityPanel.Children.Add(txtRequestID);

            Grid.SetColumn(nameQuantityPanel, 1);
            headerGrid.Children.Add(nameQuantityPanel);

            Border statusTag = CreateRequestStatusTag(status);
            Grid.SetColumn(statusTag, 2);
            headerGrid.Children.Add(statusTag);

            requestContent.Children.Add(headerGrid);

            Rectangle separator = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                Height = 1,
                Margin = new Thickness(0, 0, 0, 15)
            };
            requestContent.Children.Add(separator);


            requestContent.Children.Add(CreateIconDetailBlock("Reason", reason, darkBlueBrush, "📝"));

            StackPanel requestedOnBlock = CreateIconDetailBlock("Requested On", requestedAt, darkBlueBrush, "🗓️");
            requestedOnBlock.Margin = new Thickness(0, 8, 0, 0);
            requestContent.Children.Add(requestedOnBlock);

            
            if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(rejectReason))
            {
                StackPanel rejectReasonBlock = CreateIconDetailBlock("Rejection Reason", rejectReason, redBrush, "❌");
                rejectReasonBlock.Margin = new Thickness(0, 8, 0, 0);
                requestContent.Children.Add(rejectReasonBlock);
            }

            if (!string.IsNullOrWhiteSpace(approvedAt) && status.ToUpper() != "PENDING")
            {
                string detailTitle;
                string icon;

                if (status.ToUpper() == "APPROVED")
                {
                    detailTitle = "Approved On";
                    icon = "✅";
                }
                else
                {
                    detailTitle = "Handled On";
                    icon = "⚙️";
                }

                StackPanel handledOnBlock = CreateIconDetailBlock(detailTitle, approvedAt, darkBlueBrush, icon);
                handledOnBlock.Margin = new Thickness(0, 8, 0, 0);
                requestContent.Children.Add(handledOnBlock);
            }

            cardBorder.Child = requestContent;
            return cardBorder;
        }

        private StackPanel CreateDetailBlock(string title, string content, Brush colorBrush)
        {
            StackPanel panel = new StackPanel(); 

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

            return panel;
        }

        private Border CreateAvailabilityTag(int stock)
        {
            string stockText = $"{stock} available";
            Color bgColor = stock > 50 ? (Color)ColorConverter.ConvertFromString("#E8F5E9") :
                             stock > 10 ? (Color)ColorConverter.ConvertFromString("#FFFDE7") : 
                             (Color)ColorConverter.ConvertFromString("#FFEBEE"); 

            Brush fgColor = stock > 50 ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#388E3C")) : 
                              stock > 10 ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107")) :
                              new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D32F2F")); 

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
        private void RequestMedicine_Click(string medicineId, string medicineName, string dose, string genericName)
        {
            MedicineRequestConfirmation requestConfirm = new MedicineRequestConfirmation(dose, medicineName, genericName);
            requestConfirm.Show();
        }

        public Border medicineOrderPanels(String medicineId, String medicineName, String dosage, String genericName, String description, int quant)
        {
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush buttonRedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD0021B"));

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

            };

            DockPanel medicineContent = new DockPanel();

            Border buttonWrapper = new Border
            {
                CornerRadius = new CornerRadius(6),
                Margin = new Thickness(0, 15, 0, 0), 
                Background = buttonRedBrush,
            };

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

            medicineContent.Children.Add(buttonWrapper);

            if (quant == 0)
            {
                requestButton.IsEnabled = false;
                requestButton.Content = "Out of Stock";
                buttonWrapper.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB0B0B0"));
            }

            
            StackPanel mainContentStack = new StackPanel();

            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            headerGrid.Margin = new Thickness(0, 0, 0, 10);

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

            Border availabilityTag = CreateAvailabilityTag(quant);
            Grid.SetColumn(availabilityTag, 2);
            headerGrid.Children.Add(availabilityTag);

            mainContentStack.Children.Add(headerGrid);

            Rectangle separator = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                Height = 1,
                Margin = new Thickness(0, 0, 0, 10)
            };
            mainContentStack.Children.Add(separator);


            mainContentStack.Children.Add(CreateDetailBlock("Generic Name", genericName, darkBlueBrush));

            StackPanel descriptionBlock = CreateDetailBlock("Description", description, darkBlueBrush);
            descriptionBlock.Margin = new Thickness(0, 8, 0, 0);
            mainContentStack.Children.Add(descriptionBlock);

            
            medicineContent.Children.Add(mainContentStack);


            cardBorder.Child = medicineContent;
            return cardBorder;
        }
        public void InsertMedicineRequest(string userId, string medicineName, string purpose, string quantity)
        {
            string insertRequest = $@"
                INSERT INTO medicinerequests (user_id, medicine_name, reason, quantity, status)
                VALUES ('{userId}', '{medicineName}', '{purpose}', '{quantity}', 'Pending')";

            string insertLog = $@"
                INSERT INTO student_activity_log (user_id, activity_type, activity_desc)
                VALUES ('{userId}', 'Medicine Request', 'Request Medicine for {purpose}')";

            dbManager.sqlManager(insertRequest);
            dbManager.sqlManager(insertLog);
        }
        
    }
}
