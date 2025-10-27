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
        dbManager dbManager = new dbManager();
        private String SQL = "";

        public int getID(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public int GetActiveStudentCount()
        {
            string sql = "SELECT *FROM users WHERE role = 'Student' and enrollment_status = 'Enrolled'";
            DataTable dt = dbManager.displayRecords(sql);
            return dt.Rows.Count;
        }

        public int GetTotalStudentCount()
        {
            string sql = "SELECT *FROM users WHERE role != 'Admin'";
            DataTable dt = dbManager.displayRecords(sql);
            return dt.Rows.Count;
        }

        public int GetMedicineStatusCount()
        {
            string sql = "SELECT *FROM medicinerequests WHERE status = 'Pending'";
            DataTable dt = dbManager.displayRecords(sql);
            return dt.Rows.Count;
        }

        public int GetAppointmenCount()
        {
            string sql = "SELECT *FROM appointments WHERE status = 'Pending'";
            DataTable dt = dbManager.displayRecords(sql);
            return dt.Rows.Count;
        }

        public int getMedicineCount()
        {
            string sql = "SELECT *FROM medicineinventory WHERE amount < 20";
            DataTable dt = dbManager.displayRecords(sql);
            return dt.Rows.Count;
        }

        public int getTotalProgram()
        {
            string sql = "SELECT *FROM users WHERE role = 'Student'";
            DataTable dt = dbManager.displayRecords(sql);
            return dt.Rows.Count;
        }

    }
}