using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Documents;

namespace WpfApp2
{
    internal class Admin
    {
        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;

        private string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";


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




        public int GetActiveStudentCount()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE role = 'Student' and enrollment_status = 'Enrolled'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }

        public int GetTotalStudentCount()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE role != 'Admin'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetMedicineStatusCount()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM medicinerequests WHERE status = 'Pending'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetAppointmenCount()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM appointments WHERE status = 'Pending'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int getMedicineCount()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM medicineinventory WHERE amount < 20";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int getTotalProgram()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Accountancy()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Accountancy' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int ManagementAccounting()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Management Accounting' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Entrepreneurship()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Entrepreneurship' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        public int TourismManagement()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Tourism Management' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Communication()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Arts in Communication' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int MultiMediaArts()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Multimedia Arts' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int ComputerScience()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Computer Science' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Informationsystem()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Information Systems' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int entertaimentmultimediacomputing()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Entertainment & Multimedia Computing' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int architecture()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Architecture' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int chemicalEnginerring()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Chemical Engineering' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int civilEngineering()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Civil Engineering' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int computerEngineering()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Computer Engineering' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int electricalEngineering()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Electrical Engineering' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int electronicsEngineering()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Electronics Engineering' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int industrialEngineering()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Industrial Engineering' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int mechanicalEngineering()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Mechanical Engineering' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int biology()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Biology' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int pharmacy()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Pharmacy' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int physicalteraphy()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Physical Therapy' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int psychology()
        {
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE course_program = 'Bachelor of Science in Psychology' AND role = 'Student'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}