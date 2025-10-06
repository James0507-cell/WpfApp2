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
    }
}
