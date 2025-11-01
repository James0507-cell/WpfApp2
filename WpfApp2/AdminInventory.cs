using MySql.Data.MySqlClient;
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
    internal class AdminInventory
    {
        Admin admin = new Admin();
        dbManager dbManager = new dbManager();

        public AdminInventory()
        {
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
                    dbManager.sqlManager(SQL);

                    MessageBox.Show($"Successfully added {amountToAdd} units to inventory for ID {medId}.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);

                    SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                         $"VALUES ({admin.getID()}, '{admin.getUsername()}', 'Update Medicine Inventory', 'Added {amountToAdd} units to medicine ID {medId}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
                    dbManager.sqlManager(SQL);

                    TriggerAppointmentActivityPanelReload();
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a positive number for the quantity.", "Input Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
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
        public class MedicineRequestData
        {
            public string RequestID { get; set; }
            public int InventoryID { get; set; }
            public int Quantity { get; set; }
        }
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
        public Border inventoryPanel(String medicineID, String medicineName, String dosage, String genericName, String description, int quant, String inventoryID)
        {
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush buttonBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF014BFF"));
            Brush lowStockRedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFA0A0"));

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

            if (quant < 20)
            {
                cardBorder.BorderBrush = lowStockRedBrush;
                cardBorder.BorderThickness = new Thickness(3);
            }

            DockPanel medicineContent = new DockPanel();


            Grid buttonContainer = new Grid
            {
                Margin = new Thickness(0, 15, 0, 0),
            };
            DockPanel.SetDock(buttonContainer, Dock.Bottom);

            buttonContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            buttonContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });



            Border updateQuantWrapper = new Border
            {
                CornerRadius = new CornerRadius(6, 0, 0, 6),
                Margin = new Thickness(0, 0, 2, 0),
                Background = buttonBlueBrush,
            };
            Grid.SetColumn(updateQuantWrapper, 0);

            Button updateQuantButton = new Button
            {
                Content = "Update Quantity",
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                BorderThickness = new Thickness(0),
                FontWeight = FontWeights.Bold,
                FontSize = 13,
                Padding = new Thickness(5, 10, 5, 10),
                Cursor = Cursors.Hand,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            updateQuantButton.Tag = new { MedicineId = medicineID, InventoryId = inventoryID };
            updateQuantButton.Click += UpdateMedicineInventory_Click;

            updateQuantWrapper.Child = updateQuantButton;
            buttonContainer.Children.Add(updateQuantWrapper);



            Border updateMedicineWrapper = new Border
            {
                CornerRadius = new CornerRadius(0, 6, 6, 0), 
                Margin = new Thickness(2, 0, 0, 0),
                Background = darkBlueBrush, 
            };
            Grid.SetColumn(updateMedicineWrapper, 1);

            Button updateMedicineButton = new Button
            {
                Content = "Update Medicine", 
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                BorderThickness = new Thickness(0),
                FontWeight = FontWeights.Bold,
                FontSize = 13,
                Padding = new Thickness(5, 10, 5, 10),
                Cursor = Cursors.Hand,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            updateMedicineButton.Tag = new
            {
                MedicineId = medicineID,
                MedicineName = medicineName, 
                InventoryId = inventoryID,
                Description = description,
                GenericName = genericName,
                Dosage = dosage
            };


            updateMedicineButton.Click += NewUpdateMedicineDetails_Click; 

            updateMedicineWrapper.Child = updateMedicineButton;
            buttonContainer.Children.Add(updateMedicineWrapper);

            medicineContent.Children.Add(buttonContainer);



            StackPanel mainContentStack = new StackPanel();

            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            headerGrid.Margin = new Thickness(0, 0, 0, 10);


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

            TextBlock txtInvId = new TextBlock
            {
                Text = $"ID: {inventoryID}",
                FontWeight = FontWeights.Normal,
                FontSize = 10,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 5, 0)
            };

            Border availabilityTag = CreateAvailabilityTag(quant);

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

            Rectangle separator = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(0x1A, 0x00, 0x10, 0x4D)),
                Height = 1,
                Margin = new Thickness(0, 0, 0, 10)
            };
            mainContentStack.Children.Add(separator);

            // NOTE: Assuming CreateDetailBlock method exists elsewhere in your class
            mainContentStack.Children.Add(CreateDetailBlock("Generic Name", genericName, darkBlueBrush));
            StackPanel descriptionBlock = CreateDetailBlock("Description", description, darkBlueBrush);
            descriptionBlock.Margin = new Thickness(0, 8, 0, 0);
            mainContentStack.Children.Add(descriptionBlock);

            medicineContent.Children.Add(mainContentStack);
            cardBorder.Child = medicineContent;

            return cardBorder;
        }

        private void NewUpdateMedicineDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                if (clickedButton == null) return;

                dynamic tagData = clickedButton.Tag;
                string medicineId = tagData.MedicineId.ToString();
                string medicineName = tagData.MedicineName?.ToString();
                string genericName = tagData.GenericName?.ToString();
                string dosage = tagData.Dosage?.ToString();
                string description = tagData.Description?.ToString();

                UpdateMedicine updateMedicineWindow = new UpdateMedicine(
                    medicineId,
                    medicineName,
                    genericName,
                    dosage,
                    description
                );

                updateMedicineWindow.ShowDialog(); // use ShowDialog() to make it modal
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open update window: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TriggerAppointmentActivityPanelReload()
        {
            AdminWindow activeAdminWindow = Application.Current.Windows
                .OfType<AdminWindow>()
                .SingleOrDefault(x => x.IsActive || x.IsVisible);

            if (activeAdminWindow != null)
            {
                activeAdminWindow.displayMedicineInv("Select * from medicine_info");
                activeAdminWindow.displayActivity("Select * from admin_activity_log");
            }
            else
            {
                MessageBox.Show("Could not find the User Dashboard to refresh.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void AddMedicine(
            string medicineName,
            string genericName,
            string milligrams,
            string description,
            int inventoryAmount
            )
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string activityType = "Add New Medicine";
            string activityDesc = $"Added new medicine: {medicineName} ({milligrams}) with initial inventory of {inventoryAmount}";

            // SQL statements
            string insertMedicineInfo =
                $"INSERT INTO `medicine_info` (`medicine_name`, `milligrams`, `generic_name`, `description`) " +
                $"VALUES ('{medicineName}', '{milligrams}', '{genericName}', '{description}')";

            string insertInventory =
                $"INSERT INTO `medicineinventory` (`medicine_id`, `medicine_name`, `amount`, `added_by`) " +
                $"VALUES (LAST_INSERT_ID(), '{medicineName}', {inventoryAmount}, {admin.getID()})";

            string insertActivityLog =
                $"INSERT INTO `admin_activity_log` (`activity_id`, `admin_id`, `username`, `activity_type`, `activity_desc`, `activity_date`) " +
                $"VALUES (NULL, {admin.getID()}, '{admin.getUsername()}', '{activityType}', '{activityDesc}', '{currentDateTime}')";

            string SQL = insertMedicineInfo + ";" + insertInventory + ";" + insertActivityLog;

            try
            {
                dbManager.sqlManager(SQL);
                MessageBox.Show($"Successfully added {medicineName}, its inventory record, and logged the activity.",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database operation failed: {ex.Message}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateMedicineInfo(
            int medicineId,
            string newMedicineName,
            string newGenericName,
            string newMilligrams,
            string newDescription
)
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string activityType = "Update Medicine Info";
            string activityDesc = $"Updated medicine (ID: {medicineId}) details to: " +
                                  $"{newMedicineName} ({newMilligrams}).";

            // SQL statements
            string updateMedicineInfo =
                $"UPDATE `medicine_info` " +
                $"SET `medicine_name` = '{newMedicineName}', " +
                $"`generic_name` = '{newGenericName}', " +
                $"`milligrams` = '{newMilligrams}', " +
                $"`description` = '{newDescription}' " +
                $"WHERE `medicine_id` = {medicineId};";

            string updateInventory =
                $"UPDATE `medicineinventory` " +
                $"SET `medicine_name` = '{newMedicineName}' " +
                $"WHERE `medicine_id` = {medicineId};";

            string insertActivityLog =
                $"INSERT INTO `admin_activity_log` " +
                $"(`activity_id`, `admin_id`, `username`, `activity_type`, `activity_desc`, `activity_date`) " +
                $"VALUES (NULL, {admin.getID()}, '{admin.getUsername()}', '{activityType}', '{activityDesc}', '{currentDateTime}');";

            string SQL = updateMedicineInfo + updateInventory + insertActivityLog;

            try
            {
                dbManager.sqlManager(SQL);
                MessageBox.Show($"Successfully updated {newMedicineName} and logged the activity.",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database operation failed: {ex.Message}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
