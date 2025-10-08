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
   internal class Booking
    {
        private MySqlConnection dbCon;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;


       private int userId;
        private int studentId;

        String strConn = "server = localhost; user id = root; password =; database = db_medicaremmcm";

        public void dbConnection()
        {
            dbCon = new MySqlConnection(strConn);
            dbCon.Open();
            MessageBox.Show("Connection Successful");
            dbCon.Close();
        }
        public DataTable displayRecords(String strQuery)
        {
            dbCon = new MySqlConnection(strConn);
            dbCon.Open();
            da = new MySqlDataAdapter(strQuery, dbCon);
            dt = new DataTable();
            da.Fill(dt);
            dbCon.Close();
            return dt;
        }
        public void sqlManager(String strQuery)
        {
            dbCon.Open();
            dbCommand = new MySqlCommand(strQuery, dbCon);
            dbCommand.ExecuteNonQuery();
            dbCon.Close();
        }
        public Boolean isFound (String query, String username) //admin, medicine
        {
            //string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";
            //string getUserQuery = $"SELECT user_id, student_id FROM users WHERE username = '{username}' LIMIT 1";
           Boolean found = false;
           dbCon.Open();
            MySqlCommand cmd = new MySqlCommand(query, dbCon);
            MySqlDataReader reader = cmd.ExecuteReader();
            //da= new MySqlDataAdapter();
            //dt = new DataTable();
            //da.Fill(dt);

            //useriddt.Rows.Clear();

      
                    if (reader.Read())
                    { 
                        //userId = reader.GetInt32("user_id");
                       // studentId = reader.GetString("student_id");
                found= true;
                    }
                    else
                    {
                        MessageBox.Show("User not found in database.");
                //return false;
                    }
                
     
                dbCon.Close();
            return found;
           
        }
    }
}
