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

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            Username = TxtUserName.Text.Trim();
            password = TxtPassword.Text.Trim();

            string role = login.loginUser(Username, password);

            if (role == "Admin")
            {
                AdminWindow adminForm = new AdminWindow();
                adminForm.Show();
                this.Close();
            }
            else if (role == "Student")
            {
                // ✅ Pass username to UserForm
                UserForm studentForm = new UserForm(Username);
                studentForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void LoginForm_Loaded(object sender, RoutedEventArgs e)
        {
            login.dbConnection();
        }

        private void TxtUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtUserName.Text == "Enter Username")
                TxtUserName.Text = "";
        }

        private void TxtUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUserName.Text))
                TxtUserName.Text = "Enter Username";
        }

        private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPassword.Text == "Enter Password")
                TxtPassword.Text = "";
        }

        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtPassword.Text))
                TxtPassword.Text = "Enter Password";
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // You can leave it empty or handle text changes here if needed
        }

    }
}
