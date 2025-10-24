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
        String Username = MainWindow.Username;
        int id;
        String studUsername = "";
        public StudentManagement()
        {
            InitializeComponent();
        }

       
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

        
        public void displayUsers(String strQuerry)
        {
            AdminStudent adminStudent = new AdminStudent(id);
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
                studUsername = username;


                Border cardBorder = adminStudent.studentPanel(username, email, adress, StudentId, course, year, fullname, phone);
                targetStackPanel.Children.Add(cardBorder);
            }
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
            int totalComputerscience = admin.Accountancy();
            accountancy.Content = totalComputerscience + " students";

            int totalManagementAccounting = admin.ManagementAccounting();
            managementaccounting.Content = totalManagementAccounting + " students";
            
            int totoalEntrepreneurship = admin.Entrepreneurship();
            Entrepreneurship.Content = totoalEntrepreneurship + " students";

            int totaltourismManagement = admin.TourismManagement();
            tourismManagement.Content = totaltourismManagement + " students";

            int totalCommunication = admin.Communication();
            Communication.Content = totalCommunication + " students";   

            int totalMultimediaArt = admin.MultiMediaArts();
            multiMediaArts.Content = totalMultimediaArt + " students";

            int totalComputerScience = admin.ComputerScience();
            compueterscience.Content = totalComputerScience + " students";

            int totalInformationsystem = admin.Informationsystem(); 
            informationsystem.Content = totalInformationsystem + " students";

            int totalemc = admin.entertaimentmultimediacomputing();
            entertainmentmultimediacomputing.Content = totalemc + " students";

            int totalarchitecture = admin.architecture();
            architecture.Content = totalarchitecture + " students";

            int totalchemicalengineering = admin.chemicalEnginerring();
            chemicalengineering.Content = totalchemicalengineering + " students";

            int totalcivilengineering = admin.civilEngineering();
            civilengineering.Content = totalcivilengineering + " students";

            int totalcomputerengineering = admin.computerEngineering();
            compueterengineering.Content = totalcomputerengineering + " students";

            int totalelectricalengineering = admin.electricalEngineering();
            electricalengineering.Content = totalelectricalengineering + " students";

            int totalelectronicengineering = admin.electronicsEngineering();
            electronicsengineering.Content = totalelectronicengineering + " students";

            int totalindustrialengineering = admin.industrialEngineering();
            industrialengineering.Content = totalindustrialengineering + " students";

            int totalmechanicalengineering = admin.mechanicalEngineering();
            mechanicalengineering.Content = totalmechanicalengineering + " students";

            int totalbiology = admin.biology();
            biology.Content = totalbiology + " students";

            int totalpharmcy = admin.pharmacy();
            pharmacy.Content = totalpharmcy + " students";

            int totalphysicaltherapy = admin.physicalteraphy();
            physicaltherapy.Content = totalphysicaltherapy + " students";

            int totalpyschology = admin.psychology();
            psycholoyg.Content = totalpyschology + " students";



            setId(Username);
            SQL = "SELECT * FROM users WHERE role = 'Student'";
            displayUsers(SQL);

            DataTable dtYear = admin.displayRecords("SELECT * FROM year_levels");
            foreach (DataRow row in dtYear.Rows)
            {
                cmbYear.Items.Add(row["level_name"].ToString());
            }
            cmbYear.Items.Insert(0, "All Year Levels");
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
            //programs specific
            int totalComputerscience = admin.Accountancy();
            accountancy.Content = totalComputerscience + " students";

            int totalManagementAccounting = admin.ManagementAccounting();
            managementaccounting.Content = totalManagementAccounting + " students";

            int totoalEntrepreneurship = admin.Entrepreneurship();
            Entrepreneurship.Content = totoalEntrepreneurship + " students";

            int totaltourismManagement = admin.TourismManagement();
            tourismManagement.Content = totaltourismManagement + " students";

            int totalCommunication = admin.Communication();
            Communication.Content = totalCommunication + " students";

            int totalMultimediaArt = admin.MultiMediaArts();
            multiMediaArts.Content = totalMultimediaArt + " students";

            int totalComputerScience = admin.ComputerScience();
            compueterscience.Content = totalComputerScience + " students";

            int totalInformationsystem = admin.Informationsystem();
            informationsystem.Content = totalInformationsystem + " students";

            int totalemc = admin.entertaimentmultimediacomputing();
            entertainmentmultimediacomputing.Content = totalemc + " students";

            int totalarchitecture = admin.architecture();
            architecture.Content = totalarchitecture + " students";

            int totalchemicalengineering = admin.chemicalEnginerring();
            chemicalengineering.Content = totalchemicalengineering + " students";

            int totalcivilengineering = admin.civilEngineering();
            civilengineering.Content = totalcivilengineering + " students";

            int totalcomputerengineering = admin.computerEngineering();
            compueterengineering.Content = totalcomputerengineering + " students";

            int totalelectricalengineering = admin.electricalEngineering();
            electricalengineering.Content = totalelectricalengineering + " students";

            int totalelectronicengineering = admin.electronicsEngineering();
            electronicsengineering.Content = totalelectronicengineering + " students";

            int totalindustrialengineering = admin.industrialEngineering();
            industrialengineering.Content = totalindustrialengineering + " students";

            int totalmechanicalengineering = admin.mechanicalEngineering();
            mechanicalengineering.Content = totalmechanicalengineering + " students";

            int totalbiology = admin.biology();
            biology.Content = totalbiology + " students";

            int totalpharmcy = admin.pharmacy();
            pharmacy.Content = totalpharmcy + " students";

            int totalphysicaltherapy = admin.physicalteraphy();
            physicaltherapy.Content = totalphysicaltherapy + " students";

            int totalpyschology = admin.psychology();
            psycholoyg.Content = totalpyschology + " students";

        }
        public void setId(String username)
        {
            SQL = $"select user_id from users where username = '{username}'";
            DataTable dt = admin.displayRecords(SQL);
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
    }
}
