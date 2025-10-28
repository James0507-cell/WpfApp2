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
        private int id = 0;
        private String username = MainWindow.Username;

        public void setId()
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }
        public int getID()
        {
            setId();
            return id;
        }

        
        

    }
}