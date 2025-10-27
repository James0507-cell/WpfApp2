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
        private int userId;
        private int studentId;

        public List<string> GetBookedTimes(DateTime date)
        {
            var bookedTimes = new List<string>();
            string dateString = date.ToString("yyyy-MM-dd");

            string query = $@"
                            SELECT appointment_time 
                            FROM appointments 
                            WHERE appointment_date = '{dateString}' 
                            AND (status = 'Pending' OR status = 'Approved')";

            try
            {
                DataTable dt = dbManager.displayRecords(query);

                foreach (DataRow row in dt.Rows)
                {
                    string dbTime = row["appointment_time"].ToString();

                    if (DateTime.TryParse(dbTime, out DateTime parsedTime))
                    {
                        bookedTimes.Add(parsedTime.ToString("h:mm tt", System.Globalization.CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching booked times: {ex.Message}");
            }

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
