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
    public MainWindow()
    {
        InitializeComponent();
    }
    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        
    }
    private void BtnSignIn_Click(object sender, RoutedEventArgs e)
    {
        string username = TxtUserName.Text;
        string password = TxtPassword.Text;

        if (username.Equals("admin") && password.Equals("admin123"))
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Hide();
            //UserForm userForm = new UserForm();
            //userForm.Show();
            //this.Hide();
        }
        else
        {
           MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    


}