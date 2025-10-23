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
  
            string sql = "SELECT *FROM users WHERE role = 'Student' and enrollment_status = 'Enrolled'";
            DataTable dt = displayRecords(sql);
            return dt.Rows.Count;



        }

        public int GetTotalStudentCount()
        {
          
                string sql = "SELECT *FROM users WHERE role != 'Admin'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }

        public int GetMedicineStatusCount()
        {
          
               
            string sql = "SELECT *FROM medicinerequests WHERE status = 'Pending'";
            DataTable dt = displayRecords(sql);
            return dt.Rows.Count;


        }

        public int GetAppointmenCount()
        {
           
                
                string sql = "SELECT *FROM appointments WHERE status = 'Pending'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }

        public int getMedicineCount()
        {
           
                string sql = "SELECT *FROM medicineinventory WHERE amount < 20";
                DataTable dt = displayRecords(sql);
               return dt.Rows.Count;

        }

        public int getTotalProgram()
        {
          
                
                string sql = "SELECT *FROM users WHERE role = 'Student'";
            DataTable dt = displayRecords(sql);
            return dt.Rows.Count;

        }

        public int Accountancy()
        {
          
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Accountancy' AND role = 'Student'";
            DataTable dt = displayRecords(sql);
            return dt.Rows.Count;

        }

        public int ManagementAccounting()
        {
         
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Management Accounting' AND role = 'Student'";
            DataTable dt = displayRecords(sql);
            return dt.Rows.Count;

        }

        public int Entrepreneurship()
        {
         
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Entrepreneurship' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }


        public int TourismManagement()
        {
          
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Tourism Management' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }

        public int Communication()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Arts in Communication' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int MultiMediaArts()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Multimedia Arts' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }

        public int ComputerScience()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Computer Science' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }

        public int Informationsystem()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Information Systems' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }
        public int entertaimentmultimediacomputing()
        {
          
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Entertainment & Multimedia Computing' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }

        public int architecture()
        {
          
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Architecture' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;

        }

        public int chemicalEnginerring()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Chemical Engineering' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int civilEngineering()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Civil Engineering' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int computerEngineering()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Computer Engineering' AND role = 'Student'";
               DataTable dt = displayRecords(sql);
                return dt.Rows.Count; 
        }
        public int electricalEngineering()
        {
            
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Electrical Engineering' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int electronicsEngineering()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Electronics Engineering' AND role = 'Student'";
               DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }
        public int industrialEngineering()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Industrial Engineering' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int mechanicalEngineering()
        {
            
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Mechanical Engineering' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int biology()
        {
         
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Biology' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int pharmacy()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Pharmacy' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int physicalteraphy()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Physical Therapy' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }

        public int psychology()
        {
           
                string sql = "SELECT *FROM users WHERE course_program = 'Bachelor of Science in Psychology' AND role = 'Student'";
                DataTable dt = displayRecords(sql);
                return dt.Rows.Count;
        }
    }
}