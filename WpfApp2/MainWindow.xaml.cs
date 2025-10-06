using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Login login = new Login();
    private String username = "";
    private String password = "";

    public MainWindow()
    {
        InitializeComponent();
    }

    // Add the following property to the MainWindow class to fix the CS1061 error.  
    public string Username { get; set; }
    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        
    }
    private void BtnSignIn_Click(object sender, RoutedEventArgs e)
    {
        username = TxtUserName.Text;
        password = TxtPassword.Text;

        string role = login.loginUser(username, password);

        if (role == "Admin")
        {
            AdminWindow adminForm = new AdminWindow();
            adminForm.Show();
        }
        else if (role == "Student")
        {
            UserForm studentForm = new UserForm();
            studentForm.Show();
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
        if(TxtUserName.Text == "Enter Username")
        {
            TxtUserName.Text = "";
        }
    }

    private void TxtUserName_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtUserName.Text))
        {
            TxtUserName.Text = "Enter Username";
        }
    }

    private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
    {
        if (TxtPassword.Text == "Enter Password")
        {
            TxtPassword.Text = "";
        }
    }

    private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtPassword.Text))
        {
            TxtPassword.Text = "Enter Password";
        }

    }
    public String  getUsername()
    {
        username = TxtUserName.Text;
        return username;
    }
}