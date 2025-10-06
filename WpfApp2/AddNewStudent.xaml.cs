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
            String firstname = txtFirstName.Text;
            String lastname = txtLastName.Text;
            String studentid = txtStudentID.Text;
            String emailaddress = txtEmailAddress.Text;
            String phonenumber = txtPhoneNumber.Text;
            String address = txtAddress.Text;
            String date = dpDatePicker.Text;

            String course = cboCourse.Text;
            String yearlevel = cboYearLevel.Text;
            String enrolledstatus = cboEnrolledStatus.Text;
            String username = txtUsername.Text;
            String password = txtPassword.Text;



            String emergencycontactname = txtECN1.Text;
            String emergencycontactperson = txtECP.Text;
            String bloodtype = cboBloodType.Text;
            String allergies = txtKnownAllergies.Text;
            String existingmedicalconditions = txtMedicalConditions.Text;

            string connStr = "server=localhost;user id=root;password=;database=db_medicaremmcm";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO users " +
                                 "(student_id, first_name, last_name, username, password, date_of_birth, email, phone_number, address, course_program, year_level, enrollment_status, blood_type) " +
                                 "VALUES (@student_id, @first_name, @last_name, @username, @password, @date_of_birth, @email, @phone_number, @address, @course_program, @year_level, @enrollment_status, @blood_type)";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // add values
                    cmd.Parameters.AddWithValue("@student_id", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@date_of_birth", dpDatePicker.SelectedDate?.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                    cmd.Parameters.AddWithValue("@phone_number", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@course_program", cboCourse.Text);
                    cmd.Parameters.AddWithValue("@year_level", cboYearLevel.Text);
                    cmd.Parameters.AddWithValue("@enrollment_status", cboEnrolledStatus.Text);
                    cmd.Parameters.AddWithValue("@blood_type", cboBloodType.Text);

                    //cmd.Parameters.AddWithValue("@allergies", txtKnownAllergies.Text);
                    //cmd.Parameters.AddWithValue("@medical", txtMedicalConditions.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student record inserted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); 
                }


            }
        }
    }
}
