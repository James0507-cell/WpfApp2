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

        Admin admin = new Admin();
        String SQL = "";
        String Username = "";
        public StudentManagement()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Helper function to create a styled TextBlock for detail fields.
        /// </summary>
        private TextBlock CreateDetailBlock(string label, string value, FontWeight weight = default)
        {
            if (weight == default) weight = FontWeights.Normal;
            return new TextBlock
            {
                Text = $"{label}: {value}",
                FontSize = 12,
                Margin = new Thickness(0, 2, 0, 2),
                FontWeight = weight,
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")) // Dark blue
            };
        }

        /// <summary>
        /// Displays student user records in a modern card layout.
        /// </summary>
        public void displayUsers(String strQuerry)
        {
            StackPanel targetStackPanel = this.StudentListPanel;

            targetStackPanel.Children.Clear();
            DataTable dt = admin.displayRecords(strQuerry);

            int num = dt.Rows.Count;
            for (int i = 0; i < num; i++)
            {
                // 1. Extract Data from DataTable row
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


                // 2. Create the Card Container (Border)
                Border cardBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.LightGray),
                    BorderThickness = new Thickness(1),
                    Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xF7, 0xFA)), // Very light blue/gray background
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(10, 6, 10, 6),
                    Padding = new Thickness(15),
                    Width = 590,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Effect = new DropShadowEffect
                    {
                        Color = Colors.Gray,
                        Direction = 315,
                        ShadowDepth = 2,
                        BlurRadius = 5,
                        Opacity = 0.3
                    }
                };

                // 3. Main Grid Layout for the Card
                Grid mainLayoutGrid = new Grid();
                mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Header
                mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Divider
                mainLayoutGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Details/Buttons

                // 4. Header Section (Row 0)
                StackPanel headerPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 0, 0, 4) };

                TextBlock txtName = new TextBlock
                {
                    Text = fullname,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D")),
                    Margin = new Thickness(0, 0, 0, 4)
                };
                headerPanel.Children.Add(txtName);

                TextBlock txtIdUsername = new TextBlock
                {
                    Text = $"Student ID: {StudentId} | Username: {username}",
                    FontSize = 13,
                    FontWeight = FontWeights.DemiBold,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4D7399")),
                };
                headerPanel.Children.Add(txtIdUsername);

                Grid.SetRow(headerPanel, 0);
                mainLayoutGrid.Children.Add(headerPanel);

                // 5. Divider (Row 1)
                Separator separator = new Separator
                {
                    Margin = new Thickness(0, 8, 0, 8),
                    Foreground = new SolidColorBrush(Colors.LightGray)
                };
                Grid.SetRow(separator, 1);
                mainLayoutGrid.Children.Add(separator);

                // 6. Details and Button Section (Row 2)
                Grid detailsAndButtonsGrid = new Grid();
                detailsAndButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }); // Details
                detailsAndButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Buttons

                // Details Panel - Two-column layout within the first grid column
                Grid detailsGrid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }, // Left Column (Course/Year)
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }  // Right Column (Contact/Address)
                    },
                    Margin = new Thickness(0, 0, 10, 0)
                };

                // Left Column: Academic
                StackPanel leftPanel = new StackPanel { Margin = new Thickness(0, 0, 10, 0) };
                leftPanel.Children.Add(CreateDetailBlock("Course", course, FontWeights.DemiBold));
                leftPanel.Children.Add(CreateDetailBlock("Year Level", year));

                Grid.SetColumn(leftPanel, 0);
                detailsGrid.Children.Add(leftPanel);

                // Right Column: Contact
                StackPanel rightPanel = new StackPanel { Margin = new Thickness(10, 0, 0, 0) };
                rightPanel.Children.Add(CreateDetailBlock("Email", email));
                rightPanel.Children.Add(CreateDetailBlock("Phone", phone));
                rightPanel.Children.Add(CreateDetailBlock("Address", adress));

                Grid.SetColumn(rightPanel, 1);
                detailsGrid.Children.Add(rightPanel);

                Grid.SetColumn(detailsGrid, 0);
                detailsAndButtonsGrid.Children.Add(detailsGrid);


                // Button Panel 
                StackPanel buttonPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Helper for styled buttons
                Func<string, Brush, RoutedEventHandler, Border> createStyledButton = (string content, Brush bg, RoutedEventHandler handler) =>
                {
                    Button button = new Button
                    {
                        Content = content,
                        Background = Brushes.Transparent,
                        Foreground = Brushes.White,
                        BorderThickness = new Thickness(0),
                        Padding = new Thickness(12, 6, 12, 6),
                        FontWeight = FontWeights.SemiBold,
                        Cursor = Cursors.Hand
                    };

                    button.Click += handler;

                    Border btnBorder = new Border
                    {
                        CornerRadius = new CornerRadius(5),
                        Background = bg,
                        Margin = new Thickness(5, 0, 5, 0),
                        Child = button
                    };
                    return btnBorder;
                };

                // Update Button (Blue)
                var updateBtnBorder = createStyledButton("Update",
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2196F3")), // Blue 
                    (s, e) =>
                    {
                        Username = username;
                        UpdateStudent(Username);
                    });

                // Delete Button (Red)
                var deleteBtnBorder = createStyledButton("Delete",
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF44336")), // Red
                    (s, e) =>
                    {
                        DeleteStudent(StudentId);
                    });

                buttonPanel.Children.Add(updateBtnBorder);
                buttonPanel.Children.Add(deleteBtnBorder);

                Grid.SetColumn(buttonPanel, 1);
                detailsAndButtonsGrid.Children.Add(buttonPanel);

                Grid.SetRow(detailsAndButtonsGrid, 2);
                mainLayoutGrid.Children.Add(detailsAndButtonsGrid);

                // 7. Add the Card to the Main StackPanel
                cardBorder.Child = mainLayoutGrid;
                targetStackPanel.Children.Add(cardBorder);
            }
        }

        private void UpdateStudent(String usernmae)
        {
            UpdateStudent updateStudent = new UpdateStudent(Username);
            updateStudent.Show();
        }

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

            int totalActiveStudents = admin.GetActiveStudentCount();
            lblActiveStudent.Content = totalActiveStudents.ToString();

            int totalStudents = admin.GetTotalStudentCount();
            lblTotalstudent.Content = totalStudents.ToString();

            int totaalCoursePrograms = admin.getTotalProgram();
            lblPrograms.Content = totaalCoursePrograms.ToString();

            int totalmedicinereq = admin.GetMedicineStatusCount();
            lblMedicalAlerts.Content = totalmedicinereq;

            SQL = "SELECT * FROM users WHERE role = 'Student'";
            displayUsers(SQL);
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
            int totalActiveStudents = admin.GetActiveStudentCount();
            lblActiveStudent.Content = totalActiveStudents.ToString();

            int totalStudents = admin.GetTotalStudentCount();
            lblTotalstudent.Content = totalStudents.ToString();

            int totaalCoursePrograms = admin.getTotalProgram();
            lblPrograms.Content = totaalCoursePrograms.ToString();

            int totalmedicinereq = admin.GetMedicineStatusCount();
            lblMedicalAlerts.Content = totalmedicinereq;

        }
    }
}
