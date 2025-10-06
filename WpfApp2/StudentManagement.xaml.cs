using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    public partial class StudentManagement : Window
    {
        Admin admin = new Admin();
        String SQL = "";
        public StudentManagement()
        {
            InitializeComponent();
            //LoadStudentList(); 
            
        }
        public void displayUsers(String strQuerry)
        {

            StudentListPanel.Children.Clear();
            DataTable dt = admin.displayRecors(strQuerry);

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
                    Text = fullname,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold
                };
                TextBlock details = new TextBlock
                {
                    Text = StudentId + "\n" + email + "\n" + course + "\n" + year + "\n" + phone + "\n" + adress,
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
                    UpdateStudent(StudentId, course, year, fullname, email, adress, phone);
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
                    DeleteStudent(StudentId);
                };

                buttonPanel.Children.Add(updateBtn);
                buttonPanel.Children.Add(deleteBtn);


                studentPanel.Children.Add(name);
                studentPanel.Children.Add(details);
                studentPanel.Children.Add(buttonPanel);
                border.Child = studentPanel;
                StudentListPanel.Children.Add(border);



            }
        }
        /*private void LoadStudentList()
        {
            StudentListPanel.Children.Clear();

            SQL = "SELECT first_name, last_name, student_id, email, course_program, year_level FROM users WHERE role = 'Student'";
            DataTable dt = admin.displayRecors(SQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Border panel = CreateStudentPanel(dt.Rows[i]);
                StudentListPanel.Children.Add(panel);
            }
        }*/

        /*private Border CreateStudentPanel(DataRow row)
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
        } */

        private void UpdateStudent(string studentId, string oldCourse, string oldYear, string fullname, string email, string address, string phone)
        {
            // Floating panel window
            Window updateWindow = new Window
            {
                Title = "Update Student",
                Width = 380,
                Height = 520,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                Owner = this,
                Background = Brushes.White,

                WindowStyle = WindowStyle.None
            };

            Border mainBorder = new Border
            {
                CornerRadius = new CornerRadius(20),
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.LightGray,
                Background = Brushes.White,
                Padding = new Thickness(20),
                Margin = new Thickness(10),
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = Colors.Black,
                    Direction = 270,
                    ShadowDepth = 5,
                    Opacity = 0.3,
                    BlurRadius = 10
                }
            };

            StackPanel panel = new StackPanel { Margin = new Thickness(10) };

            TextBlock header = new TextBlock
            {
                Text = $"Update: {fullname}",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 15),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            panel.Children.Add(header);

            // Helper for rounded TextBox
            Func<string, TextBox> createRoundedTextBox = (string text) =>
            {
                TextBox box = new TextBox
                {
                    Text = text,
                    Margin = new Thickness(0, 5, 0, 10),
                    Padding = new Thickness(10),
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness(0)
                };

                Border border = new Border
                {
                    CornerRadius = new CornerRadius(10),
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    Child = box
                };

                panel.Children.Add(border);
                return box;
            };

            // Labels + fields
            panel.Children.Add(new TextBlock { Text = "Course Program:", FontWeight = FontWeights.SemiBold });
            TextBox courseBox = createRoundedTextBox(oldCourse);

            panel.Children.Add(new TextBlock { Text = "Year Level:", FontWeight = FontWeights.SemiBold });
            TextBox yearBox = createRoundedTextBox(oldYear);

            panel.Children.Add(new TextBlock { Text = "Email:", FontWeight = FontWeights.SemiBold });
            TextBox emailBox = createRoundedTextBox(email);

            panel.Children.Add(new TextBlock { Text = "Address:", FontWeight = FontWeights.SemiBold });
            TextBox addressBox = createRoundedTextBox(address);

            panel.Children.Add(new TextBlock { Text = "Phone Number:", FontWeight = FontWeights.SemiBold });
            TextBox phoneBox = createRoundedTextBox(phone);

            // Helper for rounded button
            Func<string, Brush, Brush, RoutedEventHandler, Border> createRoundedButton = (string content, Brush bg, Brush fg, RoutedEventHandler handler) =>
            {
                Button button = new Button
                {
                    Content = content,
                    Background = Brushes.Transparent,
                    Foreground = fg,
                    BorderThickness = new Thickness(0),
                    Padding = new Thickness(8, 5, 8, 5),
                    FontWeight = FontWeights.Bold,
                    Cursor = System.Windows.Input.Cursors.Hand
                };

                button.Click += handler;

                Border btnBorder = new Border
                {
                    CornerRadius = new CornerRadius(10),
                    Background = bg,
                    Margin = new Thickness(5, 10, 5, 0),
                    Child = button
                };

                return btnBorder;
            };

            // Buttons
            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var saveBtnBorder = createRoundedButton("Save Changes",
                new SolidColorBrush(Color.FromRgb(52, 152, 219)),
                Brushes.White,
                (s, e) =>
                {
                    string newCourse = courseBox.Text;
                    string newYear = yearBox.Text;
                    string newEmail = emailBox.Text;
                    string newAddress = addressBox.Text;
                    string newPhone = phoneBox.Text;

                    string updateSQL = $"UPDATE users SET " +
                                       $"course_program = '{newCourse}', " +
                                       $"year_level = '{newYear}', " +
                                       $"email = '{newEmail}', " +
                                       $"address = '{newAddress}', " +
                                       $"phone_number = '{newPhone}' " +
                                       $"WHERE student_id = '{studentId}'";

                    admin.sqlManager(updateSQL);
                    MessageBox.Show("Student updated successfully!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);

                    updateWindow.Close();
                    displayUsers("SELECT * FROM users WHERE role = 'Student'");
                });

            var cancelBtnBorder = createRoundedButton("Cancel",
                Brushes.LightGray,
                Brushes.Black,
                (s, e) => updateWindow.Close());

            buttonPanel.Children.Add(saveBtnBorder);
            buttonPanel.Children.Add(cancelBtnBorder);
            panel.Children.Add(buttonPanel);

            mainBorder.Child = panel;
            updateWindow.Content = mainBorder;

            updateWindow.ShowDialog();
        }




        /*private void DeleteStudent(string studentId)
        {
            if (MessageBox.Show($"Are you sure you want to delete student {studentId}?",
                                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                SQL = $"DELETE FROM users WHERE student_id = '{studentId}'";
                admin.sqlManager(SQL);

                MessageBox.Show("Student deleted successfully!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadStudentList(); 
            }
        }*/

        private void DeleteStudent(String studentId)
        {
            SQL = $"Delete from users where student_id = '{studentId}'";
            MessageBoxResult result = MessageBox.Show(
            "Are you sure you want to delete this user",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
    );

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Account deleted successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                admin.sqlManager(SQL);
            }
            else
            {
                MessageBox.Show("Action canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            displayUsers($"SELECT * FROM users WHERE role = 'Student'");
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LoadStudentList();
            AddNewStudent addNewStudent = new AddNewStudent();
            addNewStudent.Show();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQL = "SELECT * FROM users WHERE student_id LIKE '%" + txtSearch.Text + "%'";
            displayUsers(SQL);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQL = "SELECT * FROM users WHERE role = 'Student'";
            displayUsers(SQL);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQL = "SELECT * FROM users WHERE student_id LIKE '%" + txtSearch.Text + "%'";
            displayUsers(SQL);
        }
    }
}
