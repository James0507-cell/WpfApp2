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
        dbManager dbManager = new dbManager();
        Admin admin = new Admin();
        StudentManagement studentManagement = new StudentManagement();
        AdminStudent adminStudent;
        public AddNewStudent()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            adminStudent = new AdminStudent();
            LoadComboBoxes();
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            AdminStudent adminStudent = new AdminStudent();

            adminStudent.AddStudent(
                    txtStudentID.Text,
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtUsername.Text,
                    txtPassword.Text,
                    dpDatePicker.SelectedDate ?? DateTime.Now,
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

            adminStudent.LogAddStudentAdminAction(txtUsername.Text);
            MessageBox.Show("New student added successfully!");
            clear();
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

        

    }
}
