using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class BookingAppointment : Window
    {
        private DateTime selectedDate;
        private string selectedTime;
        private readonly Booking booking = new Booking();
        public string username = MainWindow.Username;

        public BookingAppointment()
        {
            InitializeComponent();
        }

        private void ClsDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cldDate.SelectedDate.HasValue)
            {
                selectedDate = cldDate.SelectedDate.Value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyTabBooking.SelectedIndex = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm(username); 
            userForm.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PopulateConfirmationTab();
            MyTabBooking.SelectedIndex = 2;
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                selectedTime = clickedButton.Tag.ToString();

                ClearPreviousSelection();

                clickedButton.Style = (Style)FindResource("SelectedAppointmentButtonStyle");
            }
        }

        private void ClearPreviousSelection()
        {
            foreach (var child in TimeSelectionGrid.Children)
            {
                if (child is Button btn)
                {
                    btn.Style = (Style)FindResource("AppointmentButtonStyle");
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyTabBooking.SelectedIndex > 0)
                MyTabBooking.SelectedIndex--;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateConfirmationTab();
            MyTabBooking.SelectedIndex = 1;
        }

        private void PopulateConfirmationTab()
        {
            txtConfirmPatientName.Text = $"{txtFirstName.Text} {txtLastName.Text}";
            txtConfirmEmail.Text = txtEmail.Text;
            txtConfirmPhone.Text = txtPhoneNumber.Text;
            txtConfirmStudentID.Text = txtStudentID.Text;
            txtConfirmPurpose.Text = cmbPurpose.Text;
            txtConfirmAllergies.Text = txtAllergies.Text;
            txtConfirmMedication.Text = txtCurrentMedication.Text;
            txtConfirmPreviousVisit.Text = cmbPreviousVisit.Text;
            txtConfirmEmergencyName.Text = txtEmergencyContactName.Text;
            txtConfirmEmergencyPhone.Text = txtEmergencyContactPhone.Text;
            txtConfirmDate.Text = selectedDate != DateTime.MinValue ? selectedDate.ToString("MMMM dd, yyyy") : "N/A";
            txtConfirmTime.Text = string.IsNullOrEmpty(selectedTime) ? "N/A" : selectedTime;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string username = MainWindow.Username;

            if (selectedDate == DateTime.MinValue || string.IsNullOrEmpty(selectedTime))
            {
                MessageBox.Show("Please select a date and time for the appointment.");
                return;
            }

            int userId = 0;
            string studentId = "";

            booking.dbConnection();
            /*string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";
            string getUserQuery = $"SELECT user_id, student_id FROM users WHERE username = '{username}' LIMIT 1";

            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(getUserQuery, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userId = reader.GetInt32("user_id");
                        studentId = reader.GetString("student_id");
                    }
                    else
                    {
                        MessageBox.Show("User not found in database.");
                        return;
                    }
                }
            }*/

            string insertQuery = $@"
                INSERT INTO appointments 
                (user_id, username, student_id, appointment_date, appointment_time, email, phone_number, 
                 purpose_of_visit, known_allergies, current_medication, previous_visit, 
                 emergency_contact_name, emergency_contact_phone, status, created_at)
                VALUES 
                ({userId}, '{username}', '{studentId}', '{selectedDate:yyyy-MM-dd}', '{selectedTime}', 
                '{txtConfirmEmail.Text}', '{txtConfirmPhone.Text}', '{txtConfirmPurpose.Text}', 
                '{txtConfirmAllergies.Text}', '{txtConfirmMedication.Text}', '{txtConfirmPreviousVisit.Text}', 
                '{txtConfirmEmergencyName.Text}', '{txtConfirmEmergencyPhone.Text}', 'Pending', 
                '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";

            booking.sqlManager(insertQuery);
            MessageBox.Show("Appointment successfully booked!");
            MyTabBooking.SelectedIndex = 2;
        }

        private void NextButton2_Click(object sender, RoutedEventArgs e)
        {
            PopulateConfirmationTab();
            MyTabBooking.SelectedIndex = 2;
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e) { }
        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e) { }
        private void txtStudentID_TextChanged(object sender, TextChangedEventArgs e) { }
        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e) { }
        private void txtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e) { }
        private void cmbPurpose_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void txtSymptoms_TextChanged(object sender, TextChangedEventArgs e) { }
        private void txtAllergies_TextChanged(object sender, TextChangedEventArgs e) { }
        private void txtCurrentMedication_TextChanged(object sender, TextChangedEventArgs e) { }
        private void cmbPreviousVisit_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void txtEmergencyContactName_TextChanged(object sender, TextChangedEventArgs e) { }
        private void txtEmergencyContactPhone_TextChanged(object sender, TextChangedEventArgs e) { }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            booking.dbConnection();
        }
    }
}
