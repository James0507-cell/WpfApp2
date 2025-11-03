using System.Data;

namespace WpfApp2
{
    internal class Admin : UserBase
    {
        public override void setId()
        {
            SQL = $"select user_id from users where username = '{username}' AND role='admin'";
            DataTable dt = dbManager.displayRecords(SQL);

            if (dt.Rows.Count > 0)
            {
                id = int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                base.setId();
            }
        }

        public override string getUsername()
        {
            return "ADMIN: " + base.getUsername();
        }

        public int getID(string username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);

            return (dt.Rows.Count > 0)
                ? int.Parse(dt.Rows[0][0].ToString())
                : -1;
        }
    }
}
