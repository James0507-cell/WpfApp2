using System;
using System.Windows;

namespace WpfApp2
{
    public partial class UpdateAppointment : Window
    {
        private string appointmentId;
        private Users userForm = new Users();

        public UpdateAppointment(string appointmentId, string date, string time, string purpose)
        {
            InitializeComponent();
            this.appointmentId = appointmentId;

            // Pre-fill the fields with the current appointment details
            if (DateTime.TryParse(date, out DateTime parsedDate))
                DatePickerDate.SelectedDate = parsedDate;

            TextBoxTime.Text = time;
            TextBoxPurpose.Text = purpose;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            string newDate = DatePickerDate.SelectedDate?.ToString("yyyy-MM-dd") ?? "";
            string newTime = TextBoxTime.Text.Trim();
            string newPurpose = TextBoxPurpose.Text.Trim();

            if (string.IsNullOrEmpty(newDate) || string.IsNullOrEmpty(newTime) || string.IsNullOrEmpty(newPurpose))
            {
                MessageBox.Show("Please fill out all fields before saving.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string updateQuery = $"UPDATE appointments SET " +
                                 $"appointment_date = '{newDate}', " +
                                 $"appointment_time = '{newTime}', " +
                                 $"purpose_of_visit = '{newPurpose}' " +
                                 $"WHERE appointment_id = {appointmentId}";

            try
            {

                userForm.sqlManager(updateQuery);
                MessageBox.Show("Appointment updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating appointment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBoxTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userForm.dbConnection();
        }
    }
}
