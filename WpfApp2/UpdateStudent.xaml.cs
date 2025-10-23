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
        String username = "";
        int userId = 0;
        int id;
        String adminUsername = MainWindow.Username;
        public UpdateStudent(String username)
        {
            this.username = username;
            InitializeComponent();
        }
        
        
        private void LoadStudentInfo()
        {
            SQL = $"SELECT * FROM users WHERE username = '{username}'";
            DataTable dt = admin.displayRecords(SQL);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No student found for this username.");
                return;
            }

            DataRow row = dt.Rows[0];

            userId = Convert.ToInt32(row["user_id"]);
            txtConfirmStudentID.Text = row["student_id"].ToString();
            txtFirstName.Text = row["first_name"].ToString();
            txtLastName.Text = row["last_name"].ToString();
            txtUsername.Text = row["username"].ToString();
            txtPassword.Text = row["password"].ToString();
            txtEmailAddress.Text = row["email"].ToString();
            txtPhoneNumber.Text = row["phone_number"].ToString();
            txtAddress.Text = row["address"].ToString();
            txtECN1.Text = row["emergency_contact_name"].ToString();
            txtECP.Text = row["emergency_contact_phone"].ToString();
            txtKnownAllergies.Text = row["known_allergies"].ToString();
            txtMedicalConditions.Text = row["medical_conditions"].ToString();



            if (DateTime.TryParse(row["date_of_birth"].ToString(), out DateTime dob))
            {
                dpDatePicker.SelectedDate = dob;
            }

            string courseProgram = row["course_program"].ToString().Trim();
            string yearLevel = row["year_level"].ToString().Trim();
            string status = row["enrollment_status"].ToString().Trim();
            string bloodType = row["blood_type"].ToString().Trim();
            string role = row["role"].ToString().Trim();

            cboCourse.Text = courseProgram;
            cboYearLevel.Text = yearLevel;
            cboEnrolledStatus.Text = status;
            cboBloodType.Text = bloodType;
            cmbRole.Text = role;
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            LoadComboBoxes();
            LoadStudentInfo();

            setId(adminUsername);

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

        private void txtEmailAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void setId(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = admin.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
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

        private void btnUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            SQL = $@"
                UPDATE users SET 
                student_id = '{txtConfirmStudentID.Text}', 
                first_name = '{txtFirstName.Text}', 
                last_name = '{txtLastName.Text}', 
                username = '{txtUsername.Text}', 
                password = '{txtPassword.Text}', 
                date_of_birth = '{dpDatePicker.SelectedDate:yyyy-MM-dd}', 
                email = '{txtEmailAddress.Text}', 
                phone_number = '{txtPhoneNumber.Text}', 
                address = '{txtAddress.Text}', 
                course_program = '{cboCourse.SelectedValue}', 
                year_level = '{cboYearLevel.SelectedValue}', 
                enrollment_status = '{cboEnrolledStatus.SelectedValue}', 
                blood_type = '{cboBloodType.Text}', 
                role = '{cmbRole.Text}',
                emergency_contact_name = '{txtECN1.Text}',
                emergency_contact_phone = '{txtECP.Text}',
                known_allergies = '{txtKnownAllergies.Text}',
                medical_conditions = '{txtMedicalConditions.Text}'
                WHERE user_id = {userId}";

            admin.sqlManager(SQL);
            MessageBox.Show("Student information updated successfully!");

            SQL = $"INSERT INTO admin_activity_log (admin_id, username, activity_type, activity_desc, activity_date) " +
                     $"VALUES ({id}, '{adminUsername}', 'Update Student Info', 'Update Student {txtConfirmStudentID.Text}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            admin.sqlManager(SQL);
        }
    }
}
