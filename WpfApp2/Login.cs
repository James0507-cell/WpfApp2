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
        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;

        String strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";

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
        public string loginUser(string username, string password)
        {
            string role = null;

            using (MySqlConnection dbConn = new MySqlConnection(strConn))
            {
                dbConn.Open();

                string strQuery = "SELECT role FROM users WHERE username = @username AND password = @password";
                using (MySqlCommand dbCommand = new MySqlCommand(strQuery, dbConn))
                {
                    dbCommand.Parameters.AddWithValue("@username", username);
                    dbCommand.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = dbCommand.ExecuteReader())  
                    {
                        if (reader.Read())
                        {
                            role = reader["role"].ToString();
                        }
                    }
                }
            }

            return role; 
        }
    }
}

