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
using System.Data.SqlClient;

namespace CarSalesApp
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (IsValidUser(username, password))
            {
                MainWindow mainMenu = new MainWindow();
                mainMenu.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка входа. Проверьте свои учетные данные.");
            }
        }

        private bool IsValidUser(string username, string password)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM UserTbl WHERE Uname = @Username AND Upass = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int userCount = (int)command.ExecuteScalar();

                return userCount > 0;
            }
        }

        private bool AddNewUser(string username, string password)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO UserTbl (Uname, Upass) VALUES (@Username, @Password)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox1.Text;
            string password = PasswordBox1.Password;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (AddNewUser(username, password))
                {
                    MessageBox.Show("Пользователь зарегистрирован успешно.");
                }
                else
                {
                    MessageBox.Show("Ошибка регистрации пользователя.");
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль.");
            }
        }
    }
}