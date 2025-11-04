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
                
        public String loginUser(String username, String password)
        {
            String sql = $"SELECT * FROM users WHERE BINARY username = '{username}' AND BINARY password = '{password}'";
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

