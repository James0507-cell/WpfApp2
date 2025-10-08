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
    internal class Admin
    {

        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;

        String strCOnn = "server = localhost; user id = root; password =; database = db_medicaremmcm";


        public void dbConnection()
        {
            dbConn = new MySqlConnection(strCOnn);
            dbConn.Open();
            MessageBox.Show("Connection Successful");
            dbConn.Close();
        }
        public DataTable displayRecors(String stQuerry)
        {
            dbConn = new MySqlConnection(strCOnn);
            dbConn.Open();
            da = new MySqlDataAdapter(stQuerry, dbConn);
            dt = new DataTable();
            da.Fill(dt);
            dbConn.Close();
            return dt;

        }
        public void sqlManager(String strQuesrry)
        {
            dbConn.Open();
            dbCommand = new MySqlCommand(strQuesrry, dbConn);
            dbCommand.ExecuteNonQuery();
            dbConn.Close();

        }
        private string connectionString = "server=localhost;user id=root;password=;database=db_medicaremmcm";

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
}
