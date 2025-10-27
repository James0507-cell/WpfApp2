using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Globalization;

namespace WpfApp2
{
    public partial class UpdateAppointment : Window
    {
        private DateTime selectedDate = DateTime.MinValue;
        private string selectedTime;
        private Booking booking = new Booking();
        public string username = MainWindow.Username;
        int userId = 0;
        Users users = new Users();
        String SQL = "";
        String appointmentID;

        public UpdateAppointment(String appointmentID)
        {
            InitializeComponent();
            this.appointmentID = appointmentID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            setStudentInfo();
            setDate();
            CheckAndDisableBookedSlots();
            populatForms();
        }

        private void Button_Click(object sender, RoutedEventArgs e) { MyTabBooking.SelectedIndex = 1; }
        private void Button_Click_2(object sender, RoutedEventArgs e) { PopulateConfirmationTab(); MyTabBooking.SelectedIndex = 2; }
        private void BackButton_Click(object sender, RoutedEventArgs e) { if (MyTabBooking.SelectedIndex > 0) MyTabBooking.SelectedIndex--; }
        private void Button_Click_3(object sender, RoutedEventArgs e) { if (MyTabBooking.SelectedIndex > 0) MyTabBooking.SelectedIndex--; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm(username);
            userForm.Show();
            this.Close();
        }

        private void CheckAndDisableBookedSlots()
        {
            selectedTime = null;

            Style bookedStyle = (Style)FindResource("BookedAppointmentStyle");
            Style defaultStyle = (Style)FindResource("AppointmentButtonStyle");

            if (selectedDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                foreach (var child in TimeSelectionGrid.Children)
                    if (child is Button btn)
                    {
                        btn.IsEnabled = false;
                        btn.Style = bookedStyle;
                    }
                return;
            }

            List<string> bookedTimes = booking.GetBookedTimes(selectedDate);

            foreach (var child in TimeSelectionGrid.Children)
            {
                if (child is Button btn)
                {
                    string timeTag = btn.Tag?.ToString();

                    btn.IsEnabled = true;
                    btn.Style = defaultStyle;

                    if (string.IsNullOrEmpty(timeTag)) continue;

                    if (bookedTimes.Contains(timeTag))
                    {
                        btn.Style = bookedStyle;
                        btn.IsEnabled = false;
                    }
                    else if (selectedDate.Date == DateTime.Today.Date &&
                             DateTime.TryParse($"{selectedDate.ToShortDateString()} {timeTag}", out DateTime appointmentDateTime) &&
                             appointmentDateTime <= DateTime.Now)
                    {
                        btn.Style = bookedStyle;
                        btn.IsEnabled = false;
                    }
                }
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDate == DateTime.MinValue || string.IsNullOrEmpty(selectedTime))
            {
                MessageBox.Show("Please select a date and time for the appointment.");
                return;
            }

            DataTable dt = booking.displayRecords($"SELECT user_id FROM users WHERE username = '{username}'");
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("User not found.");
                return;
            }

            userId = Convert.ToInt32(dt.Rows[0]["user_id"]);
            string studentId = txtStudentID.Text;

            booking.UpdateAppointmentRecord(
                appointmentID, userId, studentId, selectedDate, selectedTime,
                cmbPurpose.Text, txtAllergies.Text, txtCurrentMedication.Text,
                cmbPreviousVisit.Text, txtEmergencyContactName.Text,
                txtEmergencyContactPhone.Text, txtSymptoms.Text
            );

            booking.LogUpdateActivity(userId, appointmentID);

            MessageBox.Show("Appointment successfully updated! It is now pending approval.");
            MyTabBooking.SelectedIndex = 2;
            CheckAndDisableBookedSlots();
        }

        private void ClsDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cldDate.SelectedDate.HasValue) return;

            selectedDate = cldDate.SelectedDate.Value;
            CheckAndDisableBookedSlots();
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                CheckAndDisableBookedSlots();

                selectedTime = clickedButton.Tag.ToString();

                clickedButton.Style = (Style)FindResource("SelectedAppointmentButtonStyle");
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDate == DateTime.MinValue || string.IsNullOrEmpty(selectedTime))
            {
                MessageBox.Show("Please select a date and time for the appointment before proceeding.");
                return;
            }
            PopulateConfirmationTab();
            MyTabBooking.SelectedIndex = 1;
        }

        private void NextButton2_Click(object sender, RoutedEventArgs e)
        {
            PopulateConfirmationTab();
            MyTabBooking.SelectedIndex = 2;
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
            txtComfirmSymptoms.Text = txtSymptoms.Text;
        }

        public void setStudentInfo()
        {
            SQL = "Select * from users where username = '" + username + "'";
            DataTable dt = users.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                txtFirstName.Text = dt.Rows[0]["first_name"].ToString();
                txtLastName.Text = dt.Rows[0]["last_name"].ToString();
                txtStudentID.Text = dt.Rows[0]["student_id"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtPhoneNumber.Text = dt.Rows[0]["phone_number"].ToString();

                txtFirstName.IsEnabled = false;
                txtLastName.IsEnabled = false;
                txtStudentID.IsEnabled = false;
            }
        }
       public void populatForms()
        {
            string SQL = $"SELECT * FROM appointments WHERE appointment_id = '{appointmentID}'";
            DataTable dt = booking.displayRecords(SQL);

            if (dt.Rows.Count > 0)
            {
                DateTime dbDate = Convert.ToDateTime(dt.Rows[0]["appointment_date"]);
                cldDate.SelectedDate = dbDate;
                selectedDate = dbDate;

                string dbTime = dt.Rows[0]["appointment_time"].ToString();

                if (DateTime.TryParse($"2000-01-01 {dbTime}", out DateTime parsedTime))
                {
                    string formattedTime = parsedTime.ToString("h:mm tt", CultureInfo.InvariantCulture);
                    selectedTime = formattedTime;

                    Button[] buttons = { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13 };

                    foreach (Button btn in buttons)
                    {
                        if (btn.Tag.ToString().Equals(selectedTime, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Appointment_Click(btn, null);
                            break;
                        }
                    }
                }

                cmbPurpose.Text = dt.Rows[0]["purpose_of_visit"].ToString();
                txtAllergies.Text = dt.Rows[0]["known_allergies"].ToString();
                txtCurrentMedication.Text = dt.Rows[0]["current_medication"].ToString();
                cmbPreviousVisit.Text = dt.Rows[0]["previous_visit"].ToString();
                txtEmergencyContactName.Text = dt.Rows[0]["emergency_contact_name"].ToString();
                txtEmergencyContactPhone.Text = dt.Rows[0]["emergency_contact_phone"].ToString();
                txtSymptoms.Text = dt.Rows[0]["current_symptoms"].ToString();
            }
        }

        public void setDate()
        {
            if (!cldDate.SelectedDate.HasValue)
            {
                cldDate.SelectedDate = DateTime.Today;
            }
            selectedDate = cldDate.SelectedDate.Value;
        }
    }
}