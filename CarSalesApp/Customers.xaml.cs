using System;
using System.Data.SqlClient;
using System.Windows;

namespace CarSalesApp
{
    public partial class Customers : Window
    {
        public Customers()
        {
            InitializeComponent();
        }

        private void SaveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            // Получите данные о покупателе из текстовых полей
            string name = NameTextBox.Text;
            string address = AddressTextBox.Text;
            string phone = PhoneTextBox.Text;

            if (SaveCustomerToDatabase(name, address, phone))
            {
                MessageBox.Show("Покупатель сохранен успешно.");
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении покупателя.");
            }
        }

        private bool SaveCustomerToDatabase(string name, string address, string phone)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO CustomerTbl (CustName, CustAddres, CustPhone) VALUES (@Name, @Address, @Phone)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Phone", phone);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}