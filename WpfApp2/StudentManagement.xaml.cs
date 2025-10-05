using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    public partial class StudentManagement : Window
    {
        Admin admin = new Admin(); // use your Admin helper
        String SQL = "";
        public StudentManagement()
        {
            InitializeComponent();
            LoadStudentList(); 
        }

        private void LoadStudentList()
        {
            StudentListPanel.Children.Clear();

            SQL = "SELECT first_name, last_name, student_id, email, course_program, year_level FROM users WHERE role = 'Student'";
            DataTable dt = admin.displayRecors(SQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Border panel = CreateStudentPanel(dt.Rows[i]);
                StudentListPanel.Children.Add(panel);
            }
        }

        private Border CreateStudentPanel(DataRow row)
        {
            Border border = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(10),
                Background = Brushes.White,
                Margin = new Thickness(5),
                Padding = new Thickness(10)
            };

            StackPanel studentPanel = new StackPanel();

            TextBlock name = new TextBlock
            {
                Text = $"{row["first_name"]} {row["last_name"]}",
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };

            TextBlock details = new TextBlock
            {
                Text = $"ID: {row["student_id"]}\nEmail: {row["email"]}\nCourse: {row["course_program"]}\nYear: {row["year_level"]}",
                FontSize = 12,
                Foreground = Brushes.Gray,
                Margin = new Thickness(0, 5, 0, 5)
            };

            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 5, 0, 0)
            };

            Button updateBtn = new Button
            {
                Content = "Update",
                Width = 70,
                Margin = new Thickness(5, 0, 5, 0),
                BorderThickness = new Thickness(0)
            };
            updateBtn.Click += (s, e) =>
            {
                UpdateStudent(row["student_id"].ToString(), row["course_program"].ToString(), row["year_level"].ToString());
            };

            Button deleteBtn = new Button
            {
                Content = "Delete",
                Width = 70,
                Margin = new Thickness(5, 0, 5, 0),
                Background = Brushes.Red,
                Foreground = Brushes.White,
                BorderThickness = new Thickness(0)
            };
            deleteBtn.Click += (s, e) =>
            {
                DeleteStudent(row["student_id"].ToString());
            };

            buttonPanel.Children.Add(updateBtn);
            buttonPanel.Children.Add(deleteBtn);

            studentPanel.Children.Add(name);
            studentPanel.Children.Add(details);
            studentPanel.Children.Add(buttonPanel);

            border.Child = studentPanel;
            return border;
        }

        private void UpdateStudent(string studentId, string oldCourse, string oldYear)
        {
            string newCourse = Microsoft.VisualBasic.Interaction.InputBox("Enter new course:", "Update Course", oldCourse);
            string newYear = Microsoft.VisualBasic.Interaction.InputBox("Enter new year level:", "Update Year Level", oldYear);

            if (string.IsNullOrWhiteSpace(newCourse) || string.IsNullOrWhiteSpace(newYear))
                return;

            SQL = $"UPDATE users SET course_program = '{newCourse}', year_level = '{newYear}' WHERE student_id = '{studentId}'";
            admin.sqlManager(SQL);

            MessageBox.Show("Student updated successfully!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadStudentList(); 
        }

        private void DeleteStudent(string studentId)
        {
            if (MessageBox.Show($"Are you sure you want to delete student {studentId}?",
                                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                SQL = $"DELETE FROM users WHERE student_id = '{studentId}'";
                admin.sqlManager(SQL);

                MessageBox.Show("Student deleted successfully!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadStudentList(); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LoadStudentList();
            AddNewStudent addNewStudent = new AddNewStudent();
            addNewStudent.Show();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }
    }
}
