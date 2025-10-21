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
            booking.dbConnection();
            users.dbConnection();
            setStudentInfo();

            if (!cldDate.SelectedDate.HasValue)
            {
                cldDate.SelectedDate = DateTime.Today;
            }
            selectedDate = cldDate.SelectedDate.Value;

            CheckAndDisableBookedSlots();
            populatForms();
        }
        

        private List<string> GetBookedTimesForDate(DateTime date)
        {
            var bookedTimes = new List<string>();
            string dateString = date.ToString("yyyy-MM-dd");

            string SQL = $"SELECT appointment_time FROM appointments WHERE appointment_date = '{dateString}' AND (status = 'Pending' OR status = 'Approved')";

            try
            {
                DataTable dt = booking.displayRecords(SQL);

                foreach (DataRow row in dt.Rows)
                {
                    string dbTime = row["appointment_time"].ToString();

                    if (!string.IsNullOrEmpty(dbTime) &&
                        DateTime.TryParse($"2000-01-01 {dbTime}", out DateTime parsedTime))
                    {
                        string formattedTime = parsedTime.ToString("h:mm tt", CultureInfo.InvariantCulture);

                        bookedTimes.Add(formattedTime);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching booked times: {ex.Message}");
            }

            return bookedTimes;
        }


        private void CheckAndDisableBookedSlots()
        {
            selectedTime = null; // Reset selection

            Style bookedStyle = (Style)FindResource("BookedAppointmentStyle");
            Style defaultStyle = (Style)FindResource("AppointmentButtonStyle");

            // 1. Handle Weekend Closure
            if (selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                if (TimeSelectionGrid == null) return;
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

            List<string> bookedTimes = GetBookedTimesForDate(selectedDate);

            if (TimeSelectionGrid == null) return;
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
                    else if (selectedDate.Date == DateTime.Today.Date)
                    {
                        if (DateTime.TryParse($"{selectedDate.ToShortDateString()} {timeTag}", out DateTime appointmentDateTime))
                        {
                            if (appointmentDateTime <= DateTime.Now)
                            {
                                btn.Style = bookedStyle;
                                btn.IsEnabled = false;
                            }
                        }
                    }
                }
            }
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
            string username = MainWindow.Username;

            if (selectedDate == DateTime.MinValue || string.IsNullOrEmpty(selectedTime))
            {
                MessageBox.Show("Please select a date and time for the appointment.");
                return;
            }

            DataTable dt = booking.displayRecords($"Select user_id from users where username = '{username}'");
            if (dt.Rows.Count > 0)
            {
                userId = Convert.ToInt32(dt.Rows[0]["user_id"]);
            }
            else
            {
                MessageBox.Show("User not found.");
                return;
            }


            DateTime timeToConvert;
            if (!DateTime.TryParse(selectedTime, out timeToConvert))
            {
                MessageBox.Show("Error parsing selected time for database insert.");
                return;
            }
            string dbTimeFormat = timeToConvert.ToString("HH:mm:ss");

            string studentId = txtStudentID.Text;
           

            string UpdateQuerry = $"Update appointments set student_id = '{studentId}', user_id = {userId}, appointment_date = '{selectedDate:yyyy-MM-dd}', appointment_time = '{dbTimeFormat}', purpose_of_visit = '{cmbPurpose.Text}', known_allergies = '{txtAllergies.Text}', current_medication = '{txtCurrentMedication.Text}', previous_visit = '{cmbPreviousVisit.Text}', emergency_contact_name = '{txtEmergencyContactName.Text}', current_symptoms = '{txtSymptoms.Text}', emergency_contact_phone = '{txtEmergencyContactPhone.Text}', status = 'Pending' where appointment_id = {appointmentID}";

            booking.sqlManager(UpdateQuerry);
            MessageBox.Show("Appointment successfully booked! Your appointment is Pending and awaiting approval.");

            // 3. Log the activity
            string SQL_log = $@"
            INSERT INTO student_activity_log (user_id, activity_type, activity_desc)
            VALUES ({userId}, 'Appointment', 'Update appointment ID: {appointmentID}')";
            booking.sqlManager(SQL_log);

            MyTabBooking.SelectedIndex = 2;
            CheckAndDisableBookedSlots();
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
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm(username);
            userForm.Show();
            this.Close();
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


        private void Button_Click(object sender, RoutedEventArgs e) { MyTabBooking.SelectedIndex = 1; }
        private void Button_Click_2(object sender, RoutedEventArgs e) { PopulateConfirmationTab(); MyTabBooking.SelectedIndex = 2; }
        private void BackButton_Click(object sender, RoutedEventArgs e) { if (MyTabBooking.SelectedIndex > 0) MyTabBooking.SelectedIndex--; }

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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (MyTabBooking.SelectedIndex > 0) MyTabBooking.SelectedIndex--;
        }
    }
}