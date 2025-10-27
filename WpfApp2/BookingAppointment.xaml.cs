using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Globalization;

namespace WpfApp2
{
    public partial class BookingAppointment : Window
    {
        private DateTime selectedDate = DateTime.MinValue;
        private string selectedTime;
        private Booking booking = new Booking();
        private Users users = new Users();
        private String SQL = "";
        private string username = MainWindow.Username;
        private int userId = 0;

        public BookingAppointment()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            setStudentInfo();
            setDate();
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

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDate == DateTime.MinValue || string.IsNullOrEmpty(selectedTime))
            {
                MessageBox.Show("Please select a date and time for the appointment.");
                return;
            }

            int userId = booking.GetUserId(username);
            if (userId == -1)
            {
                MessageBox.Show("User not found.");
                return;
            }

            booking.InsertAppointment(
                userId,
                username,
                selectedDate,
                selectedTime,
                txtStudentID.Text,
                txtConfirmEmail.Text,
                txtConfirmPhone.Text,
                txtConfirmPurpose.Text,
                txtConfirmAllergies.Text,
                txtConfirmMedication.Text,
                txtConfirmPreviousVisit.Text,
                txtConfirmEmergencyName.Text,
                txtConfirmEmergencyPhone.Text,
                txtSymptoms.Text
            );

            booking.LogBookingActivity(userId, txtConfirmPurpose.Text);

            MessageBox.Show("Appointment successfully booked! Your appointment is pending and awaiting approval.");
            MyTabBooking.SelectedIndex = 2;
            CheckAndDisableBookedSlots();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm(username);
            userForm.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (MyTabBooking.SelectedIndex > 0) MyTabBooking.SelectedIndex--;
        }

        private void Button_Click(object sender, RoutedEventArgs e) { MyTabBooking.SelectedIndex = 1; }
        private void Button_Click_2(object sender, RoutedEventArgs e) { PopulateConfirmationTab(); MyTabBooking.SelectedIndex = 2; }
        private void BackButton_Click(object sender, RoutedEventArgs e) { if (MyTabBooking.SelectedIndex > 0) MyTabBooking.SelectedIndex--; }

        private void CheckAndDisableBookedSlots()
        {
            selectedTime = null;

            Style bookedStyle = (Style)FindResource("BookedAppointmentStyle");
            Style defaultStyle = (Style)FindResource("AppointmentButtonStyle");

            if (selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                foreach (var child in TimeSelectionGrid.Children)
                {
                    if (child is Button btn)
                    {
                        btn.IsEnabled = false;
                        btn.Style = bookedStyle;
                    }
                }
                return;
            }

            List<string> bookedTimes = booking.GetBookedTimes(selectedDate);

            foreach (var child in TimeSelectionGrid.Children)
            {
                if (child is Button btn && btn.Tag != null)
                {
                    string timeTag = btn.Tag.ToString();
                    btn.IsEnabled = true;
                    btn.Style = defaultStyle;

                    if (bookedTimes.Contains(timeTag))
                    {
                        btn.Style = bookedStyle;
                        btn.IsEnabled = false;
                    }
                }
            }
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
            txtConfirmSymptoms.Text = txtSymptoms.Text;
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
                txtEmergencyContactName.Text = dt.Rows[0]["emergency_contact_name"].ToString();
                txtEmergencyContactPhone.Text = dt.Rows[0]["emergency_contact_phone"].ToString();

                txtFirstName.IsEnabled = false;
                txtLastName.IsEnabled = false;
                txtStudentID.IsEnabled = false;


            }
        }
       public void setDate ()
        {
            if (!cldDate.SelectedDate.HasValue)
            {
                cldDate.SelectedDate = DateTime.Today;
            }
            selectedDate = cldDate.SelectedDate.Value;

        }

    }
}