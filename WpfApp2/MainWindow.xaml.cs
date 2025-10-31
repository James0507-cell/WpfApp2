using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        Login login = new Login();
        public static string Username { get; private set; }
        private string password = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click_1(object sender, RoutedEventArgs e)
        {

            Username = txtusername.Text.Trim();
            password = txtpassword.Text.Trim();

            string role = login.loginUser(Username, password);

            if (role == "Admin")
            {
                AdminWindow adminForm = new AdminWindow();
                adminForm.Show();
                this.Close();
            }
            else if (role == "Student")
            {
                UserForm studentForm = new UserForm();
                studentForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
