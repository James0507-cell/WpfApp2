using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AddNewStudent.xaml
    /// </summary>
    public partial class AddNewStudent : Window
    {

        StudentManagement student = new StudentManagement();
        String strconn = "server=localhost;user id=root;password=;database=db_medicaremmcm";
        public AddNewStudent()
        {
            InitializeComponent();
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEmailAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            cboCourse.Items.Add("BS Computer Science");
            cboCourse.Items.Add("BS Civil Engineering");
            cboCourse.Items.Add("BS Information Technology");

            cboYearLevel.Items.Add("1st Year");
            cboYearLevel.Items.Add("2nd Year");
            cboYearLevel.Items.Add("3rd Year");
            cboYearLevel.Items.Add("4th Year");

            cboEnrolledStatus.Items.Add("Enrolled");
            cboEnrolledStatus.Items.Add("Pending");
            cboEnrolledStatus.Items.Add("Dropped");
            cboEnrolledStatus.Items.Add("Graduated");

            cboBloodType.Items.Add("A+");
            cboBloodType.Items.Add("A-");
            cboBloodType.Items.Add("B+");
            cboBloodType.Items.Add("B-");
            cboBloodType.Items.Add("AB+");
            cboBloodType.Items.Add("AB-");
            cboBloodType.Items.Add("O+");
            cboBloodType.Items.Add("O-");

            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Student");
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            CreateStudentAccount createStudent = new CreateStudentAccount();

            createStudent.AddStudent(
                txtStudentID.Text,
                txtFirstName.Text,
                txtLastName.Text,
                txtUsername.Text,
                txtPassword.Text,
                dpDatePicker.SelectedDate,
                txtEmailAddress.Text,
                txtPhoneNumber.Text,
                txtAddress.Text,
                cboCourse.Text,
                cboYearLevel.Text,
                cboEnrolledStatus.Text,
                cboBloodType.Text
            );

            student.displayUsers("SELECT * FROM users WHERE role = 'Student'");


        
        }
    }
}
