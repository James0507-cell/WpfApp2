using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class Users
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
            // MessageBox.Show("Connection Successful");
            dbConn.Close();
        }
        public DataTable displayRecords(String stQuerry)
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

    }
}
