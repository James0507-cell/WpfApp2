using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media.Effects;
using System.Windows.Data;

namespace WpfApp2
{
    public partial class StudentManagement : Window
    {
        dbManager dbManager = new dbManager();
        Admin admin = new Admin();
        String SQL = "";
        String Username = MainWindow.Username;
        int id;
        String studUsername = "";
        AdminStudent adminStudent = new AdminStudent();

        public StudentManagement()
        {
            InitializeComponent();
        }

        public void displayUsers(String strQuerry)
        {
            StackPanel targetStackPanel = this.StudentListPanel;

            targetStackPanel.Children.Clear();
            DataTable dt = dbManager.displayRecords(strQuerry);

            int num = dt.Rows.Count;
            for (int i = 0; i < num; i++)
            {
                String username = dt.Rows[i]["username"].ToString();
                String email = dt.Rows[i]["email"].ToString();
                String adress = dt.Rows[i]["address"].ToString();
                String StudentId = dt.Rows[i]["student_id"].ToString();
                String course = dt.Rows[i]["course_program"].ToString();
                String year = dt.Rows[i]["year_level"].ToString();
                String firstname = dt.Rows[i]["first_name"].ToString();
                String lastname = dt.Rows[i]["last_name"].ToString();
                String fullname = firstname + " " + lastname;
                String phone = dt.Rows[i]["phone_number"].ToString();
                studUsername = username;


                Border cardBorder = adminStudent.studentPanel(username, email, adress, StudentId, course, year, fullname, phone);
                targetStackPanel.Children.Add(cardBorder);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewStudent addNewStudent = new AddNewStudent();
            this.Close();
            addNewStudent.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQL = "SELECT * FROM users WHERE student_id LIKE '%" + txtSearch.Text + "%'";
            displayUsers(SQL);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            displayEnrolledCourse("Select * from course_programs");
            setId(Username);
            displayUsers("SELECT * FROM users WHERE role = 'Student'");
            displayAnalytics();
            setComboBox();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQL = "SELECT * FROM users WHERE student_id LIKE '%" + txtSearch.Text + "%'";
            displayUsers(SQL);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            displayEnrolledCourse("Select * from course_programs");
        }
        public void setId(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = dbManager.displayRecords(SQL);
            id = int.Parse(dt.Rows[0][0].ToString());
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if(txtSearch.Text == "Search student ID..." || string.IsNullOrWhiteSpace(txtSearch.Text))
                return;
            string SQL = "SELECT * FROM users WHERE student_id LIKE '%" + txtSearch.Text + "%'";
            displayUsers(SQL);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search student ID...")
                txtSearch.Text = "";

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                txtSearch.Text = "Search student ID...";

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String yearLevel = cmbYear.SelectedItem as String;
            SQL = "SELECT * FROM users WHERE year_level LIKE '%" + yearLevel + "%'";
            displayUsers(SQL);
            if (yearLevel == "All Year Levels")
            {
                SQL = "SELECT * FROM users WHERE role = 'Student'";
                displayUsers(SQL);
            }
        }
        
        public void displayEnrolledCourse(string sqlQuery)
        {
            StackPanelCourses.Children.Clear();

            DataTable dt = dbManager.displayRecords(sqlQuery);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string course = dt.Rows[i]["course_name"].ToString();

                    DataTable dtStudentCount = dbManager.displayRecords(
                        $"SELECT * FROM users WHERE course_program = '{course}'"
                    );
                    int studentCount = dtStudentCount.Rows.Count;

                    Border border = adminStudent.coursePanel(course, studentCount);

                    StackPanelCourses.Children.Add(border);
                }
            }
        }
        public void displayAnalytics()
        {
            lblTotalstudent.Content = adminStudent.GetTotalStudentCount();
            lblPrograms.Content = adminStudent.getTotalProgram();
            lblActiveStudent.Content = adminStudent.GetActiveStudentCount();
        }
        public void setComboBox()
        {
            DataTable dtYear = dbManager.displayRecords("SELECT * FROM year_levels");
            foreach (DataRow row in dtYear.Rows)
            {
                cmbYear.Items.Add(row["level_name"].ToString());
            }
            cmbYear.Items.Insert(0, "All Year Levels");
        }

    }
}
