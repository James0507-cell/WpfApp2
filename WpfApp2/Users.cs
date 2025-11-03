using System;
using System.Data;

namespace WpfApp2
{
    internal class Users : UserBase
    {
        private string BMI = "";
        private string Height = "";
        private string Weight = "";
        private string Name = "";

        public override string getUsername()
        {
            return base.getUsername();
        }

        public override void setId()
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }

        public override int getID()
        {
            setId();
            return id;
        }

        public void setName()
        {
            SQL = $"select * from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            Name = "WelCome Back " + dt.Rows[0]["first_name"].ToString()
                                     + " "
                                     + dt.Rows[0]["last_name"].ToString();
        }

        public string getName()
        {
            setName();
            return Name;
        }


        public void setHeight()
        {
            setId();
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = dbManager.displayRecords(SQL);
            if (dt.Rows.Count > 0) Height = dt.Rows[0]["height_cm"].ToString() + " cm";
        }

        public string getHeight()
        {
            setHeight();
            return Height;
        }

        public void setWeight()
        {
            setId();
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = dbManager.displayRecords(SQL);
            if (dt.Rows.Count > 0) Weight = dt.Rows[0]["weight_kg"].ToString() + " kg";
        }

        public string getWeight()
        {
            setWeight();
            return Weight;
        }

        public void setBMI()
        {
            setId();
            SQL = $"Select * from checkups where user_id = '{id}' order by checkup_id desc limit 1";
            DataTable dt = dbManager.displayRecords(SQL);
            if (dt.Rows.Count > 0) BMI = dt.Rows[0]["bmi"].ToString();
        }

        public string getBMI()
        {
            setBMI();
            return BMI;
        }

    }
}
