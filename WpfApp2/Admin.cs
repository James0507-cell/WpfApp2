using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace WpfApp2
{
    internal class Admin
    {
        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;

        private string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";
        private string connectionString = "server=localhost;user id=root;password=;database=db_medicaremmcm";

        public void dbConnection()
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            MessageBox.Show("Connection Successful");
            dbConn.Close();
        }

        public DataTable displayRecords(string query)
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            da = new MySqlDataAdapter(query, dbConn);
            dt = new DataTable();
            da.Fill(dt);
            dbConn.Close();
            return dt;
        }

        public void sqlManager(string query)
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            dbCommand = new MySqlCommand(query, dbConn);
            dbCommand.ExecuteNonQuery();
            dbConn.Close();
        }

        public void AddStudent(
            string studentID,
            string firstName,
            string lastName,
            string username,
            string password,
            DateTime? dateOfBirth,
            string email,
            string phone,
            string address,
            string course,
            string yearLevel,
            string enrollmentStatus,
            string bloodType
        )
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO users " +
                                 "(student_id, first_name, last_name, username, password, date_of_birth, email, phone_number, address, course_program, year_level, enrollment_status, blood_type) " +
                                 "VALUES (@student_id, @first_name, @last_name, @username, @password, @date_of_birth, @email, @phone_number, @address, @course_program, @year_level, @enrollment_status, @blood_type)";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@student_id", studentID);
                    cmd.Parameters.AddWithValue("@first_name", firstName);
                    cmd.Parameters.AddWithValue("@last_name", lastName);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@date_of_birth", dateOfBirth?.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone_number", phone);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@course_program", course);
                    cmd.Parameters.AddWithValue("@year_level", yearLevel);
                    cmd.Parameters.AddWithValue("@enrollment_status", enrollmentStatus);
                    cmd.Parameters.AddWithValue("@blood_type", bloodType);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student record inserted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }

  
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public override string ToString()
        {
            return CourseName; 
        }
    }

    public class CourseRepo
    {
        private string connStr = "server=localhost;user id=root;password=;database=db_medicaremmcm";

        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT course_id, course_name FROM course_programs";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseId = reader.GetInt32("course_id"),
                            CourseName = reader.GetString("course_name")
                        });
                    }
                }
            }

            return courses;
        }
    }
}
