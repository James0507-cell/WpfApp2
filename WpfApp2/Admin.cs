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
        private String SQL = "";
        private string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";


        public void dbConnection()
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
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

        public int getID(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = displayRecords(SQL);
            return int.Parse(dt.Rows[0][0].ToString());
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

    }
}