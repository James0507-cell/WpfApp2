using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    internal class AdminActivity
    {
        private MySqlConnection dbConn;
        private MySqlCommand dbCommand;
        private MySqlDataAdapter da;
        private DataTable dt;
        private String username = MainWindow.Username;
        private int id;

        private string strConn = "server=localhost;user id=root;password=;database=db_medicaremmcm";

        public AdminActivity(int id)
        {
            this.id = id;
        }
        public void dbConnection()
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            MessageBox.Show("Connection Successful");
            dbConn.Close();
        }

        public DataTable displayRecords(string query)
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            da = new MySqlDataAdapter(query, dbConn);
            dt = new DataTable();
            da.Fill(dt);
            dbConn.Close();
            return dt;
        }

        public void sqlManager(string query)
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            dbCommand = new MySqlCommand(query, dbConn);
            dbCommand.ExecuteNonQuery();
            dbConn.Close();
        }

        public Border activityPanel(String activityID, String username, String type, String description, String dateTime, String id)
        {
            Brush darkBlueBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00104D"));
            Brush lightGrayBrush = new SolidColorBrush(Colors.Gray);
            Brush lightBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xF5, 0xF7, 0xFA));
            Brush idTagBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0F0F0"));

            Border cardBorder = new Border
            {
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(1),
                Background = lightBackground,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10, 4, 10, 4),
                Padding = new Thickness(12, 8, 12, 8), // Increased padding for a better look
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };

            StackPanel activityContent = new StackPanel();


            Grid headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // For Username
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // For ID Tag
            headerGrid.Margin = new Thickness(0, 0, 0, 4);

            TextBlock txtUsername = new TextBlock
            {
                Text = username + " ID:" + id,
                FontWeight = FontWeights.Bold,
                FontSize = 10,
                Foreground = darkBlueBrush,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(txtUsername, 0);
            headerGrid.Children.Add(txtUsername);

            Border idTag = new Border
            {
                Background = idTagBackground,
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(8, 2, 8, 2),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Child = new TextBlock
                {
                    Text = $"ID: {activityID}",
                    Foreground = Brushes.Gray,
                    FontWeight = FontWeights.Normal,
                    FontSize = 9
                }
            };
            Grid.SetColumn(idTag, 1);
            headerGrid.Children.Add(idTag);

            activityContent.Children.Add(headerGrid);

            TextBlock txtActivityType = new TextBlock
            {
                Text = $"Action: {type}",
                FontWeight = FontWeights.SemiBold,
                FontSize = 9,
                Foreground = darkBlueBrush,
                Margin = new Thickness(0, 0, 0, 2)
            };
            activityContent.Children.Add(txtActivityType);

            TextBlock txtDateTime = new TextBlock
            {
                Text = $"⌚ {dateTime}",
                FontSize = 9,
                Foreground = lightGrayBrush,
                Margin = new Thickness(0, 0, 0, 6)
            };
            activityContent.Children.Add(txtDateTime);

            TextBlock txtDescription = new TextBlock
            {
                Text = description,
                FontSize = 9,
                Foreground = darkBlueBrush,
                TextWrapping = TextWrapping.Wrap
            };
            activityContent.Children.Add(txtDescription);

            cardBorder.Child = activityContent;

            return cardBorder;
        }
    }
}
