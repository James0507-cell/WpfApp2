using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class BookingAppointment : Window
    {
        private DateTime selectedDate;
        private string selectedTime;
        String SQL = "";
        Booking booking = new Booking();


        private MainWindow mainWindow;

    public BookingAppointment(MainWindow mw)
    {
        InitializeComponent();
        mainWindow = mw;
        string user = mainWindow.Username;
        MessageBox.Show("Logged in as: " + user);
    }

        // Calendar selection
        private void ClsDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cldDate.SelectedDate.HasValue)
            {
                selectedDate = cldDate.SelectedDate.Value;
            }
        }

        // Navigate between tabs
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

        // Time selection
        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Store selected time
                selectedTime = clickedButton.Tag.ToString();

                // Clear previous selection
                ClearPreviousSelection();

                // Highlight clicked button
                clickedButton.Style = (Style)FindResource("SelectedAppointmentButtonStyle");
            }
        }

        // Clear previous selection styling
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
            MyTabBooking.SelectedIndex = 1; // Navigate back to PatientDetail tab
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateConfirmationTab();
            MyTabBooking.SelectedIndex = 1; // Confirmation tab
        }

       
        private void PopulateConfirmationTab()
        {
            // Combine first and last name
            txtConfirmPatientName.Text = $"{txtFirstName.Text} {txtLastName.Text}";

            // Personal info
            txtConfirmEmail.Text = txtEmail.Text;
            txtConfirmPhone.Text = txtPhoneNumber.Text;
            txtConfirmStudentID.Text = txtStudentID.Text;

            // Medical info
            txtConfirmPurpose.Text = cmbPurpose.Text;
            txtConfirmAllergies.Text = txtAllergies.Text;
            txtConfirmMedication.Text = txtCurrentMedication.Text;
            txtConfirmPreviousVisit.Text = cmbPreviousVisit.Text;

            // Emergency contact
            txtConfirmEmergencyName.Text = txtEmergencyContactName.Text;
            txtConfirmEmergencyPhone.Text = txtEmergencyContactPhone.Text;

            // Appointment info
            txtConfirmDate.Text = selectedDate.ToString("MMMM dd, yyyy");
            txtConfirmTime.Text = selectedTime;
        }
        
        




        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbPurpose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSymptoms_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtAllergies_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCurrentMedication_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbPreviousVisit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtEmergencyContactName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEmergencyContactPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string username = mainWindow.Username;

            if (selectedDate == DateTime.MinValue || string.IsNullOrEmpty(selectedTime))
            {
                MessageBox.Show("Please select a date and time for the appointment.");
                
            }

            // 1. Get user_id and student_id from users table
            int userId = 0;
            string studentId = "";

            // Use local connection
            booking.dbConnection(); // opens booking.conn internally
            String strConn = "server = localhost; user id = root; password =; database = db_medicaremmcm";

            string getUserQuery = "SELECT user_id, student_id FROM users WHERE username = '" + username + "' LIMIT 1";

            MySqlConnection conn = new MySqlConnection(strConn); // assuming you have connectionString in Booking
            conn.Open();
            MySqlCommand cmdGetUser = new MySqlCommand(getUserQuery, conn);
            MySqlDataReader reader = cmdGetUser.ExecuteReader();
            if (reader.Read())
            {
                userId = reader.GetInt32("user_id");
                studentId = reader.GetString("student_id");
            }
            else
            {
                MessageBox.Show("User not found in database.");
                reader.Close();
                conn.Close();
                return;
            }
            reader.Close();
            conn.Close();

            // 2. Insert new appointment using sqlManager
            string insertQuery = "INSERT INTO appointments (user_id, username, student_id, appointment_date, appointment_time, email, phone_number, purpose_of_visit, known_allergies, current_medication, previous_visit, emergency_contact_name, emergency_contact_phone, status, created_at) " +
                                 "VALUES (" + userId + ", '" + username + "', '" + studentId + "', '" + selectedDate.ToString("yyyy-MM-dd") + "', '" + selectedTime + "', '" + txtConfirmEmail.Text + "', '" + txtConfirmPhone.Text + "', '" + txtConfirmPurpose.Text + "', '" + txtConfirmAllergies.Text + "', '" + txtConfirmMedication.Text + "', '" + txtConfirmPreviousVisit.Text + "', '" + txtConfirmEmergencyName.Text + "', '" + txtConfirmEmergencyPhone.Text + "', 'Pending', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            booking.sqlManager(insertQuery);

            MessageBox.Show("Appointment successfully booked!");
            MyTabBooking.SelectedIndex = 2;
        }



        private void NextButton2_Click(object sender, RoutedEventArgs e)
        {
            PopulateConfirmationTab();
            MyTabBooking.SelectedIndex = 2;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            booking.dbConnection();
        }
    }
}
