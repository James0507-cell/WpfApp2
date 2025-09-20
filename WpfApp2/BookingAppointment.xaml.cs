using System;
using System.Collections.Generic;
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
    /// Interaction logic for BookingAppointment.xaml
    /// </summary>
    public partial class BookingAppointment : Window
    {
        public BookingAppointment()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyTabBooking.SelectedIndex = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MyTabBooking.SelectedIndex = 1;
        }
        // Add this method to the code-behind file (BookingAppointment.xaml.cs)
        // Add this method to the code-behind file (BookingAppointment.xaml.cs)
        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            // Handle the button click event here
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                MessageBox.Show($"You selected: {clickedButton.Content}");
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the Patient Info tab
            MyTabBooking.SelectedIndex = 1; // Assuming the PatientDetail tab is at index 1
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            
        }



    }
}
