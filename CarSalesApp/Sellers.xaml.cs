using System;
using System.Data.SqlClient;
using System.Windows;

namespace CarSalesApp
{
    public partial class Sellers : Window
    {
        public Sellers()
        {
            InitializeComponent();
        }

        private void SaveSellerButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string address = AddressTextBox.Text;
            string phone = PhoneTextBox.Text;

            if (SaveSellerToDatabase(name, address, phone))
            {
                MessageBox.Show("Продавец сохранен успешно.");
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении продавца.");
            }
        }

        private bool SaveSellerToDatabase(string name, string address, string phone)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CarRentaldb.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO SellersTbl (SellerName, SellerAddres, SellerPhone) VALUES (@Name, @Address, @Phone)";
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
