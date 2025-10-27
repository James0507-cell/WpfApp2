using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
   internal class Login
    {
        dbManager dbManager = new dbManager();
        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;

        private String strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";

        public void dbConnection ()
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            dbConn.Close();
        }
        public DataTable displayRecords (String stQuerry)
        {
            dbConn.Open();
            da = new MySqlDataAdapter(stQuerry, dbConn);
            dt = new DataTable();
            da.Fill(dt);
            dbConn.Close();
            return dt;

        }

        public String loginUser(String username, String password)
        {
            String sql = $"SELECT * FROM users WHERE username = '{username}' AND password = '{password}'";
            DataTable dt = dbManager.displayRecords(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["role"].ToString() == "Student")
                    return "Student";
                else
                    return "Admin";
            }
            else
            {
                return "Invalid";
            }

        }
        
    }
}

