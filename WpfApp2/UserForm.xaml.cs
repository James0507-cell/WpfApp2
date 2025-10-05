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
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window

    {
        MainWindow mainWindow = new MainWindow();
        String username = "";
        Users userForm = new Users();
        String SQL = "";

        ShortCheck shortcheck = new ShortCheck();
        public UserForm()
        {
            InitializeComponent();
            
        }

        private void dfs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            shortcheck.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            BookingAppointment bookingAppointment = new BookingAppointment();
            bookingAppointment.Show();
            this.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQL = "Select * from appointments where username = '" + "jdelacruz" + "'";
            displayAppointment(SQL);
        }
        private void displayAppointment(String query)
        {
            String username = mainWindow.getUsername();
            DataTable dt = userForm.displayRecords(query);

            AppointmentStackPanel.Children.Clear(); // Clear previous entries

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String date = dt.Rows[i]["appointment_date"].ToString();
                String time = dt.Rows[i]["appointment_time"].ToString();
                String status = dt.Rows[i]["status"].ToString();
                String email = dt.Rows[i]["email"].ToString();
                String phone = dt.Rows[i]["phone_number"].ToString();
                String purpose = dt.Rows[i]["purpose_of_visit"].ToString();
                String allergies = dt.Rows[i]["known_allergies"].ToString();
                String previousVisit = dt.Rows[i]["previous_visit"].ToString();
                String emergencyName = dt.Rows[i]["emergency_contact_name"].ToString();
                String emergencyPhone = dt.Rows[i]["emergency_contact_phone"].ToString();

                // Create the panel for one appointment
                StackPanel appointmentPanel = new StackPanel();

                appointmentPanel.Children.Add(new TextBlock { Text = $"Date: {date}", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Time: {time}", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Status: {status}", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Email: {email}", Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Phone: {phone}", Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Purpose: {purpose}", Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Known Allergies: {allergies}", Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Previous Visit: {previousVisit}", Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Emergency Contact: {emergencyName}", Margin = new Thickness(0, 0, 0, 5) });
                appointmentPanel.Children.Add(new TextBlock { Text = $"Emergency Phone: {emergencyPhone}", Margin = new Thickness(0, 0, 0, 5) });

                // Wrap in a rounded border
                Border border = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(8),
                    Padding = new Thickness(10),
                    Background = new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                    Child = appointmentPanel
                };

                AppointmentStackPanel.Children.Add(border);
            }
        }

    }
}
