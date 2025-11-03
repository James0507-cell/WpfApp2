using System.Data;

namespace WpfApp2
{
    internal abstract class UserBase
    {
        protected dbManager dbManager = new dbManager();
        protected string SQL = "";
        protected int id = 0;
        protected string username = MainWindow.Username;

        public virtual void setId()
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }

        public virtual int getID()
        {
            setId();
            return id;
        }

        public virtual string getUsername()
        {
            return username;
        }
    }
}
