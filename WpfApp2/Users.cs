using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    internal class Users
    {
        dbManager dbManager = new dbManager();
        private String username = MainWindow.Username;
        private int id = 0;
        private String SQL = "";
        private String BMI = "";
        private String Height = "";
        private String Weight = "";
        private String Name = "";

        public String getUsername() { 
            return username; 
        }

        public void setId()
        {
            String SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }
        public int getID() {
            setId();
            return id; 
        }

        public void setName()
        {
            SQL = $"select * from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            Name = "WelCome Back " + dt.Rows[0]["first_name"].ToString() + " " + dt.Rows[0]["last_name"].ToString();
        }

        public String getName()
        {
            setName();
            return Name;
        }
        public String CheckUpDate()
        {
            setId ();
            SQL = $"SELECT * FROM checkups WHERE user_id = '{id}' ORDER BY checkup_id DESC LIMIT 1";
            DataTable dt = dbManager.displayRecords(SQL);

            if (dt.Rows.Count > 0)
            {
                DateTime recordedDate = Convert.ToDateTime(dt.Rows[0]["recorded_at"]);
                return recordedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                return "No Checkup record found.";
            }
        }
        public void setHeight()
        {
            setId();
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = dbManager.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                Height = dt.Rows[0]["height_cm"].ToString() +" cm";
            }
        }
        public String getHeight()
        {
            setHeight();
            return Height;
        }
        public void setWeight()
        {
            setId();
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = dbManager.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                Weight = dt.Rows[0]["weight_kg"].ToString() + " kg";
            }
        }
        public String getWeight()
        {
            setWeight();
            return Weight;
        }
        public void setBMI ()
        {
            setId();
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = dbManager.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                BMI = dt.Rows[0]["bmi"].ToString();  
            }  
        }
        public String getBMI()
        {
            setBMI();
            return BMI;
        }
        

        public int getUserId(String username)
        {
            SQL = $"Select user_id from users where username = '{username}'";
            DataTable dt = new DataTable();
            dt = dbManager.displayRecords(SQL);
            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }
    }
}
