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

    public partial class UpdateStudent : Window
    {
        dbManager dbManager;
        private String SQL = "";
        Admin admin = new Admin();
        private String username = "";
        private int userId;
        private int id;
        String adminUsername = MainWindow.Username;
        StudentManagement studentManagement = new StudentManagement();
        AdminStudent adminStudent;

        public UpdateStudent(String username)
        {
            this.username = username;
            InitializeComponent();
        }
        
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LoadComboBoxes();
            LoadStudentInfo();
            id = adminStudent.setId(username);
            adminStudent = new AdminStudent(id);

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
            studentManagement.Show();
            this.Close();
        }

        private void btnUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            AdminStudent adminStudent = new AdminStudent(id);

            adminStudent.UpdateStudent(
            userId,
            txtConfirmStudentID.Text,
            txtFirstName.Text,
            txtLastName.Text,
            txtUsername.Text,
            txtPassword.Text,
            dpDatePicker.SelectedDate,
            txtEmailAddress.Text,
            txtPhoneNumber.Text,
            txtAddress.Text,
            cboCourse.SelectedValue?.ToString(),
            cboYearLevel.SelectedValue?.ToString(),
            cboEnrolledStatus.SelectedValue?.ToString(),
            cboBloodType.Text,
            cmbRole.Text,
            txtECN1.Text,
            txtECP.Text,
            txtKnownAllergies.Text,
            txtMedicalConditions.Text
        );

            MessageBox.Show("Student information updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            adminStudent.LogUpdateStudentAction(
                id,
                adminUsername,
                "Update Student Info",
                $"Update Student {txtConfirmStudentID.Text}"
            );
            studentManagement.displayUsers("SELECT * FROM users WHERE role = 'Student'");

        }
        
        private void LoadComboBoxes()
        {
            DataTable dtCourse = dbManager.displayRecords("SELECT * FROM course_programs");
            foreach (DataRow row in dtCourse.Rows)
            {
                cboCourse.Items.Add(row["course_name"].ToString());
            }

            DataTable dtYear = dbManager.displayRecords("SELECT * FROM year_levels");
            foreach (DataRow row in dtYear.Rows)
            {
                cboYearLevel.Items.Add(row["level_name"].ToString());
            }

            DataTable dtStatus = dbManager.displayRecords("SELECT * FROM enrollment_statuses");
            foreach (DataRow row in dtStatus.Rows)
            {
                cboEnrolledStatus.Items.Add(row["status_name"].ToString());
            }

            DataTable dt_blood = dbManager.displayRecords("SELECT * FROM blood_types");
            foreach (DataRow row in dt_blood.Rows)
            {
                cboBloodType.Items.Add(row["blood_type"].ToString());
            }

            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Student");
        }
        private void LoadStudentInfo()
        {
            SQL = $"SELECT * FROM users WHERE username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);

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
    }
}
