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

namespace WpfApp2
{
    public partial class AdminWindow : Window
    {
        String SQL = "";
        Admin admin = new Admin();
        StudentManagement studentManagement = new StudentManagement();

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
                rejectAppointment(appointmentID);
            }
        }


        public void displayAppointments(String querry)
        {
            StackPanel targetStackPanel = this.AppointmentStackPanel;

            DataTable dt = new DataTable();
            
            dt = admin.displayRecors(querry);

            int n = dt.Rows.Count;
            targetStackPanel.Children.Clear(); 

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



                Border cardBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.LightGray),
                    BorderThickness = new Thickness(1),
                    Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xF7, 0xFA)),
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(10, 6, 10, 6),
                    Padding = new Thickness(15),
                    Width = 590, 
                    HorizontalAlignment = HorizontalAlignment.Center,
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
                targetStackPanel.Children.Add(cardBorder);
            }
        }
        public void approveAppointment(String appointmentID)
        {
            String querry = $"UPDATE appointments SET status = 'Approved' WHERE appointment_id = {appointmentID}";
            admin.sqlManager(querry);
            displayAppointments("SELECT * FROM appointments");
        }

        public void rejectAppointment(String appointmentID)
        {
            String querry = $"UPDATE appointments SET status = 'Rejected' WHERE appointment_id = {appointmentID}";
            admin.sqlManager(querry);
            displayAppointments("SELECT * FROM appointments");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            displayAppointments("SELECT * FROM appointments");
        }

        private void txtSearchIDapp_TextChanged(object sender, TextChangedEventArgs e)
        {
            String querry = $"SELECT * FROM appointments WHERE student_id LIKE '%{txtSearchIDapp.Text}%'";
            displayAppointments(querry);
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
    }
}
