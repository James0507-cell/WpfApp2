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

        public void clear()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtStudentID.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1; 
            txtEmailAddress.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            dpDatePicker.SelectedDate = null;
            cboCourse.SelectedIndex = -1;
            cboYearLevel.SelectedIndex = -1;
            cboEnrolledStatus.SelectedIndex = -1;
            txtECN1.Clear();
            txtECP.Clear();
            cboBloodType.SelectedIndex = -1;
            txtKnownAllergies.Clear();
            txtMedicalConditions.Clear();
        }


        public void setId(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = admin.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
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
            setId(username);

            LoadComboBoxes();
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            Admin createStudent = new Admin();

            
            SQL = $@"
                INSERT INTO users (
                    student_id, 
                    first_name, 
                    last_name, 
                    username, 
                    password, 
                    date_of_birth, 
                    email, 
                    phone_number, 
                    address, 
                    course_program, 
                    year_level, 
                    enrollment_status, 
                    blood_type, 
                    role, 
                    emergency_contact_name,
                    emergency_contact_phone,
                    known_allergies,
                    medical_conditions
                ) VALUES (
                    '{txtStudentID.Text}', 
                    '{txtFirstName.Text}', 
                    '{txtLastName.Text}', 
                    '{txtUsername.Text}', 
                    '{txtPassword.Text}', 
                    '{dpDatePicker.SelectedDate:yyyy-MM-dd}', 
                    '{txtEmailAddress.Text}', 
                    '{txtPhoneNumber.Text}', 
                    '{txtAddress.Text}', 
                    '{cboCourse.SelectedValue}', 
                    '{cboYearLevel.SelectedValue}', 
                    '{cboEnrolledStatus.SelectedValue}', 
                    '{cboBloodType.Text}', 
                    '{cmbRole.Text}',
                    '{txtECN1.Text}',
                    '{txtECP.Text}',
                    '{txtKnownAllergies.Text}',
                    '{txtMedicalConditions.Text}'

                )";
            admin.sqlManager(SQL);
            MessageBox.Show("New Student added successfully!");


            student.displayUsers("SELECT * FROM users WHERE role = 'Student'");
            SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{username}', 'Add New Student', 'Add new Student + {txtUsername.Text}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            admin.sqlManager(SQL);
            clear();


        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

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
        private void LoadComboBoxes()
        {
            DataTable dtCourse = admin.displayRecords("SELECT * FROM course_programs");
            foreach (DataRow row in dtCourse.Rows)
            {
                cboCourse.Items.Add(row["course_name"].ToString());
            }

            DataTable dtYear = admin.displayRecords("SELECT * FROM year_levels");
            foreach (DataRow row in dtYear.Rows)
            {
                cboYearLevel.Items.Add(row["level_name"].ToString());
            }

            DataTable dtStatus = admin.displayRecords("SELECT * FROM enrollment_statuses");
            foreach (DataRow row in dtStatus.Rows)
            {
                cboEnrolledStatus.Items.Add(row["status_name"].ToString());
            }

            DataTable dt_blood = admin.displayRecords("SELECT * FROM blood_types");
            foreach (DataRow row in dt_blood.Rows)
            {
                cboBloodType.Items.Add(row["blood_type"].ToString());
            }

            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Student");
        }
    }
}
