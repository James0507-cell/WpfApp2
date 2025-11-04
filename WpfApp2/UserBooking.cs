using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
   internal class UserBooking
    {
        dbManager dbManager = new dbManager();

        public List<string> GetBookedTimes(DateTime date)
        {
            // ============================
            // FACTS: Retrieve rows from DB
            // ============================

            string dateString = date.ToString("yyyy-MM-dd");

            string query = $@"
        SELECT appointment_time, status
        FROM appointments
        WHERE appointment_date = '{dateString}'";

            DataTable dt;

            try
            {
                dt = dbManager.displayRecords(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching booked times: {ex.Message}");
                return new List<string>();
            }

            // Convert DataRows to appointment facts
            var facts = dt.AsEnumerable()
                          .Select(row => new
                          {
                              Time = row["appointment_time"]?.ToString(),
                              Status = row["status"]?.ToString()
                          })
                          .ToList();

            // =======================
            // RULES: Logic predicates
            // =======================

            bool IsBooked(string status) =>
                status == "Pending" || status == "Approved";

            bool IsValidTime(string time) =>
                DateTime.TryParse(time, out _);

            DateTime ParseTime(string time) =>
                DateTime.Parse(time);

            // =====================
            // QUERY: Apply the rules
            // =====================

            var bookedTimes =
                facts
                    .Where(f => IsBooked(f.Status))          // rule 1
                    .Where(f => IsValidTime(f.Time))         // rule 2
                    .Select(f => ParseTime(f.Time))          // transform
                    .Select(t => t.ToString("h:mm tt",
                               System.Globalization.CultureInfo.InvariantCulture))
                    .ToList();

            return bookedTimes;
        }


        public void InsertAppointment(int userId, string username, DateTime selectedDate, string selectedTime, string studentId,
            string email, string phone, string purpose, string allergies, string medication, string prevVisit,
            string emergencyName, string emergencyPhone, string symptoms)
        {
            DateTime timeToConvert = DateTime.Parse(selectedTime);
            string dbTimeFormat = timeToConvert.ToString("HH:mm:ss");

            string insertQuery = $@"
                INSERT INTO appointments
                (user_id, username, student_id, appointment_date, appointment_time, email, phone_number,
                 purpose_of_visit, known_allergies, current_medication, previous_visit,
                 emergency_contact_name, emergency_contact_phone, status, created_at, current_symptoms)
                VALUES
                ({userId}, '{username}', '{studentId}', '{selectedDate:yyyy-MM-dd}', '{dbTimeFormat}',
                '{email}', '{phone}', '{purpose}', '{allergies}', '{medication}', '{prevVisit}',
                '{emergencyName}', '{emergencyPhone}', 'Pending',
                '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{symptoms}')";

            dbManager.sqlManager(insertQuery);
        }

        public void LogBookingActivity(int userId, string purpose)
        {
            string logQuery = $@"
                INSERT INTO student_activity_log (user_id, activity_type, activity_desc)
                VALUES ({userId}, 'Appointment', 'Booking appointment for {purpose}')";
            dbManager.sqlManager(logQuery);
        }

        public int GetUserId(string username)
        {
            string query = $"SELECT user_id FROM users WHERE username = '{username}'";
            DataTable dt = dbManager.displayRecords(query);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["user_id"]) : -1;
        }
        public void UpdateAppointmentRecord(string appointmentID, int userId, string studentId,
            DateTime selectedDate, string selectedTime, string purpose, string allergies,
            string medication, string previousVisit, string emergencyName, string emergencyPhone, string symptoms)
        {
            DateTime timeToConvert;
            if (!DateTime.TryParse(selectedTime, out timeToConvert))
            {
                MessageBox.Show("Error parsing selected time for database update.");
                return;
            }

            string dbTimeFormat = timeToConvert.ToString("HH:mm:ss");

            string UpdateQuery = $@"
                UPDATE appointments 
                SET student_id = '{studentId}', 
                    user_id = {userId}, 
                    appointment_date = '{selectedDate:yyyy-MM-dd}', 
                    appointment_time = '{dbTimeFormat}', 
                    purpose_of_visit = '{purpose}', 
                    known_allergies = '{allergies}', 
                    current_medication = '{medication}', 
                    previous_visit = '{previousVisit}', 
                    emergency_contact_name = '{emergencyName}', 
                    emergency_contact_phone = '{emergencyPhone}', 
                    current_symptoms = '{symptoms}', 
                    status = 'Pending'
                WHERE appointment_id = {appointmentID}";

            dbManager.sqlManager(UpdateQuery);
        }
        public void LogUpdateActivity(int userId, string appointmentID)
        {
            string logQuery = $@"
                INSERT INTO student_activity_log (user_id, activity_type, activity_desc)
                VALUES ({userId}, 'Appointment', 'Update appointment ID:{appointmentID}')";
            dbManager.sqlManager(logQuery);
        }
    }
}
