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
    /// Interaction logic for UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Window
    {
        String SQL = "";
        Admin admin = new Admin();
        String username = MainWindow.Username;
        int userId = 0;
        public UpdateStudent()
        {
            InitializeComponent();
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            SQL = $@"
            UPDATE users SET 
            student_id = '{txtStudentID.Text}', 
            first_name = '{txtFirstName.Text}', 
            last_name = '{txtLastName.Text}', 
            username = '{txtUsername.Text}', 
            password = '{txtPassword.Text}', 
            date_of_birth = '{dpDatePicker.SelectedDate:yyyy-MM-dd}', 
            email = '{txtEmailAddress.Text}', 
            phone_number = '{txtPhoneNumber.Text}', 
            address = '{txtAddress.Text}', 
            course_program = '{cboCourse.Text}', 
            year_level = '{cboYearLevel.Text}', 
            enrollment_status = '{cboEnrolledStatus.Text}', 
            blood_type = '{cboBloodType.Text}' 
            WHERE user_id = {userId}";
            admin.sqlManager(SQL);
            MessageBox.Show("Student Information Updated!");

        }
        public void studentInfo ()
        {
            SQL = "SELECT * FROM users WHERE username = '" + username + "'";
            DataTable dt  = admin.displayRecords(SQL);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                userId = Convert.ToInt32(row["user_id"]);
                txtStudentID.Text = row["student_id"].ToString();
                txtFirstName.Text = row["first_name"].ToString();
                txtLastName.Text = row["last_name"].ToString();
                txtUsername.Text = row["username"].ToString();
                txtPassword.Text = row["password"].ToString();
                if (DateTime.TryParse(row["date_of_birth"].ToString(), out DateTime dob))
                {
                    dpDatePicker.SelectedDate = dob;
                }
                txtEmailAddress.Text = row["email"].ToString();
                txtPhoneNumber.Text = row["phone_number"].ToString();
                txtAddress.Text = row["address"].ToString();
                cboCourse.SelectedValue = row["course_program"].ToString();
                cboYearLevel.SelectedItem = row["year_level"].ToString();
                cboEnrolledStatus.SelectedItem = row["enrollment_status"].ToString();
                cboBloodType.SelectedValue = row["blood_type"];
            }

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            admin.dbConnection();
            DataTable dtCourse = admin.displayRecords("Select *from course_programs");
            cboCourse.ItemsSource = dtCourse.DefaultView;
            cboCourse.DisplayMemberPath = "course_name";
            cboCourse.SelectedValuePath = "course_name";

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
            studentInfo();

        }

        private void txtEmailAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
