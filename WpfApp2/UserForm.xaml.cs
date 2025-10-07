using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private void displayAppointment(string query)
        {
            DataTable dt = userForm.displayRecords(query);
            AppointmentStackPanel.Children.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string appointmentId = dt.Rows[i]["appointment_id"].ToString();
                string date = dt.Rows[i]["appointment_date"].ToString();
                string time = dt.Rows[i]["appointment_time"].ToString();
                string status = dt.Rows[i]["status"].ToString();
                string purpose = dt.Rows[i]["purpose_of_visit"].ToString();

                StackPanel appointmentPanel = new StackPanel();
                var foreColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));

                appointmentPanel.Children.Add(new TextBlock
                {
                    Text = $"Date: {date}",
                    FontWeight = FontWeights.DemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = foreColor
                });
                appointmentPanel.Children.Add(new TextBlock
                {
                    Text = $"Time: {time}",
                    FontWeight = FontWeights.DemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = foreColor
                });
                appointmentPanel.Children.Add(new TextBlock
                {
                    Text = $"Status: {status}",
                    FontWeight = FontWeights.DemiBold,
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = foreColor
                });
                appointmentPanel.Children.Add(new TextBlock
                {
                    Text = $"Purpose: {purpose}",
                    Margin = new Thickness(0, 0, 0, 5),
                    Foreground = foreColor
                });

                Border border = new Border
                {
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9E9E9E")),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(8),
                    Padding = new Thickness(10),
                    Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    Child = appointmentPanel
                };

                string currentAppointmentId = appointmentId;

                border.MouseRightButtonDown += (s, e) =>
                {
                    ContextMenu contextMenu = new ContextMenu();

                    MenuItem updateItem = new MenuItem { Header = "Update" };
                    updateItem.Click += (s2, e2) =>
                    {
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

                    border.ContextMenu = contextMenu;
                    contextMenu.IsOpen = true;
                };

                AppointmentStackPanel.Children.Add(border);
            }
        }

        public void cancelAppointment(string appointmentId)
        {
            string deleteQuery = $"DELETE FROM appointments WHERE appointment_id = {appointmentId}";
            userForm.sqlManager(deleteQuery);
            displayAppointment(SQL);
        }
        
    }
}
