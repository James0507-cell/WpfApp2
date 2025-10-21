using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AddNewStudent.xaml
    /// </summary>
    public partial class AddNewStudent : Window
    {


        StudentManagement student = new StudentManagement();
        Admin admin = new Admin();
        String SQL = "";
        String username = MainWindow.Username;
        int id;
        public AddNewStudent()
        {
            InitializeComponent();
        }


        public void setId(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = admin.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setId(username);

        }

        private void txtStudentID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEmailAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            DataTable dtCourse = admin.displayRecords("Select *from course_programs");
            foreach( DataRow row in dtCourse.Rows)
            {

                cboCourse.Items.Add(row["course_name"].ToString());
            }

            cboYearLevel.Items.Add("1st Year");
            cboYearLevel.Items.Add("2nd Year");
            cboYearLevel.Items.Add("3rd Year");
            cboYearLevel.Items.Add("4th Year");

            cboEnrolledStatus.Items.Add("Enrolled");
            cboEnrolledStatus.Items.Add("Pending");
            cboEnrolledStatus.Items.Add("Dropped");
            cboEnrolledStatus.Items.Add("Graduated");

            DataTable dt_blood = admin.displayRecords("Select * from blood_types");
            foreach (DataRow row in dt_blood.Rows)
            {
                cboBloodType.Items.Add(row["blood_type"].ToString());
            }

            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Student");
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            Admin createStudent = new Admin();

            

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
            SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{username}', 'Appointment Approved', 'Add new Student + {txtUsername.Text}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            admin.sqlManager(SQL);


        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            //hjgjhgjhgjhgjhg

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtFirstName_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void cboYearLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackButton2_Click(object sender, RoutedEventArgs e)
        {
            if (MyTabAddStudent.SelectedIndex > 0) MyTabAddStudent.SelectedIndex--;

        }

        private void NextButton3_Click(object sender, RoutedEventArgs e)
        {
            if (MyTabAddStudent.SelectedIndex < 2) MyTabAddStudent.SelectedIndex++;

        }
        private void BackButton3_Click(object sender, RoutedEventArgs e)
        {
            if (MyTabAddStudent.SelectedIndex > 0) MyTabAddStudent.SelectedIndex--;
        }

        private void NextButton1_Click(object sender, RoutedEventArgs e)
        {
            if (MyTabAddStudent.SelectedIndex < 2) MyTabAddStudent.SelectedIndex++;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
